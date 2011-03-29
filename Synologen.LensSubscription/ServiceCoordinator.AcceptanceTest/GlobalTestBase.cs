using System;
using System.Data;
using NHibernate;
using NUnit.Framework;
using Spinit.Data;
using Spinit.Wpc.Synologen.Data.Repositories.LensSubscriptionRepositories;
using StructureMap;
using Synologen.LensSubscription.ServiceCoordinator.App.IoC;
using Synologen.LensSubscription.BGData;
using NHibernateFactory=Spinit.Wpc.Core.Dependencies.NHibernate.NHibernateFactory;

namespace Synologen.LensSubscription.ServiceCoordinator.AcceptanceTest
{
	[SetUpFixture]
	public class GlobalTestBase
	{
		[SetUp]
		public void RunBeforeAnyTests()
		{
			//Setup structuremap & nhibernatew
			NHibernateFactory.MappingAssemblies.Add(typeof(CountryRepository).Assembly);
			ObjectFactory.Initialize(x => x.AddRegistry<TaskRunnerRegistry>());
			ActionCriteriaExtensions.ConstructConvertersUsing(ObjectFactory.GetInstance);

			//Setup data
			SetupBGDatabase();
			SetupWpcDatabase(NHibernateFactory.Instance.GetSessionFactory().OpenSession());
		}

		public void SetupBGDatabase()
		{
			BGData.NHibernateFactory.Instance.GetConfiguration().Export();
		}

		public void SetupWpcDatabase(ISession session)
		{
            
			if(String.IsNullOrEmpty(session.Connection.ConnectionString)){
				throw new OperationCanceledException("Connectionstring could not be found in configuration");
			}
			if(!IsDevelopmentServer(session.Connection.ConnectionString))
			{
				throw new OperationCanceledException("Make sure you are running tests against a development database!");
			}
			DeleteAndResetIndexForTable(session.Connection, "SynologenLensSubscriptionError");
			DeleteAndResetIndexForTable(session.Connection, "SynologenLensSubscriptionTransaction");
			DeleteAndResetIndexForTable(session.Connection, "SynologenLensSubscription");
			DeleteAndResetIndexForTable(session.Connection, "SynologenLensSubscriptionCustomer");
			DeleteAndResetIndexForTable(session.Connection, "SynologenLensSubscriptionTransactionArticle");
		}

		public static void ExecuteStatement(IDbConnection sqlConnection, string sqlStatement)
		{
			var command = sqlConnection.CreateCommand();
			command.CommandText = sqlStatement;
			command.CommandType = CommandType.Text;
			command.ExecuteNonQuery();
		}

		public static void DeleteAndResetIndexForTable(IDbConnection sqlConnection, string tableName)
		{
			ExecuteStatement(sqlConnection, String.Format("DELETE FROM {0}", tableName));
			ExecuteStatement(sqlConnection, String.Format("DBCC CHECKIDENT ({0}, reseed, 0)", tableName));
		}

		protected virtual bool IsDevelopmentServer(string connectionString)
		{
			if(connectionString.ToLower().Contains("black")) return true;
			if(connectionString.ToLower().Contains("localhost")) return true;
			if(connectionString.ToLower().Contains(@".\")) return true;
			return false;
		}

		[TearDown]
		public void RunAfterAnyTests()
		{
			// ...
		}
	}
}