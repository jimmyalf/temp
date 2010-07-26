using System;
using NHibernate;
using NHibernate.Criterion;
using Spinit.Wpc.Synologen.Core.Domain.Model;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias;
using Spinit.Wpc.Synologen.Core.Persistence;

namespace Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters
{
	public class AllFramesMatchingCriteriaConverter : IActionCriteriaConverter<AllFramesMatchingCriteria, ICriteria> {
		private readonly ISession _session;
		public AllFramesMatchingCriteriaConverter(ISession session) { _session = session; }

		public ICriteria Convert(AllFramesMatchingCriteria source)
		{
			return _session
				.CreateCriteria<Frame>()
				//.Add(Restrictions.Gt("Id", source.IdGreaterThen))
				.Add(Restrictions.InsensitiveLike("Name", String.Format("%{0}%", source.NameLike)));
		}
	}
}