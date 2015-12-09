using System.Drawing;
using System.Drawing.Imaging;
using TagCloudGenerator.Interfaces;

namespace TagCloudGenerator.Classes
{
    class PngEncoder : IImageEncoder
    {
        public PngEncoder(Bitmap image)
        {
            _image = image;
        }

        private Bitmap _image;
        public void SaveImage(string name)
        {
            _image.Save(name + ".png", ImageFormat.Png); 
        }
    }
}
