using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Text;

namespace WindowsFormsApp1
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			bnfText.Text = File.ReadAllText("BNF.txt");
		}

		private void InputTextChanged(object sender, EventArgs e)
		{
			input.DeselectAll();
			linesCounter.Text = string.Empty;
			for (int i = 1; i <= input.Lines.Length; i++)
				linesCounter.Text += $"{i}.\n";
		}

		private void F5Pressed(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F5)
				button1.PerformClick();
		}

		private void ButtonClicked(object sender, EventArgs e)
		{
			OrganizeIO();
			try
			{
				if (input.Text != string.Empty)
					output.Text = new Parser(input).Parse();
				else throw new MyException(0, 0, "Передан пустой текст");
			}
			catch (MyException exc)
			{
				output.Text = exc.Message;
				input.Select(exc.Index, exc.Length);
				input.SelectionBackColor = Color.PaleVioletRed;
			}
		}

		private void OrganizeIO()
		{
			output.Text = string.Empty;
			input.Text = input.Text.Trim();
			var symbols = ";:=+-*/!&|()[],\t";
			foreach (var symbol in symbols)
				input.Text = input.Text.Replace(symbol.ToString(), $" {symbol} ");
			input.Text = RemoveMultipleSpaces(input.Text);
		}

		private string RemoveMultipleSpaces(string input)
		{
			var output = new StringBuilder();

			bool previousWasSpace = false;
			foreach (char c in input)
			{
				if (c == ' ')
				{
					if (!previousWasSpace) output.Append(c);
					previousWasSpace = true;
				}
				else
				{
					output.Append(c);
					previousWasSpace = false;
				}
			}

			return output.ToString();
		}
	}
}