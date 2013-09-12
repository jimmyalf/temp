using NUnit.Framework;
using Spinit.Data;
using StructureMap;
using Synologen.Service.Client.BGCTaskRunner.App.IoC;

namespace Synologen.Service.Client.BGCTaskRunner.AcceptanceTest
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