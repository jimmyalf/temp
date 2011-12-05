using FluentNHibernate.Automapping;
using Spinit.Wpc.Core.Dependencies.NHibernate;
using Spinit.Wpc.Synologen.Data;
using Spinit.Wpc.Synologen.Presentation.AcceptanceTest.App.IoC;
using StructureMap;

namespace Spinit.Wpc.Synologen.Presentation.AcceptanceTest.App.Bootstrapping
{
	public static class Bootstrapper
	{
		public static void Bootstrap()
		{
			SetupNHibernate();
			SetupIoC();
			SetupStoryQ();
		}

		private static void SetupIoC()
		{
			ObjectFactory.Initialize(x => x.AddRegistry<TestRegistry>());
		}

		private static void SetupNHibernate()
		{
			NHibernateFactory.MappingAssemblies.Add(typeof(SqlProvider).Assembly);
		    //ActionCriteriaExtensions.ConstructConvertersUsing(
		    //    ObjectFactory
		    //    .With(typeof(ISession), NHibernateFactory.Instance.GetSessionFactory().OpenSession())
		    //    .GetInstance
		    //);
		}

		private static void SetupStoryQ()
		{
			StoryQ.StoryQSettings.ReportSupportsLegacyBrowsers = true;
		}
	}
}