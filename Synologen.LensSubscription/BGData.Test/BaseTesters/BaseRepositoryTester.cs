using System;
using NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Synologen.LensSubscription.BGData.Repositories;
using Synologen.LensSubscription.BGData.Test.CommonDataTestHelpers;

namespace Synologen.LensSubscription.BGData.Test.BaseTesters
{
	public abstract class BaseRepositoryTester<TRepository> : NHibernateRepositoryTester<TRepository>
	{
		protected override void SetUp()
		{
			SetupData(GetSessionFactory().OpenSession());
		}

		private void SetupData(ISession session)
		{
			var connectionstring = session.Connection.ConnectionString;
			if (String.IsNullOrEmpty(connectionstring))
			{
				throw new OperationCanceledException("Connectionstring could not be found in configuration");
			}
			if (!IsDevelopmentServer(connectionstring))
			{
				throw new OperationCanceledException("Make sure you are running tests against a development database!");
			}

			DataHelper.DeleteAndResetIndexForTable(session.Connection, "ReceivedFileSections");
			DataHelper.DeleteAndResetIndexForTable(session.Connection, "BGConsentToSend");
			DataHelper.DeleteAndResetIndexForTable(session.Connection, "BGPaymentToSend");
			DataHelper.DeleteAndResetIndexForTable(session.Connection, "FileSectionToSend");
            DataHelper.DeleteAndResetIndexForTable(session.Connection, "ReceivedConsents");
            DataHelper.DeleteAndResetIndexForTable(session.Connection, "ReceivedPayments");
            DataHelper.DeleteAndResetIndexForTable(session.Connection, "ReceivedError");
        }

		protected AutogiroPayer StoreAutogiroPayer(Func<AutogiroPayer> generatePayerToStore)
		{
			var payer = generatePayerToStore.Invoke();
            new AutogiroPayerRepository(GetSessionFactory().OpenSession()).Save(payer);
			return payer;
		}

		protected virtual bool IsDevelopmentServer(string connectionString)
		{
			if(connectionString.ToLower().Contains("black")) return true;
			if(connectionString.ToLower().Contains("dev")) return true;
			if(connectionString.ToLower().Contains("localhost")) return true;
			if(connectionString.ToLower().Contains(@".\")) return true;
			return false;
		}

		protected override ISessionFactory GetSessionFactory()
		{
			return NHibernateFactory.Instance.GetSessionFactory();
		}
	}
}