using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.IO;

using Spinit.Wpc.Utility.Business;
using Spinit.Wpc.Utility.Business.SmartMenu;
using Spinit.Wpc.Base.Data;

public partial class components_Campaign_SpotImage : System.Web.UI.UserControl
{
    private int _fileId = -1;
    private string _locationName = "extra.synologen.nu";
    //private int _selectedFileId = -1;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            this.ViewState["url"] =  Spinit.Wpc.Base
                                        .Business.Globals
                                        .CommonFileUrl + LocationName + "/";

            this.ViewState["pth"] = Spinit.Wpc.Base
                                        .Business.Globals
                                        .CommonFilePath + LocationName + "\\";


            imgThumb.Visible = false;

            if (SelectedFile > 0)
            {
                ShowSelectedFileAsThumb();
                btnClear.Visible = true;
            }
            else
            {
                btnClear.Visible = false;
            }
    }

    }

    /// <summary>
    /// Populates the files.
    /// </summary>
    private void PopulateFiles()
    {
        DataTable tblFiles = new DataTable("tblFiles");

        tblFiles.Columns.Add(new DataColumn
                                    ("Id",
                                     Type.GetType("System.String")));
        tblFiles.Columns.Add(new DataColumn
                                    ("Directory",
                                     Type.GetType("System.String")));
        tblFiles.Columns.Add(new DataColumn
                                    ("Name",
                                     Type.GetType("System.String")));
        tblFiles.Columns.Add(new DataColumn
                                    ("Description",
                                     Type.GetType("System.String")));
        tblFiles.Columns.Add(new DataColumn
                                    ("Size",
                                     Type.GetType("System.String")));
        tblFiles.Columns.Add(new DataColumn
                                    ("Pic",
                                     Type.GetType("System.String")));
        tblFiles.Columns.Add(new DataColumn
                                   ("Link",
                                    Type.GetType("System.String")));




        //Spinit.Wpc.Base.Data.Location location
        //    = new Spinit.Wpc.Base.Data.Location
        //            (Spinit.Wpc.Base.Business
        //                    .Globals.ConnectionString);
        //LocationRow lrow = (LocationRow)location.GetLocation(Location);




        Spinit.Wpc.Base.Data.File file
                = new Spinit.Wpc.Base.Data.File
                        (Spinit.Wpc.Base.Business
                            .Globals.ConnectionString);

        //string pth = Spinit.Wpc.Base
        //                        .Business.Globals
        //                        .CommonFilePath;

        //string url = Spinit.Wpc.Base
        //                        .Business.Globals
        //                        .CommonFileUrl;

        string url = (string)this.ViewState["url"];

        string pth = (string)this.ViewState["pth"];

        DirectoryInfo di = new DirectoryInfo(pth);
            if (!di.Exists)
                return;

        string[] dirs
                 = System.IO.Directory.GetDirectories(pth,
                                                 "*",
                                                 SearchOption.TopDirectoryOnly);
        
        foreach (string dir in dirs)
        {
            try
            {
                DataRow row = tblFiles.NewRow();

                DirectoryInfo inf = new DirectoryInfo(dir);

                string[] nmes = dir.Split('\\');

                string nme = nmes[nmes.Length - 1];

                string urlname = dir.Replace(Spinit.Wpc.Base.Business.Globals.CommonFilePath, "").Replace("\\","/");

                //string urlname = LocationName + "/" + nme;

                FileRow fleRow = (FileRow)file.GetFile(urlname);



                string link = "";
                string pic = "/common/icons/Folder-closed.png";
                string desc = "";

                row["Id"] = fleRow.Id;
                row["Directory"] = "1";
                row["Name"] = nme;
                row["Description"] = desc;
                row["Size"] = "";
                row["Pic"] = pic;
                row["Link"] = link;

                tblFiles.Rows.Add(row);
            }
            catch { }

        }


        string[] fles
                 = System.IO.Directory.GetFiles(pth,
                                                 "*",
                                                 SearchOption.TopDirectoryOnly);

            

        foreach (string fle in fles)
        {
            try
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

                //string urlname = LocationName + "/" + nme;

                string urlname = fle.Replace(Spinit.Wpc.Base.Business.Globals.CommonFilePath, "").Replace("\\", "/");


                FileRow fleRow = (FileRow)file.GetFile(urlname);

                string link = "";
                string pic = "";
                string desc = "";

                if (Spinit.Wpc.Base.Business.Globals.ImageType.IndexOf(fleRow.ContentInfo)
                        != -1)
                {
                    pic =  Spinit.Wpc.Base
                                .Business.Globals
                                .CommonFileUrl + fleRow.Name;
                

                    row["Id"] = fleRow.Id;
                    row["Directory"] = "0";
                    row["Name"] = nme;
                    row["Description"] = desc;
                    row["Size"] = inf.Length.ToString();
                    row["Pic"] = pic;
                    row["Link"] = link;

                    tblFiles.Rows.Add(row);
                }
            }
            catch { }

        }

        gvFiles.DataSource = tblFiles;
        gvFiles.DataBind();

    }

    protected void gvFiles_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Get the currently selected row using the SelectedRow property.
        try
        {
            GridViewRow row = gvFiles.SelectedRow;

            if (row != null)
            {
                int fileId = int.Parse(gvFiles.SelectedDataKey.Value.ToString());
                 Spinit.Wpc.Base.Data.File file
                          = new Spinit.Wpc.Base.Data.File
                                  (Spinit.Wpc.Base.Business
                                      .Globals.ConnectionString);
                 if (fileId > 0)
                 {
                     FileRow fleRow = (FileRow)file.GetFile(fileId);
                     if (fleRow.Directory)
                     {
                         //goto directory
                         imgPreview.Visible = false;

                         this.ViewState["url"] = Spinit.Wpc.Base
                                                     .Business.Globals
                                                     .CommonFileUrl + fleRow.Name + "/";

                         this.ViewState["pth"] = Spinit.Wpc.Base
                                                     .Business.Globals
                                                     .CommonFilePath + fleRow.Name.Replace("/", "\\") + "\\";

                         imgPreview.Visible = false;
                         PopulateFiles();
                     }
                     else
                     {
                         //show file
                         string url = Spinit.Wpc.Base
                                     .Business.Globals
                                     .CommonFileUrl;

                         SelectedFile = fileId;
                         int height = 0;
                         int width = 0;
                         ScaleImage(fileId, ref height, ref width);
                         imgPreview.ImageUrl = url + fleRow.Name;
                         imgPreview.Height = height;
                         imgPreview.Width = width;
                         imgPreview.Visible = true;
                         
                     }
                 }

            }

            
        }
        catch(Exception ex) {
            
            Response.Write(ex.Message);
        }


    }

    private void ScaleImage(int fileId, ref int height, ref int width)
    {
        try
        {
            string url = Spinit.Wpc.Base
                                     .Business.Globals
                                     .CommonFileUrl;

            Spinit.Wpc.Base.Data.File file
                          = new Spinit.Wpc.Base.Data.File
                                  (Spinit.Wpc.Base.Business
                                      .Globals.ConnectionString);
                 if (fileId > 0)
                 {
                     FileRow fleRow = (FileRow)file.GetFile(fileId);

                         
                    System.Drawing.Image image = null;

                    image = System.Drawing.Image.FromFile(Server.MapPath(url + fleRow.Name));
                    

                    float ratio = 0f;
                    height = -1;
                    width = -1;
                    int maxHeight = 300;
                    int maxWidth = 200;

                    //Check if scaling needed
                    if (200 > 0 && maxHeight > 0
                        && (image.Height > maxHeight || image.Width > maxWidth))
                    {

                        if (image.Height > maxHeight && image.Width < maxWidth)
                        {
                            //Height larger than max, width smaller than max
                            ratio = (float)image.Width / (float)image.Height;

                            height = maxHeight;
                            width = (int)(ratio * maxHeight);


                        }

                        else if (image.Height < maxHeight && image.Width > maxWidth)
                        {
                            //Width larger than max, height smaller than max
                            ratio = (float)image.Height / (float)image.Width;

                            height = (int)(ratio * maxWidth);
                            width = maxWidth;
                        }

                        else if (image.Height > maxHeight && image.Width > maxWidth)
                        {
                            //Width larger than max, height larger than max

                            ratio = (float)image.Height / (float)image.Width;

                            height = (int)(ratio * maxWidth);
                            width = maxWidth;


                            if (height > maxHeight)
                            {
                                ratio = (float)image.Width / (float)image.Height;
                                height = maxHeight;
                                width = (int)(ratio * maxHeight);
                            }
                        }
                 }
             }

        }
        catch { }

    }

    private void ShowSelectedFileAsThumb()
    {
        string url = Spinit.Wpc.Base
                               .Business.Globals
                               .CommonFileUrl;


        Spinit.Wpc.Base.Data.File file
              = new Spinit.Wpc.Base.Data.File
                      (Spinit.Wpc.Base.Business
                          .Globals.ConnectionString);
        if (SelectedFile > 0)
        {
            FileRow fleRow = (FileRow)file.GetFile(SelectedFile);
            imgThumb.ImageUrl = url + fleRow.Name;
            imgThumb.Visible = true;
        }
        else
        {
            imgThumb.Visible = false;
        }
    }


    public string LocationName
    {
        get { return _locationName; }
        set { _locationName = value; }
    }

    public int SelectedFile
    {
        get
        {
            if (ViewState["selectedFileId"] != null)
                return (int)ViewState["selectedFileId"];
            else
                return -1;
           
        }
        set
        {
            ViewState["selectedFileId"] = value;
        }

       
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        imgThumb.Visible = false;
        SelectedFile = -1;
    }
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        dSelectSpot.Visible = true;
        PopulateFiles();
    }
    protected void btnSet_Click(object sender, EventArgs e)
    {
        dSelectSpot.Visible = false;
        imgThumb.ImageUrl = imgPreview.ImageUrl;
        imgThumb.Visible = true;
    }
    protected void btnParent_Click(object sender, EventArgs e)
    {

        string url = (string)ViewState["url"];

        url = url.Substring(0, url.Length - 1);
        url = url.Substring(0, url.LastIndexOf("/"));
        
        this.ViewState["url"] = url + "/";

        string pth = (string)ViewState["pth"];

        pth = pth.Substring(0, pth.Length - 1);
        pth = pth.Substring(0, pth.LastIndexOf("\\"));

        this.ViewState["pth"] = pth + "\\";

        //this.ViewState["pth"] = Spinit.Wpc.Base
        //                            .Business.Globals
        //                            .CommonFilePath + LocationName + "\\";

        imgPreview.Visible = false;
        PopulateFiles();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        dSelectSpot.Visible = false;
    }
    
}
