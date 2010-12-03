using System;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters.FrameOrders;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views.FrameOrders;
using Spinit.Wpc.Synologen.Presentation.Site.Models.FrameOrders;
using WebFormsMvp;
using WebFormsMvp.Web;

namespace Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen.FrameOrders
{
	[PresenterBinding(typeof(ViewFrameOrderPresenter))]
	public partial class OrderView : MvpUserControl<ViewFrameOrderModel>, IViewFrameOrderView<ViewFrameOrderModel>
	{
		public event EventHandler SendOrder;
		public int RedirectAfterSentOrderPageId { get; set; }
		public int EditPageId { get; set; }

		protected void Page_Load(object sender, EventArgs e) {
			WireupEventProxy();
		}

		private void WireupEventProxy()
		{
			btnSend.Click += SendOrder;
		}

	}
}