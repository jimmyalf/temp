using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Spinit.Wpc.Synologen.Integration.Test.CommonDataTestHelpers
{
	public static  class DataHelper
	{
		public static void ExecuteStatement(SqlConnection sqlConnection, string sqlStatement)
		{
			var transaction = sqlConnection.BeginTransaction();
			using (var cmd = sqlConnection.CreateCommand()) {
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

		public static string ConnectionString{
			get
			{
				const string connectionStringname = "WpcServer";
				return ConfigurationManager.ConnectionStrings[connectionStringname].ConnectionString;
			}
		}
	}
}