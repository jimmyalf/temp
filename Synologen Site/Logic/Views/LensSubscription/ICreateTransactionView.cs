using System;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.EventArguments.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Models.LensSubscription;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Site.Logic.Views.LensSubscription
{
	public interface ICreateTransactionView : IView<CreateTransactionModel>
	{
		event EventHandler<SaveTransactionEventArgs> Submit;
		event EventHandler<TransactionReasonEventArgs> SetReasonToWithdrawal;
		event EventHandler<TransactionReasonEventArgs> SetReasonToCorrection;
		event EventHandler Cancel;
		event EventHandler Event;
		event EventHandler<UpdateTransactionModelEventArgs> FormUpdate;
	}
}
