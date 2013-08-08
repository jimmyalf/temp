using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;

namespace Spinit.Wpc.Synologen.Presentation.Test.Factories.LensSubscription
{
	public static class SubscriptionTransactionFactory
	{
		public static SubscriptionTransaction Get()
		{
			return new SubscriptionTransaction
			{
				Amount = 588.65M,
				CreatedDate = new DateTime(2010, 08, 25),
				Reason = TransactionReason.Payment,
				Subscription = new Subscription(),
				Type = TransactionType.Deposit
			};
		}

		public static SubscriptionTransaction Get(decimal amount, DateTime createdDate, TransactionReason reason, TransactionType type)
		{
			return new SubscriptionTransaction
			{
				Amount = amount,
				CreatedDate = createdDate,
				Reason = reason,
				Subscription = new Subscription(),
				Type = type
			};
		}

		public static IEnumerable<SubscriptionTransaction> GetList()
		{
			return new []
			{
				Get(588.65M, new DateTime(2010, 08, 25), TransactionReason.Payment, TransactionType.Deposit),
				Get(588.65M, new DateTime(2010, 09, 25), TransactionReason.Payment, TransactionType.Deposit),
				Get(588.65M, new DateTime(2010, 10, 25), TransactionReason.Payment, TransactionType.Deposit),
				Get(5.50M, new DateTime(2010, 10, 25), TransactionReason.Correction, TransactionType.Deposit),
				Get(955, new DateTime(2010, 10, 25), TransactionReason.Withdrawal, TransactionType.Withdrawal),
				Get(955, new DateTime(2010, 11, 5), TransactionReason.PaymentFailed, TransactionType.Deposit),
			};
		}
	}
}