using System.Drawing;
using TagCloudGenerator.Interfaces;

namespace TagCloudGenerator.Classes
{
    class WordBlock : IWordBlock
    {
        public WordBlock(string source, int frequency = 1)
        {
            Source = source.ToLower();
            Frequency = frequency;
        }

        public string Source { get; set; }
        public int Frequency { get; set; }
        private Rectangle _wordRectangle;
        public Rectangle WordRectangle
        {
            get
            {
                if (_wordRectangle.Width == 0 && _wordRectangle.Height == 0)
                    _wordRectangle = new Rectangle(0, 0, (int)(Font.Size * 0.7) * Source.Length, Font.Height);
                return _wordRectangle;
            }
            set
            {
                _wordRectangle = value;
            }
        }
        private Font _font;
        public Font Font
        {
            get
            {
                if (_font == null)
                    _font = new Font("Times New Roman", 12f);
                return _font;
            }
            set { _font = value; }
        }
    }
}
