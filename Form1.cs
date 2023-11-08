using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml.Serialization;
using static Kursov_Work_Hord_RootFinder.Form1;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Kursov_Work_Hord_RootFinder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;


            // sin cos
            for (float i = 0; i <= (2 * Math.PI);)
            {
                pointFList.Add(new PointF(i, Convert.ToSingle(Math.Sin(Convert.ToDouble(i)))));
                pointGList.Add(new PointF(i, Convert.ToSingle(Math.Cos(Convert.ToDouble(i)))));

                i += Convert.ToSingle(Math.PI / 4);

            }



            /* // primer2
             for (float i = 0; i <= 16;)
             {
                 pointFList.Add(new PointF(i, Convert.ToSingle(Math.Sqrt(Convert.ToDouble(i)))));
                 i += 2;
             }

             pointGList.Add(new PointF(0f, 2f));
             pointGList.Add(new PointF(16f, 2f));
             // primer2*/

            /*// primer3

            for (float i = 0f; i <= 4;)
            {
                pointFList.Add(new PointF(i, i * i));
                pointGList.Add(new PointF(i, Convert.ToSingle(Math.Cos(Convert.ToDouble(i)))));

                i += 0.25f;
            }

            // primer3*/

            // richBox starter (do not touch)
            richTextBox1.Text = string.Empty;
            foreach (var point in pointFList)
            {
                richTextBox1.Text += $"F-Point{point}\n";
            }

            chartUpdater();
        }

        // f(x), g(x)
        List<PointF> pointFList = new List<PointF>();
        List<PointF> pointGList = new List<PointF>();

        [Serializable]
        public class DataToSerialize
        {
            public List<PointF> PointFList { get; set; }
            public List<PointF> PointGList { get; set; }
        }

        // введення текстбоксів, перевірка на флоат(не число всеодно збереже як 0 )
        private void CheckFloat(System.Windows.Forms.TextBox textBox, Label label)
        {
            if (float.TryParse(textBox.Text.Replace(".", ","), out float result))
            {
                label.Text = string.Empty;
                label.Text = Convert.ToString(result);
            }
            else
            {
                label.Text = "Помилка формату";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
                // додавання точок
                if (label1.Text != "Помилка формату" || label2.Text != "Помилка формату")
                {
                    try
                    {
                        float x, y;
                        float.TryParse(textBox1.Text.Replace(".", ","), out x);
                        float.TryParse(textBox2.Text.Replace(".", ","), out y);
                        if (radioButton1.Checked == true) 
                        {
                        pointFList.Add(new PointF(x, y)); 
                        }
                        else 
                        { 
                        pointGList.Add(new PointF(x, y)); 
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Це повідомлення про помилку створення нової точки.", "Помилка! :(", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally 
                    {
                        richTextBox1.Text = string.Empty;
                        if (radioButton1.Checked == true) 
                        {
                            foreach (var point in pointFList)
                            {
                            richTextBox1.Text += $"F-Point{point}\n";
                            }
                        }
                        else 
                        {
                            foreach (var point in pointGList)
                            {
                            richTextBox1.Text += $"G-Point{point}\n";
                            }
                        }
                        
                    }
                }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            // видалення останньої точки залежно від radioButton
            if(radioButton1.Checked == true) 
            {
                if (pointFList.Count > 0)
                {
                    pointFList.RemoveAt(pointFList.Count - 1);

                    richTextBox1.Text = string.Empty;
                    foreach (var point in pointFList)
                    {
                        richTextBox1.Text += $"F-Point{point}\n";
                    }
                }
            }
            else 
            {
                if (pointGList.Count > 0)
                {
                    pointGList.RemoveAt(pointGList.Count - 1);

                    richTextBox1.Text = string.Empty;
                    foreach (var point in pointGList)
                    {
                        richTextBox1.Text += $"G-Point{point}\n";
                    }
                }
            }
            
        }

        // оновлення лейблів перевірки флоату на число
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            CheckFloat(textBox1, label5);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            CheckFloat(textBox2, label6);
        }

        // метод оновлення графіку, щоб не повторювати тий самий код
        private void chartUpdater() 
        {
            chart1.Series.Clear();
            
            Series series = new Series("f(X)"); 
            series.ChartType = SeriesChartType.Line; 

            foreach (PointF point in pointFList)
            {
                series.Points.AddXY(point.X, point.Y);
            }
   
            Series series2 = new Series("g(X)"); 
            series2.ChartType = SeriesChartType.Line; 

            foreach (PointF point in pointGList)
            {
                series2.Points.AddXY(point.X, point.Y);
            }

            chart1.Series.Add(series);
            chart1.Series.Add(series2);

           
            chart1.Invalidate();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            chartUpdater();
        }

        // у разі натискання radioBox-ів оновлювати richTextBox з тими точками, які обирав користувач
        private void radioButton1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = string.Empty;
            foreach (var point in pointFList)
            {
                richTextBox1.Text += $"F-Point{point}\n";
            }
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = string.Empty;
            foreach (var point in pointGList)
            {
                richTextBox1.Text += $"G-Point{point}\n";
            }
        }

        // math 

        // interface
        public interface IInterpolation
        {
            float LagrangeInterpolation(List<PointF> pointList, float x);
            float LinearInterpolation(List<PointF> pointList, float x);
        }
        public interface IRootFinder
        {
            float FindRoot(List<PointF> PointsF, List<PointF> PointsG, float epsilon = 1e-6f, int maxIterations = 1000);
        }
        
        // class
        public class Interpolator : IInterpolation
        {
            public float LagrangeInterpolation(List<PointF> pointList, float x)
            {
                float result = 0;

                for (int i = 0; i < pointList.Count; i++)
                {
                    float term = pointList[i].Y;
                    for (int j = 0; j < pointList.Count; j++)
                    {
                        if (i != j)
                        {
                            term = term * (x - pointList[j].X) / (pointList[i].X - pointList[j].X);
                        }
                    }
                    result += term;
                }
                return result;
            }

            public float LinearInterpolation(List<PointF> pointList, float x)
            {
                if (pointList.Any())
                {
                    pointList = pointList.OrderBy(p => p.X).ToList();

                    if (x < pointList.First().X || x > pointList.Last().X)
                    {
                        MessageBox.Show("Введене значення x знаходиться поза діапазоном доступних точок.");
                        return float.NaN;
                    }

                    int lowerBoundIndex = 0;
                    int upperBoundIndex = 1;

                    for (int i = 0; i < pointList.Count - 1; i++)
                    {
                        if (x >= pointList[i].X && x <= pointList[i + 1].X)
                        {
                            lowerBoundIndex = i;
                            upperBoundIndex = i + 1;
                            break;
                        }
                    }

                    float lowerBoundX = pointList[lowerBoundIndex].X;
                    float upperBoundX = pointList[upperBoundIndex].X;
                    float lowerBoundY = pointList[lowerBoundIndex].Y;
                    float upperBoundY = pointList[upperBoundIndex].Y;

                    float interpolatedValue = lowerBoundY + (upperBoundY - lowerBoundY) * (x - lowerBoundX) / (upperBoundX - lowerBoundX);
                    return interpolatedValue;
                }
                else
                {
                    MessageBox.Show("Список точок пустий.");
                    return float.NaN;
                }
            }
        }


        public class ChordSolver : Interpolator, IRootFinder
        {
            public float FindRoot(List<PointF> PointsF, List<PointF> PointsG, float epsilon = 1e-6f, int maxIterations = 100)
            {
                float x0 = 0.0f; // Початкове наближене значення x
                float x1 = 1.0f; // Друге початкове наближене значення x

                for (int iteration = 0; iteration < maxIterations; iteration++)
                {
                    float f0 = LagrangeInterpolation(PointsF, x0);
                    float g0 = LinearInterpolation(PointsG, x0);

                    float f1 = LagrangeInterpolation(PointsF, x1);
                    float g1 = LinearInterpolation(PointsG, x1);

                    float nextX = x1 - (f1 - g1) * (x1 - x0) / (f1 - g1 - (f0 - g0));

                    if (Math.Abs(nextX - x1) < epsilon)
                    {
                        return nextX; // Знайдено корінь з заданою точністю
                    }

                    x0 = x1;
                    x1 = nextX;
                }

                throw new Exception("Не вдалося знайти корінь за вказаним числом ітерацій.");
            }
        }


        private void button6_Click(object sender, EventArgs e)
        {
            //FindRoot(pointFList, pointGList, maxIterations: 1000);
            ChordSolver solver = new ChordSolver();
            float answer = solver.FindRoot(pointFList, pointGList);
            label8.Text = Convert.ToString(answer);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML files (*.xml)|*.xml";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Serialize data and save it to the selected XML file
                    DataToSerialize data = new DataToSerialize
                    {
                        PointFList = new List<PointF>(pointFList),
                        PointGList = new List<PointF>(pointGList)
                    };


                    XmlSerializer serializer = new XmlSerializer(typeof(DataToSerialize));
                    using (TextWriter writer = new StreamWriter(saveFileDialog.FileName))
                    {
                        serializer.Serialize(writer, data);
                    }

                    label9.Text = $"Data saved to file:\n {saveFileDialog.FileName}";
                }
                catch (Exception ex)
                {
                    label9.Text = $"Error of data saving: {ex.Message}";
                }
            }

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openXML = new OpenFileDialog();
            openXML.Filter = "XML files (*.xml)|*.xml";

            if (openXML.ShowDialog() == DialogResult.OK) 
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(DataToSerialize));
                    using (TextReader reader = new StreamReader(openXML.FileName))
                    {
                        DataToSerialize loadedData = (DataToSerialize)serializer.Deserialize(reader);
                        pointFList = loadedData.PointFList;
                        pointGList = loadedData.PointGList;

                        richTextBox1.Text = string.Empty;
                        if (radioButton1.Checked == true)
                        {
                            foreach (var point in pointFList)
                            {
                                richTextBox1.Text += $"F-Point{point}\n";
                            }
                        }
                        else
                        {
                            foreach (var point in pointGList)
                            {
                                richTextBox1.Text += $"G-Point{point}\n";
                            }
                        }
                        label9.Text = $"File opened:\n{openXML.FileName}";
                        chartUpdater();
                    }
                }
                catch (Exception ex)
                {
                    label9.Text = $"Error of data loading: {ex.Message}";
                }
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pointFList.Clear();
            pointGList.Clear();
            richTextBox1.Text = string.Empty;
        }

        private void saveToReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.txt)|*.txt";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    DataToSerialize data = new DataToSerialize
                    {
                        PointFList = new List<PointF>(pointFList),
                        PointGList = new List<PointF>(pointGList)
                    };




                    using (TextWriter writer = new StreamWriter(saveFileDialog.FileName))
                    {
                        // fx all points saver
                        string dataToSave = $"Point f(x) data:\n"; 
                        writer.Write(dataToSave);

                        foreach (var point in pointFList)
                        {
                            dataToSave = $"{point}\n";
                            writer.Write(dataToSave);
                        }


                        // gx all points saver
                        dataToSave = $"\nPoint g(x) data:\n";
                        writer.Write(dataToSave);

                        foreach (var point in pointGList)
                        {
                            dataToSave = $"{point}\n";
                            writer.Write(dataToSave);
                        }

                        writer.Write("\n\n\n");

                        // result saver
                        dataToSave = $"Result: {label8.Text}"; 
                        writer.Write(dataToSave);
                    }


                    label9.Text = $"Report saved to file:\n {saveFileDialog.FileName}";
                }

                catch (Exception ex)
                {
                    label9.Text = $"Error of data saving: {ex.Message}";
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        
    }
}

