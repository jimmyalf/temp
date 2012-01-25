namespace Spinit.Wpc.Synologen.Core.Domain.Services.Web.External
{
	public class ShopAuthenticationResult
	{
		public ShopAuthenticationResult(bool isAuthenticated, int shopId = default(int))
		{
			IsAuthenticated = isAuthenticated;
			ShopId = shopId;
		}
		public bool IsAuthenticated { get; private set; }
		public int ShopId { get; private set; }
	}
}