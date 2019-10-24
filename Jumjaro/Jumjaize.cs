using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jumjaize
{

    public class Jumjaize
    {
        private readonly Hangul _hangul = new Hangul();
        private CharacterMode _characterMode = CharacterMode.None;

        private readonly Dictionary<string, string> _acronyms = new Dictionary<string, string>()
        {
            {"그래서", "⠁⠎"},
            {"그러나", "⠁⠉"},
            {"그러면", "⠁⠒"},
            {"그러므로", "⠁⠢"},
            {"그런데", "⠁⠝"},
            {"그리고", "⠁⠥"},
            {"그리하여", "⠁⠱"},
        };

        // 문자 모드를 변경하고, 필요한 경우 모드 관련 문자를 추가한다
        private void ChangeMode(CharacterMode mode, StringBuilder sb)
        {
            if (_characterMode == mode)
            {
                return;
            }

            var prevMode = _characterMode;
            _characterMode = mode;

            if (_characterMode == CharacterMode.Number)
            {
                sb.Append('⠼');
            }
        }

        private string ConvertAsChar(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var ch in str)
            {
                if (_hangul.IsHangulCharacter(ch))
                {
                    ChangeMode(CharacterMode.Hangul, sb);
                    sb.Append(new HangulBraille(ch).ToString());
                }
                else if (char.IsNumber(ch))
                {
                    ChangeMode(CharacterMode.Number, sb);
                    sb.Append(new NumberArithmeticBraille(ch).ToString());
                }
                else
                {
                    sb.Append(ch);
                }
            }

            return sb.ToString();
        }

        private string ConvertAsWord(string str)
        {
            StringBuilder sb = new StringBuilder();
            // TODO: 최적화 여지가 있음
            foreach (var acronym in _acronyms)
            {
                if (str.StartsWith(acronym.Key))
                {
                    sb.Append(acronym.Value);
                    str = str.Substring(acronym.Key.Length);
                    break;
                }
            }

            if (!string.IsNullOrEmpty(str))
            {
                sb.Append(ConvertAsChar(str));
            }

            return sb.ToString();
        }

        public string ToJumja(string str)
        {
            // NOTE: 약어에 대한 규칙은 Jumja 클래스가 담당하고, 글자에 대한 규칙은 위임한다.

            var words = new List<string>();

            foreach(var word in str.Split(new char[] {' '}))
            {
                if(word.Contains('\n'))
                {
                    var lines = word.Split('\n').Select(ConvertAsWord).ToList();
                    words.Add(string.Join("\n", lines));
                    continue;
                }
                words.Add(ConvertAsWord(word));
            }
            return string.Join(" ", words);
        }

        /// <summary>
        /// Convert the Hangul string to the Hangul Braille string without special rules.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string ToJumjaWithoutRules(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var ch in str)
            {
                if (_hangul.IsHangulCharacter(ch))
                {
                    ChangeMode(CharacterMode.Hangul, sb);
                    sb.Append(new HangulBraille(ch).ToStringWithoutRules());
                }
                else if (char.IsNumber(ch))
                {
                    ChangeMode(CharacterMode.Number, sb);
                    sb.Append(new NumberArithmeticBraille(ch).ToStringWithoutRules());
                }
                else
                {
                    sb.Append(ch);
                }
            }
            return sb.ToString();
        }
    }
}
