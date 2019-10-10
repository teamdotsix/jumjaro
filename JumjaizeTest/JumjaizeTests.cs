using System.Linq;
using NUnit.Framework;
using Jumjaize;

namespace JumjaizeTest
{
    public class JumjaizeTests
    {
        [TestCase("한글 점자", "⠚⠒⠈⠮⠀⠨⠎⠢⠨")]
        public void ToJumja(string testStr, string expected)
        {
            Assert.AreEqual(expected, new Jumjaize.Jumjaize().ToJumja(testStr));
        }

        [TestCase("아버지", "1-2-6,4-5,2-3-4,4-6,1-3-5")]
        public void ToJumjaTestWithIndexNotaion(string testStr, string expectedIndexNotation)
        {
            string expected = string.Join(string.Empty, Braille.CreateBraillesFromMultipleIndexNotation(expectedIndexNotation).Select(x => x.ToString()));
            string actual = new Jumjaize.Jumjaize().ToJumja(testStr);
            Assert.AreEqual(expected, actual);
        }

        [TestCase("한글 점자", "⠚⠣⠒⠈⠪⠂ ⠨⠎⠢⠨⠣")]
        public void ToJumjaDirectly(string testStr, string expected)
        {
            Assert.AreEqual(expected, new Jumjaize.Jumjaize().ToJumjaDirectly(testStr));
        }
    }
}