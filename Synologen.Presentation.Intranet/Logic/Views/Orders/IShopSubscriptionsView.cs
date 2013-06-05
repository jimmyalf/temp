using Spinit.Wpc.Synologen.Presentation.Intranet.Models.Orders;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders
{
	public interface IShopSubscriptionsView : IView<ShopSubscriptionsModel> 
	{
		int CustomerDetailsPageId { get; set; }
		int SubscriptionDetailsPageId { get; set; }
	}
}