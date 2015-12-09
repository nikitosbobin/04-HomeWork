﻿using System.Collections.Generic;
using TagCloudGenerator.Classes;

namespace TagCloudGenerator.Interfaces
{
    interface ITextHandler
    {
        HashSet<string> BoringWords { get; set; }
        IEnumerable<WordBlock> GetWordBlockArray();
    }
}
