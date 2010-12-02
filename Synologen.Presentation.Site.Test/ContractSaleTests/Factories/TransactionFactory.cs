using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;
using Subscription=Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales.Subscription;

namespace Spinit.Wpc.Synologen.Presentation.Site.Test.ContractSaleTests.Factories
{
	public static class TransactionFactory
	{
		public static Transaction[] GetList()
		{
			var subscription = new Subscription();
			return new[]
			{
				new Transaction { Amount = 100.10M, Subscription = subscription },
				new Transaction { Amount = 200.20M, Subscription = subscription },
				new Transaction { Amount = 300.30M, Subscription = subscription },
				new Transaction { Amount = 85.90M, Subscription = subscription }
			};
		}
	}
}