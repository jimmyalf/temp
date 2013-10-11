using System;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.Orders;
using WebFormsMvp;
using WebFormsMvp.Web;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Orders
{
    [PresenterBinding(typeof(EditSubscriptionPresenter))]
    public partial class EditSubscription : MvpUserControl<EditSubscriptionForm>, IEditSubscriptionView
    {
    	public event EventHandler<ResetSubscriptonEventArgs> ResetSubscription;
		public int ReturnPageId { get; set; }

    	protected void Page_Load(object sender, EventArgs e)
    	{
    		btnResetSubscription.Click += Reset_Subscription_OnClick;
    	}

    	private void Reset_Subscription_OnClick(object sender, EventArgs e)
    	{
    		if(ResetSubscription == null) return;
    		var args = new ResetSubscriptonEventArgs
    		{
    			BankAccountNumber = txtBankAccountNumber.Text,
				ClearingNumber = txtClearingNumber.Text
    		};
			ResetSubscription(this, args);
    	}
    }
}