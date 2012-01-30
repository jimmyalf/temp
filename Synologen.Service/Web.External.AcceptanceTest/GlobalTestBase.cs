using NUnit.Framework;

namespace Synologen.Service.Web.External.AcceptanceTest
{
	[SetUpFixture]
	public class GlobalTestBase
	{
		[SetUp]
		public void RunBeforeAnyTests()
		{
			//ObjectFactory.Configure(x => x.AddRegistry<ServiceRegistry>());
		}

		[TearDown]
		public void RunAfterAnyTests()
		{

		}
	}
}
