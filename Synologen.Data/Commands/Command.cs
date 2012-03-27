using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using NHibernate;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Data.Queries;

namespace Spinit.Wpc.Synologen.Data.Commands
{
	[DebuggerStepThrough]
	public abstract class Command
	{
		public abstract void Execute();
		public ISession Session { get; set; }

		protected TResult Query<TResult>(Query<TResult> query)
		{
			query.Session = Session;
			return query.Execute();
		}

		protected void ExecuteCustomCommand(string sqlStatement, object parameters)
		{
			using (var transaction = Session.Connection.BeginTransaction())
			{
				var command = transaction.Connection.CreateCommand();
				command.CommandText = sqlStatement;
				command.CommandType = CommandType.Text;
				command.Transaction = transaction;
				foreach (var property in parameters.ToProperties())
				{
					command.Parameters.Add(new SqlParameter("@" + property.Key, property.Value));
				}
				command.ExecuteNonQuery();
				transaction.Commit();
			}
		}
	}

	[DebuggerStepThrough]
	public abstract class Command<TResult> : Command
	{
		public TResult Result { get; protected set; }
	}
}