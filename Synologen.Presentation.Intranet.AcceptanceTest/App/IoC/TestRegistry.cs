using NHibernate;
using Spinit.Data;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Core.Dependencies.NHibernate;
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
using Spinit.Wpc.Synologen.Presentation.Intranet.Code.Factories;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Services;
using StructureMap.Configuration.DSL;
using ArticleRepository = Spinit.Wpc.Synologen.Data.Repositories.OrderRepositories.ArticleRepository;
using IArticleRepository = Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders.IArticleRepository;
using IShopRepository = Spinit.Wpc.Synologen.Core.Domain.Persistence.FrameOrder.IShopRepository;
using ISubscriptionRepository = Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription.ISubscriptionRepository;
using ShopRepository = Spinit.Wpc.Synologen.Data.Repositories.FrameOrderRepositories.ShopRepository;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.App.IoC
{
	public class TestRegistry : Registry
	{
		public TestRegistry()
		{
			// NHibernate
			For<ISessionFactory>().Singleton().Use(NHibernateFactory.Instance.GetSessionFactory);
			For<ISession>().Use(x => x.GetInstance<ISessionFactory>().OpenSession());

			For<IFrameRepository>().HybridHttpOrThreadLocalScoped().Use<FrameRepository>();
			For<IFrameGlassTypeRepository>().HybridHttpOrThreadLocalScoped().Use<FrameGlassTypeRepository>();
			For<IFrameOrderRepository>().HybridHttpOrThreadLocalScoped().Use<FrameOrderRepository>();
			For<IShopRepository>().HybridHttpOrThreadLocalScoped().Use<ShopRepository>();
			For<ICustomerRepository>().HybridHttpOrThreadLocalScoped().Use<CustomerRepository>();
			For<ISubscriptionRepository>().HybridHttpOrThreadLocalScoped().Use<Data.Repositories.LensSubscriptionRepositories.SubscriptionRepository>();
			For<ICountryRepository>().HybridHttpOrThreadLocalScoped().Use<CountryRepository>();
			For<Core.Domain.Persistence.LensSubscription.IShopRepository>().HybridHttpOrThreadLocalScoped().Use<Data.Repositories.LensSubscriptionRepositories.ShopRepository>();
			For<ITransactionRepository>().HybridHttpOrThreadLocalScoped().Use<TransactionRepository>();
			For<ISubscriptionErrorRepository>().HybridHttpOrThreadLocalScoped().Use<SubscriptionErrorRepository>();
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
			For<Core.Domain.Persistence.Orders.ISubscriptionRepository>().Use<Data.Repositories.OrderRepositories.SubscriptionRepository>();
			For<IRoutingService>().Singleton().Use(RoutingServiceFactory.GetCachedRoutingService);
			
			

			// Register GUI and settings services
			For<ISynologenMemberService>().Use<SynologenMemberService>();
			For<IFrameOrderService>().Use<SynologenFrameOrderService>();
			For<IEmailService>().Use<EmailService>();
			For<ISynologenSettingsService>().Use<SynologenSettingsService>();
			For<IViewParser>().Use<ViewParser>();

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