using Spinit.Wpc.Synologen.Core.Attributes;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes
{
	public enum TransactionType
	{
		[EnumDisplayName("Inbetalning")]
		Deposit = 1,

		[EnumDisplayName("Utbetalning")]
		Withdrawal = 2
	}
}