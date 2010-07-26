using System;
using NHibernate;
using NHibernate.Criterion;
using Spinit.Wpc.Synologen.Core.Domain.Model;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias;
using Spinit.Wpc.Synologen.Core.Persistence;
using Spinit.Wpc.Synologen.Data.Repositories.NHibernate;

namespace Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters
{
	public class PageOfFramesMatchingCriteriaConverter : IActionCriteriaConverter<PageOfFramesMatchingCriteria, ICriteria> {
		private readonly ISession _session;
		public PageOfFramesMatchingCriteriaConverter(ISession session) { _session = session; }

		public ICriteria Convert(PageOfFramesMatchingCriteria source)
		{
			return _session
				.CreateCriteria<Frame>()
				//.Add(Restrictions.Gt("Id", source.IdGreaterThen))
				.Add(Restrictions.InsensitiveLike("Name", String.Format("%{0}%", source.NameLike)))
				.Page(source.Page, source.PageSize);
		}
	}
}