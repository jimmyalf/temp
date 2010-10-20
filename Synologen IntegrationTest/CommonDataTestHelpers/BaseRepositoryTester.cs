using System;
using System.Data.SqlClient;
using System.Linq;
using NHibernate;
using Spinit.Data;
using Spinit.Wpc.Core.Dependencies.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription;
using Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters.LensSubscription;
using Spinit.Wpc.Synologen.Data.Repositories.LensSubscriptionRepositories;
using Spinit.Wpc.Synologen.Integration.Test.LensSubscriptionData.Factories;

namespace Spinit.Wpc.Synologen.Integration.Test.CommonDataTestHelpers
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

		private object ResolveCriteriaConverters<TType>(TType objectToResolve)
		{

			if (objectToResolve.Equals(typeof(IActionCriteriaConverter<CustomersForShopMatchingCriteria, ICriteria>)))
			{
				return new CustomersForShopMatchingCriteriaConverter(GetSessionFactory().OpenSession());
			}
			throw new ArgumentException(String.Format("No criteria converter has been defined for {0}", objectToResolve), "objectToResolve");
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
			DataHelper.DeleteAndResetIndexForTable(sqlConnection, "SynologenLensSubscriptionCustomer");
			sqlConnection.Close();


			var session = GetSessionFactory().OpenSession();
			Shop shop = new ShopRepository(session).Get(158);
			Country country = new CountryRepository(session).Get(1);
			CustomerRepository reposititory = new CustomerRepository(session);
 			for (int i = 0; i < 5; i++)
			{
				var customerToSave = CustomerFactory.Get(country, shop, "Tore " + i, "Alm " + i, "630610613" + i);
				reposititory.Save(customerToSave);
			}

		}

		protected virtual bool IsDevelopmentServer(string connectionString)
		{
			if(connectionString.ToLower().Contains("black")) return true;
			if(connectionString.ToLower().Contains("localhost")) return true;
			return false;
		}
	}
}