using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Services.Web.External;

namespace Synologen.Service.Web.External.App.Services
{
	public class CustomerParser : ICustomerParser 
	{
		public OrderCustomer Parse(Customer customer, Shop shop)
		{
			if(customer == null) return null;
			return new OrderCustomer
			{
				AddressLineOne = customer.Address1,
				AddressLineTwo = customer.Address2,
				City = customer.City,
				Email = customer.Email,
				FirstName = customer.FirstName,
				LastName = customer.LastName,
				MobilePhone = customer.MobilePhone,
				PersonalIdNumber = customer.PersonalNumber,
				Phone = customer.Phone,
				PostalCode = customer.PostalCode,
				Shop = shop
			};
		}
	}
}