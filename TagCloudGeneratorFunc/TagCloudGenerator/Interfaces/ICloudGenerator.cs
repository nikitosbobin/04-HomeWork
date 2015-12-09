using System.Drawing;
using TagCloudGenerator.Classes;

namespace TagCloudGenerator.Interfaces
{
    interface ICloudGenerator
    {
        void CreateCloud();
        Size Size { get; set; }
        WordBlock[] Words { get; set; }
        float WordScale { get; set; }
        string FontFamily { get; set; }
        bool MoreDensity { get; set; }
    }
}
