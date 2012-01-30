using System.ServiceModel;

namespace Synologen.Service.Web.External.AcceptanceTest.Domain
{
	public class AddCustomerClient : ClientBase<IAddCustomerService>, IAddCustomerService
	{
		public AddEntityResponse AddCustomer(AuthenticationContext authenticationContext, Customer customer)
		{
			return Channel.AddCustomer(authenticationContext, customer);
		}
	}
}