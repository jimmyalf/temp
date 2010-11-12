using System.Web.Mvc;
using NHibernate;
using Spinit.Data;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.ContractSales;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.ContractSales;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.FrameOrder;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Data;
using Spinit.Wpc.Synologen.Data.Repositories.ContractSalesRepositories;
using Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters;
using Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters.ContractSales;
using Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters.LensSubscription;
using Spinit.Wpc.Synologen.Data.Repositories.FrameOrderRepositories;
using Spinit.Wpc.Synologen.Data.Repositories.LensSubscriptionRepositories;
using Spinit.Wpc.Synologen.Presentation.Application.Services;
using StructureMap.Configuration.DSL;

namespace Spinit.Wpc.Synologen.Presentation
{
	public class SynologenAdminRegistry : Registry
	{
		public SynologenAdminRegistry()
		{
			Scan(x =>
			{
				x.AssemblyContainingType<SynologenAdminRegistry>();
				x.AddAllTypesOf<IController>().NameBy(c => c.Name);
			});

			For<IFrameRepository>().HybridHttpOrThreadLocalScoped().Use<FrameRepository>();
			For<IFrameColorRepository>().HybridHttpOrThreadLocalScoped().Use<FrameColorRepository>();
			For<IFrameBrandRepository>().HybridHttpOrThreadLocalScoped().Use<FrameBrandRepository>();
			For<IFrameGlassTypeRepository>().HybridHttpOrThreadLocalScoped().Use<FrameGlassTypeRepository>();
			For<IFrameOrderRepository>().HybridHttpOrThreadLocalScoped().Use<FrameOrderRepository>();
			For<ISubscriptionRepository>().HybridHttpOrThreadLocalScoped().Use<SubscriptionRepository>();
			For<ISettlementRepository>().HybridHttpOrThreadLocalScoped().Use<SettlementRepository>();
			For<IContractSaleRepository>().HybridHttpOrThreadLocalScoped().Use<ContractSaleRepository>();

			var connectionString = Utility.Business.Globals.ConnectionString(Utility.Business.Globals.ConnectionName);
			For<ISqlProvider>().Use(() => new SqlProvider(connectionString));
			
			For<IActionCriteriaConverter<PageOfFramesMatchingCriteria, ICriteria>>().Use<PageOfFramesMatchingCriteriaConverter>();
			For<IActionCriteriaConverter<PagedSortedCriteria, ICriteria>>().Use<PagedSortedCriteriaConverter>();
			For<IActionCriteriaConverter<PageOfFrameOrdersMatchingCriteria, ICriteria>>().Use<PageOfFrameOrdersMatchingCriteriaConverter>();
			For<IActionCriteriaConverter<PageOfSubscriptionsMatchingCriteria, ICriteria>>().Use<PageOfSubscriptionsMatchingCriteriaConverter>();
			For<IActionCriteriaConverter<AllContractSalesMatchingCriteria, ICriteria>>().Use<AllContractSalesMatchingCriteriaConverter>();

			For<IAdminSettingsService>().Use<SettingsService>();
			For<IGridSortPropertyMappingService>().Use<SynologenGridSortPropertyMappingSerice>();
			For<ILensSubscriptionViewService>().HybridHttpOrThreadLocalScoped().Use<LensSubscriptionViewService>();
			For<IContractSalesViewService>().HybridHttpOrThreadLocalScoped().Use<ContractSalesViewService>();
		}
	}
}