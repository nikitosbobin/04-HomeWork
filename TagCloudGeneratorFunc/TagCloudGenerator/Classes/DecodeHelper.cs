using System.Collections.Generic;
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

        public static WordBlock[] ConvertTextToWordBlocks(string[] decodedText, HashSet<string> boringWords = null)
        {
            var innerWords = new Dictionary<string, WordBlock>();
            if (boringWords == null)
                boringWords = new HashSet<string>();
            foreach (var word in decodedText)
            {
                if (word.Length > 5 && !boringWords.Contains(word))
                {
                    if (innerWords.ContainsKey(word))
                        innerWords[word].Frequency++;
                    else
                        innerWords.Add(word, new WordBlock(word));
                }
                else
                    if (!boringWords.Contains(word))
                    boringWords.Add(word);
            }
            return innerWords.Select(pair => pair.Value).ToArray();
        }

    }
}
