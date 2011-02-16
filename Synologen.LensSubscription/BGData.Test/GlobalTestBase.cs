using NUnit.Framework;

namespace Synologen.LensSubscription.BGData.Test
{
	[SetUpFixture]
	public class GlobalTestBase
	{

		[SetUp]
		public void RunBeforeAnyTests()
		{
			//Rebuild database
			NHibernateFactory.Instance.GetConfiguration().Export();
		}

		[TearDown]
		public void RunAfterAnyTests()
		{

		}
	}
}