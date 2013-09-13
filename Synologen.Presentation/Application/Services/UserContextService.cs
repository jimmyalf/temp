using Spinit.Wpc.Utility.Core;

namespace Spinit.Wpc.Synologen.Presentation.Application.Services
{
	public class UserContextService : IUserContextService 
	{
		public WpcUser GetLoggedInUser()
		{
			return FetchWpcUser();
		}

		protected virtual WpcUser FetchWpcUser()
		{
			return CxUser.Current;
		}
	}
}