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
			return entity != null 
				&& Equals(ClearingNumber, entity.ClearingNumber)
				&& Equals(AccountNumber, entity.AccountNumber)
				&& Equals(MonthlyAmount, entity.MonthlyAmount);
		}
	}
}