using System;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.Routing;
using System.Web.SessionState;
using System.Configuration;
using System.Web.Security;
using System.Security.Principal;

using Spinit.Wpc.Base.Business;
using Spinit.Wpc.Base.Data;
using Spinit.Wpc.Content.Business;
using Spinit.Wpc.Utility.Business;
using Spinit.Wpc.Utility.Core;
using Spinit.Wpc.Utility.Data;
using System.Web.Mvc;

namespace Spinit.Wpc.Synologen.Presentation
{
	/// <summary>
	/// The Asp .NET Global class. Called from system.
	/// </summary>
	
	public class SynologenApplication : System.Web.HttpApplication
	{
		/// <summary>
		/// Default constructor.
		/// </summary>

		public SynologenApplication ()
		{
		}	

		/// <summary>
		/// Fires when the application starts.
		/// </summary>
		/// <param name="sender">The sending object.</param>
		/// <param name="e">The event-arguments.</param>
		
		protected void Application_Start (Object sender,
		                                  EventArgs e)
		{
			try {
				Hashtable usertable = new Hashtable ();
				Application.Set ("activeUsers", usertable);

				ThreadHandler.HandleThreads (
					true, 
					this);
			}
			catch (Exception ex) {
				try {
					Utility.Data.Log lg = new Utility.Data.Log(Spinit.Wpc.Base
					                                           	.Business.Globals.ConnectionString);
					lg.AddLogAdmin (Log.LOG_TYPE.EMERGENCY,
					                0,
					                0,
					                "Application start.",
					                ex.Message,
					                null,
					                null,
					                "Base globals.",
					                null);
				}
				catch (Exception) { }
			}
			
			/* =NOTE: MVC Bootstrapping */
			AreaRegistration.RegisterAllAreas();
			RegisterRoutes(RouteTable.Routes);
		}

		private void RegisterRoutes(RouteCollection routes)
		{
		}

		/// <summary>
		/// Fires when the session starts.
		/// 
		/// GlobalContentPage GlobalContentTree GlobalBaseFile
		/// </summary>
		/// <param name="sender">The sending object.</param>
		/// <param name="e">The event-arguments.</param>

		protected void Session_Start (Object sender,
		                              EventArgs e)
		{
			Context context = new Context ();
			context.Culture = Spinit.Wpc.Base.Business.Globals.DefaultLanguage;
			Base.Presentation.SessionContext.UserContext.Current = context;

			if (HttpContext.Current.User.Identity.IsAuthenticated) {
				try {
					CxUser wpcContext = CxUser.Current;
					Base.Presentation.SessionContext.UserContext.Current.CxUser = CxUser.Current;
				}
				catch (ContextException)
				{
					User user
						= new User(Spinit.Wpc.Base.Business
						           	.Globals.ConnectionString);
					Base.Data.UserRow userRow
						= user.GetUser(HttpContext.Current.User.Identity.Name);
					CxUser cxUser = Base.Business.Users.LoadAndStoreCxUser
						(Application,
						 userRow.Id,
						 new Content.Data.Tree
						 	(Spinit.Wpc.Base.Business
						 	 	.Globals.ConnectionString),
						 new Content.Data.Page
						 	(Spinit.Wpc.Base.Business
						 	 	.Globals.ConnectionString));
					
					Base.Presentation.SessionContext.UserContext.Current.CxUser = cxUser;
					Base.Presentation.SessionContext.UserContext.Current.Culture
						= cxUser.Language.Resource;
				}
			}
		}

		/// <summary>
		/// Fires at the begin-request.
		/// Also used by WpcMainModule.
		/// </summary>
		/// <param name="sender">The sending object.</param>
		/// <param name="e">The event-arguments.</param>

		protected void Application_BeginRequest (Object sender, EventArgs e)
		{
			//Context.User = CxUser.Current;
			//try
			//{
			//    if (Application["ContextUsers"] != null)
			//    {
			//        Hashtable ctUsers
			//            = (Hashtable)Application["ContextUsers"];
			//        if (ctUsers.ContainsKey(Context.User.Identity.Name + Session.SessionID))
			//        {
			//            Context.User = (CxUser)ctUsers[Context.User.Identity.Name + Session.SessionID];
			//        }
			//    }
			//}
			//catch (Exception ex) { 
			//    string test = "s"; 
			//}
		}

		/// <summary>
		/// Fires at the end-request.
		/// Also used by WpcMainModule.
		/// </summary>
		/// <param name="sender">The sending object.</param>
		/// <param name="e">The event-arguments.</param>

		protected void Application_EndRequest (Object sender,
		                                       EventArgs e)
		{

		}

		/// <summary>
		/// Fires at the authenticate-request.
		/// Also used by WpcMainModule.
		/// </summary>
		/// <param name="sender">The sending object.</param>
		/// <param name="e">The event-arguments.</param>

		protected void Application_AuthenticateRequest (Object sender,
		                                                EventArgs e)
		{
			if (HttpContext.Current.User != null)
			{
				if (HttpContext.Current.User.Identity.IsAuthenticated)
				{
					if (HttpContext.Current.User.Identity is FormsIdentity)
					{
						FormsIdentity id =
							(FormsIdentity) HttpContext.Current.User.Identity;
						FormsAuthenticationTicket ticket = id.Ticket;

						// Get the stored user-data, in this case, our roles
						string userData = ticket.UserData;
						string[] roles = userData.Split(';');
						HttpContext.Current.User = new GenericPrincipal(id, roles);

					}
				}
			}
		}

		/// <summary>
		/// The on-error event. Fetches all application-errors and saves them 
		/// to log-table.
		/// Also used in WpcMainModule.
		/// </summary>
		/// <param name="sender">The sending object.</param>
		/// <param name="e">The evetn-arguments.</param>

		protected void Application_Error (Object sender, 
		                                  EventArgs e)
		{
			try {
				string user = null;
				int location = 0;

				if (Application ["ContextUsers"] != null) {
					Hashtable ctUsers
						= (Hashtable) Application ["ContextUsers"];
					if (ctUsers.ContainsKey (Session.SessionID)) {
						CxUser usr = (CxUser) ctUsers [Session.SessionID];

						user = usr.User.UserName;
						location = usr.Location.Id;
					}
				}

				Log lg = new Log (Spinit.Wpc.Base.Business
				                  	.Globals.ConnectionString);

				Utility.Business.WpcExceptionType exceptionType;
				Exception exp = Utility.Business.Util.GetExceptionType(Context.Error, out exceptionType);
				switch (exceptionType)
				{
					case WpcExceptionType.Context:
						ContextException contextExp = (ContextException)exp;
						Response.Redirect("~/Logout.aspx", true);
						break;
					case WpcExceptionType.Security:
						SecurityException securityExp = (SecurityException)exp;
						int component = 0;

						if (securityExp.Component != null)
						{
							Spinit.Wpc.Base.Data.Component cmp
								= new Spinit.Wpc.Base.Data.Component
									(Spinit.Wpc.Base.Business
									 	.Globals.ConnectionString);

							ComponentRow cmpRow
								= (ComponentRow)cmp.GetComponent(securityExp.Component);

							if (cmpRow != null)
							{
								component = cmpRow.Id;
							}
						}
						lg.AddLogAdmin (Log.LOG_TYPE.SECURITY,
						                location,
						                component,
						                securityExp.Page,
						                securityExp.Message,
						                Request.UserHostAddress,
						                Request.UserAgent,
						                Request.UrlReferrer.AbsoluteUri,
						                user);
						Response.Redirect("~/NoAccess.aspx?page=" + securityExp.Page, true);
						break;
					default:
						string exception = String.Empty;
						string message = String.Empty;

						Exception ex = Server.GetLastError().GetBaseException();
						int logId = -1;
						if ((ex != null) && (ex.Message != null) && (ex.TargetSite != null) && (ex.StackTrace != null))
						{
							message = ex.Message +
							          "\r\nSOURCE: " + ex.Source +
							          "\r\nTARGETSITE: " + ex.TargetSite +
							          "\r\nSTACKTRACE: " + ex.StackTrace;

							logId = lg.AddLogAdmin(Log.LOG_TYPE.ERROR,
							                       location,
							                       0,
							                       String.Empty,
							                       message,
							                       Request.UserHostAddress,
							                       Request.UserAgent,
							                       Request.UrlReferrer.AbsoluteUri,
							                       user);
						}
						// TODO: Pass the logid instead
						Response.Redirect("~/Error.aspx?id=" + logId);
						break;
				}
			}
			catch (Exception) { }
		}

		/// <summary>
		/// Fires when the session ends.
		/// </summary>
		/// <param name="sender">The sending object.</param>
		/// <param name="e">The event-arguments.</param>

		protected void Session_End (Object sender,
		                            EventArgs e)
		{
			try {
				if (Application ["ContextUsers"] != null) {
					Hashtable ctUsers
						= (Hashtable) Application ["ContextUsers"];
					if (ctUsers.ContainsKey (Session.SessionID))
						ctUsers.Remove (Session.SessionID);						
					Application.Set ("ContextUsers", ctUsers);
				}

				if (Application ["activeUsers"] != null) {
					Hashtable usertable 
						= (Hashtable) Application ["activeUsers"];
					usertable.Remove (Session.SessionID);
					Application.Set ("activeUsers", usertable);
				}
			}
			catch (Exception ex) {
				try {
					Log lg = new Log (Spinit.Wpc.Base
					                  	.Business.Globals.ConnectionString);
					lg.AddLogAdmin (Log.LOG_TYPE.EMERGENCY,
					                0,
					                0,
					                "Session end.",
					                ex.Message,
					                null,
					                null,
					                "Base globals.",
					                null);
				}
				catch (Exception) { }
			}
		}

		/// <summary>
		/// Fires when the application ends.
		/// </summary>
		/// <param name="sender">The sending object.</param>
		/// <param name="e">The event-arguments.</param>
		
		protected void Application_End (Object sender, 
		                                EventArgs e)
		{
			try {
				ThreadHandler.ClearThreads ();
			}
			catch (Exception ex) {
				try {
					Log lg = new Log (Spinit.Wpc.Base
					                  	.Business.Globals.ConnectionString);
					lg.AddLogAdmin (Log.LOG_TYPE.EMERGENCY,
					                0,
					                0,
					                "Application start.",
					                ex.Message,
					                null,
					                null,
					                "Base globals.",
					                null);
				}
				catch (Exception) { }
			}
		}			
	}
}