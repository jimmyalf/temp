using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;

namespace ServiceCoordinator.AcceptanceTest
{
	public static class Factory
	{
		public static Customer CreateCustomer(Country country, Shop shop) 
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

		public static Subscription CreateNewSubscription(Customer customer)
		{
			return CreateSubscription(customer, SubscriptionConsentStatus.NotSent, null, null);
		}

		public static Subscription CreateSentSubscription(Customer customer, int bankgiroPayerNumber)
		{
			return CreateSubscription(customer, SubscriptionConsentStatus.Sent, new DateTime(2011, 02, 01), bankgiroPayerNumber);
		}

		public static Subscription CreateSubscriptionReadyForPayment(Customer customer, int bankgiroPayerNumber)
		{
			return CreateSubscription(customer, SubscriptionConsentStatus.Accepted, new DateTime(2011, 02, 01), bankgiroPayerNumber);
		}

		private static Subscription CreateSubscription(Customer customer, SubscriptionConsentStatus status, DateTime? sentDate, int? bankgiroNumber)
		{
			return new Subscription
			{
				ActivatedDate = null,
				Active = true,
				BankgiroPayerNumber = bankgiroNumber,
				ConsentStatus = status,
				CreatedDate = new DateTime(2011, 01, 22),
				Customer = customer,
				Errors = null,
				Notes = "Notes...",
				PaymentInfo = new SubscriptionPaymentInfo
				{
					AccountNumber = "123456",
					ClearingNumber = "1234",
					MonthlyAmount = 355,
					PaymentSentDate = sentDate
				},
				Transactions = null,
			};
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
	}
}