using System;
using Moq;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;

namespace Spinit.Wpc.Synologen.Presentation.Site.Test.LensSubscriptionTests.Factories
{
	public class SubscriptionErrorFactory
	{
		
		public static SubscriptionError Get(Subscription subscription, int errorId)
		{
			var mockedSubscriptionError = new Mock<SubscriptionError>();
			mockedSubscriptionError.SetupGet(x => x.CreatedDate).Returns(new DateTime(2010, 11, 13));
			mockedSubscriptionError.SetupGet(x => x.Subscription).Returns(subscription);
			mockedSubscriptionError.SetupGet(x => x.IsHandled).Returns(false);
			mockedSubscriptionError.SetupGet(x => x.Id).Returns(errorId);
			return mockedSubscriptionError.Object;
		}

		public static SubscriptionError[] GetList()
		{
			var customer = CustomerFactory.Get(1);
			var subscription = SubscriptionFactory.Get(customer);
			return GetList(subscription);
		}

		public static SubscriptionError[] GetList(Subscription subscription)
		{
			return new [] {
			     new SubscriptionError
					{
						Type = SubscriptionErrorType.NoAccount,
						CreatedDate = new DateTime(2010, 11, 1),
						HandledDate = new DateTime(2010, 11, 2),
						IsHandled = true,
						Subscription = subscription
					},
				new SubscriptionError
					{
						Type = SubscriptionErrorType.NoCoverage,
						CreatedDate = new DateTime(2010, 11, 2),
						IsHandled = false,
						Subscription = subscription
					},
				new SubscriptionError
					{
						Type = SubscriptionErrorType.NotApproved,
						CreatedDate = new DateTime(2010, 11, 3),
						HandledDate = new DateTime(2010, 11, 3),
						IsHandled = true,
						Subscription = subscription
					},
				new SubscriptionError
					{
						Type = SubscriptionErrorType.NotChargeable,
						CreatedDate = new DateTime(2010, 11, 4),
						IsHandled = false,
						Subscription = subscription
					},
				new SubscriptionError
					{
						Type = SubscriptionErrorType.NotPermitted,
						CreatedDate = new DateTime(2010, 11, 5),
						HandledDate = new DateTime(2010, 11, 5),
						IsHandled = true, 
						Subscription = subscription
					},
				new SubscriptionError
					{
						Type = SubscriptionErrorType.Stopped,
						CreatedDate = new DateTime(2010, 11, 6),
						IsHandled = false,
						Subscription = subscription
					}
			  };
		}
	}
}
