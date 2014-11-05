using System;
using FakeItEasy;
using Spinit.Test;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Synologen.LensSubscription.Autogiro.Writers;

namespace Synologen.LensSubscription.Autogiro.Test.TestHelpers
{
	public class TamperProtectedFileWriterTestBase : BehaviorActionTestbase<TamperProtectedFileWriter>
	{
		protected IHashService HashService;
		protected DateTime WriteDate;

		protected override void SetUp()
		{
			HashService = A.Fake<IHashService>();
			WriteDate = new DateTime(2011, 02, 25);
		}

		protected override TamperProtectedFileWriter GetTestEntity()
		{
			return new TamperProtectedFileWriter(HashService, WriteDate);
		}
	}
}