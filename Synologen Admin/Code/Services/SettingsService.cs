using Spinit.Wpc.Synologen.Core.Domain.Services;

namespace Spinit.Wpc.Synologen.Presentation.Code.Services
{
	public class SettingsService : ISettingsService
	{
		public int GetDefaultPageSize() { return Business.Globals.DefaultAdminPageSize; }
	}
}