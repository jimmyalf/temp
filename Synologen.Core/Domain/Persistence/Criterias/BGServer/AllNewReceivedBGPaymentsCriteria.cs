using Spinit.Data;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;

namespace Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.BGServer
{
	public class AllNewReceivedBGPaymentsCriteria : IActionCriteria
	{
		public AllNewReceivedBGPaymentsCriteria(AutogiroServiceType serviceType)
		{
			ServiceType = serviceType;
		}

		public AutogiroServiceType ServiceType { get; private set; }
	}
}