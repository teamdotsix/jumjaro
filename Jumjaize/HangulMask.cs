using System;
using System.Collections.Generic;
using System.Text;

namespace Jumjaize
{
    public class HangulMask
    {
        private readonly char _onset;
        private readonly char _nucleus;
        private readonly char _coda;

        public bool HasOnset => _onset != default(char);
        public bool HasNucleus=> _onset != default(char);
        public bool HasCoda => _onset != default(char);
        private static readonly Dictionary<char, char[]> _doubleCodaMap = new Dictionary<char, char[]>()
        {
            {'ㄲ', new[] {'ㄱ', 'ㄱ'}},
            {'ㄳ', new[] {'ㄱ', 'ㅅ'}},
            {'ㄵ', new[] {'ㄴ', 'ㅈ'}},
            {'ㄶ', new[] {'ㄴ', 'ㅎ'}},
            {'ㄺ', new[] {'ㄹ', 'ㄱ'}},
            {'ㄻ', new[] {'ㄹ', 'ㅁ'}},
            {'ㄼ', new[] {'ㄹ', 'ㅂ'}},
            {'ㄽ', new[] {'ㄹ', 'ㅅ'}},
            {'ㄾ', new[] {'ㄹ', 'ㅌ'}},
            {'ㄿ', new[] {'ㄹ', 'ㅍ'}},
            {'ㅀ', new[] {'ㄹ', 'ㅎ'}},
            {'ㅄ', new[] {'ㅂ', 'ㅅ'}},
        };

        public HangulMask(char onset = default, char nucleus = default, char coda = default)
        {
            _onset = onset;
            _nucleus = nucleus;
            _coda = coda;
        }

        public static bool operator &(HangulMask mask, char hangul)
        {
            return mask.IsMatch(hangul);
        }

        public bool IsMatch(char hangulLetter)
        {
            var syllables = new Hangul().Syllabification(hangulLetter);
            var onset = syllables[0];
            var nucleus = syllables[1];
            var coda = syllables[2];

            bool matched = true;
            if(_onset != default(char))
            {
                matched &= _onset == onset;
            }
            if(_nucleus != default(char))
            {
                matched &= _nucleus == nucleus;
            }
            if(_coda != default(char))
            {
                // 제15항: 글자 속에 모음으로 시작하는 약자가 포함되어 있을 때에는 해당 약자를 이용하여 적는다.

                // NOTE: 그러므로 쌍모음을 사용중일 경우에는 모음을 분리하여 약자를 체크한다.
                var codas = DissembleCoda(coda);
                matched &= _coda == codas[0];
            }
            return matched;
        }

        public bool IsMatch(char onset, char nucleus, char coda)
        {
            bool matched = true;
            if(_onset != default(char))
            {
                matched &= _onset == onset;
            }
            if(_nucleus != default(char))
            {
                matched &= _nucleus == nucleus;
            }
            if(_coda != default(char))
            {
                // 제15항: 글자 속에 모음으로 시작하는 약자가 포함되어 있을 때에는 해당 약자를 이용하여 적는다.

                // NOTE: 그러므로 쌍모음을 사용중일 경우에는 모음을 분리하여 약자를 체크한다.
                var codas = DissembleCoda(coda);
                matched &= _coda == codas[0];
            }
            return matched;
        }

        private static char[] DissembleCoda(char coda)
        {
            if (_doubleCodaMap.TryGetValue(coda, out var codas))
            {
                return codas;
            }
            return new [] {coda};

        }

    }
}
