﻿using System.Collections.Generic;
using System.Linq;
using TagCloudGenerator.Interfaces;

namespace TagCloudGenerator.Classes
{
    class SimpleTextHandler : ITextHandler
    {
        public SimpleTextHandler(string[] decodedText, params string[] boringWords)
        {
            _decodedLines = decodedText;
            BoringWords = new HashSet<string>(boringWords ?? new string[0]);
        }

        public SimpleTextHandler(string[] decodedText, CommandsParser parser)
        {
            _decodedLines = decodedText;
            var tempBoring = parser.GetResource<HashSet<string>>("boring");
            if (tempBoring != null)
                BoringWords = tempBoring;
            else 
                BoringWords = new HashSet<string>();
        }

        private Dictionary<string, WordBlock> _innerWords;
        private string[] _decodedLines;
        public HashSet<string> BoringWords { get; set; }
        private WordBlock[] _words;

        public IEnumerable<WordBlock> GetWordBlockArray()
        {
            CreateInnerWords();
            return _words;
        }

        private bool IsItRightWord(string word)
        {
            return word.Length > 5 && !BoringWords.Contains(word);
        }

        private void CreateInnerWords()
        {
            _innerWords = new Dictionary<string, WordBlock>();
            foreach (var word in _decodedLines)
            {
                if (IsItRightWord(word))
                {
                    if (_innerWords.ContainsKey(word))
                        _innerWords[word].Frequency++;
                    else
                        _innerWords.Add(word, new WordBlock(word));
                }
                else
                    if (!BoringWords.Contains(word))
                        BoringWords.Add(word);
            }
            _words = _innerWords.Select(pair => pair.Value).ToArray();
        }
    }
}