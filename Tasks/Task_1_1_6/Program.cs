using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1_1_6 // FONT ADJUSTMENT
{
    class Program
    {
        static void Main(string[] args)
        {
            char c_key;
            byte key = 9;
            FontStyles textParam = 0;
            while ( key != 0 )
            {
                Console.Write("Параметры надписи: ");
                if (textParam.HasFlag(FontStyles.None))
                    Console.Write(FontStyles.None + " ");
                if (textParam.HasFlag(FontStyles.Bold))
                    Console.Write(FontStyles.Bold + " ");
                if (textParam.HasFlag(FontStyles.Italic))
                    Console.Write(FontStyles.Italic + " ");
                if (textParam.HasFlag(FontStyles.Underline))
                    Console.Write(FontStyles.Underline + " ");
                Console.WriteLine();

                Console.WriteLine("Введите:\n\t0: Exit\n\t1: Bold\n\t2: Italic\n\t3: Underline\n");
                c_key = Console.ReadKey().KeyChar;
                byte.TryParse(c_key.ToString(), out key);
                Console.WriteLine();

                switch (key) {
                    case 0:
                        break;
                    case 1:
                        toggleParam(ref textParam, FontStyles.Bold);
                        break;
                    case 2:
                        toggleParam(ref textParam, FontStyles.Italic);
                        break;
                    case 3:
                        toggleParam(ref textParam, FontStyles.Underline);
                        break;
                    default:
                        Console.WriteLine("Некорректный ввод");
                        break;
                }
            }
        }

        //bool getParamState(FontStyles param) { }

        static void toggleParam(ref FontStyles param, FontStyles style)
        {
            if ( param.HasFlag(style) )
                param = (byte)param - style;
            else
                param = (byte)param + style;
        }

        [Flags]
        enum FontStyles : byte
        {
            None = 0,
            Bold = 1,
            Italic = 2,
            Underline = 4
        };
    }
}
