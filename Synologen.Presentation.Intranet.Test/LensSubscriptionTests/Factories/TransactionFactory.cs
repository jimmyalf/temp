using System;
using System.Collections.Generic;
using Moq;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Test.LensSubscriptionTests.Factories
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
		       				Amount = 150.10M,
		       				CreatedDate = new DateTime(2010, 10, 23),
		       				Reason = TransactionReason.Correction,
		       				Type = TransactionType.Withdrawal
		       			},
			       		new SubscriptionTransaction
		       			{
		       				Amount = 200.20M,
		       				CreatedDate = new DateTime(2010, 10, 25),
		       				Reason = TransactionReason.Payment,
		       				Type = TransactionType.Deposit
		       			},
			       		new SubscriptionTransaction
		       			{
		       				Amount = 300.30M,
		       				CreatedDate = new DateTime(2010, 10, 26),
		       				Reason = TransactionReason.Withdrawal,
		       				Type = TransactionType.Withdrawal
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

		public static IList<TransactionArticle> GetArticleList()
		{
			return new List<TransactionArticle>
			{
				GetArticle(1),
				GetArticle(2),
				GetArticle(3),
				GetArticle(4),
				GetArticle(5),
				GetArticle(6),
			};
		}

		public static TransactionArticle GetArticle(int id)
		{
			var mockedArticle = new Mock<TransactionArticle>();
			mockedArticle.SetupGet(x => x.Id).Returns(id);
			mockedArticle.SetupGet(x => x.Name).Returns("Artikel " + id);
			mockedArticle.SetupGet(x => x.NumberOfConnectedTransactions).Returns((id % 3)*2);
			return mockedArticle.Object;
		}
	}

}
