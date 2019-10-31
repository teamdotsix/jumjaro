using System.Linq;
using NUnit.Framework;
using Jumjaro;

namespace JumjaroTest
{
    public class JumjaroTests
    {
        [TestCase("한글 점자", "⠚⠒⠈⠮⠀⠨⠎⠢⠨")]
        public void ToJumja(string testStr, string expected)
        {
            Assert.AreEqual(expected, new Jumjaro.Jumjaro().ToJumja(testStr));
        }

        [TestCase("직", "4-6,1-3-5,1")]
        public void ToJumjaTestWithIndexNotaion(string testStr, string expectedIndexNotation)
        {
            string expected = string.Join(string.Empty, Braille.CreateBraillesFromMultipleIndexNotation(expectedIndexNotation).Select(x => x.ToString()));
            string actual = new Jumjaro.Jumjaro().ToJumja(testStr);
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

        [TestCase("직", ".oa")]
        [TestCase("매년 11월 4일은 ‘점자의 날’이다. 송암 박두성 선생이 한글 점자인 ‘훈맹정음’을 세상에 내놓은 1926년 11월 4일을 기념하는 날이다.", "erc* #aap1 #do1z ,8.s5.<w c10'oi4 ,=<5 ~aim,} ,),r7o j3@! .s5.<q,8jger7.}{50'! ,nl7n crcu0z #aibf c* #aap1 #do1! @oc:5jcz c1oi4")]
        public void ToJumjaTestWithBrailleASCII(string testStr, string expectedBrailleASCII)
        {
            string expected = string.Join(string.Empty, Braille.CreateBrailesFromBrailleASCIICode(expectedBrailleASCII).Select(x => x.ToString()));
            string actual = new Jumjaro.Jumjaro().ToJumja(testStr);
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
        public void AcronymRuleTest(string testStr, string expectedBrailleASCII)
        {
            ToJumjaTestWithBrailleASCII(testStr, expectedBrailleASCII);
        }

        // 아래 테스트는 "제2장 제6절, 약자"에서 가져옴
        [TestCase("가자", "$.")]
        [TestCase("바다", "^i")]
        [TestCase("자동차", ".i=;<")]
        [TestCase("가위", "$mr")]
        [TestCase("사격", "l@:a")]

        [TestCase("나들이", "ci!o")]
        [TestCase("다르다", "i\"[i")]
        [TestCase("마당", "ei7")]
        [TestCase("바나나", "^cc")]
        [TestCase("자격증", ".@:a.[7")]
        [TestCase("카네기", "fcn@o")]
        [TestCase("균형타", "@%3j]h")]
        [TestCase("과격파", "@v@:ad")]
        [TestCase("하도급", "jiu@[b")]

        [TestCase("다음", "i<[5")]
        [TestCase("마음", "e<[5")]
        [TestCase("하얀", "j<>3")]
        [TestCase("카카오", "ff<u")]
        [TestCase("타이어", "h<os")]
        [TestCase("파이프", "d<od[")]

        [TestCase("콜레라", "fu1\"n\"<")]
        [TestCase("기차", "@o;<")]

        [TestCase("얼룩소", "t\"ma,u")]
        [TestCase("열거", "\\@s")]
        [TestCase("은빛", "z^o2")]
        [TestCase("가을", "$!")]

        [TestCase("언급", ")@[b")]
        [TestCase("촬영", ";v1]")]
        [TestCase("온기", "(@o")]
        [TestCase("서울", ",s&")]
        [TestCase("크레인", "f[\"nq")]

        [TestCase("기억", "@o?")]
        [TestCase("지옥", ".ox")]
        [TestCase("옹달샘", "=i1,r5")]
        [TestCase("혜운", "j/g")]

        [TestCase("인연", "q*")]
        [TestCase("연예인", "*/q")]
        [TestCase("그것", "@[_s")]
        [TestCase("아무것", "<em_s")]

        [TestCase("강산", "$7l3")]
        [TestCase("난방", "c3^7")]
        [TestCase("달밤", "i1^5")]

        [TestCase("감칠맛", "$5;o1e'")]
        [TestCase("낙엽", "ca:b")]
        [TestCase("망망대해", "e7e7irjr")]
        [TestCase("칼국수", "f1@ma,m")]

        [TestCase("까치", ",$;o")]
        [TestCase("깡충깡충", ",$7;m7,$7;m7")]
        [TestCase("쌍둥이", ",l7im7o")]
        [TestCase("쌍쌍이", ",l7,l7o")]
        [TestCase("한껏", "j3,_s")]
        [TestCase("힘껏", "jo5,_s")]

        [TestCase("불을 껐다.", "^&! ,@s/i4")]

        [TestCase("까마귀", ",$e@mr")]
        [TestCase("쌀통", ",l1h=")]
        [TestCase("정성껏", ".],],_s")]

        [TestCase("꺾다", ",@?ai")]
        [TestCase("넋", "c?'")]
        [TestCase("덕망", "i?e7")]
        [TestCase("건전", "@).)")]
        [TestCase("얹다", ")ki")]
        [TestCase("천하", ";)j")]
        [TestCase("넓다", "ctbi")]
        [TestCase("얽다", "tai")]
        [TestCase("젊다", ".t5i")]
        [TestCase("견학", "@*ja")]
        [TestCase("변화", "^*jv")]
        [TestCase("현황", "j*jv7")]
        [TestCase("별", "^\\")]
        [TestCase("엷다", "\\bi")]
        [TestCase("혈기", "j\\@o")]
        [TestCase("경성", "@],]")]
        [TestCase("병원", "^]p3")]
        [TestCase("평화", "d]jv")]
        [TestCase("곡식", "@x,oa")]
        [TestCase("볶다", "^xai")]
        [TestCase("폭포", "dxdu")]
        [TestCase("논", "c(")]
        [TestCase("돈", "i(")]
        [TestCase("손", ",(")]
        [TestCase("공사", "@=l")]
        [TestCase("송이", ",=o")]
        [TestCase("홍삼", "j=l5")]
        [TestCase("군대", "@gir")]
        [TestCase("눈물", "cge&")]
        [TestCase("문화", "egjv")]
        [TestCase("굵다", "@&ai")]
        [TestCase("굶다", "@&5i")]
        [TestCase("훑다", "j&8i")]
        [TestCase("끊다", ",@z0i")]
        [TestCase("큰언니", "fz)co")]
        [TestCase("튼튼한", "hzhzj3")]
        [TestCase("긁다", "@!ai")]
        [TestCase("늙다", "c!ai")]
        [TestCase("읊다", "!4i")]
        [TestCase("민족", "eq.x")]
        [TestCase("신라", ",q\"<")]
        [TestCase("진실", ".q,o1")]

        [TestCase("고즈넉", "@u.[c?")]
        [TestCase("넌더리", "c)is\"o")]
        [TestCase("널뛰기", "ct,imr@o")]
        [TestCase("면발", "e*^1")]
        [TestCase("별검", "^\\@s5")]
        [TestCase("용병", "+7^]")]
        [TestCase("녹용", "cx+7")]
        [TestCase("논두렁", "c(im\"s7")]
        [TestCase("농구", "c=@m")]
        [TestCase("모눈", "eucg")]
        [TestCase("둘째", "i&,.r")]
        [TestCase("토큰", "hufz")]
        [TestCase("그늘", "@[c!")]
        [TestCase("진격", ".q@:a")]

        // 제16항:‘성, 썽, 정, 쩡, 청’은 ‘ㅅ, ㅆ, ㅈ, ㅉ, ㅊ’ 다음에 ‘ㅕㅇ’의 약자(1-2-4-5-6)를 적어 나타낸다
        [TestCase("성가", ",]$")]
        [TestCase("말썽", "e1,,]")]
        [TestCase("정성", ".],]")]
        [TestCase("어정쩡", "s.],.]")]
        [TestCase("청년", ";]c*")]

        [TestCase("나이", "c<o")]
        [TestCase("다음", "i<[5")]
        [TestCase("마을", "e<!")]
        [TestCase("바위", "^<mr")]
        [TestCase("자아", ".<<")]
        [TestCase("하얀", "j<>3")]

        [TestCase("땅을 팠다.", ",i7! d</i4")]

        [TestCase("다예", "i-/")]
        [TestCase("나예림", "c-/\"o5")]

        // 외래어 표기 또는 중세 국어에서 사용되는 ‘셩, 졍, 쳥’ 등은 해당 첫소리 글자와 모음 ‘ㅕ’, 받침 ‘ㅇ’을 사용하여 어울러 적는다.
        [TestCase("레이셩", "\"no,:7")]
        [TestCase("졍", ".:7")]
        public void AbbreviationRuleTest(string testStr, string expectedBrailleASCII)
        {
            ToJumjaTestWithBrailleASCII(testStr, expectedBrailleASCII);
        }

        [TestCase("오예", "u-/")]
        [TestCase("이예은", "o-/z")]
        [TestCase("노예로 팔리다.", "cu-/\"u d1\"oi4")]
        [TestCase("예예, 잘 알겠습니다.", "/-/\" .1 <1@n/,{bcoi4")]
        [TestCase("아예", ">-r")]
        [TestCase("소화액", ",ujv-ra")]
        [TestCase("구애", "@m-r")]
        public void NucleusChainRuleTest(string testStr, string expectedBrailleASCII)
        {
            ToJumjaTestWithBrailleASCII(testStr, expectedBrailleASCII);
        }

        [TestCase("그래서\n그러나", "⠁⠎\n⠁⠉")]
        [TestCase("그래서 그러면\n그러나", "⠁⠎⠀⠁⠒\n⠁⠉")]
        public void WordSplitTest(string testStr, string expected)
        {
            var actual = new Jumjaro.Jumjaro().ToJumja(testStr);
            Assert.AreEqual(expected, actual);
        }

        [TestCase("한글 점자", "⠚⠣⠒⠈⠪⠂ ⠨⠎⠢⠨⠣")]
        [TestCase("아이", "⠛⠣⠛⠕")]
        public void ToJumjaWithoutRules(string testStr, string expected)
        {
            Assert.AreEqual(expected, new Jumjaro.Jumjaro().ToJumjaWithoutRules(testStr));
        }

        // 아래 테스트는 "제5장 숫자 제11절 국어 문장 안의 숫자"에서 가져왔다
        // 숫자는 수표(3-4-5-6)를 앞세워 적는다
        [TestCase("0", "#j")]
        [TestCase("1", "#a")]
        [TestCase("2", "#b")]
        [TestCase("3", "#c")]
        [TestCase("4", "#d")]
        [TestCase("5", "#e")]
        [TestCase("6", "#f")]
        [TestCase("7", "#g")]
        [TestCase("8", "#h")]
        [TestCase("9", "#i")]
        [TestCase("10", "#aj")]
        [TestCase("23", "#bc")]
        [TestCase("45", "#de")]
        [TestCase("77", "#gg")]
        [TestCase("86", "#hf")]
        [TestCase("100", "#ajj")]
        [TestCase("120", "#abj")]
        [TestCase("375", "#cge")]
        [TestCase("555", "#eee")]
        [TestCase("999", "#iii")]
        // 두 숫자 사이에 빈칸이 있을 경우 수표의 효력이 정지되므로 수표를 다시 적어 준다
        [TestCase("123 4567", "#abc #defg")]
        public void NumberRuleTest(string testStr, string expectedBrailleASCII)
        {
            ToJumjaTestWithBrailleASCII(testStr, expectedBrailleASCII);
        }

        [TestCase("1가", "#a$")]
        [TestCase("2권", "#b@p3")]
        [TestCase("3반", "#c^3")]
        [TestCase("4선", "#d,)")]

        [TestCase("5월", "#ep1")]
        [TestCase("6일", "#fo1")]
        [TestCase("7자루", "#g.\"m")]
        [TestCase("8꾸러미", "#h,@m\"seo")]

        [TestCase("1평은 3.3㎡이다.", "#a d]z #c4c0m^#boi4")]

        //숫자와 혼동되는 ‘ㄴ, ㄷ, ㅁ, ㅋ, ㅌ, ㅍ, ㅎ’의 첫소리 글자와 ‘운’의 약자가 숫자 다음에 이어 나올 때에는 숫자와 한글을 띄어 쓴다.
        [TestCase("1년", "#a c*")]
        [TestCase("2도", "#b iu")]
        [TestCase("3명", "#c e]")]
        [TestCase("4칸", "#d f3")]
        [TestCase("5톤", "#e h(")]
        [TestCase("6평", "#f d]")]
        [TestCase("7항", "#g j7")]
        [TestCase("5운6기", "#e g#f@o")]
        [TestCase("79㎡형", "#gi0m^#b j]")]
        public void NumberWithStringRuleTest(string testStr, string expectedBrailleASCII)
        {
            ToJumjaTestWithBrailleASCII(testStr, expectedBrailleASCII);
        }
    }
}