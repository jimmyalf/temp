using System;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.EventArguments.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Models.LensSubscription;
using WebFormsMvp;
using WebFormsMvp.Web;

namespace Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen
{
	[PresenterBinding(typeof(EditLensSubscriptionPresenter))] 
	public partial class LensSubscriptionEditSubscription : MvpUserControl<EditLensSubscriptionModel>, IEditLensSubscriptionView
	{
		public event EventHandler<SaveSubscriptionEventArgs> Submit;
		public event EventHandler<EventArgs> StopSubscription;
		public event EventHandler<EventArgs> StartSubscription;
		public int RedirectOnSavePageId { get; set; }
		public int ReturnPageId  { get; set; }

		protected void Page_Load(object sender, EventArgs e)
		{
			btnSave.Click += Save;
			btnStop.Click += Stop;
			btnStart.Click += Start;
		}

		private void Start(object sender, EventArgs e)
		{
			if(StartSubscription == null) return;
			StartSubscription(this,new EventArgs());
		}

		private void Stop(object sender, EventArgs e)
		{
			if(StopSubscription == null) return;
			StopSubscription(this,new EventArgs());
		}

		private void Save(object sender, EventArgs e) 
		{
			if (Submit == null) return;
			Page.Validate("vgCreateSubscription");
			if(Page.IsValid == false) return;
			var args = new SaveSubscriptionEventArgs
			{
				AccountNumber = txtAccountNumber.Text,
				ClearingNumber = txtClearingNumber.Text,
				MonthlyAmount = txtMonthlyAmount.Text.ToDecimalOrDefault(),
				Notes = txtNotes.Text
			};
			Submit(this, args);
		}
	}
}