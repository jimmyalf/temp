using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Spinit.Wpc.Synologen.Presentation.Code;
using Spinit.Wpc.Utility.Business;
using Spinit.Wpc.Member.Data;
using Spinit.Wpc.Member.Business;
using Spinit.Wpc.Base.Data;
using Globals=Spinit.Wpc.Member.Business.Globals;

namespace Spinit.Wpc.Synologen.Presentation.Components.Synologen {
	public partial class Category : SynologenPage {
		private int _categoryId = -1;

		protected void Page_Load(object sender, EventArgs e) {
			if (Request.Params["id"] != null)
				_categoryId = Convert.ToInt32(Request.Params["id"]);
			if (!Page.IsPostBack) {
				PopulateGroups();
				PopulateShopCategories();
				PopulateCategory();
				if (_categoryId > 0)
					SetupForEdit(_categoryId);
			}
			lblName.Text = "Namn";
			lblGroup.Text = "Välj grupp att koppla till kategorin";
			if (Globals.UseUserConnection)
				dGroups.Visible = true;
		}

		private void PopulateShopCategories() {
			
			drpShopCategory.DataSource = Provider.GetShopCategories(0);
			drpShopCategory.DataBind();
			drpShopCategory.Items.Insert(0,new ListItem("-- Välj butikskategori --","0"));
		}

		private void SetupForEdit(int categoryId) {
			lblHeader.Text = "Redigera Kategori";
			btnSave.Text = "Ändra";
			CategoryRow catRow = Provider.GetCategory(categoryId, LanguageId, DefaultLanguageId);
			txtName.Text = catRow.Name;
			List<int> connectedShopCategories =  Provider.GetShopCategoriesPerMemberCategoryId(categoryId);
			try {
				if (connectedShopCategories.Count>0) {
					drpShopCategory.SelectedValue = connectedShopCategories[0].ToString();
				}
				drpGroups.Items.FindByValue(catRow.GroupId.ToString()).Selected = true;
			}
			catch { }
		}

		private void PopulateCategory() {
			gvCategory.DataSource = Provider.GetCategories(LocationId, LanguageId);//, base.DefaultLanguageId);
			gvCategory.DataBind();
			lblHeader.Text = "Lägg till kategori";
			btnSave.Text = "Spara";
		}

		private void PopulateGroups() {
			Group dbGroup = new Group(Spinit.Wpc.Base.Business.Globals.ConnectionString);

			ArrayList groups = dbGroup.GetGroups(null, GroupTypeRow.TYPE.NONE, 0, 0, 0, 0, 0, -1);
			drpGroups.DataTextField = "Name";
			drpGroups.DataValueField = "Id";
			drpGroups.DataSource = groups;
			drpGroups.DataBind();
		}

		#region Category Events

		/// <summary>
		/// Add delete confirmation
		/// </summary>
		/// <param Name="sender">The sending object.</param>
		/// <param Name="e">The event arguments.</param>

		protected void btnDelete_AddConfirmDelete(object sender, EventArgs e) {
			ClientConfirmation cc = new ClientConfirmation();
			cc.AddConfirmation(ref sender, "Vill du verkligen radera kategorin?");
		}

		protected void gvCategory_RowCreated(object sender, GridViewRowEventArgs e) {

		}

		protected void gvCategory_Editing(object sender, GridViewEditEventArgs e) {
			int index = e.NewEditIndex;
			int catId = (int)gvCategory.DataKeys[index].Value;
			if (!base.IsInRole(MemberRoles.Roles.Edit)) {
				Response.Redirect(ComponentPages.NoAccess);
			}
			else {
				Response.Redirect(ComponentPages.Category + "?id=" + catId, true);
			}
		}

		protected void gvCategory_Deleting(object sender, GridViewDeleteEventArgs e) {
			int index = e.RowIndex;
			int catId = (int)gvCategory.DataKeys[index].Value;
			if (!base.IsInRole(MemberRoles.Roles.Delete)) {
				Response.Redirect(ComponentPages.NoAccess);
			}
			else {
				if(Provider.CategoryHasConnectedMembers(catId)) {
					DisplayMessage("Kategorin kan inte raderas då det finns kopplade medlemmar.", true);
					return;
				}
				
				CategoryRow catRow = Provider.GetCategory(catId, base.LanguageId, base.DefaultLanguageId);
				Provider.DisconnectMemberCategoryFromShopCategories(catRow.Id);
				Provider.AddUpdateDeleteCategory(Enumerations.Action.Delete, ref catRow, base.LanguageId);
				Response.Redirect(ComponentPages.Category);
			}
		}

		protected void gvCategory_RowCommand(object sender, GridViewCommandEventArgs e) {

		}

		protected void btnSave_Click(object sender, EventArgs e) {
			CategoryRow catRow = new CategoryRow();
			Enumerations.Action action = Enumerations.Action.Create;
			if (_categoryId > 0) {
				catRow = Provider.GetCategory(_categoryId, base.LanguageId, base.DefaultLanguageId);
				action = Enumerations.Action.Update;
			}
			catRow.Name = txtName.Text;
			if (Globals.UseUserConnection)
			{
				try
				{
					catRow.GroupId = int.Parse(drpGroups.SelectedValue);
				}
				catch { }
			}

			if (!base.IsInRole(MemberRoles.Roles.Create)) {
				Response.Redirect(ComponentPages.NoAccess);
			}
			else 
			{
				Provider.AddUpdateDeleteCategory(action, ref catRow, base.LanguageId);
				Provider.DisconnectMemberCategoryFromShopCategories(catRow.Id);
				int shopCategory = Int32.Parse(drpShopCategory.SelectedValue);
				if (shopCategory > 0){
					Provider.ConnectMemberCategoryToShopCategory(catRow.Id, shopCategory);
				}
				Response.Redirect(ComponentPages.Category);
			}
		}


		#endregion

	}
}