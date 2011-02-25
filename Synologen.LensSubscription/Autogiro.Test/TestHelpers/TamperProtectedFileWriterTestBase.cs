using System;
using FakeItEasy;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Synologen.LensSubscription.Autogiro.Writers;
using Synologen.Test.Core;

namespace Synologen.LensSubscription.Autogiro.Test.TestHelpers
{
	public class TamperProtectedFileWriterTestBase : BehaviorTestBase<TamperProtectedFileWriter>
	{
		protected IHashService HashService;
		protected DateTime WriteDate;

		protected override void SetUp()
		{
			HashService = A.Fake<IHashService>();
			WriteDate = new DateTime(2011, 02, 25);
		}

		protected override TamperProtectedFileWriter GetTestModel() 
		{ 
			return new TamperProtectedFileWriter(HashService, WriteDate);
		}
	}
}