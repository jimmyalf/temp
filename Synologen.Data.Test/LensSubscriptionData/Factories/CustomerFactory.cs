using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Spinit.Wpc.Synologen.Data.Test.LensSubscriptionData.Factories
{
	public static class CustomerFactory {
		
		public static Customer Get(Country country, Shop shop)
		{

			return new Customer
			{
				Address = new CustomerAddress
				{
					AddressLineOne = "Datavägen 2",
					AddressLineTwo = "Box 416 57",
					City = "Gävle",
					Country = country,
					PostalCode = "436 32"
				},
				Contact = new CustomerContact
				{
					Email = "paiv@home.se",
					MobilePhone = "0702624715",
					Phone = "0322-16660"
				},
				FirstName = "Sune",
				LastName = "Mangs",
				PersonalIdNumber = "197301146069",
				Shop = shop,
				Notes = "Till varje kund hör ett anteckningsfält"
			};
		}


		public static Customer Get(Country country, Shop shop, string firstName, string lastName, string personalIdNumber)
		{

			return new Customer
			{
				Address = new CustomerAddress
				{
					AddressLineOne = "Datavägen 2",
					AddressLineTwo = "Box 416 57",
					City = "Gävle",
					Country = country,
					PostalCode = "436 32"
				},
				Contact = new CustomerContact
				{
					Email = "paiv@home.se",
					MobilePhone = "0702624715",
					Phone = "0322-16660"
				},
				FirstName = firstName,
				LastName = lastName,
				PersonalIdNumber = personalIdNumber,
				Shop = shop,
				Notes = "Till varje kund hör ett anteckningsfält"
			};
		}

		public static Customer Edit(Customer customer)
		{
			customer.Address.AddressLineOne = customer.Address.AddressLineOne.Reverse();
			customer.Address.AddressLineTwo = customer.Address.AddressLineTwo.Reverse();
			customer.Address.City = customer.Address.City.Reverse();
			customer.Address.PostalCode = customer.Address.PostalCode.Reverse();
			customer.Contact.Email = customer.Contact.Email.Reverse();
			customer.Contact.MobilePhone = customer.Contact.MobilePhone.Reverse();
			customer.Contact.Phone = customer.Contact.Phone.Reverse();
			customer.FirstName = customer.FirstName.Reverse();
			customer.LastName = customer.LastName.Reverse();
			customer.PersonalIdNumber = customer.PersonalIdNumber.Reverse();
			customer.Notes = customer.Notes.Reverse();

			return customer;
		}
	}
}