using NHibernate;
using NHibernate.Criterion;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.BGServer;

namespace Synologen.LensSubscription.BGData.CriteriaConverters
{
	public class AllNewConsentsToSendCriteriaConverter : NHibernateActionCriteriaConverter<AllNewConsentsToSendCriteria,BGConsentToSend>
	{
		public AllNewConsentsToSendCriteriaConverter(ISession session) : base(session) {}

		public override ICriteria Convert(AllNewConsentsToSendCriteria source)
		{
			return Criteria.Add(Restrictions.IsNull(Property(x => x.SendDate)));
		}
	}
}