using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

		public static int CreateFileEntry(string name)
		{
			var command = CommandBuilder
				.Build(@"INSERT INTO tblBaseFile(cName, cDirectory, cCreatedBy, cCreatedDate) 
					VALUES(@Name, 0, 'TestUser', GETDATE())")
				.AddParameters(new {Name = name});
			return Insert<int>(command);
		}

		public static int CreateContentEntry(string input)
		{
			var createPageCommand = CommandBuilder
				.Build(@"INSERT INTO tblContPage(cPgeTpeId,cName,cSize,cContent,cCreatedBy,cCreatedDate) 
					VALUES(1, 'TestPage', 1000, @Content, 'TestUser', GETDATE())")
				.AddParameters(new {Content = input});
			var pageId = Insert<int>(createPageCommand);
			CreateTreeEntity(pageId);
			return pageId;
		}

		public static void CreateNewsEntry(string body, string formatedBody)
		{
			var createNewsCommand = CommandBuilder
				.Build(@"INSERT INTO tblNews(cNewsType,cHeading,cSummary,cBody,cFormatedBody,cStartDate,cEndDate,cCreatedBy,cCreatedDate)
					VALUES(1, 'Test news', 'Summary', @Body, @FormatedBody, GETDATE(),GETDATE(),'TestUser', GETDATE())")
				.AddParameters(new {Body = body, FormatedBody = formatedBody});
			Execute(createNewsCommand);
		}

		private static void CreateTreeEntity(int pageId)
		{
			var createTreeCommand = CommandBuilder
				.Build(@"INSERT INTO tblContTree(cPgeId,cLocId,cLngId,cTreTpeId,cName,cFileName,cHideInMenu,cExcludeFromSearch,cOrder,cCreatedBy,cCreatedDate,cCrsTpeId,cLockFileName,cNeedsAuthentication,cPublish)
					VALUES(@PageId,1,1,1,'TestPage','TestPage',0,0,1,'TestUser',GETDATE(),1,0,0,1)")
				.AddParameters(new {PageId = pageId});
			Execute(createTreeCommand);			
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

		private static T Insert<T>(ISqlBuilder command)
		{
			var persistenceBase = new PersistenceBase(Settings.ConnectionString);
			var identityParameter = new SqlParameter("@Id", SqlDbType.Int) {Direction = ParameterDirection.Output};
			var parameters = new List<SqlParameter>(command.Parameters) {identityParameter};
			var insertStatement = command.ToString() + " SET @Id = @@IDENTITY";
			persistenceBase.Execute(insertStatement, parameters);
			return (T) identityParameter.Value;
		}
	}
}