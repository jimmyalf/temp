using Spinit.Wpc.Synologen.Core.Attributes;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes
{
	public enum SubscriptionConsentStatus
	{
		[EnumDisplayName("Kommer att �verf�ras")]
		NotSent = 0,

		[EnumDisplayName("�verf�rd f�r godk�nnande")]
		Sent = 1,

		[EnumDisplayName("Medgivet")]
		Accepted = 2,

		[EnumDisplayName("Ans�kan ej godk�nd")]
		Denied = 3
	}
}