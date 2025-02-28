using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Calculator
{
    public partial class Form5 : Form
    {
        String T = "Blue";
        public Form5() //생성자
        {
            InitializeComponent();
            //이벤트 설정
            Load += Form_Load;
            Resize += Form_Resize;
            this.drawButton.Click += drawButton_Click;
        }

        private void Form_Load(object sender, EventArgs e) // 폼 로드시 처리하기
        {
            DrawGraph();
        }

        private void Form_Resize(object sender, EventArgs e) // 폼 크기 조정시 처리하기
        {
            DrawGraph();
        }

        private MethodInfo CompileFunction(string equationText) // 함수 컴파일하기
        {
            string functionText = "using System;" +
                                  "public static class Evaluator" +
                                  "{" +
                                  "    public static double Evaluate(double x)" +
                                  "    {" +
                                  "        return " + equationText + ";" +
                                  "    }" +
                                  "}";

            CodeDomProvider codeDomProvider = CodeDomProvider.CreateProvider("C#");

            CompilerParameters compilerParameters = new CompilerParameters();

            compilerParameters.GenerateInMemory = true;
            compilerParameters.GenerateExecutable = false;

            CompilerResults compilerResults = codeDomProvider.CompileAssemblyFromSource(compilerParameters, functionText);

            if (compilerResults.Errors.Count > 0)
            {
                string message = "수식 컴파일시 에러가 발생했습니다.";

                foreach (CompilerError compilerError in compilerResults.Errors)
                {
                    message += "\n" + compilerError.ErrorText;
                }

                throw new InvalidProgramException(message);
            }
            else
            {
                Type evaluatorType = compilerResults.CompiledAssembly.GetType("Evaluator");

                return evaluatorType.GetMethod("Evaluate");
            }
        }

        private float ExecuteGraphFunction(MethodInfo functionMethodInfo/*함수 메소드 정보*/, float x) // 그래프 함수 실행하기
        {
            double result = (double)functionMethodInfo.Invoke(null, new object[] { x });

            return (float)result;
        }

        private void DrawGraph() // 그래프 그리기
        {
            float minimumX = float.Parse(this.minimumXTextBox.Text);
            float maximumX = float.Parse(this.maximumXTextBox.Text);
            float minimumY = float.Parse(this.minimumYTextBox.Text);
            float maximumY = float.Parse(this.maximumYTextBox.Text);

            int width = this.canvasPictureBox.ClientSize.Width;
            int height = this.canvasPictureBox.ClientSize.Height;

            Bitmap bitmap = new Bitmap(width, height);

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;

                RectangleF rectangle = new RectangleF
                (
                    minimumX,
                    minimumY,
                    maximumX - minimumX,
                    maximumY - minimumY
                );

                PointF[] pointArray =
                {
                    new PointF(0, height),
                    new PointF(width, height),
                    new PointF(0, 0),
                };

                graphics.Transform = new Matrix(rectangle, pointArray);

                using (Pen pen = new Pen(Color.White, 0))
                {
                    graphics.DrawLine(pen, minimumX, 0, maximumX, 0);
                    graphics.DrawLine(pen, 0, minimumY, 0, maximumY);

                    pen.Color = Color.FromName(T);

                    Matrix inverseMatrix = graphics.Transform;

                    inverseMatrix.Invert();

                    PointF[] pixelPointArray =
                    {
                        new PointF(0, 0),
                        new PointF(1, 0)
                    };

                    inverseMatrix.TransformPoints(pixelPointArray);

                    float dx = pixelPointArray[1].X - pixelPointArray[0].X;

                    dx /= 2;

                    MethodInfo functionMethodInfo = null;

                    try
                    {
                        functionMethodInfo = CompileFunction(this.equationTextBox.Text);
                    }
                    catch (Exception expcetion)
                    {
                        MessageBox.Show
                        (
                            this,
                            expcetion.Message,
                            "ERROR",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );

                        return;
                    }

                    List<PointF> pointList = new List<PointF>();

                    for (float x = minimumX; x <= maximumX; x += dx)
                    {
                        bool validPoint = false;

                        try
                        {
                            float y = ExecuteGraphFunction(functionMethodInfo, x);

                            if (pointList.Count == 0)
                            {
                                validPoint = true;
                            }
                            else
                            {
                                float dy = y - pointList[pointList.Count - 1].Y;

                                if (Math.Abs(dy / dx) < 1000)
                                {
                                    validPoint = true;
                                }
                            }

                            if (validPoint)
                            {
                                pointList.Add(new PointF(x, y));
                            }
                        }
                        catch (Exception exception)
                        {
                            MessageBox.Show
                            (
                                this,
                                "함수 평가시 에러가 발생했습니다.\n" + exception.Message,
                                "ERROR",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error
                            );
                        }

                        if (!validPoint)
                        {
                            if (pointList.Count > 1)
                            {
                                graphics.DrawLines(pen, pointList.ToArray());
                            }

                            pointList.Clear();
                        }
                    }

                    if (pointList.Count > 1)
                    {
                        graphics.DrawLines(pen, pointList.ToArray());
                    }
                }
            }

            this.canvasPictureBox.Image = bitmap;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            T = comboBox1.SelectedItem.ToString();
            DrawGraph();
        }

        private void drawButton_Click(object sender, EventArgs e) // 그리기 버튼 눌렀을 때 처리
        {
            DrawGraph();
        }
    }
}