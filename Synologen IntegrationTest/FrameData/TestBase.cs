using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using NHibernate;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Core.Domain.Model;
using Spinit.Wpc.Synologen.Core.Domain.Persistence;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias;
using Spinit.Wpc.Synologen.Core.Persistence;
using Spinit.Wpc.Synologen.Data.Repositories;
using Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters;
using Spinit.Wpc.Synologen.Data.Repositories.NHibernate;

namespace Spinit.Wpc.Synologen.Integration.Test.FrameData
{
	public class TestBase : AssertionHelper
	{
		private ISessionFactory _sessionFactory;

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

			FrameValidationRepository = new FrameRepository(validationSession);
			FrameColorValidationRepository = new FrameColorRepository(validationSession);
			FrameBrandValidationRepository = new FrameBrandRepository(validationSession);

			SavedFrameColors = Factories.FrameColorFactory.GetFrameColors();
			SavedFrameColors.ToList().ForEach(x => FrameColorRepository.Save(x));

			SavedFrameBrands = Factories.FrameBrandFactory.GetFrameBrands();
			SavedFrameBrands.ToList().ForEach(x => FrameBrandRepository.Save(x));

			SavedFrames = Factories.FrameFactory.GetFrames(SavedFrameBrands, SavedFrameColors);
			SavedFrames.ToList().ForEach(x => FrameRepository.Save(x));
		}

		private object ResolveCriteriaConverters<TType>(TType objectToResolve)
		{

			if(objectToResolve.Equals(typeof(IActionCriteriaConverter<PageOfFramesMatchingCriteria, ICriteria>)))
			{
			    return new PageOfFramesMatchingCriteriaConverter(GetNewSession());
			}
			if(objectToResolve.Equals(typeof(IActionCriteriaConverter<PageOfFrameColorsMatchingCriteria, ICriteria>)))
			{
				return new PageOfFrameColorsMatchingCriteriaConverter(GetNewSession());
			}
			if(objectToResolve.Equals(typeof(IActionCriteriaConverter<PageOfFrameBrandsMatchingCriteria, ICriteria>)))
			{
				return new PageOfFrameBrandsMatchingCriteriaConverter(GetNewSession());
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

		public IFrameRepository FrameValidationRepository { get; private set; }
		public IFrameColorRepository FrameColorValidationRepository { get; private set; }
		public IFrameBrandRepository FrameBrandValidationRepository { get; private set; }

		public IEnumerable<Frame> SavedFrames { get; private set; }
		public IEnumerable<FrameColor> SavedFrameColors { get; private set; }
		public IEnumerable<FrameBrand> SavedFrameBrands { get; private set; }

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