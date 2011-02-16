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

		//protected ISessionFactory GetSessionFactory()
		//{
		//    return NHibernateFactory.Instance.GetSessionFactory();
		//}

		//protected virtual bool IsDevelopmentServer(string connectionString)
		//{
		//    if(connectionString.ToLower().Contains("black")) return true;
		//    if(connectionString.ToLower().Contains("localhost")) return true;
		//    return false;
		//}
	}
}