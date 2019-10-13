using System;
using System.Linq;
using NUnit.Framework;
using Jumjaize;

namespace JumjaizeTest
{
    class HangulMaskTests
    {
        [Test]
        public void ANDOperatorTests()
        {
            Assert.AreEqual(true, new HangulMask('ㄱ', default, default) & '가');
            Assert.AreEqual(true, new HangulMask('ㄱ', default, default) & '각');
            Assert.AreEqual(true, new HangulMask('ㄱ', default, default) & '간');
            Assert.AreEqual(false, new HangulMask('ㄱ', default, default) & '나');
            Assert.AreEqual(false, new HangulMask('ㄱ', default, default) & '낙');
            Assert.AreEqual(false , new HangulMask('ㄱ', default, default) & '난');

            Assert.AreEqual(true, new HangulMask(default, 'ㅏ', default) & '나');
            Assert.AreEqual(true, new HangulMask(default, 'ㅏ', default) & '난');
            Assert.AreEqual(true, new HangulMask(default, 'ㅏ', default) & '아');
            Assert.AreEqual(false, new HangulMask(default, 'ㅏ', default) & '어');
            Assert.AreEqual(false, new HangulMask(default, 'ㅏ', default) & '왜');
            Assert.AreEqual(false, new HangulMask(default, 'ㅏ', default) & '뷁');

            Assert.AreEqual(true, new HangulMask(default, default, 'ㄴ') & '건');
            Assert.AreEqual(true, new HangulMask(default, default, 'ㄴ') & '는');
            Assert.AreEqual(true, new HangulMask(default, default, 'ㄴ') & '돈');
            Assert.AreEqual(false, new HangulMask(default, default, 'ㄴ') & '핳');
            Assert.AreEqual(false, new HangulMask(default, default, 'ㄴ') & '맥');
            Assert.AreEqual(false, new HangulMask(default, default, 'ㄴ') & '누');

            Assert.AreEqual(true, new HangulMask(default, 'ㅚ', 'ㄴ') & '왼');
            Assert.AreEqual(true, new HangulMask(default, 'ㅚ', 'ㄴ') & '된');
            Assert.AreEqual(true, new HangulMask(default, 'ㅚ', 'ㄴ') & '뵌');
            Assert.AreEqual(false, new HangulMask(default, 'ㅚ', 'ㄴ') & '닳');
            Assert.AreEqual(false, new HangulMask(default, 'ㅚ', 'ㄴ') & '용');
            Assert.AreEqual(false, new HangulMask(default, 'ㅚ', 'ㄴ') & '됀');
        }
    }
}
