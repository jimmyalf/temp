using NUnit.Framework;
using Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.App.Bootstrapping;
using Spinit.Wpc.Synologen.Test.Data;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest
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
			//new DataManager().CleanTables();
		}
	}
}