using System;

namespace MiniGroupWork1
{
    partial class MainForm
    {
        private void DrawMainChart1(double xmin, double xmax)
        {
            double x = xmin;
            double step = 0.1;
            while (x < xmax)
            {
                x = Math.Round(x, 2);
                double y = Math.Round(CalcFunk1(x), 2);
                chart1.Series[SeriesIdxes.mainFunk1Idx].Points.AddXY(x, y);
                x += step;
            }

            if (x < xmax)
            {
                x = Math.Round(xmax, 2);
                double y = Math.Round(CalcFunk1(x), 2);
                chart1.Series[SeriesIdxes.mainFunk1Idx].Points.AddXY(x, y);
            }
        }

        private void DrawMainChart2(double xmin, double xmax)
        {
            chart1.Series[SeriesIdxes.mainFunk2Idx].Points.AddXY(xmin, CalcFunk2(xmin));
            chart1.Series[SeriesIdxes.mainFunk2Idx].Points.AddXY(xmax, CalcFunk2(xmax));
        }

        private void DrawLimits((int xmin, int xmax, int ymin, int ymax) limits)
        {
            chart1.Series[SeriesIdxes.limitsIdx].Points.AddXY(limits.xmin, limits.ymin);
            chart1.Series[SeriesIdxes.limitsIdx].Points.AddXY(limits.xmin, limits.ymax);
            chart1.Series[SeriesIdxes.limitsIdx].Points.AddXY(limits.xmax, limits.ymax);
            chart1.Series[SeriesIdxes.limitsIdx].Points.AddXY(limits.xmax, limits.ymin);
            chart1.Series[SeriesIdxes.limitsIdx].Points.AddXY(limits.xmin, limits.ymin);
        }
    }
}
