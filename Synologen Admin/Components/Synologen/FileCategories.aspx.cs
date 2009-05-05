using System;
using System.Web.UI.WebControls;
using Spinit.Wpc.Synologen.Business.Enumeration;
using Spinit.Wpc.Synologen.Presentation.Code;
using Spinit.Wpc.Utility.Business;
using Spinit.Wpc.Member.Data;
using Spinit.Wpc.Member.Business;
using Globals=Spinit.Wpc.Synologen.Business.Globals;

namespace Spinit.Wpc.Synologen.Presentation.Components.Synologen {
	public partial class FileCategories : SynologenPage {
		private int _categoryId = -1;
		private string _type = String.Empty;

		protected void Page_Load(object sender, EventArgs e) {
			if (Request.Params["id"] != null)
				_categoryId = Convert.ToInt32(Request.Params["id"]);
			if (Request.Params["type"] != null)
				_type = Request.Params["type"];
			if (Page.IsPostBack) return;
			PopulateCategory();
			if (_categoryId > 0)
				SetupForEdit(_categoryId);
		}

		private void SetupForEdit(int categoryId) {
			lblHeader.Text = "Redigera kategori";
			btnSave.Text = "Ändra";
			FileCategoryRow fileCatRow;
			if (FileType == FileCategoryType.Synologen) {
				fileCatRow = Provider.GetSynologenFileCategory(categoryId);
			}
			else {
				fileCatRow = Provider.GetFileCategory(categoryId);
			}
			
			txtName.Text = fileCatRow.Name;
		}

		private void PopulateCategory() {
			if (FileType == FileCategoryType.Synologen) {
				gvFileCategory.DataSource = Provider.SynologenGetFileCategories(FileCategoryGetAction.All,0);
			}
			else{
				gvFileCategory.DataSource = Provider.GetFileCategories();
			}
			gvFileCategory.DataBind();
			lblHeader.Text = "Lägg till filkategori";
			btnSave.Text = "Spara";
		}

		/// <summary>
		/// Add delete confirmation
		/// </summary>
		/// <param Name="sender">The sending object.</param>
		/// <param Name="e">The event arguments.</param>

		protected void btnDelete_AddConfirmDelete(object sender, EventArgs e) {
			ClientConfirmation cc = new ClientConfirmation();
			cc.AddConfirmation(ref sender, "Vill du verkligen ta bort kategorin?");
		}

		protected void gvFileCategory_Editing(object sender, GridViewEditEventArgs e) {
			int index = e.NewEditIndex;
			int catId = (int)gvFileCategory.DataKeys[index].Value;
			if (!IsInRole(MemberRoles.Roles.Edit)) {
				Response.Redirect(ComponentPages.NoAccess);
			}
			else {
				Response.Redirect(ComponentPages.FileCategories + "?id=" + catId +"&type="+_type, true);
			}
		}

		protected void gvFileCategory_Deleting(object sender, GridViewDeleteEventArgs e) {
			int index = e.RowIndex;
			int catId = (int)gvFileCategory.DataKeys[index].Value;
			if (!IsInRole(MemberRoles.Roles.Delete)) {
				Response.Redirect(ComponentPages.NoAccess);
			}
			else {
				FileCategoryRow fileCatRow = Provider.GetFileCategory(catId);
				Provider.AddUpdateDeleteFileCategory(Enumerations.Action.Delete, fileCatRow);
				Response.Redirect(ComponentPages.FileCategories + "?type="+_type);
			}
		}

		protected void btnSave_Click(object sender, EventArgs e) {
			FileCategoryRow fileCatRow = new FileCategoryRow();
			Enumerations.Action action = Enumerations.Action.Create;
			if (_categoryId > 0) {
				if (FileType == FileCategoryType.Synologen) {
					fileCatRow = Provider.GetSynologenFileCategory(_categoryId);
				}
				else{
					fileCatRow = Provider.GetFileCategory(_categoryId);
				}
				action = Enumerations.Action.Update;
			}
			fileCatRow.Name = txtName.Text;
			if (!IsInRole(MemberRoles.Roles.Create)) {
				Response.Redirect(ComponentPages.NoAccess);
			}
			else {
				if (FileType == FileCategoryType.Synologen) {
					Provider.AddUpdateDeleteSynologenFileCategory(action, fileCatRow);
				}
				else {
					Provider.AddUpdateDeleteFileCategory(action, fileCatRow);
				}
				
				Response.Redirect(ComponentPages.FileCategories + "?type="+_type);
			}
		}

		private FileCategoryType FileType{
			get {
				if (String.IsNullOrEmpty((_type))) return FileCategoryType.Member;
				if(_type.ToLower().Equals("contractcustomer")) return FileCategoryType.Synologen;
				return FileCategoryType.Member;
			}
		}
		public string CategoryType {
			get {
				return FileType == FileCategoryType.Synologen ? "Avtalskunder" : "Medlem";
			}
		}
	}
}