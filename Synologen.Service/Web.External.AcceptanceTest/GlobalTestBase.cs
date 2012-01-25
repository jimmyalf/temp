using NUnit.Framework;
using Spinit.Wpc.Synogen.Test.Data;
using StructureMap;
using Synologen.Service.Web.External.App.IoC;

namespace Web.External.AcceptanceTest
{
	[SetUpFixture]
	public class GlobalTestBase
	{
		[SetUp]
		public void RunBeforeAnyTests()
		{
			ObjectFactory.Configure(x => x.AddRegistry<ServiceRegistry>());
		}

		[TearDown]
		public void RunAfterAnyTests()
		{

		}
	}
}
