using NUnit.Framework;

namespace Synologen_IntegrationTests.FileIO
{
	[SetUpFixture]
	public class GlobalTestBase
	{

		[SetUp]
		public void RunBeforeAnyTests(){ }

		[TearDown]
		public void RunAfterAnyTests(){ }
	}
}
