using System;
using Moq;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;

namespace Spinit.Wpc.Synologen.Presentation.Test.Factories.ContractSales
{
	public static class TransactionFactory
	{
		public static OldTransaction Get()
		{
			var subscription = SubscriptionFactory.Get(1, 1, 1);
			return Get(1, 586.23M, subscription, new DateTime(2010, 11, 30));
		}

		public static OldTransaction Get(int id, decimal amount, OldSubscription subscription, DateTime date)
		{
			var mockedTransaction = new Mock<OldTransaction>();
			mockedTransaction.SetupGet(x => x.Id).Returns(id);
			mockedTransaction.SetupGet(x => x.Subscription).Returns(subscription);
			mockedTransaction.SetupGet(x => x.Amount).Returns(amount);
			mockedTransaction.SetupGet(x => x.CreatedDate).Returns(date);
			var returnObject = mockedTransaction.Object;
			return returnObject;
		}
	}
}