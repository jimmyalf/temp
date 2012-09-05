using System;
using System.Configuration;
using System.IO;

namespace Synologen.Maintenance.UpgradeWpc2012.Domain.Settings
{
	public static class Settings
	{
		public static string InvalidCharacterPattern = @"[^a-z0-9+\-\._\/]";
		public static string ValidCharacterPattern = @"[a-z0-9+\-\._\/]";
		private static DirectoryInfo _commonFilesDirectory;

		public static DirectoryInfo CommonFilesDirectory
		{
			get{
				if(_commonFilesDirectory != null) return _commonFilesDirectory;
				var path = ConfigurationManager.AppSettings["CommonFilesFolderPath"].Replace("{ProjectFolder}", GetProjectFolder());
				_commonFilesDirectory = new DirectoryInfo(path);
				return _commonFilesDirectory;
			}
		}

		public static string ConnectionString
		{
			get { return ConfigurationManager.ConnectionStrings["WpcServer"].ConnectionString; }
			
		}

		private static string GetProjectFolder()
		{
			return (new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory)).Parent.Parent.FullName;
		}
	}
}