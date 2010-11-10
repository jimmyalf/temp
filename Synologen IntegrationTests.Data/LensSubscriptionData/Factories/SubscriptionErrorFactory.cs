using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;

namespace Spinit.Wpc.Synologen.Integration.Data.Test.LensSubscriptionData.Factories
{
	public class SubscriptionErrorFactory
	{
		public static SubscriptionError[] GetList(Subscription subscription)
		{
			return new[]
			{
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

		public static SubscriptionError Get(Subscription subscription)
		{
			return new SubscriptionError
			       	{
			       		Type = SubscriptionErrorType.NotApproved,
			       		CreatedDate = new DateTime(2010, 10, 10),
			       		HandledDate = new DateTime(2010, 11, 10),
			       		IsHandled = true,
			       		Subscription = subscription
			       	};
		}
	}
}
