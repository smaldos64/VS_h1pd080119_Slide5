using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slide5.Tools
{
    public class ToolsMenu
    {

        public static int MakeMenu(string[] StringListHere)
        {
            int KeypressedValue = 0;
            char KeypressedValueChar = '0';
            char[] ValidCharArray = new char[StringListHere.Length];
            
            for (int Counter = 0; Counter < StringListHere.Length; Counter++)
            {
                char[] _chars = (Counter + 1).ToString().ToCharArray();
                ValidCharArray[Counter] = _chars[0];
            }

            ToolsScreen.ClearScreen();
            foreach (string Item in StringListHere)
            {
                ToolsOutput.PrintStringOnSeperateLine(Item);
            }
            KeypressedValueChar = ToolsInput.GetKeyPress("Indtast dit valg (1 - " + StringListHere.Length + ") : ", ValidCharArray, false);
            KeypressedValue = KeypressedValueChar.ParseInt32();
           
            return (KeypressedValue);
        }

    }
}
