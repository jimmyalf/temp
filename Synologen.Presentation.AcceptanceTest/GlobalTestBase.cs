using NUnit.Framework;
using Spinit.Wpc.Synologen.Presentation.Site.Code.IoC;
using StructureMap;

namespace Synologen.Presentation.AcceptanceTest
{
	[SetUpFixture]
	public class GlobalTestBase
	{
		[SetUp]
		public void RunBeforeAnyTests()
		{
			//Setup Structuremap
			ObjectFactory.Initialize(x => x.AddRegistry<WebRegistry>());
		}

		[TearDown]
		public void RunAfterAnyTests()
		{
			// ...
		}
	}
}