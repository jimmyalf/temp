using System;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.Orders;
using WebFormsMvp;
using WebFormsMvp.Web;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Orders
{
    [PresenterBinding(typeof(EditCustomerPresenter))]
    public partial class EditCustomer : MvpUserControl<EditCustomerForm>, IEditCustomerView
    {
    	public event EventHandler<EditCustomerEventArgs> Submit;
		public int NextPageId { get; set; }

    	protected void Page_Load(object sender, EventArgs e)
    	{
    		btnSave.Click += Submit_Event;
    	}

        private void Submit_Event(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid || Submit == null) return;
            var args = new EditCustomerEventArgs
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
            };
        	Submit(this, args);
        }
    }
}