using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Services
{
	public interface IViewParser
	{
		OrderCustomer Parse(PickCustomerEventArgs args);
	}

	public class ViewParser : IViewParser
	{
		public OrderCustomer Parse(PickCustomerEventArgs args)
		{
			return new OrderCustomer {AddressLineOne = args.AddressLineOne, AddressLineTwo = args.AddressLineTwo, City = args.City, Email = args.Email, FirstName = args.FirstName, LastName = args.LastName, MobilePhone = args.MobilePhone, Notes = args.Notes, PersonalIdNumber = args.PersonalIdNumber, Phone = args.Phone, PostalCode = args.PostalCode};
		}
	}
}