using Spinit.Wpc.Utility.Core;

namespace Spinit.Wpc.Synologen.Presentation.Application.Services
{
	public interface IUserContextService
	{
		WpcUser GetLoggedInUser();
	}
}