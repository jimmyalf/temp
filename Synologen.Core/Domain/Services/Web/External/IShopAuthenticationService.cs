namespace Spinit.Wpc.Synologen.Core.Domain.Services.Web.External
{
	public interface IShopAuthenticationService
	{
		ShopAuthenticationResult Authenticate(string username, string password);
		ShopAuthenticationResult Authenticate(AuthenticationContext context);
	}
}