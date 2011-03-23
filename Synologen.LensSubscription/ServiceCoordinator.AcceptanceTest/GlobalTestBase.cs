using NHibernate;
using NUnit.Framework;
using Spinit.Data;
using Spinit.Wpc.Core.Dependencies.NHibernate;
using Spinit.Wpc.Synologen.Data.Repositories.LensSubscriptionRepositories;
using StructureMap;
using Synologen.LensSubscription.ServiceCoordinator.App.IoC;

namespace ServiceCoordinator.AcceptanceTest
{
	[SetUpFixture]
	public class GlobalTestBase
	{
		[SetUp]
		public void RunBeforeAnyTests()
		{
			//Setup Structuremap
			NHibernateFactory.MappingAssemblies.Add(typeof(CountryRepository).Assembly);
			ObjectFactory.Initialize(x => x.AddRegistry<TaskRunnerRegistry>());
			ActionCriteriaExtensions.ConstructConvertersUsing(ObjectFactory.GetInstance);
		}

		[TearDown]
		public void RunAfterAnyTests()
		{
			// ...
		}
	}
}