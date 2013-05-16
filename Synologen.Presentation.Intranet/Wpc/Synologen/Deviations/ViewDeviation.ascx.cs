using System;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Deviations;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Deviations;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Deviations;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.Deviations;
using WebFormsMvp;
using WebFormsMvp.Web;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Deviations
{
	[PresenterBinding(typeof(ViewDeviationPresenter))]
    public partial class ViewDeviation : MvpUserControl<ViewDeviationModel>, IViewDeviationView
	{
        public event EventHandler<ViewDeviationEventArgs> Submit;

		protected void Page_Load(object sender, EventArgs e)
		{
            btnSaveComment.Click += btnSaveComment_Click;
        }

        void btnSaveComment_Click(object sender, EventArgs e)
        {
            var eventArgs = new ViewDeviationEventArgs{ Comment = txtComment.Text };
            Submit(this, eventArgs);
            txtComment.Text = string.Empty;
        }

    }
}