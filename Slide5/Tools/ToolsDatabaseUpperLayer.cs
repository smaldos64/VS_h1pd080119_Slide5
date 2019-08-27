using System;
using System.Collections.Generic;
using System.Text;

namespace Slide5.Tools
{
    class ToolsDatabaseUpperLayer
    {
        #region DastabaseConnection
        public static bool OpenDatabaseConnection()
        {
            return (ToolsDataBaseLowerLayer.OpenDatabaseConnection());
        }
        #endregion

        #region menus
        public static void HandleStudentMenu()
        {
            int KeypressedValue = 0;
            string[] StringList = { "1 : Se Studerende Liste (SQL)",
                                    "2 : Se Studerende Liste (View)",
                                    "3 : Se Studerende Liste (Stored Procedure)",
                                    "4 : Se udvalgt Studerende (Stored Procedure)",
                                    "5 : Tilbage !!!"};
            do
            {
                KeypressedValue = ToolsMenu.MakeMenu(StringList);

                ToolsScreen.MakeEmptyLines(2);

                switch (KeypressedValue)
                {
                    case 1:
                        WatchStudentList();
                        break;

                    case 2:
                        WatchStudentListView();
                        break;

                    case 3:
                        WatchStudentListStoredProcedure();
                        break;

                    case 4:
                        WatchStudentStoredProcedure();
                        break;
                }
            } while (KeypressedValue < StringList.Length);
        }

        public static void HandleStudentCourseMenu()
        {
            int KeypressedValue = 0;
            string[] StringList = { "1 : Se Studerende - Fag Liste (SQL)",
                                    "2 : Sæt Studerende på nyt Fag",
                                    "3 : Ret Karakter for Studerende på Fag",
                                    "4 : Tilbage !!!"};
            do
            {
                KeypressedValue = ToolsMenu.MakeMenu(StringList);

                ToolsScreen.MakeEmptyLines(2);

                switch (KeypressedValue)
                {
                    case 1:
                        WatchStudentCourseList();
                        break;

                    case 2:
                        AddCourseForStudent();
                        break;

                    case 3:
                        ModifyGradeForStudentOnCourse();
                        break;
                }
            } while (KeypressedValue < StringList.Length);
        }
        #endregion

        #region Database_Access
        public static void WatchStudentList()
        {
            ToolsDataBaseLowerLayer.WatchStudentList(ToolsDataBaseLowerLayer.GetSQLCommandAndFields());
        }

        public static void WatchStudentListView()
        {
            ToolsDataBaseLowerLayer.WatchStudentList(ToolsDataBaseLowerLayer.GetSQLCommandAndFieldsView());
        }

        public static void WatchStudentListStoredProcedure()
        {
            ToolsDataBaseLowerLayer.WatchStudentList(ToolsDataBaseLowerLayer.GetSQLCommandAndFields_SP());
        }

        public static void WatchStudentStoredProcedure()
        {

        }

        public static void WatchStudentCourseList()
        {

        }

        public static void AddCourseForStudent()
        {

        }

        public static void ModifyGradeForStudentOnCourse()
        {

        }
        #endregion

    }
}
