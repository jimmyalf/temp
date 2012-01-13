using System;
using NUnit.Framework;
using Spinit.Wpc.Synogen.Test.Data;
using Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.App.Bootstrapping;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest
{
	[SetUpFixture]
	public class GlobalTestBase
	{
		[SetUp]
		public void RunBeforeAnyTests()
		{
			Bootstrapper.Bootstrap();
		}

		[TearDown]
		public void RunAfterAnyTests()
		{
			// ...
			new DataManager().CleanTables();
		}
	}
}