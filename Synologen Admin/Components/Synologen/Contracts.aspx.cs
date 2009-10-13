using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Spinit.Wpc.Member.Business;
using Spinit.Wpc.Synologen.Data.Types;
using Spinit.Wpc.Synologen.Presentation.Code;
using Spinit.Wpc.Utility.Business;
using Spinit.Wpc.Utility.Business.SmartMenu;

namespace Spinit.Wpc.Synologen.Presentation.Components.Synologen {
	public partial class Contracts : SynologenPage {
		private string _searchString;
		private int _pageSize;

		protected void Page_Init(object sender, EventArgs e) {
			RenderMemberSubMenu(Page.Master);
		}

		protected void Page_Load(object sender, EventArgs e) {
			if (!Page.IsPostBack) {
				PopulateContractCustomers();
			}
		}

		private void PopulateContractCustomers() {
			int totalRecords = 0;

			//Set pagesize
			_pageSize = SessionContext.ContractCustomers.PageSize;
			pager.PageSize = _pageSize;

			//Set drop-downs
			txtSearch.Text = _searchString;

			//Set sorting
			SortExpression = SessionContext.ContractCustomers.SortExpression;
			SortAscending = SessionContext.ContractCustomers.SortAscending;

			DataSet dsContractCustomers = Provider.GetContractsByPage(
				_searchString,
				SortExpression + ((SortAscending) ? " ASC" : " DESC"),
				pager.PageIndex,
				pager.PageSize,
				ref totalRecords);
			gvContractCustomers.DataSource = dsContractCustomers.Tables[0];
			gvContractCustomers.DataBind();
			pager.TotalRecords = totalRecords;
			pager.TotalPages = pager.CalculateTotalPages();
			//setActive(dsContractCustomers);
			Code.Utility.SetActiveGridViewControl(gvContractCustomers,dsContractCustomers,"cActive","imgActive","Active", "Inactive");

		}

		protected override void OnInit(EventArgs e) {
			pager.IndexChanged += PageIndex_Changed;
			pager.IndexButtonChanged += PageIndexButton_Changed;
			pager.PageSizeChanged += PageSize_Changed;
			base.OnInit(e);
		}

		//private void setActive(DataSet ds) {
		//    int i = 0;
		//    foreach (GridViewRow row in gvContractCustomers.Rows) {
		//        bool active = Convert.ToBoolean(ds.Tables[0].Rows[i]["cActive"]);
		//        if (row.FindControl("imgActive") != null) {
		//            Image img = (Image)row.FindControl("imgActive");
		//            if (active) {
		//                img.ImageUrl = "~/common/icons/True.png";
		//                img.AlternateText = "Active";
		//                img.ToolTip = "Active";
		//            }
		//            else {
		//                img.ImageUrl = "~/common/icons/False.png";
		//                img.AlternateText = "Inactive";
		//                img.ToolTip = "Inactive";
		//            }
		//        }
		//        i++;
		//    }
		//}

		protected void chkSelectHeader_CheckedChanged(object sender, EventArgs e) {
			CheckBox chkHeader = (CheckBox)sender;
			if (chkHeader == null) return;
			foreach (GridViewRow row in gvContractCustomers.Rows) {
				CheckBox chk = (CheckBox)row.FindControl("chkSelect");
				if (chk != null) {
					chk.Checked = chkHeader.Checked;
				}
			}
		}

		protected void AddConfirmDelete(object sender, EventArgs e) {
			ClientConfirmation cc = new ClientConfirmation();
			cc.AddConfirmation(ref sender, "Vill du verkligen ta bort avtalet?");
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
			itemCollection.AddItem("Add", null, "Lägg till", "Lägg till ny avtalskund", null, "btnAdd_OnClick", false, null);
			itemCollection.AddItem("Delete", null, "Radera", "Radera valda avtalskunder", null, "btnDelete_OnClick", false, null);
			itemCollection.AddItem("Filkategori", null, "Filkategorier", "Lista filkategorier", null,ComponentPages.FileCategories + "?type=ContractCustomer", null, null, false, true);
			//itemCollection.AddItem("Artikel", null, "Artiklar", "Lista artiklar", null, ComponentPages.ContractArticles, null, null, false, true);
			//itemCollection.AddItem("Företag", null, "Företag", "Lista företag", null, ComponentPages.ContractCompanies, null, null, false, true);

			subMenu.MenuItems = itemCollection;

			m.SynologenSmartMenu.Render(subMenu, phMemberSubMenu);
		}

		#region Button Events

		protected void btnSearch_Click(object sender, EventArgs e) {
			_searchString = txtSearch.Text;
			pager.PageIndex = 0;
			SessionContext.ContractCustomers.PageIndex = pager.PageIndex;
			SessionContext.ContractCustomers.SearchExpression = _searchString;
			PopulateContractCustomers();
		}

		protected void btnAdd_OnClick(object sender, EventArgs e) {
			Response.Redirect("EditContract.aspx");
		}

		protected void btnDelete_OnClick(object sender, EventArgs e) {
			foreach (GridViewRow row in gvContractCustomers.Rows) {
				CheckBox chk = (CheckBox)row.FindControl("chkSelect");
				if ((chk == null) || !chk.Checked) continue;
				int id = (int)gvContractCustomers.DataKeys[row.RowIndex]["cId"];
				if(Provider.ContractHasConnectedOrders(id)) {
					DisplayMessage("Avtalet kan inte raderas då det finns kopplade ordrar.", true);
					return;
				}
				ContractRow contractToDelete = new ContractRow();
				contractToDelete.Id = id;
				Provider.AddUpdateDeleteContract(Enumerations.Action.Delete, ref contractToDelete);
			}

			PopulateContractCustomers();
		}

		#endregion

		#region Grid Events

		protected void gvContractCustomers_Sorting(object sender, GridViewSortEventArgs e) {
			if (e.SortExpression == SortExpression) SortAscending = !SortAscending;
			else SortAscending = true;

			SortExpression = e.SortExpression;
			SessionContext.ContractCustomers.SortExpression = SortExpression;
			SessionContext.ContractCustomers.SortAscending = SortAscending;

			PopulateContractCustomers();
		}

		protected void gvContractCustomers_PageIndexChanging(object sender, GridViewPageEventArgs e) {
			SessionContext.ContractCustomers.PageIndex = e.NewPageIndex;
			gvContractCustomers.PageIndex = e.NewPageIndex;
			DataBind();
		}

		protected void gvContractCustomers_Editing(object sender, GridViewEditEventArgs e) {
			if (!IsInRole(MemberRoles.Roles.Edit)) {
				Response.Redirect(ComponentPages.NoAccess);
			}
			else {
				int index = e.NewEditIndex;
				int contractCustomerId = (int)gvContractCustomers.DataKeys[index].Value;
				Response.Redirect(ComponentPages.EditContractCustomer + "?id=" + contractCustomerId);
			}
		}

		protected void gvContractCustomers_Deleting(object sender, GridViewDeleteEventArgs e) {
			int id = (int)gvContractCustomers.DataKeys[e.RowIndex].Value;
			if (Provider.ContractHasConnectedOrders(id)) {
				DisplayMessage("Avtalet kan inte raderas då det finns kopplade ordrar.", true);
				return;
			}
			ContractRow contractToDelete = new ContractRow();
			contractToDelete.Id = id;
			Provider.AddUpdateDeleteContract(Enumerations.Action.Delete, ref contractToDelete);
			PopulateContractCustomers();
		}

		protected void gvContractCustomers_RowCommand(object sender, GridViewCommandEventArgs e) {
			int index = 0;
			int contractCustomerId = 0;

			switch (e.CommandName) {
				case "Edit":
					break;

				case "Files":
					index = Convert.ToInt32(e.CommandArgument);
					contractCustomerId = (int)gvContractCustomers.DataKeys[index].Value;
					Response.Redirect(ComponentPages.Files + "?type=ContractCustomer&id=" + contractCustomerId);

					break;
				case "AddFiles":
					index = Convert.ToInt32(e.CommandArgument);
					contractCustomerId = (int)gvContractCustomers.DataKeys[index].Value;
					Response.Redirect(ComponentPages.AddFiles + "?type=ContractCustomer&id=" + contractCustomerId);

					break;
			}
		}

		#endregion

		#region Pager Events

		private void PageIndex_Changed(Object sender, EventArgs e) {
			SessionContext.ContractCustomers.PageIndex = pager.PageIndex;
			PopulateContractCustomers();
		}

		private void PageIndexButton_Changed(Object sender, EventArgs e) {
			SessionContext.ContractCustomers.PageIndex = pager.PageIndex;
			PopulateContractCustomers();
		}

		private void PageSize_Changed(Object sender, EventArgs e) {
			SessionContext.ContractCustomers.PageSize = pager.PageSize;
			PopulateContractCustomers();
		}

		#endregion
	}
}
