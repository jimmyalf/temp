using System;
using System.Web;
using System.Collections;
using Spinit.Wpc.Forum.Enumerations;

namespace Spinit.Wpc.Forum.Components {

    public class ForumContext {
        int forumID =       -1;
        int messageID =     -1;
        int forumGroupID =  -1;
        int postID =        -1;
        int threadID =      -1;
        int userID =        -1;
        string userName =   "";
        int pageIndex =     -1;
        int roleID =        -1;
        string queryText =  "";
        string returnUrl =  "";

        HttpContext context;
        DateTime requestStartTime = DateTime.Now;

        public ForumContext() {

            context = HttpContext.Current;

            if (context == null)
                return;

            // Read common values we expect to find on the QS
            //
            postID = GetIntFromQueryString(context, "PostID");
            forumID = GetIntFromQueryString(context, "ForumID");
            forumGroupID = GetIntFromQueryString(context, "ForumGroupID");
            userID = GetIntFromQueryString(context, "UserID");
            messageID = GetIntFromQueryString(context, "MessageID");
            pageIndex = GetIntFromQueryString(context, "PageIndex");
            roleID = GetIntFromQueryString(context, "RoleID");
            queryText = context.Request.QueryString["q"];
            returnUrl = context.Request.QueryString["returnUrl"];

        }

        public static ForumContext Current {
            get {
                if (HttpContext.Current == null)
                    return new ForumContext();

                return (ForumContext) HttpContext.Current.Items["ForumContext"];
            }

        }

        // *********************************************************************
        //  GetIntFromQueryString
        //
        /// <summary>
        /// Retrieves a value from the query string and returns it as an int.
        /// </summary>
        // ***********************************************************************/
        public static int GetIntFromQueryString(HttpContext context, string key) {
            int returnValue = -1;
            string queryStringValue;

            // Attempt to get the value from the query string
            //
            queryStringValue = context.Request.QueryString[key];

            // If we didn't find anything, just return
            //
            if (queryStringValue == null)
                return returnValue;

            // Found a value, attempt to conver to integer
            //
            try {

                // Special case if we find a # in the value
                //
                if (queryStringValue.IndexOf("#") > 0)
                    queryStringValue = queryStringValue.Substring(0, queryStringValue.IndexOf("#"));

                returnValue = Convert.ToInt32(queryStringValue);
            } 
            catch {}

            return returnValue;

        }

        public static void RedirectToMessage (HttpContext context, ForumException exception) {

            if ((exception.InnerException != null) && ( exception.InnerException is ForumException)) {
                ForumException inner = (ForumException) exception.InnerException;
            }
            context.Response.Redirect(Globals.GetSiteUrls().Message( exception.ExceptionType ), true);
        }

        // *********************************************************************
        //  GetForumFromForumLookupTable
        //
        /// <summary>
        /// Attempts to use forum lookup table. Capable of flushing lookup table
        /// </summary>
        // ***********************************************************************/
        public Forum GetForumFromForumLookupTable(int forumID) {
            Forum f = (Forum) this.ForumLookupTable[forumID];

            if (f != null)
                return f;

            // Null out the cached list and attempt to reload
            //
            if ( (f == null) && (context.Cache["ForumsTable"] != null) )
                context.Cache.Remove("ForumsTable");

            f = (Forum) ForumLookupTable[forumID];

            if (f == null) {
                throw new Exception("Forum ID is invalid");
            }

            return f;
        }

        public Hashtable ForumLookupTable {

            get {

                if (HttpRuntime.Cache["ForumsTable"] == null)
                    HttpRuntime.Cache.Insert("ForumsTable", Forums.GetForums(this, 0, true, false), null, DateTime.Now.AddMinutes(120), TimeSpan.Zero);

                return (Hashtable) HttpRuntime.Cache["ForumsTable"];
            }

        }

        public static string GetApplicationName () {
            return GetApplicationName (HttpContext.Current);
        }

        public static string GetApplicationName (HttpContext context) {
            if (context == null)
                return "";

            string hostName = context.Request.Url.Host;
            string applicationPath = context.Request.ApplicationPath;

            return hostName + applicationPath;
        }

        public HttpContext Context { 
            get { 
                if (context == null)
                    return new HttpContext(null);

                return context; 
            } 
        }

        public int MessageID { get { return messageID; } }
        public int ForumID { get { return forumID; } }
        public int ForumGroupID { get { return forumGroupID; } }
        public int PostID { get { return postID; } }
        public int ThreadID { get { return threadID; } }
        public int UserID { get { return userID; } }
        public string UserName { get { return userName; } set { userName = value; } }
        public int RoleID { get { return roleID; } }
        public string QueryText { get { return queryText; } }
        public string ReturnUrl { get { return returnUrl; } }
        public int PageIndex { get { return (pageIndex - 1); } }
        public DateTime RequestStartTime { get { return requestStartTime; } }
        public User User { get { return Users.GetUser(); } }
        public SiteStatistics Statistics { get { return SiteStatistics.LoadSiteStatistics(context, true, 3); }}
    }
}
