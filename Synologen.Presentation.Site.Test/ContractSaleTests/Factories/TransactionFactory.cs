using System;
using Moq;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;
using Subscription=Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales.Subscription;

namespace Spinit.Wpc.Synologen.Presentation.Site.Test.ContractSaleTests.Factories
{
	public static class TransactionFactory
	{
		public static Transaction[] GetList()
		{
			var customer1 = new Customer { FirstName = "Erik", LastName = "Eriksson" };
			var subscription1 = SubscriptionFactory.Get(1, customer1);
			var customer2 = new Customer { FirstName = "Sven", LastName = "Svensson" };
			var subscription2 = SubscriptionFactory.Get(2, customer2);

			return new[]
			{
				new Transaction { Amount = 100.10M, Subscription = subscription1, CreatedDate = new DateTime(2010, 11, 20)},
				new Transaction { Amount = 200.20M, Subscription = subscription2, CreatedDate = new DateTime(2010, 11, 21) },
				new Transaction { Amount = 300.30M, Subscription = subscription1, CreatedDate = new DateTime(2010, 11, 22) },
				new Transaction { Amount = 85.90M, Subscription = subscription2, CreatedDate = new DateTime(2010, 11, 23) }
			};
		}
	}
}