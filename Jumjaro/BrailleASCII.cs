using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Jumjaro
{
    public static class BrailleASCII
    {
        // NOTE: Braille ASCII Code (also known as SimBraille)
        private static readonly string _brailleASCII = " a1b'k2l@cif/msp\"e3h9o6r^djg>ntq,*5<-u8v.%[$+x!&;:4\\0z7(_?w]#y)=";
        private static readonly string _brailleASCIItoUnicode = "⠀⠁⠂⠃⠄⠅⠆⠇⠈⠉⠊⠋⠌⠍⠎⠏⠐⠑⠒⠓⠔⠕⠖⠗⠘⠙⠚⠛⠜⠝⠞⠟⠠⠡⠢⠣⠤⠥⠦⠧⠨⠩⠪⠫⠬⠭⠮⠯⠰⠱⠲⠳⠴⠵⠶⠷⠸⠹⠺⠻⠼⠽⠾⠿";

        private static char TryGetUnicode(char ch)
        {
            var index = _brailleASCII.IndexOf(ch);
            if (index != -1)
            {
                return _brailleASCIItoUnicode[index];
            }
            return ch;
        }

        public static string ToUnicode(string brailleASCII)
        {
            brailleASCII = brailleASCII.ToLower().Replace(' ', '⠀');
            return new string(brailleASCII.Select(TryGetUnicode).ToArray());
        }

        private static char TryGetASCII(char ch)
        {
            var index = _brailleASCIItoUnicode.IndexOf(ch);
            if (index != -1)
            {
                return _brailleASCII[index];
            }
            return ch;
        }

        public static string FromUnicode(string brailleUnicode)
        {
            return new string(brailleUnicode.Select(TryGetASCII).ToArray());
        }
    }
}
