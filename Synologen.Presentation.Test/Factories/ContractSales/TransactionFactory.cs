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
			//returnObject.Amount = amount;
			return returnObject;
		}

		//public static IEnumerable<Transaction> GetList()
		//{
		//    var subscription1 = SubscriptionFactory.Get(1, 1, 1);
		//    var subscription2 = SubscriptionFactory.Get(2, 2, 2);
		//    var subscription3 = SubscriptionFactory.Get(3, 3, 100);
		//    return new[]
		//    {
		//        Get(1, 285.45M, subscription1),
		//        Get(2, 12.86M, subscription1),
		//        Get(3, 775, subscription2),
		//        Get(4, 256, subscription1),
		//        Get(5, 125, subscription1),
		//        Get(6, 555.55M, subscription2),
		//        Get(6, 129, subscription3),
		//    };
		//}
	}
}