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
    	public override event EventHandler<PaymentOptionsEventArgs> Submit;
    	public override event EventHandler<EventArgs> Abort;
    	public override event EventHandler<EventArgs> Previous;

    	protected void Page_Load(object sender, EventArgs e)
    	{
    		btnCancel.Click += btnCancel_Click;
			btnNextStep.Click += btnNextStep_Click;
			btnPreviousStep.Click += btnPreviousStep_Click;
    	}

    	private void btnPreviousStep_Click(object sender, EventArgs e)
    	{
    		if(Previous == null) return;
    		Previous(this, e);
    	}

    	private void btnNextStep_Click(object sender, EventArgs e)
    	{
			if(Submit == null) return;
    		var value = rblAccounts.SelectedValue.ToInt();
    		Submit(this, new PaymentOptionsEventArgs{SubscriptionId = value});
    	}

    	private void btnCancel_Click(object sender, EventArgs e)
    	{
    		if(Abort == null) return;
    		Abort(this, e);
    	}
    }
}