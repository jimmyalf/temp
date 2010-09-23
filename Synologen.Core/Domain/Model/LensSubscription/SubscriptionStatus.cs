namespace Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription
{
	public class SubscriptionStatus : Entity
	{
		public string Name { get; set; }
		public bool AutomaticPaymentEnabled { get; set; }
	}
}