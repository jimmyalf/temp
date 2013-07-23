using FakeItEasy;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Synologen.LensSubscription.Autogiro.Readers;

namespace Synologen.LensSubscription.Autogiro.Test.TestHelpers
{
	public abstract class FileSplitterTesteBase
	{
		protected IBGServiceCoordinatorSettingsService bgServiceCoordinatorSettingsService;
		protected IFileSplitter fileSplitter;
		protected FileSplitterTesteBase()
		{
			bgServiceCoordinatorSettingsService = A.Fake<IBGServiceCoordinatorSettingsService>();
			fileSplitter = new ReceivedFileSplitter(bgServiceCoordinatorSettingsService);
		}

	}
}