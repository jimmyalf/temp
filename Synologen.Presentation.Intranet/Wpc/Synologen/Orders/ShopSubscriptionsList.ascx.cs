using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.Orders;
using WebFormsMvp;
using WebFormsMvp.Web;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Orders
{
	[PresenterBinding(typeof(ShopSubscriptionsPresenter))] 
	public partial class ShopSubscripitonsList : MvpUserControl<ShopSubscriptionsModel>, IShopSubscriptionsView
	{
		public int CustomerDetailsPageId { get; set; }
		public int SubscriptionDetailsPageId { get; set; }
	}
}