using System;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.Orders;
using WebFormsMvp;
using WebFormsMvp.Web;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Orders
{
	[PresenterBinding(typeof(SubscriptionCorrectionPresenter))] 
	public partial class SubscriptionCorrection : MvpUserControl<SubscriptionCorrectionModel>, ISubscriptionCorrectionView
	{
		public event EventHandler<SubmitCorrectionEventArgs> Submit;
		public int RedirectOnCreatePageId { get; set; }
		public int ReturnPageId { get; set; }

		protected void Submit_Correction(object sender, EventArgs e)
		{
			if(Submit == null) return;
			var args = new SubmitCorrectionEventArgs
			{
				Amount = decimal.Parse(txtAmount.Text),
				Type = drpTransactionType.SelectedValue.ToInt().ToEnum<TransactionType>()
			};
			Submit(this, args);
		}
	}
}