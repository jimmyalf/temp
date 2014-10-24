using Spinit.Wpc.Synologen.Core.Attributes;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes
{
	public enum SubscriptionConsentStatus
	{
		[EnumDisplayName("Kommer att överföras")]
		NotSent = 0,

		[EnumDisplayName("Överförd för godkännande")]
		Sent = 1,

		[EnumDisplayName("Medgivet")]
		Accepted = 2,

		[EnumDisplayName("Ansökan ej godkänd")]
		Denied = 3
	}
}