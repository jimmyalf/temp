using NHibernate;
using Spinit.Data;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Core.Domain.Persistence;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Data;
using Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters;
using Spinit.Wpc.Synologen.Data.Repositories.FrameOrderRepositories;
using Spinit.Wpc.Synologen.Data.Repositories.NHibernate;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Services;
using StructureMap.Attributes;
using StructureMap.Configuration.DSL;
//using IUnitOfWork=Spinit.Wpc.Synologen.Core.Persistence.IUnitOfWork;

namespace Spinit.Wpc.Synologen.Presentation.Site
{
	public class SynologenSiteRegistry : Registry
	{
		public SynologenSiteRegistry()
		{
			var connectionString = Utility.Business.Globals.ConnectionString("WpcServer");
			ForRequestedType<ISessionFactory>().CacheBy(InstanceScope.Singleton).TheDefault.Is.ConstructedBy(NHibernateFactory.Instance.GetSessionFactory);
			ForRequestedType<ISession>().TheDefault.Is.ConstructedBy(x => ((NHibernateUnitOfWork)x.GetInstance<IUnitOfWork>()).Session);
			ForRequestedType<IUnitOfWork>().CacheBy(InstanceScope.Hybrid).TheDefault.Is.OfConcreteType<NHibernateUnitOfWork>();

			ForRequestedType<IFrameRepository>().CacheBy(InstanceScope.Hybrid).TheDefaultIsConcreteType<FrameRepository>();
			ForRequestedType<IFrameGlassTypeRepository>().CacheBy(InstanceScope.Hybrid).TheDefaultIsConcreteType<FrameGlassTypeRepository>();
			ForRequestedType<IFrameOrderRepository>().CacheBy(InstanceScope.Hybrid).TheDefaultIsConcreteType<FrameOrderRepository>();
			ForRequestedType<IShopRepository>().CacheBy(InstanceScope.Hybrid).TheDefaultIsConcreteType<ShopRepository>();
			ForRequestedType<IFrameOrderService>().TheDefaultIsConcreteType<FrameOrderSettingsService>();
			ForRequestedType<ISynologenMemberService>().TheDefaultIsConcreteType<SynologenMemberService>();
			ForRequestedType<ISqlProvider>().TheDefault.Is.ConstructedBy(() => new SqlProvider(connectionString));

			ForRequestedType<IActionCriteriaConverter<AllOrderableFramesCriteria, ICriteria>>().TheDefaultIsConcreteType<AllOrderableFramesCriteriaConverter>();

		}
	}
}