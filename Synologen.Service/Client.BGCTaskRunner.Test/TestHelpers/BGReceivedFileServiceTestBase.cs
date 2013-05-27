using FakeItEasy;
using Spinit.Test;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Synologen.Service.Client.BGCTaskRunner.App.Services;

namespace Synologen.Service.Client.BGCTaskRunner.Test.TestHelpers
{
    public abstract class BGReceivedFileServiceTestBase : BehaviorActionTestbase<BGReceivedFileReaderService>
    {
        protected IFileIOService FileIOService;
        protected IBGServiceCoordinatorSettingsService BgServiceCoordinatorSettingsService;
        protected IFileSplitter FileSplitter;

        protected BGReceivedFileServiceTestBase()
        {
            FileIOService = A.Fake<IFileIOService>();
            BgServiceCoordinatorSettingsService = A.Fake<IBGServiceCoordinatorSettingsService>();
            FileSplitter = A.Fake<IFileSplitter>();
        }

		protected override BGReceivedFileReaderService GetTestEntity()
		{
			return new BGReceivedFileReaderService(FileIOService, BgServiceCoordinatorSettingsService, BGFtpServiceType.Autogiro, FileSplitter);
		}

    }
}
