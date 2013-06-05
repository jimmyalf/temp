namespace Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes
{
	public class SubscriptionVersion : Enumeration<SubscriptionVersion>
	{
		public static SubscriptionVersion VersionOne = new SubscriptionVersion(1, "Version 1");
		public static SubscriptionVersion VersionTwo = new SubscriptionVersion(2, "Version 2");

		protected SubscriptionVersion(int value, string displayName) : base(value,displayName) { }
	}
}