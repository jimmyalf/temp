using System;
using System.Data.SqlClient;
using NHibernate;
using Spinit.Data;
using Spinit.Synologen.LensSubscription.BGData;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.BGServer;
using Spinit.Wpc.Synologen.Integration.Data.Test.CommonDataTestHelpers;
using Spinit.Wpc.Synologen.LensSubscription.BGData.Test.CommonDataTestHelpers;
using Synologen.LensSubscription.BGData.Repositories.CriteriaConverters;

namespace Spinit.Wpc.Synologen.LensSubscription.BGData.Test
{
    public abstract class BaseRepositoryTester<TRepository> : NHibernateRepositoryTester<TRepository>
    {
        protected override Action SetUp()
        {
            return () =>
            {
                SetupData();
                ActionCriteriaExtensions.ConstructConvertersUsing(ResolveCriteriaConverters);
            };
        }

        private void SetupData()
        {
            if (String.IsNullOrEmpty(DataHelper.ConnectionString))
            {
                throw new OperationCanceledException("Connectionstring could not be found in configuration");
            }
            if (!IsDevelopmentServer(DataHelper.ConnectionString))
            {
                throw new OperationCanceledException("Make sure you are running tests against a development database!");
            }
            var sqlConnection = new SqlConnection(DataHelper.ConnectionString);
            sqlConnection.Open();
            
            DataHelper.DeleteAndResetIndexForTable(sqlConnection, "ReceivedFileSections");
            //TODO More tables here

            sqlConnection.Close();
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

        public Action<ISession> Arrange
        {
            get { return Context; }
            set { Context = value; }
        }

        public Action<TRepository> Act
        {
            get { return Because; }
            set { Because = value; }
        }
    }
}
