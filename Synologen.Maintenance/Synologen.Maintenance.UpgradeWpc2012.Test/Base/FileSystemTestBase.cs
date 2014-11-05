using NUnit.Framework;
using Spinit.Wpc.Maintenance.FileAndContentMigration.Test;

namespace Synologen.Maintenance.UpgradeWpc2012.Test.Base
{
	public abstract class FileSystemTestBase : FileSystemTestBase<FileSystem>
	{
		[SetUp]
		protected override void Setup()
		{
			base.Setup();
		}

		[TearDown]
		protected void TearDown()
		{
			
		}	
	}
}