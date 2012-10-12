using System;
using System.Collections.Generic;
using System.Linq;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.Orders
{
	public class Subscription : Entity
	{
		public Subscription()
		{
			CreatedDate = SystemTime.Now;
		}
		public virtual OrderCustomer Customer { get; set; }
		public virtual Shop Shop { get; set; }
		public virtual string BankAccountNumber { get; set; }
		public virtual string ClearingNumber { get; set; }
		public virtual int? AutogiroPayerId { get; set; }
		public virtual IEnumerable<SubscriptionItem> SubscriptionItems { get; set; }
		public virtual IEnumerable<SubscriptionTransaction> Transactions { get; set; }
		public virtual IEnumerable<SubscriptionError> Errors { get; set; }
		public virtual SubscriptionConsentStatus ConsentStatus { get; set; }
		public virtual DateTime CreatedDate { get; protected set; }
		public virtual DateTime? ConsentedDate { get; set; }
		public virtual bool Active { get; set; }
		public virtual DateTime? LastPaymentSent { get; set; }

		public static SubscriptionAmount GetCurrentAccountBalance(IList<SubscriptionTransaction> transactions)
		{
			var taxedAmount = GetBalanceFor(transactions, x => x.GetAmount().Taxed);
			var taxFreeAmount = GetBalanceFor(transactions, x => x.GetAmount().TaxFree);
			return new SubscriptionAmount(taxedAmount, taxFreeAmount);
		}

		public virtual SubscriptionAmount GetCurrentAccountBalance()
		{
			return GetCurrentAccountBalance(Transactions.OrEmpty().ToList());
		}

		protected static decimal GetBalanceFor(IList<SubscriptionTransaction> transactions, Func<SubscriptionTransaction,decimal> amountSelector)
		{
			if (transactions == null || !transactions.Any()) return 0;
			Func<SubscriptionTransaction, bool> isWithdrawal = transaction => (transaction.Reason == TransactionReason.Withdrawal || transaction.Reason == TransactionReason.Correction) && transaction.Type == TransactionType.Withdrawal;
			Func<SubscriptionTransaction, bool> isDeposit = transaction => (transaction.Reason == TransactionReason.Payment || transaction.Reason == TransactionReason.Correction) && transaction.Type == TransactionType.Deposit;
			var withdrawals = transactions.Where(isWithdrawal).Sum(amountSelector);
			var deposits = transactions.Where(isDeposit).Sum(amountSelector);
			return deposits - withdrawals;
		}
	}
}