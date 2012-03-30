using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Test.ContractSaleTests.Factories
{
	public static class TransactionFactory
	{
		public static OldTransaction[] GetList()
		{
			var customer1 = new Customer { FirstName = "Erik", LastName = "Eriksson" };
			var subscription1 = SubscriptionFactory.Get(1, customer1);
			var customer2 = new Customer { FirstName = "Sven", LastName = "Svensson" };
			var subscription2 = SubscriptionFactory.Get(2, customer2);

			return new[]
			{
				new OldTransaction { Amount = 100.10M, Subscription = subscription1, CreatedDate = new DateTime(2010, 11, 20)},
				new OldTransaction { Amount = 200.20M, Subscription = subscription2, CreatedDate = new DateTime(2010, 11, 21) },
				new OldTransaction { Amount = 300.30M, Subscription = subscription1, CreatedDate = new DateTime(2010, 11, 22) },
				new OldTransaction { Amount = 85.90M, Subscription = subscription2, CreatedDate = new DateTime(2010, 11, 23) }
			};
		}
	}
}