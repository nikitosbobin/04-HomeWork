using TagCloudGenerator.Classes;

namespace TagCloudGenerator.Interfaces
{
    interface ICloudGenerator
    {
        void CreateCloud();
        WordBlock[] Words { get; set; }
    }
}
