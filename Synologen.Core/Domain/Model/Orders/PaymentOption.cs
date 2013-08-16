namespace Spinit.Wpc.Synologen.Core.Domain.Model.Orders
{
	public class PaymentOption
	{
		public virtual PaymentOptionType Type { get; set; }
		public virtual int? SubscriptionId { get; set; }
	}
}