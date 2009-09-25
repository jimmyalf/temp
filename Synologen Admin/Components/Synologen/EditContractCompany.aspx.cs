using System;
using System.Web.UI.WebControls;
using Spinit.Wpc.Member.Business;
using Spinit.Wpc.Synologen.Business.Enumeration;
using Spinit.Wpc.Synologen.Data.Types;
using Spinit.Wpc.Synologen.Presentation.Code;
using Spinit.Wpc.Utility.Business;

namespace Spinit.Wpc.Synologen.Presentation.Components.Synologen {
	public partial class EditContractCompany : SynologenPage {
		private int _companyId = -1;
		private int _selectedContractId = -1;

		protected void Page_Load(object sender, EventArgs e) {
			if (Request.Params["id"] != null) {
				_companyId = Convert.ToInt32(Request.Params["id"]);
			}
			if (Request.Params["contractId"] != null) {
				_selectedContractId = Convert.ToInt32(Request.Params["contractId"]);
			}
			if (Page.IsPostBack) return;
			PopulateContracts();
			PopulateInvoicingMethods();
			if (_companyId > 0) {
				SetupForEdit();

			}
		}

		private void PopulateInvoicingMethods() {
			drpInvoicingMethods.DataValueField = "cId";
			drpInvoicingMethods.DataTextField = "cName";
			drpInvoicingMethods.DataSource = Provider.GetInvoicingMethods(null, null);
			drpInvoicingMethods.DataBind();
			drpInvoicingMethods.Items.Insert(0, new ListItem("-- Välj faktureringsmetod --", "0"));
		}

		private void PopulateContracts() {
			drpContracts.DataValueField = "cId";
			drpContracts.DataTextField = "cName";
			drpContracts.DataSource = Provider.GetContracts(FetchCustomerContract.All, 0, 0, null);
			drpContracts.DataBind();
			drpContracts.Items.Insert(0, new ListItem("-- Välj avtal --", "0"));
			if(_selectedContractId>0) {
				drpContracts.SelectedValue = _selectedContractId.ToString();
				drpContracts.Enabled = false;
			}
		}


		private void SetupForEdit() {
			CompanyRow company = Provider.GetCompanyRow(_companyId);
			txtName.Text = company.Name;
			txtAddress.Text = company.Address1;
			txtAddress2.Text = company.Address2;
			txtZip.Text = company.Zip;
			txtCity.Text = company.City;
			txtCompanyCode.Text = company.CompanyCode;
			drpContracts.SelectedValue = company.ContractId.ToString();
			txtBankIDCode.Text = company.BankCode;
			chkActive.Checked = company.Active;
			txtOrganizationNumber.Text = company.OrganizationNumber;
			txtAddressCode.Text = company.AddressCode;
			txtTaxAccountingCode.Text = company.TaxAccountingCode;
			txtPaymentDuePeriod.Text = company.PaymentDuePeriod.ToString();
			txtEDIRecipientId.Text = company.EDIRecipientId;
			drpInvoicingMethods.SelectedValue = company.InvoicingMethodId.ToString();
			//Replace by Databind method
		}


		protected void btnSave_Click(object sender, EventArgs e) {
			if(!Page.IsValid) return;
			if (!IsInRole(MemberRoles.Roles.Create)) {
				Response.Redirect(ComponentPages.NoAccess);
			}
			CompanyRow company = new CompanyRow();
			Enumerations.Action action = Enumerations.Action.Create;
			if (_companyId > 0) {
				company = Provider.GetCompanyRow(_companyId);
				action = Enumerations.Action.Update;
			}
			company.ContractId = Int32.Parse(drpContracts.SelectedValue);
			company.Name = txtName.Text;
			company.Address1 = txtAddress.Text;
			company.Address2 = txtAddress2.Text;
			company.Zip = txtZip.Text;
			company.City = txtCity.Text;
			company.CompanyCode = txtCompanyCode.Text;
			company.BankCode = txtBankIDCode.Text;
			company.Active = chkActive.Checked;

			company.OrganizationNumber = txtOrganizationNumber.Text;
			company.AddressCode = txtAddressCode.Text;
			company.TaxAccountingCode = txtTaxAccountingCode.Text;
			company.PaymentDuePeriod = Convert.ToInt32(txtPaymentDuePeriod.Text);
			company.EDIRecipientId = txtEDIRecipientId.Text;
			company.InvoicingMethodId = Convert.ToInt32(drpInvoicingMethods.SelectedValue);

			Provider.AddUpdateDeleteCompany(action, ref company);
			Response.Redirect(ComponentPages.ContractCompanies + "?id="+ company.ContractId);
		}

	}

}
