using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using NHibernate;
using NUnit.Framework;
using Spinit.Data;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Core.Dependencies.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.FrameOrder;
using Spinit.Wpc.Synologen.Data;
using Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters;
using Spinit.Wpc.Synologen.Data.Repositories.FrameOrderRepositories;
using Spinit.Wpc.Synologen.Data.Test.FrameData.Factories;
using Spinit.Wpc.Synologen.Test.Data;

namespace Spinit.Wpc.Synologen.Integration.Data.Test.FrameData
{
	public class TestBase : AssertionHelper
	{
		private ISessionFactory _sessionFactory;
		private readonly SqlProvider _sqlProvider;
		protected Business.Domain.Entities.Shop TestShop;
		private DataManager _dataManager;
		//const int testableShopId = 158;

		public TestBase()
		{
			_dataManager = new DataManager();
			_sqlProvider = _dataManager.GetSqlProvider() as SqlProvider;
		}

		public void SetupDefaultContext()
		{
			ActionCriteriaExtensions.ConstructConvertersUsing(ResolveCriteriaConverters);
			SetupData();
			NHibernateFactory.MappingAssemblies.Add(typeof(Synologen.Data.Repositories.NHibernate.Mappings.FrameMap).Assembly);
			_sessionFactory = NHibernateFactory.Instance.GetSessionFactory();
			var testSession = GetNewSession();
			var validationSession = GetNewSession();
			var shop = _dataManager.CreateShop(_sqlProvider, "Testbutik");

			FrameRepository = new FrameRepository(testSession);
			FrameColorRepository = new FrameColorRepository(testSession);
			FrameBrandRepository = new FrameBrandRepository(testSession);
			FrameGlassTypeRepository = new FrameGlassTypeRepository(testSession);
			
			FrameOrderRepository = new FrameOrderRepository(testSession);
			ShopRepository = new ShopRepository(testSession);
			FrameValidationRepository = new FrameRepository(validationSession);
			FrameColorValidationRepository = new FrameColorRepository(validationSession);
			FrameBrandValidationRepository = new FrameBrandRepository(validationSession);
			FrameGlassTypeValidationRepository = new FrameGlassTypeRepository(validationSession);
			FrameOrderValidationRepository = new FrameOrderRepository(validationSession);

			SavedShop = ShopRepository.Get(shop.ShopId);

			// TODO: Try to refactor out these initiations and do them in global end rather than before each test to increase test speed
			SavedFrameColors = FrameColorFactory.GetFrameColors();
			SavedFrameColors.ToList().ForEach(x => FrameColorRepository.Save(x));

			SavedFrameBrands = FrameBrandFactory.GetFrameBrands();
			SavedFrameBrands.ToList().ForEach(x => FrameBrandRepository.Save(x));

			SavedFrames = FrameFactory.GetFrames(SavedFrameBrands, SavedFrameColors);
			SavedFrames.ToList().ForEach(x => FrameRepository.Save(x));

			SavedFrameGlassTypes = FrameGlassTypeFactory.GetGlassTypes();
			SavedFrameGlassTypes.ToList().ForEach(x => FrameGlassTypeRepository.Save(x));

			
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

		protected Shop CreateShop(ISession session)
		{
			var shop = _dataManager.CreateShop(_sqlProvider, "Testbutik");
			return new ShopRepository(session).Get(shop.ShopId);
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
			var sqlConnection = new SqlConnection(_dataManager.ConnectionString);
			sqlConnection.Open();
			_dataManager.CleanTables(sqlConnection);
			sqlConnection.Close();
		}
	}
}