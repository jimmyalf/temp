using NUnit.Framework;
using Spinit.Data;
using StructureMap;
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
			ActionCriteriaExtensions.ConstructConvertersUsing(ObjectFactory.GetInstance);
		}

		[TearDown]
		public void RunAfterAnyTests()
		{
			// ...
		}
	}
}