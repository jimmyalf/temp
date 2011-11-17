using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.LensSubscription;
using WebFormsMvp;
using WebFormsMvp.Web;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.LensSubscriptions
{
	[PresenterBinding(typeof(ShopSubscriptionsPresenter))] 
	public partial class ShopSubscripitonsList : MvpUserControl<ShopSubscriptionsModel>, IShopSubscriptionsView
	{
		public int CustomerDetailsPageId { get; set; }
		public int SubscriptionDetailsPageId { get; set; }
	}
}