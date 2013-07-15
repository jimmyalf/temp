using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace Synologen.Maintenance.MigrateSubscriptionAmounts.Persistence
{
	public class PersistenceBase : Spinit.Data.SqlClient.PersistenceBase
	{
		public PersistenceBase() : base(ConnectionString) {}


		public static void RunInTransaction(Action<SqlTransaction> transactedAction)
		{
			_sqlConnection = null;
			_transaction = null;
			_sqlConnection = GetConnection();
			transactedAction(_transaction);
		}

		public override void Execute(string sqlStatement, IEnumerable<SqlParameter> parameters)
		{
			if (_transaction == null)
			{
				base.Execute(sqlStatement, parameters);
			}
			else
			{
				var command = CreateCommand(sqlStatement, Connection, parameters);
				command.Transaction = _transaction;
				command.ExecuteNonQuery();
			}
		}
	
		protected static string ConnectionString
		{
			get { return ConfigurationManager.ConnectionStrings["WpcServer"].ConnectionString; }
		}

		private static SqlConnection _sqlConnection;
		private static SqlConnection Connection
		{
			get { return _sqlConnection ?? (_sqlConnection = GetConnection()); }
		}

		private static SqlTransaction _transaction;
		private static SqlConnection GetConnection()
		{
			var connection = new SqlConnection(ConnectionString);
			connection.Open();
			_transaction = connection.BeginTransaction("PersistenceBaseTransaction");
			return connection;
		}
	}
}
