using System;
using Moq;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.EventArguments.LensSubscription;

namespace Spinit.Wpc.Synologen.Presentation.Site.Test.LensSubscriptionTests.Factories
{
	public static class SubscriptionFactory
	{
		public static Subscription Get(Customer customer)
		{
			return Get(customer, SubscriptionStatus.Active);
		}
		public static Subscription Get(Customer customer, SubscriptionStatus status)
		{
			return CreateSubscription(customer, 2, 10, "123456789", "0089", 455.23M, status, "Fritextfält");
		}

		public static Subscription Get(int id, Customer customer)
		{
			return CreateSubscription(id, customer, 2, 10, "123456789", "0089", 455.23M, SubscriptionStatus.Active);
		}

		private static Subscription CreateSubscription(int id, Customer customer, int activatedSubtractDays, int createdSubtractDays, string accountNumber, string clearingNumber, decimal MonthlyAmount, SubscriptionStatus status)
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
			mockedSubscription.SetupGet(x => x.Status).Returns(status);
			mockedSubscription.SetupGet(x => x.Customer).Returns(customer);
			return mockedSubscription.Object;	
		}

		public static Subscription[] GetList(Customer customer)
		{
			
			return new []
			       	{
			       		
						CreateSubscription(customer, 3, 10, "111122222", "0001", 500M, SubscriptionStatus.Active, "Fritext 1"), 
						CreateSubscription(customer, 4, 11, "222233333", "0002", 600M, SubscriptionStatus.Created, "Fritext 2"), 
						CreateSubscription(customer, 5, 12, "333344444", "0003", 700M, SubscriptionStatus.Expired, "Fritext 3"), 
						CreateSubscription(customer, 6, 13, "444455555", "0004", 800M, SubscriptionStatus.Stopped, "Fritext 4")
			       	};
		}

		public static Subscription GetWithTransactions(Customer customer)
		{
			return CreateSubscriptionWithTransactions(customer, 2, 10, "123456789", "0089", 455.23M, SubscriptionStatus.Active);
		}

		public static Subscription GetWithErrors(Customer customer)
		{
			return CreateSubscriptionWithErrors(customer, 2, 10, "123456789", "0089", 455.23M, SubscriptionStatus.Active);
		}

		private static Subscription CreateSubscriptionWithErrors(Customer customer, int activatedSubtractDays, int createdSubtractDays, string accountNumber, string clearingNumber, decimal MonthlyAmount, SubscriptionStatus status)
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
				Status = status,
			};
			subscription.Errors = SubscriptionErrorFactory.GetList(subscription);
			return subscription;
		}

		private static Subscription CreateSubscriptionWithTransactions(Customer customer, int activatedSubtractDays, int createdSubtractDays, string accountNumber, string clearingNumber, decimal MonthlyAmount, SubscriptionStatus status)
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
				Status = status,
			};
			subscription.Transactions = TransactionFactory.GetList(subscription);
			return subscription;
		}



		public static Subscription CreateSubscription(Customer customer, int activatedSubtractDays, int createdSubtractDays, string accountNumber, string clearingNumber, decimal MonthlyAmount, SubscriptionStatus status, string notes)
		{
			return new Subscription
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
				Status = status,
				Notes = notes
			};
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