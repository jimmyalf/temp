using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using NHibernate;
using NUnit.Framework;
using Spinit.Data;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;
using Spinit.Wpc.Synologen.Core.Domain.Persistence;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias;
using Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters;
using Spinit.Wpc.Synologen.Data.Repositories.FrameOrderRepositories;
using Spinit.Wpc.Synologen.Data.Repositories.NHibernate;
using Spinit.Wpc.Synologen.Integration.Test.FrameData.Factories;

namespace Spinit.Wpc.Synologen.Integration.Test.FrameData
{
	public class TestBase : AssertionHelper
	{
		private ISessionFactory _sessionFactory;
		const int testableShopId = 158;

		public void SetupDefaultContext()
		{
			ActionCriteriaExtensions.ConstructConvertersUsing(ResolveCriteriaConverters);
			SetupData();
			_sessionFactory = NHibernateFactory.Instance.GetSessionFactory();
			var testSession = GetNewSession();
			var validationSession = GetNewSession();
			FrameRepository = new FrameRepository(testSession);
			FrameColorRepository = new FrameColorRepository(testSession);
			FrameBrandRepository = new FrameBrandRepository(testSession);
			FrameGlassTypeRepository = new FrameGlassTypeRepository(testSession);
			ShopRepository = new ShopRepository(testSession);
			FrameOrderRepository = new FrameOrderRepository(testSession);

			FrameValidationRepository = new FrameRepository(validationSession);
			FrameColorValidationRepository = new FrameColorRepository(validationSession);
			FrameBrandValidationRepository = new FrameBrandRepository(validationSession);
			FrameGlassTypeValidationRepository = new FrameGlassTypeRepository(validationSession);
			FrameOrderValidationRepository = new FrameOrderRepository(validationSession);

			SavedFrameColors = FrameColorFactory.GetFrameColors();
			SavedFrameColors.ToList().ForEach(x => FrameColorRepository.Save(x));

			SavedFrameBrands = FrameBrandFactory.GetFrameBrands();
			SavedFrameBrands.ToList().ForEach(x => FrameBrandRepository.Save(x));

			SavedFrames = FrameFactory.GetFrames(SavedFrameBrands, SavedFrameColors);
			SavedFrames.ToList().ForEach(x => FrameRepository.Save(x));

			SavedFrameGlassTypes = FrameGlassTypeFactory.GetGlassTypes();
			SavedFrameGlassTypes.ToList().ForEach(x => FrameGlassTypeRepository.Save(x));

			SavedShop = ShopRepository.Get(testableShopId);
			SavedFrameOrders = FrameOrderFactory.GetFrameOrders(SavedFrames, SavedFrameGlassTypes, SavedShop);
			SavedFrameOrders.ToList().ForEach(x => FrameOrderRepository.Save(x));
		}

		private object ResolveCriteriaConverters<TType>(TType objectToResolve)
		{

			if(objectToResolve.Equals(typeof(IActionCriteriaConverter<PageOfFramesMatchingCriteria, ICriteria>)))
			{
			    return new PageOfFramesMatchingCriteriaConverter(GetNewSession());
			}
			if(objectToResolve.Equals(typeof(IActionCriteriaConverter<PagedSortedCriteria, ICriteria>)))
			{
				return new PagedSortedCriteriaConverter(GetNewSession());
			}
			if(objectToResolve.Equals(typeof(IActionCriteriaConverter<PageOfFrameOrdersMatchingCriteria, ICriteria>)))
			{
				return new PageOfFrameOrdersMatchingCriteriaConverter(GetNewSession());
			}
			if(objectToResolve.Equals(typeof(IActionCriteriaConverter<AllOrderableFramesCriteria, ICriteria>)))
			{
				return new AllOrderableFramesCriteriaConverter(GetNewSession());
			}
			if(objectToResolve.Equals(typeof(IActionCriteriaConverter<AllFrameOrdersForShopCriteria, ICriteria>)))
			{
				return new AllFrameOrdersForShopCriteriaConverter(GetNewSession());
			}
			throw new ArgumentException(String.Format("No criteria converter has been defined for {0}", objectToResolve), "objectToResolve");
		}

		private static string ConnectionString{
			get
			{
				const string connectionStringname = "WpcServer";
				return ConfigurationManager.ConnectionStrings[connectionStringname].ConnectionString;
			}
		}

		public IFrameRepository FrameRepository { get; private set; }
		public IFrameColorRepository FrameColorRepository { get; private set; }
		public IFrameBrandRepository FrameBrandRepository { get; private set; }
		public IFrameGlassTypeRepository FrameGlassTypeRepository { get; private set; }
		public IShopRepository ShopRepository { get; private set; }
		public IFrameOrderRepository FrameOrderRepository { get; private set; }

		public IFrameRepository FrameValidationRepository { get; private set; }
		public IFrameColorRepository FrameColorValidationRepository { get; private set; }
		public IFrameBrandRepository FrameBrandValidationRepository { get; private set; }
		public IFrameGlassTypeRepository FrameGlassTypeValidationRepository { get; private set; }
		public IFrameOrderRepository FrameOrderValidationRepository { get; private set; }

		public IEnumerable<Frame> SavedFrames { get; private set; }
		public IEnumerable<FrameColor> SavedFrameColors { get; private set; }
		public IEnumerable<FrameBrand> SavedFrameBrands { get; private set; }
		public IEnumerable<FrameGlassType> SavedFrameGlassTypes { get; private set; }
		public IEnumerable<FrameOrder> SavedFrameOrders { get; private set; }
		public Shop SavedShop { get; private set; }

		protected ISession GetNewSession()
		{
			return _sessionFactory.OpenSession();
		}

		private void SetupData() 
		{
			if(String.IsNullOrEmpty(ConnectionString)){
				throw new OperationCanceledException("Connectionstring could not be found in configuration");
			}
			if(!IsDevelopmentServer(ConnectionString))
			{
				throw new OperationCanceledException("Make sure you are running tests against a development database!");
			}
			var sqlConnection = new SqlConnection(ConnectionString);
			sqlConnection.Open();
			DeleteAndResetIndexForTable(sqlConnection, "SynologenFrameOrder");
			DeleteAndResetIndexForTable(sqlConnection, "SynologenFrame");
			DeleteAndResetIndexForTable(sqlConnection, "SynologenFrameGlassType");
			DeleteAndResetIndexForTable(sqlConnection, "SynologenFrameColor");
			DeleteAndResetIndexForTable(sqlConnection, "SynologenFrameBrand");
			sqlConnection.Close();
		}

		protected virtual bool IsDevelopmentServer(string connectionString)
		{
			if(connectionString.ToLower().Contains("black")) return true;
			if(connectionString.ToLower().Contains("localhost")) return true;
			return false;
		}

		private static void ExecuteStatement(SqlConnection sqlConnection, string sqlStatement)
		{
			var transaction = sqlConnection.BeginTransaction();
			using (var cmd = sqlConnection.CreateCommand()) {
				cmd.Connection = sqlConnection;
				cmd.Transaction = transaction;

				cmd.CommandText = sqlStatement;
				cmd.CommandType = CommandType.Text;
				cmd.ExecuteNonQuery();
			}
			transaction.Commit();
		}

		private static void DeleteAndResetIndexForTable(SqlConnection sqlConnection, string tableName)
		{
			ExecuteStatement(sqlConnection, String.Format("DELETE FROM {0}", tableName));
			ExecuteStatement(sqlConnection, String.Format("DBCC CHECKIDENT ({0}, reseed, 0)", tableName));
		}
	}
}