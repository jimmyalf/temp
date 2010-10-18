using System;
using System.Data.SqlClient;
using System.Linq;
using NHibernate;
using Spinit.Wpc.Core.Dependencies.NHibernate;

namespace Spinit.Wpc.Synologen.Integration.Test.CommonDataTestHelpers
{
	public abstract class BaseRepositoryTester<TRepository> : NHibernateRepositoryTester<TRepository>
	{
		protected override Action SetUp()
		{
			return SetupData;
		}

		protected override ISessionFactory GetSessionFactory()
		{
			if(!NHibernateFactory.MappingAssemblies.Any())
			{
				var assembly = typeof (Data.Repositories.NHibernate.Mappings.LensSubscriptions.SubscriptionMap).Assembly;
				NHibernateFactory.MappingAssemblies.Add(assembly);				
			}
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

		private void SetupData() 
		{
			if(String.IsNullOrEmpty(DataHelper.ConnectionString)){
				throw new OperationCanceledException("Connectionstring could not be found in configuration");
			}
			if(!IsDevelopmentServer(DataHelper.ConnectionString))
			{
				throw new OperationCanceledException("Make sure you are running tests against a development database!");
			}
			var sqlConnection = new SqlConnection(DataHelper.ConnectionString);
			sqlConnection.Open();
			DataHelper.DeleteAndResetIndexForTable(sqlConnection, "SynologenLensSubscription");
			sqlConnection.Close();
		}

		protected virtual bool IsDevelopmentServer(string connectionString)
		{
			if(connectionString.ToLower().Contains("black")) return true;
			if(connectionString.ToLower().Contains("localhost")) return true;
			return false;
		}
	}
}