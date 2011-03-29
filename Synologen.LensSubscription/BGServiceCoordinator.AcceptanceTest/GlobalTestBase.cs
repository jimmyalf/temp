using NUnit.Framework;
using StructureMap;
using Synologen.LensSubscription.BGData;
using Synologen.LensSubscription.BGServiceCoordinator.App.IoC;

namespace Synologen.Lenssubscription.BGServiceCoordinator.AcceptanceTest
{
	[SetUpFixture]
	public class GlobalTestBase
	{
		[SetUp]
		public void RunBeforeAnyTests()
		{
			ObjectFactory.Initialize(x => x.AddRegistry<TaskRunnerRegistry>());
			NHibernateFactory.Instance.GetConfiguration().Export();
			//ActionCriteriaExtensions.ConstructConvertersUsing(ObjectFactory.GetInstance);
		}

		[TearDown]
		public void RunAfterAnyTests()
		{
			// ...
		}
	}
}