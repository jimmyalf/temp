using System.ServiceModel;

namespace Spinit.Wpc.Synologen.Core.Domain.Services.Web.External
{
	[ServiceContract(Name = "AddCustomerService", Namespace = ServiceSettings.Namespace)]
	public interface IAddCustomerService
	{
		[OperationContract] AddEntityResponse AddCustomer(AuthenticationContext authenticationContext, Customer customer);
	}
}