using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Integration.Data.Test.CommonDataTestHelpers;

namespace Spinit.Wpc.Synologen.Integration.Data.Test.LensSubscriptionData.Factories
{
	public static class SubscriptionFactory
	{
		public static Subscription Get(Customer customer)
		{
			return Get(customer, SubscriptionStatus.Stopped);
		}

		public static Subscription Get(Customer customer, SubscriptionStatus status)
		{
			return new Subscription
			{
				ActivatedDate = new DateTime(2010, 10, 18),
				CreatedDate = new DateTime(2010, 10, 01),
                PaymentInfo = new SubscriptionPaymentInfo
                {
                	AccountNumber = "123546789",
                    ClearingNumber = "1122",
                    MonthlyAmount = 595
                },
                Customer = customer,
                Status = status,
				Notes = "Till varje abonnemang hör ett anteckningsfält",
                ConsentStatus = SubscriptionConsentStatus.Sent
			};
		}

		public static Subscription Edit(Subscription subscription) 
		{
			subscription.ActivatedDate = subscription.ActivatedDate.Value.AddDays(5);
			subscription.CreatedDate = subscription.CreatedDate.AddDays(4);
			subscription.PaymentInfo.AccountNumber = subscription.PaymentInfo.AccountNumber.Reverse();
			subscription.PaymentInfo.ClearingNumber = subscription.PaymentInfo.ClearingNumber.Reverse();
			subscription.PaymentInfo.MonthlyAmount = subscription.PaymentInfo.MonthlyAmount + 15;
			subscription.Status = subscription.Status.Next();
			subscription.Notes = subscription.Notes.Reverse();
			subscription.ConsentStatus = subscription.ConsentStatus.Next();
			return subscription;
		}
	}
}