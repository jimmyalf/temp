using Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views;
using Spinit.Wpc.Synologen.Presentation.Site.Models;
using WebFormsMvp;
using WebFormsMvp.Web;

namespace Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen
{
	[PresenterBinding(typeof(ListFrameOrdersPresenter))] 
	public partial class FrameOrderList : MvpUserControl<ListFrameOrdersModel>, IListFrameOrdersView<ListFrameOrdersModel> {
		public int ViewPageId { get; set; }
	}
}