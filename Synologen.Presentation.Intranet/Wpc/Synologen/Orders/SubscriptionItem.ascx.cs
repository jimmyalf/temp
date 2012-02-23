using System;
using System.Web.UI.WebControls;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.Orders;
using WebFormsMvp;
using WebFormsMvp.Web;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Orders
{
	[PresenterBinding(typeof(SubscriptionItemPresenter))] 
	public partial class SubscriptionItem : MvpUserControl<SubscriptionItemModel>, ISubscriptionItemView
	{
		public event EventHandler<SubmitSubscriptionItemEventArgs> Submit;
		public int ReturnPageId { get; set; }

		protected void Validate_Subscription_Time(object source, ServerValidateEventArgs args)
		{
    		args.IsValid = txtNumberOfWithdrawals.Text.ToInt() >= Model.NumerOfPerformedWithdrawals;
		}

		protected void Submit_SubscriptionItem(object sender, EventArgs e)
		{
			if(Submit == null) return;
			var eventArgs = new SubmitSubscriptionItemEventArgs
			{
				TaxFreeAmount = txtVatFreeAmount.Text.ToDecimalOrDefault(),
				TaxedAmount = txtVATAmount.Text.ToDecimalOrDefault(),
				WithdrawalsLimit = txtNumberOfWithdrawals.Text.ToInt()
			};
			Submit(this, eventArgs);
		}
	}
}