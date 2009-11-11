using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using NUnit.Framework;
using Spinit.Wpc.Synologen.OPQ.Business.Test.Properties;
using Spinit.Wpc.Synologen.OPQ.Core;

namespace Spinit.Wpc.Synologen.OPQ.Business.Test
{
	[TestFixture, Description("NUnit tests for filecategory business layer")]
	public class BFileCategoryTests
	{
		private Configuration _configuration;
		private Context _context;
		private TestInit _init;

		[SetUp, Description("Initialize.")]
		public void NodeManagerInit()
		{
			_configuration = new Configuration(
				Settings.Default.ConnectionString,
				Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName);

			_context = new Context(
				Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName,
				string.Empty,
				1,
				"Admin");
			_init = new TestInit(_configuration);
			_init.InitDatabase();
		}

		[TearDown, Description("Close.")]
		public void NodeManagerCleanUp()
		{
			_init.DeleteDatabase();
			_configuration = null;
			_context = null;
		}


	}
}
