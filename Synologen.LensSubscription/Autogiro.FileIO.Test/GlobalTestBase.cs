using NUnit.Framework;

namespace Synologen.LensSubscription.Autogiro.FileIO.Test
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