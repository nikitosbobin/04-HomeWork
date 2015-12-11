using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagCloudGenerator.Classes
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = new CommandsParser(args);
            var decoder = new TxtDecoder(parser.GetResource<string>("path"));
            var handler = new SimpleTextHandler(decoder.GetDecodedText(), parser.GetResource<HashSet<string>>("boring"));
            var cloud = new ArchimedSpiralFunctionCloud(() => handler.GetWordBlockArray(), parser.GetResource<int>("scale"),
                parser.GetResource<Size>("size"), parser.GetResource<string>("font"), parser.GetResource<bool>("moreDensity"));
            cloud.CreateCloud();
            var cloudDrawer = new ImageGenerator(cloud.Words, parser.GetResource<Size>("size"), parser.GetResource<List<SolidBrush>>("colors"));
            cloudDrawer.CreateImage();
            var encoder = new PngEncoder(cloudDrawer.Image);
            encoder.SaveImage("out");
            Console.WriteLine("Я всё");
            Console.ReadKey();
        }
    }
}
