using Calculator;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace winformsapp_test
{
    public partial class Calculator : Form
    {
        decimal number = 0; // 입력 받는 숫자
        Stack prev_numbers = new Stack(); // backspace 위한 이전에 입력 받은 수 저장
        Stack prev_label2 = new Stack(); // 이전에 label2(계산 내용)에 표시되었던 문자열 저장
        decimal prev; // 전에 입력한 숫자
        decimal prev_prev; // 전에 입력한 숫자 임시 저장
        bool calculation_check = true; // 앞에 계산이 일어났는지 확인 false = 계산 일어남
        bool minus_check = true; // 부호 확인 false = minus
        bool point_check = true; // 소숫점 확인 false = 소수점
        bool power = true; // 여러 제곱 확인
        bool deg_check = true; // rad, deg 확인
        BigInteger point_cnt = 0; // 소수점 개수
        Dictionary<BigInteger, BigInteger> factors = new Dictionary<BigInteger, BigInteger>(); // 딕셔너리형, 소인수분해에서 인수 저장용
        next_calculation c = new next_calculation(); // 계산 내용 확인 위한 enum
        enum next_calculation
        {
            nul, // 계산 없음
            plus,
            minus,
            multiple,
            mod,
            division,
            power,
            sqrt,
            sin,
            cos,
            tan,
            log,
            ln
        };

        public Calculator()
        {
            InitializeComponent();
            label1.Text = "0";
            label2.Text = "";
        }

        private void calculate() // 계산 결과 반환, 숫자, 텍스트 초기화
        {
            prev_numbers.Push(number);
            prev_label2.Push(label2.Text);
            if (c == next_calculation.plus) // +
            {
                number += prev;
            }
            else if (c == next_calculation.minus) // -
            {
                number = prev - number;
            }
            else if (c == next_calculation.multiple) // *
            {
                number = prev * number;
            }
            else if (c == next_calculation.division) // /
            {
                number = prev / number;
            }
            else if (c == next_calculation.mod) // %
            {
                number = prev % number;
            }
            else if (c == next_calculation.power) // ^2
            {
                number *= number;
            }
            else if (c == next_calculation.sin) // sin
            {
                if (deg_check)
                {
                    number = Convert.ToDecimal(Math.Sin(Convert.ToDouble(number) * (Math.PI / 180)));
                }
                else
                {
                    number = Convert.ToDecimal(Math.Sin(Convert.ToDouble(number)));
                }
            }
            else if (c == next_calculation.cos) // cos
            {
                if (deg_check)
                {
                    number = Convert.ToDecimal(Math.Cos(Convert.ToDouble(number) * (Math.PI / 180)));
                }
                else
                {
                    number = Convert.ToDecimal(Math.Cos(Convert.ToDouble(number)));
                }
            }
            else if (c == next_calculation.tan) // tan
            {
                if (deg_check)
                {
                    number = Convert.ToDecimal(Math.Tan(Convert.ToDouble(number) * (Math.PI / 180)));
                }
                else
                {
                    number = Convert.ToDecimal(Math.Tan(Convert.ToDouble(number)));
                }
            }
            else if (c == next_calculation.sqrt) // sqrt
            {
                if (number < 0)
                {
                    number *= -1;
                }
                number = Convert.ToDecimal(Math.Sqrt(Convert.ToDouble(number)));
            }
            else if (c == next_calculation.log) // log
            {
                number = Convert.ToDecimal(Math.Log10(Convert.ToDouble(number)));
            }
            else if (c == next_calculation.ln) // ln
            {
                number = Convert.ToDecimal(Math.Log(Convert.ToDouble(number)));
            }
            point_check = true;
            point_cnt = 0;
            minus_check = true;
            calculation_check = false;
            prev = 0;
            c = next_calculation.nul;
            label2.Text = "";
            label1.Text = number.ToString();
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

        #region IsPrime
        static readonly decimal[] P = { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37 }; // 소수 판별 위한 소수 배열
        static Random random = new Random();

        private static BigInteger modPower(BigInteger x, BigInteger y, BigInteger m)
        {
            x %= m;
            BigInteger result = 1;
            while (y > 0)
            {
                if (y % 2 == 1)
                {
                    result = (result * x) % m;
                }
                x = (x * x) % m;
                y /= 2;
            }
            return result;
        }

        private static bool miller_rabin(BigInteger n, BigInteger a) // 밀러-라빈 소수 판별
        {
            if (a == n)
                return true;
            BigInteger k = n - 1;
            BigInteger prev;
            while (true)
            {
                prev = modPower(a, k, n);
                if (prev == n - 1)
                    return true;
                if (k % 2 == 1)
                {
                    return prev == 1 || prev == n - 1;
                }
                k /= 2;
            }
        }

        private static bool IsPrime(BigInteger n) // 소수 판별
        {
            if (n == 2 || n == 3)
                return true;
            if (n % 2 == 0)
                return false;
            foreach (BigInteger i in P)
            {
                if (!miller_rabin(n, i))
                    return false;
            }
            return true;
        }

        private static BigInteger F(BigInteger x, BigInteger c, BigInteger n) // 폴라드 로 다음 단계 함수
        {
            return (x * x % n + c) % n;
        }

        private static BigInteger pollards_rho(BigInteger n) // 폴라드 로
        {
            if (n % 2 == 0)
                return 2;
            if (IsPrime(n))
                return n;
            BigInteger x = random.Next(2, (int)(n - 1));
            BigInteger y = x;
            BigInteger c = random.Next(1, 10);
            BigInteger g = 1;
            while (g == 1)
            {
                x = F(x, c, n);
                y = F(F(y, c, n), c, n);
                g = BigInteger.GreatestCommonDivisor(BigInteger.Abs(x - y), n);
                if (g == n)
                    return pollards_rho(n);
            }
            return IsPrime(g) ? g : pollards_rho(g);
        }
        #endregion // 소수 판별 & 소인수분해

        private void button30_Click(object sender, EventArgs e) // 제작자
        {
            MessageBox.Show("아이디어 제공: DH.L, MG.K, SH.L\n" +
                            "프로그램 개발: DH.L\n" +
                            "아이콘 이미지: 위키피디아\n" +
                            "글꼴: Britannic Bold, 배달의민족 한나체 Pro", "정보", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button16_Click(object sender, EventArgs e) // AC
        {
            point_check = true;
            point_cnt = 0;
            minus_check = true;
            number = 0;
            c = next_calculation.nul;
            label2.Text = "";
            label1.Text = number.ToString();
            prev_numbers = new Stack();
            prev_label2 = new Stack();
        }

        private void button17_Click(object sender, EventArgs e) // CE
        {
            point_check = true;
            point_cnt = 0;
            minus_check = true;
            number = 0;
            label1.Text = number.ToString();
            prev_numbers = new Stack();
            prev_label2 = new Stack();
        }

        #region NumberInput
        private void button0_Click(object sender, EventArgs e) // 0
        {
            prev_numbers.Push(number);
            prev_label2.Push(label2.Text);
            if (!calculation_check)
            {
                calculation_check = true;
                number = 0;
                label1.Text = number.ToString();
            }
            if (!point_check)
            {
                point_cnt++;
                label1.Text = String.Format("{0}0", label1.Text);
            }
            else
            {
                number *= 10;
                label1.Text = number.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e) // 1
        {
            prev_numbers.Push(number);
            prev_label2.Push(label2.Text);
            if (minus_check)
            {
                if (!calculation_check)
                {
                    calculation_check = true;
                    number = 0;
                }
                if (!point_check)
                {
                    point_cnt++;
                    number += (Power(Convert.ToDecimal(0.1), point_cnt) * 1);
                }
                else
                {
                    number = number * 10 + 1;
                }
            }
            else
            {
                if (!calculation_check)
                {
                    calculation_check = true;
                    number = 0;
                }
                if (!point_check)
                {
                    point_cnt++;
                    number -= (Power(Convert.ToDecimal(0.1), point_cnt) * 1);
                }
                else
                {
                    number = number * 10 - 1;
                }
            }
            label1.Text = number.ToString();
        }

        private void button2_Click(object sender, EventArgs e) // 2
        {
            prev_numbers.Push(number);
            prev_label2.Push(label2.Text);
            if (minus_check)
            {
                if (!calculation_check)
                {
                    calculation_check = true;
                    number = 0;
                }
                if (!point_check)
                {
                    point_cnt++;
                    number += (Power(Convert.ToDecimal(0.1), point_cnt) * 2);
                }
                else
                {
                    number = number * 10 + 2;
                }
            }
            else
            {
                if (!calculation_check)
                {
                    calculation_check = true;
                    number = 0;
                }
                if (!point_check)
                {
                    point_cnt++;
                    number -= (Power(Convert.ToDecimal(0.1), point_cnt) * 2);
                }
                else
                {
                    number = number * 10 - 2;
                }
            }
            label1.Text = number.ToString();
        }

        private void button3_Click(object sender, EventArgs e) // 3
        {
            prev_numbers.Push(number);
            prev_label2.Push(label2.Text);
            if (minus_check)
            {
                if (!calculation_check)
                {
                    calculation_check = true;
                    number = 0;
                }
                if (!point_check)
                {
                    point_cnt++;
                    number += (Power(Convert.ToDecimal(0.1), point_cnt) * 3);
                }
                else
                {
                    number = number * 10 + 3;
                }
            }
            else
            {
                if (!calculation_check)
                {
                    calculation_check = true;
                    number = 0;
                }
                if (!point_check)
                {
                    point_cnt++;
                    number -= (Power(Convert.ToDecimal(0.1), point_cnt) * 3);
                }
                else
                {
                    number = number * 10 - 3;
                }
            }
            label1.Text = number.ToString();
        }

        private void button4_Click(object sender, EventArgs e) // 4
        {
            prev_numbers.Push(number);
            prev_label2.Push(label2.Text);
            if (minus_check)
            {
                if (!calculation_check) //앞에 계산이 이루어졌다면
                {
                    calculation_check = true;
                    number = 0;
                }
                if (!point_check) //소숫점 입력이라면
                {
                    point_cnt++;
                    number += (Power(Convert.ToDecimal(0.1), point_cnt) * 4);
                }
                else
                {
                    number = number * 10 + 4;
                }
            }
            else //number가 음수라면
            {
                if (!calculation_check)
                {
                    calculation_check = true;
                    number = 0;
                }
                if (!point_check)
                {
                    point_cnt++;
                    number -= (Power(Convert.ToDecimal(0.1), point_cnt) * 4);
                }
                else
                {
                    number = number * 10 - 4;
                }
            }
            label1.Text = number.ToString(); //label1의 Text를 number로 바꾸기
        }

        private void button5_Click(object sender, EventArgs e) // 5
        {
            prev_numbers.Push(number);
            prev_label2.Push(label2.Text);
            if (minus_check)
            {
                if (!calculation_check)
                {
                    calculation_check = true;
                    number = 0;
                }
                if (!point_check)
                {
                    point_cnt++;
                    number += (Power(Convert.ToDecimal(0.1), point_cnt) * 5);
                }
                else
                {
                    number = number * 10 + 5;
                }
            }
            else
            {
                if (!calculation_check)
                {
                    calculation_check = true;
                    number = 0;
                }
                if (!point_check)
                {
                    point_cnt++;
                    number -= (Power(Convert.ToDecimal(0.1), point_cnt) * 5);
                }
                else
                {
                    number = number * 10 - 5;
                }
            }
            label1.Text = number.ToString();
        }

        private void button6_Click(object sender, EventArgs e) // 6
        {
            prev_numbers.Push(number);
            prev_label2.Push(label2.Text);
            if (minus_check)
            {
                if (!calculation_check)
                {
                    calculation_check = true;
                    number = 0;
                }
                if (!point_check)
                {
                    point_cnt++;
                    number += Power(Convert.ToDecimal(0.1), point_cnt) * 6;
                }
                else
                {
                    number = number * 10 + 6;
                }
            }
            else
            {
                if (!calculation_check)
                {
                    calculation_check = true;
                    number = 0;
                }
                if (!point_check)
                {
                    point_cnt++;
                    number -= (Power(Convert.ToDecimal(0.1), point_cnt) * 6);
                }
                else
                {
                    number = number * 10 - 6;
                }
            }
            label1.Text = number.ToString();
        }

        private void button7_Click(object sender, EventArgs e) // 7
        {
            prev_numbers.Push(number);
            prev_label2.Push(label2.Text);
            if (minus_check)
            {
                if (!calculation_check)
                {
                    calculation_check = true;
                    number = 0;
                }
                if (!point_check)
                {
                    point_cnt++;
                    number += (Power(Convert.ToDecimal(0.1), point_cnt) * 7);
                }
                else
                {
                    number = number * 10 + 7;
                }
            }
            else
            {
                if (!calculation_check)
                {
                    calculation_check = true;
                    number = 0;
                }
                if (!point_check)
                {
                    point_cnt++;
                    number -= (Power(Convert.ToDecimal(0.1), point_cnt) * 7);
                }
                else
                {
                    number = number * 10 - 7;
                }
            }
            label1.Text = number.ToString();
        }

        private void button8_Click(object sender, EventArgs e) // 8
        {
            prev_numbers.Push(number);
            prev_label2.Push(label2.Text);
            if (minus_check)
            {
                if (!calculation_check)
                {
                    calculation_check = true;
                    number = 0;
                }
                if (!point_check)
                {
                    point_cnt++;
                    number += (Power(Convert.ToDecimal(0.1), point_cnt) * 8);
                }
                else
                {
                    number = number * 10 + 8;
                }
            }
            else
            {
                if (!calculation_check)
                {
                    calculation_check = true;
                    number = 0;
                }
                if (!point_check)
                {
                    point_cnt++;
                    number -= (Power(Convert.ToDecimal(0.1), point_cnt) * 8);
                }
                else
                {
                    number = number * 10 - 8;
                }
            }
            label1.Text = number.ToString();
        }

        private void button9_Click(object sender, EventArgs e) // 9
        {
            prev_numbers.Push(number);
            prev_label2.Push(label2.Text);
            if (minus_check)
            {
                if (!calculation_check)
                {
                    calculation_check = true;
                    number = 0;
                }
                if (!point_check)
                {
                    point_cnt++;
                    number += (Power(Convert.ToDecimal(0.1), point_cnt) * 9);
                }
                else
                {
                    number = number * 10 + 9;
                }
            }
            else
            {
                if (!calculation_check)
                {
                    calculation_check = true;
                    number = 0;
                }
                if (!point_check)
                {
                    point_cnt++;
                    number -= (Power(Convert.ToDecimal(0.1), point_cnt) * 9);
                }
                else
                {
                    number = number * 10 - 9;
                }
            }
            label1.Text = number.ToString();
        }

        private void button34_Click(object sender, EventArgs e) // e
        {
            number = Convert.ToDecimal(Math.E);
            label1.Text = number.ToString();
            prev_numbers = new Stack();
            prev_label2 = new Stack();
        }

        private void button38_Click(object sender, EventArgs e) // pi
        {
            number = Convert.ToDecimal(Math.PI);
            label1.Text = number.ToString();
            prev_numbers = new Stack();
            prev_label2 = new Stack();
        }
        #endregion

        private void button10_Click(object sender, EventArgs e) // 부호 변경
        {
            number *= -1;
            if (number < 0)
            {
                minus_check = false;
            }
            else
            {
                minus_check = true;
            }
            label1.Text = number.ToString();
        }

        #region calculation
        private void button12_Click(object sender, EventArgs e) // +
        {
            prev = number;
            label2.Text = String.Format("{0} + ", prev.ToString());
            c = next_calculation.plus;
            calculation_check = false;
            label1.Text = number.ToString();
            point_check = true;
            point_cnt = 0;
            minus_check = true;
        }

        private void button13_Click(object sender, EventArgs e) // -
        {
            prev = number;
            label2.Text = String.Format("{0} - ", prev.ToString());
            c = next_calculation.minus;
            calculation_check = false;
            label1.Text = number.ToString();
            point_check = true;
            point_cnt = 0;
            minus_check = true;
            prev_numbers = new Stack();
            prev_label2 = new Stack();
        }

        private void button14_Click(object sender, EventArgs e) // *
        {
            prev = number;
            label2.Text = String.Format("{0} × ", prev.ToString());
            c = next_calculation.multiple;
            calculation_check = false;
            label1.Text = number.ToString();
            point_check = true;
            point_cnt = 0;
            minus_check = true;
            prev_numbers = new Stack();
            prev_label2 = new Stack();
        }

        private void button15_Click(object sender, EventArgs e) // /
        {
            if (number == 0)
            {
                MessageBox.Show("0으로 나눌 수 없습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Error);
                number = prev;
            }
            else
            {
                prev = number;
                label2.Text = String.Format("{0} ÷ ", prev.ToString());
                c = next_calculation.division;
                calculation_check = false;
                label1.Text = number.ToString();
                point_check = true;
                point_cnt = 0;
                minus_check = true;
                prev_numbers = new Stack();
                prev_label2 = new Stack();
            }
        }

        private void button19_Click(object sender, EventArgs e) // mod
        {
            if (c != next_calculation.nul && number != 0)
            {
                calculate();
            }
            else if (number == 0) number = prev;
            prev = number;
            label2.Text = String.Format("{0} % ", prev.ToString());
            c = next_calculation.mod;
            calculation_check = false;
            label1.Text = number.ToString();
            point_check = true;
            point_cnt = 0;
            minus_check = true;
        }

        private void button11_Click(object sender, EventArgs e) // =
        {
            if ((c != next_calculation.nul && number != 0))
            {
                calculate();
                prev_numbers = new Stack();
                prev_label2 = new Stack();
            }
            else if (number == 0 && c != next_calculation.division)
            {
                MessageBox.Show("0으로 나눌 수 없습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Error);
                number = prev;
            }
        }

        private void button18_Click(object sender, EventArgs e) // 소수점
        {
            if (point_check)
            {
                point_check = false;
                label1.Text += ".";
            }
        }

        private void button23_Click(object sender, EventArgs e) // sin
        {
            c = next_calculation.sin;
            prev_prev = number;
            if (c != next_calculation.nul)
            {
                calculate();
            }
            prev = prev_prev;
            calculation_check = false;
            label2.Text = String.Format("sin {0}", prev.ToString());
            label1.Text = number.ToString();
            point_check = true;
            point_cnt = 0;
            minus_check = true;
            prev_numbers = new Stack();
            prev_label2 = new Stack();
        }

        private void button24_Click(object sender, EventArgs e) // cos
        {
            c = next_calculation.cos;
            prev_prev = number;
            if (c != next_calculation.nul)
            {
                calculate();
            }
            prev = prev_prev;
            calculation_check = false;
            label2.Text = String.Format("cos {0}", prev.ToString());
            label1.Text = number.ToString();
            point_check = true;
            point_cnt = 0;
            minus_check = true;
            prev_numbers = new Stack();
            prev_label2 = new Stack();
        }

        private void button25_Click(object sender, EventArgs e) // tan
        {
            c = next_calculation.tan;
            prev_prev = number;
            if (c != next_calculation.nul)
            {
                calculate();
            }
            prev = prev_prev;
            calculation_check = false;
            label2.Text = String.Format("tan {0}", prev.ToString());
            label1.Text = number.ToString();
            point_check = true;
            point_cnt = 0;
            minus_check = true;
            prev_numbers = new Stack();
            prev_label2 = new Stack();
        }

        private void button20_Click(object sender, EventArgs e) // power
        {
            c = next_calculation.power;
            prev_prev = number;
            prev = prev_prev;
            calculation_check = false;
            label2.Text = String.Format("{0}^2 ", prev.ToString());
            label1.Text = number.ToString();
            point_check = true;
            point_cnt = 0;
            minus_check = true;
            prev_numbers = new Stack();
            prev_label2 = new Stack();
        }

        private void button21_Click(object sender, EventArgs e) // sqrt
        {
            c = next_calculation.sqrt;
            prev_prev = number;
            if (c != next_calculation.nul)
            {
                calculate();
            }
            prev = prev_prev;
            calculation_check = false;
            if (prev < 0)
            {
                label1.Text = String.Format("{0}i", number.ToString());
            }
            else
            {
                label1.Text = number.ToString();
            }
            label2.Text = String.Format("√{0}", prev.ToString());
            point_check = true;
            point_cnt = 0;
            minus_check = true;
            prev_numbers = new Stack();
            prev_label2 = new Stack();
        }

        private void button22_Click(object sender, EventArgs e) // 절댓값
        {
            point_check = true;
            point_cnt = 0;
            prev = number;
            if (number < 0)
            {
                number *= -1;
            }
            calculation_check = false;
            label2.Text = String.Format("|{0}| ", prev.ToString());
            label1.Text = number.ToString();
            point_check = true;
            point_cnt = 0;
            prev_numbers = new Stack();
            prev_label2 = new Stack();
        }

        private void button32_Click(object sender, EventArgs e) // log
        {
            c = next_calculation.log;
            prev_prev = number;
            if (c != next_calculation.nul)
            {
                calculate();
            }
            prev = prev_prev;
            calculation_check = false;
            label2.Text = String.Format("log10 {0}", prev.ToString());
            label1.Text = number.ToString();
            point_check = true;
            point_cnt = 0;
            minus_check = true;
            prev_numbers = new Stack();
            prev_label2 = new Stack();
        }

        private void button33_Click(object sender, EventArgs e) // ln
        {
            c = next_calculation.ln;
            prev_prev = number;
            if (c != next_calculation.nul)
            {
                calculate();
            }
            prev = prev_prev;
            calculation_check = false;
            label2.Text = String.Format("ln {0}", prev.ToString());
            label1.Text = number.ToString();
            point_check = true;
            point_cnt = 0;
            minus_check = true;
            prev_numbers = new Stack();
            prev_label2 = new Stack();
        }

        private void button35_Click(object sender, EventArgs e) // 반올림
        {
            prev = number;
            number = Math.Round(number);
            label1.Text = number.ToString();
            label2.Text = String.Format("반올림 {0}", prev);
            calculation_check = false;
            point_check = true;
            point_cnt = 0;
            minus_check = true;
            prev_numbers = new Stack();
            prev_label2 = new Stack();
        }

        private void button36_Click(object sender, EventArgs e) // ceil
        {
            prev = number;
            number = Math.Ceiling(number);
            label1.Text = number.ToString();
            label2.Text = String.Format("⌈{0}⌉", prev);
            calculation_check = false;
            point_check = true;
            point_cnt = 0;
            minus_check = true;
            prev_numbers = new Stack();
            prev_label2 = new Stack();
        }

        private void button37_Click(object sender, EventArgs e) // floor
        {
            prev = number;
            number = Math.Truncate(number);
            label1.Text = number.ToString();
            label2.Text = String.Format("⌊{0}⌋", prev);
            calculation_check = false;
            point_check = true;
            point_cnt = 0;
            minus_check = true;
            prev_numbers = new Stack();
            prev_label2 = new Stack();
        }
        #endregion

        private void button28_Click(object sender, EventArgs e) // 소수 판정
        {
            if (number == Math.Truncate(number) && number > 0)
            {
                if (number == 0)
                {
                    MessageBox.Show("0은 자연수가 아닙니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (number == Math.Truncate(number))
                    {
                        if (IsPrime((BigInteger)number))
                            MessageBox.Show(String.Format("{0}은(는) 소수입니다.", number), "결과", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show(String.Format("{0}은(는) 소수가 아닙니다.", number), "결과", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        MessageBox.Show(String.Format("{0}은(는) 자연수가 아닙니다.", number), "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (number < 0)
            {
                MessageBox.Show(String.Format("{0}은(는) 음수입니다.", number), "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show(String.Format("{0}은(는) 자연수가 아닙니다.", number), "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button29_Click(object sender, EventArgs e) // 소인수분해
        {
            if (number == Math.Truncate(number) && number > 0)
            {
                if (number == 0)
                {
                    MessageBox.Show("0은 자연수가 아닙니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (IsPrime((BigInteger)number))
                {
                    MessageBox.Show(String.Format("{0}은(는) 소수입니다.", number), "결과", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    BigInteger n = (BigInteger)number;
                    Dictionary<BigInteger, BigInteger> factors = new Dictionary<BigInteger, BigInteger>();
                    while (n > 1) // factors에 소인수와 개수 저장
                    {
                        BigInteger factor = pollards_rho(n);
                        if (factors.ContainsKey(factor))
                            factors[factor]++;
                        else
                            factors[factor] = 1;
                        n /= factor;
                    }
                    if (number == Convert.ToInt64(number))
                    {
                        string str = "";
                        BigInteger cnt = 0;
                        foreach (var i in factors)
                        {
                            cnt++;
                            if (i.Value == 1)
                            {
                                str += i.Key.ToString();
                            }
                            else
                            {
                                str += i.Key.ToString();
                                str += "^";
                                str += i.Value.ToString();
                            }
                            if (cnt != factors.Count)
                            {
                                str += " × ";
                            }
                        }
                        MessageBox.Show(String.Format("{0} = {1}", number, str), String.Format("결과"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else if (number < 0)
            {
                MessageBox.Show(String.Format("{0}은(는) 음수입니다.", number), "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show(String.Format("{0}은(는) 자연수가 아닙니다.", number), "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button31_Click(object sender, EventArgs e) // 조합
        {
            Form3 showForm3 = new Form3();
            showForm3.Show();
        }

        private void button26_Click(object sender, EventArgs e) // 거듭 제곱
        {
            Form4 showForm4 = new Form4();
            showForm4.Show();
        }

        private void button27_Click(object sender, EventArgs e) // equation
        {
            Form2 showForm2 = new Form2();
            showForm2.Show();
        }

        private void button39_Click(object sender, EventArgs e) // backspace
        {
            if (c != next_calculation.nul && number == 0)
            {
                c = next_calculation.nul;
                label1.Text = Convert.ToString(prev);
                label2.Text = "";
                number = prev;
            }
            else
            {
                if (!point_check && point_cnt == 0)
                {
                    point_check = true;
                    label1.Text = number.ToString();
                }
                else if (prev_numbers.Count > 0)
                {
                    if (!point_check)
                    {
                        point_cnt--;
                    }
                    number = Convert.ToDecimal(prev_numbers.Peek());
                    prev_numbers.Pop();
                    if (prev_label2.Count > 0)
                    {
                        label2.Text = Convert.ToString(prev_label2.Peek());
                        prev_label2.Pop();
                    }
                    else
                        label2.Text = "";
                    label1.Text = number.ToString();
                    if (point_cnt == 0 && !point_check)
                        label1.Text += ".";
                }
            }
        }

        #region KeyPress
        private void KeyPressEvent(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case '0':
                    button0_Click(sender, e); // 0
                    break;
                case '1':
                    button1_Click(sender, e); // 1
                    break;
                case '2':
                    button2_Click(sender, e); // 2
                    break;
                case '3':
                    button3_Click(sender, e); // 3
                    break;
                case '4':
                    button4_Click(sender, e); // 4
                    break;
                case '5':
                    button5_Click(sender, e); // 5
                    break;
                case '6':
                    button6_Click(sender, e); // 6
                    break;
                case '7':
                    button7_Click(sender, e); // 7
                    break;
                case '8':
                    button8_Click(sender, e); // 8
                    break;
                case '9':
                    button9_Click(sender, e); // 9
                    break;
                case '+':
                    button12_Click(sender, e); // +
                    break;
                case '-':
                    button13_Click(sender, e); // -
                    break;
                case '*':
                    button14_Click(sender, e); // *
                    break;
                case '/':
                    button15_Click(sender, e); // /
                    break;
                case '%':
                    button19_Click(sender, e); // %
                    break;
                case '=':
                case '\n':
                    button11_Click(sender, e); // =
                    break;
                case '.':
                    button18_Click(sender, e); // .
                    break;
                case 'e':
                case 'E':
                    button34_Click(sender, e); // e
                    break;
                case 'p':
                case 'P':
                    button38_Click(sender, e); // pi
                    break;
                case 's':
                case 'S':
                    button23_Click(sender, e); // sin
                    break;
                case 'c':
                case 'C':
                    button24_Click(sender, e); // cos
                    break;
                case 't':
                case 'T':
                    button25_Click(sender, e); // tan
                    break;
                case 'q':
                case 'Q':
                    button21_Click(sender, e); // sqrt
                    break;
                case '^':
                    button20_Click(sender, e); // power
                    break;
                case '|':
                    button22_Click(sender, e); // abs
                    break;
                case 'l':
                case 'L':
                    button32_Click(sender, e); // log
                    break;
                case 'N':
                case 'n':
                    button33_Click(sender, e); // ln
                    break;
                case '\b':
                    button39_Click(sender, e); // backspace
                    break;
                default:
                    break;
            }
        }

        private void button30_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressEvent(sender, e);
        }

        private void button16_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressEvent(sender, e);
        }

        private void button17_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressEvent(sender, e);
        }

        private void button19_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressEvent(sender, e);
        }

        private void button22_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressEvent(sender, e);
        }

        private void button20_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressEvent(sender, e);
        }

        private void button36_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressEvent(sender, e);
        }

        private void button23_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressEvent(sender, e);
        }

        private void button28_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressEvent(sender, e);
        }

        private void button7_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressEvent(sender, e);
        }

        private void button8_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressEvent(sender, e);
        }

        private void button9_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressEvent(sender, e);
        }

        private void button15_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressEvent(sender, e);
        }

        private void button21_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressEvent(sender, e);
        }

        private void button37_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressEvent(sender, e);
        }

        private void button24_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressEvent(sender, e);
        }

        private void button29_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressEvent(sender, e);
        }

        private void button4_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressEvent(sender, e);
        }

        private void button5_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressEvent(sender, e);
        }

        private void button6_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressEvent(sender, e);
        }

        private void button14_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressEvent(sender, e);
        }

        private void button34_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressEvent(sender, e);
        }

        private void button38_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressEvent(sender, e);
        }

        private void button35_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressEvent(sender, e);
        }

        private void button25_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressEvent(sender, e);
        }

        private void button26_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressEvent(sender, e);
        }

        private void button1_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressEvent(sender, e);
        }

        private void button2_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressEvent(sender, e);
        }

        private void button3_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressEvent(sender, e);
        }

        private void button13_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressEvent(sender, e);
        }

        private void button32_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressEvent(sender, e);
        }

        private void button11_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressEvent(sender, e);
        }

        private void button39_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressEvent(sender, e);
        }

        private void button31_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressEvent(sender, e);
        }

        private void button10_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressEvent(sender, e);
        }

        private void button0_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressEvent(sender, e);
        }

        private void button18_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressEvent(sender, e);
        }

        private void button12_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressEvent(sender, e);
        }

        private void button33_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressEvent(sender, e);
        }

        private void button27_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressEvent(sender, e);
        }

        private void Calculator_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressEvent(sender, e);
        }
        #endregion

        private void button40_Click(object sender, EventArgs e) // rad, deg
        {
            deg_check = !deg_check;
            if (deg_check)
            {
                button40.Text = "R a d";
            }
            else
            {
                button40.Text = "D e g";
            }
        }

        #region MouseHover
        private void button0_MouseHover(object sender, EventArgs e) // 0
        {
            ToolTip tooltip = new ToolTip();
            tooltip.UseFading = true;
            tooltip.UseAnimation = true;
            tooltip.IsBalloon = false;
            tooltip.ShowAlways = true;
            tooltip.InitialDelay = 200;
            tooltip.ReshowDelay = 200;
            tooltip.SetToolTip(button0, "Key: 0");
        }

        private void button1_MouseHover(object sender, EventArgs e) // 1
        {
            ToolTip tooltip = new ToolTip();
            tooltip.UseFading = true;
            tooltip.UseAnimation = true;
            tooltip.IsBalloon = false;
            tooltip.ShowAlways = true;
            tooltip.InitialDelay = 200;
            tooltip.ReshowDelay = 200;
            tooltip.SetToolTip(button1, "Key: 1");
        }

        private void button2_MouseHover(object sender, EventArgs e) // 2
        {
            ToolTip tooltip = new ToolTip();
            tooltip.UseFading = true;
            tooltip.UseAnimation = true;
            tooltip.IsBalloon = false;
            tooltip.ShowAlways = true;
            tooltip.InitialDelay = 200;
            tooltip.ReshowDelay = 200;
            tooltip.SetToolTip(button2, "Key: 2");
        }

        private void button3_MouseHover(object sender, EventArgs e) // 3
        {
            ToolTip tooltip = new ToolTip();
            tooltip.UseFading = true;
            tooltip.UseAnimation = true;
            tooltip.IsBalloon = false;
            tooltip.ShowAlways = true;
            tooltip.InitialDelay = 200;
            tooltip.ReshowDelay = 200;
            tooltip.SetToolTip(button3, "Key: 3");
        }

        private void button4_MouseHover(object sender, EventArgs e) // 4
        {
            ToolTip tooltip = new ToolTip();
            tooltip.UseFading = true;
            tooltip.UseAnimation = true;
            tooltip.IsBalloon = false;
            tooltip.ShowAlways = true;
            tooltip.InitialDelay = 200;
            tooltip.ReshowDelay = 200;
            tooltip.SetToolTip(button4, "Key: 4");
        }

        private void button5_MouseHover(object sender, EventArgs e) // 5
        {
            ToolTip tooltip = new ToolTip();
            tooltip.UseFading = true;
            tooltip.UseAnimation = true;
            tooltip.IsBalloon = false;
            tooltip.ShowAlways = true;
            tooltip.InitialDelay = 200;
            tooltip.ReshowDelay = 200;
            tooltip.SetToolTip(button5, "Key: 5");
        }

        private void button6_MouseHover(object sender, EventArgs e) // 6
        {
            ToolTip tooltip = new ToolTip();
            tooltip.UseFading = true;
            tooltip.UseAnimation = true;
            tooltip.IsBalloon = false;
            tooltip.ShowAlways = true;
            tooltip.InitialDelay = 200;
            tooltip.ReshowDelay = 200;
            tooltip.SetToolTip(button6, "Key: 6");
        }

        private void button7_MouseHover(object sender, EventArgs e) // 7
        {
            ToolTip tooltip = new ToolTip();
            tooltip.UseFading = true;
            tooltip.UseAnimation = true;
            tooltip.IsBalloon = false;
            tooltip.ShowAlways = true;
            tooltip.InitialDelay = 200;
            tooltip.ReshowDelay = 200;
            tooltip.SetToolTip(button7, "Key: 7");
        }

        private void button8_MouseHover(object sender, EventArgs e) // 8
        {
            ToolTip tooltip = new ToolTip();
            tooltip.UseFading = true;
            tooltip.UseAnimation = true;
            tooltip.IsBalloon = false;
            tooltip.ShowAlways = true;
            tooltip.InitialDelay = 200;
            tooltip.ReshowDelay = 200;
            tooltip.SetToolTip(button8, "Key: 8");
        }

        private void button9_MouseHover(object sender, EventArgs e) // 9
        {
            ToolTip tooltip = new ToolTip();
            tooltip.UseFading = true;
            tooltip.UseAnimation = true;
            tooltip.IsBalloon = false;
            tooltip.ShowAlways = true;
            tooltip.InitialDelay = 200;
            tooltip.ReshowDelay = 200;
            tooltip.SetToolTip(button9, "Key: 9");
        }

        private void button18_MouseHover(object sender, EventArgs e) // .
        {
            ToolTip tooltip = new ToolTip();
            tooltip.UseFading = true;
            tooltip.UseAnimation = true;
            tooltip.IsBalloon = false;
            tooltip.ShowAlways = true;
            tooltip.InitialDelay = 200;
            tooltip.ReshowDelay = 200;
            tooltip.SetToolTip(button18, "Key: .");
        }

        private void button12_MouseHover(object sender, EventArgs e) // +
        {
            ToolTip tooltip = new ToolTip();
            tooltip.UseFading = true;
            tooltip.UseAnimation = true;
            tooltip.IsBalloon = false;
            tooltip.ShowAlways = true;
            tooltip.InitialDelay = 200;
            tooltip.ReshowDelay = 200;
            tooltip.SetToolTip(button12, "Key: +");
        }

        private void button13_MouseHover(object sender, EventArgs e) // -
        {
            ToolTip tooltip = new ToolTip();
            tooltip.UseFading = true;
            tooltip.UseAnimation = true;
            tooltip.IsBalloon = false;
            tooltip.ShowAlways = true;
            tooltip.InitialDelay = 200;
            tooltip.ReshowDelay = 200;
            tooltip.SetToolTip(button13, "Key: -");
        }

        private void button14_MouseHover(object sender, EventArgs e) // *
        {
            ToolTip tooltip = new ToolTip();
            tooltip.UseFading = true;
            tooltip.UseAnimation = true;
            tooltip.IsBalloon = false;
            tooltip.ShowAlways = true;
            tooltip.InitialDelay = 200;
            tooltip.ReshowDelay = 200;
            tooltip.SetToolTip(button14, "Key: *");
        }

        private void button15_MouseHover(object sender, EventArgs e) // /
        {
            ToolTip tooltip = new ToolTip();
            tooltip.UseFading = true;
            tooltip.UseAnimation = true;
            tooltip.IsBalloon = false;
            tooltip.ShowAlways = true;
            tooltip.InitialDelay = 200;
            tooltip.ReshowDelay = 200;
            tooltip.SetToolTip(button15, "Key: /");
        }

        private void button19_MouseHover(object sender, EventArgs e) // %
        {
            ToolTip tooltip = new ToolTip();
            tooltip.UseFading = true;
            tooltip.UseAnimation = true;
            tooltip.IsBalloon = false;
            tooltip.ShowAlways = true;
            tooltip.InitialDelay = 200;
            tooltip.ReshowDelay = 200;
            tooltip.SetToolTip(button19, "Key: %");
        }

        private void button22_MouseHover(object sender, EventArgs e) // abs
        {
            ToolTip tooltip = new ToolTip();
            tooltip.UseFading = true;
            tooltip.UseAnimation = true;
            tooltip.IsBalloon = false;
            tooltip.ShowAlways = true;
            tooltip.InitialDelay = 200;
            tooltip.ReshowDelay = 200;
            tooltip.SetToolTip(button22, "Key: |");
        }

        private void button20_MouseHover(object sender, EventArgs e) // power
        {
            ToolTip tooltip = new ToolTip();
            tooltip.UseFading = true;
            tooltip.UseAnimation = true;
            tooltip.IsBalloon = false;
            tooltip.ShowAlways = true;
            tooltip.InitialDelay = 200;
            tooltip.ReshowDelay = 200;
            tooltip.SetToolTip(button20, "Key: ^");
        }

        private void button21_MouseHover(object sender, EventArgs e)
        {
            ToolTip tooltip = new ToolTip();
            tooltip.UseFading = true;
            tooltip.UseAnimation = true;
            tooltip.IsBalloon = false;
            tooltip.ShowAlways = true;
            tooltip.InitialDelay = 200;
            tooltip.ReshowDelay = 200;
            tooltip.SetToolTip(button21, "Key: Q, q");
        }

        private void button34_MouseHover(object sender, EventArgs e) // e
        {
            ToolTip tooltip = new ToolTip();
            tooltip.UseFading = true;
            tooltip.UseAnimation = true;
            tooltip.IsBalloon = false;
            tooltip.ShowAlways = true;
            tooltip.InitialDelay = 200;
            tooltip.ReshowDelay = 200;
            tooltip.SetToolTip(button34, "Key: E, e");
        }

        private void button38_MouseHover(object sender, EventArgs e) // pi
        {
            ToolTip tooltip = new ToolTip();
            tooltip.UseFading = true;
            tooltip.UseAnimation = true;
            tooltip.IsBalloon = false;
            tooltip.ShowAlways = true;
            tooltip.InitialDelay = 200;
            tooltip.ReshowDelay = 200;
            tooltip.SetToolTip(button38, "Key: P, p");
        }

        private void button32_MouseHover(object sender, EventArgs e) // log
        {
            ToolTip tooltip = new ToolTip();
            tooltip.UseFading = true;
            tooltip.UseAnimation = true;
            tooltip.IsBalloon = false;
            tooltip.ShowAlways = true;
            tooltip.InitialDelay = 200;
            tooltip.ReshowDelay = 200;
            tooltip.SetToolTip(button32, "Key: L, l");
        }

        private void button33_MouseHover(object sender, EventArgs e) // ln
        {
            ToolTip tooltip = new ToolTip();
            tooltip.UseFading = true;
            tooltip.UseAnimation = true;
            tooltip.IsBalloon = false;
            tooltip.ShowAlways = true;
            tooltip.InitialDelay = 200;
            tooltip.ReshowDelay = 200;
            tooltip.SetToolTip(button33, "Key: N, n");
        }

        private void button23_MouseHover(object sender, EventArgs e) // sin
        {
            ToolTip tooltip = new ToolTip();
            tooltip.UseFading = true;
            tooltip.UseAnimation = true;
            tooltip.IsBalloon = false;
            tooltip.ShowAlways = true;
            tooltip.InitialDelay = 200;
            tooltip.ReshowDelay = 200;
            tooltip.SetToolTip(button23, "Key: S, s");
        }

        private void button24_MouseHover(object sender, EventArgs e) // cos
        {
            ToolTip tooltip = new ToolTip();
            tooltip.UseFading = true;
            tooltip.UseAnimation = true;
            tooltip.IsBalloon = false;
            tooltip.ShowAlways = true;
            tooltip.InitialDelay = 200;
            tooltip.ReshowDelay = 200;
            tooltip.SetToolTip(button24, "Key: C, c");
        }

        private void button25_MouseHover(object sender, EventArgs e) // tan
        {
            ToolTip tooltip = new ToolTip();
            tooltip.UseFading = true;
            tooltip.UseAnimation = true;
            tooltip.IsBalloon = false;
            tooltip.ShowAlways = true;
            tooltip.InitialDelay = 200;
            tooltip.ReshowDelay = 200;
            tooltip.SetToolTip(button25, "Key: T, t");
        }

        private void button11_MouseHover(object sender, EventArgs e) // =
        {
            ToolTip tooltip = new ToolTip();
            tooltip.UseFading = true;
            tooltip.UseAnimation = true;
            tooltip.IsBalloon = false;
            tooltip.ShowAlways = true;
            tooltip.InitialDelay = 200;
            tooltip.ReshowDelay = 200;
            tooltip.SetToolTip(button11, "Key: =");
        }

        private void button39_MouseHover(object sender, EventArgs e) // backspace
        {
            ToolTip tooltip = new ToolTip();
            tooltip.UseFading = true;
            tooltip.UseAnimation = true;
            tooltip.IsBalloon = false;
            tooltip.ShowAlways = true;
            tooltip.InitialDelay = 200;
            tooltip.ReshowDelay = 200;
            tooltip.SetToolTip(button39, "Key: BackSpace");
        }
        #endregion
    }
}