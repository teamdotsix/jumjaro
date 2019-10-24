using System;
using System.Linq;
using NUnit.Framework;
using Jumjaro;

namespace JumjaroTest
{
    class BrailleTests
    {
        [TestCase("4", 8)]
        [TestCase("1-4", 9)]
        [TestCase("1-2-5", 19)]
        [TestCase("1-2-4-5", 27)]
        public void ConvertIndexNotation(string notation, int expectedValue)
        {
            Assert.AreEqual(expectedValue, Braille.ConvertIndexNotationToInt(notation));
        }

        [TestCase("4-6,1-3-5,1", "⠨⠕⠁")]
        public void CreateBrailes(string notation, string expected)
        {
            string brailles = string.Join(string.Empty,
                Braille.CreateBraillesFromMultipleIndexNotation(notation).Select(x => x.ToString()));
            Assert.AreEqual(expected, brailles);
        }

        [TestCase(",,[@o", "⠠⠠⠪⠈⠕")]
        public void CreateBraillesFromASCII(string brailASCII, string expected)
        {
            var actual = string.Join(string.Empty,
                Braille.CreateBrailesFromBrailleASCIICode(brailASCII).Select(x => x.ToString()));
            Assert.AreEqual(expected, actual);
        }
    }
}
