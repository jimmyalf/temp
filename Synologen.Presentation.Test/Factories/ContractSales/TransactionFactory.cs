using System;
using System.Collections.Generic;
using Moq;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;

namespace Spinit.Wpc.Synologen.Presentation.Test.Factories.ContractSales
{
	public static class TransactionFactory
	{
		public static OldTransaction GetOld(OldSubscription subscription = null)
		{
			var subscriptionToUse = subscription ?? SubscriptionFactory.GetOld(1, 1, 1);
			return GetOld(1, 586.23M, subscriptionToUse, new DateTime(2010, 11, 30));
		}

		public static OldTransaction GetOld(int id, decimal amount, OldSubscription subscription, DateTime date)
		{
			var mockedTransaction = new Mock<OldTransaction>();
			mockedTransaction.SetupGet(x => x.Id).Returns(id);
			mockedTransaction.SetupGet(x => x.Subscription).Returns(subscription);
			mockedTransaction.SetupGet(x => x.Amount).Returns(amount);
			mockedTransaction.SetupGet(x => x.CreatedDate).Returns(date);
			var returnObject = mockedTransaction.Object;
			return returnObject;
		}

		public static NewTransaction GetNew(NewSubscription subscription = null)
		{
			var subscriptionToUse = subscription ?? SubscriptionFactory.GetNew(1);
			return GetNew(1, 586.23M, subscriptionToUse, new DateTime(2010, 11, 30));
		}

		public static NewTransaction GetNew(int id, decimal amount, NewSubscription subscription, DateTime date)
		{
			var mockedTransaction = new Mock<NewTransaction>();
			mockedTransaction.SetupGet(x => x.Id).Returns(id);
			mockedTransaction.SetupGet(x => x.Subscription).Returns(subscription);
			mockedTransaction.Setup(x => x.GetAmount()).Returns(new SubscriptionAmount(amount, 0));
			mockedTransaction.SetupGet(x => x.CreatedDate).Returns(date);
			var returnObject = mockedTransaction.Object;
			return returnObject;
		}
	}
}