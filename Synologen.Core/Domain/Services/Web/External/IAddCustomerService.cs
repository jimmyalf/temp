using System.ServiceModel;

namespace Spinit.Wpc.Synologen.Core.Domain.Services.Web.External
{
	[ServiceContract]
	public interface IAddCustomerService
	{
		[OperationContract] void AddCustomer(AuthenticationContext authenticationContext, Customer customer);
	}
}