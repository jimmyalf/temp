using System;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription
{
	public class SubscriptionPaymentInfo
	{
		public virtual string ClearingNumber { get; set; }
		public virtual string AccountNumber { get; set; }
		public virtual decimal MonthlyAmount { get; set; }
		public virtual DateTime? PaymentSentDate { get; set; }

		public override bool Equals(object obj)
		{
			var entity = obj as SubscriptionPaymentInfo;
			return Equals(entity);
		}

		public virtual bool Equals(SubscriptionPaymentInfo other)
		{
			return other != null 
				&& Equals(ClearingNumber, other.ClearingNumber)
				&& Equals(AccountNumber, other.AccountNumber)
				&& Equals(MonthlyAmount, other.MonthlyAmount);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				int result = (ClearingNumber != null ? ClearingNumber.GetHashCode() : 0);
				result = (result * 397) ^ (AccountNumber != null ? AccountNumber.GetHashCode() : 0);
				result = (result * 397) ^ MonthlyAmount.GetHashCode();
				result = (result * 397) ^ (PaymentSentDate.HasValue ? PaymentSentDate.Value.GetHashCode() : 0);
				return result;
			}
		}
	}
}