using System;
using System.Collections.Generic;
using System.Text;

namespace Jumjaize
{

    public class Jumjaize
    {
        private readonly Hangul _hangul = new Hangul();

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

        private string ConvertAsChar(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var ch in str)
            {
                if (!_hangul.IsHangulCharacter(ch))
                {
                    sb.Append(ch);
                    continue;
                }
                sb.Append(new HangulBraille(ch).ToString());
            }

            return sb.ToString();
        }

        private string ConvertAsWord(string str)
        {
            StringBuilder sb = new StringBuilder();
            // TODO: 최적화 여지가 있음
            foreach (var acronym in _acronyms)
            {
                if(str.StartsWith(acronym.Key))
                {
                    sb.Append(acronym.Value);
                    str = str.Substring(acronym.Key.Length);
                    break;
                }
            }

            if(!string.IsNullOrEmpty(str))
            {
                sb.Append(ConvertAsChar(str));
            }

            return sb.ToString();
        }

        public string ToJumja(string str)
        {
            // NOTE: 약어에 대한 규칙은 Jumja 클래스가 담당하고, 글자에 대한 규칙은 위임한다.

            StringBuilder sb = new StringBuilder();
            foreach (var word in str.Split(new char[] {' '}))
            {
                sb.Append(ConvertAsWord(str));
            }

            return sb.ToString();
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
                if (!_hangul.IsHangulCharacter(ch))
                {
                    sb.Append(ch);
                    continue;
                }

                sb.Append(new HangulBraille(ch).ToStringWithoutRules());
            }
            return sb.ToString();
        }
    }
}
