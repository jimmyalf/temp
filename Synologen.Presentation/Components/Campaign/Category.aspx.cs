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

using Spinit.Wpc.Utility.Business;
using Spinit.Wpc.Campaign.Data;
using Spinit.Wpc.Campaign.Business;

public partial class components_Campaign_Category : CampaignPage
{
    private int _categoryId = -1;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["id"] != null)
            _categoryId = Convert.ToInt32(Request.Params["id"]);
        if (!Page.IsPostBack)
        {
            PopulateCategory();
            if (_categoryId > 0)
                SetupForEdit(_categoryId);
        }
    }

    private void SetupForEdit(int categoryId)
    {
        lblHeader.Text = "Edit Category";
        btnSave.Text = "Change";
        CampaignCategoryRow catRow = Provider.GetCategory(categoryId, base.LanguageId, base.DefaultLanguageId);
        txtName.Text = catRow.Name;
    }

    private void PopulateCategory()
    {
        DataSet ds = base.Provider.GetCategories(base.LocationId, base.LanguageId);
        gvCategory.DataSource = ds.Tables[0];
        gvCategory.DataBind();
        lblHeader.Text = "Add Category";
        btnSave.Text = "Save";
    }

    #region Category Events

    /// <summary>
    /// Add delete confirmation
    /// </summary>
    /// <param name="sender">The sending object.</param>
    /// <param name="e">The event arguments.</param>

    protected void btnDelete_AddConfirmDelete(object sender, EventArgs e)
    {
        ClientConfirmation cc = new ClientConfirmation();
        cc.AddConfirmation(ref sender, "Do you really wan\\'t to delete the category?");
    }

    protected void gvCategory_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }

    protected void gvCategory_Editing(object sender, GridViewEditEventArgs e)
    {
        int index = e.NewEditIndex;
        int catId = (int)gvCategory.DataKeys[index].Value;
        if (!base.IsInRole(CampaignRoles.Roles.Edit))
        {
            Response.Redirect(Util.ApplicationPath + Spinit.Wpc.Base.Business.Globals.ComponentApplicationPath + "NoAccess.aspx");
        }
        else
        {
            Response.Redirect(Util.ApplicationPath + Spinit.Wpc.Campaign.Business.Globals.ComponentApplicationPath + "Category.aspx?id=" + catId, true);
        }
    }

    protected void gvCategory_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        int index = e.RowIndex;
        int catId = (int)gvCategory.DataKeys[index].Value;
        if (!base.IsInRole(CampaignRoles.Roles.Delete))
        {
            Response.Redirect(Util.ApplicationPath + Spinit.Wpc.Base.Business.Globals.ComponentApplicationPath + "NoAccess.aspx");
        }
        else
        {
            CampaignCategoryRow catRow = Provider.GetCategory(catId, base.LocationId, base.LanguageId);
            Provider.AddUpdateDeleteCategory(Spinit.Wpc.Utility.Business.Enumerations.Action.Delete, 
                                            ref catRow, 
                                            base.LanguageId);
            Response.Redirect(Util.ApplicationPath + Spinit.Wpc.Campaign.Business.Globals.ComponentApplicationPath + "Category.aspx");
        }
    }

    protected void gvCategory_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        CampaignCategoryRow catRow = new CampaignCategoryRow();
        Spinit.Wpc.Utility.Business.Enumerations.Action action 
            = Spinit.Wpc.Utility.Business.Enumerations.Action.Create;
        if (_categoryId > 0)
        {
            catRow = Provider.GetCategory(_categoryId, base.LanguageId, base.DefaultLanguageId);
            action = Enumerations.Action.Update;
        }
        catRow.Name = txtName.Text;
        if (!base.IsInRole(CampaignRoles.Roles.Create))
        {
            Response.Redirect(Util.ApplicationPath + Spinit.Wpc.Base.Business.Globals.ComponentApplicationPath + "NoAccess.aspx");
        }
        else
        {
            Provider.AddUpdateDeleteCategory(action, ref catRow, base.LanguageId);
            Response.Redirect(Util.ApplicationPath + Spinit.Wpc.Campaign.Business.Globals.ComponentApplicationPath + "Category.aspx");
        }
    }


    #endregion

}
