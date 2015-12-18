using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace TagCloudGenerator.Classes
{
    class Program
    {
        static void Main(string[] args)
        {
            var decodedText = DecodeHelper.GetDecodedTextFromTxt(CommandsHelper.GetResource<string>(args));
            var temp = DecodeHelper.ConvertTextToWordBlocks(decodedText,
                CommandsHelper.GetResource<HashSet<string>>(args));
            var cloud = new ArchimedSpiralFunctionCloud(temp, CommandsHelper.GetResource<int>(args),
                CommandsHelper.GetResource<Size>(args), CommandsHelper.GetResource<Font>(args), CommandsHelper.GetResource<bool>(args));
            cloud.CreateCloud();
            var resultImage = ImageHelper.GetCloudImage(cloud.Words, CommandsHelper.GetResource<Size>(args),
                CommandsHelper.GetResource<List<SolidBrush>>(args));
            ImageHelper.SaveImage(resultImage, "out", ImageFormat.Png);
            Console.WriteLine("Я всё");
            Console.ReadKey();
        }
    }
}
