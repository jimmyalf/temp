using NHibernate;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.BGServer;

namespace Synologen.LensSubscription.BGData.CriteriaConverters
{
	public class AllNewReceivedBGConsentsMatchingServiceTypeCriteriaConverter : NHibernateActionCriteriaConverter<AllNewReceivedBGConsentsMatchingServiceTypeCriteria, BGReceivedConsent>
	{
		public AllNewReceivedBGConsentsMatchingServiceTypeCriteriaConverter(ISession session) : base(session) {}
		public override ICriteria Convert(AllNewReceivedBGConsentsMatchingServiceTypeCriteria source) 
		{
		return Criteria
				.CreateAlias(x => x.Payer)
				.FilterEqual(x => x.Payer.ServiceType, source.ServiceType)
				.FilterEqual(x => x.Handled, false);
		}
	}
}