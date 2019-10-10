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

        private readonly HangulSyllableBraille[] _onsets = 
        {
            /*
             * ㄱ(4), ㄴ(1-4), ㄷ(2-4), ㄹ(5), ㅁ(1-5), ㅂ(4-5), ㅅ(8), ㅇ(1-2-4-5), ㅈ(4-6), ㅊ(5-6), ㅋ(1-2-4), ㅌ(1-2-5), ㅍ(1-4-5), ㅎ(2-4-5)
             * 된소리는 초성 앞에 된소리표(6)를 적는다.
             */
            new HangulSyllableBraille("4"),       // ㄱ
            new HangulSyllableBraille("6,8"),     // ㄲ
            new HangulSyllableBraille("1-4"),     // ㄴ
            new HangulSyllableBraille("2-4"),     // ㄷ
            new HangulSyllableBraille("6,2-4"),   // ㄸ
            new HangulSyllableBraille("5"),       // ㄹ
            new HangulSyllableBraille("1-5"),     // ㅁ
            new HangulSyllableBraille("4-5"),     // ㅂ
            new HangulSyllableBraille("6,4-5"),   // ㅃ
            new HangulSyllableBraille("8"),       // ㅅ
            new HangulSyllableBraille("6,8"),     // ㅆ
            new HangulSyllableBraille("1-2-4-5"), // ㅇ
            new HangulSyllableBraille("4-6"),     // ㅈ
            new HangulSyllableBraille("6,4-6"),   // ㅉ
            new HangulSyllableBraille("5-6"),     // ㅊ
            new HangulSyllableBraille("1-2-4"),   // ㅋ
            new HangulSyllableBraille("1-2-5"),   // ㅌ 
            new HangulSyllableBraille("1-4-5"),   // ㅍ
            new HangulSyllableBraille("2-4-5"),   // ㅎ
        };

        private readonly HangulSyllableBraille[] _nucleuses =
        {
            /*
             * 모음 뒤에 '예'가 오거나 'ㅑ, ㅘ, ㅜ, ㅞ' 다음에 '애'가 이어 나오는 경우 그 사이에 (3-6)를 적는다. 단, 행이 바뀌는 경우에는 적지 않아도 된다.
             */
            new HangulSyllableBraille("1-2-6"),            // 'ㅏ',
            new HangulSyllableBraille("1-2-3-5"),          // 'ㅐ',
            new HangulSyllableBraille("3-4-5"),            // 'ㅑ',
            new HangulSyllableBraille("3-4-5,1-2-3-5"),    // 'ㅒ',
            new HangulSyllableBraille("2-3-4"),            // 'ㅓ',
            new HangulSyllableBraille("1-3-4-5"),          // 'ㅔ',
            new HangulSyllableBraille("1-5-6"),            // 'ㅕ',
            new HangulSyllableBraille("3-4"),              // 'ㅖ',
            new HangulSyllableBraille("1-3-6"),            // 'ㅗ',
            new HangulSyllableBraille("1-2-3-6"),          // 'ㅘ',
            new HangulSyllableBraille("1-2-3-6,1-2-3-5"),  // 'ㅙ',
            new HangulSyllableBraille("1-3-4-5-6"),        // 'ㅚ',
            new HangulSyllableBraille("3-4-6"),            // 'ㅛ',
            new HangulSyllableBraille("1-3-4"),            //'ㅜ',
            new HangulSyllableBraille("1-2-3-4"),          //'ㅝ',
            new HangulSyllableBraille("1-2-3-4,1-2-3-5"),  //'ㅞ',
            new HangulSyllableBraille("1-3-4,1-2-3-5"),    //'ㅟ',
            new HangulSyllableBraille("1-4-6"),            //'ㅠ',
            new HangulSyllableBraille("2-4-6"),            //'ㅡ',
            new HangulSyllableBraille("2-4-5-6"),          //'ㅢ',
            new HangulSyllableBraille("1-3-5"),            //'ㅣ'

           // 모음  ㅏ          ㅑ          ㅓ         ㅕ          ㅗ         ㅛ       ㅜ           ㅠ         ㅡ        ㅣ         ㅐ        ㅔ
           // 점자 (1-2-6)    (3-4-5)    (2-3-4)    (1-5-6)    (1-3-6)    (3-4-6)    (1-3-4)    (1-4-6)    (2-4-6)    (1-3-5)    (1-2-3-5)  (1-3-4-5)

           // 이중모음    ㅒ                  ㅖ         ㅘ                 ㅙ               ㅚ                ㅝ             ㅞ                       ㅟ            ㅢ
           // 점자   (3-4-5) (1-2-3-5)  |   (3-4) |  (1-2-3-6)  |  (1-2-3-6) (1-2-3-5) |  (1-3-4-5-6) |   (1-2-3-4) |  (1-2-3-4) (1-2-3-5) |  (1-3-4) (1-2-3-5)  |  (2-4-5-6)
        };

        private readonly HangulSyllableBraille[] _codas =
        {
            // 받침 'ㄲ'은 6점 점자 (1)6점 점자 (1)(1, 1), 받침 'ㅆ'은 6점 점자 (3-4)(3-4)로 적고, 나머지 겹받침은 해당하는 낱자를 순서대로 적는다.
            null,

            new HangulSyllableBraille("1"), // ㄱ
            new HangulSyllableBraille("1,1"), // ㄲ
            new HangulSyllableBraille("1,3"), // ㄳ
            new HangulSyllableBraille("2-5"), // ㄴ
            new HangulSyllableBraille("2-5,1-3"), // ㄵ
            new HangulSyllableBraille("2-5,3-5-6"), // ㄶ
            new HangulSyllableBraille("3-5"), // ㄷ
            new HangulSyllableBraille("2"), // ㄹ
            new HangulSyllableBraille("2,1"), // ㄺ
            new HangulSyllableBraille("2,2-6"), // ㄻ
            new HangulSyllableBraille("2,1-2"), // ㄼ
            new HangulSyllableBraille("2,3"), // ㄽ
            new HangulSyllableBraille("2,2-3-6"), // ㄾ
            new HangulSyllableBraille("2,2-5-6"), // ㄿ
            new HangulSyllableBraille("2,3-5-6"), // ㅀ
            new HangulSyllableBraille("2-6"), // ㅁ
            new HangulSyllableBraille("1-2"), // ㅂ
            new HangulSyllableBraille("1-2,3"), // ㅄ
            new HangulSyllableBraille("3"), // ㅅ
            new HangulSyllableBraille("3-4"), // ㅆ
            new HangulSyllableBraille("2-3-5-6"), // ㅇ
            new HangulSyllableBraille("1-3"), // ㅈ
            new HangulSyllableBraille("2-3"), // ㅊ
            new HangulSyllableBraille("2-3-5"), // ㅋ
            new HangulSyllableBraille("2-3-6"), // ㅌ
            new HangulSyllableBraille("2-5-6"), // ㅍ
            new HangulSyllableBraille("3-5-6"), // ㅎ

            // 자음	ㄱ	ㄴ          	ㄷ 	 ㄹ   	ㅁ	      ㅂ	 ㅅ     	ㅇ	      ㅈ	 ㅊ     	ㅋ	      ㅌ	      ㅍ      	ㅎ
            // 종성	 (1)	 (2-5)	 (3-5)	 (2)	 (2-6)	 (1-2)	 (3)	 (2-3-5-6)	 (1-3)	 (2-3)	 (2-3-5)	 (2-3-6)	 (2-5-6)	 (3-5-6)

            // '\0', 'ㄱ','ㄲ','ㄳ','ㄴ','ㄵ','ㄶ','ㄷ','ㄹ','ㄺ','ㄻ','ㄼ','ㄽ','ㄾ',
            // 'ㄿ','ㅀ','ㅁ','ㅂ','ㅄ','ㅅ','ㅆ','ㅇ','ㅈ','ㅊ','ㅋ','ㅌ','ㅍ','ㅎ'
        };

        public string ToJumja(string str)
        {
            throw new NotImplementedException();
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

                var syllableIndices = _hangul.SyllabificationIndex(ch);
                var onset = _onsets[syllableIndices[0]];
                if (onset != null)
                {
                    sb.Append(onset.ToString());
                }

                var nucleus = _nucleuses[syllableIndices[1]];
                if (nucleus != null)
                {
                    sb.Append(nucleus.ToString());
                }

                var coda = _codas[syllableIndices[2]];
                if (coda != null)
                {
                    sb.Append(coda.ToString());
                }
            }
            return sb.ToString();
        }

        // private readonly string _brailleASCII = @" A1B'K2L@CIF/MSP\"E3H9O6R^DJG>NTQ,*5<-U8V.%[$+X!&;:4\\0Z7(_?W]#Y)=";
        // private readonly string _brailleASCIItoUnicode = "⠀⠁⠂⠃⠄⠅⠆⠇⠈⠉⠊⠋⠌⠍⠎⠏⠐⠑⠒⠓⠔⠕⠖⠗⠘⠙⠚⠛⠜⠝⠞⠟⠠⠡⠢⠣⠤⠥⠦⠧⠨⠩⠪⠫⠬⠭⠮⠯⠰⠱⠲⠳⠴⠵⠶⠷⠸⠹⠺⠻⠼⠽⠾⠿";
    }
}
