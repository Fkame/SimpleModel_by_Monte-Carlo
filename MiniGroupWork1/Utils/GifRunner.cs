using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Windows.Forms.DataVisualization.Charting;

namespace MiniGroupWork1.Utils
{
    public class GifRunner
    {
        public Timer FrameTimer { get; set; }
        public PictureBox GifCanvas { get; set; }

        public int FrameRate { get; set; } = 80;

        private System.Drawing.Imaging.FrameDimension dim;
        private int frame = 0;

        public GifRunner(Timer timer, PictureBox picBox)
        {
            FrameTimer = timer;
            GifCanvas = picBox;

            Image gifImg = Image.FromFile(BackgroundImages_Paths.gif);

            GifCanvas.BackgroundImage = gifImg;
            Guid[] dims = gifImg.FrameDimensionsList;

            if (dims.Length == 0)
            {
                throw new Exception("Error with image frames = 0");
            }

            dim = new System.Drawing.Imaging.FrameDimension(dims[0]);

            FrameTimer.Interval = FrameRate;
            FrameTimer.Start();
        }

        public void ChangeFrame()
        {
            Image gifImg = GifCanvas.BackgroundImage;

            int count = gifImg.GetFrameCount(dim);
            frame++;
            if (frame >= count) frame = 0;
            gifImg.SelectActiveFrame(dim, frame);
            GifCanvas.Invalidate();
            GifCanvas.Update();
        }


    }
}
