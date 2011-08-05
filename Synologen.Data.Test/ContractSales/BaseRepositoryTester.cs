using System;
using NHibernate;
using Spinit.Data;
using Spinit.Wpc.Core.Dependencies.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.ContractSales;
using Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters.ContractSales;
using Spinit.Wpc.Synologen.Data.Test.CommonDataTestHelpers;

namespace Spinit.Wpc.Synologen.Data.Test.ContractSales
{
	public class BaseRepositoryTester<TModel> :  NHibernateRepositoryTester<TModel> //: AssertionHelper
	{
		protected const int testableShopId = 158;
		protected const int testableShopId2 = 159;
		public const int TestableShopMemberId = 485;
		public const int TestableShop2MemberId = 484;
		public const int TestableCompanyId = 57;
		public const int TestableContractId = 14;
		protected int TestCountryId = 1;

		public BaseRepositoryTester()
		{
			ActionCriteriaExtensions.ConstructConvertersUsing(ResolveCriteriaConverters);
		}

		protected override void SetUp()
		{
			SetupData(GetSessionFactory().OpenSession());
		}

		private void SetupData(ISession session)
		{
			if(String.IsNullOrEmpty(DataHelper.ConnectionString)){
				throw new OperationCanceledException("Connectionstring could not be found in configuration");
			}
			if(!IsDevelopmentServer(DataHelper.ConnectionString))
			{
				throw new OperationCanceledException("Make sure you are running tests against a development database!");
			}
			DataHelper.DeleteForTable(session.Connection, "tblSynologenSettlementOrderConnection");
			DataHelper.DeleteForTable(session.Connection, "tblSynologenContractArticleConnection");
			DataHelper.DeleteAndResetIndexForTable(session.Connection, "SynologenLensSubscriptionTransaction");
			DataHelper.DeleteAndResetIndexForTable(session.Connection, "tblSynologenSettlement");
			DataHelper.DeleteAndResetIndexForTable(session.Connection, "tblSynologenOrderHistory");
			DataHelper.DeleteAndResetIndexForTable(session.Connection, "tblSynologenOrderItems");
			DataHelper.DeleteAndResetIndexForTable(session.Connection, "tblSynologenOrder");
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

		protected virtual bool IsDevelopmentServer(string connectionString)
		{
			if(connectionString.ToLower().Contains("black")) return true;
			if(connectionString.ToLower().Contains("dev")) return true;
			if(connectionString.ToLower().Contains("localhost")) return true;
			return false;
		}
	}
}