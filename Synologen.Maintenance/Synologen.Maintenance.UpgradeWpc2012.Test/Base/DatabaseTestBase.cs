using NUnit.Framework;
using Spinit.Wpc.Maintenance.FileAndContentMigration;
using Spinit.Wpc.Maintenance.FileAndContentMigration.Test;
using Database = Synologen.Maintenance.UpgradeWpc2012.Test.Persistence.Database;

namespace Synologen.Maintenance.UpgradeWpc2012.Test.Base
{
	public abstract class DatabaseTestBase : DatabaseTestBase<Database>
	{
		[SetUp]
		protected override void Setup()
		{
			Database.DropSchema();
			Database.CreateSchema();
			Database.CreateDefaultData();
			Migrator = new Migrator(Configuration);
			Migrator.Initialize();
		}

		[TearDown]
		protected void TearDown()
		{
			
		}
	}
}