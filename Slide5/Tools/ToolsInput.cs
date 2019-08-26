using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Slide5.Tools
{
    public class ToolsInput
    {
        public static Char[] NumericChars
        {
            get
            {
                return new char[10] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            }
        }

        public static Char[] NumericCharsPlusReturn
        {
            get
            {
                return new char[11] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '\r' };
            }
        }

        public const char ReturnChar = '\r';

        public static Char GetKeyPress(String msg, Char[] validChars, bool? MakeNewLine = true)
        {
            ConsoleKeyInfo keyPressed;
            bool valid = false;

            for (int Counter = 0; Counter < validChars.Length; Counter++)
            {
                validChars[Counter] = Char.ToLower(validChars[Counter]);
            }

            Console.WriteLine();
            Console.Write(msg);
            do
            {
                keyPressed = Console.ReadKey(true);
                if (Array.Exists(validChars, ch => ch.Equals(Char.ToLower(keyPressed.KeyChar))))
                {
                    valid = true;
                    Console.Write(keyPressed.KeyChar);
                }

            } while (!valid);

            if ((bool)MakeNewLine)
            {
                Console.WriteLine();
            }

            return keyPressed.KeyChar;
        }

        public static void WaitForUser()
        {
            Console.ReadLine();
        }

        //public static void GetInputValues<T>(T[] InputArray,
        //                                     string TextToShow,
        //                                     char ? TextCharSplitter = ' ')
        //{
        //    ToolsOutput.PrintStringOnSameLine(TextToShow);
        //    string[] PointStrings = Console.ReadLine().Split((char)TextCharSplitter);
        //    for (int Counter = 0; Counter < PointStrings.Length; Counter++)
        //    {
        //        switch (InputArray.GetType())
        //        //InputArray[Counter] = PointStrings[Counter];
        //    }

        //}


        public static bool GetUserInput(out int Variable, string Message)
        {
            string S;
            bool Return = false;
            do
            {
                Console.Write(Message);
                S = Console.ReadLine();
                if (int.TryParse(S, out Variable))
                {
                    Return = !Return;
                }
                else
                {
                    ClearLast();
                }
            }
            while (!Return);
            return true;
        }

        //private static double GetDouble(string value, double defaultValue)
        //{
        //    double result;

        //    //Try parsing in the current culture
        //    if (!double.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out result) &&
        //        //Then try in US english
        //        !double.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out result) &&
        //        //Then in neutral language
        //        !double.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out result))
        //    {
        //        result = defaultValue;
        //    }

        //    return result;
        //}
                
        public static bool GetUserInput(out double Variable, string Message)
        {
            string S;
            bool Return = false;
            do
            {
                Console.Write(Message);
                S = Console.ReadLine();
                if (double.TryParse(S.Replace(',', '.'), System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.NumberFormatInfo.InvariantInfo, out Variable))
                //if (double.TryParse(S.Replace('.', ','), out Variable)) 
                {
                    Return = !Return;
                }
                else
                {
                    ClearLast();
                }
            }
            while (!Return);
            return true;
        }

        public static bool GetUserInput(out string Variable, string Message)
        {
            bool Return = false;
            do
            {
                Console.Write(Message);
                Variable = Console.ReadLine();
                if (Variable.Length > 0 || Variable[0] != ' ')
                {
                    Return = !Return;
                }
                else
                {
                    ClearLast();
                }
            }
            while (!Return);
            return true;
        }

        public static bool ClearLast()
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            return true;
        }
    }
}
