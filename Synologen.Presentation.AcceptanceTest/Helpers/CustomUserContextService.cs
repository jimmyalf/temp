using FakeItEasy;
using Spinit.Wpc.Synologen.Presentation.Application.Services;
using Spinit.Wpc.Utility.Core;

namespace Spinit.Wpc.Synologen.Presentation.AcceptanceTest.Helpers
{
	public class CustomUserContextService : IUserContextService
	{
		private static WpcUser _user;

		public WpcUser GetLoggedInUser()
		{
			return GetUser();
		}

		private static WpcUser GetUser()
		{
			if(_user != null) return _user;
			_user = new WpcUser {User = A.Fake<IBaseUserRow>()};
			return _user;
		}

		public static void SetUserWithUserName(string userName)
		{
			var user = GetUser().User;
			A.CallTo(() => user.UserName).Returns(userName);
		}
	}
}