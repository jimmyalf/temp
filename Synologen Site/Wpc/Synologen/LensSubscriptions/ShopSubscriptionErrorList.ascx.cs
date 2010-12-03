using Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Models.LensSubscription;
using WebFormsMvp;
using WebFormsMvp.Web;

namespace Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen.LensSubscriptions
{
	[PresenterBinding(typeof(ShopSubscriptionErrorListPresenter))]
	public partial class ShopSubscriptionErrorList : MvpUserControl<ShopSubscriptionErrorListModel>, IShopSubscriptionErrorListView
	{
		public int SubscriptionPageId { get; set; }
	}
}