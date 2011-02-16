using NHibernate;
using Synologen.LensSubscription.BGData.Repositories;

namespace Synologen.LensSubscription.BGData.Test.BaseTesters
{
	public abstract class BGConsentToSendRepositoryBaseTester : BaseRepositoryTester<BGConsentToSendRepository>
	{
		protected override ISessionFactory GetSessionFactory() 
		{
			return NHibernateFactory.Instance.GetSessionFactory();
		}
	}
}