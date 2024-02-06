using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
	public class Parser
	{
		readonly RichTextBox input;
		readonly List<string> words = new List<string>();
		readonly Dictionary<string, string> variables = new Dictionary<string, string>();
		int currentRow = 0;
		int current = 0;

		public Parser(RichTextBox input)
		{
			words = new List<string>();
			variables = new Dictionary<string, string>();
			this.input = input;
			foreach (var line in input.Lines)
			{
				foreach (var word in line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList())
					words.Add(word);
				words.Add(Environment.NewLine);
			}
			words.RemoveAt(words.Count - 1);
		}

		public string Parse()
		{
			if (IsCurrentEqualTo("Begin")) current++;
			else Error("Язык должен начинаться с \"Begin\"");

			ProcessSets();
			ProcessOperators();

			if (!IsCurrentEqualTo("End")) Error("Язык должен заканчиваться на \"End\"");
			current++;
			if (current < words.Count) Error("Встречен текст после \"End\"");

			return GenerateResult();
		}

		private void ProcessSets()
		{
			do
			{
				CheckSet();
			}
			while (IsCurrentEqualTo(","));
			if (IsCurrentEqualTo("First") || IsCurrentEqualTo("Second"))
				Error("Пропущен разделитель \",\"");
		}

		private void CheckSet()
		{
			if (IsCurrentEqualTo(",")) Skip(1, true);
			if (IsCurrentEqualTo("First") || IsCurrentEqualTo("Second"))
			{
				Skip(1, true);
				CheckNumbers();
				if (IsCurrentEqualTo("Add") || IsCurrentEqualTo("Mult"))
				{
					var add = IsCurrentEqualTo("Add") ? "Add" : "Mult";
					Skip(1, true);
					if (IsCurrentTypeEqualTo(Type.Real)) Skip(1, true);
					else Error($"После \"{add}\" должно идти вещестенное число");
				}
				else Error("Пропущено \"Add\" или \"Mult\"");
			}
			else Error("Множество должно начинаться с \"First\" или \"Second\"");
		}

		private void CheckNumbers()
		{
			var wasInt = IsCurrentTypeEqualTo(Type.Int);
			if (!wasInt) Error("Перечисление должно начинаться с целого числа");
			Skip(1, true);

			foreach (var word in words.Skip(current))
			{
				if (word == "Add" || word == "Mult")
				{
					Skip(1, false);
					if (!IsCurrentTypeEqualTo(Type.Int))
						Error("Перечисление не может заканчиваться разделителем");
					Skip(1, true);
					break;
				}

				switch (Typer.GetTypeOf(word))
				{
					case Type.Int:
						if (wasInt) Error("Требуется разделитель");
						else wasInt = true;
						break;
					case Type.Separator:
						if (wasInt) wasInt = false;
						else Error("Пропущено целое число");
						break;
					case Type.Real:
						Error("Вещественное число недопустимо в контексте данного перечисления");
						break;
					case Type.Variable:
						Error("Переменная недопустима в контексте данного перечисления");
						break;
					case Type.Operator:
						Error("Знак математической операции недопустим в контексте данного перечисления");
						break;
					case Type.OpenBracket:
						Error("Открывающая скобка недопустима в контексте данного перечисления");
						break;
					case Type.CloseBracket:
						Error("Закрывающая скобка недопустима в контексте данного перечисления");
						break;
					case Type.NewLine:
						currentRow++;
						break;
					case Type.WTF:
						Error($"Не удалось определить тип слова \"{word}\"");
						break;
					default:
						break;
				}
				current++;
			}

		}

		private void ProcessOperators()
		{
			do
			{
				CheckOperator();
			}
			while (IsCurrentEqualTo(";"));
		}

		private void CheckOperator()
		{
			if (IsCurrentEqualTo(";")) Skip(1, true);

			CheckMarks();
			var varName = CheckVar();
			var segment = CheckMath();

			string result = string.Empty;

			var expression = string.Join(" ", words
				.Skip(segment.Item1)
				.Take(segment.Item2 - segment.Item1 + 1)
				.ToList())
				.Replace("!", "").Replace("|", "+").Replace("&", "*")
				.Replace("[", "(").Replace("]", ")")
				.Replace("\t", "").Replace(Environment.NewLine, "");
			foreach (var key in variables.Keys)
				expression = expression.Replace(key, variables[key]);
			try
			{
				result = Convert.ToDouble(new DataTable().Compute(expression, "")).ToString().Replace(',', '.');
			}
			catch (OverflowException)
			{
				Error("Возникла ошибка в процессе вычислений. Полученные вычисления превысели Int64");
			}
			catch (DivideByZeroException)
			{
				Error("Возникла ошибка в процессе вычислений. Деление на ноль");
			}
			catch (EvaluateException)
			{
				Error("Возникла ошибка в процессе вычислений. Полученные вычисления превысели Int64");
			}
			catch (SyntaxErrorException e)
			{
				Error(e.Message);
			}
			variables[varName] = result;
		}

		private void CheckMarks()
		{
			if (IsCurrentEqualTo(":")) Error("Пропущены метки");
			if (!IsCurrentTypeEqualTo(Type.Variable))
			{
				foreach (var word in words.Skip(current))
				{
					if (word == ":")
					{
						Skip(1, true);
						break;
					}

					switch (Typer.GetTypeOf(word))
					{
						case Type.Separator:
							Error($"Разделитель \"{word}\" недопустим в контексте данного перечисления");
							break;
						case Type.Real:
							Error("Вещественное число недопустимо в контексте данного перечисления");
							break;
						case Type.Variable:
							Error("Переменная недопустима в контексте данного перечисления");
							break;
						case Type.Operator:
							Error("Знак математической операции недопустим в контексте данного перечисления");
							break;
						case Type.OpenBracket:
							Error("Открывающая скобка недопустима в контексте данного перечисления");
							break;
						case Type.CloseBracket:
							Error("Закрывающая скобка недопустима в контексте данного перечисления");
							break;
						case Type.NewLine:
							currentRow++;
							break;
						case Type.WTF:
							Error($"Не удалось определить тип слова \"{word}\"");
							break;
						default:
							break;
					}
					current++;
				}
			}
		}

		private string CheckVar()
		{
			var name = string.Empty;
			if (IsCurrentTypeEqualTo(Type.Variable))
			{
				name = words[current];
				current++;
				if (IsCurrentEqualTo("="))
				{
					Skip(1, true);
					if (IsCurrentTypeEqualTo(Type.Operator) && ((words[current] == "!" || words[current] == "-") == false))
						Error($"Правая часть не может начинаться с знака математической операции \"{words[current]}\"");
					if (IsCurrentTypeEqualTo(Type.CloseBracket))
						Error("Правая часть не может начинаться с закрывающей скобки");
					if (IsNextEqualTo(":"))
						Error("Пропущена правая часть");
				}
				else Error("После переменной должно идти \"=\"");
			}
			else Error("Пропущена переменная");

			return name;
		}

		private Tuple<int, int> CheckMath()
		{
			int start = current;
			var bracketStack = new Stack<string>();

			foreach (var word in words.Skip(current))
			{
				if (word == ";" || word == "End")
				{
					if (word == ";" && IsNextEqualTo("End")) Error("Перечисление не может заканчиваться на \";\"");

					Skip(1, false);
					switch (Typer.GetTypeOf(words[current]))
					{
						case Type.Operator:
							Error("Правая часть не может заканчиваться знаком математической операции");
							break;
						case Type.OpenBracket:
							Error("Правая часть не может заканчиваться открывающей скобкой");
							break;
						default:
							break;
					}
					if (bracketStack.Count() > 0)
						Error("Открывающих скобок больше, чем закрывающих");
					var end = current;
					Skip(1, true);

					return Tuple.Create(start, end);
				}

				var prevWord = GetPrev();
				var prevType = Typer.GetTypeOf(prevWord);

				switch (Typer.GetTypeOf(word))
				{
					case Type.Variable:
						if (!variables.Keys.Contains(word))
							Error("Обращение к неинициализированной переменной");
						switch (prevType)
						{
							case Type.Int:
								Error("Число и переменная не могут стоять подряд");
								break;
							case Type.Variable:
								Error("Две и более переменные не могут стоять подряд");
								break;
							case Type.CloseBracket:
								Error("Переменная не может идти после закрывающей скобки");
								break;
							default:
								break;
						}
						break;
					case Type.Int:
						switch (prevType)
						{
							case Type.Int:
								Error("Два и более числа не могут стоять подряд");
								break;
							case Type.Variable:
								Error("Переменная и число не могут стоять подряд");
								break;
							case Type.CloseBracket:
								Error("Число не может идти после закрывающей скобки");
								break;
							default:
								break;
						}
						break;
					case Type.Operator:
						switch (prevType)
						{
							case Type.Operator:
								if ((word == "!" && prevWord != "!") == false)
									Error("Два и более знака математической операции не могут идти подряд");
								break;
							case Type.OpenBracket:
								if ((word == "-" || word == "!") == false)
									Error($"Знак математической операции \"{word}\" не может идти после открывающей скобки");
								break;
							default:
								break;
						}
						break;
					case Type.OpenBracket:
						switch (prevType)
						{
							case Type.Variable:
								Error("Открывающая скобка не может идти после переменной");
								break;
							case Type.Int:
								Error("Открывающая скобка не может идти после числа");
								break;
							case Type.CloseBracket:
								Error("Открывающая скобка не может идти после закрывающей скобки");
								break;
							default:
								break;
						}
						bracketStack.Push(word);
						break;
					case Type.CloseBracket:
						switch (prevType)
						{
							case Type.Operator:
								Error("Закрывающая скобка не может идти после знака математической операции");
								break;
							case Type.OpenBracket:
								Error("Закрывающая скобка не может идти после открывающей скобки");
								break;
							case Type.WTF:
								break;
							default:
								break;
						}
						if (bracketStack.Count > 0)
							bracketStack.Pop();
						else
							Error("Лишняя закрывающая скобка");
						break;
					case Type.Separator:
						Error("Разделитель недопустим в контексте правой части");
						break;
					case Type.WTF:
						Error($"Не удалось определить тип слова \"{word}\"");
						break;
					case Type.NewLine:
						currentRow++;
						break;
					default:
						break;
				}
				current++;
			}

			current--;
			Error("В правой части встречен конец строки");
			return null;
		}


		private bool IsCurrentEqualTo(string value)
		{
			if (current >= words.Count)
			{
				Skip(1, false);
				Error($"Программа должна заканчиваться словом \"End\"");
			}	

			if (IsCurrentTypeEqualTo(Type.NewLine)
				|| IsCurrentTypeEqualTo(Type.Tab))
			{
				if (IsCurrentTypeEqualTo(Type.NewLine)) currentRow++;
				current++;
				return IsCurrentEqualTo(value);
			}

			if (words[current] != value)
				return false;
			return true;
		}

		private bool IsCurrentTypeEqualTo(Type typeToFind)
		{
			if (current >= words.Count)
			{
				Skip(1, false);
				Error($"Программа должна заканчиваться словом \"End\"");
			}

			if (Typer.GetTypeOf(words[current]) != typeToFind)
				return false;
			return true;
		}

		private string GetPrev()
		{
			var count = 0;
			current--;
			while (IsCurrentTypeEqualTo(Type.NewLine) || IsCurrentTypeEqualTo(Type.Tab))
			{
				current--;
				count++;
			}
			var result = words[current];
			current++;
			for (int i = 0; i < count; i++)
				current++;
			return result;
		}

		private bool IsNextEqualTo(string value)
		{
			var count = 0;
			current++;
			while (IsCurrentTypeEqualTo(Type.NewLine) || IsCurrentTypeEqualTo(Type.Tab))
			{
				current++;
				count++; 
			}
			var result = words[current] == value;
			current--;
			for (int i = 0; i < count; i++)
				current--;
			return result;
		}

		private void Skip(int count, bool forvard)
		{
			for (int i = 0; i < count; i++)
			{
				current += forvard ? 1 : -1;
				while (IsCurrentTypeEqualTo(Type.NewLine) || IsCurrentTypeEqualTo(Type.Tab))
				{
					if (IsCurrentTypeEqualTo(Type.NewLine)) currentRow += forvard ? 1 : -1;
					current += forvard ? 1 : -1;
				}
			}
		}

		public void Error(string msg)
		{
			var sb = new StringBuilder();
			for (int i = 0; i < current; i++) sb.Append(words[i] + " ");
			var str = sb.ToString();
			var ind = currentRow > 0 ? str.LastIndexOf(Environment.NewLine) + 3 : 0;
			var substr = str.Substring(ind);

			var index = input.GetFirstCharIndexFromLine(currentRow);
			index += substr.Contains("\t") ? substr.Length + 1 : substr.Length;
			var length = words[current < words.Count ? current : current - 1].Length;
			throw new MyException(index, length, msg);
		}

		private string GenerateResult()
		{
			var result = "Результаты вычислений:";
			foreach (var key in variables.Keys)
				result += $"{Environment.NewLine}{key} = {variables[key]}";
			return result;
		}
	}
}