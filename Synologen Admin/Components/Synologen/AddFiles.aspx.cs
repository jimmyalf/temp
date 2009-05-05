using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Spinit.Wpc.Member.Data;
using Spinit.Wpc.Member.Business;
using Spinit.Wpc.Synologen.Business.Enumeration;
using Spinit.Wpc.Synologen.Presentation.Code;
using Spinit.Wpc.Utility.Business;

namespace Spinit.Wpc.Synologen.Presentation.Components.Synologen {
	public partial class AddFiles : SynologenPage {
		private int _entityId = -1;
		private string _type = String.Empty;

		protected void Page_Load(object sender, EventArgs e) {
			if (Request.Params["id"] != null)
				_entityId = Convert.ToInt32(Request.Params["id"]);
			if (Request.Params["type"] != null)
				_type = Request.Params["type"];

			if (!IsPostBack)
				PopulateForm();

		}

		private void PopulateForm() {
			//lblDesc1.Text = "Description 1";
			//lblDesc2.Text = "Description 2";
			//lblDesc3.Text = "Description 3";
			//lblDesc4.Text = "Description 4";
			//lblDesc5.Text = "Description 5";
			//lblFile1.Text = "File 1";
			//lblFile2.Text = "File 2";
			//lblFile3.Text = "File 3";
			//lblFile4.Text = "File 4";
			//lblFile5.Text = "File 5";
			//lblCategory1.Text = "Category 1";
			//lblCategory2.Text = "Category 2";
			//lblCategory3.Text = "Category 3";
			//lblCategory4.Text = "Category 4";
			//lblCategory5.Text = "Category 5";


			PopulateCategories();
		}

		private void PopulateCategories() {
			List<FileCategoryRow> list;
			if(FileType == FileCategoryType.Synologen) {
				list = Provider.GetAllSynologenFileCategoriesList();
			}
			else{
				list = Provider.GetAllFileCategoriesList();
			}
			drpCategory1.DataSource = new List<FileCategoryRow>(list);
			drpCategory1.DataTextField = "Name";
			drpCategory1.DataValueField = "Id";
			drpCategory1.DataBind();
			drpCategory2.DataSource = new List<FileCategoryRow>(list);
			drpCategory2.DataTextField = "Name";
			drpCategory2.DataValueField = "Id";
			drpCategory2.DataBind();
			drpCategory3.DataSource = new List<FileCategoryRow>(list);
			drpCategory3.DataTextField = "Name";
			drpCategory3.DataValueField = "Id";
			drpCategory3.DataBind();
			drpCategory4.DataSource = new List<FileCategoryRow>(list);
			drpCategory4.DataTextField = "Name";
			drpCategory4.DataValueField = "Id";
			drpCategory4.DataBind();
			drpCategory5.DataSource = new List<FileCategoryRow>(list);
			drpCategory5.DataTextField = "Name";
			drpCategory5.DataValueField = "Id";
			drpCategory5.DataBind();

		}

		protected void btnSave_Click(object sender, EventArgs e) {
			string allowedExtensions = Base.Business.Globals.ImageType;
			allowedExtensions = allowedExtensions + "," + Base.Business.Globals.MediaType;
			allowedExtensions = allowedExtensions + "," + Base.Business.Globals.FlashType;
			allowedExtensions = allowedExtensions + "," + Base.Business.Globals.DocumentType;

			String[] allowedExtensionsArr = allowedExtensions.Split(new char[] { ',' });

			//MemberRow memberRow = Provider.GetMember(_entityId, LocationId, LanguageId);

			//if (memberRow == null) return;
			string path = GetFilePath();
			string url = GetFileUrl();

			UploadFile(uplFile1, txtDesc1, path, url, drpCategory1.SelectedItem.Text, allowedExtensionsArr);
			UploadFile(uplFile2, txtDesc2, path, url, drpCategory2.SelectedItem.Text, allowedExtensionsArr);
			UploadFile(uplFile3, txtDesc3, path, url, drpCategory3.SelectedItem.Text, allowedExtensionsArr);
			UploadFile(uplFile4, txtDesc4, path, url, drpCategory4.SelectedItem.Text, allowedExtensionsArr);
			UploadFile(uplFile5, txtDesc5, path, url, drpCategory5.SelectedItem.Text, allowedExtensionsArr);
			if (FileType == FileCategoryType.Synologen) {
				Response.Redirect("Contracts.aspx", true);
			}
			else{
				Response.Redirect("Index.aspx", true);
			}
		}

		private void UploadFile(FileUpload uplFile,ITextControl txtDesc,string path,string url,string categoryName,String[] allowedExtensionsArr) {
			if (!uplFile.HasFile) return;
			bool fileOK = false;

			String fileExtension = Path.GetExtension(uplFile.FileName).ToLower();
			fileExtension= fileExtension.Substring(1,fileExtension.Length - 1);

			for (int i = 0; i < allowedExtensionsArr.Length; i++) {
				if (!fileExtension.Equals(allowedExtensionsArr[i])) continue;
				fileOK = true;
				break;
			}
			if (!fileOK) {
				//Label1.Text = "Cannot accept files of this type.";
			}
			else {
				try {
					string name = url + "/" + categoryName + "/" + uplFile.FileName;

					DirectoryInfo di = new DirectoryInfo(path + categoryName);
					if (!di.Exists)
						di.Create();

					uplFile.PostedFile.SaveAs(path + categoryName + "\\" + uplFile.FileName);

					Base.Data.File fle = new Base.Data.File(Base.Business.Globals.ConnectionString);

					string description = txtDesc.Text;
					if (description.Length == 0) {
						description = null;
					}

					fle.AddFile(name,false,fileExtension,null,description,CurrentUser);
					//Label1.Text = "File uploaded!";
				}
				catch (Exception ex) {
					//Label1.Text = "File could not be uploaded.";
				}
			}
		}


		private string  GetFilePath() {
			if (FileType == FileCategoryType.Synologen) {
				return Base.Business.Globals.CommonFilePath + "\\" + LocationName + "\\SynologenContract\\" + _entityId + "\\";
			}
			//MemberRow memberRow = Provider.GetMember(_entityId, LocationId, LanguageId);
			MemberRow memberRow = Provider.GetSynologenMember(_entityId, LocationId, LanguageId);
			if (memberRow == null) return String.Empty;
			return Base.Business.Globals.CommonFilePath + "\\" + LocationName + "\\Member\\" + memberRow.OrgName + "\\";
		}

		private string GetFileUrl() {
			if (FileType == FileCategoryType.Synologen) {
				return LocationName + "/SynologenContract/" + _entityId;	
			}
			//MemberRow memberRow = Provider.GetMember(_entityId, LocationId, LanguageId);
			MemberRow memberRow = Provider.GetSynologenMember(_entityId, LocationId, LanguageId);
			if (memberRow == null) return String.Empty;
			return LocationName + "/Member/" + memberRow.OrgName;
		}

		#region Properties

		private FileCategoryType FileType {
			get {
				if (String.IsNullOrEmpty((_type))) return FileCategoryType.Member;
				if (_type.ToLower().Equals("contractcustomer")) return FileCategoryType.Synologen;
				return FileCategoryType.Member;
			}
		}

		public string Legend {
			get {
				if (FileType == FileCategoryType.Synologen) {
					return "Lägg till avtalsfiler";
				}
				return "Lägg till medlemsfiler";

			}
		}

		public string Heading {
			get {
				if (FileType == FileCategoryType.Synologen) {
					return "Medlem";
				}
				return "Avtalskund";

			}
		}

		#endregion
	}
}