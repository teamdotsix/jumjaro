using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace Jumjaize
{
    public class HangulBraille
    {
        private readonly List<Braille> _brailles;

        public HangulBraille(string multipleIndexNotation)
        {
            _brailles = Braille.CreateBraillesFromMultipleIndexNotation(multipleIndexNotation);
        }

        public override string ToString()
        {
            return string.Join(string.Empty, _brailles.Select(x => x.ToString()));
        }
    }
}
