using System.Web.Mvc;
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
using Spinit.Wpc.Synologen.Presentation.Application.Services;
using StructureMap.Configuration.DSL;

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
			For<ISubscriptionRepository>().HybridHttpOrThreadLocalScoped().Use<SubscriptionRepository>();
			For<ISettlementRepository>().HybridHttpOrThreadLocalScoped().Use<SettlementRepository>();
			For<IContractSaleRepository>().HybridHttpOrThreadLocalScoped().Use<ContractSaleRepository>();
			For<ITransactionRepository>().HybridHttpOrThreadLocalScoped().Use<TransactionRepository>();
			For<ICustomerRepository>().HybridHttpOrThreadLocalScoped().Use<CustomerRepository>();
			For<ITransactionArticleRepository>().HybridHttpOrThreadLocalScoped().Use<TransactionArticleRepository>();
			var connectionString = Utility.Business.Globals.ConnectionString(Utility.Business.Globals.ConnectionName);
			For<ISqlProvider>().Use(() => new SqlProvider(connectionString));
			

			// Register criteria converters
			Scan(x =>
			{
				x.AssemblyContainingType<PageOfFramesMatchingCriteriaConverter>();
				x.Assembly(typeof(NHibernateActionCriteriaConverter<,>).Assembly.FullName);
				x.ConnectImplementationsToTypesClosing(typeof(IActionCriteriaConverter<,>));
			});

			//For<IActionCriteriaConverter<PageOfFramesMatchingCriteria, ICriteria>>().Use<PageOfFramesMatchingCriteriaConverter>();
			//For<IActionCriteriaConverter<PagedSortedCriteria, ICriteria>>().Use<PagedSortedCriteriaConverter>();
			//For<IActionCriteriaConverter<PageOfFrameOrdersMatchingCriteria, ICriteria>>().Use<PageOfFrameOrdersMatchingCriteriaConverter>();
			//For<IActionCriteriaConverter<PageOfSubscriptionsMatchingCriteria, ICriteria>>().Use<PageOfSubscriptionsMatchingCriteriaConverter>();
			//For<IActionCriteriaConverter<AllContractSalesMatchingCriteria, ICriteria>>().Use<AllContractSalesMatchingCriteriaConverter>();
			//For<IActionCriteriaConverter<AllTransactionsMatchingCriteria, ICriteria>>().Use<AllTransactionsMatchingCriteriaConverter>();
			//For<IActionCriteriaConverter<PageOfTransactionArticlesMatchingCriteria, ICriteria>>().Use<PageOfTransactionArticlesMatchingCriteriaConverter>();
			
			// Register GUI and settings services
			For<IAdminSettingsService>().Use<SettingsService>();
			For<IGridSortPropertyMappingService>().Use<SynologenGridSortPropertyMappingSerice>();
			For<ILensSubscriptionViewService>().HybridHttpOrThreadLocalScoped().Use<LensSubscriptionViewService>();
			For<IContractSalesViewService>().HybridHttpOrThreadLocalScoped().Use<ContractSalesViewService>();
		}
	}
}