using System;
using System.Collections.Generic;
using System.Linq;

namespace Jumjaro
{
    public class NumberArithmeticBraille
    {
        internal static readonly Dictionary<char, string> ConvertData = new Dictionary<char, string>()
        {
            {'0', "2-4-5"},
            {'1', "1"},
            {'2', "1-2"},
            {'3', "1-4"},
            {'4', "1-4-5"},
            {'5', "1-5"},
            {'6', "1-2-4"},
            {'7', "1-2-4-5"},
            {'8', "1-2-5"},
            {'9', "2-4"},
            /* 사칙연산에 대한 문서를 찾지 못해서 우선은 주석처리
            {',', "2"},
            {'+', "2-6"},
            // NOTE: 하이픈(-), 붙임표(–), 줄표(—)와 혼동하지 말아야 한다.
            // 맹인이 아닌 사람이 빼기표(−)를 제대로 구별해서 쓰는 경우는 드물다. 어떻게 처리해야 할까?
            {'−', "3-5"},
            {'×', "1-6"},
            {'÷', "3-4,3-4"},
            {'=', "2-5,2-5"},
            */
        };

        private readonly char _character;

        public NumberArithmeticBraille(char numberCharacter)
        {
            _character = numberCharacter;
        }

        public override string ToString()
        {
            return Braille.CreateFromIndexNotation(ConvertData[_character]).ToString();
        }

        public string ToStringWithoutRules()
        {
            // 숫자는 룰이 있는것과 없는것이 똑같음
            return ToString();
        }
    }
}
