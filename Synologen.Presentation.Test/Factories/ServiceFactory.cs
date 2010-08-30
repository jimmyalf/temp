using Spinit.Wpc.Synologen.Core.Domain.Services;

namespace Spinit.Wpc.Synologen.Presentation.Test.Factories
{
	public static class ServiceFactory
	{
		public static IAdminSettingsService GetSettingsService()
		{
			return new MockedSettingsService();
		}
	}

	internal class MockedSettingsService : IAdminSettingsService {
		public int GetDefaultPageSize() { return 10; }
	}
}