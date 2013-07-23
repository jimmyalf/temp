using System;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.LensSubscription;
using WebFormsMvp;
using WebFormsMvp.Web;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.LensSubscriptions
{
	[PresenterBinding(typeof(CreateLensSubscriptionPresenter))] 
	public partial class CreateSubscription : MvpUserControl<CreateLensSubscriptionModel>, ICreateLensSubscriptionView
	{
		public event EventHandler<SaveSubscriptionEventArgs> Submit;
		public int RedirectOnSavePageId { get; set; }

		protected void Page_Load(object sender, EventArgs e)
		{
			btnSave.Click += Save;
		}

		private void Save(object sender, EventArgs e) 
		{
			if (Submit == null) return;
			Page.Validate();
			if(Page.IsValid == false) return;
			var args = new SaveSubscriptionEventArgs
			{
				AccountNumber = txtAccountNumber.Text,
				ClearingNumber = txtClearingNumber.Text,
				MonthlyAmount = txtMonthlyAmount.Text,
				Notes = txtNotes.Text
			};
			Submit(this, args);
		}
	}
}