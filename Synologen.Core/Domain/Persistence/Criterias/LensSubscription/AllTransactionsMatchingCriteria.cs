using Spinit.Data;

namespace Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription
{
	public class AllTransactionsMatchingCriteria : IActionCriteria {
		public bool ConnectedToSettlement { get; set; }
	}
}