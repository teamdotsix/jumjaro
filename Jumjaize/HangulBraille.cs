using System;
using System.Collections.Generic;
using System.Linq;

namespace Jumjaize
{
    public class HangulBraille
    {
        // NOTE: 변환의 편의를 위해 한글 초성, 중성, 종성의 순서와 맞춘다.
        // SEE: Hangul.cs

        private static readonly string[] Onsets =
        {
            /*
             * ㄱ(4), ㄴ(1-4), ㄷ(2-4), ㄹ(5), ㅁ(1-5), ㅂ(4-5), ㅅ(8), ㅇ(1-2-4-5), ㅈ(4-6), ㅊ(5-6), ㅋ(1-2-4), ㅌ(1-2-5), ㅍ(1-4-5), ㅎ(2-4-5)
             * 된소리는 초성 앞에 된소리표(6)를 적는다.
             */
            "4",       // ㄱ
            "6,4",     // ㄲ
            "1-4",     // ㄴ
            "2-4",     // ㄷ
            "6,2-4",   // ㄸ
            "5",       // ㄹ
            "1-5",     // ㅁ
            "4-5",     // ㅂ
            "6,4-5",   // ㅃ
            "6",       // ㅅ
            "6,6",     // ㅆ
            "1-2-4-5", // ㅇ
            "4-6",     // ㅈ
            "6,4-6",   // ㅉ
            "5-6",     // ㅊ
            "1-2-4",   // ㅋ
            "1-2-5",   // ㅌ
            "1-4-5",   // ㅍ
            "2-4-5",   // ㅎ
        };

        private static readonly string[] Nucleuses =
        {
            /*
             * 모음 뒤에 '예'가 오거나 'ㅑ, ㅘ, ㅜ, ㅞ' 다음에 '애'가 이어 나오는 경우 그 사이에 붙임표(3-6)를 적는다. 단, 행이 바뀌는 경우에는 적지 않아도 된다.
             */
            "1-2-6",            // ㅏ
            "1-2-3-5",          // ㅐ
            "3-4-5",            // ㅑ
            "3-4-5,1-2-3-5",    // ㅒ
            "2-3-4",            // ㅓ
            "1-3-4-5",          // ㅔ
            "1-5-6",            // ㅕ
            "3-4",              // ㅖ
            "1-3-6",            // ㅗ
            "1-2-3-6",          // ㅘ
            "1-2-3-6,1-2-3-5",  // ㅙ
            "1-3-4-5-6",        // ㅚ
            "3-4-6",            // ㅛ
            "1-3-4",            // ㅜ
            "1-2-3-4",          // ㅝ
            "1-2-3-4,1-2-3-5",  // ㅞ
            "1-3-4,1-2-3-5",    // ㅟ
            "1-4-6",            // ㅠ
            "2-4-6",            // ㅡ
            "2-4-5-6",          // ㅢ
            "1-3-5",            // ㅣ
        };

        private static readonly string[] Codas =
        {
            // 받침 'ㄲ'은 6점 점자 (1)6점 점자 (1)(1, 1), 받침 'ㅆ'은 6점 점자 (3-4)(3-4)로 적고, 나머지 겹받침은 해당하는 낱자를 순서대로 적는다.
            null,
            "1",         // ㄱ
            "1,1",       // ㄲ
            "1,3",       // ㄳ
            "2-5",       // ㄴ
            "2-5,1-3",   // ㄵ
            "2-5,3-5-6", // ㄶ
            "3-5",       // ㄷ
            "2",         // ㄹ
            "2,1",       // ㄺ
            "2,2-6",     // ㄻ
            "2,1-2",     // ㄼ
            "2,3",       // ㄽ
            "2,2-3-6",   // ㄾ
            "2,2-5-6",   // ㄿ
            "2,3-5-6",   // ㅀ
            "2-6",       // ㅁ
            "1-2",       // ㅂ
            "1-2,3",     // ㅄ
            "3",         // ㅅ
            "3-4",       // ㅆ
            "2-3-5-6",   // ㅇ
            "1-3",       // ㅈ
            "2-3",       // ㅊ
            "2-3-5",     // ㅋ
            "2-3-6",     // ㅌ
            "2-5-6",     // ㅍ
            "3-5-6",     // ㅎ
        };

        private readonly char _onset;
        private readonly char _nucleus;
        private readonly char _coda;

        public HangulBraille(char hangulCharacter)
        {
            var hangul = new Hangul();
            var syllables = hangul.Syllabification(hangulCharacter);
            _onset = syllables[0];
            _nucleus = syllables[1];
            _coda = syllables[2];
        }

        public HangulBraille(char onset = default, char nucleus = default, char coda = default)
        {
            _onset = onset;
            _nucleus = nucleus;
            _coda = coda;
        }

        public override string ToString()
        {
            var hangul = new Hangul();
            var brailleNotations = new List<string>();
            if (_onset != default(char))
            {
                // 점자에서는 음가 없는 첫소리 ‘ㅇ’을 표기하지 않고, 이것을 약자가 아니라 정자로 삼는다.
                if (_onset != 'ㅇ')
                {
                    brailleNotations.Add(Onsets[hangul.FindIndexOfOnset(_onset)]);
                }
            }
            if (_nucleus != default(char))
            {
                brailleNotations.Add(Nucleuses[hangul.FindIndexOfNucleus(_nucleus)]);
            }
            if (_coda != default(char))
            {
                brailleNotations.Add(Codas[hangul.FindIndexOfCoda(_coda)]);
            }

            var mergedNotation = string.Join(",", brailleNotations);

            return string.Join(string.Empty,
                Braille.CreateBraillesFromMultipleIndexNotation(mergedNotation).Select(x => x.ToString()));
        }

        public string ToStringWithoutRules()
        {
            var hangul = new Hangul();
            var brailleNotations = new List<string>();
            if (_onset != default(char))
            {
                brailleNotations.Add(Onsets[hangul.FindIndexOfOnset(_onset)]);
            }
            if (_nucleus != default(char))
            {
                brailleNotations.Add(Nucleuses[hangul.FindIndexOfNucleus(_nucleus)]);
            }
            if (_coda != default(char))
            {
                brailleNotations.Add(Codas[hangul.FindIndexOfCoda(_coda)]);
            }

            var mergedNotation = string.Join(",", brailleNotations);

            return string.Join(string.Empty,
                Braille.CreateBraillesFromMultipleIndexNotation(mergedNotation).Select(x => x.ToString()));
        }
    }
}
