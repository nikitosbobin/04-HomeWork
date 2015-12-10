using System;

namespace TagCloudGenerator.Classes
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = new CommandsParser(args);
            var decoder = new TxtDecoder(parser);
            var handler = new SimpleTextHandler(decoder.GetDecodedText(), parser);
            var cloud = new ArchimedSpiralFunctionCloud(() => handler.GetWordBlockArray(), parser);
            cloud.CreateCloud();
            var cloudDrawer = new ImageGenerator(cloud.Words, parser);
            cloudDrawer.CreateImage();
            var encoder = new PngEncoder(cloudDrawer.Image);
            encoder.SaveImage("out");
            Console.WriteLine("Я всё");
            Console.ReadKey();
        }
    }
}
