using Spinit.Wpc.Synologen.Presentation.AcceptanceTest.App.IoC;
using StructureMap;

namespace Spinit.Wpc.Synologen.Presentation.AcceptanceTest.App.Bootstrapping
{
	public static class Bootstrapper
	{
		public static void Bootstrap()
		{
			SetupIoC();
			SetupNHibernateCriterias();
			SetupStoryQ();
		}

		private static void SetupIoC()
		{
			ObjectFactory.Initialize(x => x.AddRegistry<TestRegistry>());
		}

		private static void SetupNHibernateCriterias()
		{
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