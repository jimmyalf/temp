namespace Spinit.Wpc.Synologen.Core.Domain.Model.Orders
{
	public class SubscriptionPendingPaymentAmount
	{
		public virtual SubscriptionItem SubscriptionItem { get; set; }
		public virtual SubscriptionPendingPayment PendingPayment { get; set; }
		public virtual SubscriptionAmount Amount { get; set; }

		public virtual bool Equals(SubscriptionPendingPaymentAmount other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Equals(other.SubscriptionItem.Id, SubscriptionItem.Id) && Equals(other.PendingPayment.Id, PendingPayment.Id);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof (SubscriptionPendingPaymentAmount)) return false;
			return Equals((SubscriptionPendingPaymentAmount) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return ((SubscriptionItem != null ? SubscriptionItem.GetHashCode() : 0) * 397) ^ (PendingPayment != null ? PendingPayment.GetHashCode() : 0);
			}
		}
	}
}