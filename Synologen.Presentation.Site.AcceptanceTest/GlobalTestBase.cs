using NUnit.Framework;
using Spinit.Wpc.Synologen.Presentation.Site.AcceptanceTest.App.Bootstrapping;

namespace Spinit.Wpc.Synologen.Presentation.Site.AcceptanceTest
{
	[SetUpFixture]
	public class GlobalTestBase
	{
		[SetUp]
		public void RunBeforeAnyTests()
		{
			Bootstrapper.Bootstrap();
		}

		[TearDown]
		public void RunAfterAnyTests()
		{
			// ...
		}
	}
}