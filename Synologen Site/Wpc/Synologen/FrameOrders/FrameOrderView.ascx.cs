using System;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views;
using Spinit.Wpc.Synologen.Presentation.Site.Models;
using WebFormsMvp;
using WebFormsMvp.Web;

namespace Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen
{

	[PresenterBinding(typeof(ViewFrameOrderPresenter))]
	public partial class FrameOrderView : MvpUserControl<ViewFrameOrderModel>, IViewFrameOrderView<ViewFrameOrderModel>
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