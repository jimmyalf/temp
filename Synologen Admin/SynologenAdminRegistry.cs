using System.Web.Mvc;
using NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Persistence;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias;
using Spinit.Wpc.Synologen.Core.Persistence;
using Spinit.Wpc.Synologen.Data.Repositories;
using Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters;
using Spinit.Wpc.Synologen.Data.Repositories.NHibernate;
using StructureMap.Attributes;
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

			ForRequestedType<ISessionFactory>().CacheBy(InstanceScope.Singleton).TheDefault.Is.ConstructedBy(NHibernateFactory.Instance.GetSessionFactory);
			ForRequestedType<ISession>().TheDefault.Is.ConstructedBy(x => ((NHibernateUnitOfWork)x.GetInstance<IUnitOfWork>()).Session);
			ForRequestedType<IUnitOfWork>().CacheBy(InstanceScope.Hybrid).TheDefault.Is.OfConcreteType<NHibernateUnitOfWork>();
			ForRequestedType<IFrameRepository>().CacheBy(InstanceScope.Hybrid).TheDefaultIsConcreteType<FrameRepository>();
			ForRequestedType<IFrameColorRepository>().CacheBy(InstanceScope.Hybrid).TheDefaultIsConcreteType<FrameColorRepository>();
			ForRequestedType<IFrameBrandRepository>().CacheBy(InstanceScope.Hybrid).TheDefaultIsConcreteType<FrameBrandRepository>();

			ForRequestedType<IActionCriteriaConverter<PageOfFramesMatchingCriteria, ICriteria>>().TheDefault.Is.OfConcreteType<PageOfFramesMatchingCriteriaConverter>();
			ForRequestedType<IActionCriteriaConverter<AllFramesMatchingCriteria, ICriteria>>().TheDefault.Is.OfConcreteType<AllFramesMatchingCriteriaConverter>();
			ForRequestedType<IActionCriteriaConverter<PageOfFrameColorsMatchingCriteria, ICriteria>>().TheDefault.Is.OfConcreteType<PageOfFrameColorsMatchingCriteriaConverter>();
			ForRequestedType<IActionCriteriaConverter<PageOfFrameBrandsMatchingCriteria, ICriteria>>().TheDefaultIsConcreteType<PageOfFrameBrandsMatchingCriteriaConverter>();

		}
	}
}