using System;

namespace TagCloudGenerator.Classes
{
    class Program
    {
        static void Main(string[] args)
        {
            var decoder = new TxtDecoder(args[0]);
            var handler = new SimpleTextHandler(decoder.GetDecodedText());
            var cloud = new ArchimedSpiralFunctionCloud(() => handler.GetWordBlockArray());
            cloud.CreateCloud();
            var cloudDrawer = new ImageGenerator(cloud.Words, cloud.Size);
            var commandsParser = new CommandsParser(cloud, cloudDrawer, args);
            cloudDrawer.CreateImage();
            if (commandsParser.ExecuteAllCommands())
            {
                var encoder = new PngEncoder(cloudDrawer.Image);
                encoder.SaveImage("out");
                Console.WriteLine("Я всё");
            }
            Console.ReadKey();
        }
    }
}
