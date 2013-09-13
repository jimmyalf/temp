using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Spinit.Wpc.Synologen.Presentation.Intranet.Code;
using TallComponents.Web.PDF;
using System.IO;
using Spinit.Wpc.Member.Data;
using Spinit.Wpc.Utility.Business;
using Spinit.Wpc.Base.Data;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Supplier 
{
    public partial class AdminMemberFiles : SynologenCommonSupplierControl 
	{
        protected void Page_Load(object sender, EventArgs e) 
		{
        	if (IsPostBack) return;
        	PopulateFileCategories();
        	PopulateFiles();
		}

        private void PopulateFileCategories() 
		{
            var list = Provider.GetAllFileCategoriesList() ?? new List<FileCategoryRow>();

        	drpFileCategories.DataSource = list;
            drpFileCategories.DataBind();

            drpCategory1.DataSource = list;
            drpCategory1.DataBind();
        }

        private void PopulateFiles() 
		{
            var tblFiles = new DataTable("tblFiles");
            tblFiles.Columns.Add(new DataColumn("Id", typeof(String)));
            tblFiles.Columns.Add(new DataColumn("Name", typeof(String)));
            tblFiles.Columns.Add(new DataColumn("Description", typeof(String)));
            tblFiles.Columns.Add(new DataColumn("Application", typeof(String)));
            tblFiles.Columns.Add(new DataColumn("Pic", typeof(String)));
            tblFiles.Columns.Add(new DataColumn("Link", typeof(String)));

            var memberRow = Provider.GetMember(MemberId, LocationId, LanguageId);
            if (memberRow == null) return;

            if (SelectedCategory > 0) 
			{
                var fileCatRow = Provider.GetFileCategory(SelectedCategory);
                if (fileCatRow == null) return;
				AddCategoryFilesToFileTable(tblFiles, memberRow, fileCatRow);
			}
            else 
			{
                var fileCatRows = Provider.GetAllFileCategoriesList();
                if (fileCatRows == null) return;
                foreach (var fileCatRow in fileCatRows) 
				{
					AddCategoryFilesToFileTable(tblFiles, memberRow, fileCatRow);
                }
            }

            dlFiles.DataSource = tblFiles;
            dlFiles.DataBind();
        }

        #region Events

        protected void btnSetFilter_Click(Object sender, EventArgs e) 
		{

            SelectedCategory = Convert.ToInt32(drpFileCategories.SelectedItem.Value);
            PopulateFiles();
        }

        protected void btnShowAll_Click(Object sender, EventArgs e) 
		{

            SelectedCategory = -1;
            drpFileCategories.SelectedIndex = 0;
            PopulateFiles();
        }

        protected void AddConfirmDelete(object sender, EventArgs e) 
		{
            var cc = new ClientConfirmation();
            cc.AddConfirmation(ref sender, "Vill du verkligen radera filen?");
        }

        protected void btnAdd_Click(object sender, EventArgs e) 
		{
			if(!Page.IsValid) return;
            UploadFile(uplFile1, txtDesc1, drpCategory1.SelectedItem.Text, AllowedExtensions);
            PopulateFiles();
        }

        protected void dlFiles_ItemDataBound(object sender, DataListItemEventArgs e) 
		{
        	if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem) return;
        	var thumbLink = (HyperLink)e.Item.FindControl("hlThumb");
        	var thumbPdf = (Thumbnail)e.Item.FindControl("Thumbnail1");
        	var textLink = (HyperLink)e.Item.FindControl("hlText");
        	var deleteButton = (Button)e.Item.FindControl("btnDelete");
        	var label = (Label)e.Item.FindControl("lblCategory");

        	var description = GetRowValue(e, "Description");
        	var category = GetRowValue(e, "Application");
        	var pic = GetRowValue(e, "Pic");
        	var link = GetRowValue(e, "Link");
        	var id = GetRowValue(e, "Id");

        	deleteButton.CommandArgument = id;

        	textLink.Text = description;
        	textLink.NavigateUrl = link;
        	label.Text = category;

        	if (pic.Contains("pdfthumbnail"))
			{
        		thumbLink.Visible = false;
        		thumbPdf.Visible = true;
        		thumbPdf.Path = link;
        		thumbPdf.URL = link;
        		thumbPdf.DPI = 10;

        	}
        	else 
			{
        		thumbLink.NavigateUrl = link;
        		thumbLink.ImageUrl = pic;
        		thumbLink.ToolTip = description;
        		thumbLink.Visible = true;
        		thumbPdf.Visible = false;
        	}
        }

        protected void dlFiles_Delete(object sender, DataListCommandEventArgs e) 
		{
        	var fileId = int.Parse((string)e.CommandArgument);
            //Delete file
            if (fileId > 0) 
			{
                var fileRepository = new Base.Data.File(Base.Business.Globals.ConnectionString);
                var url = Base.Business.Globals.CommonFileUrl;
                var fileToDelete = (FileRow)fileRepository.GetFile(fileId);

                if (fileToDelete != null) 
				{
                    var fi = new FileInfo(Server.MapPath(url + fileToDelete.Name));
                    if (fi.Exists) fi.Delete();

                    try 
					{
                        fileRepository.DeleteFile(fileId);
                    }
                    catch { }
                }
            }

            //Refresh
            PopulateFiles();
        }

		#endregion

        public int SelectedCategory 
		{
            set
            {
            	ViewState["SelectedCategory"] = value;
            }

            get
            {
            	if (ViewState["SelectedCategory"] != null) return (int)ViewState["SelectedCategory"];
            	return -1;
            }
		}

    	private Location _locationRepository;
    	private Location LocationRepository
    	{
			get { return GetStoredOrNewValue(ref _locationRepository, () => new Location(Base.Business.Globals.ConnectionString)); }
    	}

    	private Base.Data.File _fileRepository;
    	private Base.Data.File FileRepository
    	{
			get { return GetStoredOrNewValue(ref _fileRepository, () => new Base.Data.File(Base.Business.Globals.ConnectionString)); }
    	}

		protected virtual void UploadFile(FileUpload uplFile, ITextControl txtDesc, string categoryName, IEnumerable<string> allowedExtensionsArr) 
		{
			if (!uplFile.HasFile) return;
			var fileName = UrlFriendlyRenamingService.Rename(uplFile.FileName);
            var lrow = (LocationRow)LocationRepository.GetLocation(LocationId);
            var memberRow = Provider.GetMember(MemberId, LocationId, LanguageId);
			if (memberRow == null) return;

        	var fileExtension = Path.GetExtension(fileName).ToLower();
        	fileExtension = fileExtension.Substring(1, fileExtension.Length - 1);

        	var fileOK = allowedExtensionsArr.Any(t => fileExtension.Equals(t));
        	if (!fileOK) return;
			
        	var di = GetMemberDirectory(lrow, memberRow, categoryName);
    		if (!di.Exists) di.Create();

    		uplFile.PostedFile.SaveAs(di.FullName + "\\" + fileName);

    		var description = (String.IsNullOrEmpty(txtDesc.Text)) ? null : txtDesc.Text;
			var fileUrl = GetFileUrl(lrow, memberRow, categoryName, fileName);
    		FileRepository.AddFile(fileUrl, false, fileExtension, null, description, CurrentUser);
        }
		
		protected virtual void AddCategoryFilesToFileTable(DataTable tblFiles, MemberRow memberRow, FileCategoryRow fileCatRow)
		{
			var directory = GetMemberDirectory(LocationRow, memberRow, fileCatRow);
            if (!directory.Exists) return;
			var filesOnDisk = GetMemberFiles(LocationRow, memberRow, fileCatRow);

			foreach (var fileInfo in filesOnDisk)
			{
				var row = tblFiles.NewRow();
				var urlname = GetFileUrl(LocationRow, memberRow, fileCatRow, fileInfo.Name);
				var fleRow = (FileRow) FileRepository.GetFile(urlname);

				var pic = GetThumbUrl(fleRow, fileInfo, urlname);
				var desc = GetFileDescription(fleRow);

				if (fleRow == null || pic == null) continue;

				row["Id"] = fleRow.Id;
				row["Name"] = fileInfo.Name;
				row["Description"] = desc;
				row["Application"] = fileCatRow.Name;
				row["Pic"] = pic;
				row["Link"] = Base.Business.Globals.CommonFileUrl + urlname;

				tblFiles.Rows.Add(row);
			}
		}
				
		private static string GetRowValue(DataListItemEventArgs e, string columnName)
		{
			if(e.Item.DataItem is DBNull) return null;
			return ((DataRowView)e.Item.DataItem).Row[columnName] as string;
		}
		private static TType GetStoredOrNewValue<TType>(ref TType storedValue, Func<TType> getValue)
			where TType : class
		{
			return storedValue ?? (storedValue = getValue());
		}
	}
}
