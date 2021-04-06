using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MiniGroupWork1.Utils;

namespace MiniGroupWork1
{
    public partial class MainForm : Form
    {
        private short pointsAmountDefault = 100;
        private short runsAmountDefault = 1;
        private readonly (int xmin, int xmax, int ymin, int ymax) DefaultLimits = (-1, 9, -1, 3);

        Random rand = new Random();
        GifRunner gif;
        SeriesGraphsIdxes SeriesIdxes;

        public MainForm()
        {
            InitializeComponent();
            SetUpSomeOptions();
        }

        private void SetUpSomeOptions()
        {
            DefaultElementSetuper defaultConf = new DefaultElementSetuper(chart1, dataGridView1, this);
            SeriesIdxes = defaultConf.SetDefaultConfigurationsForElements();
            
            var limits = DefaultLimits;
            ShowCurrentLimits(limits);
            DrawMainChart1(limits.xmin, limits.xmax);
            DrawMainChart2(limits.xmin, limits.xmax);

            DrawLimits(limits);      

            gif = new GifRunner(timer1, pictureBox1);
        }

        private double CalcFunk1(double argument)
        {
            return Math.Sqrt(argument);
        }

        private double CalcFunk2(double argument)
        {
            return 0.5 * argument;
        }
           
        private (int xmin, int xmax, int ymin, int ymax) GetLimitsFromForm()
        {
            int xmin, xmax, ymin, ymax = 0;
            if (!int.TryParse(textBox1.Text, out xmin)) 
                xmin = DefaultLimits.xmin;
            if (!int.TryParse(textBox2.Text, out xmax))
                xmax = DefaultLimits.xmax;
            if (!int.TryParse(textBox3.Text, out ymin))
                ymin = DefaultLimits.ymin;
            if (!int.TryParse(textBox4.Text, out ymax))
                ymax = DefaultLimits.ymax;

            return (xmin, xmax, ymin, ymax);
        }

        /// <summary>
        /// Перерисовка границ под ново-введённые данные
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Series[SeriesIdxes.limitsIdx].Points.Clear();
            var limits = GetLimitsFromForm();
            ShowCurrentLimits(limits);
            DrawLimits(limits);
        }

        private void GenerateDataGrid(uint cols)
        { 
            for (uint i = 0; i < cols + 1; i++)
            {
                dataGridView1.Columns.Add((i + 1).ToString(), "");
            }

            dataGridView1.Rows.Add(7);

            for (int i = 0; i < cols; i++)
            {
                dataGridView1.Rows[0].Cells[i + 1].Value = String.Format("Прогон №{0}", i + 1);
            }

            dataGridView1.Rows[1].Cells[0].Value = "Точек попало в фигуру";
            dataGridView1.Rows[2].Cells[0].Value = "Точек мимо";
            dataGridView1.Rows[3].Cells[0].Value = "Процент попаданий";
            dataGridView1.Rows[4].Cells[0].Value = "Площадь ограничивающей фигуры";
            dataGridView1.Rows[5].Cells[0].Value = "Вычислинная площадь вложенной фигуры";
            dataGridView1.Rows[6].Cells[0].Value = "Время обработки модели";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            gif.ChangeFrame();
        }

        private void ShowCurrentLimits((int xmin, int xmax, int ymin, int ymax) limits)
        {
            textBox1.Text = limits.xmin.ToString();
            textBox2.Text = limits.xmax.ToString();
            textBox3.Text = limits.ymin.ToString();
            textBox4.Text = limits.ymax.ToString();

        }
    }
}
