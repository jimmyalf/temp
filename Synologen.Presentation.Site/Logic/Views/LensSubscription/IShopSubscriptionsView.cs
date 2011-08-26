using Spinit.Wpc.Synologen.Presentation.Site.Models.LensSubscription;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Site.Logic.Views.LensSubscription
{
	public interface IShopSubscriptionsView : IView<ShopSubscriptionsModel> 
	{
		int CustomerDetailsPageId { get; set; }
		int SubscriptionDetailsPageId { get; set; }
	}
}