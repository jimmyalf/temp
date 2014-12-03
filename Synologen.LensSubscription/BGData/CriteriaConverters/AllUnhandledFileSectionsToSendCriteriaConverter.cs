using NHibernate;
using NHibernate.Criterion;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.BGServer;

namespace Synologen.LensSubscription.BGData.CriteriaConverters
{
	public class AllUnhandledFileSectionsToSendCriteriaConverter : NHibernateActionCriteriaConverter<AllUnhandledFileSectionsToSendCriteria,FileSectionToSend>
	{
		public AllUnhandledFileSectionsToSendCriteriaConverter(ISession session) : base(session) {}
		public override ICriteria Convert(AllUnhandledFileSectionsToSendCriteria source) 
		{
			return Criteria.Add(Restrictions.IsNull(Property(x => x.SentDate)));
		}
	}
}