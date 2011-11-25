using System;
using System.Data.SqlClient;
using NHibernate;
using Spinit.Data;
using Spinit.Test.NHibernate;
using Spinit.Wpc.Core.Dependencies.NHibernate;
using Spinit.Wpc.Synogen.Test.Data;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription;
using Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters.LensSubscription;
using Spinit.Wpc.Synologen.Data.Repositories.LensSubscriptionRepositories;
using Shop = Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription.Shop;

namespace Spinit.Wpc.Synologen.Data.Test.LensSubscriptionData
{
	public abstract class BaseRepositoryTester<TRepository> : NHibernateRepositoryTestbase<TRepository>
	{
		protected int TestCountryId = 1;
		protected readonly SqlProvider Provider;
		private readonly DataManager _dataManager;
		//protected int TestShopId = 158;
		//protected int TestShop2Id = 159;

		protected BaseRepositoryTester()
		{
			_dataManager = new DataManager();
			Provider = _dataManager.GetSqlProvider() as SqlProvider;
		}

		protected override void SetUp()
		{
			SetupData();
			ActionCriteriaExtensions.ConstructConvertersUsing(ResolveCriteriaConverters);
		}

		protected override ISessionFactory GetSessionFactory()
		{
			return NHibernateFactory.Instance.GetSessionFactory();
		}

		private object ResolveCriteriaConverters<TType>(TType objectToResolve)
		{

			if (objectToResolve.Equals(typeof(IActionCriteriaConverter<CustomersForShopMatchingCriteria, ICriteria>)))
			{
				return new CustomersForShopMatchingCriteriaConverter(GetSessionFactory().OpenSession());
			}
			if (objectToResolve.Equals(typeof(IActionCriteriaConverter<PageOfSubscriptionsMatchingCriteria, ICriteria>)))
			{
				return new PageOfSubscriptionsMatchingCriteriaConverter(GetSessionFactory().OpenSession());
			}
			if (objectToResolve.Equals(typeof(IActionCriteriaConverter<AllTransactionsMatchingCriteria, ICriteria>)))
			{
				return new AllTransactionsMatchingCriteriaConverter(GetSessionFactory().OpenSession());
			}
			if (objectToResolve.Equals(typeof(IActionCriteriaConverter<TransactionsForSubscriptionMatchingCriteria, ICriteria>)))
			{
				return new TransactionsForSubscriptionMatchingCriteriaConverter(GetSessionFactory().OpenSession());
			}
			if(objectToResolve.Equals(typeof(IActionCriteriaConverter<AllUnhandledSubscriptionErrorsForShopCriteria,ICriteria>)))
			{
				return new AllUnhandledSubscriptionErrorsForShopCriteriaConverter(GetSessionFactory().OpenSession());
			}
			if(objectToResolve.Equals(typeof(IActionCriteriaConverter<AllActiveTransactionArticlesCriteria,ICriteria>)))
			{
				return new AllActiveTransactionArticlesCriteriaConverter(GetSessionFactory().OpenSession());
			}
			if(objectToResolve.Equals(typeof(IActionCriteriaConverter<PageOfTransactionArticlesMatchingCriteria,ICriteria>)))
			{
				return new PageOfTransactionArticlesMatchingCriteriaConverter(GetSessionFactory().OpenSession());
			}
			if(objectToResolve.Equals(typeof(IActionCriteriaConverter<AllSubscriptionsToSendConsentsForCriteria,ICriteria>)))
			{
				return new AllSubscriptionsToSendConsentsForCriteriaConverter(GetSessionFactory().OpenSession());
			}
			if (objectToResolve.Equals(typeof(IActionCriteriaConverter<AllSubscriptionsToSendPaymentsForCriteria, ICriteria>)))
			{
				return new AllSubscriptionsToSendPaymentsForCriteriaConverter(GetSessionFactory().OpenSession());
			}
			throw new ArgumentException(String.Format("No criteria converter has been defined for {0}.\r\n Create a converter in base class.", objectToResolve), "objectToResolve");
		}

		private void SetupData() 
		{
			if(String.IsNullOrEmpty(ConnectionString)){
				throw new OperationCanceledException("Connectionstring could not be found in configuration");
			}
			if(!_dataManager.IsDevelopmentServer(ConnectionString))
			{
				throw new OperationCanceledException("Make sure you are running tests against a development database!");
			}
			var sqlConnection = new SqlConnection(ConnectionString);
			sqlConnection.Open();
			//DataHelper.DeleteAndResetIndexForTable(sqlConnection, "SynologenLensSubscriptionError");
			//DataHelper.DeleteAndResetIndexForTable(sqlConnection, "SynologenLensSubscriptionTransaction");
			//DataHelper.DeleteAndResetIndexForTable(sqlConnection, "SynologenLensSubscription");
			//DataHelper.DeleteAndResetIndexForTable(sqlConnection, "SynologenLensSubscriptionCustomer");
			//DataHelper.DeleteAndResetIndexForTable(sqlConnection, "SynologenLensSubscriptionTransactionArticle");
			_dataManager.CleanTables(sqlConnection);
			sqlConnection.Close();
		}

		protected Shop CreateShop(ISession session, string shopName = "Testbutik")
		{
			var shop = _dataManager.CreateShop(Provider, shopName);
			return new ShopRepository(session).Get(shop.ShopId);
		}

		protected string ConnectionString
		{
			get { return _dataManager.ConnectionString; }
		}
	}
}