using System;
using System.Web.UI.WebControls;
using Spinit.Wpc.Member.Business;
using Spinit.Wpc.Synologen.Data.Types;
using Spinit.Wpc.Synologen.Presentation.Code;
using Spinit.Wpc.Utility.Business;

namespace Spinit.Wpc.Synologen.Presentation.Components.Synologen {
	public partial class ShopCategories : SynologenPage {
		private int _shopCategoryId = -1;

		protected void Page_Load(object sender, EventArgs e) {
			if (Request.Params["id"] != null)
				_shopCategoryId = Convert.ToInt32(Request.Params["id"]);
			if (Page.IsPostBack) return;
			PopulateCategories();
			if (_shopCategoryId > 0)
				SetupForEdit();
		}

		private void SetupForEdit() {
			ltHeading.Text = "Redigera kategori";
			btnSave.Text = "Ändra";
			ShopCategoryRow article = Provider.GetShopCategoryRow(_shopCategoryId);
			txtName.Text = article.Name;
		}

		private void PopulateCategories() {
			gvCategories.DataSource = Provider.GetShopCategories(0);
			gvCategories.DataBind();
			ltHeading.Text = "Lägg till kategori";
			btnSave.Text = "Spara";
		}

		#region Category Events

		/// <summary>
		/// Add delete confirmation
		/// </summary>
		/// <param Name="sender">The sending object.</param>
		/// <param Name="e">The event arguments.</param>

		protected void btnDelete_AddConfirmDelete(object sender, EventArgs e) {
			ClientConfirmation cc = new ClientConfirmation();
			cc.AddConfirmation(ref sender, "Vill du verkligen ta bort kategorin?");
		}

		protected void gvCategories_Editing(object sender, GridViewEditEventArgs e) {
			int index = e.NewEditIndex;

			int id = (int)gvCategories.DataKeys[index].Value;
			if (!IsInRole(MemberRoles.Roles.Edit)) {
				Response.Redirect(ComponentPages.NoAccess);
			}
			else {
				Response.Redirect(ComponentPages.ShopCategories + "?id=" + id, true);
			}
		}

		protected void gvCategories_Deleting(object sender, GridViewDeleteEventArgs e) {
			int index = e.RowIndex;
			int id = (int)gvCategories.DataKeys[index].Value;
			if (!IsInRole(MemberRoles.Roles.Delete)) {
				Response.Redirect(ComponentPages.NoAccess);
			}
			else {
				if (Provider.ShopCategoryHasConnectedShops(id)) {
					DisplayMessage("Kategorin kan inte raderas då det finns kopplade butiker.", true);
					return;
				}
				ShopCategoryRow category = new ShopCategoryRow();
				category.Id = id;
				Provider.AddUpdateDeleteShopCategory(Enumerations.Action.Delete, ref category);
				Response.Redirect(ComponentPages.ShopCategories);
			}
		}

		protected void btnSave_Click(object sender, EventArgs e) {
			ShopCategoryRow category = new ShopCategoryRow();
			Enumerations.Action action = Enumerations.Action.Create;
			if (_shopCategoryId > 0) {
				category = Provider.GetShopCategoryRow(_shopCategoryId);
				action = Enumerations.Action.Update;
			}
			category.Name = txtName.Text;

			if (!IsInRole(MemberRoles.Roles.Create)) {
				Response.Redirect(ComponentPages.NoAccess);
			}
			else {
				Provider.AddUpdateDeleteShopCategory(action, ref category);
				Response.Redirect(ComponentPages.ShopCategories);
			}
		}


		#endregion

	}
}
