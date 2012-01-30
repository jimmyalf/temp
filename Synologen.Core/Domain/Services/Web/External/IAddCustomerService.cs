using System.ServiceModel;

namespace Spinit.Wpc.Synologen.Core.Domain.Services.Web.External
{
	[ServiceContract(Name = "AddCustomerService", Namespace = "http://www.synologen.se/service.web.external/")]
	public interface IAddCustomerService
	{
		[OperationContract] AddEntityResponse AddCustomer(AuthenticationContext authenticationContext, Customer customer);
	}
}