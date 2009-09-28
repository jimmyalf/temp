using System;
using System.Web.UI.WebControls;
using Spinit.Wpc.Member.Business;
using Spinit.Wpc.Synologen.Business.Enumeration;
using Spinit.Wpc.Synologen.Data.Types;
using Spinit.Wpc.Synologen.Presentation.Code;
using Spinit.Wpc.Utility.Business;

namespace Spinit.Wpc.Synologen.Presentation.Components.Synologen {
	public partial class ContractCompanies : SynologenPage {
		private int _contractId = -1;
		//private int _pageSize = -1;
		private ContractRow _selectedContract = new ContractRow();

		public ContractRow SelectedContract {
			get { return _selectedContract; }
		}

		//protected void Page_Init(object sender, EventArgs e) {
		//    RenderMemberSubMenu(Page.Master);
		//}

		protected void Page_Load(object sender, EventArgs e) {
			//plRST.Visible = false;
			if (Request.Params["id"] != null) {
				_contractId = Convert.ToInt32(Request.Params["id"]);
				_selectedContract = Provider.GetContract(_contractId);
				
				//plRST.Visible = true;
				//PopulateRsts(_contractId);
			}
			plFilterByContract.Visible = (_contractId > 0);
			if (Page.IsPostBack) return;
			//PopulateContracts();
			PopulateContractCompanies();
			//PopulateInvoicingMethods();
			//if (_contractId > 0) {
			//    SetupForEdit();

			//}
		}

		//private void PopulateInvoicingMethods() {
		//    drpInvoicingMethods.DataValueField = "cId";
		//    drpInvoicingMethods.DataTextField = "cName";
		//    drpInvoicingMethods.DataSource = Provider.GetInvoicingMethods(null, null);
		//    drpInvoicingMethods.DataBind();
		//    drpInvoicingMethods.Items.Insert(0, new ListItem("-- Välj faktureringsmetod --", "0"));
		//}

		//private void PopulateContracts() {
		//    drpContracts.DataValueField = "cId";
		//    drpContracts.DataTextField = "cName";
		//    drpContracts.DataSource = Provider.GetContracts(FetchCustomerContract.All, 0, 0, null);
		//    drpContracts.DataBind();
		//    drpContracts.Items.Insert(0, new ListItem("-- Välj avtal --", "0"));
		//}


			//private void SetupForEdit() {
			//    ltHeading.Text = "Redigera avtalsföretag";
			//    btnSave.Text = "Ändra";
			//    CompanyRow company = Provider.GetCompanyRow(_contractId);
			//    txtName.Text = company.Name;
			//    txtAddress.Text = company.Address1;
			//    txtAddress2.Text = company.Address2;
			//    txtZip.Text = company.Zip;
			//    txtCity.Text = company.City;
			//    txtCompanyCode.Text = company.CompanyCode;
			//    drpContracts.SelectedValue = company.ContractId.ToString();
			//    txtBankIDCode.Text = company.BankCode;
			//    chkActive.Checked = company.Active;
			//    txtOrganizationNumber.Text = company.OrganizationNumber;
			//    txtAddressCode.Text = company.AddressCode;
			//    txtTaxAccountingCode.Text = company.TaxAccountingCode;
			//    txtPaymentDuePeriod.Text = company.PaymentDuePeriod.ToString();
			//    txtEDIRecipientId.Text = company.EDIRecipientId;
			//    drpInvoicingMethods.SelectedValue = company.InvoicingMethodId.ToString();
			//}

		//private void PopulateRsts(int companyId) {
		//    DataSet rsts = Provider.GetCompanyRSTs(0, companyId, "cId");
		//    gvRST.DataSource = rsts;
		//    gvRST.DataBind();
		//}

		private void PopulateContractCompanies() {

			//Set pagesize
			//_pageSize = SessionContext.ContractCompanies.PageSize;
			//pager.PageSize = _pageSize;


			//Set sorting
			SortExpression = SessionContext.ContractCompanies.SortExpression;
			SortAscending = SessionContext.ContractCompanies.SortAscending;
			var companies = Provider.GetCompanies(0, _contractId, "cId", ActiveFilter.Both);

			gvContractCompanies.DataSource = companies;
			gvContractCompanies.DataBind();

		}

		///// <summary>
		///// Renders the submenu.
		///// </summary>
		//public void RenderMemberSubMenu(MasterPage master) {
		//    SynologenMain m = (SynologenMain)master;
		//    PlaceHolder phMemberSubMenu = m.SubMenu;
		//    SmartMenu.Menu subMenu = new SmartMenu.Menu();
		//    subMenu.ID = "SubMenu";
		//    subMenu.ControlType = "ul";
		//    subMenu.ItemControlType = "li";
		//    subMenu.ItemWrapperElement = "span";

		//    SmartMenu.ItemCollection itemCollection = new SmartMenu.ItemCollection();
		//    itemCollection.AddItem("Avtal", null, "Avtal", "Lista avtal", null, ComponentPages.Contracts, null, null, false, true);
		//    itemCollection.AddItem("Filkategori", null, "Filkategorier", "Lista filkategorier", null, ComponentPages.FileCategories + "?type=ContractCustomer", null, null, false, true);
		//    itemCollection.AddItem("Artikel", null, "Artiklar", "Lista artiklar", null, ComponentPages.ContractArticles, null, null, false, true);

		//    subMenu.MenuItems = itemCollection;

		//    m.SynologenSmartMenu.Render(subMenu, phMemberSubMenu);
		//}

		#region Category Events

		/// <summary>
		/// Add delete confirmation
		/// </summary>
		/// <param Name="sender">The sending object.</param>
		/// <param Name="e">The event arguments.</param>

		protected void btnDelete_AddConfirmDelete(object sender, EventArgs e) {
			ClientConfirmation cc = new ClientConfirmation();
			cc.AddConfirmation(ref sender, "Vill du verkligen ta bort företaget?");
		}

		protected void gvContractCompanies_Editing(object sender, GridViewEditEventArgs e) {
			var index = e.NewEditIndex;

			var contractCompanyId = Convert.ToInt32(gvContractCompanies.DataKeys[index].Value); 
			if (!IsInRole(MemberRoles.Roles.Edit)) {
				Response.Redirect(ComponentPages.NoAccess);
			}
			else {
				if(SelectedContract.Id>0) {
					Response.Redirect(ComponentPages.EditContractCompany +"?id=" + contractCompanyId + "&contractId=" + SelectedContract.Id, true);
				}
				Response.Redirect(ComponentPages.EditContractCompany +"?id=" + contractCompanyId, true);
				
			}
		}

		protected void gvContractCompanies_Deleting(object sender, GridViewDeleteEventArgs e) {
			int index = e.RowIndex;
			int companyId = (int)gvContractCompanies.DataKeys[index].Value;
			if (!IsInRole(MemberRoles.Roles.Delete)) {
				Response.Redirect(ComponentPages.NoAccess);
			}
			else {
				if (Provider.CompanyHasConnectedOrders(companyId)) {
					DisplayMessage("Företaget kan inte raderas då det finns kopplade ordrar.", true);
					return;
				}
				//if (Provider.CompanyHasConnectedRSTs(companyId)) {
				//    DisplayMessage("Företaget kan inte raderas då det finns kopplade RST.", true);
				//    return;
				//}
				CompanyRow company = new CompanyRow();
				company.Id = companyId;
				Provider.AddUpdateDeleteCompany(Enumerations.Action.Delete, ref company);
				Response.Redirect(ComponentPages.ContractCompanies);
			}
		}

		protected void gvContractCompanies_Sorting(object sender, GridViewSortEventArgs e) {
			if (e.SortExpression == SortExpression) SortAscending = !SortAscending;
			else SortAscending = true;

			SortExpression = e.SortExpression;
			SessionContext.ContractCompanies.SortExpression = SortExpression;
			SessionContext.ContractCompanies.SortAscending = SortAscending;

			PopulateContractCompanies();
		}

		protected void gvContractCompanies_PageIndexChanging(object sender, GridViewPageEventArgs e) {
			SessionContext.ContractCompanies.PageIndex = e.NewPageIndex;
			gvContractCompanies.PageIndex = e.NewPageIndex;
			DataBind();
		}

		//protected override void OnInit(EventArgs e) {
		//    pager.IndexChanged += PageIndex_Changed;
		//    pager.IndexButtonChanged += PageIndexButton_Changed;
		//    pager.PageSizeChanged += PageSize_Changed;
		//    base.OnInit(e);
		//}

		//private void PageIndex_Changed(Object sender, EventArgs e) {
		//    SessionContext.ContractCompanies.PageIndex = pager.PageIndex;
		//    PopulateContractCompanies();
		//}

		//private void PageIndexButton_Changed(Object sender, EventArgs e) {
		//    SessionContext.ContractCompanies.PageIndex = pager.PageIndex;
		//    PopulateContractCompanies();
		//}

		//private void PageSize_Changed(Object sender, EventArgs e) {
		//    SessionContext.ContractCompanies.PageSize = pager.PageSize;
		//    PopulateContractCompanies();
		//}

		#endregion

	}

}
