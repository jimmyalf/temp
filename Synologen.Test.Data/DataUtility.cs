using System;
using System.Configuration;
using System.Data;
using Spinit.Extensions;
using Spinit.Wpc.Base.Data;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Data;

namespace Spinit.Wpc.Synogen.Test.Data
{
	public class DataUtility
	{
		private string _connectionString;
		public virtual string ConnectionString
		{
			get { 
				return _connectionString 
				?? (_connectionString = ConfigurationManager.ConnectionStrings["WpcServer"].ConnectionString);
			}
		}

		private ISqlProvider _sqlProvider;
		public virtual ISqlProvider GetSqlProvider(string connectionstring = null)
		{
			return _sqlProvider ?? (_sqlProvider =  new SqlProvider(connectionstring ?? ConnectionString));
		}

		private User _userRepository;
		public virtual User GetUserRepository(string connectionString = null)
		{
			return _userRepository ?? (_userRepository = new User(connectionString ?? ConnectionString));
		}

		protected virtual void ExecuteStatement(IDbConnection sqlConnection, string sqlStatement)
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

		protected virtual void DeleteAndResetIndexForTable(IDbConnection sqlConnection, string tableName)
		{
			ExecuteStatement(sqlConnection, String.Format("DELETE FROM {0}", tableName));
			var expression = "IF NOT EXISTS(select * FROM SYS.IDENTITY_COLUMNS JOIN SYS.TABLES ON SYS.IDENTITY_COLUMNS.Object_ID = SYS.TABLES.Object_ID WHERE SYS.TABLES.Name = '{TableName}' AND SYS.IDENTITY_COLUMNS.Last_Value IS NULL) DBCC CHECKIDENT ({TableName}, RESEED, 0)".ReplaceWith(new {TableName = tableName});
			ExecuteStatement(sqlConnection, expression);
			//ExecuteStatement(sqlConnection, String.Format("DBCC CHECKIDENT ({0}, reseed, 0)", tableName));
		}

		protected virtual void DeleteForTable(IDbConnection sqlConnection, string tableName)
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