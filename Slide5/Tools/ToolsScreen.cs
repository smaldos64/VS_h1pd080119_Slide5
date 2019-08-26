using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slide5.Tools
{
    class ToolsScreen
    {
        public static void ClearLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
        }

        public static void ClearScreen()
        {
            Console.Clear();
        }

        public static void MakeEmptyLines(int NumberOfEmptyLines)
        {
            for (int Counter = 0; Counter < NumberOfEmptyLines; Counter++)
            {
                ToolsOutput.PrintStringOnSeperateLine("");
            }
        }
    }
}
