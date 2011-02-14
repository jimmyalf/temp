using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Spinit.Wpc.Synologen.LensSubscription.BGData.Test.CommonDataTestHelpers
{
    public static class DataHelper
    {
        public static void ExecuteStatement(SqlConnection sqlConnection, string sqlStatement)
        {
            var transaction = sqlConnection.BeginTransaction();
            using (var cmd = sqlConnection.CreateCommand())
            {
                cmd.Connection = sqlConnection;
                cmd.Transaction = transaction;

                cmd.CommandText = sqlStatement;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
            transaction.Commit();
        }

        public static void DeleteAndResetIndexForTable(SqlConnection sqlConnection, string tableName)
        {
            ExecuteStatement(sqlConnection, String.Format("DELETE FROM {0}", tableName));
            ExecuteStatement(sqlConnection, String.Format("DBCC CHECKIDENT ({0}, reseed, 0)", tableName));
        }

        public static void DeleteForTable(SqlConnection sqlConnection, string tableName)
        {
            ExecuteStatement(sqlConnection, String.Format("DELETE FROM {0}", tableName));
        }

        public static string ConnectionString
        {
            get
            {
                const string connectionStringname = "BGServer";
                return ConfigurationManager.ConnectionStrings[connectionStringname].ConnectionString;
            }
        }

        public static TType Parse<TType>(this DataRow row, string columnName)
        {
            return (TType)row[columnName];
        }

        public static TType? ParseNullable<TType>(this DataRow row, string columnName)
            where TType : struct
        {
            if (row.IsNull(columnName)) return null;
            return (TType?)row[columnName];
        }
    }
}
