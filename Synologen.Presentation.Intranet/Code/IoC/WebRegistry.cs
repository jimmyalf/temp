using Spinit.Data;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.ContractSales;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.FrameOrder;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Data;
using Spinit.Wpc.Synologen.Data.Repositories.ContractSalesRepositories;
using Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters;
using Spinit.Wpc.Synologen.Data.Repositories.FrameOrderRepositories;
using Spinit.Wpc.Synologen.Data.Repositories.LensSubscriptionRepositories;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Services;
using StructureMap.Configuration.DSL;
using IShopRepository = Spinit.Wpc.Synologen.Core.Domain.Persistence.FrameOrder.IShopRepository;
using ShopRepository = Spinit.Wpc.Synologen.Data.Repositories.FrameOrderRepositories.ShopRepository;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Code.IoC
{
	public class WebRegistry : Registry
	{
		public WebRegistry()
		{
			//Register repositories
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
			var connectionString = Utility.Business.Globals.ConnectionString(Utility.Business.Globals.ConnectionName);
			For<ISqlProvider>().Use(() => new SqlProvider(connectionString));
			For<ISettlementRepository>().Use<SettlementRepository>();
			For<ITransactionArticleRepository>().Use<TransactionArticleRepository>();

			// Register GUI and settings services
			For<ISynologenMemberService>().Use<SynologenMemberService>();
			For<IFrameOrderService>().Use<SynologenFrameOrderService>();
			For<IEmailService>().Use<EmailService>();
			For<ISynologenSettingsService>().Use<SynologenSettingsService>();
		    For<IYammerService>().Use<YammerService>();

			//For<IActionCriteriaConverter<AllOrderableFramesCriteria, ICriteria>>().Use<AllOrderableFramesCriteriaConverter>();
			//For<IActionCriteriaConverter<AllFrameOrdersForShopCriteria, ICriteria>>().Use<AllFrameOrdersForShopCriteriaConverter>();
			//For<IActionCriteriaConverter<CustomersForShopMatchingCriteria, ICriteria>>().Use<CustomersForShopMatchingCriteriaConverter>();
			//For<IActionCriteriaConverter<TransactionsForSubscriptionMatchingCriteria, ICriteria>>().Use<TransactionsForSubscriptionMatchingCriteriaConverter>();
			//For<IActionCriteriaConverter<AllUnhandledSubscriptionErrorsForShopCriteria, ICriteria>>().Use<AllUnhandledSubscriptionErrorsForShopCriteriaConverter>();
			//For<IActionCriteriaConverter<AllActiveTransactionArticlesCriteria, ICriteria>>().Use<AllActiveTransactionArticlesCriteriaConverter>();

			// Register criteria converters
			Scan(x =>
			{
				x.AssemblyContainingType<PageOfFramesMatchingCriteriaConverter>();
				x.Assembly(typeof(NHibernateActionCriteriaConverter<,>).Assembly.FullName);
				x.ConnectImplementationsToTypesClosing(typeof(IActionCriteriaConverter<,>));
			});
		}
	}
}