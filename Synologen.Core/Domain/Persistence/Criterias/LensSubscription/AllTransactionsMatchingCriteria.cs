using Spinit.Data;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;

namespace Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription
{
	public class AllTransactionsMatchingCriteria : IActionCriteria 
	{
		public SettlementStatus SettlementStatus { get; set; }
		public TransactionType? Type { get; set; }
		public TransactionReason? Reason { get; set; }
		
	}

	public enum SettlementStatus
	{
		Any = 1,
		HasSettlement = 2,
		DoesNotHaveSettlement = 3
	}
}