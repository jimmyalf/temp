using System;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.EventArguments.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Models.LensSubscription;
using WebFormsMvp;
using WebFormsMvp.Web;

namespace Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen.LensSubscriptions
{
	[PresenterBinding(typeof(EditLensSubscriptionPresenter))] 
	public partial class EditSubscription : MvpUserControl<EditLensSubscriptionModel>, IEditLensSubscriptionView
	{
		public event EventHandler<SaveSubscriptionEventArgs> Submit;
		public event EventHandler<EventArgs> StopSubscription;
		public event EventHandler<EventArgs> StartSubscription;
		public event EventHandler<SaveSubscriptionEventArgs> UpdateForm;
		public int RedirectOnSavePageId { get; set; }
		public int ReturnPageId  { get; set; }

		protected void btn_Start(object sender, EventArgs e)
		{
			if(StartSubscription == null) return;
			StartSubscription(this, e);
		}

		protected void btn_Stop(object sender, EventArgs e)
		{
			if(StopSubscription == null) return;
			StopSubscription(this, e);
		}

		protected void btn_Save(object sender, EventArgs e) 
		{
			if(Submit == null) return;
			Page.Validate("vgCreateSubscription");
			if(Page.IsValid == false) return;
			var args = GetEventArgs();
			Submit(this, args);
		}

		protected void Update_Form(object sender, EventArgs e) 
		{
			var args = GetEventArgs();
			if(UpdateForm!= null) UpdateForm(this, args);
		}

		private SaveSubscriptionEventArgs GetEventArgs()
		{
			return new SaveSubscriptionEventArgs
			{
				AccountNumber = txtAccountNumber.Text,
				ClearingNumber = txtClearingNumber.Text,
				MonthlyAmount = txtMonthlyAmount.Text,
				Notes = txtNotes.Text
			};
		}
	}
}