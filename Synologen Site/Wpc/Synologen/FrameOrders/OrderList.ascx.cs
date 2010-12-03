using Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters.FrameOrders;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views.FrameOrders;
using Spinit.Wpc.Synologen.Presentation.Site.Models.FrameOrders;
using WebFormsMvp;
using WebFormsMvp.Web;

namespace Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen.FrameOrders
{
	[PresenterBinding(typeof(ListFrameOrdersPresenter))] 
	public partial class OrderList : MvpUserControl<ListFrameOrdersModel>, IListFrameOrdersView<ListFrameOrdersModel> 
	{
		public int ViewPageId { get; set; }
	}
}