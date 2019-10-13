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

        // 아래 테스트는 "제1장 제1절, 첫소리 자리에 쓰인 자음자"에서 가져옴
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
        public void OnsetRuleTest(string testStr, string expectedIndexNotation)
        {
            ToJumjaTestWithIndexNotaion(testStr, expectedIndexNotation);
        }

        [TestCase("직", ".OA")]
        public void ToJumjaTestWithBrailleASCII(string testStr, string expectedBrailleASCII)
        {
            string expected = string.Join(string.Empty, Braille.CreateBrailesFromBrailleASCIICode(expectedBrailleASCII).Select(x => x.ToString()));
            string actual = new Jumjaize.Jumjaize().ToJumja(testStr);
            Assert.AreEqual(expected, actual);
        }

        // 아래 테스트는 "제1장 제2절, 받침으로 쓰인 자음자"에서 가져옴
        [TestCase("국어", "@mas")]
        [TestCase("롤러", "\"u1\"s")]
        [TestCase("법률", "^sb\"%1")]
        [TestCase("버섯", "^s,s'")]
        [TestCase("젖다", ".ski")]
        [TestCase("꽃", ",@u2")]
        [TestCase("난중일기", "c3.m7o1@o")]
        [TestCase("딛다", "io9i")]
        [TestCase("멈추다", "es5;mi")]
        [TestCase("포용","du+7")]
        [TestCase("북녘", "^mac:6")]
        [TestCase("밥솥", "^b,u8")]
        [TestCase("앞자리", "<4.\"o")]
        [TestCase("좋다", ".u0i")]
        [TestCase("일자 형태의 부엌!", "o1. j]hrw ^ms66")]
        [TestCase("낚시", "caa,o")]
        [TestCase("있다", "o/i")]
        [TestCase("안팎", "<3daa")]
        [TestCase("보았다", "^u</i")]
        [TestCase("깎았다", ",$aa</i")]
        [TestCase("엮었다", ":aas/i")]
        [TestCase("무예", "em-/")]
        [TestCase("이지예", "o.o-/")]
        [TestCase("삯", "la'")]
        [TestCase("앉다", "<3ki")]
        [TestCase("않다", "<30i")]
        [TestCase("읽다", "o1ai")]
        [TestCase("옮기다", "u15@oi")]
        [TestCase("밟다", "^1bi")]
        [TestCase("외곬", "y@u1'")]
        [TestCase("핥다", "j18i")]
        [TestCase("읊다", "!4i")]
        [TestCase("옳다", "u10i")]
        [TestCase("없다", "sb'i")]
        [TestCase("품삯", "dm5la'")]
        [TestCase("앉은 자리", "<3kz .\"o")]
        [TestCase("많이", "e30o")]
        [TestCase("칡차", ";o1a;<")]
        [TestCase("옮김", "u15@o5")]
        [TestCase("얇다", ">1bi")]
        [TestCase("옰", "u1'")]
        [TestCase("개미핥기", "@reoj18@o")]
        [TestCase("앒", "<14")]
        [TestCase("싫증", ",o10.[7")]
        [TestCase("책값", ";ra$b'")]
        [TestCase("몫", "ex'")]
        [TestCase("얽매이다", "taeroi")]
        [TestCase("읊조리다", "!4.u\"oi")]
        public void CodaRuleTest(string testStr, string expectedBrailleASCII)
        {
            ToJumjaTestWithBrailleASCII(testStr, expectedBrailleASCII);
        }

        // 아래 테스트는 "제1장 제3절, 모음자"에서 가져옴
        [TestCase("새우", ",rm")]
        [TestCase("얘기", ">r@o")]
        [TestCase("게으름", "@n[\"[5")]
        [TestCase("예약", "/>a")]
        [TestCase("관람", "@v3\"<5")]
        [TestCase("쇄국", ",vr@ma")]
        [TestCase("쇠고기", ",y@u@o")]
        [TestCase("권리", "@p3\"o")]
        [TestCase("스웨터", ",[prhs")]
        [TestCase("취소", ";mr,u")]
        [TestCase("의식", "w,oa")]
        public void NucleusRuleTest(string testStr, string expectedBrailleASCII)
        {
            ToJumjaTestWithBrailleASCII(testStr, expectedBrailleASCII);
        }

        // 아래 테스트는 "제1장 제7절, 약어"에서 가져옴
        [TestCase("그래서", "as")]
        [TestCase("그러나", "ac")]
        [TestCase("그러면", "a3")]
        [TestCase("그러므로", "a5")]
        [TestCase("그런데", "an")]
        [TestCase("그리고", "au")]
        [TestCase("그리하여", "a:")]
        [TestCase("그림을 그리고 있다.", "@[\"o5! au o/i4")]
        [TestCase("그래서인지", "asq.o")]
        [TestCase("그러면서", "a3,s")]
        [TestCase("그런데도", "aniu")]
        [TestCase("그리하여도", "a:iu")]
        [TestCase("쭈그리고", ",.m@[\"o@u")]
        [TestCase("우그리고", "m@[\"o@u")]
        [TestCase("오그리고", "u@[\"o@u")]
        [TestCase("찡그리고", ",.o7@[\"o@u")]
        public void AcronymsRuleTest(string testStr, string expectedBrailleASCII)
        {
            ToJumjaTestWithBrailleASCII(testStr, expectedBrailleASCII);
        }

        [TestCase("그래서\n그러나", "⠁⠎\n⠁⠉")]
        [TestCase("그래서 그러면\n그러나", "⠁⠎ ⠁⠒\n⠁⠉")]
        public void WordSplitTest(string testStr, string expected)
        {
            var actual = new Jumjaize.Jumjaize().ToJumja(testStr);
            Assert.AreEqual(expected, actual);
        }

        [TestCase(",,[@o", "⠠⠠⠪⠈⠕")]
        public void BrailleASCIItoBrailleUnicodeTest(string testStr, string expected)
        {
            var actual = Jumjaize.BrailleASCII.ToUnicode(testStr);
            Assert.AreEqual(expected, actual);
        }

        [TestCase("⠠⠠⠪⠈⠕", ",,[@O")]
        public void BrailleUnicodetoBrailleASCIITest(string testStr, string expected)
        {
            var actual = Jumjaize.BrailleASCII.FromUnicode(testStr);
            Assert.AreEqual(expected, actual);
        }

        [TestCase("한글 점자", "⠚⠣⠒⠈⠪⠂ ⠨⠎⠢⠨⠣")]
        [TestCase("아이", "⠛⠣⠛⠕")]
        public void ToJumjaWithoutRules(string testStr, string expected)
        {
            Assert.AreEqual(expected, new Jumjaize.Jumjaize().ToJumjaWithoutRules(testStr));
        }
    }
}