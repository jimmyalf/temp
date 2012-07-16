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

using Spinit.Wpc.Campaign.Data;
using Spinit.Wpc.Campaign.Data.Enumerations;
using Spinit.Wpc.Campaign.Business;
using Spinit.Wpc.Utility.Business;

public partial class components_Campaign_EditCampaign : CampaignPage
{
    private int _campaignId = -1;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["id"] != null)
            _campaignId = Convert.ToInt32(Request.Params["id"]);

        try
        {
            SpotImage1.LocationName = base.LocationName;
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }

        if (!Page.IsPostBack)
        {
            PopulateForm();
            PopulateLocationList();
            PopulateCategoryList();

            if (_campaignId > 0)
                PopulateCampaign();
        }
      
    }

    private void PopulateForm()
    {
        lblHeader.Text = "Add Campaign";
        if (_campaignId > 0)
            lblHeader.Text = "Edit Campaign";

        lblName.Text = "Name";
        lblHeading.Text = "Heading";
        lblDescription.Text = "Description";
        lblSpotImage.Text = "Spot image";
        lblSpotHeight.Text = "Spot scale max height in pixels";
        lblSpotWidth.Text = "Spot scale max width in pixels";
        txtSpotHeight.ToolTip = "Scale image to fit this height. No scaling when empty.";
        txtSpotWidth.ToolTip = "Scale image to fit this width. No scaling when empty.";
        lblStartDate.Text = "StartDate";
        lblEndDate.Text = "EndDate";

        lblType.Text = "Campaign type";
        rblType.SelectedValue = "Thumbs";
        if (rblType.SelectedValue == "Thumbs")
        {
            divThumbsColumns.Visible = true;
            divThumbsHeight.Visible = true;
            divThumbsRows.Visible = false;
            divThumbsWidth.Visible = true;
            divListRowsPerPage.Visible = false;

        }
        else
        {
            divThumbsColumns.Visible = false;
            divThumbsHeight.Visible = false;
            divThumbsRows.Visible = false;
            divThumbsWidth.Visible = false;
            divListRowsPerPage.Visible = false;

        }
        lblThumbsRows.Text = "Rows";
        lblThumbsColumns.Text = "Thumbnail Columns";
        lblThumbsHeight.Text = "Thumbnail Height";
        lblThumbsWidth.Text = "Thumbnail Width";
        lblListRowsPerPage.Text = "Rows per page";
        

    }

    private void PopulateCampaign()
    {
        CampaignRow row = row = Provider.GetCampaign(_campaignId, base.LocationId, base.LanguageId);
        txtName.Text = row.Name;
        txtHeading.Text = row.Heading;
        txtDescription.Text = row.Description;

        if (row.SpotHeight > 0)
            txtSpotHeight.Text = row.SpotHeight.ToString();
        else
            txtSpotHeight.Text = "";
        if(row.SpotWidth > 0)
            txtSpotWidth.Text = row.SpotWidth.ToString();
        else
            txtSpotWidth.Text = "";

        SpotImage1.SelectedFile = row.CampaignSpot;

        if (row.StartDate != DateTime.MinValue)
            dtcStartDate.SelectedDate = row.StartDate;
        if (row.EndDate != DateTime.MinValue)
            dtcEndDate.SelectedDate = row.EndDate;


        if (row.CampaignType == CampaignTypes.Thumbs)
            rblType.SelectedValue = "Thumbs";
        else
            rblType.SelectedValue = "List";

        if (rblType.SelectedValue == "Thumbs")
        {
            divThumbsColumns.Visible = true;
            divThumbsHeight.Visible = true;
            divThumbsRows.Visible = false;
            divThumbsWidth.Visible = true;
            divListRowsPerPage.Visible = false;
          
        }
        else
        {
            divThumbsColumns.Visible = false;
            divThumbsHeight.Visible = false;
            divThumbsRows.Visible = false;
            divThumbsWidth.Visible = false;
            divListRowsPerPage.Visible = false;
           
        }
        txtThumbsRows.Text = row.ThumbsRows.ToString();
        txtThumbsColumns.Text = row.ThumbsColumns.ToString();
        txtThumbsHeight.Text = row.ThumbsHeight.ToString();
        txtThumbsWidth.Text = row.ThumbsWidth.ToString();
        txtListRowsPerPage.Text = row.ListRowsPerPage.ToString();

       

        foreach (ListItem item in chklLocations.Items)
        {
            if (row.IsConnectedLocation(Convert.ToInt32(item.Value)))
                item.Selected = true;
            else
                item.Selected = false;
        }
        foreach (ListItem item in chklCategories.Items)
        {
            if (row.IsConnectedCategory(Convert.ToInt32(item.Value)))
                item.Selected = true;
            else
                item.Selected = false;
        }
       
    }

    private void PopulateCategoryList()
    {
        chklCategories.DataSource = base.Provider.GetCategories(base.LocationId, base.LanguageId);
        chklCategories.DataBind();
    }

    /// <summary>
    /// Populates the checklistbox for all available locations.
    /// The current location will be default selected
    /// </summary>
    private void PopulateLocationList()
    {
        chklLocations.DataSource = base.Locations;
        chklLocations.DataBind();
        foreach (ListItem item in chklLocations.Items)
        {
            if ((Convert.ToInt32(item.Value)) == base.LocationId)
            {
                item.Selected = true;
                break;
            }
        }
    }

    protected void rblType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblType.SelectedValue == "Thumbs")
        {
            divThumbsColumns.Visible = true;
            divThumbsHeight.Visible = true;
            divThumbsRows.Visible = false;
            divThumbsWidth.Visible = true;
            divListRowsPerPage.Visible = false;
            //txtThumbsColumns.Visible = true;
            //txtThumbsHeight.Visible = true;
            //txtThumbsRows.Visible = true;
            //txtThumbsWidth.Visible = true;
            //txtListRowsPerPage.Visible = false;
        }
        else
        {
            divThumbsColumns.Visible = false;
            divThumbsHeight.Visible = false;
            divThumbsRows.Visible = false;
            divThumbsWidth.Visible = false;
            divListRowsPerPage.Visible = false;
            //lblThumbsColumns.Visible = false;
            //lblThumbsHeight.Visible = false;
            //lblThumbsRows.Visible = false;
            //lblThumbsWidth.Visible = false;
            //lblListRowsPerPage.Visible = true;
            //txtThumbsColumns.Visible = false;
            //txtThumbsHeight.Visible = false;
            //txtThumbsRows.Visible = false;
            //txtThumbsWidth.Visible = false;
            //txtListRowsPerPage.Visible = true;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        CampaignRow row = new CampaignRow();
        Enumerations.Action action = Enumerations.Action.Create;
        Button btnSave = (Button)sender;
        Spinit.Wpc.Campaign.Data.Enumerations.SaveType typeOfSave =
            (Spinit.Wpc.Campaign.Data.Enumerations.SaveType)
            Enum.Parse(typeof(Spinit.Wpc.Campaign.Data.Enumerations.SaveType), btnSave.CommandName);
        if (_campaignId > 0)
        {
            row = Provider.GetCampaign(_campaignId, base.LocationId, base.LanguageId);
            action = Enumerations.Action.Update;
            row.EditedBy = base.CurrentUser;
        }
        else
        {
            row.CreatedBy = base.CurrentUser;
        }
        switch (typeOfSave)
        {
            case SaveType.SaveAndPublish:
                row.ApprovedBy = base.CurrentUser;
                row.ApprovedDate = DateTime.Now;
                row.LockedBy = null;
                row.LockedDate = DateTime.MinValue;
                break;
            case SaveType.SaveForApproval:
                row.ApprovedBy = null;
                row.ApprovedDate = DateTime.MinValue;
                row.LockedBy = null;
                row.LockedDate = DateTime.MinValue;
                break;
            case SaveType.SaveForLater:
                row.ApprovedBy = null;
                row.ApprovedDate = DateTime.MinValue;
                row.LockedBy = base.CurrentUser;
                row.LockedDate = DateTime.Now;
                break;
        }
        row.Name = txtName.Text;

        if (txtHeading.Text != String.Empty)
            row.Heading = txtHeading.Text;
        if (txtDescription.Text != String.Empty)
            row.Description = txtDescription.Text;

        row.CampaignSpot = SpotImage1.SelectedFile;


        int spotheight = -1;
        try
        {
            spotheight = int.Parse(txtSpotHeight.Text);
        }
        catch { }
        row.SpotHeight = spotheight;

        int spotwidth = -1;
        try
        {
            spotwidth = int.Parse(txtSpotWidth.Text);
        }
        catch { }
        row.SpotWidth = spotwidth;

        if (rblType.SelectedValue == "Thumbs")
            row.CampaignType = CampaignTypes.Thumbs;
        else
            row.CampaignType = CampaignTypes.List;

        int rows = 0;
        try
        {
            rows = int.Parse(txtThumbsRows.Text);
        }
        catch { }

        row.ThumbsRows = rows;
        int cols = 0;
        try
        {
            cols = int.Parse(txtThumbsColumns.Text);
        }
        catch { }
        row.ThumbsColumns = cols;
        int height = 0;
        try
        {
            height = int.Parse(txtThumbsHeight.Text);
        }
        catch { }
        row.ThumbsHeight = height;
        int width = 0;
        try
        {
            width = int.Parse(txtThumbsWidth.Text);
        }
        catch { }
        row.ThumbsWidth = width;
        int listRows = 0;
        try
        {
            listRows = int.Parse(txtListRowsPerPage.Text);
        }
        catch { }
        row.ListRowsPerPage = listRows;


        row.Active = true;

        row.StartDate = dtcStartDate.SelectedDate;
        row.EndDate = dtcEndDate.SelectedDate;

        Provider.AddUpdateDeleteCampaign(action, ref row, base.LanguageId );

        foreach (ListItem item in chklCategories.Items)
        {
            if ((item.Selected) && (!row.IsConnectedCategory(Convert.ToInt32(item.Value))))
                Provider.ConnectToCategory(row.Id, Convert.ToInt32(item.Value));
            else if ((!item.Selected && (row.IsConnectedCategory(Convert.ToInt32(item.Value)))))
                Provider.DisconnectFromCategory(row.Id, Convert.ToInt32(item.Value));
        }
        foreach (ListItem item in chklLocations.Items)
        {
            if ((item.Selected) && (!row.IsConnectedLocation(Convert.ToInt32(item.Value))))
                Provider.ConnectToLocation(row.Id, Convert.ToInt32(item.Value));
            else if ((!item.Selected) && (row.IsConnectedLocation(Convert.ToInt32(item.Value))))
                Provider.DisconnectFromLocation(row.Id, Convert.ToInt32(item.Value));
        }

        // The news is allways connected to current language
        if (action == Enumerations.Action.Create)
            Provider.ConnectToLanguage(row.Id, base.LanguageId);

        //Crate directory
        if (_campaignId == -1)
        {
            string path = Spinit.Wpc.Base.Business.Globals.CommonFilePath + "\\" 
                            + base.LocationName + "\\Campaign\\" + row.Name + "\\";
            string url = base.LocationName + "/Campaign/";

            DirectoryInfo di = new DirectoryInfo(path);
            if (!di.Exists)
                di.Create();

            Spinit.Wpc.Base.Data.File fle
                            = new Spinit.Wpc.Base.Data.File
                                    (Spinit.Wpc.Base.Business
                                        .Globals.ConnectionString);

            fle.AddFile(url + row.Name,
                         true,
                         null,
                         null,
                         row.Description,
                         base.CurrentUser);
        }

        Response.Redirect(Util.ApplicationPath + Spinit.Wpc.Campaign.Business.Globals.ComponentApplicationPath + "Index.aspx", true);
    }
}
