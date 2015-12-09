using System.Collections.Generic;
using System.Drawing;

namespace TagCloudGenerator.Interfaces
{
    interface ICloudImageGenerator
    {
        Bitmap Image { get; set; }
        List<SolidBrush> WordsBrushes { get; set; }
        void CreateImage();
    }
}
