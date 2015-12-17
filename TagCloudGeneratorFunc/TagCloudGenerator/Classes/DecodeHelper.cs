using System.IO;
using System.Linq;

namespace TagCloudGenerator.Classes
{
    static class DecodeHelper
    {
        public static string[] GetDecodedTextFromTxt(string path)
        {
            try
            {
                var tmpText = File.ReadAllLines(path);
                return tmpText.Select(line => line.ToLower()).ToArray();
            }
            catch
            {
                return new string[0];
            }
        }
    }
}
