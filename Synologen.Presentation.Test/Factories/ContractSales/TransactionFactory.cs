using Moq;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;

namespace Spinit.Wpc.Synologen.Presentation.Test.Factories.ContractSales
{
	public static class TransactionFactory
	{
		public static Transaction Get()
		{
			var subscription = SubscriptionFactory.Get(1, 1, 1);
			return Get(1, 586.23M, subscription);
		}

		public static Transaction Get(int id, decimal amount, Subscription subscription)
		{
			var mockedTransaction = new Mock<Transaction>();
			mockedTransaction.SetupGet(x => x.Id).Returns(id);
			mockedTransaction.SetupGet(x => x.Subscription).Returns(subscription);
			mockedTransaction.SetupGet(x => x.Amount).Returns(amount);
			var returnObject = mockedTransaction.Object;
			return returnObject;
		}
	}
}