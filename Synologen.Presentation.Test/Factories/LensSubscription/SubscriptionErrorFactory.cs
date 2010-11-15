using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;

namespace Spinit.Wpc.Synologen.Presentation.Test.Factories.LensSubscription
{
	public static class SubscriptionErrorFactory
	{
		public static SubscriptionError Get()
		{
			return new SubscriptionError
			{
				Type = SubscriptionErrorType.NoAccount,
				CreatedDate = new DateTime(2010, 11, 1),
				HandledDate = new DateTime(2010, 11, 2),
				IsHandled = true,
				Subscription = new Subscription()
			};
		}

		public static SubscriptionError Get(SubscriptionErrorType type, DateTime createdDate, DateTime? handledDate, bool isHandled)
		{
			return new SubscriptionError
			{
				Type = type,
				CreatedDate = createdDate,
				HandledDate = handledDate,
				IsHandled = isHandled,
				Subscription = new Subscription()
			};
		}

		public static IEnumerable<SubscriptionError> GetList()
		{
			return  new []
			{
				Get(SubscriptionErrorType.NoAccount, new DateTime(2010, 11, 1), new DateTime(2010, 11, 2), true),
				Get(SubscriptionErrorType.NoCoverage, new DateTime(2010, 11, 2), null, false),
				Get(SubscriptionErrorType.NotApproved, new DateTime(2010, 11, 3), new DateTime(2010, 11, 3), true),
				Get(SubscriptionErrorType.NotChargeable, new DateTime(2010, 11, 4), null, false),
				Get(SubscriptionErrorType.NotPermitted, new DateTime(2010, 11, 5), new DateTime(2010, 11, 5), true),
				Get(SubscriptionErrorType.Stopped, new DateTime(2010, 11, 6), null, false),
			};
		}
	}
}