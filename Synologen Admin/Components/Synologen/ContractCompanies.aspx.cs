using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Spinit.Wpc.Member.Business;
using Spinit.Wpc.Synologen.Business.Enumeration;
using Spinit.Wpc.Synologen.Data.Types;
using Spinit.Wpc.Synologen.Presentation.Code;
using Spinit.Wpc.Utility.Business;
using Spinit.Wpc.Utility.Business.SmartMenu;

namespace Spinit.Wpc.Synologen.Presentation.Components.Synologen {
	public partial class ContractCompanies : SynologenPage {
		private int _companyId = -1;

		//protected void Page_Init(object sender, EventArgs e) {
		//    RenderMemberSubMenu(Page.Master);
		//}

		protected void Page_Load(object sender, EventArgs e) {
			//plRST.Visible = false;
			if (Request.Params["id"] != null) {
				_companyId = Convert.ToInt32(Request.Params["id"]);
				//plRST.Visible = true;
				//PopulateRsts(_companyId);
			}
			if (Page.IsPostBack) return;
			PopulateContracts();
			PopulateContractCompanies();
			if (_companyId > 0) {
				SetupForEdit();

			}
		}

		private void PopulateContracts() {
			drpContracts.DataValueField = "cId";
			drpContracts.DataTextField = "cName";
			drpContracts.DataSource = Provider.GetContracts(FetchCustomerContract.All, 0, 0, null);
			drpContracts.DataBind();
			drpContracts.Items.Insert(0, new ListItem("-- Välj avtal --", "0"));
		}


		private void SetupForEdit() {
			ltHeading.Text = "Redigera avtalsföretag";
			btnSave.Text = "Ändra";
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
		}

		//private void PopulateRsts(int companyId) {
		//    DataSet rsts = Provider.GetCompanyRSTs(0, companyId, "cId");
		//    gvRST.DataSource = rsts;
		//    gvRST.DataBind();
		//}

		private void PopulateContractCompanies() {
			DataSet companies = Provider.GetCompanies(0, 0, "cId", ActiveFilter.Both);
			gvContractCompanies.DataSource = companies;
			gvContractCompanies.DataBind();
			ltHeading.Text = "Lägg till avtalsföretag";
			btnSave.Text = "Spara";

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
			int index = e.NewEditIndex;

			int articleId = Convert.ToInt32(gvContractCompanies.DataKeys[index].Value); 
			if (!IsInRole(MemberRoles.Roles.Edit)) {
				Response.Redirect(ComponentPages.NoAccess);
			}
			else {
				Response.Redirect(ComponentPages.ContractCompanies +"?id=" + articleId, true);
				
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

		protected void btnSave_Click(object sender, EventArgs e) {
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
			Provider.AddUpdateDeleteCompany(action, ref company);
			Response.Redirect(ComponentPages.ContractCompanies);
		}


		#endregion

		//protected void gvRST_Deleting(object sender, GridViewDeleteEventArgs e) {
		//    int index = e.RowIndex;
		//    int rstId = (int)gvRST.DataKeys[index].Value;
		//    int numberOfConnectedOrders = (int)gvRST.DataKeys[index].Values["cConnectedOrders"];
		//    if (numberOfConnectedOrders>0) return;


		//    const Enumerations.Action action = Enumerations.Action.Delete;
		//    RSTRow rst = new RSTRow();
		//    rst.Id = rstId;
		//    Provider.AddUpdateDeleteRST(action, ref rst);
		//    Response.Redirect(ComponentPages.ContractCompanies + "?" + Request.QueryString);
		//}

		//protected void btnSaveRST_Click(object sender, EventArgs e) {
		//    const Enumerations.Action action = Enumerations.Action.Create;
		//    RSTRow rst = new RSTRow();
		//    rst.Name = txtNewRST.Text;
		//    rst.CompanyId = _companyId;
		//    Provider.AddUpdateDeleteRST(action, ref rst);
		//    Response.Redirect(ComponentPages.ContractCompanies + "?" + Request.QueryString);
		//}


		//protected void gvRST_RowDataBound(object sender, GridViewRowEventArgs e) {
		//    if (e.Row.RowType != DataControlRowType.DataRow) return;
		//    DataRowView rowView = (DataRowView)e.Row.DataItem;
		//    int numberOfConnections = Int32.Parse(rowView["cConnectedOrders"].ToString());
		//    if(numberOfConnections>0){ 
		//        //Disables delete-button if there are orders connected to this RST
		//        e.Row.Cells[2].Enabled = false;
		//    }

		//}
	}

}
