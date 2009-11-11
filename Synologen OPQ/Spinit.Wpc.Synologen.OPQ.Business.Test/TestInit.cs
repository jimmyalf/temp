using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
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
			_dataContext = new WpcSynologenDataContext(_config.ConnectionString);
		}

		public void InitDatabase()
		{
			CreateDatabase(_dataContext);
			CreateConstraints(_dataContext);
			//CreateProcedures();
			//CreateFunctions();
			CreateTriggers(_dataContext);
			CreateDefaultData(_dataContext);
		}

		private void CreateTriggers(DataContext context)
		{
			const string triggerPath = @"C:\Develop\WPC\CustomerSpecific\Synologen\trunk\Synologen OPQ\Spinit.Wpc.Synologen.OPQ.Database\Schema Objects\Tables\Triggers";
			ExecuteScripts(triggerPath);
		}

		private void CreateConstraints(DataContext context)
		{
			const string constraintsPath = @"C:\Develop\WPC\CustomerSpecific\Synologen\trunk\Synologen OPQ\Spinit.Wpc.Synologen.OPQ.Database\Schema Objects\Tables\Constraints";
			ExecuteScripts(constraintsPath);
		}

		private void CreateDefaultData(DataContext context)
		{
			const string defaultDataPath =
				@"C:\Develop\WPC\CustomerSpecific\Synologen\trunk\Synologen OPQ\Spinit.Wpc.Synologen.OPQ.Database\Scripts\Test Data\Business Layer Tests";
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
	}
}
