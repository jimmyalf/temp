using System;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.EventArguments;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views;
using Spinit.Wpc.Synologen.Presentation.Site.Models;
using WebFormsMvp;
using WebFormsMvp.Web;

namespace Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen 
{
	[PresenterBinding(typeof(MVPTestPresenter), ViewType = typeof(IFrameOrderView<MVPTestModel>))] 
	public partial class MVPTest : MvpUserControl<MVPTestModel>, IFrameOrderView<MVPTestModel> {

		public event EventHandler<FrameSelectedEventArgs> FrameSelected;

		protected void drpFrameList_OnSelectedIndexChanged(object sender, EventArgs e) 
		{
			OnFrameSelected(drpFrameList.SelectedValue);
		}

        private void OnFrameSelected(string selectedValue)
        {
            if (FrameSelected != null)
            {
                FrameSelected(this, new FrameSelectedEventArgs{SelectedValue = selectedValue});
            }
        }

	}
}