using System;
using System.Web.UI.WebControls;
using Spinit.Extensions;
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
        public event EventHandler<AutogiroDetailsInvalidFormEventArgs> FillForm;

    	protected void Page_Load(object sender, EventArgs e)
        {
        	btnCancel.Click += TryFireAbort;
        	btnPreviousStep.Click += TryFirePrevious;
        	btnNextStep.Click += btnNextStep_Click;
        }

    	private void btnNextStep_Click(object sender, EventArgs e)
    	{
			Page.Validate();
			if(!Page.IsValid)
			{
                var invalidArgs = new AutogiroDetailsInvalidFormEventArgs
                {
                    BankAccountNumber = txtBankAccountNumber.Text,
                    ClearingNumber = txtClearingNumber.Text,
                    CustomNumberOfPayments = txtCustomNumberOfTransactions.Text,
                    ProductPrice = txtProductAmount.Text.ToDecimal(),
                    FeePrice = txtFeeAmount.Text.ToDecimal(),
					NumberOfPaymentsSelectedValue = GetNumberOfPayments(),
                };
				if(invalidArgs.NumberOfPaymentsSelectedValue == null)
				{
                	invalidArgs.MonthlyFee = txtCustomMonthlyFee.Text.ToDecimal();
					invalidArgs.MonthlyPrice = txtCustomMonthlyPrice.Text.ToDecimal();					
				}
                FillForm(this, invalidArgs);
			    return;
			}

            var args = new AutogiroDetailsEventArgs
            {
                BankAccountNumber = txtBankAccountNumber.Text,
                ClearingNumber = txtClearingNumber.Text,
                NumberOfPayments = GetNumberOfPayments(),
                ProductPrice = txtProductAmount.Text.ToDecimal(),
                FeePrice = txtFeeAmount.Text.ToDecimal(),
            };
			if(args.NumberOfPayments == null)
			{
                args.MonthlyFee = txtCustomMonthlyFee.Text.ToDecimal();
				args.MonthlyProduct = txtCustomMonthlyPrice.Text.ToDecimal();
			}
    		TryFireSubmit(this, args);
    	}

		private int? GetNumberOfPayments()
		{
		    var selectedSubscriptionTime = rblSubscriptionTime.SelectedValue.ToInt();
		    if(selectedSubscriptionTime == AutogiroDetailsModel.UseCustomNumberOfWithdrawalsId)
		    {
		    	return txtCustomNumberOfTransactions.Text.ToInt();
		    }
			if(selectedSubscriptionTime == AutogiroDetailsModel.OngoingSubscription)
			{
				return null;
			}
			return selectedSubscriptionTime;
		}

    	protected void Validate_Custom_Subscription_Time(object source, ServerValidateEventArgs args)
    	{
    		args.IsValid = true;
    		if(Equals(rblSubscriptionTime.SelectedValue, AutogiroDetailsModel.UseCustomNumberOfWithdrawalsId.ToString()))
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