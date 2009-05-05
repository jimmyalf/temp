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
	public partial class Orders : SynologenPage {
		private string _searchString;
		private int _pageSize;
		private int _settlementId = -1;

		protected void Page_Init(object sender, EventArgs e) {
			RenderMemberSubMenu(Page.Master);
		}

		protected void Page_Load(object sender, EventArgs e) {

			if (!Page.IsPostBack) {
				if (Request.Params["settlementId"] != null){
					_settlementId = Convert.ToInt32(Request.Params["settlementId"]);
				}
				DisplayFilterMessageToUser();
				PopulateContractCustomers();
				PopulateOrderStatuses();
				PopulateOrders();
			}
		}

		private void DisplayFilterMessageToUser() {
			bool hideNormalFilters = (_settlementId>0);
			plDisplayingFilteredListing.Visible = hideNormalFilters;
			plNormalListing.Visible = !hideNormalFilters;
		}

		private void PopulateContractCustomers() {
			drpContracts.DataValueField = "cId";
			drpContracts.DataTextField = "cName";
			drpContracts.DataSource = Provider.GetContracts(FetchCustomerContract.All, 0, 0);
			drpContracts.DataBind();
			drpContracts.Items.Insert(0,new ListItem("-- Välj Avtalskund --", "0"));
		}

		private void PopulateOrderStatuses() {
			drpStatuses.DataValueField = "cId";
			drpStatuses.DataTextField = "cName";
			drpStatuses.DataSource = Provider.GetOrderStatuses(0);
			drpStatuses.DataBind();
			drpStatuses.Items.Insert(0, new ListItem("-- Välj Fakturastatus --", "0"));
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
			//itemCollection.AddItem("Delete", null, "Radera", "Radera vald order", null, "btnDelete_OnClick", false, null);
			itemCollection.AddItem("Fakturastatus", null, "Fakturastatus", "Lista fakturastatus", null, ComponentPages.OrderStatus, null, null, false, true);

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

		private void PopulateOrders() {
			int totalRecords = 0;

			//Set pagesize
			_pageSize = SessionContext.Orders.PageSize;
			pager.PageSize = _pageSize;

			//Set drop-downs
			txtSearch.Text = _searchString;

			//Set sorting
			SortExpression = SessionContext.Orders.SortExpression;
			SortAscending = SessionContext.Orders.SortAscending;

			//Set pageindex
			//pager.PageIndex = SessionContext.Shops.PageIndex;

			int contractId = Int32.Parse(drpContracts.SelectedValue);
			DateTime startInterval = dtcStartInterval.SelectedDate;
			DateTime stopInterval = dtcEndInterval.SelectedDate;
			int statusId = Int32.Parse(drpStatuses.SelectedValue);

			DataSet orderDataSet = Provider.GetOrdersByPage(
				contractId,
				statusId,
				SettlementId,
				startInterval,
				stopInterval,
				_searchString,
				SortExpression + ((SortAscending) ? " ASC" : " DESC"),
				pager.PageIndex,
				pager.PageSize,
				ref totalRecords);
			gvOrders.DataSource = orderDataSet.Tables[0];
			gvOrders.DataBind();
			pager.TotalRecords = totalRecords;
			pager.TotalPages = pager.CalculateTotalPages();
		}

		#region Common Events

		protected void btnSetFilter_Click(Object sender, EventArgs e) {
			SessionContext.Orders.PageIndex = pager.PageIndex;
			PopulateOrders();
		}

		protected void btnSearch_Click(Object sender, EventArgs e) {
			_searchString = txtSearch.Text;
			pager.PageIndex = 0;
			SessionContext.Orders.PageIndex = pager.PageIndex;
			SessionContext.Orders.SearchExpression = _searchString;
			PopulateOrders();
		}

		protected void btnAdd_OnClick(object sender, EventArgs e) {
			Response.Redirect(ComponentPages.EditOrder);
		}

		protected void btnDelete_OnClick(object sender, EventArgs e) {
			foreach (GridViewRow row in gvOrders.Rows) {
				CheckBox chk = (CheckBox)row.FindControl("chkSelect");
				if ((chk == null) || !chk.Checked) continue;
				int id = (int)gvOrders.DataKeys[row.RowIndex]["cId"];
				//TODO: Check order-status to determine if order can be deleted
				OrderRow orderToDelete = new OrderRow();
				orderToDelete.Id = id;
				Provider.AddUpdateDeleteOrder(Enumerations.Action.Delete, ref orderToDelete);
			}

			PopulateOrders();
		}

		protected void chkSelectHeader_CheckedChanged(object sender, EventArgs e) {
			CheckBox chkHeader = (CheckBox)sender;
			if (chkHeader == null) return;
			foreach (GridViewRow row in gvOrders.Rows) {
				CheckBox chk = (CheckBox)row.FindControl("chkSelect");
				if (chk != null) {
					chk.Checked = chkHeader.Checked;
				}
			}
		}

		#endregion

		#region GridView Events

		protected void gvOrders_PageIndexChanging(object sender, GridViewPageEventArgs e) {
			SessionContext.Orders.PageIndex = e.NewPageIndex;
			gvOrders.PageIndex = e.NewPageIndex;
			DataBind();
		}

		protected void gvOrders_Sorting(object sender, GridViewSortEventArgs e) {
			if (e.SortExpression == SortExpression) SortAscending = !SortAscending;
			else SortAscending = true;

			SortExpression = e.SortExpression;
			SessionContext.Orders.SortExpression = SortExpression;
			SessionContext.Orders.SortAscending = SortAscending;

			PopulateOrders();
		}



		protected void gvOrders_Deleting(object sender, GridViewDeleteEventArgs e) {
			int id = (int)gvOrders.DataKeys[e.RowIndex].Value;
			//TODO: Check order-status to determine if order can be deleted
			OrderRow orderToDelete = new OrderRow();
			orderToDelete.Id = id;
			Provider.AddUpdateDeleteOrder(Enumerations.Action.Delete, ref orderToDelete);

			PopulateOrders();
		}

		protected void gvOrders_Editing(object sender, GridViewEditEventArgs e) {
			if (!IsInRole(MemberRoles.Roles.Edit)) {
				Response.Redirect(ComponentPages.NoAccess);
			}
			else {
				int index = e.NewEditIndex;
				int orderId = (int)gvOrders.DataKeys[index].Value;
				Response.Redirect(ComponentPages.EditOrder + "?id=" + orderId);
			}
		}

		protected void AddConfirmDelete(object sender, EventArgs e) {
			ClientConfirmation cc = new ClientConfirmation();
			cc.AddConfirmation(ref sender, "Vill du verkligen radera ordern?");
		}
		protected void AddConfirmAbort(object sender, EventArgs e) {
			ClientConfirmation cc = new ClientConfirmation();
			cc.AddConfirmation(ref sender, "Vill du verkligen avbryta ordern?");
		}

		#endregion

		#region Pager Events

		private void PageIndex_Changed(Object sender, EventArgs e) {
			SessionContext.Orders.PageIndex = pager.PageIndex;
			PopulateOrders();
		}

		private void PageIndexButton_Changed(Object sender, EventArgs e) {
			SessionContext.Orders.PageIndex = pager.PageIndex;
			PopulateOrders();
		}

		private void PageSize_Changed(Object sender, EventArgs e) {
			SessionContext.Orders.PageSize = pager.PageSize;
			PopulateOrders();
		}

		#endregion

		public int SettlementId {
			get { return _settlementId; }
		}

	}
}
