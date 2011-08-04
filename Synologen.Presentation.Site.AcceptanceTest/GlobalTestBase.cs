using NUnit.Framework;
using Spinit.Wpc.Core.Dependencies.NHibernate;
using Spinit.Wpc.Synologen.Data;
using Spinit.Wpc.Synologen.Presentation.Site.Code.IoC;
using StructureMap;

namespace Spinit.Wpc.Synologen.Presentation.Site.AcceptanceTest
{
	[SetUpFixture]
	public class GlobalTestBase
	{
		[SetUp]
		public void RunBeforeAnyTests()
		{
			//Setup Structuremap
			ObjectFactory.Initialize(x => x.AddRegistry<WebRegistry>());
			NHibernateFactory.MappingAssemblies.Add(typeof(SqlProvider).Assembly);
		}

		[TearDown]
		public void RunAfterAnyTests()
		{
			// ...
		}
	}
}