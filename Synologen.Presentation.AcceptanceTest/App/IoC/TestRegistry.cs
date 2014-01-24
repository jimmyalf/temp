using System.Web.Mvc;
using NHibernate;
using Spinit.Data;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Core.Dependencies.NHibernate;
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
using Spinit.Wpc.Synologen.Presentation.AcceptanceTest.Helpers;
using Spinit.Wpc.Synologen.Presentation.Application.IoC;
using Spinit.Wpc.Synologen.Presentation.Application.Services;
using StructureMap.Configuration.DSL;

namespace Spinit.Wpc.Synologen.Presentation.AcceptanceTest.App.IoC
{
	public class TestRegistry : Registry
	{
		public TestRegistry()
		{
			// NHibernate
			For<ISessionFactory>().Singleton().Use(NHibernateFactory.Instance.GetSessionFactory);
			For<ISession>().Use(x => x.GetInstance<ISessionFactory>().OpenSession());

			// Register Controllers
			Scan(x => {
				x.AssemblyContainingType<SynologenAdminRegistry>();
				x.AddAllTypesOf<IController>().NameBy(c => c.Name);
			});

			//Register repositories
			For<IFrameRepository>().Use<FrameRepository>();
			For<IFrameColorRepository>().Use<FrameColorRepository>();
			For<IFrameBrandRepository>().Use<FrameBrandRepository>();
			For<IFrameGlassTypeRepository>().Use<FrameGlassTypeRepository>();
			For<IFrameOrderRepository>().Use<FrameOrderRepository>();
			For<ISubscriptionRepository>().Use<SubscriptionRepository>();
			For<ISettlementRepository>().Use<SettlementRepository>();
			For<IContractSaleRepository>().Use<ContractSaleRepository>();
			For<ITransactionRepository>().Use<TransactionRepository>();
			For<ICustomerRepository>().Use<CustomerRepository>();
			For<ITransactionArticleRepository>().Use<TransactionArticleRepository>();
			var connectionString = Utility.Business.Globals.ConnectionString(Utility.Business.Globals.ConnectionName);
			For<ISqlProvider>().Use(() => new SqlProvider(connectionString));
			For<IArticleRepository>().Use<ArticleRepository>();


			// Register criteria converters
			Scan(x => {
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
			For<IUserContextService>().Use<CustomUserContextService>();
			For<IGridSortPropertyMappingService>().Use<SynologenGridSortPropertyMappingSerice>();
			For<ILensSubscriptionViewService>().Use<LensSubscriptionViewService>();
			For<IContractSalesViewService>().Use<ContractSalesViewService>();
			For<IContractSalesCommandService>().Use<ContractSalesCommandService>();
		}
	}
}