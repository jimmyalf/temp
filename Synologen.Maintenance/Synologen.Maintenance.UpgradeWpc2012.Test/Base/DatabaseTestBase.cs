using NUnit.Framework;
using Synologen.Maintenance.UpgradeWpc2012.Test.Persistence;

namespace Synologen.Maintenance.UpgradeWpc2012.Test.Base
{
	public class DatabaseTestBase
	{
		protected Migrator Migrator;

		[SetUp]
		protected void Setup()
		{
			Database.DropSchema();
			Database.CreateSchema();
			Database.CreateDefaultData();
			Migrator = new Migrator();
			Migrator.Initialize();
		}

		[TearDown]
		protected void TearDown()
		{
			
		}
	}
}