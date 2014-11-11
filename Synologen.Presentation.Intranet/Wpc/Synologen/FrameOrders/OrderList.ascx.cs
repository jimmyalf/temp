using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.FrameOrders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.FrameOrders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.FrameOrders;
using WebFormsMvp;
using WebFormsMvp.Web;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.FrameOrders
{
	[PresenterBinding(typeof(ListFrameOrdersPresenter))] 
	public partial class OrderList : MvpUserControl<ListFrameOrdersModel>, IListFrameOrdersView<ListFrameOrdersModel> 
	{
		public int ViewPageId { get; set; }
	}
}