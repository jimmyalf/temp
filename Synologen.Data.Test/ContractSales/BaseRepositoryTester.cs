using System;
using NHibernate;
using Spinit.Data;
using Spinit.Test.NHibernate;
using Spinit.Wpc.Core.Dependencies.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.ContractSales;
using Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters.ContractSales;
using Spinit.Wpc.Synologen.Test.Data;

namespace Spinit.Wpc.Synologen.Data.Test.ContractSales
{
	public class BaseRepositoryTester<TModel> :  NHibernateRepositoryTestbase<TModel>
	{
		protected int TestCountryId = 1;
		protected readonly DataManager DataManager;

		public BaseRepositoryTester()
		{
			DataManager = new DataManager();
			ActionCriteriaExtensions.ConstructConvertersUsing(ResolveCriteriaConverters);
		}

		protected override void SetUp()
		{
			SetupData(GetSessionFactory().OpenSession());
		}

		private void SetupData(ISession session)
		{
			var connection = session.Connection;
			DataManager.CleanTables(connection);
			//DataManager.DeleteMembersAndConnections(connection);
			session.Connection.Close();
		}

		protected override ISessionFactory GetSessionFactory() 
		{ 
			return NHibernateFactory.Instance.GetSessionFactory();
		}

		private object ResolveCriteriaConverters<TType>(TType objectToResolve)
		{

			if (objectToResolve.Equals(typeof(IActionCriteriaConverter<AllContractSalesMatchingCriteria, ICriteria>)))
			{
				return new AllContractSalesMatchingCriteriaConverter(GetSessionFactory().OpenSession());
			}

			throw new ArgumentException(String.Format("No criteria converter has been defined for {0}", objectToResolve), "objectToResolve");
		}
	}
}