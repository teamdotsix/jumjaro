using System;
using Jumjaro;
using CommandLine;

namespace JumjaroCLI
{
    class Program
    {
        public class Options
        {
            [Option(SetName="OutputFormat", Default = true, HelpText = "결과를 유니코드 점자로 출력합니다.")]
            public bool Unicode { get; set; }

            [Option(SetName = "OutputFormat", HelpText = "결과를 BRF 점자로 출력합니다.")]
            public bool BRF { get; set; }

            [Value(0, Required = true, HelpText = "점자로 변환할 문자열")]
            public string inputText { get; set; }
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Parser.Default.ParseArguments<Options>(args)
                   .WithParsed<Options>(o =>
                   {
                       if (o.Unicode)
                       {
                           Console.WriteLine(new Jumjaro.Jumjaro().ToJumja(o.inputText));
                       }
                       else
                       {
                           Console.WriteLine(BrailleASCII.FromUnicode(new Jumjaro.Jumjaro().ToJumja(o.inputText)));
                       }
                   });
        }
    }
}
