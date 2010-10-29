using Spinit.Wpc.Synologen.Core.Attributes;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription
{
	public enum TransactionReason
	{
		[EnumDisplayName("Inbetalning")]
		Payment = 1,

		[EnumDisplayName("Utbetalning")]
		Withdrawal = 2,

		[EnumDisplayName("Korrigering")]
		Correction = 3
	}
}