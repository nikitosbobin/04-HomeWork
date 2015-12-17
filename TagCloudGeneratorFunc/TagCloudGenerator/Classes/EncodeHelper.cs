using System.Drawing;
using System.Drawing.Imaging;

namespace TagCloudGenerator.Classes
{
    static class EncodeHelper
    {
        public static void SaveImage(Bitmap image, string name, ImageFormat format)
        {
            image.Save(name + format, format);
        }
    }
}
