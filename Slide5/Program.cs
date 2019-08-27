using System;
using System.Text;
using System.Data.Common;
using Slide5.Tools;

namespace Slide5
{
    class Program
    {

        #region Program_Startup
        static void StartProgram()
        {
            ToolsOutput.PrintStringOnSeperateLine("Connect to SQL Server and demo Create, Read, Update and Delete operations.");
            ToolsOutput.PrintStringOnSeperateLine("Tryk en tast for at starte programmet !!!");
            ToolsInput.WaitForUser();
        }

        static void Main(string[] args)
        {
            StartProgram();

            if (ToolsDatabaseUpperLayer.OpenDatabaseConnection())
            {
                MainMenu();
                ToolsScreen.MakeEmptyLines(2);
                ToolsOutput.PrintStringOnSeperateLine("Database Demo done !!!");
            }
            else
            {
                ToolsOutput.PrintStringOnSeperateLine("Database Demo not done - Fejl ved Database forbindelse !!!");
            }

            ToolsInput.WaitForUser();
        }
        #endregion

        #region Menus
        public static void MainMenu()
        {
            string[] StringList = { "1 : Håndter Studerende",
                                    "2 : Håndter Elev Fag",
                                    "3 : Afslut program !!!"};

            int KeypressedValue = 0;

            do
            {
                KeypressedValue = ToolsMenu.MakeMenu(StringList);

                switch (KeypressedValue)
                {
                    case 1:
                        ToolsDatabaseUpperLayer.HandleStudentMenu();
                        break;

                    case 2:
                        ToolsDatabaseUpperLayer.HandleStudentCourseMenu();
                        break;
                }
            } while (KeypressedValue < StringList.Length);
        }
        #endregion
    }
}
