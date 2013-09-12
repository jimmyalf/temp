using System;
using System.Web.UI.WebControls;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Enumerations;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.Orders;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Orders
{
    [PresenterBinding(typeof(AutogiroDetailsPresenter))]
    public partial class AutogiroDetails : OrderUserControl<AutogiroDetailsModel, AutogiroDetailsEventArgs>, IAutogiroDetailsView
    {
        public event EventHandler<AutogiroDetailsEventArgs> FillForm;
    	public event EventHandler<AutogiroDetailsEventArgs> SelectedSubscriptionTimeChanged;

    	protected void Page_Load(object sender, EventArgs e)
        {
        	btnCancel.Click += TryFireAbort;
        	btnPreviousStep.Click += TryFirePrevious;
        	btnNextStep.Click += btnNextStep_Click;
			rblSubscriptionTime.SelectedIndexChanged += RblSubscriptionTimeOnSelectedIndexChanged;
        }

    	private void RblSubscriptionTimeOnSelectedIndexChanged(object sender, EventArgs eventArgs)
    	{
    		var args = GetArgs();
			if (SelectedSubscriptionTimeChanged != null) SelectedSubscriptionTimeChanged(this, args);
    	}

    	private void btnNextStep_Click(object sender, EventArgs e)
    	{
			Page.Validate();
    		var args = GetArgs();
			if(!Page.IsValid)
			{
				if(FillForm != null) FillForm(this, args);
			}
			else
			{
				TryFireSubmit(this, args);	
			}
    	}

		private AutogiroDetailsEventArgs GetArgs()
		{
            var args = new AutogiroDetailsEventArgs
            {
                BankAccountNumber = txtBankAccountNumber.Text,
                ClearingNumber = txtClearingNumber.Text,
                ProductPrice = txtProductAmount.Text.ToNullableDecimal(),
                FeePrice = txtFeeAmount.Text.ToNullableDecimal(),
				Type = GetSubscriptionType(),
                // Title = txtName.Text
            };
			if(args.Type == SubscriptionType.Ongoing)
			{
                args.MonthlyFee = txtCustomMonthlyFee.Text.ToNullableDecimal();
				args.MonthlyProduct = txtCustomMonthlyPrice.Text.ToNullableDecimal();
			}
			return args;
		}

		private SubscriptionType GetSubscriptionType()
		{
		    var type = SubscriptionType.FromValue(rblSubscriptionTime.SelectedValue.ToInt());
			if(type == SubscriptionType.CustomNumberOfWithdrawals)
			{
		    	int output;
				if(Int32.TryParse(txtCustomNumberOfTransactions.Text,out output))
				{
					type.SetCustomNumberOfWithdrawals(output);
				}
			}
			return type;
		}

    	protected void Validate_Custom_Subscription_Time(object source, ServerValidateEventArgs args)
    	{
    		args.IsValid = true;
    		if(Equals(rblSubscriptionTime.SelectedValue, SubscriptionType.CustomNumberOfWithdrawals.Value.ToString()))
    		{
    			args.IsValid = CanBeParsedToNumber(txtCustomNumberOfTransactions.Text);
    		}
    	}

		private bool CanBeParsedToNumber(string input)
		{
			int parsedValue;
			if(String.IsNullOrEmpty(input)) return false;
			if(!Int32.TryParse(input, out parsedValue)) return false;
			if(parsedValue <= 0) return false;
			return true;
		}
    }
}