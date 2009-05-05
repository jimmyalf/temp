using System;
using System.Web.Security;
using Spinit.Wpc.Synologen.Business;
using Spinit.Wpc.Synologen.Presentation.Site.Code;

namespace Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen {
	public partial class SecurityLogout : System.Web.UI.UserControl {

		protected void Page_Load(object sender, EventArgs e) {
			PerformAuthenticationCheck();
		}

		private void PerformAuthenticationCheck() {
			if (SynologenSessionContext.SecurityIsValidUntil >= DateTime.Now) return;
			SetSecurityIsValidTimeout();
			string thisUrl = Request.Url.PathAndQuery;
			FormsAuthentication.SignOut();
			Response.Redirect(thisUrl);
		}

		private static void SetSecurityIsValidTimeout() {
			TimeSpan validTimeout = Globals.SecurityLogoutTimeout;
			SynologenSessionContext.SecurityIsValidUntil = DateTime.Now.Add(validTimeout);
		}
	}
}