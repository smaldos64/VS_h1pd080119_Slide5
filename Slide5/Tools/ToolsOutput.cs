using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slide5.Tools
{
    public class ToolsOutput
    {
        public static void PrintStringOnSeperateLine(string StringToPrint)
        {
            Console.WriteLine(StringToPrint);
        }

        public static void PrintStringOnSameLine(string StringToPrint)
        {
            Console.Write(StringToPrint);
        }
    }
}
