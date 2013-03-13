#define DEBUG
using Spinit.Data;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Core.UI.Tasks;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.ContractSales;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.FrameOrder;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Data;
using Spinit.Wpc.Synologen.Data.Repositories.ContractSalesRepositories;
using Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters;
using Spinit.Wpc.Synologen.Data.Repositories.FrameOrderRepositories;
using Spinit.Wpc.Synologen.Data.Repositories.LensSubscriptionRepositories;
using Spinit.Wpc.Synologen.Data.Repositories.OrderRepositories;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Services;
using StructureMap.Configuration.DSL;
using ArticleRepository = Spinit.Wpc.Synologen.Data.Repositories.OrderRepositories.ArticleRepository;
using IArticleRepository = Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders.IArticleRepository;
using IShopRepository = Spinit.Wpc.Synologen.Core.Domain.Persistence.FrameOrder.IShopRepository;
using ISubscriptionErrorRepository = Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription.ISubscriptionErrorRepository;
using ISubscriptionRepository = Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription.ISubscriptionRepository;
using ITransactionRepository = Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription.ITransactionRepository;
using ShopRepository = Spinit.Wpc.Synologen.Data.Repositories.FrameOrderRepositories.ShopRepository;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Code.IoC
{
	public class WebRegistry : Registry
	{
		public WebRegistry()
		{
			//Register repositories
			For<IFrameRepository>().HybridHttpOrThreadLocalScoped().Use<FrameRepository>();
            For<IFrameSupplierRepository>().HybridHttpOrThreadLocalScoped().Use<FrameSupplierRepository>();
			For<IFrameGlassTypeRepository>().HybridHttpOrThreadLocalScoped().Use<FrameGlassTypeRepository>();
			For<IFrameOrderRepository>().HybridHttpOrThreadLocalScoped().Use<FrameOrderRepository>();
			For<IShopRepository>().HybridHttpOrThreadLocalScoped().Use<ShopRepository>();
			For<ICustomerRepository>().HybridHttpOrThreadLocalScoped().Use<CustomerRepository>();
			For<ISubscriptionRepository>().HybridHttpOrThreadLocalScoped().Use<Data.Repositories.LensSubscriptionRepositories.SubscriptionRepository>();
			For<ICountryRepository>().HybridHttpOrThreadLocalScoped().Use<CountryRepository>();
			For<Core.Domain.Persistence.LensSubscription.IShopRepository>().HybridHttpOrThreadLocalScoped().Use<Data.Repositories.LensSubscriptionRepositories.ShopRepository>();
			For<ITransactionRepository>().HybridHttpOrThreadLocalScoped().Use<Data.Repositories.LensSubscriptionRepositories.TransactionRepository>();
			For<ISubscriptionErrorRepository>().HybridHttpOrThreadLocalScoped().Use<Data.Repositories.LensSubscriptionRepositories.SubscriptionErrorRepository>();
			var connectionString = Utility.Business.Globals.ConnectionString(Utility.Business.Globals.ConnectionName);
			For<ISqlProvider>().Use(() => new SqlProvider(connectionString));
			For<ISettlementRepository>().Use<SettlementRepository>();
			For<ITransactionArticleRepository>().Use<TransactionArticleRepository>();
			For<IOrderCustomerRepository>().Use<OrderCustomerRepository>();
		    For<IOrderRepository>().Use<OrderRepository>();
		    For<IArticleCategoryRepository>().Use<ArticleCategoryRepository>();
		    For<IArticleSupplierRepository>().Use<ArticleSupplierRepository>();
		    For<IArticleTypeRepository>().Use<ArticleTypeRepository>();
            For<IArticleRepository>().Use<ArticleRepository>();
		    For<ILensRecipeRepository>().Use<LensRecipeRepository>();
			//Order Repositories
			For<Core.Domain.Persistence.Orders.ISubscriptionRepository>().Use<Data.Repositories.OrderRepositories.SubscriptionRepository>();
			For<Core.Domain.Persistence.Orders.IShopRepository>().Use<Data.Repositories.OrderRepositories.ShopRepository>();
			For<Core.Domain.Persistence.Orders.ISubscriptionItemRepository>().Use<Data.Repositories.OrderRepositories.SubscriptionItemRepository>();
			For<Core.Domain.Persistence.Orders.ITransactionRepository>().Use<Data.Repositories.OrderRepositories.TransactionRepository>();
			For<Core.Domain.Persistence.Orders.ISubscriptionErrorRepository>().Use<Data.Repositories.OrderRepositories.SubscriptionErrorRepository>();

			// Register GUI and settings services
			For<ISynologenMemberService>().Use<SynologenMemberService>();
			For<IFrameOrderService>().Use<SynologenFrameOrderService>();
			For<IEmailService>().Use<EmailService>();
			For<ISynologenSettingsService>().Use<SynologenSettingsService>();
		    For<IYammerService>().Use<YammerService>();
			For<IViewParser>().Use<ViewParser>();
			#if DEBUG
			For<IRoutingService>().Singleton().Use<DebugRoutingService>();
			#else
			For<IRoutingService>().Singleton().Use<CachedRoutingService>();
			#endif

			// Register criteria converters
			Scan(x =>
			{
				x.AssemblyContainingType<PageOfFramesMatchingCriteriaConverter>();
				x.Assembly(typeof(NHibernateActionCriteriaConverter<,>).Assembly.FullName);
				x.ConnectImplementationsToTypesClosing(typeof(IActionCriteriaConverter<,>));
			});

			//Scan request events
			Scan(x =>
			{
				x.AssemblyContainingType<WebRegistry>();
				x.AddAllTypesOf<IWpcApplicationEventTask>();
			});
		}
	}
}