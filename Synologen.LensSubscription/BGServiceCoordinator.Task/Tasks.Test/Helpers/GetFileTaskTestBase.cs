using FakeItEasy;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.Test.Helpers
{
    public abstract class GetFileTaskTestBase : CommonTaskTestBase
    {
        protected IFileReaderService FileReaderService;

		protected override void SetUp()
		{
			FileReaderService = A.Fake<IFileReaderService>();
			base.SetUp();
		}

        protected override ITask GetTask()
        {
            return new GetFile.Task(LoggingService, FileReaderService);
        }
    }
}
