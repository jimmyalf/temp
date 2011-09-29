using System;
using FakeItEasy;
using Moq;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.EventArguments.LensSubscription;

namespace Spinit.Wpc.Synologen.Presentation.Site.Test.LensSubscriptionTests.Factories
{
	public static class SubscriptionFactory
	{

		private static bool Subscription_Is_Active = true;
		private static bool Subscription_Not_Active = false;

		public static Subscription Get(Customer customer)
		{
			return Get(customer, Subscription_Is_Active);
		}
		public static Subscription Get(Customer customer, bool isActive)
		{
			return CreateSubscription(customer, 2, 10, "123456789", "0089", 455.23M, isActive, "Fritextfält", 55);
		}

		public static Subscription Get(int id, Customer customer)
		{
			return CreateSubscription(id, customer, 2, 10, "123456789", "0089", 455.23M, Subscription_Not_Active);
		}

		private static Subscription CreateSubscription(int id, Customer customer, int activatedSubtractDays, int createdSubtractDays, string accountNumber, string clearingNumber, decimal MonthlyAmount, bool isActive)
		{
			var mockedSubscription = new Mock<Subscription>();
			mockedSubscription.SetupGet(x => x.Id).Returns(id);
			mockedSubscription.SetupGet(x => x.ActivatedDate).Returns(DateTime.Now.SubtractDays(activatedSubtractDays));
			mockedSubscription.SetupGet(x => x.CreatedDate).Returns(DateTime.Now.SubtractDays(createdSubtractDays));
			mockedSubscription.SetupGet(x => x.PaymentInfo).Returns(new SubscriptionPaymentInfo
				{
					AccountNumber = accountNumber,
					ClearingNumber = clearingNumber,
					MonthlyAmount = MonthlyAmount
				});
			mockedSubscription.SetupGet(x => x.Active).Returns(isActive);
			mockedSubscription.SetupGet(x => x.Customer).Returns(customer);
			return mockedSubscription.Object;	
		}

		public static Subscription[] GetList(Customer customer)
		{
			
			return new []
	       	{
				CreateSubscription(customer, 3, 10, "111122222", "0001", 500M, Subscription_Is_Active, "Fritext 1", 5), 
				CreateSubscription(customer, 4, 11, "222233333", "0002", 600M, Subscription_Not_Active, "Fritext 2", 6), 
				CreateSubscription(customer, 5, 12, "333344444", "0003", 700M, Subscription_Is_Active, "Fritext 3", 7), 
				CreateSubscription(customer, 6, 13, "444455555", "0004", 800M, Subscription_Not_Active, "Fritext 4", 8)
	       	};
		}

		public static Subscription[] GetListWithTransactions(Customer customer)
		{
			Func<bool, SubscriptionConsentStatus, decimal, bool, Subscription> getSubscription = (active, status, montlyAmount, hasErrors) => new Subscription
			{
				Active = active,
				ConsentStatus = status,
				Transactions = TransactionFactory.GetList(null),
				Customer = customer,
				PaymentInfo = new SubscriptionPaymentInfo { MonthlyAmount = montlyAmount },
				Errors = hasErrors ? new[]{ new SubscriptionError() } : null
			};
			return new []
	       	{
				//Active
				getSubscription(true, SubscriptionConsentStatus.Accepted, 130, false),
				getSubscription(true, SubscriptionConsentStatus.Denied, 654.4m, false),
				getSubscription(true, SubscriptionConsentStatus.NotSent, 320, false),
				getSubscription(true, SubscriptionConsentStatus.Sent, 345.99m, false),
				//Inactive
				getSubscription(false, SubscriptionConsentStatus.Accepted, 654.75m, false),
				getSubscription(false, SubscriptionConsentStatus.Denied, 48.63m, false),
				getSubscription(false, SubscriptionConsentStatus.NotSent, 64.32m, false),
				getSubscription(false, SubscriptionConsentStatus.Sent, 1200, false),
				//Active with errors
				getSubscription(true, SubscriptionConsentStatus.Accepted, 130, true),
				getSubscription(true, SubscriptionConsentStatus.Denied, 654.4m, true),
				getSubscription(true, SubscriptionConsentStatus.NotSent, 320, true),
				getSubscription(true, SubscriptionConsentStatus.Sent, 345.99m, true),
				//Inactive with errors
				getSubscription(false, SubscriptionConsentStatus.Accepted, 654.75m, true),
				getSubscription(false, SubscriptionConsentStatus.Denied, 48.63m, true),
				getSubscription(false, SubscriptionConsentStatus.NotSent, 64.32m, true),
				getSubscription(false, SubscriptionConsentStatus.Sent, 1200, true),

	       	};
		}

		public static Subscription GetWithTransactions(Customer customer)
		{
			return CreateSubscriptionWithTransactions(customer, 2, 10, "123456789", "0089", 455.23M, Subscription_Is_Active);
		}

		public static Subscription GetWithErrors(Customer customer)
		{
			return CreateSubscriptionWithErrors(customer, 2, 10, "123456789", "0089", 455.23M, Subscription_Is_Active);
		}

		private static Subscription CreateSubscriptionWithErrors(Customer customer, int activatedSubtractDays, int createdSubtractDays, string accountNumber, string clearingNumber, decimal MonthlyAmount, bool isActive)
		{
			var subscription = new Subscription
			{
				ActivatedDate = DateTime.Now.SubtractDays(activatedSubtractDays),
				CreatedDate = DateTime.Now.SubtractDays(createdSubtractDays),
				Customer = customer,
				PaymentInfo = new SubscriptionPaymentInfo
				{
					AccountNumber = accountNumber,
					ClearingNumber = clearingNumber,
					MonthlyAmount = MonthlyAmount
				},
				Active = isActive,
			};
			subscription.Errors = SubscriptionErrorFactory.GetList(subscription);
			return subscription;
		}

		private static Subscription CreateSubscriptionWithTransactions(Customer customer, int activatedSubtractDays, int createdSubtractDays, string accountNumber, string clearingNumber, decimal MonthlyAmount, bool isActive)
		{
			var subscription = new Subscription
			{
				ActivatedDate = DateTime.Now.SubtractDays(activatedSubtractDays),
				CreatedDate = DateTime.Now.SubtractDays(createdSubtractDays),
				Customer = customer,
				PaymentInfo = new SubscriptionPaymentInfo
				{
					AccountNumber = accountNumber,
					ClearingNumber = clearingNumber,
					MonthlyAmount = MonthlyAmount
				},
				Active = isActive,
			};
			subscription.Transactions = TransactionFactory.GetList(subscription);
			return subscription;
		}



		public static Subscription CreateSubscription(Customer customer, int activatedSubtractDays, int createdSubtractDays, string accountNumber, string clearingNumber, decimal MonthlyAmount, bool isActive, string notes, int subscriptionId)
		{
			var subscription = A.Fake<Subscription>(x => x.Wrapping(new Subscription
				{
					ActivatedDate = DateTime.Now.SubtractDays(activatedSubtractDays),
					CreatedDate = DateTime.Now.SubtractDays(createdSubtractDays),
					Customer = customer,
					PaymentInfo = new SubscriptionPaymentInfo
					{
						AccountNumber = accountNumber,
						ClearingNumber = clearingNumber,
						MonthlyAmount = MonthlyAmount
					},
					Active = isActive,
					Notes = notes
				}
			));
			A.CallTo(() => subscription.Id).Returns(subscriptionId);
			return subscription;

		}

		public static SaveSubscriptionEventArgs GetSaveSubscriptionEventArgs(Subscription subscription) 
		{
			return new SaveSubscriptionEventArgs
			{
				AccountNumber = subscription.PaymentInfo.AccountNumber.Reverse(),
				ClearingNumber = subscription.PaymentInfo.ClearingNumber.Reverse(),
				MonthlyAmount = (subscription.PaymentInfo.MonthlyAmount + 255.21M).ToString(),
				Notes = subscription.Notes.Reverse()
			};
		}

		public static SaveSubscriptionEventArgs GetSaveSubscriptionEventArgs()
		{
			return new SaveSubscriptionEventArgs
			{
				AccountNumber = "123456789",
                ClearingNumber = "1234",
                MonthlyAmount = "699.25",
				Notes = "Plats för valfria anteckningar"
			};
		}
	}
}