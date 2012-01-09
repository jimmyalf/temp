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
    	protected const int UseCustomNumberOfWithdrawals = -1;
    	protected const int UseContinousWithdrawals = 0;

    	protected void Page_Load(object sender, EventArgs e)
        {
        	btnCancel.Click += TryFireAbort;
        	btnPreviousStep.Click += TryFirePrevious;
        	btnNextStep.Click += btnNextStep_Click;
        }

    	private void btnNextStep_Click(object sender, EventArgs e)
    	{
			Page.Validate();
			if(!Page.IsValid) return;
    		var args = new AutogiroDetailsEventArgs
    		{
    			BankAccountNumber = txtBankAccountNumber.Text,
				ClearingNumber = txtClearingNumber.Text,
				Description = "? Description",
				Notes = "? Notes",
				NumberOfPayments = GetNumberOfPayments(),
				TaxFreeAmount = txtVatFreeAmount.Text.ToDecimal(),
				TaxedAmount = txtVATAmount.Text.ToDecimal()
    		};
    		TryFireSubmit(this, args);
    	}

    	private int? GetNumberOfPayments()
    	{
    		var selectedSubscriptionTime = rblSubscriptionTime.SelectedValue.ToInt();
			switch (selectedSubscriptionTime)
			{
				case UseCustomNumberOfWithdrawals: return txtCustomNumberOfTransactions.Text.ToInt();
				case UseContinousWithdrawals: return null;
				default: return selectedSubscriptionTime;
			}
    	}

    	protected void Validate_Custom_Subscription_Time(object source, ServerValidateEventArgs args)
    	{
    		args.IsValid = true;
    		if(Equals(rblSubscriptionTime.SelectedValue, UseCustomNumberOfWithdrawals.ToString()))
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