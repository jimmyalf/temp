using System;
using System.Collections.Generic;
using System.Linq;
using Spinit;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes;

namespace Synologen.Service.Client.SubscriptionTaskRunner.AcceptanceTest
{
	public static class Factory
	{
		public static OrderCustomer CreateCustomer(Shop shop)
		{ 
			return new OrderCustomer
			{
				AddressLineOne = "Storgatan 1",
				AddressLineTwo = "Box 1234",
				City = "Storstad",
				Email = "test.kund@test.se",
				FirstName = "Adam",
				LastName = "Bertil",
				MobilePhone = "0701-234567",
				Notes = "Anteckningar...",
				PersonalIdNumber = "197001011234",
				Phone = "031-123456",
				PostalCode = "12345",
				Shop = shop
			};
		}

		public static Subscription CreateSubscription(
			OrderCustomer customer, 
			Shop shop,
			int? autogiroPayerId = null, 
			SubscriptionConsentStatus consentStatus = SubscriptionConsentStatus.NotSent,
			DateTime? sentDate = null)
		{
			return new Subscription
			{
				ConsentedDate = null,
				Active = true,
				AutogiroPayerId = autogiroPayerId,
				ConsentStatus = consentStatus,
				//CreatedDate = new DateTime(2011, 01, 22),
				Customer = customer,
				Errors = null,
				//Notes = "Notes...",
				BankAccountNumber = "123456",
				ClearingNumber = "1234",
				LastPaymentSent = sentDate,
				Transactions = null,
				Shop = shop
			};
		}

		public static IEnumerable<Subscription> CreateSubscriptions(OrderCustomer customer, Shop shop, int? autogiroPayerId = null, SubscriptionConsentStatus consentStatus = SubscriptionConsentStatus.NotSent, DateTime? sentDate = null)
		{
			return Sequence.Generate(() => CreateSubscription(customer, shop, autogiroPayerId, consentStatus, sentDate), 10);
		}

		public static BGReceivedConsent CreateConsentedConsent(AutogiroPayer payer) 
		{
			return CreateConsent(ConsentCommentCode.NewConsent, ConsentInformationCode.AnswerToNewAccountApplication, new DateTime(2011, 03, 23), payer);
		}

		public static BGReceivedConsent CreateFailedConsent(AutogiroPayer payer) 
		{
			return CreateConsent(ConsentCommentCode.IncorrectPaymentReceiverBankgiroNumber, ConsentInformationCode.InitiatedByPayersBank, null, payer);
		}

		private static BGReceivedConsent CreateConsent(ConsentCommentCode commentCode, ConsentInformationCode informationCode, DateTime? consentValidForDate, AutogiroPayer payer)
		{
			return new BGReceivedConsent
			{
				ActionDate = new DateTime(2011,03,23),
                CommentCode = commentCode,
                ConsentValidForDate = consentValidForDate,
                CreatedDate = new DateTime(2011,03,23),
                InformationCode = informationCode,
                Payer = payer,
			};
		}

		public static BGReceivedPayment CreateSuccessfulPayment(AutogiroPayer payer, string reference = "Reference", decimal amount = 823M) 
		{
			return CreatePayment(payer, PaymentResult.Approved, reference, amount);
		}

		public static BGReceivedPayment CreateFailedPayment(AutogiroPayer payer) 
		{ 
			return CreatePayment(payer, PaymentResult.InsufficientFunds);
		}

		private static BGReceivedPayment CreatePayment(AutogiroPayer payer, PaymentResult result, string reference = "Reference", decimal amount = 823M)
		{
			return new BGReceivedPayment
			{
				Amount = amount,
				CreatedDate = new DateTime(2011, 03, 24),
				NumberOfReoccuringTransactionsLeft = null,
				Payer = payer,
				PaymentDate = new DateTime(2011, 03, 24),
				PeriodCode = PaymentPeriodCode.PaymentOnceOnSelectedDate,
				Reciever = new PaymentReciever
				{
					BankgiroNumber = "123456-123",
					CustomerNumber = "123456"
				},
				Reference = reference,
				ResultType = result,
				Type = PaymentType.Debit
			};
		}

		public static BGReceivedError CreateError(AutogiroPayer payer) 
		{
			return new BGReceivedError
			{
				Amount = 654M,
				CommentCode = ErrorCommentCode.NotYetDebitable,
				CreatedDate = new DateTime(2011, 03, 24),
				Payer = payer,
				PaymentDate = new DateTime(2011, 03, 01),
				Reference = "Reference"
			};
		}

		public static SubscriptionItem CreateSubscriptionItem(Subscription subscription, int? withdrawalsLimit = 12, int performedWithdrawals = 0)
		{
			return new SubscriptionItem
			{
				WithdrawalsLimit = withdrawalsLimit,
				PerformedWithdrawals = performedWithdrawals,
				Subscription = subscription,
				TaxFreeAmount = 150,
				TaxedAmount = 300
			};
		}

		public static IEnumerable<SubscriptionItem> CreateSubscriptionItems(Subscription subscription)
		{
			return new []
			{
				CreateSubscriptionItem(subscription), //Active subscription item with limit
				CreateSubscriptionItem(subscription, null), //Active subscription item without limit
				CreateSubscriptionItem(subscription, 12, 12) //Expired subscription item
			};
		}

		public static SubscriptionPendingPayment CreatePendingPayment(IList<SubscriptionItem> subscriptionItems)
		{
			return new SubscriptionPendingPayment
			{
				Amount = subscriptionItems.Where(x => x.IsActive).Sum(x => x.AmountForAutogiroWithdrawal),
				HasBeenPayed = false,
				SubscriptionItems = subscriptionItems.Where(x => x.IsActive).ToList()
			};
		}
	}
}