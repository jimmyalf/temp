using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Spinit.Wpc.Utility.Business;
using Spinit.Wpc.Utility.Core;


namespace Spinit.Wpc.Synologen.OPQ.Site.Code
{
	/// <summary>
	/// The user-context
	/// </summary>

	[Serializable]
	public class SessionContext : UserContext
	{
		/// <summary>
		/// The user-context key.
		/// </summary>

		const string SessionKeyCurrentOpq = "Synologen-Opq";
		
		/// <summary>
		/// Gets or sets the current commerce-context.
		/// </summary>

		public static Core.Context CurrentOpq
		{
			get {
				string key = sessionKey_ComponentBase + SessionKeyCurrentOpq;

				if (HttpContext.Current.Session [key] != null) {
					return (Core.Context) HttpContext.Current.Session [key];
				}
				if (HttpContext.Current.Session [sessionKey_Current] == null)
				{
					const string errorMesage = "No Spinit.Wpc.Utitlity.Core.Context found in Session.";
					throw new NullReferenceException (errorMesage);
				}
				var cnt = (Context) HttpContext.Current.Session [sessionKey_Current];

				var cCnt = new Core.Context(
					cnt.Culture,
					cnt.Debug,
					cnt.CxUser,
					cnt.PublicUser);

				return cCnt;
			}

			set {
				string key = sessionKey_ComponentBase + SessionKeyCurrentOpq;
				
				HttpContext.Current.Session [key] = value;
			}
		}
	}
}