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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private static decimal Power(decimal x, BigInteger y) // 거듭 제곱 계산
        {
            decimal result = 1;
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

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(String.Format("{0}^{1} = {2}", textBox1.Text, textBox2.Text, Power(decimal.Parse(textBox1.Text), BigInteger.Parse(textBox2.Text)), "결과", MessageBoxButtons.OK, MessageBoxIcon.Information));
        }
      
        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(String.Format("{0}√{1} = {2}", textBox4.Text, textBox3.Text, Math.Pow(Convert.ToDouble(textBox3.Text), 1 / Convert.ToDouble(textBox4.Text))), "결과", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}