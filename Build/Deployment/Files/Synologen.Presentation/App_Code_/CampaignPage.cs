using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Text;

using Spinit.Wpc.Campaign.Data;
using Spinit.Wpc.Campaign.Business;
using Spinit.Wpc.Utility.Business;
using Spinit.Wpc.Utility.Core;

/// <summary>
/// Summary description for CampaignPage
/// </summary>
public class CampaignPage : System.Web.UI.Page
{
    private ApplicationSettingsRow _settings = null;
    private string _connectionString = null;
    private IBaseLocationRow _defaultLocation = null;
    private IBaseLanguageRow _defaultLanguage = null;
    private IBaseLocationRow _location = null;
    private IBaseLanguageRow _language = null;
    private List<IBaseLocationRow> _locations = new List<IBaseLocationRow>();
    private List<IBaseLanguageRow> _languages = new List<IBaseLanguageRow>();
    private SqlProvider _provider = null;
    private int _userId = -1;
    private string _user = String.Empty;

    protected void CampaignPage_Load(object sender, EventArgs e)
    {
    }

    protected void GiveFocus(System.Web.UI.Control c)
    {
        StringBuilder script = new StringBuilder();
        script.Append("<script language=\"JavaScript\">");
        script.Append(Environment.NewLine);
        script.Append("function CSP_focus(id) { var o = document.getElementById(id); if (o != null) o.focus(); }");
        script.Append(Environment.NewLine);
        script.Append("</script>");
        Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "CSP-focus-function", script.ToString());
        Page.ClientScript.RegisterStartupScript(Page.GetType(), "CSP-focus",
            "<script language=\"JavaScript\">CSP_focus('" + c.ClientID + "');</script>");
    }

    protected void DisplayAlert(string message)
    {
        Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), Guid.NewGuid().ToString(), "<script language=\"JavaScript\">" +
            GetAlertScript(message) +
            "</script>");
    }

    protected string GetAlertScript(string message)
    {
        message = message.Replace("'", "\'");
        message = message.Replace("\n", "\\n");
        return "alert('" + message + "');";
    }

    protected bool SortAscending
    {
        get
        {
            object o = ViewState["SortAscending"];
            if (o == null)
                return true;
            else
                return Convert.ToBoolean(o);
        }
        set { ViewState["SortAscending"] = value; }
    }

    protected SqlProvider Provider
    {
        get { return _provider; }
    }

    protected string SortExpression
    {
        get
        {
            object o = ViewState["SortExpression"];
            if (o == null)
                return string.Empty;
            else
                return o.ToString();
        }
        set { ViewState["SortExpression"] = value; }
    }

    protected ApplicationSettingsRow Settings
    {
        get { return _settings; }
    }

    protected string ConnectionString
    {
        get { return _connectionString; }
    }

    protected int LocationId
    {
        get { return _location.Id; }
    }

    protected string LocationName
    {
        get { return _location.Name; }
    }

    protected int LanguageId
    {
        get { return _language.Id; }
    }

    protected int UserId
    {
        get { return _userId; }
    }

    protected List<IBaseLocationRow> Locations
    {
        get { return _locations; }
    }

    protected List<IBaseLanguageRow> Languages
    {
        get { return _languages; }
    }

    protected int DefaultLocationId
    {
        get { return _defaultLocation.Id; }
    }

    protected string CurrentUser
    {
        get { return _user; }
    }

    protected int DefaultLanguageId
    {
        get
        {
            if (_defaultLanguage != null)
                return _defaultLanguage.Id;
            else
                return -1;
        }
    }

    protected bool IsInRole(CampaignRoles.Roles role)
    {
        string sRole = role.ToString();
        bool access = true;
        try
        {
			//TODO: Fix made straight into code (not checked in)
            //return access = ((CxUser)Context.User).IsInRole(Globals.ComponentName, sRole);
			return access = CxUser.Current.IsInRole(Spinit.Wpc.Campaign.Business.Globals.ComponentName, sRole);
        }
        catch (InvalidCastException)
        {
            // access = Context.User.IsInRole(sRole);
        }
        return access;
    }

    protected override void OnInit(EventArgs e)
    {
        Load += new EventHandler(CampaignPage_Load);
        base.OnInit(e);
        CampaignPageInit();
    }

    private void CampaignPageInit()
    {
        _connectionString = ConfigurationManager.ConnectionStrings["WpcServer"].ConnectionString;
        if (Cache["CampaignSettings"] != null)
            _settings = (ApplicationSettingsRow)Cache["CampaignSettings"];
        else
        {
            //TODO Read from db
            _settings = new ApplicationSettingsRow();
            Cache["CampaignSettings"] = _settings;
        }
        try
        {
            CxUser wpcContext = CxUser.Current;
            _location = wpcContext.Location;
            _language = wpcContext.Language;
            _locations = wpcContext.Locations;
            _languages = wpcContext.Languages;
            _userId = wpcContext.User.Id;
            _defaultLanguage = wpcContext.DefaultLanguage;
            _defaultLocation = wpcContext.DefaultLocation;
            _user = wpcContext.User.UserName;
        }
        catch
        {
            _user = Context.User.Identity.Name;
        }
        _provider = new Spinit.Wpc.Campaign.Data.SqlProvider(_connectionString);
    }
}
