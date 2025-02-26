using System;
using System.Collections.Generic;
using System.Text;
using Jumjaro;
using NUnit.Framework;

namespace JumjaroTest
{
    class PunctuationMarkBrailleTests
    {
        [TestCase("[윤석중 전집(1988), 70쪽 참조]", "⠦⠆⠩⠒⠠⠹⠨⠍⠶⠀⠨⠾⠨⠕⠃⠦⠄⠼⠁⠊⠓⠓⠠⠴⠐⠀⠼⠛⠚⠠⠨⠭⠀⠰⠣⠢⠨⠥⠰⠴")]
        [TestCase("훈민정음』은 1997년에 유네스코 세계 기록 유산으로 지정되었다.", "⠚⠛⠑⠟⠨⠻⠪⠢⠴⠆⠵⠀⠼⠁⠊⠊⠛⠀⠉⠡⠝⠀⠩⠉⠝⠠⠪⠋⠥⠀⠠⠝⠈⠌⠀⠈⠕⠐⠭⠀⠩⠇⠒⠪⠐⠥⠀⠨⠕⠨⠻⠊⠽⠎⠌⠊⠲")]
        [TestCase("이 곡은 베르디가 작곡한 「축배의 노래」이다.", "⠕⠀⠈⠭⠵⠀⠘⠝⠐⠪⠊⠕⠫⠀⠨⠁⠈⠭⠚⠒⠀⠐⠦⠰⠍⠁⠘⠗⠺⠀⠉⠥⠐⠗⠴⠂⠕⠊⠲")]
        [TestCase("《한성순보》는 우리나라 최초의 근대 신문이다.", "⠰⠶⠚⠒⠠⠻⠠⠛⠘⠥⠶⠆⠉⠵⠀⠍⠐⠕⠉⠐⠣⠀⠰⠽⠰⠥⠺⠀⠈⠵⠊⠗⠀⠠⠟⠑⠛⠕⠊⠲")]
        [TestCase("백남준은 2005년에 〈엄마〉라는 작품을 선보였다.", "⠘⠗⠁⠉⠢⠨⠛⠵⠀⠼⠃⠚⠚⠑⠀⠉⠡⠝⠀⠐⠶⠎⠢⠑⠶⠂⠐⠣⠉⠵⠀⠨⠁⠙⠍⠢⠮⠀⠠⠾⠘⠥⠱⠌⠊⠲")]
        [TestCase("이번 토론회의 제목은 ‘역사 바로잡기 ― 근대의 설정 ―’이다.", "⠕⠘⠾⠀⠓⠥⠐⠷⠚⠽⠺⠀⠨⠝⠑⠭⠵⠀⠠⠦⠱⠁⠇⠀⠘⠐⠥⠨⠃⠈⠕⠀⠤⠤⠀⠈⠵⠊⠗⠺⠀⠠⠞⠨⠻⠀⠤⠤⠴⠄⠕⠊⠲")] // 줄표(dash)
        [TestCase("드디어 서울-호치민의 항로가 열렸다.", "⠊⠪⠊⠕⠎⠀⠠⠎⠯⠤⠚⠥⠰⠕⠑⠟⠺⠀⠚⠶⠐⠥⠫⠀⠳⠐⠱⠌⠊⠲")] // 붙임표(hypen)
        [TestCase("9월 15일~9월 25일", "⠼⠊⠏⠂⠀⠼⠁⠑⠕⠂⠈⠔⠼⠊⠏⠂⠀⠼⠃⠑⠕⠂")]
        public void ToJumjaTestWithPunctuationMarkBraille(string testStr, string expected)
        {
            Assert.AreEqual(expected, new Jumjaro.Jumjaro().ToJumja(testStr));
        }
    }
}
