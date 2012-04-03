using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Test.ContractSaleTests.Factories
{
	public static class TransactionFactory
	{
		public static OldTransaction[] GetOldList()
		{
			var customer1 = new Customer { FirstName = "Erik", LastName = "Eriksson" };
			var subscription1 = SubscriptionFactory.GetOld(1, customer1);
			var customer2 = new Customer { FirstName = "Sven", LastName = "Svensson" };
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
			var customer1 = new Customer { FirstName = "Erik", LastName = "Eriksson" };
			var subscription1 = SubscriptionFactory.GetNew(1, customer1);
			var customer2 = new Customer { FirstName = "Sven", LastName = "Svensson" };
			var subscription2 = SubscriptionFactory.GetNew(2, customer2);

			return new[]
			{
				new NewTransaction { Amount = 100.10M, Subscription = subscription1, CreatedDate = new DateTime(2010, 11, 20)},
				new NewTransaction { Amount = 200.20M, Subscription = subscription2, CreatedDate = new DateTime(2010, 11, 21) },
				new NewTransaction { Amount = 300.30M, Subscription = subscription1, CreatedDate = new DateTime(2010, 11, 22) },
				new NewTransaction { Amount = 85.90M, Subscription = subscription2, CreatedDate = new DateTime(2010, 11, 23) }
			};
		}
	}
}