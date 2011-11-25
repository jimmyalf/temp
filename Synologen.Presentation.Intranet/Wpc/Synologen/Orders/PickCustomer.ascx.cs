using System;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.Orders;
using WebFormsMvp;
using WebFormsMvp.Web;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Orders
{
    [PresenterBinding(typeof(PickCustomerPresenter))]
    public partial class PickCustomer : MvpUserControl<PickCustomerModel>, IPickCustomerView
    {
		public int NextPageId { get; set; }
    	public event EventHandler<PickCustomerEventArgs> Submit;
        public event EventHandler<FetchCustomerDataByPersonalIdEventArgs> FetchCustomerByPersonalIdNumber;

        protected void Page_Load(object sender, EventArgs e)
        {
            btnNextStep.Click += NextStep;
            btnFetchByPersonalIdNumber.Click += FillFormFromPersonalIdNumber;
        }

        private void NextStep(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid) return;
			if(Submit == null) return;
            var args = new PickCustomerEventArgs
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
                Notes = txtNotes.Text
            };

            Submit(this, args);

            
        }

        private void FillFormFromPersonalIdNumber(object sender, EventArgs e)
        {
            Page.Validate("PersonalIdNumberValidationGroup");
            if(!Page.IsValid) return;

            var args = new FetchCustomerDataByPersonalIdEventArgs
                           {
                               PersonalIdNumber = txtPersonalIdNumber.Text
                           };

            FetchCustomerByPersonalIdNumber(this, args);
        }
    }
}