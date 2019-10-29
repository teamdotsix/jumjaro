using System;
using System.Collections.Generic;
using System.Text;
using NUnit;
using NUnit.Framework;

namespace JumjaroTest
{
    class BrailleASCIITests
    {
        [TestCase(",,[@o", "⠠⠠⠪⠈⠕")]
        public void BrailleASCIItoBrailleUnicodeTest(string testStr, string expected)
        {
            var actual = Jumjaro.BrailleASCII.ToUnicode(testStr);
            Assert.AreEqual(expected, actual);
        }

        [TestCase("⠠⠠⠪⠈⠕", ",,[@o")]
        public void BrailleUnicodetoBrailleASCIITest(string testStr, string expected)
        {
            var actual = Jumjaro.BrailleASCII.FromUnicode(testStr);
            Assert.AreEqual(expected, actual);
        }
    }
}
