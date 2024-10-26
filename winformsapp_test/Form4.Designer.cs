namespace Calculator
{
    partial class Form4
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form4));
            button1 = new Button();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            label1 = new Label();
            textBox4 = new TextBox();
            textBox3 = new TextBox();
            button2 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(64, 64, 64);
            button1.Cursor = Cursors.Hand;
            button1.FlatAppearance.BorderColor = SystemColors.WindowFrame;
            button1.FlatAppearance.MouseDownBackColor = Color.Gray;
            button1.FlatAppearance.MouseOverBackColor = Color.Gray;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("배달의민족 한나체 Pro", 12F, FontStyle.Regular, GraphicsUnit.Point);
            button1.ForeColor = Color.White;
            button1.Location = new Point(202, 68);
            button1.Margin = new Padding(6, 5, 6, 5);
            button1.Name = "button1";
            button1.Size = new Size(74, 46);
            button1.TabIndex = 35;
            button1.Text = "계산";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.FromArgb(64, 64, 64);
            textBox1.Cursor = Cursors.IBeam;
            textBox1.Font = new Font("Britannic Bold", 18F, FontStyle.Regular, GraphicsUnit.Point);
            textBox1.ForeColor = Color.White;
            textBox1.ImeMode = ImeMode.Disable;
            textBox1.Location = new Point(26, 68);
            textBox1.Margin = new Padding(4, 5, 4, 5);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(74, 47);
            textBox1.TabIndex = 36;
            textBox1.TextAlign = HorizontalAlignment.Center;
            // 
            // textBox2
            // 
            textBox2.BackColor = Color.FromArgb(64, 64, 64);
            textBox2.Cursor = Cursors.IBeam;
            textBox2.Font = new Font("Britannic Bold", 18F, FontStyle.Regular, GraphicsUnit.Point);
            textBox2.ForeColor = Color.White;
            textBox2.ImeMode = ImeMode.Disable;
            textBox2.Location = new Point(102, 30);
            textBox2.Margin = new Padding(4, 5, 4, 5);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(57, 47);
            textBox2.TabIndex = 37;
            textBox2.TextAlign = HorizontalAlignment.Center;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.ActiveCaptionText;
            label1.Font = new Font("Britannic Bold", 35F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(58, 188);
            label1.Name = "label1";
            label1.Size = new Size(150, 78);
            label1.TabIndex = 45;
            label1.Text = "√￣";
            // 
            // textBox4
            // 
            textBox4.BackColor = Color.FromArgb(64, 64, 64);
            textBox4.Cursor = Cursors.IBeam;
            textBox4.Font = new Font("Britannic Bold", 18F, FontStyle.Regular, GraphicsUnit.Point);
            textBox4.ForeColor = Color.White;
            textBox4.ImeMode = ImeMode.Disable;
            textBox4.Location = new Point(26, 169);
            textBox4.Margin = new Padding(4, 5, 4, 5);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(57, 47);
            textBox4.TabIndex = 46;
            textBox4.TextAlign = HorizontalAlignment.Center;
            // 
            // textBox3
            // 
            textBox3.BackColor = Color.FromArgb(64, 64, 64);
            textBox3.Cursor = Cursors.IBeam;
            textBox3.Font = new Font("Britannic Bold", 18F, FontStyle.Regular, GraphicsUnit.Point);
            textBox3.ForeColor = Color.White;
            textBox3.ImeMode = ImeMode.Disable;
            textBox3.Location = new Point(118, 212);
            textBox3.Margin = new Padding(4, 5, 4, 5);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(74, 47);
            textBox3.TabIndex = 47;
            textBox3.TextAlign = HorizontalAlignment.Center;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(64, 64, 64);
            button2.Cursor = Cursors.Hand;
            button2.FlatAppearance.BorderColor = SystemColors.WindowFrame;
            button2.FlatAppearance.MouseDownBackColor = Color.Gray;
            button2.FlatAppearance.MouseOverBackColor = Color.Gray;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("배달의민족 한나체 Pro", 12F, FontStyle.Regular, GraphicsUnit.Point);
            button2.ForeColor = Color.White;
            button2.Location = new Point(202, 198);
            button2.Margin = new Padding(6, 5, 6, 5);
            button2.Name = "button2";
            button2.Size = new Size(74, 46);
            button2.TabIndex = 48;
            button2.Text = "계산";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // Form4
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(302, 300);
            Controls.Add(button2);
            Controls.Add(textBox3);
            Controls.Add(textBox4);
            Controls.Add(label1);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(button1);
            ForeColor = SystemColors.Control;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form4";
            Text = "Power Calculator | 거듭제곱 계산기";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private TextBox textBox1;
        private TextBox textBox2;
        private Label label1;
        private TextBox textBox4;
        private TextBox textBox3;
        private Button button2;
    }
}