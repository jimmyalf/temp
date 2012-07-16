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
using System.Text.RegularExpressions;

using Spinit.Wpc.Campaign.Data;
using Spinit.Wpc.Campaign.Business;
using Spinit.Wpc.Utility.Business;
using Spinit.Wpc.Utility.Business.SmartMenu;

public partial class components_Campaign_Index : CampaignPage
{
    private int _pageSize;
    private string _searchString = null;
    private int _selectedCategory = 0;

    protected void Page_Init(object sender, EventArgs e)
    {
        RenderCampaignSubMenu(Page.Master);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        _pageSize = 40;
        if (!Page.IsPostBack)
        {
            PopulateCategories();
            base.SortExpression = "cName";
            base.SortAscending = false;
            pager.PageSize = _pageSize;
            PopulateCampaigns();
        }
    }

    private void PopulateCategories()
    {
        List<CampaignCategoryRow> catList =
            base.Provider.GetAllCategoriesList(0,base.LocationId, base.LanguageId);
        drpCategories.Items.Add(new ListItem("-- Choose Category --", "0"));
        foreach (CampaignCategoryRow row in catList)
        {
            drpCategories.Items.Add(new ListItem(row.Name, row.Id.ToString()));
        }
    }

    public void PopulateCampaigns()
    {
        int totalRecords = 0;
        gvCampaigns.DataSource = base.Provider.GetByPage(
            base.LocationId,
            base.LanguageId,
            _selectedCategory,
            _searchString,
            base.SortExpression + ((base.SortAscending) ? " ASC" : " DESC"),
            pager.PageIndex,
            pager.PageSize,
            ref totalRecords).Tables[SqlProvider._tblCampaign];
        gvCampaigns.DataBind();
        pager.TotalRecords = totalRecords;
        pager.TotalPages = pager.CalculateTotalPages();
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

    protected void gvCampaigns_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
            AddGlyph(gvCampaigns, e.Row);
    }

    private void UpdateColumnHeaders(GridView gv)
    {
        foreach (DataControlField c in gv.Columns)
        {
            c.HeaderText = Regex.Replace(c.HeaderText, "\\s<.*>", String.Empty);
            if (c.SortExpression == base.SortExpression)
            {
                if (base.SortAscending)
                {
                    c.HeaderText += " <img src=\"img/up.gif\" border=\"0\" width=\"11\" height=\"7\">";
                }
                else
                {
                    c.HeaderText += " <img src=\"img/down.gif\" border=\"0\" width=\"11\" height=\"7\">";
                }
            }
        }
    }

    protected override void OnInit(EventArgs e)
    {
        pager.IndexChanged += new EventHandler(PageIndex_Changed);
        pager.IndexButtonChanged += new EventHandler(PageIndexButton_Changed);
        pager.PageSizeChanged += new EventHandler(PageSize_Changed);
        base.OnInit(e);
    }

    protected DataView SortDataTable(DataTable dataTable, bool isPageIndexChanging)
    {
        if (dataTable != null)
        {
            DataView dataView = new DataView(dataTable);
            if (base.SortExpression != string.Empty)
            {
                if (isPageIndexChanging)
                {
                    dataView.Sort = string.Format("{0} {1}", base.SortExpression, base.SortAscending ? "ASC" : "DESC");
                }
                else
                {
                    dataView.Sort = string.Format("{0} {1}", base.SortExpression, base.SortAscending ? "ASC" : "DESC");
                }
            }
            return dataView;
        }
        else
        {
            return new DataView();
        }
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
        itemCollection.AddItem("New", null, "New", "Create new campaign", null, "btnAdd_OnClick", false, null);
        itemCollection.AddItem("Delete", null, "Delete", "Delete selected campaigns", null, "btnDelete_OnClick", false, null);

        subMenu.MenuItems = itemCollection;

        m.CampaignSmartMenu.Render(subMenu, phCampaignSubMenu);
    }

    protected void btnAdd_OnClick(object sender, EventArgs e)
    {
        Response.Redirect(Util.ApplicationPath + Spinit.Wpc.Campaign.Business.Globals.ComponentApplicationPath + "EditCampaign.aspx");
    }

    protected void btnDelete_OnClick(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gvCampaigns.Rows)
        {
            CheckBox chk = (CheckBox)row.FindControl("chkSelect");
            if ((chk != null) && chk.Checked)
            {
                int id = (int)gvCampaigns.DataKeys[row.RowIndex]["cId"];
                CampaignRow campaignToDelete = new CampaignRow();
                campaignToDelete.Id = id;

                Provider.AddUpdateDeleteCampaign(Enumerations.Action.Delete, ref campaignToDelete, base.LanguageId);

                
            }
        }

        PopulateCampaigns();
    }

    protected void btnSetFilter_Click(Object sender, EventArgs e)
    {
        pager.PageIndex = 0;
        _selectedCategory = Convert.ToInt32(drpCategories.SelectedItem.Value);
        PopulateCampaigns();
    }

    protected void btnShowAll_Click(Object sender, EventArgs e)
    {
        pager.PageIndex = 0;
        _selectedCategory = 0;
        drpCategories.SelectedIndex = 0;
        PopulateCampaigns();
    }

    protected void btnSearch_Click(Object sender, EventArgs e)
    {
        _searchString = txtSearch.Text;
        pager.PageIndex = 0;
        PopulateCampaigns();
    }

    protected void chkSelectHeader_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkHeader = (CheckBox)sender;
        if (chkHeader != null)
        {
            foreach (GridViewRow row in gvCampaigns.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkSelect");
                if (chk != null)
                {
                    chk.Checked = chkHeader.Checked;
                }
            }
        }
    }

    #region GridView Events


    protected void gvCampaigns_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //        gvNews.DataSource = SortDataTable(gvNews.DataSource as DataTable, true);
        gvCampaigns.PageIndex = e.NewPageIndex;
        DataBind();
    }

    protected void gvCampaigns_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (e.SortExpression == base.SortExpression)
            base.SortAscending = !base.SortAscending;
        else
            base.SortAscending = true;
        base.SortExpression = e.SortExpression;
        PopulateCampaigns();
    }

    protected void gvCampaigns_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        int campaignId = (int)gvCampaigns.DataKeys[e.RowIndex].Value;
        CampaignRow campaignToDelete = new CampaignRow();
        campaignToDelete.Id = campaignId;

        Provider.AddUpdateDeleteCampaign(Enumerations.Action.Delete, ref campaignToDelete, base.LanguageId);

        PopulateCampaigns();
    }

    protected void gvCampaigns_Editing(object sender, GridViewEditEventArgs e)
    {
        int index = e.NewEditIndex;
        int campaignId = (int)gvCampaigns.DataKeys[index].Value;
        if (!base.IsInRole(CampaignRoles.Roles.Edit))
        {
            Response.Redirect(Util.ApplicationPath + Spinit.Wpc.Base.Business.Globals.ComponentApplicationPath + "NoAccess.aspx");
        }
        else
        {
            Response.Redirect(Util.ApplicationPath + Spinit.Wpc.Campaign.Business.Globals.ComponentApplicationPath + "/EditCampaign.aspx" + "?id=" + campaignId);
        }
    }


    protected void gvCampaigns_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = -1;
        int campaignId = -1;

        switch (e.CommandName)
        {
            case "Edit":
                break;
            case "AddFiles":
                index = Convert.ToInt32(e.CommandArgument);
                campaignId = (int)gvCampaigns.DataKeys[index].Value;
                Response.Redirect("AddMedia.aspx?id=" + campaignId);
                break;
            case "Files":
                index = Convert.ToInt32(e.CommandArgument);
                campaignId = (int)gvCampaigns.DataKeys[index].Value;
                Response.Redirect("Files.aspx?id=" + campaignId);
                break;
        }

    }

    protected void gvCampaigns_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridViewRow row = e.Row;
        if (row.RowType == DataControlRowType.DataRow)
        {
           
            // Retrieve the Button control from the fifth column.
            Button addMediaButton = (Button)e.Row.Cells[6].Controls[0];

            // Set the Button's CommandArgument property with the
            // row's index.
            addMediaButton.CommandArgument = e.Row.RowIndex.ToString();
            

            int campaignId = (int)gvCampaigns.DataKeys[row.RowIndex].Value;
            if (campaignId > 0)
            {
               

                Spinit.Wpc.Campaign.Data.CampaignRow campaignRow = Provider.GetCampaign(campaignId, base.LocationId, base.LanguageId);
                Label lblStatus = (Label)gvCampaigns.FindControl("lblStatus");
                lblStatus = (Label)e.Row.FindControl("lblStatus");
                string statusText = String.Empty;
                if (lblStatus != null)
                {
                    //switch (memberRow.Status)
                    //{
                    //    case Spinit.Wpc.Member.Data.Enumerations.MemberStatus.Unknown:
                    //        statusText = "Unknown";
                    //        break;
                    //    case Spinit.Wpc.Member.Data.Enumerations.MemberStatus.Active:
                    //        statusText = "Active";
                    //        break;
                    //    case Spinit.Wpc.Member.Data.Enumerations.MemberStatus.InActive:
                    //        statusText = "Inactive";
                    //        break;
                    //    case Spinit.Wpc.Member.Data.Enumerations.MemberStatus.LockedBy:
                    //        statusText = "Locked by " + memberRow.LockedBy;
                    //        break;
                    //    case Spinit.Wpc.Member.Data.Enumerations.MemberStatus.AwaitingApproval:
                    //        statusText = "Awaiting approval";
                    //        break;
                    //}
                    lblStatus.Text = statusText;
                }
            }
        }
    }


    /// <summary>
    /// Add delete confirmation
    /// </summary>
    /// <param name="sender">The sending object.</param>
    /// <param name="e">The event arguments.</param>

    protected void AddConfirmDelete(object sender, EventArgs e)
    {
        ClientConfirmation cc = new ClientConfirmation();
        cc.AddConfirmation(ref sender, "Do you really wan\\'t to delete the campaign?");
    }

    #endregion

    #region Pager Events

    private void PageIndex_Changed(Object sender, EventArgs e)
    {
        PopulateCampaigns();
    }

    private void PageIndexButton_Changed(Object sender, EventArgs e)
    {
        PopulateCampaigns();
    }

    private void PageSize_Changed(Object sender, EventArgs e)
    {
        PopulateCampaigns();
    }

    #endregion
}
