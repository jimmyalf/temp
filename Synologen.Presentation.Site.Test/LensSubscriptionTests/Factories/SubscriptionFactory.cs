using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Spinit.Wpc.Synologen.Presentation.Site.Test.LensSubscriptionTests.Factories
{
	public static class SubscriptionFactory
	{
		public static Subscription Get(Customer customer)
		{
			return new Subscription
			{
				ActivatedDate = DateTime.Now.SubtractDays(2),
				CreatedDate = DateTime.Now.SubtractDays(10),
				Customer = customer,
				PaymentInfo = new SubscriptionPaymentInfo
				{
					AccountNumber = "123456789",
					ClearingNumber = "0089",
					MonthlyAmount = 455.23M
				},
				Status = SubscriptionStatus.Active,
			};
		}
	}
}