using System;
using System.Web.UI.WebControls;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Extensions;
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
	    public event EventHandler Stop;
	    public event EventHandler Start;

	    public int ReturnPageId { get; set; }

		protected void Validate_Subscription_Time(object source, ServerValidateEventArgs args)
		{
    		args.IsValid = txtNumberOfWithdrawals.Text.ToInt() >= Model.NumerOfPerformedWithdrawals;
		}

		protected void Submit_SubscriptionItem(object sender, EventArgs e)
		{
		    if (Submit == null)
		    {
		        return;
		    }

		    var eventArgs = new SubmitSubscriptionItemEventArgs 
            {
				ProductAmount = txtProductAmount.Text.ToDecimal(),
				FeeAmount = txtFeeAmount.Text.ToDecimal(),
				WithdrawalsLimit = txtNumberOfWithdrawals.Text.ToNullableInt(),
				CustomMonthlyFeeAmount = txtCustomMonthlyFee.Text.ToNullableDecimal(),
				CustomMonthlyProductAmount = txtCustomMonthlyPrice.Text.ToNullableDecimal()
			};

			Submit(this, eventArgs);
		}

        protected void Stop_SubscriptionItem(object sender, EventArgs e)
	    {
	        if (Stop != null)
	        {
	            Stop(this, new EventArgs());
	        }
	    }

        protected void Start_SubscriptionItem(object sender, EventArgs e)
	    {
            if (Start != null)
            {
                Start(this, new EventArgs());
            }
	    }
	}
}