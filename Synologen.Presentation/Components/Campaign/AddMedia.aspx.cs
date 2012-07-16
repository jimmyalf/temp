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
using Spinit.Wpc.Campaign.Data.Enumerations;
using Spinit.Wpc.Campaign.Business;
using Spinit.Wpc.Utility.Business;

public partial class components_Campaign_AddMedia : CampaignPage
{
    private int _campaignId = -1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["id"] != null)
            _campaignId = Convert.ToInt32(Request.Params["id"]);

        if(!IsPostBack)
            PopulateForm();
    }

    private void PopulateForm()
    {
        lblDesc1.Text = "Description 1";
        lblDesc2.Text = "Description 2";
        lblDesc3.Text = "Description 3";
        lblDesc4.Text = "Description 4";
        lblDesc5.Text = "Description 5";
        lblFile1.Text = "File 1";
        lblFile2.Text = "File 2";
        lblFile3.Text = "File 3";
        lblFile4.Text = "File 4";
        lblFile5.Text = "File 5";
        lblCategory1.Text = "Category 1";
        lblCategory2.Text = "Category 2";
        lblCategory3.Text = "Category 3";
        lblCategory4.Text = "Category 4";
        lblCategory5.Text = "Category 5";

        PopulateCategories();

        if (_campaignId != -1)
        {
            CampaignRow row = row = Provider.GetCampaign(_campaignId, base.LocationId, base.LanguageId);
            lblName.Text = row.Name;
        }
    }

    private void PopulateCategories()
    {

        List<FileCategoryRow> list = base.Provider.GetAllFileCategoriesList();
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

  

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string allowedExtensions = Spinit.Wpc.Base.Business.Globals.ImageType;
        allowedExtensions
            = allowedExtensions + "," + Spinit.Wpc.Base.Business.Globals.MediaType;
        allowedExtensions
           = allowedExtensions + "," + Spinit.Wpc.Base.Business.Globals.FlashType;
        allowedExtensions
           = allowedExtensions + "," + Spinit.Wpc.Base.Business.Globals.DocumentType;

        String[] allowedExtensionsArr = allowedExtensions.Split(new char[] { ',' });

        string path = Spinit.Wpc.Base.Business.Globals.CommonFilePath 
                        + base.LocationName + "\\Campaign\\" + lblName.Text + "\\";
        string url = base.LocationName + "/Campaign/" + lblName.Text;

        UploadFile(uplFile1, txtDesc1, path, url, drpCategory1.SelectedItem.Text, allowedExtensionsArr);
        UploadFile(uplFile2, txtDesc2, path, url, drpCategory2.SelectedItem.Text, allowedExtensionsArr);
        UploadFile(uplFile3, txtDesc3, path, url, drpCategory3.SelectedItem.Text, allowedExtensionsArr);
        UploadFile(uplFile4, txtDesc4, path, url, drpCategory4.SelectedItem.Text, allowedExtensionsArr);
        UploadFile(uplFile5, txtDesc5, path, url, drpCategory5.SelectedItem.Text, allowedExtensionsArr);

        Response.Redirect(Util.ApplicationPath + Spinit.Wpc.Campaign.Business.Globals.ComponentApplicationPath + "Index.aspx", true);

    }

    private void UploadFile(FileUpload uplFile, 
                            TextBox txtDesc, 
                            string path, 
                            string url,
                            string categoryName,
                            String[] allowedExtensionsArr)
    {
        if (uplFile.HasFile)
        {
            bool fileOK = false;

            String fileExtension =
               System.IO.Path.GetExtension(uplFile.FileName).ToLower();
            fileExtension
           = fileExtension.Substring(1,
                                      fileExtension.Length - 1);

            for (int i = 0; i < allowedExtensionsArr.Length; i++)
            {
                if (fileExtension.Equals(allowedExtensionsArr[i]))
                {
                    fileOK = true;
                    break;
                }
            }
            if (fileOK)
            {
                try
                {
                    string name = url + "/" + categoryName + "/" + uplFile.FileName;

                    DirectoryInfo di = new DirectoryInfo(path + categoryName);
                    if(!di.Exists)
                        di.Create();

                    uplFile.PostedFile.SaveAs(path
                        + categoryName + "\\" + uplFile.FileName);

                    Spinit.Wpc.Base.Data.File fle
                        = new Spinit.Wpc.Base.Data.File
                                (Spinit.Wpc.Base.Business
                                    .Globals.ConnectionString);

                    string description = txtDesc.Text;
                    if (description.Length == 0)
                    {
                        description = null;
                    }

                    fle.AddFile(name,
                                 false,
                                 fileExtension,
                                 null,
                                 description,
                                 base.CurrentUser);
                    //Label1.Text = "File uploaded!";
                }
                catch (Exception ex)
                {
                    //Label1.Text = "File could not be uploaded.";
                }
            }
            else
            {
                //Label1.Text = "Cannot accept files of this type.";
            }
        }
    }
}
