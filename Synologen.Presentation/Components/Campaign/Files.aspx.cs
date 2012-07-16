using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;

using Spinit.Wpc.Campaign.Data;
using Spinit.Wpc.Campaign.Business;
using Spinit.Wpc.Utility.Business;
using Spinit.Wpc.Utility.Business.SmartMenu;
using Spinit.Wpc.Base.Data;

public partial class components_Campaign_Files : CampaignPage
{
    private int _pageSize;
    private int _selectedCategory = 0;
    //private int _fileCategoryId = 0;
    private int _campaignId = -1;

    protected void Page_Init(object sender, EventArgs e)
    {
        RenderCampaignSubMenu(Page.Master);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["id"] != null)
            _campaignId = Convert.ToInt32(Request.Params["id"]);

        if (!IsPostBack)
        {
            base.SortExpression = "Name";
            base.SortAscending = false;
            PopulateFileCategories();
            PopulateFiles();
        }
    }


    private void PopulateFileCategories()
    {
        List<FileCategoryRow> list = base.Provider.GetAllFileCategoriesList();
        if (list == null)
            list = new List<FileCategoryRow>();
        //FileCategoryRow fcr = new FileCategoryRow();
        //fcr.Name = "-- All --";
        //fcr.Id = -1;
        //list.Insert(0, fcr);
        drpFileCategories.DataSource = list;
        drpFileCategories.DataTextField = "Name";
        drpFileCategories.DataValueField = "Id";
        drpFileCategories.DataBind();
    }

    private void PopulateFiles()
    {
        DataTable tblFiles = new DataTable("tblFiles");

        tblFiles.Columns.Add(new DataColumn
                                    ("Id",
                                     Type.GetType("System.String")));
        tblFiles.Columns.Add(new DataColumn
                                    ("Name",
                                     Type.GetType("System.String")));
        tblFiles.Columns.Add(new DataColumn
                                    ("Description",
                                     Type.GetType("System.String")));
        tblFiles.Columns.Add(new DataColumn
                                    ("Application",
                                     Type.GetType("System.String")));
        tblFiles.Columns.Add(new DataColumn
                                    ("Pic",
                                     Type.GetType("System.String")));
        tblFiles.Columns.Add(new DataColumn
                                   ("Link",
                                    Type.GetType("System.String")));


        Spinit.Wpc.Base.Data.Location location
            = new Spinit.Wpc.Base.Data.Location
                    (Spinit.Wpc.Base.Business
                            .Globals.ConnectionString);
        LocationRow lrow = (LocationRow)location.GetLocation(base.LocationId);


        

        Spinit.Wpc.Base.Data.File file
                = new Spinit.Wpc.Base.Data.File
                        (Spinit.Wpc.Base.Business
                            .Globals.ConnectionString);

        string pth = Spinit.Wpc.Base
                                .Business.Globals
                                .CommonFilePath;

        string url = Spinit.Wpc.Base
                                .Business.Globals
                                .CommonFileUrl;


        CampaignRow campaignRow = Provider.GetCampaign(_campaignId, base.LocationId, base.LanguageId);
        


        if (_selectedCategory > 0)
        {
            FileCategoryRow fileCatRow = base.Provider.GetFileCategory(_selectedCategory);

            if (fileCatRow == null)
                return;

            DirectoryInfo di = new DirectoryInfo(pth + lrow.Name + "\\Campaign\\" + campaignRow.Name
                                                  + "\\" + fileCatRow.Name);
            if (!di.Exists)
                return;

            string[] fles
                     = System.IO.Directory.GetFiles(pth + lrow.Name + "\\Campaign\\" + campaignRow.Name 
                                                        + "\\" + fileCatRow.Name,
                                                     "*",
                                                     SearchOption.TopDirectoryOnly);

           

            //ArrayList fileList = new ArrayList();

            foreach (string fle in fles)
            {
                DataRow row = tblFiles.NewRow();

                FileInfo inf = new FileInfo(fle);

                string[] nmes = fle.Split('\\');

                string nme = nmes[nmes.Length - 1];

                string contentInfo = null;

                int inx = nme.LastIndexOf(".");
                if (inx != -1)
                {
                    contentInfo
                            = nme.Substring(inx + 1,
                                             nme.Length - inx - 1);
                }

                string urlname = lrow.Name + "/Campaign/" + campaignRow.Name + "/" + fileCatRow.Name + "/" + nme;

                FileRow fleRow = (FileRow)file.GetFile(urlname);


                string link = "";
                string pic = "";
                bool fileOk = false;

                if (Spinit.Wpc.Base.Business.Globals.ImageType.IndexOf(contentInfo)
                        != -1)
                {
                    fileOk = true;
                    link = url + urlname;
                    pic = "/wpc/Campaign/img/photo_icon.gif";
                }

                if (Spinit.Wpc.Base.Business.Globals.MediaType.IndexOf(contentInfo)
                        != -1)
                {
                    fileOk = true;
                    link = url + urlname;
                    pic = "/wpc/Campaign/img/video_icon.gif";
                }

                if (Spinit.Wpc.Base.Business.Globals.DocumentType.IndexOf(contentInfo)
                       != -1)
                {
                    fileOk = true;
                    link = url + urlname;
                    pic = "/wpc/Campaign/img/video_icon.gif";
                }

                if (Spinit.Wpc.Base.Business.Globals.FlashType.IndexOf(contentInfo)
                       != -1)
                {
                    fileOk = true;
                    link = url + urlname;
                    pic = "/wpc/Campaign/img/video_icon.gif";
                }


                if (fleRow == null || !fileOk)
                    continue;

                string desc = fleRow.Description;

                row["Id"] = fleRow.Id;
                row["Name"] = nme;
                row["Description"] = desc;
                row["Application"] = fileCatRow.Name;
                row["Pic"] = pic;
                row["Link"] = link;

                tblFiles.Rows.Add(row);
            }
        }
        else
        {
            ArrayList fileCatRows = new ArrayList(base.Provider.GetAllFileCategoriesList());
            if (fileCatRows == null)
                return;

            foreach (FileCategoryRow fileCatRow in fileCatRows)
            {
                DirectoryInfo di = new DirectoryInfo(pth + lrow.Name + "\\Campaign\\" + campaignRow.Name
                                                 + "\\" + fileCatRow.Name);
                if (!di.Exists)
                    continue;


                string[] fles
                     = System.IO.Directory.GetFiles(pth + lrow.Name + "\\Campaign\\" + campaignRow.Name 
                                                    + "\\" + fileCatRow.Name,
                                                     "*",
                                                     SearchOption.TopDirectoryOnly);



                foreach (string fle in fles)
                {
                    DataRow row = tblFiles.NewRow();

                    FileInfo inf = new FileInfo(fle);

                    string[] nmes = fle.Split('\\');

                    string nme = nmes[nmes.Length - 1];

                    string contentInfo = null;

                    int inx = nme.LastIndexOf(".");
                    if (inx != -1)
                    {
                        contentInfo
                                = nme.Substring(inx + 1,
                                                 nme.Length - inx - 1);
                    }

                    string urlname = lrow.Name + "/Campaign/" + campaignRow.Name + "/" 
                                        + fileCatRow.Name + "/" + nme;

                    FileRow fleRow = (FileRow)file.GetFile(urlname);


                    string link = "";
                    string pic = "";
                    bool fileOk = false;

                    if (Spinit.Wpc.Base.Business.Globals.ImageType.IndexOf(contentInfo)
                            != -1)
                    {
                        fileOk = true;
                        link = url + urlname;
                        pic = "/wpc/Campaign/img/photo_icon.gif";
                    }

                    if (Spinit.Wpc.Base.Business.Globals.MediaType.IndexOf(contentInfo)
                            != -1)
                    {
                        fileOk = true;
                        link = url + urlname;
                        pic = "/wpc/Campaign/img/video_icon.gif";
                    }

                    if (Spinit.Wpc.Base.Business.Globals.DocumentType.IndexOf(contentInfo)
                            != -1)
                    {
                        fileOk = true;
                        link = url + urlname;
                        pic = "/wpc/Campaign/img/video_icon.gif";
                    }

                    if (Spinit.Wpc.Base.Business.Globals.FlashType.IndexOf(contentInfo)
                            != -1)
                    {
                        fileOk = true;
                        link = url + urlname;
                        pic = "/wpc/Campaign/img/video_icon.gif";
                    }


                    if (fleRow == null || !fileOk)
                        continue;

                    string desc = fleRow.Description;

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


        

        DataRow[] rows = tblFiles.Select("", base.SortExpression + ((base.SortAscending) ? " ASC" : " DESC"));
        
        DataTable tblSorted = new DataTable();
        tblSorted = tblFiles.Clone();
        foreach (DataRow row in rows)
        {
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
    /// <param name="deleteEventHandler">The delete EventHandler. Null if delete menu should be hidden.</param>

    public void RenderCampaignSubMenu(System.Web.UI.MasterPage master)
    {
        Spinit.Wpc.Campaign.Presentation.CampaignMain m = (Spinit.Wpc.Campaign.Presentation.CampaignMain)master;
        PlaceHolder phCampaignSubMenu = m.SubMenu;
        SmartMenu.Menu subMenu = new SmartMenu.Menu();
        subMenu.ID = "SubMenu";
        subMenu.ControlType = "ul";
        subMenu.ItemControlType = "li";
        subMenu.ItemWrapperElement = "span";

        SmartMenu.ItemCollection itemCollection = new SmartMenu.ItemCollection();
        itemCollection.AddItem("New", null, "New file", "Create new files", null, "btnAdd_OnClick", false, null);
        itemCollection.AddItem("Delete", null, "Delete files", "Delete selected files", null, "btnDelete_OnClick", false, null);

        subMenu.MenuItems = itemCollection;

        m.CampaignSmartMenu.Render(subMenu, phCampaignSubMenu);
    }

    void AddGlyph(GridView grid, GridViewRow item)
    {
        Label glyph = new Label();
        glyph.EnableTheming = false;
        glyph.Font.Name = "webdings";
        glyph.Font.Size = FontUnit.XSmall;
        glyph.Text = (base.SortAscending ? " 5" : " 6");

        // Find the column you sorted by
        for (int i = 0; i < grid.Columns.Count; i++)
        {
            string colExpr = grid.Columns[i].SortExpression;
            if (colExpr != "" && colExpr == base.SortExpression)
            {
                item.Cells[i].Controls.Add(glyph);
            }
        }
    }

   

    #region Common Events

    protected void btnSetFilter_Click(Object sender, EventArgs e)
    {
        //pager.PageIndex = 0;
        _selectedCategory = Convert.ToInt32(drpFileCategories.SelectedItem.Value);
        PopulateFiles();
    }

    protected void btnShowAll_Click(Object sender, EventArgs e)
    {
        //pager.PageIndex = 0;
        _selectedCategory = 0;
        drpFileCategories.SelectedIndex = 0;
        PopulateFiles();
    }

    

    protected void btnAdd_OnClick(object sender, EventArgs e)
    {
        Response.Redirect(Util.ApplicationPath + Spinit.Wpc.Campaign.Business.Globals.ComponentApplicationPath + "AddMedia.aspx?id=" + _campaignId);
    }

    protected void btnDelete_OnClick(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gvFiles.Rows)
        {
            CheckBox chk = (CheckBox)row.FindControl("chkSelect");
            if ((chk != null) && chk.Checked)
            {

                //Delete file
                int fileId = 0;

                try
                {
                    fileId = int.Parse((string)gvFiles.DataKeys[row.RowIndex]["Id"]);
                }
                catch { }

                //Delete file

                if (fileId > 0)
                {
                    Spinit.Wpc.Base.Data.File file
                            = new Spinit.Wpc.Base.Data.File
                                    (Spinit.Wpc.Base.Business
                                        .Globals.ConnectionString);

                    string pth = Spinit.Wpc.Base
                                            .Business.Globals
                                            .CommonFilePath;

                    string url = Spinit.Wpc.Base
                                            .Business.Globals
                                            .CommonFileUrl;

                    FileRow fileToDelete = (FileRow)file.GetFile(fileId);

                    if (fileToDelete != null)
                    {
                        FileInfo fi = new FileInfo(Server.MapPath(url + fileToDelete.Name));
                        if (fi.Exists)
                            fi.Delete();



                        try
                        {
                            file.DeleteFile(fileId);
                        }
                        catch { }
                    }
                }

            }
        }
        PopulateFiles();
    }

    protected void chkSelectHeader_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkHeader = (CheckBox)sender;
        if (chkHeader != null)
        {
            foreach (GridViewRow row in gvFiles.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkSelect");
                if (chk != null)
                {
                    chk.Checked = chkHeader.Checked;
                }
            }
        }
    }
    #endregion

    #region GridView Events

    protected void gvFiles_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
            AddGlyph(gvFiles, e.Row);
    }

    protected void gvFiles_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        
        gvFiles.PageIndex = e.NewPageIndex;
        DataBind();
    }

    protected void gvFiles_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (e.SortExpression == base.SortExpression)
            base.SortAscending = !base.SortAscending;
        else
            base.SortAscending = true;
        base.SortExpression = e.SortExpression;
        PopulateFiles();
    }

    protected void gvFiles_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        
        int fileId = 0;
        try
        {
            fileId = int.Parse((string)gvFiles.DataKeys[e.RowIndex].Value);
        }
        catch { }

        if (fileId > 0)
        {
            Spinit.Wpc.Base.Data.File file
                    = new Spinit.Wpc.Base.Data.File
                            (Spinit.Wpc.Base.Business
                                .Globals.ConnectionString);

            string pth = Spinit.Wpc.Base
                                    .Business.Globals
                                    .CommonFilePath;

            string url = Spinit.Wpc.Base
                                    .Business.Globals
                                    .CommonFileUrl;

            FileRow fileToDelete = (FileRow)file.GetFile(fileId);

            if (fileToDelete != null)
            {
                FileInfo fi = new FileInfo(Server.MapPath(url + fileToDelete.Name));
                if (fi.Exists)
                    fi.Delete();



                try
                {
                    file.DeleteFile(fileId);
                }
                catch { }
            }
        }
        PopulateFiles();
    }

    protected void gvFiles_Editing(object sender, GridViewEditEventArgs e)
    {
       
    }


    protected void gvFiles_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Edit":
                break;
        }

    }

    protected void gvFiles_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
        
    }

  

    /// <summary>
    /// Add delete confirmation
    /// </summary>
    /// <param name="sender">The sending object.</param>
    /// <param name="e">The event arguments.</param>

    protected void AddConfirmDelete(object sender, EventArgs e)
    {
        ClientConfirmation cc = new ClientConfirmation();
        cc.AddConfirmation(ref sender, "Do you really wan\\'t to delete the file?");
    }

    #endregion
}
