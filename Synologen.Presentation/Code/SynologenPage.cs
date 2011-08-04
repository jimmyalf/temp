using System;
using System.Configuration;
using System.Text;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Spinit.Wpc.Member.Data;
using Spinit.Wpc.Member.Business;
using Spinit.Wpc.Synologen.Business;
using Spinit.Wpc.Synologen.Presentation.Application.Services;
using Spinit.Wpc.Utility.Core;


namespace Spinit.Wpc.Synologen.Presentation.Code 
{
	/// <summary>
	/// Summary description for MemeberPage
	/// </summary>
	public class SynologenPage : System.Web.UI.Page 
	{
		private ApplicationSettingsRow _settings;
		private string _connectionString;
		private IBaseLocationRow _defaultLocation;
		private IBaseLanguageRow _defaultLanguage;
		private IBaseLocationRow _location;
		private IBaseLanguageRow _language;
		private List<IBaseLocationRow> _locations = new List<IBaseLocationRow>();
		private List<IBaseLanguageRow> _languages = new List<IBaseLanguageRow>();
		private Data.SqlProvider _provider;
		private int _userId = -1;
		private string _user = String.Empty;
		protected CustomValidator CustomValidation = new CustomValidator();

		protected override void OnInit(EventArgs e) {
			Load += MemberPage_Load;
			base.OnInit(e);
			MemberPageInit();
		}

		private void MemberPageInit() {
			_connectionString = ConfigurationManager.ConnectionStrings["WpcServer"].ConnectionString;
			if (Cache["MemberSettings"] != null)
				_settings = (ApplicationSettingsRow)Cache["MemberSettings"];
			else {
				//TODO Read from db
				_settings = new ApplicationSettingsRow();
				Cache["MemberSettings"] = _settings;
			}
			try {
				var wpcContext = CxUser.Current;
				_location = wpcContext.Location;
				_language = wpcContext.Language;
				_locations = wpcContext.Locations;
				_languages = wpcContext.Languages;
				_userId = wpcContext.User.Id;
				_defaultLanguage = wpcContext.DefaultLanguage;
				_defaultLocation = wpcContext.DefaultLocation;
				_user = wpcContext.User.UserName;
			}
			catch {
				_user = Context.User.Identity.Name;
			}
			_provider = new Data.SqlProvider(_connectionString);
		}

		protected void MemberPage_Load(object sender, EventArgs e) 
		{
			InitCustomValidationMessage();
		}

		private void InitCustomValidationMessage() 
		{
			CustomValidation.EnableViewState = false;
			CustomValidation.Display = ValidatorDisplay.None;
			Form.Controls.Add(CustomValidation);
			if(MessageQueue.HasMessages)
			{
				var message = MessageQueue.ReadMessage();
				DisplayMessage(message.Text, message.IsError);
			}
		}

		public void DisplayMessage(string message, bool isError) 
		{
			CustomValidation.ValidationGroup = isError ? "Error" : "Success";
			CustomValidation.ErrorMessage = message;
			CustomValidation.IsValid = false;
		}

		protected void GiveFocus(System.Web.UI.Control c) 
		{
			var script = new StringBuilder();
			script.Append("<script language=\"JavaScript\">");
			script.Append(Environment.NewLine);
			script.Append("function CSP_focus(id) { var o = document.getElementById(id); if (o != null) o.focus(); }");
			script.Append(Environment.NewLine);
			script.Append("</script>");
			Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "CSP-focus-function", script.ToString());
			Page.ClientScript.RegisterStartupScript(Page.GetType(), "CSP-focus", "<script language=\"JavaScript\">CSP_focus('" + c.ClientID + "');</script>");
		}

		protected void DisplayAlert(string message) 
		{
			Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), Guid.NewGuid().ToString(), "<script language=\"JavaScript\">" + GetAlertScript(message) + "</script>");
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
				var o = ViewState["SortAscending"];
				return o == null || Convert.ToBoolean(o);
			}
			set { ViewState["SortAscending"] = value; }
		}

		protected Data.SqlProvider Provider 
		{
			get { return _provider; }
		}

		protected string SortExpression 
		{
			get
			{
				var o = ViewState["SortExpression"];
				return o == null ? string.Empty : o.ToString();
			}
			set { ViewState["SortExpression"] = value; }
		}

		protected ApplicationSettingsRow Settings {
			get { return _settings; }
		}

		protected string ConnectionString {
			get { return _connectionString; }
		}

		protected int LocationId {
			get { return _location.Id; }
		}

		protected string LocationName {
			get { return _location.Name; }
		}

		protected int LanguageId {
			get { return _language.Id; }
		}

		protected int UserId {
			get { return _userId; }
		}

		protected List<IBaseLocationRow> Locations {
			get { return _locations; }
		}

		protected List<IBaseLanguageRow> Languages {
			get { return _languages; }
		}

		protected int DefaultLocationId {
			get { return _defaultLocation.Id; }
		}

		protected string CurrentUser {
			get { return _user; }
		}

		protected int DefaultLanguageId {
			get {
				if (_defaultLanguage != null)
					return _defaultLanguage.Id;
				else
					return -1;
			}
		}

		protected bool IsInRole(MemberRoles.Roles role) {
			var sRole = role.ToString();
			var access = true;
			var memberComponentName = Member.Business.Globals.ComponentName;
			try {
				return access = ((CxUser)Context.User).IsInRole(memberComponentName, sRole);
			}
			catch (InvalidCastException) {
				// access = Context.User.IsInRole(sRole);
			}
			return access;
		}

		protected bool IsInSynologenRole(SynologenRoles.Roles role) {
			var sRole = role.ToString();
			var access = true;
			var synologenComponentName = Business.Globals.ComponentName;
			try {
				return access = ((CxUser)Context.User).IsInRole(synologenComponentName, sRole);
			}
			catch (InvalidCastException) {
				// access = Context.User.IsInRole(sRole);
			}
			return access;
		}
	}
}