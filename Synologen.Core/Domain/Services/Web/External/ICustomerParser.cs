using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;

namespace Spinit.Wpc.Synologen.Core.Domain.Services.Web.External
{
	public interface ICustomerParser
	{
		OrderCustomer Parse(Customer customer, Shop shop);
	}
}