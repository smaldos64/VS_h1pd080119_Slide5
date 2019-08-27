using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace Slide5.Tools
{
    #region Delegates
    public delegate string ConvertReceivedDatabaseValue(SqlDataReader Reader, int ColumnNumber);
    #endregion

    #region Database_Table_Definitions
    public class DatabaseInfo
    {
        public string FieldName;
        public int FieldColumnNumber;
        public bool ShowField;
        public ConvertReceivedDatabaseValue ConvertReceivedDatabaseValueFunc;

        public DatabaseInfo(string FieldName,
                            int FieldColumnNumber,
                            bool ShowField,
                            ConvertReceivedDatabaseValue ConvertReceivedDatabaseValueFunc)
        {
            this.FieldName = FieldName;
            this.FieldColumnNumber = FieldColumnNumber;
            this.ShowField = ShowField;
            this.ConvertReceivedDatabaseValueFunc = ConvertReceivedDatabaseValueFunc;
        }
    }

    public class SQLAndDatabaseInfo
    {
        public List<DatabaseInfo> DatabseInfoList = new List<DatabaseInfo>();
        public string SQLCommandString;

        public SQLAndDatabaseInfo(List<DatabaseInfo> DatabseInfoList, string SQLCommandString)
        {
            this.DatabseInfoList = DatabseInfoList;
            this.SQLCommandString = SQLCommandString;
        }

        public SQLAndDatabaseInfo()
        {

        }
    }
    #endregion

    #region Databse_Access
    public class ToolsDataBaseLowerLayer
    {
        private static SqlConnectionStringBuilder Builder;
        private static SqlConnection Connection;

        #region SQL_Strings
        private const string SqlString = @"SELECT s.StudentID, s.StudentName, s.StudentAddress, s.StudentNumberOfCourses, cl.ClassName, co.CourseName
                                            FROM  dbo.Class AS cl INNER JOIN
                                            dbo.StudentClass_RepetitionOnClass AS scc ON cl.ClassID = scc.ClassID INNER JOIN
                                            dbo.Student AS s ON s.StudentID = scc.StudentID INNER JOIN
                                            dbo.Course AS co ON scc.CourseID = co.CourseID
                                            WHERE(cl.ClassName LIKE '%')";

        private const string  SQLStringView = @"Select * FROM FullStudentInfoView";

        private const string SQLString_SP = @"EXEC FullStudentInfo_SP";
        #endregion

        #region SQL_Tables
        private static List<DatabaseInfo> StudentListInfo = new List<DatabaseInfo>()
        {
            new DatabaseInfo(FieldName: "StudentID", FieldColumnNumber: 0, ShowField: true,  ConvertReceivedDatabaseValueFunc:ToolsDatabaseExtensions.ConvertReceivedIntDatabaseValue),
            new DatabaseInfo(FieldName: "StudentName", FieldColumnNumber: 1, ShowField: true, ConvertReceivedDatabaseValueFunc:ToolsDatabaseExtensions.ConvertReceivedStringDatabaseValue),
            new DatabaseInfo(FieldName: "StudentAddress", FieldColumnNumber: 2, ShowField: false, ConvertReceivedDatabaseValueFunc:ToolsDatabaseExtensions.ConvertReceivedStringDatabaseValue),
            new DatabaseInfo(FieldName: "ClassID", FieldColumnNumber: 3, ShowField: true, ConvertReceivedDatabaseValueFunc:ToolsDatabaseExtensions.ConvertReceivedIntDatabaseValue),
            new DatabaseInfo(FieldName: "StudentNumberOfCourses", FieldColumnNumber: 4, ShowField: true, ConvertReceivedDatabaseValueFunc:ToolsDatabaseExtensions.ConvertReceivedIntDatabaseValue),
            new DatabaseInfo(FieldName: "StudentSumOfAllCharacters", FieldColumnNumber: 5, ShowField: true, ConvertReceivedDatabaseValueFunc:ToolsDatabaseExtensions.ConvertReceivedIntDatabaseValue)
        };

        private static List<DatabaseInfo> StudentListInfoFull = new List<DatabaseInfo>()
        {
            new DatabaseInfo(FieldName: "StudentID", FieldColumnNumber: 0, ShowField: true,  ConvertReceivedDatabaseValueFunc:ToolsDatabaseExtensions.ConvertReceivedIntDatabaseValue),
            new DatabaseInfo(FieldName: "StudentName", FieldColumnNumber: 1, ShowField: false, ConvertReceivedDatabaseValueFunc:ToolsDatabaseExtensions.ConvertReceivedStringDatabaseValue),
            new DatabaseInfo(FieldName: "StudentAddress", FieldColumnNumber: 2, ShowField: true, ConvertReceivedDatabaseValueFunc:ToolsDatabaseExtensions.ConvertReceivedStringDatabaseValue),
            new DatabaseInfo(FieldName: "StudentNumberOfCourses", FieldColumnNumber: 3, ShowField: true, ConvertReceivedDatabaseValueFunc:ToolsDatabaseExtensions.ConvertReceivedIntDatabaseValue),
            new DatabaseInfo(FieldName: "ClassName", FieldColumnNumber: 4, ShowField: true, ConvertReceivedDatabaseValueFunc:ToolsDatabaseExtensions.ConvertReceivedStringDatabaseValue),
            new DatabaseInfo(FieldName: "CourseName", FieldColumnNumber: 5, ShowField: true, ConvertReceivedDatabaseValueFunc:ToolsDatabaseExtensions.ConvertReceivedStringDatabaseValue)
        };

        private static SQLAndDatabaseInfo StudentListSQL = new SQLAndDatabaseInfo(DatabseInfoList: StudentListInfoFull, SQLCommandString: SqlString);

        private static SQLAndDatabaseInfo StudentListView = new SQLAndDatabaseInfo(DatabseInfoList: StudentListInfoFull, SQLCommandString: SQLStringView);

        private static SQLAndDatabaseInfo StudentList_SP = new SQLAndDatabaseInfo(DatabseInfoList: StudentListInfoFull, SQLCommandString: SQLString_SP);
        #endregion

        #region GET_SQL_Tables
        public static SQLAndDatabaseInfo GetSQLCommandAndFields()
        {
            return (StudentListSQL);
        }

        public static SQLAndDatabaseInfo GetSQLCommandAndFieldsView()
        {
            return (StudentListView);
        }

        public static SQLAndDatabaseInfo GetSQLCommandAndFields_SP()
        {
            return (StudentList_SP);
        }
        #endregion

        #region DatabaseConnection
        public static bool OpenDatabaseConnection()
        {
            try
            {
                // Build connection string
                Builder = new SqlConnectionStringBuilder();
                Builder.DataSource = "PCM15715";   // update me
                Builder.InitialCatalog = "h1pd080119";
                Builder.IntegratedSecurity = true;

                Connection = new SqlConnection(Builder.ConnectionString);

                // Connect to SQL
                ToolsOutput.PrintStringOnSeperateLine("Connecting to SQL Server ... ");
                
                Connection.Open();
                ToolsOutput.PrintStringOnSeperateLine("Done.");
                Connection.Close();
                
                return (true);
            }
            catch (SqlException e)
            {
                ToolsOutput.PrintStringOnSeperateLine(e.ToString());
                return (false);
            }
        }
        #endregion

        #region Database_Data_Functions
        public static void WatchStudentList(SQLAndDatabaseInfo SQLTable)
        {
            int Counter;
            string OutputString;

            Connection.Open();

            ToolsOutput.PrintStringOnSeperateLine(SQLTable.SQLCommandString);
            ToolsOutput.PrintStringOnSeperateLine("");

            using (SqlCommand Command = new SqlCommand(SQLTable.SQLCommandString, Connection))
            {
                OutputString = "";
                for (Counter = 0; Counter < SQLTable.DatabseInfoList.Count; Counter++)
                {
                    if (true == SQLTable.DatabseInfoList[Counter].ShowField)
                    {
                        OutputString += SQLTable.DatabseInfoList[Counter].FieldName + '\t';
                    }
                }
                ToolsOutput.PrintStringOnSeperateLine(OutputString);
                ToolsOutput.PrintStringOnSeperateLine("");

                using (SqlDataReader Reader = Command.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        OutputString = "";
                        for (Counter = 0; Counter < StudentListInfo.Count; Counter++)
                        {
                            if (true == StudentListSQL.DatabseInfoList[Counter].ShowField)
                            {
                                OutputString += SQLTable.DatabseInfoList[Counter].ConvertReceivedDatabaseValueFunc(Reader, SQLTable.DatabseInfoList[Counter].FieldColumnNumber) + '\t';
                            }
                        }
                        ToolsOutput.PrintStringOnSeperateLine(OutputString);
                    }
                }
            }

            Connection.Close();

            ToolsInput.WaitForUser();
        }
        #endregion
    }
    #endregion
}
