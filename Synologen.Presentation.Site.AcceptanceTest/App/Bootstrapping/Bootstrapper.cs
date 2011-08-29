using Spinit.Data;
using Spinit.Wpc.Core.Dependencies.NHibernate;
using Spinit.Wpc.Synologen.Data;
using Spinit.Wpc.Synologen.Presentation.Site.AcceptanceTest.App.IoC;
using StructureMap;

namespace Spinit.Wpc.Synologen.Presentation.Site.AcceptanceTest.App.Bootstrapping
{
	public class Bootstrapper
	{
		public static void Bootstrap()
		{
			InitiateNHibernate();
			InitiateIoc();
			InitiateNHibernateActionCriteriaConverters();
		}

		private static void InitiateNHibernate()
		{
			NHibernateFactory.MappingAssemblies.Add(typeof(SqlProvider).Assembly);
		}

		private static void InitiateIoc()
		{
			ObjectFactory.Container.Configure(x => x.AddRegistry<TestRegistry>());
			
		}

		private static void InitiateNHibernateActionCriteriaConverters()
		{
			ActionCriteriaExtensions.ConstructConvertersUsing(ObjectFactory.GetInstance);
		}
	}
}