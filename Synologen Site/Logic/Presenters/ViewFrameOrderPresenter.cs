using System;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views;
using Spinit.Wpc.Synologen.Presentation.Site.Models;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters
{
	public class ViewFrameOrderPresenter : Presenter<IViewFrameOrderView<ViewFrameOrderModel>>
	{
		public ViewFrameOrderPresenter(IViewFrameOrderView<ViewFrameOrderModel> view) : base(view)
		{
			InitiateEventHandlers();
		}

		private void InitiateEventHandlers()
		{
			View.Load += View_Load;
		}

		public void View_Load(object sender, EventArgs e)
		{
			InitializeModel();
		}

		private void InitializeModel() { 

		}


		public override void ReleaseView()
		{
			View.Load -= View_Load;
		}
	}
}
