using NHibernate;
using Spinit.Data;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.FrameOrder;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Data;
using Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters;
using Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters.LensSubscription;
using Spinit.Wpc.Synologen.Data.Repositories.FrameOrderRepositories;
using Spinit.Wpc.Synologen.Data.Repositories.LensSubscriptionRepositories;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Services;
using StructureMap.Configuration.DSL;
using IShopRepository=Spinit.Wpc.Synologen.Core.Domain.Persistence.FrameOrder.IShopRepository;
using ShopRepository=Spinit.Wpc.Synologen.Data.Repositories.FrameOrderRepositories.ShopRepository;

namespace Spinit.Wpc.Synologen.Presentation.Site
{
	public class SynologenSiteRegistry : Registry
	{
		public SynologenSiteRegistry()
		{
			For<IFrameRepository>().HybridHttpOrThreadLocalScoped().Use<FrameRepository>();
			For<IFrameGlassTypeRepository>().HybridHttpOrThreadLocalScoped().Use<FrameGlassTypeRepository>();
			For<IFrameOrderRepository>().HybridHttpOrThreadLocalScoped().Use<FrameOrderRepository>();
			For<IShopRepository>().HybridHttpOrThreadLocalScoped().Use<ShopRepository>();
			
			For<ICustomerRepository>().HybridHttpOrThreadLocalScoped().Use<CustomerRepository>();
			For<ISubscriptionRepository>().HybridHttpOrThreadLocalScoped().Use<SubscriptionRepository>();
			For<ICountryRepository>().HybridHttpOrThreadLocalScoped().Use<CountryRepository>();
			For<Core.Domain.Persistence.LensSubscription.IShopRepository>().HybridHttpOrThreadLocalScoped().Use<Data.Repositories.LensSubscriptionRepositories.ShopRepository>();
			For<ITransactionRepository>().HybridHttpOrThreadLocalScoped().Use<TransactionRepository>();
			For<ISubscriptionErrorRepository>().HybridHttpOrThreadLocalScoped().Use<SubscriptionErrorRepository>();
			For<IFrameOrderService>().Use<SynologenFrameOrderService>();
			For<ISynologenSettingsService>().Use<SynologenSettingsService>();
			For<IEmailService>().Use<EmailService>();
			For<ISynologenMemberService>().Use<SynologenMemberService>();
			var connectionString = Utility.Business.Globals.ConnectionString(Utility.Business.Globals.ConnectionName);
			For<ISqlProvider>().Use(() => new SqlProvider(connectionString));

			For<IActionCriteriaConverter<AllOrderableFramesCriteria, ICriteria>>().Use<AllOrderableFramesCriteriaConverter>();
			For<IActionCriteriaConverter<AllFrameOrdersForShopCriteria, ICriteria>>().Use<AllFrameOrdersForShopCriteriaConverter>();
			For<IActionCriteriaConverter<CustomersForShopMatchingCriteria, ICriteria>>().Use<CustomersForShopMatchingCriteriaConverter>();
		}
	}
}