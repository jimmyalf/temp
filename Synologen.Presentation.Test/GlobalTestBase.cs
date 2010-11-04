using AutoMapper;
using NUnit.Framework;

namespace Spinit.Wpc.Synologen.Presentation.Test
{
	[SetUpFixture]
	public class GlobalTestBase
	{
		[SetUp]
		public void RunBeforeAnyTests()
		{
			//Setup AutoMapper
		  	Mapper.Initialize(x => x.AddProfile<WpcSynologenAdminProfile>());
			Mapper.AssertConfigurationIsValid();
		}

		[TearDown]
		public void RunAfterAnyTests()
		{
		  // ...
		}
	}
}