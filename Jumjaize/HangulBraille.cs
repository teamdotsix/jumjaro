using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            "⠈",    // ㄱ
            "⠠⠈",   // ㄲ
            "⠉",    // ㄴ
            "⠊",    // ㄷ
            "⠠⠊",   // ㄸ
            "⠐",    // ㄹ
            "⠑",    // ㅁ
            "⠘",    // ㅂ
            "⠠⠘",   // ㅃ
            "⠠",    // ㅅ
            "⠠⠠",   // ㅆ
            "⠛",    // ㅇ
            "⠨",    // ㅈ
            "⠠⠨",   // ㅉ
            "⠰",    // ㅊ
            "⠋",    // ㅋ
            "⠓",    // ㅌ
            "⠙",    // ㅍ
            "⠚",    // ㅎ
        };

        private static readonly string[] Nucleuses =
        {
            /*
             * 모음 뒤에 '예'가 오거나 'ㅑ, ㅘ, ㅜ, ㅞ' 다음에 '애'가 이어 나오는 경우 그 사이에 붙임표(3-6)를 적는다. 단, 행이 바뀌는 경우에는 적지 않아도 된다.
             */
            "⠣",   // ㅏ
            "⠗",   // ㅐ
            "⠜",   // ㅑ
            "⠜⠗",  // ㅒ
            "⠎",   // ㅓ
            "⠝",   // ㅔ
            "⠱",   // ㅕ
            "⠌",   // ㅖ
            "⠥",   // ㅗ
            "⠧",   // ㅘ
            "⠧⠗",  // ㅙ
            "⠽",   // ㅚ
            "⠬",   // ㅛ
            "⠍",   // ㅜ
            "⠏",   // ㅝ
            "⠏⠗",  // ㅞ
            "⠍⠗",  // ㅟ
            "⠩",   // ㅠ
            "⠪",   // ㅡ
            "⠺",   // ㅢ
            "⠕",   // ㅣ
        };

        private static readonly string[] Codas =
        {
            // 받침 'ㄲ'은 6점 점자 (1)6점 점자 (1)(1, 1), 받침 'ㅆ'은 6점 점자 (3-4)(3-4)로 적고, 나머지 겹받침은 해당하는 낱자를 순서대로 적는다.
            null,
            "⠁",    // ㄱ
            "⠁⠁",   // ㄲ
            "⠁⠄",   // ㄳ
            "⠒",    // ㄴ
            "⠒⠅",   // ㄵ
            "⠒⠴",   // ㄶ
            "⠔",    // ㄷ
            "⠂",    // ㄹ
            "⠂⠁",   // ㄺ
            "⠂⠢",   // ㄻ
            "⠂⠃",   // ㄼ
            "⠂⠄",   // ㄽ
            "⠂⠦",   // ㄾ
            "⠂⠲",   // ㄿ
            "⠂⠴",   // ㅀ
            "⠢",    // ㅁ
            "⠃",    // ㅂ
            "⠃⠄",   // ㅄ
            "⠄",    // ㅅ
            "⠌",    // ㅆ
            "⠶",    // ㅇ
            "⠅",    // ㅈ
            "⠆",    // ㅊ
            "⠖",    // ㅋ
            "⠦",    // ㅌ
            "⠲",    // ㅍ
            "⠴",    // ㅎ
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
            var sb = new StringBuilder();
            if (_onset != default(char))
            {
                // 점자에서는 음가 없는 첫소리 ‘ㅇ’을 표기하지 않고, 이것을 약자가 아니라 정자로 삼는다.
                if (_onset != 'ㅇ')
                {
                    sb.Append(Onsets[hangul.FindIndexOfOnset(_onset)]);
                }
            }
            if (_nucleus != default(char))
            {
                sb.Append(Nucleuses[hangul.FindIndexOfNucleus(_nucleus)]);
            }
            if (_coda != default(char))
            {
                sb.Append(Codas[hangul.FindIndexOfCoda(_coda)]);
            }

            return sb.ToString();
        }

        public string ToStringWithoutRules()
        {
            var hangul = new Hangul();
            var sb = new StringBuilder();
            if (_onset != default(char))
            {
                sb.Append(Onsets[hangul.FindIndexOfOnset(_onset)]);
            }
            if (_nucleus != default(char))
            {
                sb.Append(Nucleuses[hangul.FindIndexOfNucleus(_nucleus)]);
            }
            if (_coda != default(char))
            {
                sb.Append(Codas[hangul.FindIndexOfCoda(_coda)]);
            }

            return sb.ToString();
        }
    }
}
