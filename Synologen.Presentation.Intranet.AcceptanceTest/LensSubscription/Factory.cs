using System;
using System.Collections.Generic;
using System.Linq;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.LensSubscription
{
	public static class Factory
	{	
		public static  Customer CreateCustomer(Country country, Shop shop)
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

		private static IEnumerable<Subscription> CreateSubscriptions(Customer customer)
		{
			Func<bool, SubscriptionConsentStatus, decimal, Subscription> getSubscription = (active, status, monthlyAmount) => new Subscription
			{
				Active = active,
				ConsentStatus = status,
				CreatedDate = new DateTime(2011, 05, 23),
				Customer = customer,
				PaymentInfo = new SubscriptionPaymentInfo { MonthlyAmount = monthlyAmount, AccountNumber = "123456789", ClearingNumber = "1234"}
			};
			return new[]
			{
				getSubscription(true, SubscriptionConsentStatus.Accepted, 256),
				getSubscription(false, SubscriptionConsentStatus.Denied, 15),
				getSubscription(false, SubscriptionConsentStatus.NotSent, 456.23m),
				getSubscription(false, SubscriptionConsentStatus.Sent, 125.27m),
			};
		}
	
		public static IList<Subscription> CreateSubscriptions(params Customer[] customers)
		{
			return customers.SelectMany(CreateSubscriptions).ToList();
		}

		public static IList<SubscriptionTransaction> CreateTransactions(TransactionArticle transactionArticle, IEnumerable<Subscription> subscriptions)
		{
			return subscriptions.SelectMany(subscription => CreateTransactions(transactionArticle, subscription)).ToList();
		}

		private static IEnumerable<SubscriptionTransaction> CreateTransactions(TransactionArticle transactionArticle, Subscription subscription)
		{
			Func<decimal, TransactionReason, TransactionType, TransactionArticle, SubscriptionTransaction> getTransaction = (amount, reason, type, article) => new SubscriptionTransaction
			{
				Amount = amount, 
				Article = article, 
				Reason = reason, 
				Subscription = subscription, 
				Type = type,
				CreatedDate = new DateTime(2011,05,23)                
			};
			return new[]
			{
				getTransaction(1025.25m, TransactionReason.Correction, TransactionType.Deposit, null),
				getTransaction(999.99m, TransactionReason.Correction, TransactionType.Withdrawal, null),
				getTransaction(275, TransactionReason.Payment, TransactionType.Deposit, null),
				getTransaction(275, TransactionReason.PaymentFailed, TransactionType.Deposit, null),
				getTransaction(1500, TransactionReason.Withdrawal, TransactionType.Withdrawal, transactionArticle)
			};
		}

		public static TransactionArticle CreateTransactionArticle()
		{
			return new TransactionArticle {Active = true, Name = "Artikel ABC" };
		}
	}
}