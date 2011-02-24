using FakeItEasy;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.Test.Helpers
{
	public abstract class SendFileTaskTestBase : CommonTaskTestBase
	{
		protected ITamperProtectedFileWriter TamperProtectedFileWriter;
		protected IFtpService FtpService;
		protected IFileWriterService FileWriterService;

		protected SendFileTaskTestBase()
		{
			TamperProtectedFileWriter = A.Fake<ITamperProtectedFileWriter>();
			FtpService = A.Fake<IFtpService>();
			FileWriterService = A.Fake<IFileWriterService>();
		}
		protected override ITask GetTask()
		{
			return new SendFile.Task(
				Log4NetLogger, 
				FileSectionToSendRepository,
				TamperProtectedFileWriter,
				FtpService,
				FileWriterService);
		}
	}
}