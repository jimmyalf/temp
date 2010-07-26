using NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Persistence;
using Spinit.Wpc.Synologen.Core.Persistence;
using Spinit.Wpc.Synologen.Data.Repositories;
using Spinit.Wpc.Synologen.Data.Repositories.NHibernate;
using StructureMap.Attributes;
using StructureMap.Configuration.DSL;

namespace Spinit.Wpc.Synologen.Presentation.Site
{
	public class SynologenSiteRegistry : Registry
	{
		public SynologenSiteRegistry()
		{
			ForRequestedType<ISessionFactory>().CacheBy(InstanceScope.Singleton).TheDefault.Is.ConstructedBy(NHibernateFactory.Instance.GetSessionFactory);
			ForRequestedType<ISession>().TheDefault.Is.ConstructedBy(x => ((NHibernateUnitOfWork)x.GetInstance<IUnitOfWork>()).Session);
			ForRequestedType<IUnitOfWork>().CacheBy(InstanceScope.Hybrid).TheDefault.Is.OfConcreteType<NHibernateUnitOfWork>();

			ForRequestedType<IFrameRepository>().CacheBy(InstanceScope.Hybrid).TheDefaultIsConcreteType<FrameRepository>();
		}
	}
}