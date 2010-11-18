using System;
using System.Collections.Generic;
using Moq;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.EventArguments.LensSubscription;

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
							   "08.45634235", "Solvägen 25", "3A", "Stockholm", "533 21", "Fritextfält", countryId, shopId);
		}

		public static SaveCustomerEventArgs GetSaveCustomerEventArgs(Customer customer)
		{
			return new SaveCustomerEventArgs
			{
				AddressLineOne = customer.Address.AddressLineOne,
				AddressLineTwo = customer.Address.AddressLineTwo,
				City = customer.Address.City,
				PostalCode = customer.Address.PostalCode,
				CountryId = customer.Address.Country.Id,
				Email = customer.Contact.Email,
				FirstName = customer.FirstName,
				LastName = customer.LastName,
				MobilePhone = customer.Contact.MobilePhone,
				PersonalIdNumber = customer.PersonalIdNumber,
				Phone = customer.Contact.Phone,
				Notes = customer.Notes
			};
		}

		public static SaveCustomerEventArgs GetSaveCustomerEventArgs(int countryId) 
		{ 
			return  new SaveCustomerEventArgs
			{
				FirstName = "Carina",
				LastName = "Melander",
				AddressLineOne = "Vinkelslipsgatan 32",
				AddressLineTwo = "Uppgång 3H",
				City = "Storstad",
				CountryId = countryId,
				Email = "carina.melander@gmail.com",
				MobilePhone = "0704-565675",
				PersonalIdNumber = "8106296729",
				Phone = "0783-45674537",
				PostalCode = "688 44",
				Notes = "Här kan man skriva vad man vill."
			};
		}

		private static Customer GetCustomer(int id, string firstName, string lastName, string personalIdNumber, int shopId)
		{
			var mockedShop = new Mock<Shop>();
			mockedShop.SetupGet(x => x.Id).Returns(shopId);
			mockedShop.SetupGet(x => x.Name).Returns("Butik");
			return GetCustomer(id, firstName, lastName, personalIdNumber, mockedShop);
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
												string notes,
												int countryId,
												int shopId)
		{
			var mockedCustomer = new Mock<Customer>();
			mockedCustomer.SetupGet(x => x.Id).Returns(id);
			mockedCustomer.SetupGet(x => x.FirstName).Returns(firstName);
			mockedCustomer.SetupGet(x => x.LastName).Returns(lastName);
			mockedCustomer.SetupGet(x => x.PersonalIdNumber).Returns(personalIdNumber);
			mockedCustomer.SetupGet(x => x.Notes).Returns(notes);
			mockedCustomer.SetupGet(x => x.Shop).Returns(ShopFactory.Get(shopId));

			var mockedCountry = CountryFactory.Get(countryId);

			var address = new CustomerAddress
			{
				AddressLineOne = addressLineOne,
				AddressLineTwo = addressLineTwo,
				City = city,
				Country = mockedCountry,
				PostalCode = postalCode,
			};

			var contact = new CustomerContact
			{
				Email = email, 
				MobilePhone = mobilePhone, 
				Phone = phone
			};

			mockedCustomer.SetupGet(x => x.Address).Returns(address);
			mockedCustomer.SetupGet(x => x.Contact).Returns(contact);
			mockedCustomer.SetupGet(x => x.Subscriptions).Returns(SubscriptionFactory.GetList(mockedCustomer.Object));

			return mockedCustomer.Object;
		}

	}
}
