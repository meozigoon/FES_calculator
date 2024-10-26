using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form3 : Form
    {
        BigInteger ans = 0;
        public Form3()
        {
            InitializeComponent();
        }

        #region Calculation
        private static BigInteger Power(BigInteger x, BigInteger y) // 거듭 제곱 계산
        {
            BigInteger result = 1;
            while (y > 0)
            {
                if (y % 2 == 1)
                {
                    result *= x;
                }
                x *= x;
                y /= 2;
            }
            return result;
        }

        private BigInteger Factorial(BigInteger Num) //팩토리얼 계산
        {
            if (Num == 0)
            {
                return 0;
            }
            BigInteger i = 1;
            BigInteger Fac_Value = 1;
            for (i = 1; i <= Num; i++)
            {
                Fac_Value *= i;
            }
            return Fac_Value;
        }

        private BigInteger P(BigInteger n, BigInteger r) //P(순열) 계산
        {
            if (n == r)
            {
                return Factorial(n);
            }
            if (r == 0)
            {
                return 1;
            }
            else
            {
                return Factorial(n) / Factorial(n - r);
            }
        }

        private BigInteger C(BigInteger n, BigInteger r) //C(조합) 계산
        {
            if (r == 0)
            {
                return 1;
            }
            else
            {
                return P(n, r) / Factorial(r);
            }
        }

        private BigInteger H(BigInteger n, BigInteger r) //H(중복조합) 계산
        {
            return C(n + r - 1, r);
        }

        private BigInteger Pi(BigInteger n, BigInteger r) //Π(중복순열) 계산
        {
            return Power(n, r);
        }

        private BigInteger D(BigInteger n) //교란순열 계산
        {
            if (n == 0)
            {
                MessageBox.Show("값이 0 입니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
            else
            {
                if (n == 1)
                {
                    return 0;
                }
                else if (n == 2)
                {
                    return 1;
                }
                else
                {
                    return (n - 1) * (D(n - 1) + D(n - 2));
                }
            }
        }

        private BigInteger S(BigInteger n, BigInteger k) //제 2종 스털링수 S 계산
        {
            if (n == 0 || k == 0)
            {
                MessageBox.Show("값이 0 입니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
            else
            {
                if (n == k || k == 1)
                {
                    return 1;
                }
                else if (k == n - 1)
                {
                    return C(n, 2);
                }
                else if (k == 2)
                {
                    return Power(2, n - 1) - 1;
                }
                else
                {
                    return S(n - 1, k - 1) + k * S(n - 1, k);
                }
            }
        }

        private BigInteger Par(BigInteger n, BigInteger k) //분할수 P 계산
        {
            if (n == 0 || k == 0)
            {
                MessageBox.Show("값이 0 입니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
            else
            {
                if (n == k)
                {
                    return 1;
                }
                else if (k == 1)
                {
                    return 1;
                }
                else if (k == n - 1)
                {
                    return 1;
                }
                else if (k == 2)
                {
                    return n / 2;
                }
                else
                {
                    return Par(n - 1, k - 1) + Par(n - k, k);
                }
            }
        }
        #endregion

        private void button1_Click(object sender, EventArgs e) // factorial
        {
            if (textBox8.Text == "")
            {
                MessageBox.Show("값이 입력되지 않았습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show(String.Format("{0}! = {1}", textBox8.Text, Factorial(BigInteger.Parse(textBox8.Text)), "결과", MessageBoxButtons.OK, MessageBoxIcon.Information));
            }
            textBox8.Text = "";
        }

        private void button2_Click(object sender, EventArgs e) // P
        {
            if (textBox9.Text == "" || textBox1.Text == "")
            {
                MessageBox.Show("값이 입력되지 않았습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show(String.Format("{0}P{1} = {2}", textBox9.Text, textBox1.Text, P(BigInteger.Parse(textBox9.Text), BigInteger.Parse(textBox1.Text)), "결과", MessageBoxButtons.OK, MessageBoxIcon.Information));
            }
            textBox9.Text = "";
            textBox1.Text = "";
        }

        private void button6_Click(object sender, EventArgs e) // C
        {
            if (textBox3.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("값이 입력되지 않았습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show(String.Format("{0}C{1} = {2}", textBox3.Text, textBox2.Text, C(BigInteger.Parse(textBox3.Text), BigInteger.Parse(textBox2.Text)), "결과", MessageBoxButtons.OK, MessageBoxIcon.Information));
            }
            textBox3.Text = "";
            textBox2.Text = "";
        }

        private void button7_Click(object sender, EventArgs e) // H
        {
            if (textBox5.Text == "" || textBox4.Text == "")
            {
                MessageBox.Show("값이 입력되지 않았습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show(String.Format("{0}H{1} = {2}", textBox5.Text, textBox4.Text, H(BigInteger.Parse(textBox5.Text), BigInteger.Parse(textBox4.Text)), "결과", MessageBoxButtons.OK, MessageBoxIcon.Information));
            }
            textBox4.Text = "";
            textBox5.Text = "";
        }

        private void button8_Click(object sender, EventArgs e) // PI
        {
            if (textBox7.Text == "" || textBox6.Text == "")
            {
                MessageBox.Show("값이 입력되지 않았습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show(String.Format("{0}Π{1} = {2}", textBox7.Text, textBox6.Text, Pi(BigInteger.Parse(textBox7.Text), BigInteger.Parse(textBox6.Text)), "결과", MessageBoxButtons.OK, MessageBoxIcon.Information));
            }
            textBox6.Text = "";
            textBox7.Text = "";
        }

        private void button3_Click(object sender, EventArgs e) // Dn
        {
            if (textBox10.Text == "")
            {
                MessageBox.Show("값이 입력되지 않았습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (D(BigInteger.Parse(textBox10.Text)) != -1)
            {
                MessageBox.Show(String.Format("D{0} = {1}", textBox10.Text, D(BigInteger.Parse(textBox10.Text)), "결과", MessageBoxButtons.OK, MessageBoxIcon.Information));
            }
            textBox10.Text = "";
        }

        private void button4_Click(object sender, EventArgs e) // S(n, k)
        {
            if (textBox11.Text == "" || textBox12.Text == "")
            {
                MessageBox.Show("값이 입력되지 않았습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (S(BigInteger.Parse(textBox11.Text), BigInteger.Parse(textBox12.Text)) != -1)
            {
                MessageBox.Show(String.Format("S({0}, {1}) = {2}", textBox11.Text, textBox12.Text, S(BigInteger.Parse(textBox11.Text), BigInteger.Parse(textBox12.Text))), "결과", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            textBox11.Text = "";
            textBox12.Text = "";
        }

        private void button10_Click(object sender, EventArgs e) // B
        {
            if (textBox15.Text == "")
            {
                MessageBox.Show("값이 입력되지 않았습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                for (BigInteger i = 1; i <= BigInteger.Parse(textBox15.Text); i++)
                {
                    ans += S(BigInteger.Parse(textBox15.Text), i);
                }
                MessageBox.Show(String.Format("B({0}) = {1}", textBox15.Text, ans), "결과", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ans = 0;
            }
        }

        private void button9_Click(object sender, EventArgs e) // P
        {
            if (textBox16.Text == "")
            {
                MessageBox.Show("값이 입력되지 않았습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                for (BigInteger i = 1; i <= BigInteger.Parse(textBox16.Text); i++)
                {
                    ans += Par(BigInteger.Parse(textBox16.Text), i);
                }
                MessageBox.Show(String.Format("P({0}) = {1}", textBox16.Text, ans), "결과", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ans = 0;
            }
        }

        private void button5_Click(object sender, EventArgs e) // p(n, k)
        {
            if (textBox14.Text == "" || textBox13.Text == "")
            {
                MessageBox.Show("값이 입력되지 않았습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (Par(BigInteger.Parse(textBox14.Text), BigInteger.Parse(textBox13.Text)) != -1)
            {
                MessageBox.Show(String.Format("P({0}, {1}) = {2}", textBox14.Text, textBox13.Text, Par(BigInteger.Parse(textBox14.Text), BigInteger.Parse(textBox13.Text))), "결과", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            textBox14.Text = "";
            textBox13.Text = "";
        }

        #region MouseHover
        private void label5_MouseHover(object sender, EventArgs e) // P
        {
            ToolTip tooltip = new ToolTip();
            tooltip.ToolTipTitle = "nPr";
            tooltip.UseFading = true;
            tooltip.UseAnimation = true;
            tooltip.IsBalloon = false;
            tooltip.ShowAlways = true;
            tooltip.InitialDelay = 200;
            tooltip.ReshowDelay = 200;
            tooltip.SetToolTip(label5, "순열 계산\nCalculate Permutation");
        }

        private void label4_MouseHover(object sender, EventArgs e) // factorial
        {
            ToolTip tooltip = new ToolTip();
            tooltip.ToolTipTitle = "n!";
            tooltip.UseFading = true;
            tooltip.UseAnimation = true;
            tooltip.IsBalloon = false;
            tooltip.ShowAlways = true;
            tooltip.InitialDelay = 200;
            tooltip.ReshowDelay = 200;
            tooltip.SetToolTip(label4, "팩토리얼 계산\nCalculate Factorial");
        }

        private void label1_MouseHover(object sender, EventArgs e) // C
        {
            ToolTip tooltip = new ToolTip();
            tooltip.ToolTipTitle = "nCr";
            tooltip.UseFading = true;
            tooltip.UseAnimation = true;
            tooltip.IsBalloon = false;
            tooltip.ShowAlways = true;
            tooltip.InitialDelay = 200;
            tooltip.ReshowDelay = 200;
            tooltip.SetToolTip(label1, "조합 계산\nCalculate Combination");
        }

        private void label2_MouseHover(object sender, EventArgs e) // H
        {
            ToolTip tooltip = new ToolTip();
            tooltip.ToolTipTitle = "nHr";
            tooltip.UseFading = true;
            tooltip.UseAnimation = true;
            tooltip.IsBalloon = false;
            tooltip.ShowAlways = true;
            tooltip.InitialDelay = 200;
            tooltip.ReshowDelay = 200;
            tooltip.SetToolTip(label2, "중복 조합 계산\nCalculate Combination with Repetition");
        }

        private void label3_MouseHover(object sender, EventArgs e) // PI
        {
            ToolTip tooltip = new ToolTip();
            tooltip.ToolTipTitle = "nΠr";
            tooltip.UseFading = true;
            tooltip.UseAnimation = true;
            tooltip.IsBalloon = false;
            tooltip.ShowAlways = true;
            tooltip.InitialDelay = 200;
            tooltip.ReshowDelay = 200;
            tooltip.SetToolTip(label3, "중복 순열 계산\nCalculate Permutation with Repetition");
        }

        private void label10_MouseHover(object sender, EventArgs e) // 교란 순열
        {
            ToolTip tooltip = new ToolTip();
            tooltip.ToolTipTitle = "Dn";
            tooltip.UseFading = true;
            tooltip.UseAnimation = true;
            tooltip.IsBalloon = false;
            tooltip.ShowAlways = true;
            tooltip.InitialDelay = 200;
            tooltip.ReshowDelay = 200;
            tooltip.SetToolTip(label10, "교란 순열 계산\nCalculate Derangement Permutation");
        }

        private void label6_MouseHover(object sender, EventArgs e) // S(n, k)
        {
            ToolTip tooltip = new ToolTip();
            tooltip.ToolTipTitle = "S(n, k)";
            tooltip.UseFading = true;
            tooltip.UseAnimation = true;
            tooltip.IsBalloon = false;
            tooltip.ShowAlways = true;
            tooltip.InitialDelay = 200;
            tooltip.ReshowDelay = 200;
            tooltip.SetToolTip(label6, "제 2종 스털링 수 계산\nCalculate Stirling Numbers Of The Second Kind");
        }

        private void label7_MouseHover(object sender, EventArgs e) // P
        {
            ToolTip tooltip = new ToolTip();
            tooltip.ToolTipTitle = "Pn";
            tooltip.UseFading = true;
            tooltip.UseAnimation = true;
            tooltip.IsBalloon = false;
            tooltip.ShowAlways = true;
            tooltip.InitialDelay = 200;
            tooltip.ReshowDelay = 200;
            tooltip.SetToolTip(label7, "분할수 계산\nCalculate Partition Number");
        }

        private void label9_MouseHover(object sender, EventArgs e) // P(n, k)
        {
            ToolTip tooltip = new ToolTip();
            tooltip.ToolTipTitle = "p(n, k)";
            tooltip.UseFading = true;
            tooltip.UseAnimation = true;
            tooltip.IsBalloon = false;
            tooltip.ShowAlways = true;
            tooltip.InitialDelay = 200;
            tooltip.ReshowDelay = 200;
            tooltip.SetToolTip(label9, "분할수 계산\nCalculate Partition Number");
        }

        private void label8_MouseHover(object sender, EventArgs e) // B
        {
            ToolTip tooltip = new ToolTip();
            tooltip.ToolTipTitle = "Bn";
            tooltip.UseFading = true;
            tooltip.UseAnimation = true;
            tooltip.IsBalloon = false;
            tooltip.ShowAlways = true;
            tooltip.InitialDelay = 200;
            tooltip.ReshowDelay = 200;
            tooltip.SetToolTip(label8, "벨 계산\nCalculate Bell Number");
        }
        #endregion

        #region KeyDown
        private void textBox8_KeyDown(object sender, KeyEventArgs e) // factorial
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(sender, e);
            }
        }

        private void textBox9_KeyDown(object sender, KeyEventArgs e) // P
        {
            if (e.KeyCode == Keys.Enter)
            {
                button2_Click(sender, e);
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e) // P
        {
            if (e.KeyCode == Keys.Enter)
            {
                button2_Click(sender, e);
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e) // C
        {
            if (e.KeyCode == Keys.Enter)
            {
                button6_Click(sender, e);
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e) // C
        {
            if (e.KeyCode == Keys.Enter)
            {
                button6_Click(sender, e);
            }
        }

        private void textBox5_KeyDown(object sender, KeyEventArgs e) // H
        {
            if (e.KeyCode == Keys.Enter)
            {
                button7_Click(sender, e);
            }
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e) // H
        {
            if (e.KeyCode == Keys.Enter)
            {
                button7_Click(sender, e);
            }
        }

        private void textBox7_KeyDown(object sender, KeyEventArgs e) // PI
        {
            if (e.KeyCode == Keys.Enter)
            {
                button8_Click(sender, e);
            }
        }

        private void textBox6_KeyDown(object sender, KeyEventArgs e) // PI
        {
            if (e.KeyCode == Keys.Enter)
            {
                button8_Click(sender, e);
            }
        }

        private void textBox10_KeyDown(object sender, KeyEventArgs e) // D
        {
            if (e.KeyCode == Keys.Enter)
            {
                button3_Click(sender, e);
            }
        }

        private void textBox11_KeyDown(object sender, KeyEventArgs e) // S(n, k)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button4_Click(sender, e);
            }
        }

        private void textBox12_KeyDown(object sender, KeyEventArgs e) // S(n, k)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button4_Click(sender, e);
            }
        }

        private void textBox15_KeyDown(object sender, KeyEventArgs e) // B
        {
            if (e.KeyCode == Keys.Enter)
            {
                button10_Click(sender, e);
            }
        }

        private void textBox16_KeyDown(object sender, KeyEventArgs e) // P
        {
            if (e.KeyCode == Keys.Enter)
            {
                button9_Click(sender, e);
            }
        }

        private void textBox14_KeyDown(object sender, KeyEventArgs e) // p(n, k)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button5_Click(sender, e);
            }
        }

        private void textBox13_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button5_Click(sender, e);
            }
        }
        #endregion
    }
}