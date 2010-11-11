using System;
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
			return new SubscriptionTransaction
			       	{
			       		Amount = 350.50M,
						CreatedDate = new DateTime(2010, 10, 27),
						Reason = reason,
						Type = type,
						Subscription = subscription
			       	};
		}

		public static SubscriptionTransaction[] GetList(Subscription subscription)
		{
			return new[]
			{
				new SubscriptionTransaction
				{
					Amount = 100.10M,
					CreatedDate = new DateTime(2010, 10, 24),
					Reason = TransactionReason.Correction,
					Type = TransactionType.Deposit,
					Subscription = subscription
				}, new SubscriptionTransaction
				{
					Amount = 200.20M,
					CreatedDate = new DateTime(2010, 10, 25),
					Reason = TransactionReason.Payment,
					Type = TransactionType.Deposit,
					Subscription = subscription
				}, new SubscriptionTransaction
				{
					Amount = 300.30M,
					CreatedDate = new DateTime(2010, 10, 26),
					Reason = TransactionReason.Withdrawal,
					Type = TransactionType.Withdrawal,
					Subscription = subscription
				}, new SubscriptionTransaction
				{
					Amount = 400.45M,
					CreatedDate = new DateTime(2010, 10, 27),
					Reason = TransactionReason.PaymentFailed,
					Type = TransactionType.Deposit,
					Subscription = subscription
				}
			};
		}

	}
}
