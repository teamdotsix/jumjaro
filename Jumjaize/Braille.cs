using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jumjaize
{
    public class Braille
    {
        private readonly int _index;

        public Braille(int index)
        {
            _index = index;
        }

        public Braille(string indexNotation)
        {
            _index = ConvertIndexNotationToInt(indexNotation);
        }

        public static int ConvertIndexNotationToInt(string indexNotation)
        {
            indexNotation = indexNotation.Replace(" ", string.Empty);
            var indices = indexNotation.Trim().Split(new char[] {'-'});
            int result = 0;
            foreach (var index in indices)
            {
                result += (int)Math.Pow(2, (int.Parse(index) - 1));
            }

            return result;
        }

        public override string ToString()
        {
            return char.ConvertFromUtf32(0x2800 + _index);
        }

        public static List<Braille> CreateBraillesFromMultipleIndexNotation(string multipleIndexNotation)
        {
            var notations = multipleIndexNotation.Split(new char[] { ',' });
            if (notations.Length == 0)
            {
                return null;
            }

            return notations.Select(notation => new Braille(notation)).ToList();
        }
    }
}
