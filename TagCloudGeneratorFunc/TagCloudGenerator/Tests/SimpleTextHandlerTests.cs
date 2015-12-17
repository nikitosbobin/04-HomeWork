﻿using System.Collections.Generic;
using NUnit.Framework;
using System.Linq;
using TagCloudGenerator.Classes;
using TagCloudGenerator.Interfaces;

namespace TagCloudGenerator.Tests
{
    [TestFixture]
    class SimpleTextHandler_Should
    {
        public static void DoTest(string[] words, IWordBlock[] expected, string[] boring = null)
        {
            var handler = new SimpleTextHandler(words, new HashSet<string>(boring ?? new string[0]));
            IWordBlock[] actual = handler.GetWordBlockArray().ToArray();
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
