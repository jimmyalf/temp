using System;
using System.Collections.Generic;
using Moq;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Synologen.LensSubscription.ServiceCoordinator.Task.Test.Factories
{
	public static class SubscriptionFactory 
	{
		public static IEnumerable<Subscription> GetList()
		{
			Func<int, Subscription> generateItem = Get;
			return generateItem.GenerateRange(1, 15);
		}

		public static Subscription Get(int id)
		{
			var mockedSubscription = new Mock<Subscription>();
			mockedSubscription.SetupGet(x => x.Id).Returns(id);
			mockedSubscription.SetupProperty(x => x.Customer.PersonalIdNumber, "197502065019");
			mockedSubscription.SetupProperty(x => x.PaymentInfo.ClearingNumber, "0123");
			mockedSubscription.SetupProperty(x => x.PaymentInfo.AccountNumber, "12345678");
			mockedSubscription.SetupProperty(x => x.PaymentInfo.MonthlyAmount, 399);
			mockedSubscription.SetupProperty(x => x.ConsentStatus);
			mockedSubscription.SetupProperty(x => x.ActivatedDate);
			return mockedSubscription.Object;
		}
	}
}