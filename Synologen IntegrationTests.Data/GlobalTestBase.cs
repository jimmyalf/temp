using System.Linq;
using NUnit.Framework;
using Spinit.Wpc.Core.Dependencies.NHibernate;

namespace Spinit.Wpc.Synologen.Integration.Data.Test
{
	[SetUpFixture]
	public class GlobalTestBase
	{
		[SetUp]
		public void RunBeforeAnyTests()
		{
			//Setup NHibernate
            if(!NHibernateFactory.MappingAssemblies.Any())
			{
				var assembly = typeof(Synologen.Data.Repositories.NHibernate.Mappings.LensSubscriptions.SubscriptionMap).Assembly;
				NHibernateFactory.MappingAssemblies.Add(assembly);				
			}
		}

		[TearDown]
		public void RunAfterAnyTests()
		{
		  // ...
		}
	}
}