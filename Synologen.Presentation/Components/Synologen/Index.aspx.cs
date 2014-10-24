using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

using Spinit.Wpc.Member.Data;
using Spinit.Wpc.Member.Business;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Presentation.Code;
using Spinit.Wpc.Utility.Business;
using Spinit.Wpc.Utility.Business.SmartMenu;
using Globals=Spinit.Wpc.Member.Business.Globals;

namespace Spinit.Wpc.Synologen.Presentation.Components.Synologen {
	public partial class Index : SynologenPage {
		private int _pageSize;
		private string _searchString;
		private int _selectedCategory;
		private int _selectedShopId;
		private Shop _selectedShop = new Shop();

		public Shop SelectedShop {
			get { return _selectedShop; }
		}

		//protected void Page_Init(object sender, EventArgs e) {
		//    RenderMemberSubMenu(Page.Master);
		//}

		protected void Page_Load(object sender, EventArgs e) {

			if (Request.Params["shopId"] != null){
				_selectedShopId = Convert.ToInt32(Request.Params["shopId"]);
				_selectedShop = Provider.GetShop(_selectedShopId);
				plFilterByShop.Visible = true;
			}
			plRegularFilter.Visible = !plFilterByShop.Visible;

			if (Page.IsPostBack) return;
			PopulateCategories();
			//Set pagesize
			if (Session["MemberIndexPageSize"] != null) {
				_pageSize = (int)Session["MemberIndexPageSize"];
			}
			else {
				_pageSize = Globals.DefaultPageSize;
			}
			pager.PageSize = _pageSize;
			//set category
			if (Session["MemberIndexPageCategory"] != null) {
				_selectedCategory = (int)Session["MemberIndexPageCategory"];
				drpCategories.SelectedIndex = _selectedCategory;

			}
			else {
				_selectedCategory = 0;
			}
			//Set sorting
			if(Session["MemberIndexSortExpression"] != null)
				SortExpression = (string)Session["MemberIndexSortExpression"];
			else
				SortExpression = "cFirstName";
			if(Session["MemberIndexSortAscending"] != null)
				SortAscending = (bool)Session["MemberIndexSortAscending"];
			else
				SortAscending = true;
               
			//Set pageindex
			if (Session["MemberIndexPageIndex"] != null) {
				pager.PageIndex = (int)Session["MemberIndexPageIndex"];
			}

			PopulateMembers();
		}

		/// <summary>
		/// Renders the submenu.
		/// </summary>
		//public void RenderMemberSubMenu(MasterPage master) {
		//    //var m = (SynologenMain)master;
		//    //var _phSynologenSubMenu = m.SubMenu;
		//    //var subMenu = new SmartMenu.Menu {ID = "SubMenu", ControlType = "ul", ItemControlType = "li", ItemWrapperElement = "span"};

		//    //var itemCollection = new SmartMenu.ItemCollection();
		//    //itemCollection.AddItem("NewMember", null, "Ny medlem", "Skapa ny medlem", null, "btnAdd_OnClick", false, null);
		//    //itemCollection.AddItem("Delete", null, "Radera medlem", "Radera vald medlem", null, "btnDelete_OnClick", false, null);
		//    //itemCollection.AddItem("Filkategori", null, "Filkategorier", "Lista filkategorier", null, "~/Components/Synologen/FileCategories.aspx?type=Member", null, null, false, true);
		//    //itemCollection.AddItem("Medlemskategori", null, "Medlemskategorier", "Lista medlemskategorier", null, "~/Components/Synologen/Category.aspx", null, null, false, true);
		//    //itemCollection.AddItem("Shops", null, "Butiker", "Lista butiker", null, ComponentPages.Shops, null, null, false, true);
			
		//    //subMenu.MenuItems = itemCollection;

		//    //m.SynologenSmartMenu.Render(subMenu, _phSynologenSubMenu);
		//}

		private void PopulateCategories() {
			var catList = Provider.GetAllCategoriesList(LocationId, LanguageId);
			drpCategories.Items.Add(new ListItem("-- Välj Kategori --", "0"));
			foreach (var row in catList) {
				drpCategories.Items.Add(new ListItem(row.Name, row.Id.ToString()));
			}
		}

		public void PopulateMembers() {
			var totalRecords = 0;
			var type = Globals.UseUserConnection ? 0 : 1;


			var dsMembers = Provider.GetSynologenMembersByPage(
				type,
				LocationId,
				LanguageId,
				_selectedCategory,
				_selectedShopId,
				_searchString,
				SortExpression + ((SortAscending) ? " ASC" : " DESC"),
				pager.PageIndex,
				pager.PageSize,
				ref totalRecords);
			gvMembers.DataSource = dsMembers.Tables[SqlProvider._TblMembers];
			gvMembers.DataBind();
			pager.TotalRecords = totalRecords;
			pager.TotalPages = pager.CalculateTotalPages();
			setActive(dsMembers);
		}

		void AddGlyph(GridView grid, TableRow item) {
			var glyph = new Label {EnableTheming = false};
			glyph.Font.Name = "webdings";
			glyph.Font.Size = FontUnit.XSmall;
			glyph.Text = (SortAscending ? " 5" : " 6");

			// Find the column you sorted by
			for (var i = 0; i < grid.Columns.Count; i++) {
				var colExpr = grid.Columns[i].SortExpression;
				if (colExpr != "" && colExpr == SortExpression) {
					item.Cells[i].Controls.Add(glyph);
				}
			}
		}

		protected void gvMember_RowCreated(object sender, GridViewRowEventArgs e) {
			if (e.Row.RowType == DataControlRowType.Header)
				AddGlyph(gvMembers, e.Row);
		}

		private void UpdateColumnHeaders(GridView gv) {
			foreach (DataControlField c in gv.Columns) {
				c.HeaderText = Regex.Replace(c.HeaderText, "\\s<.*>", String.Empty);
				if (c.SortExpression != SortExpression) continue;
				if (SortAscending) {
					c.HeaderText += " <img src=\"img/up.gif\" border=\"0\" width=\"11\" height=\"7\">";
				}
				else {
					c.HeaderText += " <img src=\"img/down.gif\" border=\"0\" width=\"11\" height=\"7\">";
				}
			}
		}

		protected override void OnInit(EventArgs e) {
			pager.IndexChanged += new EventHandler(PageIndex_Changed);
			pager.IndexButtonChanged += new EventHandler(PageIndexButton_Changed);
			pager.PageSizeChanged += new EventHandler(PageSize_Changed);
			base.OnInit(e);
		}

		protected DataView SortDataTable(DataTable dataTable, bool isPageIndexChanging){
			if (dataTable == null) return new DataView();
			var dataView = new DataView(dataTable);
			if (SortExpression != string.Empty){
				dataView.Sort = isPageIndexChanging ? string.Format("{0} {1}", SortExpression, SortAscending ? "ASC" : "DESC") : string.Format("{0} {1}", SortExpression, SortAscending ? "ASC" : "DESC");
			}
			return dataView;

		}

		/// <summary>
		/// Sets the active flag on the users.
		/// </summary>
		/// <param Name="ds">The data-set.</param>

		private void setActive(DataSet ds) {
			var i = 0;
			foreach (GridViewRow row in gvMembers.Rows) {
				var active = Convert.ToBoolean(ds.Tables[0].Rows[i]["cActive"]);
				if (row.FindControl("imgActive") != null) {
					var img = (Image) row.FindControl("imgActive");
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
		


		#region Common Events

		protected void btnSearch_Click(Object sender, EventArgs e) {
			_searchString = txtSearch.Text;
            
			pager.PageIndex = 0;
			Session["MemberIndexPageIndex"] = pager.PageIndex;

			_selectedCategory = Convert.ToInt32(drpCategories.SelectedItem.Value);
			Session["MemberIndexPageCategory"] = _selectedCategory;
			PopulateMembers();
		}

		protected void btnAdd_OnClick(object sender, EventArgs e) {
			Response.Redirect(ComponentPages.EditMember);
		}

		protected void btnDelete_OnClick(object sender, EventArgs e) {
			foreach (GridViewRow row in gvMembers.Rows) {
				var chk = (CheckBox)row.FindControl("chkSelect");
				if ((chk == null) || !chk.Checked) continue;
				var id = (int)gvMembers.DataKeys[row.RowIndex]["cId"];
				if(Provider.MemberHasConnectedOrders(id)) {
					DisplayMessage("Medlemmen kan inte raderas då det finns kopplade ordrar.", true);
					return;
				}
				var memberToDelete = new MemberRow {Id = id};
				Provider.AddUpdateDeleteMember(Enumerations.Action.Delete, LanguageId, ref memberToDelete);
			}

			PopulateMembers();
		}

		protected void chkSelectHeader_CheckedChanged(object sender, EventArgs e) {
			var chkHeader = (CheckBox)sender;
			if (chkHeader == null) return;
			foreach (GridViewRow row in gvMembers.Rows) {
				var chk = (CheckBox)row.FindControl("chkSelect");
				if (chk != null) {
					chk.Checked = chkHeader.Checked;
				}
			}
		}
		#endregion

		#region GridView Events


		protected void gvMembers_PageIndexChanging(object sender, GridViewPageEventArgs e) {
			gvMembers.PageIndex = e.NewPageIndex;
			DataBind();
		}

		protected void gvMembers_Sorting(object sender, GridViewSortEventArgs e) {
			if (e.SortExpression == SortExpression) SortAscending = !SortAscending;
			else SortAscending = true;
			SortExpression = e.SortExpression;

			Session["MemberIndexSortExpression"] = SortExpression;
			Session["MemberIndexSortAscending"] = SortAscending;

			if (Session["MemberIndexPageCategory"] != null) {
				var selectedCategory = (int)Session["MemberIndexPageCategory"];
				if (selectedCategory > 0)
					_selectedCategory = selectedCategory;
			}
            
			PopulateMembers();
		}

		protected void gvMembers_Deleting(object sender, GridViewDeleteEventArgs e) {
			var memberId = (int)gvMembers.DataKeys[e.RowIndex].Value;
			if (Provider.MemberHasConnectedOrders(memberId)) {
				DisplayMessage("Medlemmen kan inte raderas då det finns kopplade ordrar.", true);
				return;
			}
			//DELETE ALL INFO ABOUT A MEMBER
			var memberToDelete = new MemberRow {Id = memberId};
			Provider.AddUpdateDeleteMember(Enumerations.Action.Delete, LanguageId, ref memberToDelete);

			if (Session["MemberIndexPageCategory"] != null) {
				var selectedCategory = (int)Session["MemberIndexPageCategory"];
				if (selectedCategory > 0)
					_selectedCategory = selectedCategory;
			}

			PopulateMembers();

		}

		protected void gvMembers_Editing(object sender, GridViewEditEventArgs e) {
			var index = e.NewEditIndex;
			var memberId = (int)gvMembers.DataKeys[index].Value;
			if (!IsInRole(MemberRoles.Roles.Edit)) {
				Response.Redirect(ComponentPages.NoAccess);
			}
			else {
				Response.Redirect(ComponentPages.EditMember + "?id=" + memberId);
			}
		}


		protected void gvMembers_RowCommand(object sender, GridViewCommandEventArgs e) {
			int index;
			int memberId;

			switch (e.CommandName) {
				case "Edit":
					break;

				case "Files":
					index = Convert.ToInt32(e.CommandArgument);
					memberId = (int)gvMembers.DataKeys[index].Value;
					Response.Redirect(ComponentPages.Files + "?type=Member&id=" + memberId);

					break;
				case "AddFiles":
					index = Convert.ToInt32(e.CommandArgument);
					memberId = (int)gvMembers.DataKeys[index].Value;
					Response.Redirect(ComponentPages.AddFiles + "?type=Member&id=" + memberId);

					break;
			}

		}

		protected void gvMembers_RowDataBound(object sender, GridViewRowEventArgs e) {
			var row = e.Row;
			if (row.RowType != DataControlRowType.DataRow) return;
			// Retrieve the Button control from the fourth and fith column.
			var filesButton = (Button)e.Row.Cells[7].Controls[0];
			var addFileButton = (Button)e.Row.Cells[8].Controls[0];


			// Set the Button's CommandArgument property with the
			// row's index.
			filesButton.CommandArgument = e.Row.RowIndex.ToString();
			addFileButton.CommandArgument = e.Row.RowIndex.ToString();
		}

		protected void gvMembers_RowCreated(object sender, GridViewRowEventArgs e) {

		}

		/// <summary>
		/// Add delete confirmation
		/// </summary>
		/// <param Name="sender">The sending object.</param>
		/// <param Name="e">The event arguments.</param>

		protected void AddConfirmDelete(object sender, EventArgs e) {
			var cc = new ClientConfirmation();
			cc.AddConfirmation(ref sender, "Do you really want to delete the member?");
		}

		#endregion

		#region Pager Events

		private void PageIndex_Changed(Object sender, EventArgs e) {
			Session["MemberIndexPageIndex"] = pager.PageIndex;

			if (Session["MemberIndexPageCategory"] != null) {
				var selectedCategory = (int)Session["MemberIndexPageCategory"];
				if (selectedCategory > 0)
					_selectedCategory = selectedCategory;
			}

			PopulateMembers();
		}

		private void PageIndexButton_Changed(Object sender, EventArgs e) {
			Session["MemberIndexPageIndex"] = pager.PageIndex;

			if (Session["MemberIndexPageCategory"] != null) {
				var selectedCategory = (int)Session["MemberIndexPageCategory"];
				if (selectedCategory > 0)
					_selectedCategory = selectedCategory;
			}

			PopulateMembers();
		}

		private void PageSize_Changed(Object sender, EventArgs e) {
			Session["MemberIndexPageSize"] = pager.PageSize;

			if (Session["MemberIndexPageCategory"] != null) {
				var selectedCategory = (int)Session["MemberIndexPageCategory"];
				if (selectedCategory > 0)
					_selectedCategory = selectedCategory;
			}

			PopulateMembers();
		}

		#endregion

	}
}