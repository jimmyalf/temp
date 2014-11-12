using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;

namespace Spinit.Wpc.Synologen.Presentation.AcceptanceTest.Helpers
{
	public static class LensSubscriptionFactory
	{
		 public static Subscription GetSubscription(Customer customer, bool active = true, SubscriptionConsentStatus consentStatus = SubscriptionConsentStatus.Accepted, decimal monthlyAmount = 255m)
		 {
		 	return new Subscription
			{
				Active = active,
				ConsentStatus = consentStatus,
				CreatedDate = new DateTime(2011, 05, 23),
				Customer = customer,
				PaymentInfo = new SubscriptionPaymentInfo
				{
					MonthlyAmount = monthlyAmount, AccountNumber = "123456789", ClearingNumber = "1234"
				}
			};		 		
		 }

		public static SubscriptionTransaction GetTransaction(Subscription subscription, TransactionArticle article = null, decimal amount = 255, TransactionReason reason = TransactionReason.Payment, Settlement settlement = null, TransactionType type = TransactionType.Deposit)
		{
			return new SubscriptionTransaction
			{
				Amount = amount,
				Article = article,
				CreatedDate = new DateTime(2011, 01, 01),
				Reason = reason,
				Settlement = settlement,
				Subscription = subscription,
				Type = type,
			};
		}

		public static IList<SubscriptionTransaction> GetTransactions(Subscription subscription)
		{
			return new[]
			{
				GetTransaction(subscription, amount: 55, reason: TransactionReason.Correction, type: TransactionType.Deposit),
				GetTransaction(subscription, amount: 155, reason: TransactionReason.Correction, type: TransactionType.Withdrawal),
				GetTransaction(subscription, amount: 255, reason: TransactionReason.Payment, type: TransactionType.Deposit),
				GetTransaction(subscription, amount: 155, reason: TransactionReason.Payment, type: TransactionType.Withdrawal),
				GetTransaction(subscription, amount: 55, reason: TransactionReason.PaymentFailed, type: TransactionType.Deposit),
				GetTransaction(subscription, amount: 155, reason: TransactionReason.PaymentFailed, type: TransactionType.Withdrawal),
				GetTransaction(subscription, amount: 255, reason: TransactionReason.Withdrawal, type: TransactionType.Deposit),
				GetTransaction(subscription, amount: 55, reason: TransactionReason.Withdrawal, type: TransactionType.Withdrawal),
			};
		}

		public static Customer GetCustomer(Country country, Shop shop)
		{
			return new Customer
			{
				Address = new CustomerAddress
				{
					AddressLineOne = "AddressLineOne",
					City = "Göteborg",
					PostalCode = "43632",
					Country = country
				},
				Contact = new CustomerContact
				{
					Email = "abc@abc.se",
					MobilePhone = "0700-00 00 00",
					Phone = "031 - 00 00 00"
				},
				FirstName = "FirstName",
				LastName = "LastName",
				Shop = shop,
				PersonalIdNumber = "197910071111"
			};
		}

	}
}