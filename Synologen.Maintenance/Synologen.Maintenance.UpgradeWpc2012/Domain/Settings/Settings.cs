using System.Configuration;

namespace Synologen.Maintenance.UpgradeWpc2012.Domain.Settings
{
	public static class Settings
	{
		public static string InvalidCharacterPattern = @"[^a-z0-9+\-\._\/]";
		public static string ValidCharacterPattern = @"[a-z0-9+\-\._\/]";

		public static string GetCommonFilesPath()
		{
			return ConfigurationManager.AppSettings["CommonFilesFolderPath"];
		}

		public static string GetConnectionString()
		{
			return ConfigurationManager.ConnectionStrings["WpcServer"].ConnectionString;
		}
	}
}