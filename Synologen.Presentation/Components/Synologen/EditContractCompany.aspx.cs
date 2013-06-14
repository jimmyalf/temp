using System;
using System.Linq;
using System.Web.UI.WebControls;
using Spinit.Wpc.Member.Business;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Business.Domain.Enumerations;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Code;
using Spinit.Wpc.Utility.Business;

namespace Spinit.Wpc.Synologen.Presentation.Components.Synologen {
	public partial class EditContractCompany : SynologenPage {
		private int _companyId = -1;
		private int _selectedContractId = -1;

		public int SelectedContractID {
			get { return _selectedContractId; }
		}

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
			PopulateValidationRules();
			PopulateCountries();
			if (_companyId > 0) {
				SetupForEdit();

			}
		}

		private void PopulateInvoicingMethods() {
            var items = EnumExtensions
                .Enumerate<InvoicingMethod>()
                .Select(x => new ListItem(x.GetEnumDisplayName(), ((int)x).ToString()));
			drpInvoicingMethods.DataValueField = "Value";
			drpInvoicingMethods.DataTextField = "Text";
		    drpInvoicingMethods.DataSource = items;
			drpInvoicingMethods.DataBind();
			drpInvoicingMethods.Items.Insert(0, new ListItem("-- Välj faktureringsmetod --", "0"));
		}

		private void PopulateContracts() {
			drpContracts.DataValueField = "cId";
			drpContracts.DataTextField = "cName";
			drpContracts.DataSource = Provider.GetContracts(FetchCustomerContract.All, 0, 0, null);
			drpContracts.DataBind();
			drpContracts.Items.Insert(0, new ListItem("-- Välj avtal --", "0"));
			if(SelectedContractID>0) {
				drpContracts.SelectedValue = SelectedContractID.ToString();
				drpContracts.Enabled = false;
			}
		}

		private void PopulateValidationRules() {
			chkValidationRules.DataSource = Provider.GetCompanyValidationRulesDataSet(null, null);
			chkValidationRules.DataBind();
		}
		private void PopulateCountries() {
			drpCountry.DataSource = Provider.GetCountryRows(x=> x.Name);
			drpCountry.DataBind();
		}


		private void SetupForEdit() {
			var company = Provider.GetCompanyRow(_companyId);
			txtName.Text = company.Name;
			txtAddress.Text = company.PostBox;
			txtAddress2.Text = company.StreetName;
			txtZip.Text = company.Zip;
			txtCity.Text = company.City;
			txtCompanyCode.Text = company.SPCSCompanyCode;
			drpContracts.SelectedValue = company.ContractId.ToString();
			txtBankIDCode.Text = company.BankCode;
			chkActive.Checked = company.Active;
			txtOrganizationNumber.Text = company.OrganizationNumber;
			txtInvoiceCompanyName.Text = company.InvoiceCompanyName;
			txtTaxAccountingCode.Text = company.TaxAccountingCode;
			txtPaymentDuePeriod.Text = company.PaymentDuePeriod.ToString();
			txtEDIRecipientId.Text = company.EDIRecipientId;
			drpInvoicingMethods.SelectedValue = company.InvoicingMethodId.ToString();
			txtInvoiceFreeTextTemplate.Text = company.InvoiceFreeTextFormat;
			foreach (var validationRule in company.CompanyValidationRules){
				var listItem = chkValidationRules.Items.FindByValue(validationRule.Id.ToString());
				if(listItem !=null) listItem.Selected = true;
			}
			if(company.Country != null){
				var listItem = drpCountry.Items.FindByValue(company.Country.Id.ToString());
				if(listItem !=null) listItem.Selected = true;
			}

			//Replace by Databind method
		}


		protected void btnSave_Click(object sender, EventArgs e) {
			if(!Page.IsValid) return;
			if (!IsInRole(MemberRoles.Roles.Create)) {
				Response.Redirect(ComponentPages.NoAccess);
			}
			var company = new Company();
			var action = Enumerations.Action.Create;
			if (_companyId > 0) {
				company = Provider.GetCompanyRow(_companyId);
				action = Enumerations.Action.Update;
			}
			company.ContractId = Int32.Parse(drpContracts.SelectedValue);
			company.Name = txtName.Text;
			company.PostBox = txtAddress.Text;
			company.StreetName = txtAddress2.Text;
			company.Zip = txtZip.Text;
			company.City = txtCity.Text;
			company.SPCSCompanyCode = txtCompanyCode.Text;
			company.BankCode = txtBankIDCode.Text;
			company.Active = chkActive.Checked;

			company.OrganizationNumber = txtOrganizationNumber.Text;
			company.InvoiceCompanyName = txtInvoiceCompanyName.Text;
			company.TaxAccountingCode = txtTaxAccountingCode.Text;
			company.PaymentDuePeriod = Convert.ToInt32(txtPaymentDuePeriod.Text);
			company.EDIRecipientId = txtEDIRecipientId.Text;
			company.InvoicingMethodId = Convert.ToInt32(drpInvoicingMethods.SelectedValue);
			company.InvoiceFreeTextFormat = txtInvoiceFreeTextTemplate.Text.Trim();
			if (drpCountry.SelectedValue != "0"){
				company.Country = Provider.GetCountryRow(Int32.Parse(drpCountry.SelectedValue));
			}
			Provider.AddUpdateDeleteCompany(action, ref company);

			ConnectDisconnectValidationRules(company);
			Response.Redirect(ComponentPages.ContractCompanies + "?id="+ company.ContractId);
		}

		private void ConnectDisconnectValidationRules(Company company) {
			foreach (ListItem listItem in chkValidationRules.Items){
				int validationRuleId;
				if(!Int32.TryParse(listItem.Value, out validationRuleId)) throw new ArgumentException("listItem");
				if (listItem.Selected && !company.HasValidationRule(validationRuleId)){ //Connect validation rule
					Provider.ConnectCompanyToValidationRule(company.Id, validationRuleId);
				}
				if (!listItem.Selected && company.HasValidationRule(validationRuleId)){ //Disconnect validation rule
					Provider.DisconnectCompanyFromValidationRule(company.Id, validationRuleId);
				}
			}
		}

	}

}
