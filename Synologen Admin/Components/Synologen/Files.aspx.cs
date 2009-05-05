using System;
using System.Data;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Spinit.Wpc.Member.Data;
using Spinit.Wpc.Member.Business;
using Spinit.Wpc.Synologen.Business.Enumeration;
using Spinit.Wpc.Synologen.Presentation.Code;
using Spinit.Wpc.Utility.Business;
using Spinit.Wpc.Utility.Business.SmartMenu;
using Spinit.Wpc.Base.Data;
using Spinit.Wpc.Utility.Core;
using File=Spinit.Wpc.Base.Data.File;

namespace Spinit.Wpc.Synologen.Presentation.Components.Synologen {
	public partial class Files : SynologenPage {
		private int _selectedCategory;
		private int _entityId = -1;
		private string _type = String.Empty;

		protected void Page_Init(object sender, EventArgs e) {
			RenderMemberSubMenu(Page.Master);
		}

		protected void Page_Load(object sender, EventArgs e) {

			if (Request.Params["id"] != null)
				_entityId = Convert.ToInt32(Request.Params["id"]);

			if (Request.Params["type"] != null)
				_type = Request.Params["type"];

			if (IsPostBack) return;

			SortExpression = "Name";
			SortAscending = false;
			PopulateFileCategories();
			PopulateFiles();
		}


		private void PopulateFileCategories() {
			List<FileCategoryRow> list;
			if (FileType == FileCategoryType.Synologen) {
				list = Provider.GetAllSynologenFileCategoriesList();
			}
			else{
				 list = Provider.GetAllFileCategoriesList();
			}
			if (list == null) list = new List<FileCategoryRow>();

			drpFileCategories.DataSource = list;
			drpFileCategories.DataTextField = "Name";
			drpFileCategories.DataValueField = "Id";
			drpFileCategories.DataBind();
		}

		private void PopulateFiles() {
			DataTable tblFiles = new DataTable("tblFiles");
			tblFiles.Columns.Add(new DataColumn("Id",Type.GetType("System.String")));
			tblFiles.Columns.Add(new DataColumn("Name",Type.GetType("System.String")));
			tblFiles.Columns.Add(new DataColumn("Description",Type.GetType("System.String")));
			tblFiles.Columns.Add(new DataColumn("Application",Type.GetType("System.String")));
			tblFiles.Columns.Add(new DataColumn("Pic",Type.GetType("System.String")));
			tblFiles.Columns.Add(new DataColumn("Link",Type.GetType("System.String")));

			File file = new File(Base.Business.Globals.ConnectionString);
			string identifier = GetIdentifier();

			if (_selectedCategory > 0) {
				FileCategoryRow fileCatRow = GetFileCategoryRow();
				if (fileCatRow == null) return;
				string folder = GetFolderPath(fileCatRow, identifier);
				string[] files = GetFilesInFolder(folder);
				foreach (string fileItem in files) {
					TryAddFileToTable(tblFiles, identifier, fileCatRow, fileItem, file);
				}
			}
			else {
				List<FileCategoryRow> fileCatRows = GetFileCategories();
				foreach (FileCategoryRow fileCatRow in fileCatRows) {
					string folder = GetFolderPath(fileCatRow, identifier);
					string[] files = GetFilesInFolder(folder);

					foreach (string fileItem in files) {
						TryAddFileToTable(tblFiles, identifier, fileCatRow, fileItem, file);
					}
				}
			}

			DataRow[] rows = tblFiles.Select("", SortExpression + ((SortAscending) ? " ASC" : " DESC"));

			DataTable tblSorted = new DataTable();
			tblSorted = tblFiles.Clone();
			foreach (DataRow row in rows) {
				DataRow newRow = tblSorted.NewRow();
				newRow.ItemArray = row.ItemArray;
				tblSorted.Rows.Add(newRow);
			}

			gvFiles.DataSource = tblSorted;
			gvFiles.DataBind();
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
			itemCollection.AddItem("New", null, "New file", "Create new files", null, "btnAdd_OnClick", false, null);
			itemCollection.AddItem("Delete", null, "Delete files", "Delete selected files", null, "btnDelete_OnClick", false, null);

			subMenu.MenuItems = itemCollection;

			m.SynologenSmartMenu.Render(subMenu, phMemberSubMenu);
		}

		void AddGlyph(GridView grid, TableRow item) {
			Label glyph = new Label();
			glyph.EnableTheming = false;
			glyph.Font.Name = "webdings";
			glyph.Font.Size = FontUnit.XSmall;
			glyph.Text = (SortAscending ? " 5" : " 6");

			// Find the column you sorted by
			for (int i = 0; i < grid.Columns.Count; i++) {
				string colExpr = grid.Columns[i].SortExpression;
				if (colExpr != "" && colExpr == SortExpression) {
					item.Cells[i].Controls.Add(glyph);
				}
			}
		}


		#region Common Events

		protected void btnSetFilter_Click(Object sender, EventArgs e) {
			//pager.PageIndex = 0;
			_selectedCategory = Convert.ToInt32(drpFileCategories.SelectedItem.Value);
			PopulateFiles();
		}

		protected void btnShowAll_Click(Object sender, EventArgs e) {
			//pager.PageIndex = 0;
			_selectedCategory = 0;
			drpFileCategories.SelectedIndex = 0;
			PopulateFiles();
		}



		protected void btnAdd_OnClick(object sender, EventArgs e) {
			Response.Redirect("AddFiles.aspx?type="+_type+"&id=" + _entityId);
		}

		protected void btnDelete_OnClick(object sender, EventArgs e) {
			foreach (GridViewRow row in gvFiles.Rows) {
				CheckBox chk = (CheckBox)row.FindControl("chkSelect");
				if ((chk == null) || !chk.Checked) continue;
				int fileId = 0;

				try {
					fileId = int.Parse((string)gvFiles.DataKeys[row.RowIndex]["Id"]);
				}
				catch { }

				//Delete file

				if (fileId <= 0) continue;
				File file = new File(Base.Business.Globals.ConnectionString);
				//string pth = Base.Business.Globals.CommonFilePath;
				string url = Base.Business.Globals.CommonFileUrl;
				FileRow fileToDelete = (FileRow)file.GetFile(fileId);
				if (fileToDelete == null) continue;
				FileInfo fi = new FileInfo(Server.MapPath(url + fileToDelete.Name));
				if (fi.Exists) fi.Delete();
				try {
					file.DeleteFile(fileId);
				}
				catch { }
			}
			PopulateFiles();
		}

		protected void chkSelectHeader_CheckedChanged(object sender, EventArgs e) {
			CheckBox chkHeader = (CheckBox)sender;
			if (chkHeader == null) return;
			foreach (GridViewRow row in gvFiles.Rows) {
				CheckBox chk = (CheckBox)row.FindControl("chkSelect");
				if (chk != null) {
					chk.Checked = chkHeader.Checked;
				}
			}
		}
		#endregion

		#region GridView Events

		protected void gvFiles_RowCreated(object sender, GridViewRowEventArgs e) {
			if (e.Row.RowType == DataControlRowType.Header)
				AddGlyph(gvFiles, e.Row);
		}

		protected void gvFiles_PageIndexChanging(object sender, GridViewPageEventArgs e) {

			gvFiles.PageIndex = e.NewPageIndex;
			DataBind();
		}

		protected void gvFiles_Sorting(object sender, GridViewSortEventArgs e) {
			if (e.SortExpression == SortExpression)
				SortAscending = !SortAscending;
			else
				SortAscending = true;
			SortExpression = e.SortExpression;
			PopulateFiles();
		}

		protected void gvFiles_Deleting(object sender, GridViewDeleteEventArgs e) {
			int fileId = 0;
			try {
				fileId = int.Parse((string)gvFiles.DataKeys[e.RowIndex].Value);
			}
			catch { }

			if (fileId > 0) {
				File file = new File(Base.Business.Globals.ConnectionString);

				//string pth = Base.Business.Globals.CommonFilePath;
				string url = Base.Business.Globals.CommonFileUrl;
				FileRow fileToDelete = (FileRow)file.GetFile(fileId);

				if (fileToDelete != null) {
					FileInfo fi = new FileInfo(Server.MapPath(url + fileToDelete.Name));
					if (fi.Exists)fi.Delete();
					try {
						file.DeleteFile(fileId);
					}
					catch { }
				}
			}
			PopulateFiles();

		}

		protected void gvFiles_Editing(object sender, GridViewEditEventArgs e) {

		}


		protected void gvFiles_RowCommand(object sender, GridViewCommandEventArgs e) {
			switch (e.CommandName) {
				case "Edit":
					break;
			}

		}

		protected void gvFiles_RowDataBound(object sender, GridViewRowEventArgs e) {


		}


		/// <summary>
		/// Add delete confirmation
		/// </summary>
		/// <param Name="sender">The sending object.</param>
		/// <param Name="e">The event arguments.</param>

		protected void AddConfirmDelete(object sender, EventArgs e) {
			ClientConfirmation cc = new ClientConfirmation();
			cc.AddConfirmation(ref sender, "Do you really want to delete the file?");
		}

		#endregion

		#region Helper Methods

		private FileCategoryType FileType {
			get {
				if (String.IsNullOrEmpty((_type))) return FileCategoryType.Member;
				if (_type.ToLower().Equals("contractcustomer")) return FileCategoryType.Synologen;
				return FileCategoryType.Member;
			}
		}
		private void TryAddFileToTable(DataTable tblFiles, string identifier, FileCategoryRow fileCatRow, string fileItem, IBaseFile file) {
			//FileInfo inf = new FileInfo(fileItem);
			string[] nmes = fileItem.Split('\\');
			string nme = nmes[nmes.Length - 1];
			string contentInfo = null;
			int index = nme.LastIndexOf(".");
			if (index != -1) {
				contentInfo = nme.Substring(index + 1, nme.Length - index - 1);
			}
			string urlname = GetFileUrl(fileCatRow, identifier, nme);
			//string urlname = lrow.Name + "/Member/" + identifier + "/" + fileCatRow.Name + "/" + nme;



			string link = "";
			string pic = "";
			bool fileOk = false;

			string url = Base.Business.Globals.CommonFileUrl;

			if (Base.Business.Globals.ImageType.IndexOf(contentInfo) != -1) {
				fileOk = true;
				link = url + urlname;
				pic = "/wpc/Member/img/photo_icon.gif";
			}

			if (Base.Business.Globals.DocumentType.IndexOf(contentInfo) != -1) {
				fileOk = true;
				link = url + urlname;
				pic = "/wpc/Member/img/photo_icon.gif";
			}

			if (Base.Business.Globals.MediaType.IndexOf(contentInfo) != -1) {
				fileOk = true;
				link = url + urlname;
				pic = "/wpc/Member/img/video_icon.gif";
			}


			if (Base.Business.Globals.FlashType.IndexOf(contentInfo) != -1) {
				fileOk = true;
				link = url + urlname;
				pic = "/wpc/Member/img/video_icon.gif";
			}

			FileRow fileRow = (FileRow)file.GetFile(urlname);
			if (!fileOk || fileRow == null) return;

			string desc = fileRow.Description;
			if ((desc != null) && (desc.Length > 30)) {
				desc = desc.Substring(0, 27);

				if (desc.LastIndexOf(" ") != -1) {
					desc = desc.Substring(0, desc.LastIndexOf(" "));
				}

				desc += "...";
			}

			DataRow row = tblFiles.NewRow();
			row["Id"] = fileRow.Id;
			row["Name"] = nme;
			row["Description"] = desc;
			row["Application"] = fileCatRow.Name;
			row["Pic"] = pic;
			row["Link"] = link;

			tblFiles.Rows.Add(row);
		}
		private static string[] GetFilesInFolder(string folder) {
			DirectoryInfo di = new DirectoryInfo(folder);
			if (!di.Exists) return new string[0];
			return Directory.GetFiles(folder, "*", SearchOption.TopDirectoryOnly);
		}
		private string GetFolderPath(FileCategoryRow categoryRow, string identifier) {
			//pth + lrow.Name + "\\Member\\" + memberRow.OrgName+ "\\" + fileCatRow.Name
			string path = Base.Business.Globals.CommonFilePath;
			path += LocationName;
			if (FileType == FileCategoryType.Synologen){
				path += @"\SynologenContract";
			}
			else {
				path += @"\Member";
			}
			path += @"\" + identifier;
			path += @"\" + categoryRow.Name;
			return path;
		}
		private string GetFileUrl(FileCategoryRow categoryRow,string identifier, string fileName) {
			string path =  LocationName;
			if (FileType == FileCategoryType.Synologen){
				path += @"/SynologenContract";
			}
			else {
				path += @"/Member";
			}
			path += @"/" + identifier;
			path += @"/" + categoryRow.Name;
			path += @"/" + fileName;
			return path;
		}
		private string GetIdentifier() {
			if (FileType == FileCategoryType.Synologen) {
				return _entityId.ToString();
			}
			//MemberRow memberRow = Provider.GetMember(_entityId, LocationId, LanguageId);
			MemberRow memberRow = Provider.GetSynologenMember(_entityId, LocationId, LanguageId);
			return memberRow.OrgName;
		}
		private FileCategoryRow GetFileCategoryRow() {
			if (FileType == FileCategoryType.Synologen) {
				return Provider.GetSynologenFileCategory(_selectedCategory);
			}
			return Provider.GetFileCategory(_selectedCategory);
		}
		private List<FileCategoryRow> GetFileCategories() {
			if (FileType == FileCategoryType.Synologen) {
				return Provider.GetAllSynologenFileCategoriesList();
			}
			return Provider.GetAllFileCategoriesList();
		}

		#endregion
	}
}