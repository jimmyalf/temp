using Spinit.Wpc.Synologen.Core.Attributes;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription
{
	public enum SubscriptionStatus 
	{
		[EnumDisplayName("Skapad")]
		Created = 1,

		[EnumDisplayName("Aktiv")]
		Active = 2,

		[EnumDisplayName("Stoppad")]
		Stopped = 3,

		[EnumDisplayName("Utgången")]
		Expired = 4,
	}
}