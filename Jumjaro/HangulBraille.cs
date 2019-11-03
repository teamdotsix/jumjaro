using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jumjaro
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

        private static readonly Dictionary<char, char> NoAbbrDoubleOnsetMap = new Dictionary<char, char>()
        {
            {'ㄸ', 'ㄷ'},
            {'ㅃ', 'ㅂ'},
            {'ㅉ', 'ㅈ'},
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

        private readonly char _letter;
        private readonly char _onset;
        private readonly char _nucleus;
        private readonly char _coda;

        private static readonly Dictionary<HangulAbbrMask, string> Abbreviations = new Dictionary<HangulAbbrMask, string>()
        {
            // 제12항. 다음 글자가 포함된 글자들은 아래 표에 제시한 약자 표기를 이용하여 적는 것을 표준으로 삼는다.
            {new HangulAbbrMask('ㄱ', 'ㅏ'), "⠫"},
            {new HangulAbbrMask('ㄴ', 'ㅏ'), "⠉"},
            {new HangulAbbrMask('ㄷ', 'ㅏ'), "⠊"},
            {new HangulAbbrMask('ㅁ', 'ㅏ'), "⠑"},
            {new HangulAbbrMask('ㅂ', 'ㅏ'), "⠘"},
            {new HangulAbbrMask('ㅅ', 'ㅏ'), "⠇"},
            {new HangulAbbrMask('ㅈ', 'ㅏ'), "⠨"},
            {new HangulAbbrMask('ㅋ', 'ㅏ'), "⠋"},
            {new HangulAbbrMask('ㅌ', 'ㅏ'), "⠓"},
            {new HangulAbbrMask('ㅍ', 'ㅏ'), "⠙"},
            {new HangulAbbrMask('ㅎ', 'ㅏ'), "⠚"},
            {new HangulAbbrMask('ㄱ', 'ㅓ', 'ㅅ'), "⠸⠎"},

            // 제14항. ‘까, 싸, 껏’은 각각 ‘가, 사, 것’의 약자 표기에 된소리 표를 덧붙여 적는다.
            {new HangulAbbrMask('ㄲ', 'ㅏ'), "⠠⠫"},
            {new HangulAbbrMask('ㅆ', 'ㅏ'), "⠠⠇"},
            {new HangulAbbrMask('ㄲ', 'ㅓ', 'ㅅ'), "⠠⠸⠎"},

            // 제16항:‘성, 썽, 정, 쩡, 청’은 ‘ㅅ, ㅆ, ㅈ, ㅉ, ㅊ’ 다음에 ‘ㅕㅇ’의 약자(1-2-4-5-6)를 적어 나타낸다
            {new HangulAbbrMask('ㅅ', 'ㅓ', 'ㅇ'), "⠠⠻"},
            {new HangulAbbrMask('ㅆ', 'ㅓ', 'ㅇ'), "⠠⠠⠻"},
            {new HangulAbbrMask('ㅈ', 'ㅓ', 'ㅇ'), "⠨⠻"},
            {new HangulAbbrMask('ㅉ', 'ㅓ', 'ㅇ'), "⠠⠨⠻"},
            {new HangulAbbrMask('ㅊ', 'ㅓ', 'ㅇ'), "⠰⠻"},
        };

        private static readonly Dictionary<HangulAbbrMask, string> AbbreviationsWithoutOnset = new Dictionary<HangulAbbrMask, string>()
        {
            // 제15항. 글자 속에 모음으로 시작하는 약자가 포함되어 있을 때에는 해당 약자를 이용하여 적는다
            // 억 언 얼 연 열 영 옥 온 옹 운 울 은 을 인
            {new HangulAbbrMask(default, 'ㅓ', 'ㄱ'), "⠹" },
            {new HangulAbbrMask(default, 'ㅓ', 'ㄴ'), "⠾" },
            {new HangulAbbrMask(default, 'ㅓ', 'ㄹ'), "⠞" },
            {new HangulAbbrMask(default, 'ㅕ', 'ㄴ'), "⠡" },
            {new HangulAbbrMask(default, 'ㅕ', 'ㄹ'), "⠳" },
            {new HangulAbbrMask(default, 'ㅕ', 'ㅇ'), "⠻" },
            {new HangulAbbrMask(default, 'ㅗ', 'ㄱ'), "⠭" },
            {new HangulAbbrMask(default, 'ㅗ', 'ㄴ'), "⠷" },
            {new HangulAbbrMask(default, 'ㅗ', 'ㅇ'), "⠿" },
            {new HangulAbbrMask(default, 'ㅜ', 'ㄴ'), "⠛" },
            {new HangulAbbrMask(default, 'ㅜ', 'ㄹ'), "⠯" },
            {new HangulAbbrMask(default, 'ㅡ', 'ㄴ'), "⠵" },
            {new HangulAbbrMask(default, 'ㅡ', 'ㄹ'), "⠮" },
            {new HangulAbbrMask(default, 'ㅣ', 'ㄴ'), "⠟" },
        };

        public HangulBraille(char hangulCharacter)
        {
            var hangul = new Hangul();
            var syllables = hangul.Syllabification(hangulCharacter);
            _onset = syllables[0];
            _nucleus = syllables[1];
            _coda = syllables[2];
            _letter = hangulCharacter;
        }

        public HangulBraille(char onset = default, char nucleus = default, char coda = default)
        {
            _onset = onset;
            _nucleus = nucleus;
            _coda = coda;
            _letter = new Hangul().JoinSyllables(onset, nucleus, coda);
        }

        private static string ConvertNucleusAndCoda(char nucleus = default, char coda = default)
        {
            // 제12항에 제시된 약자 ‘가, 나, 다, 마, 바, 사, 자, 카, 타, 파, 하’에 받침 글자가 오더라도 해당 약자를 사용하여 적는다.

            HangulAbbrMask processedAbbr = null;
            var sb = new StringBuilder();

            // 최적화 여지가 있을듯
            foreach(var abbr in AbbreviationsWithoutOnset)
            {
                if (abbr.Key.IsMatch(default(char), nucleus, coda))
                {
                    sb.Append(abbr.Value);
                    processedAbbr = abbr.Key;
                    break;
                }
            }

            var syllables = new[] { default(char), nucleus, coda };
            if (processedAbbr != null)
            {
                // 약어가 처리되었을 경우, 약어를 처리하고 남은 음절을 구한다
                syllables = processedAbbr.SubtractIfMatched(default(char), nucleus, coda);
            }

            // 남아있는 음절을 마저 처리함

            if (syllables[1] != default(char)) // has Nucleus
            {
                sb.Append(Nucleuses[new Hangul().FindIndexOfNucleus(syllables[1])]);
            }

            if(syllables[2] != default(char)) // has Coda
            {
                sb.Append(ConvertCoda(syllables[2]));
            }

            return sb.ToString();
        }

        private static string ConvertCoda(char coda)
        {
            if (coda != default(char))
            {
                return Codas[new Hangul().FindIndexOfCoda(coda)];
            }

            return string.Empty;
        }

        private bool hasSsangsiotCoda()
        {
            var coda = new Hangul().Syllabification(_letter, false, false, true)[2];
            return coda == 'ㅆ';
        }

        public override string ToString()
        {
            var hangul = new Hangul();

            var sb = new StringBuilder();

            // 된소리 글자 중 약자 표기가 없는 ㄸ, ㅃ, ㅉ은 "된소리표 + 된소리 제거된 글자"로 취급하여 처리한다.
            // 예) 땅 -> 된소리표 + 당
            var onset = hangul.Syllabification(_letter, true, false, false)?[0] ?? default(char);
            if (NoAbbrDoubleOnsetMap.ContainsKey(onset))
            {
                var nucleusAndCoda = hangul.Syllabification(_letter, false, true, true);
                return '⠠' + new HangulBraille(NoAbbrDoubleOnsetMap[onset], nucleusAndCoda[1], nucleusAndCoda[2]).ToString();
            }

            // ‘껐’과 같이 받침 글자가 ‘ㅆ’일 경우에는 받침 ‘ㅅ’을 덧붙여 적는 것이 아니라 받침 ‘ㅆ’ 약자를 우선 적용하여 ‘꺼’와 ‘받침 ㅆ’으로 적는다.
            if (hasSsangsiotCoda())
            {
                //‘팠’은 항상 ‘ㅏ’를 생략하지 않는다.
                if (_letter == '팠')
                {
                    return new HangulBraille('파').ToStringWithoutRules() + '⠌';
                }

                return new HangulBraille(hangul.RemoveCoda(_letter)).ToString() + '⠌';
            }

            // 제12항에 제시된 약자 ‘가, 나, 다, 마, 바, 사, 자, 카, 타, 파, 하’에 받침 글자가 오더라도 해당 약자를 사용하여 적는다.

            HangulAbbrMask processedAbbr = null;
            foreach(var abbr in Abbreviations)
            {
                if (abbr.Key & _letter)
                {
                    sb.Append(abbr.Value);
                    processedAbbr = abbr.Key;
                    break;
                }
            }

            if(processedAbbr == null)
            {
                if (_onset != default(char))
                {
                    // 점자에서는 음가 없는 첫소리 ‘ㅇ’을 표기하지 않고, 이것을 약자가 아니라 정자로 삼는다.
                    if (_onset != 'ㅇ')
                    {
                        sb.Append(Onsets[hangul.FindIndexOfOnset(_onset)]);
                    }
                }
            }

            char[] syllables = new[] { _onset, _nucleus, _coda };
            if (processedAbbr != null)
            {
                syllables = processedAbbr.SubtractIfMatched(_letter);
            }

            if(syllables[1] != default(char)) // has Nucleus
            {
                sb.Append(ConvertNucleusAndCoda(syllables[1], syllables[2]));
            }
            else
            {
                sb.Append(ConvertCoda(syllables[2]));
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
