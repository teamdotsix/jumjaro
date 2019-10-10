using System;
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
