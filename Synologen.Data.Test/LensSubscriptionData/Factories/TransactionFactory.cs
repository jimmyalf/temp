using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;

namespace Spinit.Wpc.Synologen.Integration.Data.Test.LensSubscriptionData.Factories
{
	public static class TransactionFactory
	{

		public static SubscriptionTransaction Get(Subscription subscription)
		{
			return Get(subscription, TransactionType.Withdrawal, TransactionReason.Withdrawal);
		}

		public static SubscriptionTransaction Get(Subscription subscription, TransactionType type, TransactionReason reason)
		{
			return Get(350.50M, new DateTime(2010, 10, 27), subscription, type, reason);
		}

		public static SubscriptionTransaction Get(decimal amount, DateTime createdDate, Subscription subscription, TransactionType type, TransactionReason reason)
		{
			return new SubscriptionTransaction
			{
				Amount = amount,
				CreatedDate = createdDate,
				Reason = reason,
				Type = type,
				Subscription = subscription
			};
		}

		public static IList<SubscriptionTransaction> GetList(Subscription subscription)
		{
			return new[]
			{
				Get(100.10M, new DateTime(2010, 10, 27), subscription, TransactionType.Deposit, TransactionReason.Correction),
				Get(200.20M, new DateTime(2010, 10, 26), subscription, TransactionType.Deposit, TransactionReason.Payment),
				Get(300.30M, new DateTime(2010, 10, 25), subscription, TransactionType.Withdrawal, TransactionReason.Withdrawal),
				Get(400.45M, new DateTime(2010, 10, 24), subscription, TransactionType.Deposit, TransactionReason.PaymentFailed),
			};
		}

	}
}
