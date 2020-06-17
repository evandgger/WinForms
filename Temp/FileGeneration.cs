using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Temp
{
    public partial class FileGeneration : Form
    {
        public double A;
        public double B;

        public FileGeneration()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // srand(time(NULL));
            var random = new Random();
            
            var inputs = new List<string>();

            foreach (var textBox in Controls.OfType<TextBox>())
            {
                inputs.Add(textBox.Text);
            }

            if (inputs.Any(string.IsNullOrEmpty))
            {
                MessageBox.Show("Not all inputs are set");
            }
            else
            {
                // считываем все поля ввода
                var countOfPoint = Convert.ToInt32(textBox1.Text);
                var lowerBorder = Convert.ToInt32(textBox2.Text);
                var upperBorder = Convert.ToInt32(textBox3.Text);

                if (lowerBorder > upperBorder)
                {
                    var tmp = lowerBorder;
                    lowerBorder = upperBorder;
                    upperBorder = tmp;
                }

                if (countOfPoint < 0)
                {
                    countOfPoint = Math.Abs(countOfPoint);
                }

                // создаём пустые списки чисел для Х и У
                var listOfX = new List<double>();
                var listOfY = new List<int>();

                // считаем длину шага (длина отрезка)/(количество точек - 1)
                var step = (B - A) / (countOfPoint - 1);

                // идём от левого края до правого с шагом ^^^
                for (var x = A; x <= B; x += step)
                {
                    listOfX.Add(x);

                    // lowerBorder до UpperBorder    C++: - lowerBorder + rand() % (upperBorder-lowerBorder)
                    var y = lowerBorder + RandomNumber(random, 0, (upperBorder - lowerBorder));

                    listOfY.Add(y);
                }

                // удаляем файл, чтоб не было коллизий
                File.Delete(@"c:/temp/test.txt");

                // создаём новый текстовый файл
                var file = File.CreateText(@"c:/temp/test.txt");

                // выводим список Х, разделенных между собой пробелами
                file.WriteLine(string.Join(" ", listOfX));

                // выводим список У, разделенных между собой пробелами
                file.WriteLine(string.Join(" ", listOfY));

                // закрываем файл для записи
                file.Close();

                // закрываем форму
                Close();
            }
        }

        public int RandomNumber(Random random, int min, int max)
        {
            // возвращает рандомное число от min до max
            
            // min>0 max>0 min<max
            return random.Next(min, max);
        }
    }
}