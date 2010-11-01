using Spinit.Wpc.Synologen.Core.Attributes;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription
{
	public enum TransactionReason
	{
		[EnumDisplayName("Insättning")]
		Payment = 1,

		[EnumDisplayName("Uttag")]
		Withdrawal = 2,

		[EnumDisplayName("Korrigering")]
		Correction = 3
	}
}