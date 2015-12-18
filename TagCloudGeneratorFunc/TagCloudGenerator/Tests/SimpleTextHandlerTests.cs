using System.Collections.Generic;
using NUnit.Framework;
using System.Linq;
using TagCloudGenerator.Classes;

namespace TagCloudGenerator.Tests
{
    [TestFixture]
    class SimpleTextHandler_Should
    {
        public static void DoTest(string[] words, WordBlock[] expected, string[] boring = null)
        {
            WordBlock[] actual = DecodeHelper.ConvertTextToWordBlocks(words,
                new HashSet<string>(boring ?? new string[0]));
            for (int i = 0; i < expected.Length; ++i)
            {
                Assert.AreEqual(expected[i].Source, actual[i].Source);
                Assert.AreEqual(expected[i].Frequency, actual[i].Frequency);
            }
        }

        [Test]
        public static void CreateWords()
        {
            string[] words = { "word", "a", "an", "no", "by", "smartphone", "notebook" };
            WordBlock[] expected = { new WordBlock("smartphone"), new WordBlock("notebook") };
            DoTest(words, expected);
        }

        [Test]
        public static void CreateWordsWithoutBoringWords()
        {
            string[] words = { "word", "a", "an", "no", "by", "smartphone", "notebook", "unittesting" };
            WordBlock[] expected = { new WordBlock("smartphone"), new WordBlock("notebook") };
            string[] boring = { "unittesting" };
            DoTest(words, expected, boring);
        }
    }
}
