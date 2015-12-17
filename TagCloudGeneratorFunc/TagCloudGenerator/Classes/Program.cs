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
            var parser = new CommandsParser(args);
            var decodedText = DecodeHelper.GetDecodedTextFromTxt(parser.GetResource<string>("path"));
            var handler = new SimpleTextHandler(decodedText, parser.GetResource<HashSet<string>>("boring"));
            var cloud = new ArchimedSpiralFunctionCloud(handler.GetWordBlockArray().ToArray(), parser.GetResource<int>("scale"),
                parser.GetResource<Size>("size"), parser.GetResource<string>("font"), parser.GetResource<bool>("moreDensity"));
            cloud.CreateCloud();
            var cloudDrawer = new ImageGenerator(cloud.Words, parser.GetResource<Size>("size"), parser.GetResource<List<SolidBrush>>("colors"));
            cloudDrawer.CreateImage();
            EncodeHelper.SaveImage(cloudDrawer.Image, "out", ImageFormat.Png);
            Console.WriteLine("Я всё");
            Console.ReadKey();
        }
    }
}
