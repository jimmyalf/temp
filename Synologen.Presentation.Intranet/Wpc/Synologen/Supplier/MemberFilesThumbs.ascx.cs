using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI.WebControls;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Intranet.Code;
using TallComponents.Web.PDF;
using System.IO;
using Spinit.Wpc.Member.Data;
using Spinit.Wpc.Base.Data;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Supplier 
{
    public partial class MemberFilesThumbs : SynologenUserControl 
	{
    	private UrlFriendlyRenamingService _urlFriendlyRenamingService;

    	public MemberFilesThumbs()
    	{
    		_urlFriendlyRenamingService = new UrlFriendlyRenamingService();
    	}
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

            var location = new Location(Base.Business.Globals.ConnectionString);
            var lrow = (LocationRow)location.GetLocation(LocationId);
            var file = new Base.Data.File(Base.Business.Globals.ConnectionString);
            //var pth = Base.Business.Globals.CommonFilePath;
            var url = Base.Business.Globals.CommonFileUrl;

            var memberRow = Provider.GetMember(MemberId, LocationId, LanguageId);
            if (memberRow == null) return;

            if (RequestFileCategoryId.HasValue) 
			{
                var fileCatRow = Provider.GetFileCategory(RequestFileCategoryId.Value);
                lblFileCategory.Text = fileCatRow.Name;

				var di = GetMemberDirectory(lrow,memberRow,fileCatRow);
                if (!di.Exists) return;

                var fles = GetMemberFiles(lrow,memberRow,fileCatRow);

                foreach (var fle in fles) 
				{
                    var fileOk = false;
                    var row = tblFiles.NewRow();
                    var inf = new FileInfo(fle);

                    var nmes = fle.Split('\\');
                    var nme = nmes[nmes.Length - 1];
                    string contentInfo = null;
                    var inx = nme.LastIndexOf(".");
                    if (inx != -1) 
					{
                        contentInfo = nme.Substring(inx + 1, nme.Length - inx - 1);
                    }

					var orgName = _urlFriendlyRenamingService.Rename(memberRow.OrgName);
                    var urlname = lrow.Name + "/Member/" + orgName + "/" + fileCatRow.Name + "/" + nme;
					var escapedName = EscapeSqlLikeWildcards(urlname);
                    var fleRow = (FileRow)file.GetFile(escapedName);

                    var link = "";
                    var pic = "";

                    if (Base.Business.Globals.ImageType.IndexOf(contentInfo) != -1) 
					{
                        fileOk = true;
                        link = url + urlname;
                        const int thumbWidth = 100;
                        const int thumbHeight = 100;
                        try {
                            pic = "/wpc/Synologen/Supplier/ViewMemberImage.aspx?filename="
                                                + HttpUtility.UrlEncode(urlname) + "&width=" + thumbWidth
                                                + "&height=" + thumbHeight + "&ext=" + fleRow.ContentInfo.ToLower();
                        }
                        catch { }
                    }
                    if (Base.Business.Globals.DocumentType.IndexOf(contentInfo) != -1) 
					{
                        fileOk = true;
                        link = url + urlname;
						pic = GetImageSrcForDocument(inf);
					}

                    if (Base.Business.Globals.MediaType.IndexOf(contentInfo) != -1) {
                        fileOk = true;
                        link = url + urlname;
                        pic = "/wpc/Synologen/Supplier/images/video_icon.gif";
                    }


                    if (fleRow == null || !fileOk) continue;

                    var desc = fleRow.Description;

                    row["Id"] = fleRow.Id;
                    row["Name"] = nme;
                    row["Description"] = desc;
                    row["Application"] = fileCatRow.Name;
                    row["Pic"] = pic;
                    row["Link"] = link;

                    tblFiles.Rows.Add(row);
                }
            }
            else {
                var fileCatRows = new ArrayList(Provider.GetAllFileCategoriesList());

            	foreach (FileCategoryRow fileCatRow in fileCatRows) 
				{
					var di = GetMemberDirectory(lrow, memberRow, fileCatRow);
                    if (!di.Exists) continue;

					var fles = GetMemberFiles(lrow, memberRow, fileCatRow);

                    foreach (var fle in fles) 
					{
                        var row = tblFiles.NewRow();
                        var inf = new FileInfo(fle);
                        var nmes = fle.Split('\\');
                        var nme = nmes[nmes.Length - 1];
                        string contentInfo = null;
                        var inx = nme.LastIndexOf(".");
                        if (inx != -1) 
						{
                            contentInfo = nme.Substring(inx + 1, nme.Length - inx - 1);
                        }

						var orgName = _urlFriendlyRenamingService.Rename(memberRow.OrgName);
                        var urlname = lrow.Name + "/Member/" + orgName + "/" + fileCatRow.Name + "/" + nme;

						var escapedName = EscapeSqlLikeWildcards(urlname);
                        var fleRow = (FileRow)file.GetFile(escapedName);

                        var fileOk = false;
                        var link = "";
                        var pic = "";

                        if (Base.Business.Globals.ImageType.IndexOf(contentInfo) != -1) {
                            fileOk = true;
                            link = url + urlname;
                            pic = "/wpc/Synologen/Supplier/images/photo_icon.gif";

                            var thumbWidth = 100;
                            var thumbHeight = 100;
                            try 
							{
                                pic = "/wpc/Synologen/Supplier/ViewMemberImage.aspx?filename="
                                                    + HttpUtility.UrlEncode(urlname) + "&width=" + thumbWidth
                                                    + "&height=" + thumbHeight + "&ext=" + fleRow.ContentInfo.ToLower();
                            }
                            catch { }
                        }

                        if (Base.Business.Globals.DocumentType.IndexOf(contentInfo) != -1) { 
							fileOk = true;
                            link = url + urlname;
                        	pic = GetImageSrcForDocument(inf);
                        }

                        if (Base.Business.Globals.MediaType.IndexOf(contentInfo) != -1) {
                            fileOk = true;
                            link = url + urlname;
                            pic = "/wpc/Synologen/Supplier/images/video_icon.gif";
                        }

                        if (fleRow == null || !fileOk)
                            continue;

                        var desc = fleRow.Description;

                        if ((desc != null) && (desc.Length > 30)) 
						{
                            desc = desc.Substring(0, 27);

                            if (desc.LastIndexOf(" ") != -1)
							{
                                desc = desc.Substring(0, desc.LastIndexOf(" "));
                            }

                            desc += "...";
                        }
                        row["Id"] = fleRow.Id;
                        row["Name"] = nme;
                        row["Description"] = desc;
                        row["Application"] = fileCatRow.Name;
                        row["Pic"] = pic;
                        row["Link"] = link;

                        tblFiles.Rows.Add(row);
                    }
                }
            }

            dlThumbs.DataSource = tblFiles;
            dlThumbs.DataBind();
        }


		protected virtual DirectoryInfo GetMemberDirectory(LocationRow lrow, MemberRow memberRow, FileCategoryRow fileCatRow)
		{
            var pth = Base.Business.Globals.CommonFilePath;
			var orgName = _urlFriendlyRenamingService.Rename(memberRow.OrgName);
			return new DirectoryInfo(pth + lrow.Name + "\\Member\\" + orgName + "\\" + fileCatRow.Name);
		}

		protected virtual string[] GetMemberFiles(LocationRow lrow, MemberRow memberRow, FileCategoryRow fileCatRow)
		{
            var pth = Base.Business.Globals.CommonFilePath;
			var orgName = _urlFriendlyRenamingService.Rename(memberRow.OrgName);
			return Directory.GetFiles(pth + lrow.Name + "\\Member\\" + orgName + "\\" + fileCatRow.Name, "*", SearchOption.TopDirectoryOnly);			
		}

		private string EscapeSqlLikeWildcards(string name)
		{
			return name
				.Replace("[", "[ [ ]")
				.Replace("%","[%]");
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

		protected virtual string GetImageSrcForDocument(FileInfo fileInfo)
		{
            switch (fileInfo.Extension) 
			{
                case ".pdf": return "/wpc/Synologen/Supplier/pdfthumbnail.aspx";
                case ".xls": return "/wpc/Synologen/Supplier/Images/excel.gif";
                case ".xlsx": return "/wpc/Synologen/Supplier/Images/exel.gif";
                case ".ppt": return "/wpc/Synologen/Supplier/Images/powerpoint.gif";
                case ".pptx": return "/wpc/Synologen/Supplier/Images/powerpoint.gif";
                case ".doc": return "/wpc/Synologen/Supplier/Images/word.gif";
                case ".docx": return "/wpc/Synologen/Supplier/Images/word.gif";
                case ".eps": return "/wpc/Synologen/Supplier/Images/eps.png";
                default: return "/wpc/Synologen/Supplier/Images/standard.png";
            }
		}
    }
}
