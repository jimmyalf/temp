using System;
using System.Collections.Generic;
using FakeItEasy;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes;

namespace Spinit.Wpc.Synologen.Presentation.Test.Factories.Order
{
	public class Factory
	{
		public static IList<SubscriptionTransaction> GetTransactions(Subscription subscription = null)
		{
			var selectedSubscription = subscription ?? GetSubscription();
			return new List<SubscriptionTransaction>
			{
				GetTransaction(1, selectedSubscription, TransactionReason.Correction, TransactionType.Deposit),
				GetTransaction(2, selectedSubscription, TransactionReason.Correction, TransactionType.Withdrawal),
				GetTransaction(3, selectedSubscription, TransactionReason.Payment, TransactionType.Deposit),
				GetTransaction(4, selectedSubscription, TransactionReason.Payment, TransactionType.Withdrawal),
				GetTransaction(5, selectedSubscription, TransactionReason.PaymentFailed, TransactionType.Deposit),
				GetTransaction(6, selectedSubscription, TransactionReason.PaymentFailed, TransactionType.Withdrawal),
				GetTransaction(7, selectedSubscription, TransactionReason.Withdrawal, TransactionType.Deposit),
				GetTransaction(8, selectedSubscription, TransactionReason.Withdrawal, TransactionType.Withdrawal),

			};
		}

		public static IList<SubscriptionTransaction> GetSettlementableTransactions(Subscription subscription = null)
		{
			var selectedSubscription = subscription ?? GetSubscription();
			return new List<SubscriptionTransaction>
			{
				GetTransaction(1, selectedSubscription, TransactionReason.Payment, TransactionType.Deposit, amount: 255),
				GetTransaction(2, selectedSubscription, TransactionReason.Payment, TransactionType.Deposit, amount: 500),
				GetTransaction(3, selectedSubscription, TransactionReason.Payment, TransactionType.Deposit, amount: 125),
				GetTransaction(4, selectedSubscription, TransactionReason.Payment, TransactionType.Deposit, amount: 123.33m),
			};
		}

		public static SubscriptionTransaction GetTransaction(
			int id = 1,
			Subscription subscription = null, 
			TransactionReason reason = TransactionReason.Payment, 
			TransactionType type = TransactionType.Deposit,
			int? settlementId = null,
			decimal amount = 250m)
		{
			var transaction = A.Fake<SubscriptionTransaction>();
			transaction.Amount = amount;
			transaction.Reason = reason;
			transaction.Subscription = subscription ?? GetSubscription();
			transaction.SettlementId = settlementId;
			transaction.Type = type;
			A.CallTo(() => transaction.Id).Returns(id);
			return transaction;
		}

		public static Subscription GetSubscription(OrderCustomer customer = null, Shop shop = null)
		{
			var selectedShop = shop ?? GetShop();
			return new Subscription
			{
				AutogiroPayerId = 5,
				Active = true,
				BankAccountNumber = "1234567",
				ClearingNumber = "1234",
				ConsentStatus = SubscriptionConsentStatus.Accepted,
				ConsentedDate = new DateTime(2012,01,01),
				Customer = customer ?? GetCustomer(selectedShop),
				Errors = null,
				LastPaymentSent = new DateTime(2012,02,01),
				Shop = selectedShop,
				SubscriptionItems = null,
				Transactions = null
			};
		}

		public static OrderCustomer GetCustomer(Shop shop = null)
		{
			return new OrderCustomer
			{
				AddressLineOne = "Vägen 1",
				AddressLineTwo = null,
				City = "Storstad",
				Email = "test@test.se",
				FirstName = "Adam",
				LastName = "Bertil",
				MobilePhone = "0701-123456",
				Notes = "Anteckningar",
				PersonalIdNumber = "197001012233",
				Phone = "031-123456",
				PostalCode = "12345",
				Shop = shop ?? GetShop()
			};
		}

		public static Shop GetShop()
		{
			return new Shop
			{
				AddressLineOne = "Vägen 1",
				AddressLineTwo = null,
				City = "Västerås",
				Name = "Synbutiken Västerås",
				PostalCode = "12345",
			};
		}
	}
}