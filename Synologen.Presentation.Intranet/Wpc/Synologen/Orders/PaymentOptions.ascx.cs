using System;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.Orders;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Orders
{
    [PresenterBinding(typeof(PaymentOptionsPresenter))]
    public partial class PaymentOptions : OrderUserControl<PaymentOptionsModel, PaymentOptionsEventArgs>, IPaymentOptionsView
    {
    	protected void Page_Load(object sender, EventArgs e)
    	{
    		btnCancel.Click += TryFireAbort;
			btnNextStep.Click += btnNextStep_Click;
			btnPreviousStep.Click += TryFirePrevious;
    	}

    	private void btnNextStep_Click(object sender, EventArgs e)
    	{
    		var value = rblAccounts.SelectedValue.ToInt();
    		var args = GetSubmitEventArgs(value);
    		TryFireSubmit(sender, args);
    	}

		private PaymentOptionsEventArgs GetSubmitEventArgs(int selectedSubscriptionId)
		{
			return (selectedSubscriptionId == 0)
				? new PaymentOptionsEventArgs { SubscriptionId = null }
				: new PaymentOptionsEventArgs { SubscriptionId = selectedSubscriptionId };
		}

    }
}