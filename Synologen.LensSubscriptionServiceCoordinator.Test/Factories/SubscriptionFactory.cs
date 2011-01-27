using System.Collections.Generic;
using Moq;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;

namespace Synologen.ServiceCoordinator.Test.Factories
{
	public static class SubscriptionFactory 
	{
		public static IEnumerable<Subscription> GetList()
		{
			return TestHelper.GenerateSequence<Subscription>(Get, 15);
		}

		public static Subscription Get(int id)
		{
			var mockedSubscription = new Mock<Subscription>();
			mockedSubscription.SetupGet(x => x.Id).Returns(id);
			mockedSubscription.SetupProperty(x => x.Customer.PersonalIdNumber, "197502065019");
			mockedSubscription.SetupProperty(x => x.PaymentInfo.ClearingNumber, "0123");
			mockedSubscription.SetupProperty(x => x.PaymentInfo.AccountNumber, "12345678");
			mockedSubscription.SetupProperty(x => x.ConsentStatus);
			return mockedSubscription.Object;
		}
	}
}