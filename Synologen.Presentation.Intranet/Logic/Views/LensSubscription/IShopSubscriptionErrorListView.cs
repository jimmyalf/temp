using Spinit.Wpc.Synologen.Presentation.Intranet.Models.LensSubscription;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.LensSubscription
{
	public interface IShopSubscriptionErrorListView : IView<ShopSubscriptionErrorListModel> 
	{
		int SubscriptionPageId { get; set; }
	}
}