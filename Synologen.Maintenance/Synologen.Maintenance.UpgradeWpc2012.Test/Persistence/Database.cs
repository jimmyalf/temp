using System.Linq;
using System.Text.RegularExpressions;
using Spinit.Data.SqlClient;
using Spinit.Data.SqlClient.SqlBuilder;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Settings;

namespace Synologen.Maintenance.UpgradeWpc2012.Test.Persistence
{
	public static class Database
	{
		public static void DropSchema()
		{
			Execute(Sql.DropSchema);
		}

		public static void CreateSchema()
		{
			Execute(Sql.CreateBaseFileTable);
			Execute(Sql.CreateBaseLocationLanguageTable);
			Execute(Sql.CreateContentPageTable);
			Execute(Sql.CreateContentTreeTable);
			Execute(Sql.CreateNewsTable);
			Execute(Sql.CreateGetPageUrlFunction);
		}

		public static void CreateDefaultData()
		{
			Execute(Sql.CreateDefaultBaseLocationLanguageData);
		}

		public static void CreateFileEntry(string name)
		{
			var command = CommandBuilder
				.Build(@"
					INSERT INTO tblBaseFile(cName, cDirectory, cCreatedBy, cCreatedDate) 
					VALUES(@Name, 0, 'TestUser', GETDATE())")
				.AddParameters(new {Name = name});
			Execute(command);
		}

		private static void Execute(string sql)
		{
			var regex = new Regex("^[\t]*GO", RegexOptions.IgnoreCase | RegexOptions.Multiline);
			var operations = regex.Split(sql.Trim()).Where(operation => !string.IsNullOrEmpty(operation));
			var persistenceBase = new PersistenceBase(Settings.ConnectionString);
			foreach (var operation in operations)
			{
				persistenceBase.Execute(operation, null);	
			}
		}

		private static void Execute(ICommandBuilder command)
		{
			var persistenceBase = new PersistenceBase(Settings.ConnectionString);
			persistenceBase.Execute(command);
		}
	}
}