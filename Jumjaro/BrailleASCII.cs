using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Jumjaro
{
    public static class BrailleASCII
    {
        // NOTE: Braille ASCII Code (also known as SimBraille)
        private static readonly string _brailleASCII = " A1B'K2L@CIF/MSP\"E3H9O6R^DJG>NTQ,*5<-U8V.%[$+X!&;:4\\0Z7(_?W]#Y)=";
        private static readonly string _brailleASCIItoUnicode = "⠀⠁⠂⠃⠄⠅⠆⠇⠈⠉⠊⠋⠌⠍⠎⠏⠐⠑⠒⠓⠔⠕⠖⠗⠘⠙⠚⠛⠜⠝⠞⠟⠠⠡⠢⠣⠤⠥⠦⠧⠨⠩⠪⠫⠬⠭⠮⠯⠰⠱⠲⠳⠴⠵⠶⠷⠸⠹⠺⠻⠼⠽⠾⠿";

        public static string ToUnicode(string brailleASCII)
        {
            brailleASCII = brailleASCII.ToUpper();
            return new string(brailleASCII.Select(x => _brailleASCIItoUnicode[_brailleASCII.IndexOf(x)]).ToArray());
        }

        public static string FromUnicode(string brailleUnicode)
        {
            return new string(brailleUnicode.Select(x => _brailleASCII[_brailleASCIItoUnicode.IndexOf(x)]).ToArray());
        }
    }
}
