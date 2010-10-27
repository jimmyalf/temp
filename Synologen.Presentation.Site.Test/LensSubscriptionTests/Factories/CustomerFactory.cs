using System.Collections.Generic;
using Moq;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;

namespace Spinit.Wpc.Synologen.Presentation.Site.Test.LensSubscriptionTests.Factories
{
	public static class CustomerFactory
	{
		public static IEnumerable<Customer> GetList()
		{
			return new[]
			{
				GetCustomer(1, "Eva", "Bergström", "8407143778", 1), 
				GetCustomer(2, "Lasse", "Larsson", "5406011857", 1),
				GetCustomer(3, "Lotta", "Olsson", "4906103207", 1),
			};
		}
		public static Customer Get(int id)
		{
			return GetCustomer(id, "Eva", "Bergström", "8407143778", 1);
		}
		public static Customer Get(int id, int shopId)
		{
			return GetCustomer(id, "Eva", "Bergström", "8407143778", shopId);
		}
		public static Customer Get(int id, Mock<Shop> mockedShop)
		{
			return GetCustomer(id, "Eva", "Bergström", "8407143778", mockedShop);
		}
		public static Customer Get(int customerid, int countryId, int shopId)
		{
			return GetCustomer(customerid, "Birgitta", "Wennerberg", "641011-7061", "birgwen@gmail.com", "0708.456435",
							   "08.45634235", "Solvägen 25", "3A", "Stockholm", "533 21", countryId, shopId);
		}

		private static Customer GetCustomer(int id, string firstName, string lastName, string personalIdNumber, int shopId)
		{
			var mockedCustomer = new Mock<Customer>();
			mockedCustomer.SetupGet(x => x.Id).Returns(id);
			mockedCustomer.SetupGet(x => x.FirstName).Returns(firstName);
			mockedCustomer.SetupGet(x => x.LastName).Returns(lastName);
			mockedCustomer.SetupGet(x => x.PersonalIdNumber).Returns(personalIdNumber);
			var mockedShop = new Mock<Shop>();
			mockedShop.SetupGet(x => x.Id).Returns(shopId);
			mockedShop.SetupGet(x => x.Name).Returns("Butik");
			mockedCustomer.SetupGet(x => x.Shop).Returns(mockedShop.Object);
			return mockedCustomer.Object;
		}

		private static Customer GetCustomer(int id, string firstName, string lastName, string personalIdNumber, Mock<Shop> mockedShop)
		{
			var mockedCustomer = new Mock<Customer>();
			mockedCustomer.SetupGet(x => x.Id).Returns(id);
			mockedCustomer.SetupGet(x => x.FirstName).Returns(firstName);
			mockedCustomer.SetupGet(x => x.LastName).Returns(lastName);
			mockedCustomer.SetupGet(x => x.PersonalIdNumber).Returns(personalIdNumber);
			mockedCustomer.SetupGet(x => x.Shop).Returns(mockedShop.Object);
			return mockedCustomer.Object;
		}

		private static Customer GetCustomer(int id,
												string firstName,
												string lastName,
												string personalIdNumber,
												string email,
												string mobilePhone,
												string phone,
												string addressLineOne,
												string addressLineTwo,
												string city,
												string postalCode,
												int countryId,
												int shopId)
		{
			var mockedCustomer = new Mock<Customer>();
			mockedCustomer.SetupGet(x => x.Id).Returns(id);
			mockedCustomer.SetupGet(x => x.FirstName).Returns(firstName);
			mockedCustomer.SetupGet(x => x.LastName).Returns(lastName);
			mockedCustomer.SetupGet(x => x.PersonalIdNumber).Returns(personalIdNumber);

			var mockedShop = new Mock<Shop>();
			mockedShop.SetupGet(x => x.Id).Returns(shopId);
			mockedShop.SetupGet(x => x.Name).Returns("Butik");
			mockedCustomer.SetupGet(x => x.Shop).Returns(mockedShop.Object);

			var mockedCountry = CountryFactory.Get(countryId);

			var mockedAddress = new Mock<CustomerAddress>();
			mockedAddress.SetupGet(x => x.AddressLineOne).Returns(addressLineOne);
			mockedAddress.SetupGet(x => x.AddressLineTwo).Returns(addressLineTwo);
			mockedAddress.SetupGet(x => x.City).Returns(city);
			mockedAddress.SetupGet(x => x.Country).Returns(mockedCountry);
			mockedAddress.SetupGet(x => x.PostalCode).Returns(postalCode);

			var mockedContact = new Mock<CustomerContact>();
			mockedContact.SetupGet(x => x.Email).Returns(email);
			mockedContact.SetupGet(x => x.MobilePhone).Returns(mobilePhone);
			mockedContact.SetupGet(x => x.Phone).Returns(phone);

			mockedCustomer.SetupGet(x => x.Address).Returns(mockedAddress.Object);
			mockedCustomer.SetupGet(x => x.Contact).Returns(mockedContact.Object);
			mockedCustomer.SetupGet(x => x.Subscriptions).Returns(SubscriptionFactory.GetList(mockedCustomer.Object));

			return mockedCustomer.Object;
		}
	}
}
