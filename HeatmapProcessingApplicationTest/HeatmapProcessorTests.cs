using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;

namespace HeatmapProcessingApplicationTest
{
    public class HeatmapProcessorTests
    {
        public void ProcessImage()
        {
            Mat imageOriginal = CvInvoke.Imread("Heineken.jpg", LoadImageType.AnyColor);

            var imageWithHitsBgr = CreateHitImage(imageOriginal.Size);
            imageWithHitsBgr.Save("Test hit bgr.jpg");
            var imageWithHitsGray = new Mat();
            CvInvoke.CvtColor(imageWithHitsBgr, imageWithHitsGray, ColorConversion.Bgr2Gray);
            var mask = new Mat();
            CvInvoke.Threshold(imageWithHitsGray, mask, 1, 255, ThresholdType.Binary);
            mask.Save("Test mask.jpg");
            var inverseMask = new Mat();
            CvInvoke.BitwiseNot(mask, inverseMask);
            inverseMask.Save("Test inverse mask.jpg");

            CvInvoke.ApplyColorMap(imageWithHitsBgr, imageWithHitsBgr, ColorMapType.Jet);
            var imageWithHitsWithoutBackground = new Mat();
            CvInvoke.BitwiseAnd(imageWithHitsBgr, imageWithHitsBgr, imageWithHitsWithoutBackground, mask);
            imageWithHitsWithoutBackground.Save("Test hit bgr without background.jpg");

            int ch = imageOriginal.NumberOfChannels;
            VectorOfMat vMat = new VectorOfMat(ch);
            CvInvoke.Split(imageOriginal, vMat);
            Mat b = vMat[0];
            Mat g = vMat[1];
            Mat r = vMat[2];
            b.Save("Test original image B channel.jpg");
            g.Save("Test original image G channel.jpg");
            r.Save("Test original image R channel.jpg");
            CvInvoke.BitwiseAnd(b, b, b, inverseMask);
            CvInvoke.BitwiseAnd(g, g, g, inverseMask);
            CvInvoke.BitwiseAnd(r, r, r, inverseMask);
            b.Save("Test channel B after masking.jpg");
            g.Save("Test channel G after masking.jpg");
            r.Save("Test channel R after masking.jpg");
        }

        private Image<Bgr, Byte> CreateHitImage(Size size)
        {
            var imgGray = new Image<Bgr, Byte>(size);

            using (var fileReader = new StreamReader(new FileStream("Heineken.csv", FileMode.Open, FileAccess.Read)))
            {
                var line = string.Empty;
                while ((line = fileReader.ReadLine()) != null)
                {
                    var tokens = line.Split(';');
                    var column = int.Parse(tokens[1]);
                    var row = int.Parse(tokens[2]);
                    if (row >= 0 && row < imgGray.Height && column >= 0 && column < imgGray.Width)
                    {

                        var hitColor = imgGray[row, column].MCvScalar;
                        int hitDelta = 5;
                        var newHitColor = new MCvScalar(hitDelta + hitColor.V0, hitDelta + hitColor.V1, hitDelta + hitColor.V2);
                        CvInvoke.Circle(imgGray, new Point(column, row), 25, newHitColor, -1);
                    }
                }
            }

            return imgGray;
        }
    }
}
