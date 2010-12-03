using Spinit.Wpc.Synologen.Presentation.Site.Models.LensSubscription;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Site.Logic.Views.LensSubscription
{
	public interface IShopSubscriptionErrorListView : IView<ShopSubscriptionErrorListModel> 
	{
		int SubscriptionPageId { get; set; }
	}
}