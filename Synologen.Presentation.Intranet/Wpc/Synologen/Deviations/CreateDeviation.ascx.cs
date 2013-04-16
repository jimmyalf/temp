using System;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Deviations;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Deviations;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Deviations;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.Deviations;
using WebFormsMvp;
using WebFormsMvp.Web;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Deviations
{
	[PresenterBinding(typeof(CreateDeviationPresenter))] 
	public partial class CreateDeviation : MvpUserControl<CreateDeviationModel>, ICreateDeviationView
	{
		public event EventHandler<CreateDeviationEventArgs> Submit;

		protected void Page_Load(object sender, EventArgs e)
		{
			btnSubmit.Click += btnSubmit_OnClick;
		}

		private void btnSubmit_OnClick(object sender, EventArgs e)
		{
			if (Submit == null)
			{
				return;
			}

			Submit(this, new CreateDeviationEventArgs());
		}
	}
}