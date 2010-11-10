using Moq;
using Spinit.Wpc.Synologen.Core.Domain.Services;

namespace Spinit.Wpc.Synologen.Presentation.Test.Factories
{
	public static class ServiceFactory
	{
		public static IAdminSettingsService GetSettingsService()
		{
			var mockedService =  new Mock<IAdminSettingsService>();
			mockedService.Setup(x => x.GetDefaultPageSize()).Returns(10);
			return mockedService.Object;
		}
	}
}