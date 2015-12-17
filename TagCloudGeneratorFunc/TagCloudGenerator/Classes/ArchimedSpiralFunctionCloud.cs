using System;
using System.Drawing;
namespace TagCloudGenerator.Classes
{
    class ArchimedSpiralFunctionCloud : PolarFunctionCloud
    {
        public override Point GetBlockCoords()
        {
            var gcd = GetGreatestCommonDivisor(Size.Height, Size.Width);
            var x = (int)((double)Size.Width / gcd * CurrentAngle * Math.Cos(CurrentAngle));
            var y = (int)((double)Size.Height / gcd * CurrentAngle * Math.Sin(CurrentAngle));
            return new Point(x, y);
        }

        private static int GetGreatestCommonDivisor(int firstItem, int secondItem)
        {
            while (firstItem != secondItem)
            {
                if (firstItem > secondItem)
                    firstItem -= secondItem;
                else
                    secondItem -= firstItem;
            }
            return firstItem;
        }

        public ArchimedSpiralFunctionCloud(WordBlock[] words, int wordsScale = 0, Size imageSize = default(Size), 
            Font font = null, bool moreDensity = false) : base(words, wordsScale, 
                imageSize, font, moreDensity)
        {
        }
    }
}