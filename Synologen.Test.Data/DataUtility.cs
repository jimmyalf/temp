using System;
using System.Configuration;
using System.Data;
using Spinit.Wpc.Base.Data;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Data;

namespace Spinit.Wpc.Synogen.Test.Data
{
	public class DataUtility
	{
		public virtual string ConnectionString
		{
			get { return ConfigurationManager.ConnectionStrings["WpcServer"].ConnectionString; }
		}

		public virtual ISqlProvider GetSqlProvider(string connectionstring = null)
		{
			return new SqlProvider(connectionstring ?? ConnectionString);
		}

		public virtual User GetUserRepository(string connectionString = null)
		{
			return new User(connectionString ?? ConnectionString);
		}

		public virtual void ExecuteStatement(IDbConnection sqlConnection, string sqlStatement)
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

		public virtual void DeleteAndResetIndexForTable(IDbConnection sqlConnection, string tableName)
		{
			ExecuteStatement(sqlConnection, String.Format("DELETE FROM {0}", tableName));
			ExecuteStatement(sqlConnection, String.Format("DBCC CHECKIDENT ({0}, reseed, 0)", tableName));
		}

		public virtual void DeleteForTable(IDbConnection sqlConnection, string tableName)
		{
			ExecuteStatement(sqlConnection, String.Format("DELETE FROM {0}", tableName));
		}

		public virtual bool IsDevelopmentServer(string connectionString)
		{
			if(connectionString.ToLower().Contains("black")) return true;
			if(connectionString.ToLower().Contains("dev")) return true;
			if(connectionString.ToLower().Contains("localhost")) return true;
			if(connectionString.ToLower().Contains(@".\")) return true;
			return false;
		}
	}
}