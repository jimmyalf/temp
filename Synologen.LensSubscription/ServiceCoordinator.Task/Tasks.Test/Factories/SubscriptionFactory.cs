using System;
using System.Collections.Generic;
using Moq;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Synologen.LensSubscription.ServiceCoordinator.Task.Test.Factories
{
	public static class SubscriptionFactory 
	{
		public static IEnumerable<Subscription> GetList()
		{
			Func<int, Subscription> generateItem = Get;
			return generateItem.GenerateRange(1, 15);
		}

		public static IEnumerable<Subscription> GetListWithAndWithoutBankgiroPayerNumber()
		{
			Func<int, Subscription> generateItem = Get;
			Func<int, Subscription> generateItemWithoutBankGiroNumber = GetWithoutBankgiroNumber;
			return new Subscription[]{}
				.Append(generateItem.GenerateRange(1, 7)
				.Append(generateItemWithoutBankGiroNumber.GenerateRange(1, 8)));
		}

		public static Subscription Get(int id)
		{
			var mockedSubscription = new Mock<Subscription>();
			mockedSubscription.SetupGet(x => x.Id).Returns(id);
			mockedSubscription.SetupProperty(x => x.Customer.PersonalIdNumber, "197502065019");
			mockedSubscription.SetupProperty(x => x.Customer.FirstName, "Adam");
			mockedSubscription.SetupProperty(x => x.Customer.LastName, "Bertil");
			mockedSubscription.SetupProperty(x => x.PaymentInfo.ClearingNumber, "0123");
			mockedSubscription.SetupProperty(x => x.PaymentInfo.AccountNumber, "12345678");
			mockedSubscription.SetupProperty(x => x.PaymentInfo.MonthlyAmount, 399);
			mockedSubscription.SetupProperty(x => x.PaymentInfo.PaymentSentDate, null);
			mockedSubscription.SetupProperty(x => x.ConsentStatus);
			mockedSubscription.SetupProperty(x => x.ActivatedDate);
			mockedSubscription.SetupProperty(x => x.BankgiroPayerNumber, 5);
			return mockedSubscription.Object;
		}

		public static Subscription GetWithoutBankgiroNumber(int id)
		{
			var subscription = Get(id);
			subscription.BankgiroPayerNumber = null;
			return subscription;
		}
	}
}