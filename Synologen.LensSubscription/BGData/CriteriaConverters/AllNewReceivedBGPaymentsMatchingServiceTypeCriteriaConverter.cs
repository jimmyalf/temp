using NHibernate;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.BGServer;

namespace Synologen.LensSubscription.BGData.CriteriaConverters
{
	public class AllNewReceivedBGPaymentsMatchingServiceTypeCriteriaConverter : NHibernateActionCriteriaConverter<AllNewReceivedBGPaymentsMatchingServiceTypeCriteria,BGReceivedPayment>
	{
		public AllNewReceivedBGPaymentsMatchingServiceTypeCriteriaConverter(ISession session) : base(session) {}

		public override ICriteria Convert(AllNewReceivedBGPaymentsMatchingServiceTypeCriteria source)
		{
			return Criteria
				.CreateAlias(x => x.Payer)
				.FilterEqual(x => x.Payer.ServiceType, source.ServiceType)
				.FilterEqual(x => x.Handled, false);
		}
	}
}