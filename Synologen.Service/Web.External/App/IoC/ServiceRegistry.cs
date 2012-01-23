using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Services.Web.External;
using Spinit.Wpc.Synologen.Data.Repositories.OrderRepositories;
using StructureMap.Configuration.DSL;
using Synologen.Service.Web.External.App.Services;

namespace Synologen.Service.Web.External.App.IoC
{
	public class ServiceRegistry : Registry
	{
		public ServiceRegistry()
		{
			For<IShopAuthenticationService>().Use<ShopAuthenticationService>();
			For<IOrderCustomerRepository>().Use<OrderCustomerRepository>();
			For<ICustomerParser>().Use<CustomerParser>();
			For<ICustomerValidator>().Use<CustomerValidator>();
		}
	}
}