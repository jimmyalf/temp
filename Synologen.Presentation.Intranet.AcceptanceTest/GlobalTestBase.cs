using System;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.App.Bootstrapping;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest
{
	[SetUpFixture]
	public class GlobalTestBase
	{
		[SetUp]
		public void RunBeforeAnyTests()
		{
			Console.WriteLine("hej");
			Bootstrapper.Bootstrap();
		}

		[TearDown]
		public void RunAfterAnyTests()
		{
			// ...
			Console.WriteLine("hej då");
		}
	}
}