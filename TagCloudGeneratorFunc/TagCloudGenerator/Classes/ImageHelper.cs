using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace TagCloudGenerator.Classes
{
    static class ImageHelper
    {
        public static void SaveImage(Bitmap image, string name, ImageFormat format)
        {
            image.Save(name + "." + format, format);
        }

        public static Bitmap GetCloudImage(WordBlock[] wordBlocks, Size imageSize, List<SolidBrush> wordsBrushes)
        {
            var image = new Bitmap(imageSize.Width, imageSize.Height);
            Graphics graphics;
            using (graphics = Graphics.FromImage(image))
            {

                var rnd = new Random(DateTime.Now.Millisecond);
                graphics.Clear(Color.CadetBlue);
                foreach (var word in wordBlocks)
                {
                    graphics.DrawString(word.Source, word.Font, wordsBrushes[rnd.Next(0, wordsBrushes.Count)],
                        (imageSize.Width / 2 + word.WordRectangle.X), (imageSize.Height / 2 - word.WordRectangle.Y));
                }
                return image;
            }
           }
    }
}
