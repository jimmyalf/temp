using System.ServiceModel;

namespace Synologen.Service.Web.External.AcceptanceTest.Domain
{
	[ServiceContract(Name = "AddCustomerService", Namespace = ServiceSettings.Namespace)]
	public interface IAddCustomerService
	{
		[OperationContract] AddEntityResponse AddCustomer(AuthenticationContext authenticationContext, Customer customer);
	}
}