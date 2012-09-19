using System;
using System.Data;
using System.Web.UI.WebControls;
using Spinit.Wpc.Synologen.Presentation.Intranet.Code;
using TallComponents.Web.PDF;
using Spinit.Wpc.Member.Data;
using Spinit.Wpc.Base.Data;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Supplier 
{
    public partial class MemberFilesThumbs : SynologenCommonSupplierControl 
	{

        protected void Page_Load(object sender, EventArgs e) 
		{
            if (!IsPostBack) PopulateFiles();
        }

        private void PopulateFiles() 
		{
            var tblFiles = new DataTable("tblFiles");

            tblFiles.Columns.Add(new DataColumn("Id", Type.GetType("System.String")));
            tblFiles.Columns.Add(new DataColumn("Name", Type.GetType("System.String")));
            tblFiles.Columns.Add(new DataColumn("Description", Type.GetType("System.String")));
            tblFiles.Columns.Add(new DataColumn("Application", Type.GetType("System.String")));
            tblFiles.Columns.Add(new DataColumn ("Pic", Type.GetType("System.String")));
            tblFiles.Columns.Add(new DataColumn ("Link", Type.GetType("System.String")));

            var memberRow = Provider.GetMember(MemberId, LocationId, LanguageId);
            if (memberRow == null) return;

            if (RequestFileCategoryId.HasValue) 
			{
                var fileCatRow = Provider.GetFileCategory(RequestFileCategoryId.Value);
				if (fileCatRow == null) return;
                AddFilesForCategoryRow(tblFiles, memberRow, fileCatRow);
            }
            else 
			{
                var fileCatRows = Provider.GetAllFileCategoriesList();
				if(fileCatRows == null) return;
            	foreach (var fileCatRow in fileCatRows)
            	{
            		AddFilesForCategoryRow(tblFiles, memberRow, fileCatRow);
            	}
            }

            dlThumbs.DataSource = tblFiles;
            dlThumbs.DataBind();
        }

		protected void AddFilesForCategoryRow(DataTable tblFiles, MemberRow memberRow,FileCategoryRow fileCatRow)
		{
			var di = GetMemberDirectory(LocationRow, memberRow, fileCatRow);
            if (!di.Exists) return;

			var fles = GetMemberFiles(LocationRow, memberRow, fileCatRow);

			foreach (var fle in fles) 
			{
				var urlname = GetFileUrl(LocationRow, memberRow, fileCatRow, fle.Name);
                var fleRow = (FileRow)FileRepository.GetFile(urlname);
				if (fleRow == null) continue;
				var link = Base.Business.Globals.CommonFileUrl + urlname;
				var pic = GetThumbUrl(fleRow, fle, urlname);
				var desc = GetFileDescription(fleRow);
				var row = tblFiles.NewRow();
                row["Id"] = fleRow.Id;
                row["Name"] = fle.Name;
                row["Description"] = desc;
                row["Application"] = fileCatRow.Name;
                row["Pic"] = pic;
                row["Link"] = link;
                tblFiles.Rows.Add(row);
            }			
		}

		private int? RequestFileCategoryId 
		{ 
			get
			{
				var value = Request.Params["categoryId"];
				if(value == null) return null;
				return Convert.ToInt32(Request.Params["categoryId"]);
			}
		}

        protected void dlThumbs_DataBinding(object sender, EventArgs e) 
		{

        }

        protected void dlThumbs_ItemDataBound(object sender, DataListItemEventArgs e) 
		{
        	if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem) return;
        	var thumbLink = (HyperLink)e.Item.FindControl("hlThumb");
        	var thumbPdf = (Thumbnail)e.Item.FindControl("Thumbnail1");
        	var textLink = (HyperLink)e.Item.FindControl("hlText");

        	var description = "";
        	try {
        		description =
        			(string)((DataRowView)e.Item.DataItem).Row.ItemArray[2];
        	}
        	catch { }

        	var pic = (string)((DataRowView)e.Item.DataItem).Row.ItemArray[4];

        	var link = (string)((DataRowView)e.Item.DataItem).Row.ItemArray[5];

        	textLink.Text = description;
        	textLink.NavigateUrl = link;

        	if (pic.Contains("pdfthumbnail") && !pic.Contains("?")) {
        		thumbLink.Visible = false;
        		thumbPdf.Visible = true;
        		thumbPdf.Path = link;
        		thumbPdf.URL = link;
        		thumbPdf.DPI = 10;
        		thumbPdf.Height = 100;
        		thumbPdf.Width = 100;

        	}
        	else {

        		thumbLink.NavigateUrl = link;
        		thumbLink.ImageUrl = pic;
        		thumbLink.ToolTip = description;
        		thumbLink.Visible = true;
        		thumbPdf.Visible = false;
        	}
		}
    }
}
