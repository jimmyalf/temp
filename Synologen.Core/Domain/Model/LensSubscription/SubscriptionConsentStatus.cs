using Spinit.Wpc.Synologen.Core.Attributes;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription
{
	public enum SubscriptionConsentStatus
	{
		[EnumDisplayName("Ej skickat")]
		NotSent = 0,

		[EnumDisplayName("Skickat")]
		Sent = 1,

		[EnumDisplayName("Medgivet")]
		Accepted = 2,

		[EnumDisplayName("Ej medgivet")]
		Denied = 3
	}
}