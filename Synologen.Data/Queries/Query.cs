using System;
using System.Linq.Expressions;
using NHibernate;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Persistence;
using IQuery = Spinit.Wpc.Synologen.Core.Domain.Persistence.IQuery;

namespace Spinit.Wpc.Synologen.Data.Queries
{
	public abstract class Query<TResult> : Query, IQuery<TResult>
	{
		protected Query() : base(typeof(TResult)) { }
		public abstract TResult Execute();
		public ISession Session { get; set; }
		protected string Property<T>(Expression<Func<T,object>> expression) where T : class
		{
			return expression.GetName();
		}
	}

	public abstract class Query : IQuery
	{
	    public Type ResultType { get; set; }
		protected Query(Type type)
	    {
	        ResultType = type;
	    }
	}


}