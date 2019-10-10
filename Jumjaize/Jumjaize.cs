using System;
using System.Text;

namespace Jumjaize
{

    public class Jumjaize
    {
        private readonly Hangul _hangul = new Hangul();

        public string ToJumja(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var ch in str)
            {
                if (!_hangul.IsHangulCharacter(ch))
                {
                    sb.Append(ch);
                    continue;
                }

                // NOTE: 약어에 대한 규칙은 Jumja 클래스가 담당하고, 글자에 대한 규칙은 위임한다.

                // TODO: 약어 규칙 (그래서, 그러나, 그러면, 그러므로, 그런데, 그리고, 그리하여)

                sb.Append(new HangulBraille(ch).ToString());
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

        // private readonly string _brailleASCII = @" A1B'K2L@CIF/MSP\"E3H9O6R^DJG>NTQ,*5<-U8V.%[$+X!&;:4\\0Z7(_?W]#Y)=";
        // private readonly string _brailleASCIItoUnicode = "⠀⠁⠂⠃⠄⠅⠆⠇⠈⠉⠊⠋⠌⠍⠎⠏⠐⠑⠒⠓⠔⠕⠖⠗⠘⠙⠚⠛⠜⠝⠞⠟⠠⠡⠢⠣⠤⠥⠦⠧⠨⠩⠪⠫⠬⠭⠮⠯⠰⠱⠲⠳⠴⠵⠶⠷⠸⠹⠺⠻⠼⠽⠾⠿";
    }
}
