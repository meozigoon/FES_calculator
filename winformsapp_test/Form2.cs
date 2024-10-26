namespace Calculator
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) // 일차방정식
        {
            if (textBox5.Text == "" || textBox4.Text == "")
            {
                MessageBox.Show("값이 입력되지 않았습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (textBox5.Text == "0")
                {
                    if (textBox4.Text == "0")
                    {
                        MessageBox.Show(String.Format("모든 실수"), "결과", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(String.Format("해가 없음"), "결과", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show(String.Format("x = {0}", -1 * int.Parse(textBox4.Text) / int.Parse(textBox5.Text)), "결과", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        
        private void button2_Click(object sender, EventArgs e) // 이차방정식
        {
            int a = int.Parse(textBox1.Text);
            int b = int.Parse(textBox2.Text);
            int c = int.Parse(textBox3.Text);
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("값이 입력되지 않았습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (b * b - 4 * a * c > 0)
                {
                    MessageBox.Show(String.Format("x = {0} 또는 x = {1}", (-b + Math.Sqrt(b * b - 4 * a * c)) / (2 * a), (-b - Math.Sqrt(b * b - 4 * a * c)) / (2 * a)), "결과", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (b * b - 4 * a * c == 0)
                {
                    MessageBox.Show(String.Format("x = {0}", (-b + Math.Sqrt(b * b - 4 * a * c)) / (2 * a)), "결과", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (b * b - 4 * a * c < 0)
                {
                    if (a == 1)
                    {
                        if (b % 2 == 0)
                        {
                            MessageBox.Show(String.Format("x = {0} ± √({1})i", -b / 2, -(b * b - 4 * a * c) / 4), "결과", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show(String.Format("x = ({0} ± √({1})i) / 2", -b, -(b * b - 4 * a * c)), "결과", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        if (b % 2 == 0)
                        {
                            MessageBox.Show(String.Format("x = ({0} ± √({1})i) / {2}", -b / 2, -(b * b - 4 * a * c) / 4, a), "결과", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show(String.Format("x = ({0} ± √({1})i) / {2}", -b / 2, -(b * b - 4 * a * c) / 4, 2 * a), "결과", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
        }
    }
}