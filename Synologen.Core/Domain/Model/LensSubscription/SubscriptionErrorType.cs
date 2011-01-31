using Spinit.Wpc.Synologen.Core.Attributes;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription
{
	public enum SubscriptionErrorType
	{

		[EnumDisplayName("Täckning saknades")]
		NoCoverage = 1,

		[EnumDisplayName("AG-koppling saknas (kontot avslutat eller medgivandet makulerat)")]
		NoAccount = 2,

		[EnumDisplayName("Utgår, medgivande saknas)")]
		ConsentMissing = 3,

		[EnumDisplayName("Utgår, konto ej godkänt")]
		NotApproved = 4,

		[EnumDisplayName("Utgår, medgivande stoppat")]
		CosentStopped = 5,

		[EnumDisplayName("Avvisad, ännu ej debiterbar")]
		NotDebitable = 6
	}
}
