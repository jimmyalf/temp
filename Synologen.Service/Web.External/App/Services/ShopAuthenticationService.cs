using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Services.Web.External;

namespace Synologen.Service.Web.External.App.Services
{
	public class ShopAuthenticationService : IShopAuthenticationService 
	{
		public ShopAuthenticationResult Authenticate(string username, string password)
		{
			throw new System.NotImplementedException();
		}

		public ShopAuthenticationResult Authenticate(AuthenticationContext context)
		{
			return Authenticate(context.With(x => x.UserName), context.With(x => x.Password));
		}
	}
}