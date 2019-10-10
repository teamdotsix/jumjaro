using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jumjaize
{
    /*
     * 점자의 번호:
     *
     *  (1) (4)
     *  (2) (5)
     *  (3) (6)
     *  (7) (8)
     *
     * 한글 점자는 6점 점자만 사용한다.
     *
     *  (1) (4)
     *  (2) (5)
     *  (3) (6)
     *
     * 점자 유니코드는 U+2800 부터 시작한다.
     * 점자 코드는 기계적으로 계산이 가능하게 배치되어있다.
     * 점자 번호를 역순으로 나열해서 2진수로 계산하면 해당 점자 코드가 나온다.
     *
     * 예) ⠓ (1-2-5)일 경우, 점자 유무로 표기하면 "1 1 0 0 1 0 0 0"가 되고, 이를 역순 이진법으로 취하면 "00010011(19, 0x13)"이 된다. 
     *     그러므로 0x2800 + 0x13 = 0x2813, 즉 U+2813이 해당 점자의 유니코드가 된다.
     */

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
