using System;
using System.Collections.Generic;
using System.Text;

namespace Jumjaize
{
    public class Hangul
    {
        private readonly char[] _onsets =
        {
            'ㄱ','ㄲ','ㄴ','ㄷ','ㄸ','ㄹ','ㅁ','ㅂ','ㅃ','ㅅ','ㅆ','ㅇ','ㅈ',
            'ㅉ','ㅊ','ㅋ','ㅌ','ㅍ','ㅎ'
        };

        private readonly char[] _nucleuses =
        {
            'ㅏ','ㅐ','ㅑ','ㅒ','ㅓ','ㅔ','ㅕ','ㅖ','ㅗ','ㅘ','ㅙ','ㅚ','ㅛ',
            'ㅜ','ㅝ','ㅞ','ㅟ','ㅠ','ㅡ','ㅢ','ㅣ'
        };

        private readonly char[] _codas =
        {
            '\0', 'ㄱ','ㄲ','ㄳ','ㄴ','ㄵ','ㄶ','ㄷ','ㄹ','ㄺ','ㄻ','ㄼ','ㄽ','ㄾ',
            'ㄿ','ㅀ','ㅁ','ㅂ','ㅄ','ㅅ','ㅆ','ㅇ','ㅈ','ㅊ','ㅋ','ㅌ','ㅍ','ㅎ'
        };

        public bool IsHangulCharacter(char ch)
        {
            return (('가' <= ch) && (ch <= '힣'));
        }

        public char[] Syllabification(char letter, bool onset = true, bool nucleus = true, bool coda = true)
        {
            if (!IsHangulCharacter(letter))
            {
                return null;
            }

            var syllables = new char[3];

            var offset = letter - '가';
            if (onset)
            {
                syllables[0] = _onsets[offset / (_nucleuses.Length * _codas.Length)];
            }
            if (nucleus)
            {
                syllables[1] = _nucleuses[(offset / _codas.Length) % _nucleuses.Length];
            }
            if (coda)
            {
                syllables[2] = _codas[offset % _codas.Length];
            }
            return syllables;
        }

        public int[] SyllabificationIndex(char letter, bool onset = true, bool nucleus = true, bool coda = true)
        {
            if (!IsHangulCharacter(letter))
            {
                return null;
            }

            var syllables = new int[] {-1, -1, -1};

            var offset = letter - '가';
            if (onset)
            {
                syllables[0] = offset / (_nucleuses.Length * _codas.Length);
            }
            if (nucleus)
            {
                syllables[1] = (offset / _codas.Length) % _nucleuses.Length;
            }
            if (coda)
            {
                syllables[2] = offset % _codas.Length;
            }
            return syllables;
        }
    }
}
