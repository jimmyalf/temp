using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.EventArguments.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Models.LensSubscription;
using Spinit.Wpc.Synologen.Core.Extensions;
using WebFormsMvp;
using WebFormsMvp.Web;

namespace Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen.LensSubscriptions
{
	[PresenterBinding(typeof(CreateTransactionPresenter))] 
	public partial class CreateTransaction : MvpUserControl<CreateTransactionModel>, ICreateTransactionView
	{
		public event EventHandler<SaveTransactionEventArgs> Submit;
		public event EventHandler<TransactionReasonEventArgs> SetReasonToWithdrawal;
		public event EventHandler<TransactionReasonEventArgs> SetReasonToCorrection;
		public event EventHandler Cancel;

		protected void Page_Load(object sender, EventArgs e)
		{
			btnSave.Click += Save;
			btnWithdrawal.Click += ReasonToWithdrawal;
			btnCorrection.Click += ReasonToCorrection;
			btnCancel.Click += Cancel;
		}

		private void ReasonToWithdrawal(object sender, EventArgs e)
		{
			if (SetReasonToCorrection == null) return;
			var args = new TransactionReasonEventArgs { Reason = TransactionReason.Withdrawal };
			SetReasonToWithdrawal(this, args);
		}

		private void ReasonToCorrection(object sender, EventArgs e)
		{
			if (SetReasonToCorrection == null) return;
			var args = new TransactionReasonEventArgs { Reason = TransactionReason.Correction };
			SetReasonToCorrection(this, args);
		}

		private void Save(object sender, EventArgs e)
		{
			if (Submit == null) return;

			if (Model.Reason == TransactionReason.Withdrawal)
				drpTransactionType.SelectedValue = ((int)TransactionType.Withdrawal).ToString();

			Page.Validate("vgCreateTransaction");
			if (Page.IsValid == false) return;
			var args = new SaveTransactionEventArgs
			{
				Amount = decimal.Parse(txtAmount.Text),
				TransactionReason = Model.Reason.ToNumberString(),
				TransactionType = drpTransactionType.SelectedValue
			};
			Submit(this, args);
		}
	}
}