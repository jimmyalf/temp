using Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Models.LensSubscription;
using WebFormsMvp;
using WebFormsMvp.Web;

namespace Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen.LensSubscriptions
{
	[PresenterBinding(typeof(ShopSubscriptionsPresenter))] 
	public partial class ShopSubscripitonsList : MvpUserControl<ShopSubscriptionsModel>, IShopSubscriptionsView
	{
		public int CustomerDetailsPageId { get; set; }
		public int SubscriptionDetailsPageId { get; set; }
	}
}