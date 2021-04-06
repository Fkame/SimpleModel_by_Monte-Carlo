using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace MiniGroupWork1.Utils
{
    public class DefaultElementSetuper
    {
        public Chart FieldToDraw { get; set; }
        public DataGridView TableForOutPut { get; set; }
        public Form MainForm { get; set; }
        public SeriesGraphsIdxes idxes { get; private set; }

        public DefaultElementSetuper (Chart chart, DataGridView grid, Form form)
        {
            FieldToDraw = chart;
            TableForOutPut = grid;
            MainForm = form;

            idxes = new SeriesGraphsIdxes();
        }

        public SeriesGraphsIdxes SetDefaultConfigurationsForElements()
        {
            SetFormConfigurations();
            SetChartConfigurations();
            SetGridConfigurations();
            return this.idxes;
        }

        public SeriesGraphsIdxes SetChartConfigurations()
        {
            // Basic charts
            var allGraprics = FieldToDraw.Series;

            allGraprics[0].Name = "Not matched";
            allGraprics[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            allGraprics[0].Color = ColorTranslator.FromHtml("#A60000");
            idxes.notMatchedPointSeriesIdx = 0;

            allGraprics.Add("Matched");
            allGraprics[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            allGraprics[1].Color = ColorTranslator.FromHtml("#59EA3A");
            idxes.matchedPointSeriesIdx = 1;

            allGraprics.Add("Main funk #1");
            allGraprics[2].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            allGraprics[2].Color = ColorTranslator.FromHtml("#FFE040");
            allGraprics[2].BorderWidth = 4;
            idxes.mainFunk1Idx = 2;

            allGraprics.Add("Main funk #2");
            allGraprics[3].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            allGraprics[3].Color = ColorTranslator.FromHtml("#33CEC3");
            allGraprics[3].BorderWidth = 4;
            idxes.mainFunk2Idx = 3;

            allGraprics.Add("Limits");
            allGraprics[4].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            allGraprics[4].Color = ColorTranslator.FromHtml("#65016C");
            allGraprics[4].BorderWidth = 4;
            idxes.limitsIdx = 4;

            FieldToDraw.BackImage = BackgroundImages_Paths.chartBackground;

            SetCharAxesConfigurations();
            return this.idxes;
        }

        public void SetCharAxesConfigurations()
        {
            var axes = FieldToDraw.ChartAreas;
            axes[0].BackImage = BackgroundImages_Paths.chartAxesBackground;

            axes[0].AxisX.Crossing = 0;
            axes[0].AxisY.Crossing = 0;

            axes[0].AxisX.LineWidth = 3;
            axes[0].AxisY.LineWidth = 3;

            axes[0].AxisX.MajorGrid.Enabled = false;
            axes[0].AxisY.MajorGrid.Enabled = false;
        }

        public void SetFormConfigurations()
        {
            MainForm.BackgroundImage = Image.FromFile(BackgroundImages_Paths.formBackground);
        }

        public void SetGridConfigurations()
        {
            TableForOutPut.RowHeadersVisible = false;
            TableForOutPut.ColumnHeadersVisible = false;
        }
    }
}
