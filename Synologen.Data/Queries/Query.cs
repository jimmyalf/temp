using System;
using System.Linq.Expressions;
using NHibernate;
using Spinit.Extensions;

namespace Spinit.Wpc.Synologen.Data.Queries
{
	public abstract class Query<TResult> : Query
	{
		protected Query() : base(typeof(TResult)) { }
		public abstract TResult Execute();
		public ISession Session { get; set; }
		protected string Property<T>(Expression<Func<T,object>> expression) where T : class
		{
			return expression.GetName();
		}
	}

	public abstract class Query
	{
		public Type Type { get; set; }

		public Query(Type type)
		{
			Type = type;
		}
	}
}