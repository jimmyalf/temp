using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.LensSubscription;
using WebFormsMvp;
using WebFormsMvp.Web;
using Spinit.Extensions;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.LensSubscriptions
{
	[PresenterBinding(typeof(CreateTransactionPresenter))] 
	public partial class CreateTransaction : MvpUserControl<CreateTransactionModel>, ICreateTransactionView
	{
		public event EventHandler<SaveTransactionEventArgs> Submit;
		public event EventHandler<TransactionReasonEventArgs> SetReasonToWithdrawal;
		public event EventHandler<TransactionReasonEventArgs> SetReasonToCorrection;
		public event EventHandler Cancel;
		public event EventHandler Event;
		public event EventHandler<UpdateTransactionModelEventArgs> FormUpdate;

		public void ReasonToWithdrawal(object sender, EventArgs e)
		{
			Event.TryInvoke(sender, e);
			if (SetReasonToCorrection == null) return;
			var args = new TransactionReasonEventArgs { Reason = TransactionReason.Withdrawal };
			SetReasonToWithdrawal(this, args);
		}

		public void ReasonToCorrection(object sender, EventArgs e)
		{
			Event.TryInvoke(sender, e);
			if (SetReasonToCorrection == null) return;
			var args = new TransactionReasonEventArgs { Reason = TransactionReason.Correction };
			SetReasonToCorrection(this, args);
		}

		public void Save(object sender, EventArgs e)
		{
			Event.TryInvoke(sender, e);
			if (Submit == null) return;

			if (Model.Reason == TransactionReason.Withdrawal)
				drpTransactionType.SelectedValue = ((int)TransactionType.Withdrawal).ToString();

			Page.Validate("vgCreateTransaction");
			if (Page.IsValid == false) return;
			var args = new SaveTransactionEventArgs
			{
				Amount = decimal.Parse(txtAmount.Text),
				TransactionReason = Model.Reason.ToNumberString(),
				TransactionType = drpTransactionType.SelectedValue,
                SelectedArticleValue = drpArticle.SelectedValue.ToIntOrDefault()
			};
			Submit(this, args);
		}

		public void btnCancel_Click(object sender, EventArgs e) 
		{
			Event.TryInvoke(sender, e);
			Cancel.TryInvoke(sender, e);
		}

		public void Update_Form(object sender, EventArgs e) 
		{
			var args = new UpdateTransactionModelEventArgs
			{
				Amount = txtAmount.Text,
				TransactionType = drpTransactionType.SelectedValue.ToIntOrDefault(),
				SelectedArticleValue = drpArticle.SelectedValue.ToIntOrDefault()
			};
			FormUpdate.TryInvoke(sender, args);
		}
	}
}