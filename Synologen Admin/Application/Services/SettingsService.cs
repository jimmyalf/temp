using Spinit.Wpc.Synologen.Core.Domain.Services;

namespace Spinit.Wpc.Synologen.Presentation.Application.Services
{
	public class SettingsService : IAdminSettingsService
	{
		public int GetDefaultPageSize() { return Business.Globals.DefaultAdminPageSize; }
	}
}