using System.ServiceModel;

namespace Synologen.Service.Web.External.AcceptanceTest.Domain
{
	public class AddCustomerClient : ClientBase<IAddCustomerService>, IAddCustomerService
	{
		public void AddCustomer(AuthenticationContext authenticationContext, Customer customer)
		{
			Channel.AddCustomer(authenticationContext, customer);
		}
	}
}