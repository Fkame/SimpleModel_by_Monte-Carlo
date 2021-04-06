using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MiniGroupWork1.Utils;

namespace MiniGroupWork1
{
    partial class MainForm
    {
        /// <summary>
        /// Запуск моделирования
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            CleaningData();

            uint pointsAmount = 0;
            if (!uint.TryParse(textBox5.Text, out pointsAmount))
            {
                pointsAmount = (uint)pointsAmountDefault;
                textBox5.Text = pointsAmount.ToString();
            }

            uint runsAmount = 0;
            if (!uint.TryParse(textBox6.Text, out runsAmount))
            {
                runsAmount = (uint)runsAmountDefault;
                textBox6.Text = runsAmount.ToString();
            }

            GenerateDataGrid(runsAmount);

            var limits = GetLimitsFromForm();
            double areasSum = 0;
            for (int i = 0; i < runsAmount; i++)
            {
                System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                sw.Start();
                int hit = MakeRun(pointsAmount, limits.xmin, limits.xmax, limits.ymin, limits.ymax);
                sw.Stop();

                double timeOfWorking = sw.Elapsed.TotalMilliseconds;
                double successProcent = Math.Round((double)pointsAmount / hit, 2);
                double outerFigureArea = Math.Round((double)(limits.xmax - limits.xmin) * (limits.ymax - limits.ymin), 2);
                double innerFigureArea = Math.Round(hit * outerFigureArea / pointsAmount, 2);

                dataGridView1.Rows[1].Cells[i + 1].Value = hit.ToString();
                dataGridView1.Rows[2].Cells[i + 1].Value = (pointsAmount - hit).ToString();
                dataGridView1.Rows[3].Cells[i + 1].Value = successProcent.ToString();
                dataGridView1.Rows[4].Cells[i + 1].Value = outerFigureArea.ToString();
                dataGridView1.Rows[5].Cells[i + 1].Value = innerFigureArea.ToString();
                dataGridView1.Rows[6].Cells[i + 1].Value = timeOfWorking.ToString() + " мс";

                areasSum += innerFigureArea;
            }

            areasSum = Math.Round(areasSum, 3);

            dataGridView1.RowCount += 1;
            dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[0].Value = "Средняя площадь";
            dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[1].Value = (areasSum / runsAmount).ToString();
        }

        private int MakeRun(uint pointsAmount, int xmin, int xmax, int ymin, int ymax)
        {
            if (xmax < xmin)
            {
                int temp = xmin;
                xmin = xmax;
                xmax = temp;
            }

            if (ymax < ymin)
            {
                int temp = ymin;
                ymin = ymax;
                ymax = temp;
            }

            int hit = 0;
            for (uint i = 0; i < pointsAmount; i++)
            {
                double x = Math.Round(xmin + rand.NextDouble() * (xmax - xmin), 2);
                double y = Math.Round(ymin + rand.NextDouble() * (ymax - ymin), 2);

                if ((y > CalcFunk2(x)) & (y < CalcFunk1(x)))
                {
                    hit += 1;

                    chart1.Series[SeriesIdxes.matchedPointSeriesIdx].Points.AddXY(x, y);
                }
                else
                {
                    chart1.Series[SeriesIdxes.notMatchedPointSeriesIdx].Points.AddXY(x, y);
                }
            }
            return hit;
        }

        private void CleaningData()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            chart1.Series[SeriesIdxes.notMatchedPointSeriesIdx].Points.Clear();
            chart1.Series[SeriesIdxes.matchedPointSeriesIdx].Points.Clear();
        }
    }
}
