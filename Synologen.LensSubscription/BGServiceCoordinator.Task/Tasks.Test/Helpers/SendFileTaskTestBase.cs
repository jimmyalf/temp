using System;
using FakeItEasy;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;
using Synologen.LensSubscription.Autogiro.Writers;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.Test.Helpers
{
	public abstract class SendFileTaskTestBase : TaskTestBase
	{
		protected ITamperProtectedFileWriter TamperProtectedFileWriter;
		protected IHashService HashService;
		protected DateTime WriteDate;

		protected SendFileTaskTestBase()
		{
			WriteDate = new DateTime(2011,02,23);
			HashService = A.Fake<IHashService>();
			TamperProtectedFileWriter = new TamperProtectedFileWriter(HashService, WriteDate);

		}
		protected override ITask GetTask()
		{
			return new SendFile.Task(Log4NetLogger, FileSectionToSendRepository, TamperProtectedFileWriter);
		}
	}
}