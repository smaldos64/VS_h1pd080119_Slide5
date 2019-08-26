using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace Slide5.Tools
{
    public class ToolsDatabaseExtensions
    {
        public static string ConvertReceivedStringDatabaseValue(SqlDataReader Reader, int ColumnNumber)
        {
            if (!Reader.IsDBNull(ColumnNumber))
            {
                return (Reader.GetString(ColumnNumber));
            }
            return "NULL ";
        }

        public static string ConvertReceivedIntDatabaseValue(SqlDataReader Reader, int ColumnNumber)
        {
            if (!Reader.IsDBNull(ColumnNumber))
            {
                return (Reader.GetInt32(ColumnNumber).ToString());
            }
            return "NULL ";
        }

        public static string ConvertReceivedFloatDatabaseValue(SqlDataReader Reader, int ColumnNumber)
        {
            if (!Reader.IsDBNull(ColumnNumber))
            {
                return (Reader.GetFloat(ColumnNumber).ToString());
            }
            return "NULL ";
        }
    }
}
