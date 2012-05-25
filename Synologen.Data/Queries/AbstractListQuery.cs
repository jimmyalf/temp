using System;
using System.Collections;
using System.Collections.Generic;
using NHibernate;
using Spinit.Data.NHibernate;

namespace Spinit.Wpc.Synologen.Data.Queries
{
	public abstract class AbstractListQuery<TType> : Query<IEnumerable<TType>> where TType : class
	{
		protected ICriteria<TType> Criteria { get; set; }
		public Func<ICriteria<TType>, ICriteria> CustomCriteria { get; set; }

		protected AbstractListQuery(ICriteria<TType> criteria)
		{
			Criteria = criteria;
		}
		protected ICriteria GetCriteria()
		{
			var criteria = Criteria ?? Session.CreateCriteriaOf<TType>();
			return CustomCriteria != null ? CustomCriteria(criteria) : criteria;
		}

		protected virtual IEnumerable<TType> GetResultWithLength(ICriteria criteria, out long count)
		{
			var countCriteria = ((ICriteria) criteria.Clone()).ToCountCriteria();
			
			var multiResults = Session.CreateMultiCriteria()
				.Add(criteria)
				.Add(countCriteria)
				.List();

			count = (long)((IList)multiResults[1])[0];
			
			return (IEnumerable<TType>) ((ArrayList)multiResults[0]).ToArray(typeof(TType));
		}
	}
}