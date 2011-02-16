using System;
using NHibernate;
using Spinit.Data;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.BGServer;
using Synologen.LensSubscription.BGData.CriteriaConverters;
using Synologen.LensSubscription.BGData.Test.CommonDataTestHelpers;

namespace Synologen.LensSubscription.BGData.Test.BaseTesters
{
	public abstract class BaseRepositoryTester<TRepository> : NHibernateRepositoryTester<TRepository>
	{
		protected override Action SetUp()
		{
			return () =>
			{
				SetupData(GetSessionFactory().OpenSession());
				ActionCriteriaExtensions.ConstructConvertersUsing(ResolveCriteriaConverters);
			};
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
		}

		private object ResolveCriteriaConverters<TType>(TType objectToResolve)
		{

			if (objectToResolve.Equals(typeof(IActionCriteriaConverter<AllUnhandledReceivedConsentFileSectionsCriteria, ICriteria>)))
			{
				return new AllUnhandledReceivedConsentFileSectionsCriteriaConverter(GetSessionFactory().OpenSession());
			}
			return null;
           
		}

		protected virtual bool IsDevelopmentServer(string connectionString)
		{
			if (connectionString.ToLower().Contains("black")) return true;
			if (connectionString.ToLower().Contains("localhost")) return true;
			return false;
		}

		protected override ISessionFactory GetSessionFactory()
		{
			return NHibernateFactory.Instance.GetSessionFactory();
		}
	}
}