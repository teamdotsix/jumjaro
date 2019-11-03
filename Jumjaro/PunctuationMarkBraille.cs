using System;
using System.Collections.Generic;
using System.Text;

namespace Jumjaro
{
    class PunctuationMarkBraille
    {
        private static readonly Dictionary<char, string> _brailleMap = new Dictionary<char, string>()
        {
            { '.', "⠲" },
            { '?', "⠦" },
            { '!', "⠖" },
            { ',', "⠐" }, // [다만] 수의 자릿점을 표시하는 쉼표는 (2)⠂으로 적는다
            { '·', "⠐⠆" },
            { ':', "⠐⠂" },
            { ';', "⠰⠆" },  // 제50항 쌍반점( ; )은 (4-5,2-3)으로 적되, 앞말은 붙여 쓰고 뒷말은 띄어 쓴다.
            { '/', "⠸⠌" },
            { '“', "⠦" },
            { '”', "⠴" },
            { '‘', "⠠⠦" },
            { '’', "⠴⠄" },
            { '(', "⠦⠄" },
            { ')', "⠠⠴" },
            { '{', "⠦⠂" },
            { '}', "⠐⠴" },
            { '〔', "⠦⠆" },
            { '〕', "⠰⠴" },
            { '[', "⠦⠆" },
            { ']', "⠰⠴" },
        };

        private readonly char _letter;

        public PunctuationMarkBraille(char letter)
        {
            _letter = letter;
        }

        public static bool IsPunctuationMark(char letter)
        {
            return _brailleMap.ContainsKey(letter);
        }

        public override string ToString()
        {
            try
            {
                return _brailleMap[_letter];
            }
            catch (IndexOutOfRangeException)
            {
                return _letter.ToString();
            }
        }
    }
}
