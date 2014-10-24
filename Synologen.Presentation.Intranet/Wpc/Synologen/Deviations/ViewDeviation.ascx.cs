using System;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Deviations;
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
        public event EventHandler<ViewDeviationEventArgs> StatusSubmit;

		protected void Page_Load(object sender, EventArgs e)
		{
            btnSaveComment.Click += btnSaveComment_Click;
            btnSaveStatus.Click += btnSaveStatus_Click;
        }

        void btnSaveStatus_Click(object sender, EventArgs e)
        {
            var eventArgs = new ViewDeviationEventArgs { Comment = txtComment.Text, SelectedStatus = (DeviationStatus)drpStatus.SelectedValue.ToIntOrDefault(0) };
            StatusSubmit(this, eventArgs);
        }

        void btnSaveComment_Click(object sender, EventArgs e)
        {
            var eventArgs = new ViewDeviationEventArgs { Comment = txtComment.Text, SelectedStatus = (DeviationStatus)drpStatus.SelectedValue.ToIntOrDefault(0) };
            Submit(this, eventArgs);
            txtComment.Text = string.Empty;
        }

    }
}