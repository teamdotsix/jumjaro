using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace Jumjaize
{
    public class HangulSyllableBraille
    {
        private const int EMPTY_BRAILLE = -1;
        private List<Braille> _brailles = new List<Braille>();

        public HangulSyllableBraille(string multipleIndexNotation)
        {
            var notations = multipleIndexNotation.Split(new char[] { ',' }, 2);
            foreach (var notation in notations)
            {
                _brailles.Add(new Braille(notation));
            }
        }

        public override string ToString()
        {
            return string.Join(string.Empty, _brailles.Select(x => x.ToString()));
        }
    }
}
