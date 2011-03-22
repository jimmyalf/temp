using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;

namespace Synologen.Presentation.AcceptanceTest.LensSubscription
{
	public static class Factory
	{	
		public static  Customer CreateCustomer(Country country, Shop shop)
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
	}
}