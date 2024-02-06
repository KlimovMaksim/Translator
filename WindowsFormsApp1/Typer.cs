using System;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
	public static class Typer
	{
		public static Type GetTypeOf(string value)
		{
			if (IsNewLine(value)) return Type.NewLine;
			if (IsTab(value)) return Type.Tab;
			if (IsOpenBracket(value)) return Type.OpenBracket;
			if (IsCloseBracket(value)) return Type.CloseBracket;
			if (IsOperator(value)) return Type.Operator;
			if (IsSeparator(value)) return Type.Separator;
			if (IsReal(value)) return Type.Real;
			if (IsInt(value)) return Type.Int;
			if (IsVariable(value)) return Type.Variable;

			return Type.WTF;
		}

		public static bool IsNewLine(string value)
		{
			return value == Environment.NewLine;
		}

		public static bool IsTab(string value)
		{
			return value == "\t";
		}

		public static bool IsSeparator(string value)
		{
			var list = new[] { ":", ";", ","};
			return list.Contains(value);
		}

		public static bool IsOperator(string value)
		{
			var list = new[] { "+", "-", "*", "/", "&", "|", "!" };
			return list.Contains(value);
		}

		public static bool IsOpenBracket(string value)
		{
			return value == "(";
		}

		public static bool IsCloseBracket(string value)
		{
			return value == ")";
		}

		public static bool IsVariable(string value)
		{
			value = value.ToUpper();
			if ((value[0] >= 'A' && value[0] <= 'Z') == false)
				return false;
			for (int i = 1; i < value.Length; i++)
			{
				if ((value[i] >= '0' && value[i] <= '9'
					|| value[0] >= 'A' && value[0] <= 'Z') == false)
					return false;
			}
			return true;
		}

		public static bool IsReal(string value)
		{
			if (!value.Contains(".")) return false;
			if (value.Count(ch => ch == '.') != 1) return false;
			var parts = value.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
			if (parts.Length != 2) return false;
			foreach (var part in parts)
				if (IsInt(part) == false) return false;
			return true;
		}

		public static bool IsInt(string value)
		{
			foreach (char ch in value.ToUpper())
			{
				if ((ch >= '0' && ch <= '9') == false)
					return false;
			}
			return true;
		}
	}
}