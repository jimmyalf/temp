using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq.Expressions;
using NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Persistence;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Data.Queries;

namespace Spinit.Wpc.Synologen.Data.Commands
{
	[DebuggerStepThrough]
	public abstract class Command : ICommand
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
			var command = Session.Connection.CreateCommand();
			Session.Transaction.Enlist(command);
			command.CommandText = sqlStatement;
			command.CommandType = CommandType.Text;
			foreach (var property in parameters.ToProperties())
			{
				command.Parameters.Add(new SqlParameter("@" + property.Key, property.Value));
			}
			command.ExecuteNonQuery();
		}

		protected string Property<TResult>(Expression<Func<TResult,object>> expression) where TResult : class
		{
			return expression.GetName();
		}
	}

	[DebuggerStepThrough]
	public abstract class Command<TResult> : Command, ICommand<TResult>
	{
		public TResult Result { get; protected set; }
	}
}