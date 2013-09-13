using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spinit.Wpc.Utility.Business;

namespace Spinit.Wpc.Synologen.OPQ.Business.Test
{
	public class TestConfiguration:ConfigurationFetch
	{
		public const string SynologenOpqSetting = "SynologenOpq-Business-Test";

		/// <summary>
		/// Specifies the path to script folder
		/// </summary>

		static public string DataScriptRootPath
		{
			get
			{
				return SafeConfigString(SynologenOpqSetting, "DataScriptRootPath", string.Empty);
			}
		}

		/// <summary>
		/// Specifies the path to trigger folder
		/// </summary>

		static public string DataScriptTriggerPath
		{
			get
			{
				return string.Concat(DataScriptRootPath,
				                     SafeConfigString(SynologenOpqSetting, "DataScriptTriggerPath", string.Empty));
			}
		}

		/// <summary>
		/// Specifies the path to test data folder
		/// </summary>

		static public string DataScriptTestDataPath
		{
			get
			{
				return string.Concat(DataScriptRootPath,
									 SafeConfigString(SynologenOpqSetting, "DataScriptTestDataPath", string.Empty));
			}
		}

		/// <summary>
		/// Specifies the path to default data folder
		/// </summary>
		static public string DataScriptDefaultDataPath
		{
			get
			{
				return string.Concat(DataScriptRootPath,
									 SafeConfigString(SynologenOpqSetting, "DataScriptDefaultDataPath", string.Empty));
			}
		}

		/// <summary>
		/// Specifies the path to table script folder
		/// </summary>
		static public string DataScriptTablePath
		{
			get
			{
				return string.Concat(DataScriptRootPath,
									 SafeConfigString(SynologenOpqSetting, "DataScriptTablePath", string.Empty));
			}
		}

		/// <summary>
		/// Specifies the path to stored procedure script folder
		/// </summary>
		static public string DataScriptProcedurePath
		{
			get
			{
				return string.Concat(DataScriptRootPath,
									 SafeConfigString(SynologenOpqSetting, "DataScriptProcedurePath", string.Empty));
			}
		}

		/// <summary>
		/// Specifies the path to stored function folder
		/// </summary>
		static public string DataScriptFunctionPath
		{
			get
			{
				return string.Concat(DataScriptRootPath,
									 SafeConfigString(SynologenOpqSetting, "DataScriptFunctionPath", string.Empty));
			}
		}

		/// <summary>
		/// Specifies the path to view script folder
		/// </summary>
		static public string DataScriptViewPath
		{
			get
			{
				return string.Concat(DataScriptRootPath,
									 SafeConfigString(SynologenOpqSetting, "DataScriptViewPath", string.Empty));
			}
		}

	}
}
