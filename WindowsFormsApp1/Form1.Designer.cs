namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
			this.output = new System.Windows.Forms.TextBox();
			this.input = new System.Windows.Forms.RichTextBox();
			this.linesCounter = new System.Windows.Forms.RichTextBox();
			this.bnfText = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// output
			// 
			this.output.BackColor = System.Drawing.Color.LightCyan;
			this.output.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.output.Cursor = System.Windows.Forms.Cursors.No;
			this.output.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.output.ForeColor = System.Drawing.SystemColors.InfoText;
			this.output.Location = new System.Drawing.Point(11, 517);
			this.output.Margin = new System.Windows.Forms.Padding(2);
			this.output.Multiline = true;
			this.output.Name = "output";
			this.output.ReadOnly = true;
			this.output.Size = new System.Drawing.Size(1242, 153);
			this.output.TabIndex = 1;
			// 
			// input
			// 
			this.input.AcceptsTab = true;
			this.input.AutoWordSelection = true;
			this.input.BackColor = System.Drawing.Color.LightCyan;
			this.input.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.input.DetectUrls = false;
			this.input.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.input.ForeColor = System.Drawing.SystemColors.InfoText;
			this.input.Location = new System.Drawing.Point(49, 10);
			this.input.Margin = new System.Windows.Forms.Padding(2);
			this.input.Name = "input";
			this.input.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
			this.input.Size = new System.Drawing.Size(695, 451);
			this.input.TabIndex = 2;
			this.input.Text = "";
			this.input.TextChanged += new System.EventHandler(this.InputTextChanged);
			// 
			// linesCounter
			// 
			this.linesCounter.BackColor = System.Drawing.Color.LightCyan;
			this.linesCounter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.linesCounter.Cursor = System.Windows.Forms.Cursors.No;
			this.linesCounter.DetectUrls = false;
			this.linesCounter.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.linesCounter.ForeColor = System.Drawing.SystemColors.InfoText;
			this.linesCounter.Location = new System.Drawing.Point(9, 10);
			this.linesCounter.Margin = new System.Windows.Forms.Padding(2);
			this.linesCounter.Name = "linesCounter";
			this.linesCounter.ReadOnly = true;
			this.linesCounter.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
			this.linesCounter.Size = new System.Drawing.Size(36, 451);
			this.linesCounter.TabIndex = 3;
			this.linesCounter.Text = "";
			// 
			// bnfText
			// 
			this.bnfText.BackColor = System.Drawing.Color.LightCyan;
			this.bnfText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.bnfText.Cursor = System.Windows.Forms.Cursors.No;
			this.bnfText.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.bnfText.ForeColor = System.Drawing.SystemColors.InfoText;
			this.bnfText.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.bnfText.Location = new System.Drawing.Point(748, 10);
			this.bnfText.Margin = new System.Windows.Forms.Padding(2);
			this.bnfText.Multiline = true;
			this.bnfText.Name = "bnfText";
			this.bnfText.ReadOnly = true;
			this.bnfText.Size = new System.Drawing.Size(505, 451);
			this.bnfText.TabIndex = 4;
			// 
			// button1
			// 
			this.button1.BackColor = System.Drawing.SystemColors.HighlightText;
			this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.button1.FlatAppearance.BorderColor = System.Drawing.Color.Black;
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1.Font = new System.Drawing.Font("Consolas", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.button1.ForeColor = System.Drawing.SystemColors.InfoText;
			this.button1.Location = new System.Drawing.Point(536, 465);
			this.button1.Margin = new System.Windows.Forms.Padding(2);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(208, 48);
			this.button1.TabIndex = 5;
			this.button1.Text = "Выполнить";
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Click += new System.EventHandler(this.ButtonClicked);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Thistle;
			this.ClientSize = new System.Drawing.Size(1264, 681);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.bnfText);
			this.Controls.Add(this.linesCounter);
			this.Controls.Add(this.input);
			this.Controls.Add(this.output);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.KeyPreview = true;
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Сергеева Ульяна БСБО-01-20 Вариант 24";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.F5Pressed);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox output;
        private System.Windows.Forms.RichTextBox input;
        private System.Windows.Forms.RichTextBox linesCounter;
        private System.Windows.Forms.TextBox bnfText;
        private System.Windows.Forms.Button button1;
    }
}

