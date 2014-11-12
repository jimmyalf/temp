using System.Web;

using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.Events.Synologen;
using Spinit.Wpc.Synologen.OPQ.Business;
using Spinit.Wpc.Synologen.OPQ.Core;

namespace Spinit.Wpc.Synologen.OPQ.Presentation.Admin.App
{
	public class HandleShopMove : IListener<ShopWasConnectedToShopGroupEvent>
	{
		public void Handle (ShopWasConnectedToShopGroupEvent message)
		{
			BShopGroup bShopGroup = new BShopGroup (SessionContext.CurrentOpq);

			string shopPath = HttpContext.Current.Server.MapPath (string.Concat (Configuration.DocumentShopRootUrl, "/", message.ShopId));
			string shopGroupPath = HttpContext.Current.Server.MapPath (string.Concat (Configuration.DocumentShopGroupRootUrl, "/", message.ShopGroupId));

			if (bShopGroup.IsFirst (message.ShopGroupId)) {
				bShopGroup.MoveDocumentsToGroup (message.ShopId, message.ShopGroupId);
				bShopGroup.MoveFilesToGroup (
					message.ShopId, 
					message.ShopGroupId, 
					string.Concat (Configuration.DocumentShopGroupRootUrl, "/", message.ShopGroupId, "/"),
					shopPath, 
					shopGroupPath);
			}
		}
	}
}