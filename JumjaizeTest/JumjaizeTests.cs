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

        [TestCase("직", "4-6,1-3-5,1")]
        public void ToJumjaTestWithIndexNotaion(string testStr, string expectedIndexNotation)
        {
            string expected = string.Join(string.Empty, Braille.CreateBraillesFromMultipleIndexNotation(expectedIndexNotation).Select(x => x.ToString()));
            string actual = new Jumjaize.Jumjaize().ToJumja(testStr);
            Assert.AreEqual(expected, actual);
        }

        [TestCase("아이", "1-2-6,1-3-5")]
        [TestCase("우유", "1-3-4,1-4-6")]
        [TestCase("중앙", "4-6,1-3-4,2-3-5-6,1-2-6,2-3-5-6")]
        [TestCase("발음", "4-5,2,2-4-6,2-6")]
        [TestCase("아버지", "1-2-6,4-5,2-3-4,4-6,1-3-5")]
        [TestCase("야유", "3-4-5,1-4-6")]
        [TestCase("어머니", "2-3-4,1-5,2-3-4,1-4,1-3-5")]
        [TestCase("여우", "1-5-6,1-3-4")]
        [TestCase("용", "3-4-6,2-3-5-6")]
        [TestCase("우거지", "1-3-4,4,2-3-4,4-6,1-3-5")]
        [TestCase("솥뚜껑", "6,1-3-6,2-3-6,6,2-4,1-3-4,6,4,2-3-4,2-3-5-6")]
        [TestCase("소뼈", "6,1-3-6,6,4-5,1-5-6")]
        [TestCase("쓰기", "6,6,2-4-6,4,1-3-5")]
        [TestCase("찌개", "6,4-6,1-3-5,4,1-2-3-5")]
        public void JamoTest(string testStr, string expectedIndexNotation)
        {
            ToJumjaTestWithIndexNotaion(testStr, expectedIndexNotation);
        }

        [TestCase("한글 점자", "⠚⠣⠒⠈⠪⠂ ⠨⠎⠢⠨⠣")]
        [TestCase("아이", "⠛⠣⠛⠕")]
        public void ToJumjaWithoutRules(string testStr, string expected)
        {
            Assert.AreEqual(expected, new Jumjaize.Jumjaize().ToJumjaWithoutRules(testStr));
        }
    }
}