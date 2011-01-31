using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Extensions;

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
						Type = SubscriptionErrorType.NotDebitable,
						CreatedDate = new DateTime(2010, 11, 4),
						IsHandled = false,
						Subscription = subscription
					},
				new SubscriptionError
					{
						Type = SubscriptionErrorType.ConsentMissing,
						CreatedDate = new DateTime(2010, 11, 5),
						HandledDate = new DateTime(2010, 11, 5),
						IsHandled = true, 
						Subscription = subscription
					},
				new SubscriptionError
					{
						Type = SubscriptionErrorType.CosentStopped,
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
			       		IsHandled = false,
			       		Subscription = subscription
			       	};
		}

		public static SubscriptionError Edit(SubscriptionError subscriptionError)
		{
			subscriptionError.CreatedDate = subscriptionError.CreatedDate.AddDays(1);
			subscriptionError.HandledDate = SetHandledDate(subscriptionError.HandledDate);
			subscriptionError.IsHandled = !subscriptionError.IsHandled;
			subscriptionError.Type = subscriptionError.Type.Next();
			return subscriptionError;
		}

		private static DateTime SetHandledDate(DateTime? date)
		{
			if (date.HasValue)
				return date.Value.AddDays(1);
			{
				var myDate = DateTime.Now;
				return new DateTime(myDate.Year, myDate.Month, myDate.Day);
			}
		}
	}
}
