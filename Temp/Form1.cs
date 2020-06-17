using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Cursor = System.Windows.Forms.Cursor;

namespace Temp
{
    public partial class Form1 : Form
    {
        public Form1()
        { 
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DrawGraphics();
        }

        public void DrawGraphics()
        {
            Cursor = Cursors.WaitCursor;

            // попытаться сделать кусок кода в try, если возникнет ошибка - выполнится код catch
            try
            {
                var inputs = new List<string>();

                // собираем данные со всех инпутов (текстбоксов)
                foreach (var textBox in Controls.OfType<TextBox>())
                {
                    inputs.Add(textBox.Text);
                }

                // проверка, заполнены ли текстовые поля (stringIsNullOrEmpty (textA == null || textA == "")) 
                if (inputs.Any(string.IsNullOrEmpty))
                {
                    MessageBox.Show("A or B is not set");
                }
                else
                {
                    var a = Convert.ToDouble(textBox1.Text);
                    var b = Convert.ToDouble(textBox2.Text);

                    // если начало больше, чем конец, меняем местами

                    if (a > b)
                    {
                        var tmp = a;
                        a = b;
                        b = tmp;
                    }

                    Series graphic1 = chart1.Series["y = x"];
                    Series graphic2 = chart1.Series["y = cos(x)"];
                    Series graphic3 = chart1.Series["from file"];


                    // очищаем все раннее добавленные точки 

                    graphic1.Points.Clear();
                    graphic2.Points.Clear();
                    graphic3.Points.Clear();

                    // достаём график

                    // если чекбокс нажат
                    if (checkBox1.Checked)
                    {
                        // Добавляем точки
                        for (double i = a; i <= b; i++)
                        {
                            double x = i;
                            double y = Y(x);

                            graphic1.Points.AddXY(x, y);
                        }
                    }

                    // если чекбокс нажат
                    if (checkBox2.Checked)
                    {
                        // Добавляем точки
                        for (double i = a; i <= b; i++)
                        {
                            double x = i;
                            double y = Cos(x);

                            graphic2.Points.AddXY(x, y);
                        }
                    }

                    // если чекбокс нажат

                    if (checkBox3.Checked)
                    {
                        // считываем файл в виде массива строк
                        var fileContent = File.ReadAllLines(@"c:/temp/test.txt");

                        // раздеяем строку X и Y
                        var stringOfX = fileContent[0];
                        var stringOfY = fileContent[1];

                        // разделяем х и y через пробел
                        var listOfX = stringOfX.Split(' ');
                        var listOfY = stringOfY.Split(' ');

                        for (var i = 0; i < listOfX.Length; i++)
                        {
                            // преобразовываем х и у, записанные в виде строки в тип double
                            var x = Convert.ToDouble(listOfX[i]);
                            var y = Convert.ToDouble(listOfY[i]);

                            graphic3.Points.AddXY(x, y);
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Unexpected error occured");
            }

            Cursor = Cursors.Default;
        }

        public double MyY(double x)
        {
            if (x == 0.0)
            {
                return 0;
            }
            else if (x >= 6 && x <= 7)
            {
                return x;
            }
            else if (x >= 1 && x <= 3)
            {
                return Math.Pow(x, 2);
            }
            else
            {
                var mainPart = Math.Sqrt(Math.Sqrt(x));

                return Math.Pow(mainPart, 2);
            }
        }

        // Сюда подставить вашу функцию
        public double Y(double x)
        {
            return x;
        }

        public double Cos(double x)
        {
            return Math.Cos(x);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // object - базовый тип, от которого наследуются все ссылочные типы

            // привести объект типа object к типу ComboBox
            var comboBox  = (ComboBox) sender;

            // достаём выбранный элемент
            var selectedType = comboBox.SelectedItem.ToString();

            // проходимся по всем графикам
            var graphics = chart1.Series;

            foreach (var graphic in graphics)
            {
                switch (selectedType)
                {
                    case "Spline":
                    {
                        graphic.ChartType = SeriesChartType.Spline;
                        break;
                    }
                    case "Point":
                    {
                        graphic.ChartType = SeriesChartType.Point;
                        break;
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var a = Convert.ToDouble(textBox1.Text);
                var b = Convert.ToDouble(textBox2.Text);

                var childForm = new FileGeneration
                {
                    A = a,
                    B = b
                };

                childForm.Show();
            }
            catch
            {
                
            }
        }

        private void aboutProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var about = new About();

            about.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
