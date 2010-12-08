using NUnit.Framework;

namespace Spinit.Wpc.Synologen.Integration.FileIO.Test
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