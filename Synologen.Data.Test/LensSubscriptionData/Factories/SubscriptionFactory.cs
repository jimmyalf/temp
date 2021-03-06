using System;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Spinit.Wpc.Synologen.Data.Test.LensSubscriptionData.Factories
{
	public static class SubscriptionFactory
	{
		public static Subscription Get(Customer customer)
		{
			return Get(customer, false);
		}

		public static Subscription Get(Customer customer, bool isActive)
		{
			return Get(customer, isActive, SubscriptionConsentStatus.Sent);
		}

		public static Subscription Get(Customer customer, int seed)
		{
			return Get(customer, !seed.IsDivisibleBy(5), SubscriptionConsentStatus.Sent.SkipItems(seed));
		}

		public static Subscription Get(Customer customer, bool isActive, SubscriptionConsentStatus consentStatus)
		{
			return Get(customer, isActive, consentStatus, new DateTime(2010, 11, 28));
		}

		public static Subscription Get(Customer customer, bool isActive, SubscriptionConsentStatus consentStatus, DateTime? paymentSent)
		{
			return Get(customer, isActive, consentStatus, paymentSent, 5);
		}

		public static Subscription Get(Customer customer, bool isActive, SubscriptionConsentStatus consentStatus, DateTime? paymentSent, int? bankgiroPayerNumber)
		{
			return new Subscription
			{
				ActivatedDate = new DateTime(2010, 10, 18),
				CreatedDate = new DateTime(2010, 10, 01),
				PaymentInfo = new SubscriptionPaymentInfo
				{
					AccountNumber = "123546789",
					ClearingNumber = "1122",
					MonthlyAmount = 595,
					PaymentSentDate = paymentSent
				},
				Customer = customer,
				Active = isActive,
				Notes = "Till varje abonnemang h�r ett anteckningsf�lt",
				ConsentStatus = consentStatus,
				BankgiroPayerNumber = bankgiroPayerNumber
			};
		}


		public static Subscription Edit(Subscription subscription) 
		{
			subscription.ActivatedDate = subscription.ActivatedDate.Value.AddDays(5);
			subscription.CreatedDate = subscription.CreatedDate.AddDays(4);
			subscription.PaymentInfo.AccountNumber = subscription.PaymentInfo.AccountNumber.Reverse();
			subscription.PaymentInfo.ClearingNumber = subscription.PaymentInfo.ClearingNumber.Reverse();
			subscription.PaymentInfo.MonthlyAmount = subscription.PaymentInfo.MonthlyAmount + 15;
			subscription.Active = !subscription.Active;
			subscription.Notes = subscription.Notes.Reverse();
			subscription.ConsentStatus = subscription.ConsentStatus.Next();
			subscription.PaymentInfo.PaymentSentDate = subscription.PaymentInfo.PaymentSentDate.Value.AddMonths(1);
			subscription.BankgiroPayerNumber = (subscription.BankgiroPayerNumber == null)
			                                   	? 5 : subscription.BankgiroPayerNumber + 15;
			return subscription;
		}
	}
}