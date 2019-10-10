using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Jumjaize;

namespace JumjaizeTest
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
    }
}
