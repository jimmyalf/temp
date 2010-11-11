using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;

namespace Spinit.Wpc.Synologen.Presentation.Site.Test.LensSubscriptionTests.Factories
{
	public static class TransactionFactory
	{
		public static SubscriptionTransaction[] GetList(Subscription subscription)
		{

			return new[]
			       	{
			       		new SubscriptionTransaction
			       			{
			       				Amount = 100.10M,
			       				CreatedDate = new DateTime(2010, 10, 24),
			       				Reason = TransactionReason.Correction,
			       				Type = TransactionType.Deposit
			       			},
			       		new SubscriptionTransaction
			       			{
			       				Amount = 200.20M,
			       				CreatedDate = new DateTime(2010, 10, 25),
			       				Reason = TransactionReason.Payment,
			       				Type = TransactionType.Withdrawal
			       			},
			       		new SubscriptionTransaction
			       			{
			       				Amount = 300.30M,
			       				CreatedDate = new DateTime(2010, 10, 26),
			       				Reason = TransactionReason.Withdrawal,
			       				Type = TransactionType.Deposit
			       			},
						new SubscriptionTransaction
			       			{
			       				Amount = 85.90M,
			       				CreatedDate = new DateTime(2010, 11, 03),
			       				Reason = TransactionReason.PaymentFailed,
			       				Type = TransactionType.Deposit
			       			}
			       	};

		}
	}
}
