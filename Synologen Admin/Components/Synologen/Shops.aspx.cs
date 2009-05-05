// ==========================================================================
//
//  PROGRAM
//	
//
//  FILENAME
//	$Workfile: Shops.aspx.cs $
//
//  ARCHIVE
//  $Archive: /Develop/WPC/Version 4.1/CustomerSpecific/Synologen/Synologen Admin/Components/Synologen/Shops.aspx.cs $
//
//  VERSION
//	$Revision: 5 $
//
//  DATES
//	Last check in: $Date: 09-01-08 18:08 $
//	Last modified: $Modtime: 09-01-08 13:33 $
//
//  AUTHOR(S)
//	$Author: Cber $
// 	
//
//  COPYRIGHT
// 	Copyright (c) 2008 Spinit AB --- ALL RIGHTS RESERVED
// 	Spinit AB, Datavägen 2, 436 32 Askim, SWEDEN
//
// ==========================================================================
// 
//  DESCRIPTION
//  
//
// ==========================================================================
//
//	History
//
//	$Log: /Develop/WPC/Version 4.1/CustomerSpecific/Synologen/Synologen Admin/Components/Synologen/Shops.aspx.cs $
//
//5     09-01-08 18:08 Cber
//
//4     09-01-07 17:35 Cber
//
//3     08-12-22 17:22 Cber
//
//2     08-12-19 17:22 Cber
//
//1     08-12-16 17:01 Cber
// 
// ==========================================================================
using System;
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
	public partial class Shops : SynologenPage {
		private string _searchString;
		private int _pageSize;

		protected void Page_Init(object sender, EventArgs e) {
			RenderMemberSubMenu(Page.Master);
		}

		protected void Page_Load(object sender, EventArgs e) {

			if (!Page.IsPostBack) {
				PopulateCategories();
				PopulateContractCustomers();
				PopulateEquipment();
				PopulateShops();
			}
		}

		private void PopulateEquipment() {
			drpEquipment.DataValueField = "cId";
			drpEquipment.DataTextField = "cName";
			drpEquipment.DataSource = Provider.GetShopEquipment(0,0,null);
			drpEquipment.DataBind();
			drpEquipment.Items.Insert(0, new ListItem("-- Välj Utrustning --", "0"));
		}

		private void PopulateContractCustomers() {
			drpContractCustomers.DataValueField = "cId";
			drpContractCustomers.DataTextField = "cName";
			drpContractCustomers.DataSource = Provider.GetContracts(FetchCustomerContract.All, 0, 0);
			drpContractCustomers.DataBind();
			drpContractCustomers.Items.Insert(0,new ListItem("-- Välj Avtalskund --", "0"));
		}

		private void PopulateCategories() {
			drpCategories.DataValueField = "cId";
			drpCategories.DataTextField = "cName";
			//drpCategories.DataSource = Provider.GetAllCategoriesList(LocationId, LanguageId);
			drpCategories.DataSource = Provider.GetShopCategories(0);
			drpCategories.DataBind();
			drpCategories.Items.Insert(0,new ListItem("-- Välj Kategori --", "0"));
		}

		/// <summary>
		/// Renders the submenu.
		/// </summary>
		public void RenderMemberSubMenu(MasterPage master) {
			SynologenMain m = (SynologenMain)master;
			PlaceHolder phMemberSubMenu = m.SubMenu;
			SmartMenu.Menu subMenu = new SmartMenu.Menu();
			subMenu.ID = "SubMenu";
			subMenu.ControlType = "ul";
			subMenu.ItemControlType = "li";
			subMenu.ItemWrapperElement = "span";

			SmartMenu.ItemCollection itemCollection = new SmartMenu.ItemCollection();
			itemCollection.AddItem("New", null, "Ny", "Skapa ny butik", null, "btnAdd_OnClick", false, null);
			itemCollection.AddItem("Delete", null, "Radera", "Radera valda butiker", null, "btnDelete_OnClick", false,null,null,false,true,false);
			itemCollection.AddItem("Butikkategori", null, "Butikkategorier", "Lista butikkategorier", null, ComponentPages.ShopCategories, null, null, false, true);
			itemCollection.AddItem("Butiksutrustning", null, "Butiksutrustning", "Lista Butiksutrustning", null, ComponentPages.ShopEquipment, null, null, false, true);
			subMenu.MenuItems = itemCollection;

			m.SynologenSmartMenu.Render(subMenu, phMemberSubMenu);
		}

		protected override void OnInit(EventArgs e) {
			pager.IndexChanged += PageIndex_Changed;
			pager.IndexButtonChanged += PageIndexButton_Changed;
			pager.PageSizeChanged += PageSize_Changed;
			base.OnInit(e);
		}

		protected DataView SortDataTable(DataTable dataTable, bool isPageIndexChanging) {
			if (dataTable != null) {
				DataView dataView = new DataView(dataTable);
				if (SortExpression != string.Empty) {
					dataView.Sort = isPageIndexChanging ? string.Format("{0} {1}", SortExpression, SortAscending ? "ASC" : "DESC") : string.Format("{0} {1}", SortExpression, SortAscending ? "ASC" : "DESC");
				}
				return dataView;
			}
			return new DataView();
		}

		private void PopulateShops() {
			int totalRecords = 0;

			//Set pagesize
			_pageSize = SessionContext.Shops.PageSize;
			pager.PageSize = _pageSize;

			//Set drop-downs
			txtSearch.Text = _searchString;

			//Set sorting
			SortExpression = SessionContext.Shops.SortExpression;
			SortAscending = SessionContext.Shops.SortAscending;

			//Set pageindex
			//pager.PageIndex = SessionContext.Shops.PageIndex;

			int categoryId = Int32.Parse(drpCategories.SelectedValue);
			int contractCustomerId = Int32.Parse(drpContractCustomers.SelectedValue);
			int equipmentId = Int32.Parse(drpEquipment.SelectedValue);
			DataSet dsShops = Provider.GetShopsByPage(
				categoryId, 
				contractCustomerId,
				equipmentId,
				_searchString,
				SortExpression + ((SortAscending) ? " ASC" : " DESC"),
				pager.PageIndex,
				pager.PageSize,
				ref totalRecords);
			gvShops.DataSource = dsShops.Tables[0];
			gvShops.DataBind();
			pager.TotalRecords = totalRecords;
			pager.TotalPages = pager.CalculateTotalPages();
			setActive(dsShops);
		}

		#region Common Events

		protected void btnSetFilter_Click(Object sender, EventArgs e) {
			SessionContext.Shops.PageIndex = pager.PageIndex;
			PopulateShops();
		}

		protected void btnSearch_Click(Object sender, EventArgs e) {
			_searchString = txtSearch.Text;
			pager.PageIndex = 0;
			SessionContext.Shops.PageIndex = pager.PageIndex;
			SessionContext.Shops.SearchExpression = _searchString;
			PopulateShops();
		}

		protected void btnAdd_OnClick(object sender, EventArgs e) {
			Response.Redirect("EditShop.aspx");
		}

		protected void btnDelete_OnClick(object sender, EventArgs e) {
			foreach (GridViewRow row in gvShops.Rows) {
				CheckBox chk = (CheckBox)row.FindControl("chkSelect");
				if ((chk == null) || !chk.Checked) continue;
				int id = (int)gvShops.DataKeys[row.RowIndex]["cId"];
				//TODO: Check For connected members before deleting
				if (Provider.ShopHasConnectedOrders(id)) {
					DisplayMessage("Butiken kan inte raderas då det finns kopplade ordrar.", true);
					return;
				}
				if (Provider.ShopHasConnectedMembers(id)) {
					DisplayMessage("Butiken kan inte raderas då det finns kopplade medlemmar.", true);
					return;
				}
				if (Provider.ShopHasConnectedContracts(id)) {
					DisplayMessage("Butiken kan inte raderas då det finns kopplade avtal.", true);
					return;
				}
				ShopRow shopToDelete = new ShopRow();
				shopToDelete.ShopId = id;
				Provider.AddUpdateDeleteShop(Enumerations.Action.Delete, ref shopToDelete);
			}

			PopulateShops();
		}

		protected void chkSelectHeader_CheckedChanged(object sender, EventArgs e) {
			CheckBox chkHeader = (CheckBox)sender;
			if (chkHeader == null) return;
			foreach (GridViewRow row in gvShops.Rows) {
				CheckBox chk = (CheckBox)row.FindControl("chkSelect");
				if (chk != null) {
					chk.Checked = chkHeader.Checked;
				}
			}
		}


		private void setActive(DataSet ds) {
			int i = 0;
			foreach (GridViewRow row in gvShops.Rows) {
				bool active = Convert.ToBoolean(ds.Tables[0].Rows[i]["cActive"]);
				if (row.FindControl("imgActive") != null) {
					Image img = (Image) row.FindControl("imgActive");
					if (active) {
						img.ImageUrl = "~/common/icons/True.png";
						img.AlternateText = "Active";
						img.ToolTip = "Active";
					}
					else {
						img.ImageUrl = "~/common/icons/False.png";
						img.AlternateText = "Inactive";
						img.ToolTip = "Inactive";
					}
				}
				i++;
			}
		}
		#endregion

		#region GridView Events

		protected void gvShops_PageIndexChanging(object sender, GridViewPageEventArgs e) {
			SessionContext.Shops.PageIndex = e.NewPageIndex;
			gvShops.PageIndex = e.NewPageIndex;
			DataBind();
		}

		protected void gvShops_Sorting(object sender, GridViewSortEventArgs e) {
			if (e.SortExpression == SortExpression) SortAscending = !SortAscending;
			else SortAscending = true;

			SortExpression = e.SortExpression;
			SessionContext.Shops.SortExpression = SortExpression;
			SessionContext.Shops.SortAscending = SortAscending;

			PopulateShops();
		}



		protected void gvShops_Deleting(object sender, GridViewDeleteEventArgs e) {
			int id = (int)gvShops.DataKeys[e.RowIndex].Value;

			//TODO: Check For connected members before deleting
			
			ShopRow shopToDelete = new ShopRow();
			shopToDelete.ShopId = id;
			Provider.AddUpdateDeleteShop(Enumerations.Action.Delete, ref shopToDelete);

			PopulateShops();
		}

		protected void gvShops_Editing(object sender, GridViewEditEventArgs e) {
			if (!IsInRole(MemberRoles.Roles.Edit)) {
				Response.Redirect(ComponentPages.NoAccess);
			}
			else {
				int index = e.NewEditIndex;
				int shopId = (int)gvShops.DataKeys[index].Value;

				Response.Redirect(ComponentPages.EditShop + "?id=" + shopId);
			}
		}

		protected void AddConfirmDelete(object sender, EventArgs e) {
			ClientConfirmation cc = new ClientConfirmation();
			cc.AddConfirmation(ref sender, "Do you really want to delete the shop?");
		}

		#endregion

		#region Pager Events

		private void PageIndex_Changed(Object sender, EventArgs e) {
			SessionContext.Shops.PageIndex = pager.PageIndex;
			PopulateShops();
		}

		private void PageIndexButton_Changed(Object sender, EventArgs e) {
			SessionContext.Shops.PageIndex = pager.PageIndex;
			PopulateShops();
		}

		private void PageSize_Changed(Object sender, EventArgs e) {
			SessionContext.Shops.PageSize = pager.PageSize;
			PopulateShops();
		}

		#endregion
	}
}
