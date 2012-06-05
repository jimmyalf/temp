using Spinit.Extensions;

namespace Spinit.Wpc.Synologen.Core.Domain.Services.Events.Synologen
{
	public class ShopWasConnectedToShopGroupEvent
	{
		public int ShopId { get; set; }
		public int ShopGroupId { get; set; }

		public ShopWasConnectedToShopGroupEvent (int shopId, int shopGroupId)
		{
			ShopId = shopId;
			ShopGroupId = shopGroupId;
		}

		public override string ToString()
		{
			return "{ ShopId = {ShopId}, ShopGroupId = {ShopGroupId} }".ReplaceWith (new { ShopId, ShopGroupId });
		}
	}
}