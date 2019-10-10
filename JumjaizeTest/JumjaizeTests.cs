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

        [TestCase("한글 점자", "⠚⠣⠒⠈⠪⠂ ⠨⠎⠢⠨⠣")]
        public void ToJumjaDirectly(string testStr, string expected)
        {
            Assert.AreEqual(expected, new Jumjaize.Jumjaize().ToJumjaDirectly(testStr));
        }
    }
}