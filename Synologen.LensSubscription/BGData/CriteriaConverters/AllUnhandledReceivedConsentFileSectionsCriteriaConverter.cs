using System;
using NHibernate;
using NHibernate.Criterion;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.BGServer;

namespace Synologen.LensSubscription.BGData.CriteriaConverters
{
	public class AllUnhandledReceivedConsentFileSectionsCriteriaConverter 
		: NHibernateActionCriteriaConverter<AllUnhandledReceivedConsentFileSectionsCriteria, ReceivedFileSection>
	{
		public AllUnhandledReceivedConsentFileSectionsCriteriaConverter(ISession session) : base(session) { }

		public override ICriteria Convert(AllUnhandledReceivedConsentFileSectionsCriteria source)
		{
			return Criteria
				.Add(Restrictions.IsNull(Property(x => x.HandledDate)))
				.Sort(Property(x => x.CreatedDate), true);
		}
	}
}