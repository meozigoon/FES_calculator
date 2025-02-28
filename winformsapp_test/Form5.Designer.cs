namespace Calculator
{
    partial class Form5
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
            drawButton = new Button();
            maximumYTextBox = new TextBox();
            minimumYTextBox = new TextBox();
            maximumXTextBox = new TextBox();
            xLabel = new Label();
            minimumXTextBox = new TextBox();
            equationTextBox = new TextBox();
            equationLabel = new Label();
            canvasPictureBox = new PictureBox();
            label1 = new Label();
            label2 = new Label();
            comboBox1 = new ComboBox();
            label5 = new Label();
            ((System.ComponentModel.ISupportInitialize)canvasPictureBox).BeginInit();
            SuspendLayout();
            // 
            // drawButton
            // 
            drawButton.BackColor = Color.FromArgb(64, 64, 64);
            drawButton.BackgroundImageLayout = ImageLayout.None;
            drawButton.Cursor = Cursors.Hand;
            drawButton.FlatAppearance.BorderColor = Color.Gray;
            drawButton.FlatAppearance.BorderSize = 0;
            drawButton.FlatStyle = FlatStyle.Flat;
            drawButton.Font = new Font("배달의민족 한나체 Pro", 9.999999F, FontStyle.Regular, GraphicsUnit.Point);
            drawButton.ForeColor = SystemColors.ButtonHighlight;
            drawButton.Location = new Point(716, 58);
            drawButton.Margin = new Padding(0);
            drawButton.Name = "drawButton";
            drawButton.Size = new Size(135, 72);
            drawButton.TabIndex = 8;
            drawButton.Text = "그리기";
            drawButton.UseVisualStyleBackColor = false;
            drawButton.Click += drawButton_Click;
            // 
            // maximumYTextBox
            // 
            maximumYTextBox.BackColor = SystemColors.InfoText;
            maximumYTextBox.BorderStyle = BorderStyle.FixedSingle;
            maximumYTextBox.Font = new Font("Britannic Bold", 12F, FontStyle.Regular, GraphicsUnit.Point);
            maximumYTextBox.ForeColor = SystemColors.Window;
            maximumYTextBox.Location = new Point(341, 96);
            maximumYTextBox.Margin = new Padding(0);
            maximumYTextBox.Name = "maximumYTextBox";
            maximumYTextBox.Size = new Size(97, 34);
            maximumYTextBox.TabIndex = 7;
            maximumYTextBox.Text = "50.0";
            maximumYTextBox.TextAlign = HorizontalAlignment.Right;
            // 
            // minimumYTextBox
            // 
            minimumYTextBox.BackColor = SystemColors.InfoText;
            minimumYTextBox.BorderStyle = BorderStyle.FixedSingle;
            minimumYTextBox.Font = new Font("Britannic Bold", 12F, FontStyle.Regular, GraphicsUnit.Point);
            minimumYTextBox.ForeColor = SystemColors.Window;
            minimumYTextBox.Location = new Point(117, 96);
            minimumYTextBox.Margin = new Padding(0);
            minimumYTextBox.Name = "minimumYTextBox";
            minimumYTextBox.Size = new Size(97, 34);
            minimumYTextBox.TabIndex = 5;
            minimumYTextBox.Text = "-50.0";
            minimumYTextBox.TextAlign = HorizontalAlignment.Right;
            // 
            // maximumXTextBox
            // 
            maximumXTextBox.BackColor = SystemColors.InfoText;
            maximumXTextBox.BorderStyle = BorderStyle.FixedSingle;
            maximumXTextBox.Font = new Font("Britannic Bold", 12F, FontStyle.Regular, GraphicsUnit.Point);
            maximumXTextBox.ForeColor = SystemColors.Window;
            maximumXTextBox.Location = new Point(341, 61);
            maximumXTextBox.Margin = new Padding(0);
            maximumXTextBox.Name = "maximumXTextBox";
            maximumXTextBox.Size = new Size(97, 34);
            maximumXTextBox.TabIndex = 4;
            maximumXTextBox.Text = "50.0";
            maximumXTextBox.TextAlign = HorizontalAlignment.Right;
            // 
            // xLabel
            // 
            xLabel.Font = new Font("Britannic Bold", 12F, FontStyle.Regular, GraphicsUnit.Point);
            xLabel.ForeColor = SystemColors.ButtonHighlight;
            xLabel.Location = new Point(214, 58);
            xLabel.Name = "xLabel";
            xLabel.Size = new Size(127, 35);
            xLabel.TabIndex = 3;
            xLabel.Text = "≤    ｘ    ≤";
            xLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // minimumXTextBox
            // 
            minimumXTextBox.BackColor = SystemColors.InfoText;
            minimumXTextBox.BorderStyle = BorderStyle.FixedSingle;
            minimumXTextBox.Font = new Font("Britannic Bold", 12F, FontStyle.Regular, GraphicsUnit.Point);
            minimumXTextBox.ForeColor = SystemColors.Window;
            minimumXTextBox.Location = new Point(117, 61);
            minimumXTextBox.Margin = new Padding(0);
            minimumXTextBox.Name = "minimumXTextBox";
            minimumXTextBox.Size = new Size(97, 34);
            minimumXTextBox.TabIndex = 2;
            minimumXTextBox.Text = "-50.0";
            minimumXTextBox.TextAlign = HorizontalAlignment.Right;
            // 
            // equationTextBox
            // 
            equationTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            equationTextBox.BackColor = SystemColors.InfoText;
            equationTextBox.BorderStyle = BorderStyle.FixedSingle;
            equationTextBox.Font = new Font("Britannic Bold", 12F, FontStyle.Regular, GraphicsUnit.Point);
            equationTextBox.ForeColor = SystemColors.Window;
            equationTextBox.Location = new Point(70, 15);
            equationTextBox.Margin = new Padding(0);
            equationTextBox.Name = "equationTextBox";
            equationTextBox.Size = new Size(781, 34);
            equationTextBox.TabIndex = 1;
            equationTextBox.Text = "Math.Sin(0.5 * x) * x";
            // 
            // equationLabel
            // 
            equationLabel.Font = new Font("배달의민족 한나체 Pro", 12F, FontStyle.Regular, GraphicsUnit.Point);
            equationLabel.ForeColor = SystemColors.ButtonHighlight;
            equationLabel.Location = new Point(8, 15);
            equationLabel.Name = "equationLabel";
            equationLabel.Size = new Size(61, 35);
            equationLabel.TabIndex = 0;
            equationLabel.Text = "수식";
            equationLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // canvasPictureBox
            // 
            canvasPictureBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            canvasPictureBox.BorderStyle = BorderStyle.FixedSingle;
            canvasPictureBox.Location = new Point(20, 138);
            canvasPictureBox.Margin = new Padding(0);
            canvasPictureBox.Name = "canvasPictureBox";
            canvasPictureBox.Size = new Size(831, 549);
            canvasPictureBox.TabIndex = 11;
            canvasPictureBox.TabStop = false;
            // 
            // label1
            // 
            label1.Font = new Font("배달의민족 한나체 Pro", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.ButtonHighlight;
            label1.Location = new Point(8, 71);
            label1.Name = "label1";
            label1.Size = new Size(103, 53);
            label1.TabIndex = 12;
            label1.Text = "표시 범위";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            label2.BackColor = SystemColors.ActiveCaptionText;
            label2.Font = new Font("배달의민족 한나체 Pro", 11F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = SystemColors.ButtonHighlight;
            label2.Location = new Point(491, 50);
            label2.Name = "label2";
            label2.Size = new Size(111, 45);
            label2.TabIndex = 14;
            label2.Text = "그래프 색상";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // comboBox1
            // 
            comboBox1.BackColor = SystemColors.InfoText;
            comboBox1.Cursor = Cursors.Hand;
            comboBox1.FlatStyle = FlatStyle.Flat;
            comboBox1.Font = new Font("Britannic Bold", 12F, FontStyle.Regular, GraphicsUnit.Point);
            comboBox1.ForeColor = SystemColors.Window;
            comboBox1.Items.AddRange(new object[] { "AliceBlue", "AntiqueWhite", "Aqua", "Aquamarine", "Azure", "Beige", "Bisque", "Black", "BlanchedAlmond", "Blue", "BlueViolet", "Brown\tName", "BurlyWood", "CadetBlue", "Chartreuse", "Chocolate", "Coral", "CornflowerBlue", "Cornsilk", "Crimson", "Cyan", "DarkBlue", "DarkCyan", "DarkGoldenrod", "DarkGray", "DarkGreen", "DarkKhaki", "DarkMagenta", "DarkOliveGreen", "DarkOrange", "DarkOrchid", "DarkRed", "DarkSalmon", "DarkSeaGreen", "DarkSlateBlue", "DarkSlateGray", "DarkTurquoise", "DarkViolet", "DeepPink", "DeepSkyBlue", "DimGray", "DodgerBlue", "Feldspar", "Firebrick", "FloralWhite", "ForestGreen", "Fuchsia", "Gainsboro", "GhostWhite", "Gold", "Goldenrod", "Gray", "Green", "GreenYellow", "Honeydew", "HotPink", "IndianRed", "Indigo\t", "Ivory", "Khaki", "Lavender", "LavenderBlush", "LawnGreen", "LemonChiffon", "LightBlue", "LightCoral", "LightCyan", "LightGoldenrodYellow", "LightGray", "LightGreen", "LightPink", "LightSalmon", "LightSeaGreen", "LightSkyBlue", "LightSlateBlue", "LightSlateGray", "LightSteelBlue", "LightYellow", "Lime", "LimeGreen", "Linen", "Magenta", "Maroon", "MediumAquamarine", "MediumBlue", "MediumOrchid", "MediumPurple", "MediumSeaGreen", "MediumSlateBlue", "MediumSpringGreen", "MediumTurquoise", "MediumVioletRed", "MidnightBlue", "MintCream", "MistyRose", "Moccasin", "NavajoWhite\t", "Navy", "OldLace", "Olive", "OliveDrab", "Orange", "OrangeRed", "Orchid", "PaleGoldenrod", "PaleGreen", "PaleTurquoise", "PaleVioletRed", "PapayaWhip", "PeachPuff", "Peru", "Pink", "Plum", "PowderBlue", "Purple", "Red", "RosyBrown", "RoyalBlue", "SaddleBrown", "Salmon", "SandyBrown", "SeaGreen", "SeaShell", "Sienna", "Silver", "SkyBlue", "SlateBlue", "SlateGray", "Snow", "SpringGreen", "SteelBlue", "Tan", "Teal", "Thistle", "Tomato", "Transparent", "Turquoise", "TVBlack", "TVWhite", "Violet", "VioletRed", "Wheat", "White", "WhiteSmoke", "Yellow", "YellowGreen" });
            comboBox1.Location = new Point(491, 89);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(161, 35);
            comboBox1.TabIndex = 0;
            comboBox1.Text = "Blue";
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // label5
            // 
            label5.Font = new Font("Britannic Bold", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label5.ForeColor = SystemColors.ButtonHighlight;
            label5.Location = new Point(214, 93);
            label5.Name = "label5";
            label5.Size = new Size(127, 35);
            label5.TabIndex = 16;
            label5.Text = "≤    ｙ    ≤";
            label5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Form5
            // 
            AcceptButton = drawButton;
            AutoScaleMode = AutoScaleMode.None;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(875, 708);
            Controls.Add(label5);
            Controls.Add(comboBox1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(drawButton);
            Controls.Add(maximumYTextBox);
            Controls.Add(minimumYTextBox);
            Controls.Add(maximumXTextBox);
            Controls.Add(xLabel);
            Controls.Add(minimumXTextBox);
            Controls.Add(equationTextBox);
            Controls.Add(equationLabel);
            Controls.Add(canvasPictureBox);
            Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form5";
            Text = "Graph Calculator | 그래프 계산기";
            ((System.ComponentModel.ISupportInitialize)canvasPictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button drawButton;
        private System.Windows.Forms.TextBox maximumYTextBox;
        private System.Windows.Forms.TextBox minimumYTextBox;
        private System.Windows.Forms.TextBox maximumXTextBox;
        private System.Windows.Forms.Label xLabel;
        private System.Windows.Forms.TextBox minimumXTextBox;
        private System.Windows.Forms.TextBox equationTextBox;
        private System.Windows.Forms.Label equationLabel;
        private System.Windows.Forms.PictureBox canvasPictureBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
        private Label label5;
    }
}