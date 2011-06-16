using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Spinit.Wpc.Synologen.OPQ.Core;
using Spinit.Wpc.Synologen.OPQ.Data;
using Spinit.Wpc.Utility.Data;
using System.IO;

namespace Spinit.Wpc.Synologen.OPQ.Business.Test
{
	public class TestInit
	{
		private readonly Configuration _config;
		private readonly DataContext _dataContext;

		public TestInit(Configuration config)
		{
			_config = config;
			if (!IsDevelopmentServer(_config.ConnectionString)) throw new Exception("Test must be run on local server");
			_dataContext = new WpcSynologenDataContext(_config.ConnectionString);
		}

		public void InitDatabase()
		{
			string solutionPath = Path.GetFullPath(string.Concat(Environment.CurrentDirectory, @"\..\..\Synologen OPQ\"));
			CreateDatabase(_dataContext);
			CreateConstraints(_dataContext, solutionPath);
			CreateProcedures(_dataContext, solutionPath);
			//CreateFunctions();
			CreateTriggers(_dataContext, solutionPath);
			CreateDefaultData(_dataContext, solutionPath);
			CreateTestData(_dataContext, solutionPath);
		}

		private void CreateProcedures(DataContext context, string solutionPath)
		{
			string procedurePath =
				string.Concat(solutionPath, @"Spinit.Wpc.Synologen.OPQ.Database\Schema Objects\Programmability\Stored Procedures");
			ExecuteScripts(procedurePath);	
		}

		private void CreateTriggers(DataContext context, string solutionPath)
		{
			
			string triggerPath = 
				string.Concat(solutionPath, @"Spinit.Wpc.Synologen.OPQ.Database\Schema Objects\Tables\Triggers");
			ExecuteScripts(triggerPath);
		}

		private void CreateConstraints(DataContext context, string solutionPath)
		{
			string constraintsPath = 
				string.Concat(solutionPath, @"Spinit.Wpc.Synologen.OPQ.Database\Schema Objects\Tables\Constraints");
			ExecuteScripts(constraintsPath);
		}

		private void CreateTestData(DataContext context, string solutionPath)
		{
			string defaultDataPath =
				string.Concat(solutionPath, @"Spinit.Wpc.Synologen.OPQ.Database\Scripts\Test Data\Business Layer Tests");
			ExecuteScripts(defaultDataPath);
		}
		
		private void CreateDefaultData(DataContext context, string solutionPath)
		{
			string defaultDataPath =
				string.Concat(solutionPath, @"Spinit.Wpc.Synologen.OPQ.Database\Scripts\Default Data");
			ExecuteScripts(defaultDataPath);
		}

		
		private static void CreateDatabase(DataContext context)
		{
			DeleteDatabase(context);
			context.CreateDatabase();
		}

		public void DeleteDatabase()
		{
			DeleteDatabase(_dataContext);
		}

		private static void DeleteDatabase(DataContext context)
		{
			if (context.DatabaseExists())
				context.DeleteDatabase();
		}

		public void Dispose()
		{
			_dataContext.Connection.Close();
			_dataContext.Dispose();
		}


		private void ExecuteScripts(string path)
		{
				var scriptFiles = new List<string>();
				if (Directory.Exists(path))
				{
					scriptFiles.AddRange(Directory.GetFiles(path, "*.sql"));
				}
				foreach (var script in scriptFiles)
				{
					DatabaseManager.execScriptFile(_config.ConnectionString, script);
				}
		}
		
		private bool IsDevelopmentServer(string connectionString)
		{
			if (connectionString.ToLower().Contains("dev")) return true;
			if (connectionString.ToLower().Contains("black")) return true;
			if (connectionString.ToLower().Contains("localhost")) return true;
			if (connectionString.ToLower().Contains("Data Source=.\\")) return true;
			return false;
		}
	}
}
