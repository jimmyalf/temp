using System.IO;
using System.Linq;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Settings;

namespace Synologen.Maintenance.UpgradeWpc2012.Test.Persistence
{
	public static class FileSystem
	{
		public static void CleanCommonFilesFolder()
		{
			
			var files = Settings.CommonFilesDirectory.GetFiles("*.*", SearchOption.AllDirectories);
			foreach (var file in files)
			{
				file.Delete();
			}
			var directories = Settings.CommonFilesDirectory.GetDirectories("*");
			foreach (var directory in directories)
			{
				directory.Delete(true);
			}
		}
	}
}