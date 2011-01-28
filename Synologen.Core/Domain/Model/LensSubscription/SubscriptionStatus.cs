using Spinit.Wpc.Synologen.Core.Attributes;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription
{
	public enum SubscriptionStatus 
	{
		[EnumDisplayName("Stoppad")]
		Stopped = 1,

		[EnumDisplayName("Startad")]
		Started = 2,
	}
}