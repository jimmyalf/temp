using Spinit.Wpc.Synologen.Presentation.Intranet.Models.LensSubscription;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.LensSubscription
{
	public interface IShopSubscriptionsView : IView<ShopSubscriptionsModel> 
	{
		int CustomerDetailsPageId { get; set; }
		int SubscriptionDetailsPageId { get; set; }
	}
}