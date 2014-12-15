//------------------------------------------------------------------------------
// <copyright company="Telligent Systems, Inc.">
//	Copyright (c) Telligent Systems, Inc.  All rights reserved. Please visit
//	http://www.communityserver.org for more details.
// </copyright>                                                                
//------------------------------------------------------------------------------

using System;
using System.Web; 
using System.Web.Security;
using System.Collections;
using System.Threading;
using System.IO;
using Spinit.Wpc.Forum.Components;
using Spinit.Wpc.Forum.Configuration;
using Spinit.Wpc.Forum.Enumerations;
using System.Text.RegularExpressions;
using System.Security.Principal;

namespace Spinit.Wpc.Forum {

    // *********************************************************************
    //  ForumsHttpModule
    //
    /// <summary>
    /// This HttpModule encapsulates all the forums related events that occur 
    /// during ASP.NET application start-up, errors, and end request.
    /// </summary>
    // ***********************************************************************/
    public class ForumsHttpModule : IHttpModule {
        #region Member variables and inherited properties / methods
        static Timer statsTimer;
        static Timer emailTimer;

		private long EmailInterval = ForumConfiguration.GetConfig().ThreadIntervalEmail * 60000;
		private long StatsInterval = ForumConfiguration.GetConfig().ThreadIntervalStats * 60000;

        public String ModuleName { 
            get { return "ForumsHttpModule"; } 
        }    


        // *********************************************************************
        //  ForumsHttpModule
        //
        /// <summary>
        /// Initializes the HttpModule and performs the wireup of all application
        /// events.
        /// </summary>
        /// <param name="application">Application the module is being run for</param>
        public void Init(HttpApplication application) { 

            // Wire-up application events
            //
            application.BeginRequest += new EventHandler(this.Application_BeginRequest);
            application.AuthenticateRequest += new EventHandler(Application_AuthenticateRequest);
            application.Error += new EventHandler(this.Application_OnError);
            application.AuthorizeRequest += new EventHandler(this.Application_AuthorizeRequest);

#if DEBUG
            application.ReleaseRequestState += new EventHandler(this.Application_ReleaseRequestState);
#endif
			
			ForumConfiguration forumConfig = ForumConfiguration.GetConfig();
			if( forumConfig != null
			&&  forumConfig.IsBackgroundThreadingDisabled == false ) {
				if (emailTimer == null)
					emailTimer = new Timer(new TimerCallback(ScheduledWorkCallbackEmailInterval), application.Context, EmailInterval, EmailInterval);

				if( forumConfig.IsIndexingDisabled == false 
				&&	statsTimer == null ) {
					statsTimer = new Timer(new TimerCallback(ScheduledWorkCallbackStatsInterval), application.Context, StatsInterval, StatsInterval);
			}
        }
        }

        public void Dispose() {
            statsTimer = null;
            emailTimer = null;
        }


        #endregion

        #region Application OnError
        private void Application_OnError (Object source, EventArgs e) {
            ForumConfiguration forumConfig = ForumConfiguration.GetConfig();
            string defaultLanguage = forumConfig.DefaultLanguage;
            HttpApplication application = (HttpApplication)source;
            HttpContext context = application.Context;
            ForumException forumException;
            string html;
            StreamReader reader;

            if (context.Server.GetLastError().GetBaseException() is ForumException) {
                forumException = (ForumException) context.Server.GetLastError().GetBaseException();

                switch (forumException.ExceptionType) {

                    case ForumExceptionType.DataProvider:

                        // We can't connect to the data store
                        //
                        reader = new StreamReader( context.Server.MapPath("~/Languages/" + defaultLanguage + "/errors/DataStoreUnavailable.htm") );
                        html = reader.ReadToEnd();
                        reader.Close();

                        html = html.Replace("[DATASTOREEXCEPTION]", forumException.Message);
                        context.Response.Write(html);
                        context.Response.End();
                        break;

                    case ForumExceptionType.UserInvalidCredentials:
                        forumException.Log();
                        break;

                    case ForumExceptionType.AccessDenied:
                        forumException.Log();
                        break;

                    case ForumExceptionType.AdministrationAccessDenied:
                        forumException.Log();
                        break;

                    case ForumExceptionType.ModerateAccessDenied:
                        forumException.Log();
                        break;

                    case ForumExceptionType.PostDeleteAccessDenied:
                        forumException.Log();
                        break;

                    case ForumExceptionType.PostProblem:
                        forumException.Log();
                        break;

                    case ForumExceptionType.UserAccountBanned:
                        forumException.Log();
                        break;
                    
                    // LN 6/9/04: New exception added 
                    case ForumExceptionType.ResourceNotFound:
                        forumException.Log();
                        break;

                    case ForumExceptionType.UserUnknownLoginError:
                        forumException.Log();
                        break;
                }
            } else {
                forumException = new ForumException(ForumExceptionType.UnknownError, context.Server.GetLastError().Message, context.Server.GetLastError());
                forumException.Log();
            }

            if (forumException.ExceptionType == ForumExceptionType.UnknownError) {
                if ((context.IsCustomErrorEnabled) && (!context.Request.Url.IsLoopback))
                    ForumContext.RedirectToMessage(context, forumException);
            } else{
                ForumContext.RedirectToMessage(context, forumException);
            }
        }
        #endregion

        #region Application Release Request State
        private void Application_ReleaseRequestState (Object source, EventArgs e) {
            HttpApplication application = (HttpApplication)source;
            HttpContext context = application.Context;
            Version v = SiteStatistics.AspNetForumsVersion;
        
            // Only dump debug details for home or about
            //
			//if ( (context.Request.RawUrl.IndexOf("default.aspx") > 0) || (context.Request.RawUrl.IndexOf("about.aspx") > 0) ) {
			//    context.Response.Write("<center>");
			//    context.Response.Write("============ Debug Build ============<br>Version: " + v.ToString() + "<br>============ Debug Build ============" );
			//    context.Response.Write("</center>");
			//}
        }
        #endregion

        #region Application AuthenticateRequest
        private void Application_AuthenticateRequest(Object source, EventArgs e) {
			HttpContext context = HttpContext.Current;

			if (context != null && context.User != null && context.User.Identity != null)
				ForumContext.Current.UserName = context.User.Identity.Name;

            Spinit.Wpc.Forum.Components.Roles roles = new Spinit.Wpc.Forum.Components.Roles();
			roles.GetUserRoles();
        }
        #endregion

        #region Application AuthorizeRequest
        private void Application_AuthorizeRequest (Object source, EventArgs e) {

            HttpApplication application = (HttpApplication)source;
            HttpContext context = application.Context;

            // Track anonymous users
            //
            Users.TrackAnonymousUsers();

            // Do we need to force the user to login?
            //
            if (Users.GetUser().ForceLogin) {
                Moderate.ToggleUserSettings(ModerateUserSetting.ToggleForceLogin, Users.GetUser(), 0);
                context.Response.Redirect(Globals.GetSiteUrls().Logout, true);
            }



        }
        #endregion

        #region Application BeginRequest
        private void Application_BeginRequest(Object source, EventArgs e) {
			try {
				HttpApplication application = (HttpApplication)source;
				HttpContext context = application.Context;

				if( application == null 
					||	context		== null )
					return;

				// Url Rewriting
				//
				RewriteUrl(context);

				// Create the forum context
				//
				context.Items.Add("ForumContext", new ForumContext());

				// Capture any pingback information
				//
				CaptureForumPingback();

				// Are the forums disabled?
				//
				if ((Globals.GetSiteSettings().ForumsDisabled) && (HttpContext.Current.Request.Url.Host != "localhost"))  {
					ForumConfiguration forumConfig = ForumConfiguration.GetConfig();
					string defaultLanguage = forumConfig.DefaultLanguage;

					// Forums is disabled
					//
					StreamReader reader = new StreamReader( context.Server.MapPath("~/Languages/" + defaultLanguage + "/errors/ForumsDisabled.htm") );
					string html = reader.ReadToEnd();
					reader.Close();

					context.Response.Write(html);
					context.Response.End();

				}
			}
			catch( Exception ex ) {
				ForumException forumEx = new ForumException( ForumExceptionType.UnknownError, "Unknown error in BeginRequest", ex );
				forumEx.Log();
			}
									
        }
        #endregion

        #region URL Rewriting
        private void RewriteUrl (HttpContext context) {

            Match match = Regex.Match(context.Request.RawUrl, context.Request.ApplicationPath + "/(?<Destination>(.|\\n)*?)/(?<Url>(.|\\n)*?)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            string pathToRewriteto;

            if (match.Captures.Count > 0) {

                switch (match.Groups["Destination"].ToString().Substring(0, 2)) {

                        // Forum
                    case "F$":
                        pathToRewriteto = Globals.GetSiteUrls().Forum( int.Parse(match.Groups["Destination"].ToString().Substring(2)), false );
                        context.RewritePath( pathToRewriteto );
                        break;

                        // Post
                    case "P$":
                        pathToRewriteto = Globals.GetSiteUrls().Post( int.Parse(match.Groups["Destination"].ToString().Substring(2)), false );
                        context.RewritePath( pathToRewriteto );
                        break;

                        // ForumGroup
                    case "G$":
                        pathToRewriteto = Globals.GetSiteUrls().ForumGroup( int.Parse(match.Groups["Destination"].ToString().Substring(2)), false );
                        context.RewritePath( pathToRewriteto );
                        break;

                        // User
                    case "U$":
                        pathToRewriteto = Globals.GetSiteUrls().UserProfile( int.Parse(match.Groups["Destination"].ToString().Substring(2)), false );
                        context.RewritePath( pathToRewriteto );
                        break;

                    
                }
            }
        }
        #endregion

        #region Timer Callbacks
        private void ScheduledWorkCallbackEmailInterval (object sender) {
			try {
				// suspend the timer while we process emails
				emailTimer.Change( System.Threading.Timeout.Infinite, EmailInterval );

				// Send emails
				//
				Emails.SendQueuedEmails( (HttpContext) sender);


				// Update anonymous users
				//
				Users.UpdateAnonymousUsers( (HttpContext) sender);
			}
			catch( Exception e ) {
				ForumException fe = new ForumException( ForumExceptionType.EmailUnableToSend, "Scheduled Worker Thread failed.", e );
				fe.Log();
			}
			finally {
				emailTimer.Change( EmailInterval, EmailInterval );
			}
        }

        private void ScheduledWorkCallbackStatsInterval(object sender) {
			try {
				// suspend timer while we process
				statsTimer.Change( System.Threading.Timeout.Infinite, StatsInterval );

				// Reindex posts
				//
				Search.IndexPosts( (HttpContext) sender, 100);

				SiteStatistics.LoadSiteStatistics( (HttpContext) sender, true, 1 );
			}
			catch( Exception e ) {
				ForumException fe = new ForumException( ForumExceptionType.UnknownError, "Failure performing scheduled statistics maintenance.", e );
				fe.Log();
			}
			finally {
				statsTimer.Change( StatsInterval, StatsInterval);
			}
        }
        #endregion

        private void CaptureForumPingback () {
            ForumContext forumContext = ForumContext.Current;
            Hashtable pingbackList = new Hashtable();
            RssPingback p;

            if (forumContext.Context.Request.UrlReferrer == null)
                return;

            string pingback = forumContext.Context.Request.UrlReferrer.ToString();

#if !DEBUG
            // Ignore local requests
            //
            if ((pingback.IndexOf("localhost") > 0) || (pingback.StartsWith("/")))
                return;
#endif

            // Ignore all non-RSS requests
            //
            if (forumContext.Context.Request.Url.ToString().IndexOf("rss.aspx") <= 0)
                return;

            if (pingbackList[pingback] == null) {
                p = new RssPingback();
                p.Count = 1;
                p.Url = pingback;
                p.ForumID = forumContext.ForumID;

                pingbackList[p.Url] = p;
            } else {
                p = (RssPingback) pingbackList[pingback];
                p.Count += 1;
            }

            // Save the pingback
            if (pingbackList.Count > 0) {
                // We don't have it in our lookup table
                //
                ForumsDataProvider dp = ForumsDataProvider.Instance();

                dp.RssPingback(pingbackList);
            }

        }
    }

}
