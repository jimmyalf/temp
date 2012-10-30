using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Test.ContractSaleTests.Factories
{
	public static class TransactionFactory
	{
		public static OldTransaction[] GetOldList()
		{
			var customer1 = new OldCustomer { FirstName = "Erik", LastName = "Eriksson" };
			var subscription1 = SubscriptionFactory.GetOld(1, customer1);
			var customer2 = new OldCustomer { FirstName = "Sven", LastName = "Svensson" };
			var subscription2 = SubscriptionFactory.GetOld(2, customer2);

			return new[]
			{
				new OldTransaction { Amount = 100.10M, Subscription = subscription1, CreatedDate = new DateTime(2010, 11, 20)},
				new OldTransaction { Amount = 200.20M, Subscription = subscription2, CreatedDate = new DateTime(2010, 11, 21) },
				new OldTransaction { Amount = 300.30M, Subscription = subscription1, CreatedDate = new DateTime(2010, 11, 22) },
				new OldTransaction { Amount = 85.90M, Subscription = subscription2, CreatedDate = new DateTime(2010, 11, 23) }
			};
		}

		public static IList<NewTransaction> GetNewList()
		{
			var customer1 = new NewCustomer { FirstName = "Erik", LastName = "Eriksson" };
			var subscription1 = SubscriptionFactory.GetNew(1, customer1);
			var customer2 = new NewCustomer { FirstName = "Sven", LastName = "Svensson" };
			var subscription2 = SubscriptionFactory.GetNew(2, customer2);

			return new[]
			{
				new NewTestTransaction
				{
					Subscription = subscription1, CreatedDate = new DateTime(2010, 11, 20),
					PendingPayment = new PendingPayment{ TaxedAmount = 75, TaxFreeAmount = 25.10M }
				}.SetAmount(75, 25.10M),
				new NewTestTransaction
				{
					Subscription = subscription2, CreatedDate = new DateTime(2010, 11, 21),
					PendingPayment = new PendingPayment{ TaxedAmount = 175, TaxFreeAmount = 25.20M }
				}.SetAmount(175, 25.20M),
				new NewTestTransaction
				{
					Subscription = subscription1, CreatedDate = new DateTime(2010, 11, 22),
					PendingPayment = new PendingPayment{ TaxedAmount = 250, TaxFreeAmount = 50.30M }
				}.SetAmount(250, 50.30M),
				new NewTestTransaction
				{
					Subscription = subscription2, CreatedDate = new DateTime(2010, 11, 23),
					PendingPayment = new PendingPayment{ TaxedAmount = 80, TaxFreeAmount = 5.90M }
				}.SetAmount(80, 5.90M)
			};
		}
	}

	public class NewTestTransaction : NewTransaction
	{
		public NewTestTransaction SetAmount(decimal taxedAmount, decimal taxFreeAmount)
		{
			Amount = new SubscriptionAmount(taxedAmount, taxFreeAmount);
			return this;
		}
	}

}