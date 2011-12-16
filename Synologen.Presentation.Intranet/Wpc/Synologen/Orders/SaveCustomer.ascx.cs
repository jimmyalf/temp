using System;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.Orders;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Orders
{
    [PresenterBinding(typeof(SaveCustomerPresenter))]
    public partial class SaveCustomer : OrderUserControl<SaveCustomerModel, SaveCustomerEventArgs>, ISaveCustomerView
    {
    	public override event EventHandler<EventArgs> Previous;
    	public override event EventHandler<EventArgs> Abort;
    	public override event EventHandler<SaveCustomerEventArgs> Submit;

    	protected void Page_Load(object sender, EventArgs e)
        {
            btnNextStep.Click += NextStep;
        }

        private void NextStep(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid) return;
			if(Submit == null) return;
            var args = new SaveCustomerEventArgs
            {
                AddressLineOne = txtAddressLineOne.Text,
                AddressLineTwo = txtAddressLineTwo.Text,
                City = txtCity.Text,
                Email = txtEmail.Text,
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                MobilePhone = txtMobilePhone.Text,
                PersonalIdNumber = txtPersonalIdNumber.Text,
                PostalCode = txtPostalCode.Text,
                Phone = txtPhone.Text,
                Notes = txtNotes.Text,
				CustomerId = GetCustomerIdFromForm()
            };
            Submit(this, args);
        }

		private int? GetCustomerIdFromForm()
		{
			if (String.IsNullOrEmpty(hfCustomerId.Value)) return null;
			var value = hfCustomerId.Value.ToIntOrDefault();
			return (value > 0) ? value : (int?) null;
		}
    }
}