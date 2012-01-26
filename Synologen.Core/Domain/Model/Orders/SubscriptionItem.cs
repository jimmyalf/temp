namespace Spinit.Wpc.Synologen.Core.Domain.Model.Orders
{
	public class SubscriptionItem : Entity
	{
	    public virtual string Description { get; set; }
		public virtual Subscription Subscription { get; set; }
		public virtual int? NumberOfPayments { get; set; }
		public virtual int? NumberOfPaymentsLeft { get; set; }
		public virtual decimal TaxedAmount { get; set; }
		public virtual decimal TaxFreeAmount { get; set; }
		public virtual string Notes { get; set; }
		public virtual decimal AmountForAutogiroWithdrawal { get { return TaxedAmount + TaxFreeAmount; } }
	}
}