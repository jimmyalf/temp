using System;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.EventArguments.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Models.LensSubscription;
using WebFormsMvp;
using WebFormsMvp.Web;

namespace Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen.LensSubscriptions
{
	[PresenterBinding(typeof(EditCustomerPresenter))]
	public partial class EditCustomer : MvpUserControl<EditCustomerModel>, IEditCustomerView
	{
		public event EventHandler<SaveCustomerEventArgs> Submit;

		public int RedirectOnSavePageId { get; set; }
		public int EditSubscriptionPageId { get; set; }
		public int CreateSubscriptionPageId { get; set; }

		protected void Page_Load(object sender, EventArgs e)
		{
			btnSave.Click += Save;
		}

		private void Save(object sender, EventArgs e)
		{
			if (Submit == null) return;
			Page.Validate();
			if (Page.IsValid == false) return;
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
				Notes = txtNotes.Text
			};
			Submit(this, args);
		}
	}
}