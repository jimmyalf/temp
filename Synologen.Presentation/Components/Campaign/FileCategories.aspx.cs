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

public partial class components_Campaign_FileCategories : CampaignPage
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
        FileCategoryRow fileCatRow = Provider.GetFileCategory(categoryId);
        txtName.Text = fileCatRow.Name;
        chkMustOrder.Checked = fileCatRow.MustOrder;
    }

    private void PopulateCategory()
    {
        DataSet ds = base.Provider.GetFileCategories();
        gvFileCategory.DataSource = ds;
        gvFileCategory.DataBind();
        lblHeader.Text = "Add File Category";
        btnSave.Text = "Save";
    }

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

    protected void gvFileCategory_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }

    protected void gvFileCategory_Editing(object sender, GridViewEditEventArgs e)
    {
        int index = e.NewEditIndex;
        int catId = (int)gvFileCategory.DataKeys[index].Value;
        if (!base.IsInRole(CampaignRoles.Roles.Edit))
        {
            Response.Redirect(Util.ApplicationPath + Spinit.Wpc.Base.Business.Globals.ComponentApplicationPath + "NoAccess.aspx");
        }
        else
        {
            Response.Redirect(Util.ApplicationPath + Spinit.Wpc.Campaign.Business.Globals.ComponentApplicationPath + "FileCategories.aspx?id=" + catId, true);
        }
    }

    protected void gvFileCategory_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        int index = e.RowIndex;
        int catId = (int)gvFileCategory.DataKeys[index].Value;
        if (!base.IsInRole(CampaignRoles.Roles.Delete))
        {
            Response.Redirect(Util.ApplicationPath + Spinit.Wpc.Base.Business.Globals.ComponentApplicationPath + "NoAccess.aspx");
        }
        else
        {
            FileCategoryRow fileCatRow = Provider.GetFileCategory(catId);
            Provider.AddUpdateDeleteFileCategory(Enumerations.Action.Delete, fileCatRow);
            Response.Redirect(Util.ApplicationPath + Spinit.Wpc.Campaign.Business.Globals.ComponentApplicationPath + "FileCategories.aspx");
        }
    }

    protected void gvFileCategory_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        FileCategoryRow fileCatRow = new FileCategoryRow();
        Enumerations.Action action = Enumerations.Action.Create;
        if (_categoryId > 0)
        {
            fileCatRow = Provider.GetFileCategory(_categoryId);
            action = Enumerations.Action.Update;
        }
        fileCatRow.Name = txtName.Text;
        fileCatRow.MustOrder = chkMustOrder.Checked;
        if (!base.IsInRole(CampaignRoles.Roles.Create))
        {
            Response.Redirect(Util.ApplicationPath + Spinit.Wpc.Base.Business.Globals.ComponentApplicationPath + "NoAccess.aspx");
        }
        else
        {
            Provider.AddUpdateDeleteFileCategory(action, fileCatRow);
            Response.Redirect(Util.ApplicationPath + Spinit.Wpc.Campaign.Business.Globals.ComponentApplicationPath + "FileCategories.aspx");
        }
    }


}
