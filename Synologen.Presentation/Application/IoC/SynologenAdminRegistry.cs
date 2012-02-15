using System.Web.Mvc;
using Spinit.Data;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Business;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.ContractSales;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.FrameOrder;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Data;
using Spinit.Wpc.Synologen.Data.DataServices;
using Spinit.Wpc.Synologen.Data.Repositories.ContractSalesRepositories;
using Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters;
using Spinit.Wpc.Synologen.Data.Repositories.FrameOrderRepositories;
using Spinit.Wpc.Synologen.Data.Repositories.LensSubscriptionRepositories;
using Spinit.Wpc.Synologen.Data.Repositories.OrderRepositories;
using Spinit.Wpc.Synologen.Presentation.Application.Services;
using StructureMap.Configuration.DSL;
using ContractSales_IArticleRepository = Spinit.Wpc.Synologen.Core.Domain.Persistence.ContractSales.IArticleRepository;
using ContractSales_ArticleRepository = Spinit.Wpc.Synologen.Data.Repositories.ContractSalesRepositories.ArticleRepository;
using ITransactionRepository = Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription.ITransactionRepository;
using Order_IArticleRepository = Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders.IArticleRepository;
using Order_ArticleRepository = Spinit.Wpc.Synologen.Data.Repositories.OrderRepositories.ArticleRepository;
using LensSubscription_ISubscriptionRepository = Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription.ISubscriptionRepository;
using LensSubscription_SubscriptionRepository = Spinit.Wpc.Synologen.Data.Repositories.LensSubscriptionRepositories.SubscriptionRepository;

namespace Spinit.Wpc.Synologen.Presentation.Application.IoC
{
	public class SynologenAdminRegistry : Registry
	{
		public SynologenAdminRegistry()
		{
			// Register Controllers
			Scan(x =>
			{
				x.AssemblyContainingType<SynologenAdminRegistry>();
				x.AddAllTypesOf<IController>().NameBy(c => c.Name);
			});

			//Register repositories
			For<IFrameRepository>().HybridHttpOrThreadLocalScoped().Use<FrameRepository>();
			For<IFrameColorRepository>().HybridHttpOrThreadLocalScoped().Use<FrameColorRepository>();
			For<IFrameBrandRepository>().HybridHttpOrThreadLocalScoped().Use<FrameBrandRepository>();
			For<IFrameGlassTypeRepository>().HybridHttpOrThreadLocalScoped().Use<FrameGlassTypeRepository>();
			For<IFrameOrderRepository>().HybridHttpOrThreadLocalScoped().Use<FrameOrderRepository>();
			For<LensSubscription_ISubscriptionRepository>().HybridHttpOrThreadLocalScoped().Use<LensSubscription_SubscriptionRepository>();
			For<ISettlementRepository>().HybridHttpOrThreadLocalScoped().Use<SettlementRepository>();
			For<IContractSaleRepository>().HybridHttpOrThreadLocalScoped().Use<ContractSaleRepository>();
			For<ITransactionRepository>().HybridHttpOrThreadLocalScoped().Use<Data.Repositories.LensSubscriptionRepositories.TransactionRepository>();
			For<ICustomerRepository>().HybridHttpOrThreadLocalScoped().Use<CustomerRepository>();
			For<ITransactionArticleRepository>().HybridHttpOrThreadLocalScoped().Use<TransactionArticleRepository>();
			For<ContractSales_IArticleRepository>().HybridHttpOrThreadLocalScoped().Use<ContractSales_ArticleRepository>();
			
            //Order Repositories
			For<IOrderRepository>().HybridHttpOrThreadLocalScoped().Use<OrderRepository>();
			For<IArticleCategoryRepository>().HybridHttpOrThreadLocalScoped().Use<ArticleCategoryRepository>();
			For<IArticleSupplierRepository>().HybridHttpOrThreadLocalScoped().Use<ArticleSupplierRepository>();
			For<IArticleTypeRepository>().HybridHttpOrThreadLocalScoped().Use<ArticleTypeRepository>();
			For<Order_IArticleRepository>().HybridHttpOrThreadLocalScoped().Use<Order_ArticleRepository>();
			var connectionString = Utility.Business.Globals.ConnectionString(Utility.Business.Globals.ConnectionName);
			For<ISqlProvider>().Use(() => new SqlProvider(connectionString));
			
            // Register criteria converters
			Scan(x =>
			{
				x.AssemblyContainingType<PageOfFramesMatchingCriteriaConverter>();
				x.Assembly(typeof(NHibernateActionCriteriaConverter<,>).Assembly.FullName);
				x.ConnectImplementationsToTypesClosing(typeof(IActionCriteriaConverter<,>));
			});

			// Register GUI and settings services
			For<IAdminSettingsService>().Use<SettingsService>();
			For<IUserContextService>().Use<UserContextService>();
			For<IGridSortPropertyMappingService>().Use<SynologenGridSortPropertyMappingSerice>();
			For<ILensSubscriptionViewService>().HybridHttpOrThreadLocalScoped().Use<LensSubscriptionViewService>();
			For<IContractSalesViewService>().HybridHttpOrThreadLocalScoped().Use<ContractSalesViewService>();
			For<IContractSalesCommandService>().HybridHttpOrThreadLocalScoped().Use<ContractSalesCommandService>();
			For<IInvoiceReportViewService>().Use<InvoiceReportViewService>();
			For<IOrderViewParser>().Use<OrderViewParser>();
		}
	}
}