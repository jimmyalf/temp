using System;
using System.Collections.Generic;
using Moq;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Extensions;
using Country=Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription.Country;
using Shop=Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription.Shop;

namespace Spinit.Wpc.Synologen.Presentation.Test.Factories.LensSubscription
{
	public static class SubscriptionFactory 
	{
		private const bool Subscription_Is_Active = true;
		private const bool Subscription_Not_Active = false;

		public static IEnumerable<Subscription> GetList() 
		{
			for (var i = 0; i < 50; i++)
			{
				bool isActive = (i % 2) == 1 ? Subscription_Is_Active : Subscription_Not_Active;
				yield return Get(i, isActive);
			}
			yield break;
		}

		public static Subscription Get(int id)
		{
			return Get(id, Subscription_Is_Active);
		}

		public static Subscription Get(int id, bool isActive)
		{
			var customer = new Customer
			{
				FirstName = "Adam " + id.GetChar(),
				LastName = "Bertil " + id.GetChar(),
                Shop = new Shop{ Name = "Optiker " + id.GetChar()},
			};
			var mockedSubscription = new Mock<Subscription>();
			mockedSubscription.SetupGet(x => x.Id).Returns(id);
			mockedSubscription.SetupGet(x => x.Customer).Returns(customer);
			mockedSubscription.SetupGet(x => x.Active).Returns(isActive);
			return mockedSubscription.Object;
		}

		public static Subscription GetFull(int id)
		{
			var customer = new Customer
			{
				FirstName = "Adam",
				LastName = "Bertil",
                Shop = new Shop{ Name = "Optiker ABC"},
				Address = new CustomerAddress
				{
					AddressLineOne = "Datavägen 2",
                    AddressLineTwo = "Box 123",
                    City = "Askim",
					Country = new Country{ Name = "Sverige"},
                    PostalCode = "43632",
				}
				,
				Contact = new CustomerContact
				{
					Email = "info@spinit.se",
                    MobilePhone = "0708-223344",
                    Phone = "031-7483008",
				},
                PersonalIdNumber = "197010245467",
			};
			var paymentInfo = new SubscriptionPaymentInfo
			{
				AccountNumber = "123456789",
				ClearingNumber = "3300",
				MonthlyAmount = 588.65M
			};
			
			var mockedSubscription = new Mock<Subscription>();
			mockedSubscription.SetupGet(x => x.Id).Returns(id);
			mockedSubscription.SetupGet(x => x.ActivatedDate).Returns(new DateTime(2010,11,02));
			mockedSubscription.SetupGet(x => x.CreatedDate).Returns(new DateTime(2010,11,01));
			mockedSubscription.SetupProperty(x => x.PaymentInfo, paymentInfo);
			mockedSubscription.SetupGet(x => x.Transactions).Returns(SubscriptionTransactionFactory.GetList());
			mockedSubscription.SetupProperty(x => x.Customer, customer);
			mockedSubscription.SetupGet(x => x.Active).Returns(Subscription_Is_Active);
			mockedSubscription.SetupGet(x => x.Errors).Returns(SubscriptionErrorFactory.GetList());
			mockedSubscription.SetupProperty(x => x.Notes, "Lite text här");
			return mockedSubscription.Object;
		}
	}
}