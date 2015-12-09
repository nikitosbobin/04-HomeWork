using System;
using System.Collections.Generic;
using System.Drawing;
namespace TagCloudGenerator.Classes
{
    class ArchimedSpiralFunctionCloud : PolarFunctionCloud
    {
        public override Point GetBlockCoords()
        {
            var nod = GetGreatestCommonDivisor(Size.Height, Size.Width);
            var x = (int)((double)Size.Width / nod * CurrentAngle * Math.Cos(CurrentAngle));
            var y = (int)((double)Size.Height / nod * CurrentAngle * Math.Sin(CurrentAngle));
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

        public ArchimedSpiralFunctionCloud(Func<IEnumerable<WordBlock>> 
            getConvertedWords) : base(getConvertedWords)
        {
        }
    }
}