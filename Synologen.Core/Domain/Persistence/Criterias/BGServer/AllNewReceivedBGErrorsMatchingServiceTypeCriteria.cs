using Spinit.Data;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro;

namespace Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.BGServer
{
	public class AllNewReceivedBGErrorsMatchingServiceTypeCriteria : IActionCriteria
	{
		public AllNewReceivedBGErrorsMatchingServiceTypeCriteria(AutogiroServiceType serviceType)
		{
			ServiceType = serviceType;
		}

		public AutogiroServiceType ServiceType { get; private set; }
	}
}