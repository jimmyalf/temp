using System;
using System.Collections.Generic;
using System.Linq;
using Spinit.Wpc.Synologen.Core.Domain.Services;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.Orders
{
	public class SubscriptionPendingPayment : Entity
	{
		public SubscriptionPendingPayment()
		{
			Created = SystemTime.Now;
			SubscriptionItemAmounts = new List<SubscriptionPendingPaymentAmount>();
		}

		protected virtual SubscriptionAmount Amount { get; set; }
		protected virtual IList<SubscriptionPendingPaymentAmount> SubscriptionItemAmounts { get; set; }

		public virtual SubscriptionPendingPayment AddSubscriptionItem(SubscriptionItem item)
		{
			SubscriptionItemAmounts.Add(new SubscriptionPendingPaymentAmount
			{
				Amount = item.MonthlyWithdrawal,
				PendingPayment = this,
				SubscriptionItem = item
			});
			return this;
		}
		public virtual SubscriptionPendingPayment AddSubscriptionItems(IEnumerable<SubscriptionItem> items)
		{
			foreach (var subscriptionItem in items)
			{
				AddSubscriptionItem(subscriptionItem);
			}
			return this;
		}

		public virtual bool HasBeenPayed { get; set; }
		public virtual DateTime Created { get; private set; }

		public virtual IEnumerable<SubscriptionItem> GetSubscriptionItems()
		{
			return SubscriptionItemAmounts.Select(subscriptionPendingPaymentAmount => subscriptionPendingPaymentAmount.SubscriptionItem);
		}

		public virtual IEnumerable<SubscriptionPendingPaymentAmount> GetSubscriptionItemAmounts(Func<SubscriptionPendingPaymentAmount,bool> predicate = null)
		{
			return predicate == null ? SubscriptionItemAmounts : SubscriptionItemAmounts.Where(predicate);
		}

		public virtual SubscriptionAmount GetValue(IEnumerable<SubscriptionPendingPaymentAmount> amounts = null)
		{
			if (Amount != null) return Amount;
			var items = amounts ?? SubscriptionItemAmounts;
			return items.Select(x => x.Amount).Sum();
		}
	}
}