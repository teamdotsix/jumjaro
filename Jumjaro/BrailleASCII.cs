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

        public static string ToUnicode(string brailleASCII)
        {
            brailleASCII = brailleASCII.ToLower();
            return new string(brailleASCII.Select(x => _brailleASCIItoUnicode[_brailleASCII.IndexOf(x)]).ToArray());
        }

        public static string FromUnicode(string brailleUnicode)
        {
            return new string(brailleUnicode.Select(x => _brailleASCII[_brailleASCIItoUnicode.IndexOf(x)]).ToArray());
        }
    }
}
