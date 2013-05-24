using NHibernate;
using NUnit.Framework;
using Spinit.Data;
using StructureMap;

namespace Synologen.LensSubscription.BGData.Test
{
	[SetUpFixture]
	public class GlobalTestBase
	{

		[SetUp]
		public void RunBeforeAnyTests()
		{
			//Bootstrap
			Bootstrapper.Bootstrap();

			//Rebuild database
			NHibernateFactory.Instance.GetConfiguration().Export();

			//Setup criteria converters
			ActionCriteriaExtensions.ConstructConvertersUsing(
				ObjectFactory
				.With(typeof(ISession), NHibernateFactory.Instance.GetSessionFactory().OpenSession())
				.GetInstance
			);
		}

		[TearDown]
		public void RunAfterAnyTests()
		{

		}
	}
}