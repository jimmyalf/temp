using System;
using System.Text;
using Spinit.Wpc.Member.Data;

namespace Spinit.Wpc.Synologen.Presentation.Code {
	/// <summary>
	/// Summary description for MemberControlPage
	/// </summary>
	public partial class SynologenControlPage : System.Web.UI.UserControl {
		private ApplicationSettingsRow _settings = null;
		private string _connectionString = Framework.GetConnectionString();
		private int _locationId = Framework.GetLocationId();
		private int _languageId = Framework.GetLanguageId();


		protected void MemeberControlPage_Load(object sender, EventArgs e) {
		}

		protected void GiveFocus(System.Web.UI.Control c) {
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

		protected void DisplayAlert(string message) {
			Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), Guid.NewGuid().ToString(), "<script language=\"JavaScript\">" +
			                                                                                       GetAlertScript(message) +
			                                                                                       "</script>");
		}

		protected string GetAlertScript(string message) {
			message = message.Replace("'", "\'");
			message = message.Replace("\n", "\\n");
			return "alert('" + message + "');";
		}

		protected bool SortAscending {
			get {
				object o = ViewState["SortAscending"];
				if (o == null)
					return true;
				else
					return Convert.ToBoolean(o);
			}
			set { ViewState["SortAscending"] = value; }
		}

		protected string SortExpression {
			get {
				object o = ViewState["SortExpression"];
				if (o == null)
					return string.Empty;
				else
					return o.ToString();
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
			get { return _locationId; }
		}

		protected int LanguageId {
			get { return _languageId; }
		}

		protected override void OnInit(EventArgs e) {
			base.OnInit(e);
			Load += new EventHandler(MemeberControlPage_Load);
			MemeberControlPageInit();
		}

		private void MemeberControlPageInit() {
			if (Cache["MemeberSettings"] != null)
				_settings = (ApplicationSettingsRow)Cache["MemeberSettings"];
			else {
				//TODO Read from db
				_settings = new ApplicationSettingsRow();
				Cache["MemeberSettings"] = _settings;
			}
		}
	}
}