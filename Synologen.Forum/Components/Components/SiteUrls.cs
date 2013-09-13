using System;
using System.Web;
using System.Collections;
using System.Collections.Specialized;
using Spinit.Wpc.Forum.Configuration;
using Spinit.Wpc.Forum.Enumerations;
using System.Xml;

namespace Spinit.Wpc.Forum.Components {

    public class SiteUrls {

        #region Member variables & constructor
        ForumContext forumContext = ForumContext.Current;
        NameValueCollection paths = null;
        NameValueCollection reversePaths = null;
        string siteUrlsXmlFile = "SiteUrls.config";
        bool enableWaitPage = false;

        // Constructor loads all the site urls from the SiteUrls.config file
        // or returns from the cache
        //
        public SiteUrls() {
            string cacheKey = "SiteUrls";
            string cacheKeyReverse = "SiteUrls-ReverseLookup";

            // Get the names of the providers
            //
            ForumConfiguration config = ForumConfiguration.GetConfig();

            // Get the path to the URLs file
            //
            siteUrlsXmlFile = config.ForumFilesPath + siteUrlsXmlFile;

            if (HttpRuntime.Cache[cacheKey] == null) {
                string file = "";

                if (forumContext != null)
                     file = forumContext.Context.Server.MapPath(Globals.ApplicationPath + siteUrlsXmlFile);
                else
                    file = HttpContext.Current.Server.MapPath(Globals.ApplicationPath + siteUrlsXmlFile);

                paths = new NameValueCollection();
                reversePaths = new NameValueCollection();

                // Load SiteUrls from the SiteUrls.xml file
                //
                XmlDocument doc = new XmlDocument();
                doc.Load( file );

                XmlNode urls = doc.SelectSingleNode("urls");

                foreach (XmlNode n in urls.ChildNodes) {

                    if (n.NodeType != XmlNodeType.Comment) {
                        string name = n.Attributes["name"].Value;
                        string path = ResolveUrl(n.Attributes["path"].Value.Replace("^", "&"));
                        paths.Add(name, path);
                        reversePaths.Add(n.Attributes["path"].Value.Replace("^", "&"), name);
                    }

                }

                // If the file changes we want to reload it
                System.Web.Caching.CacheDependency dep = new System.Web.Caching.CacheDependency(file);
                System.Web.Caching.CacheDependency dep2 = new System.Web.Caching.CacheDependency(file);

                HttpRuntime.Cache.Insert(cacheKey, paths, dep, DateTime.MaxValue, TimeSpan.Zero);
                HttpRuntime.Cache.Insert(cacheKeyReverse, reversePaths, dep2, DateTime.MaxValue, TimeSpan.Zero);

            }

            paths = (NameValueCollection) HttpRuntime.Cache[cacheKey];
            reversePaths = (NameValueCollection) HttpRuntime.Cache[cacheKeyReverse];

        }
        #endregion

        #region Who is online
        // Who is online
        //
        public string WhoIsOnline {
            get { return paths["whoIsOnline"]; }
        }
        #endregion

        #region Moderation
        // Moderation
        //
        public string Moderate {
            get { return paths["moderate"]; }
        }

        public string ModerationHome {
            get { return paths["moderate_home"]; }
        }

        public string ModerateViewPost (int postID) {
            return string.Format( paths["moderate_ViewPost"], postID.ToString() );
        }

        public string ModeratePostDelete (int postID, string returnUrl) {
            return string.Format(paths["moderate_Post_Delete"], postID.ToString(), returnUrl );
        }

        public string ModeratePostEdit (int postID, string returnUrl) {
            return string.Format(paths["moderate_Post_Edit"], postID.ToString(), returnUrl );
        }

        public string ModerateViewUserProfile (int userID) {
            return string.Format( paths["moderate_ViewUserProfile"], userID.ToString() );
        }

        public string ModeratePostMove (int postID, string returnUrl) {
            return string.Format(paths["moderate_Post_Move"], postID.ToString(), returnUrl );
        }

        public string ModerateThreadSplit (int postID, string returnUrl) {
            return string.Format(paths["moderate_Thread_Split"], postID.ToString(), returnUrl );
        }

        public string ModerateThreadJoin (int postID, string returnUrl) {
            return string.Format(paths["moderate_Thread_Join"], postID.ToString(), returnUrl );
        }

        public string ModerationHistory {
            get { return paths["moderation_History"]; }
        }

        public string ModerateForum (int forumID) {
            return string.Format(paths["moderate_Forum"], forumID.ToString()) ;
        }
        #endregion

        #region Admin
        // Admin
        //
        public string Admin {
            get { return paths["admin"]; }
        }

		public string AdminConfiguration {
			get{ return paths["admin_Configuration"]; }
		}
		public string AdminManageForums {
			get{ return paths["admin_Forums"];}
		}
        public string AdminUserEdit ( int userID ) {
            return string.Format(paths["admin_User_Edit"], userID.ToString()) ;
        }

        public string AdminForumCreate {
            get { return paths["admin_Forum_Create"]; }
        }

		public string AdminForumGroup( int forumID ) {
			return string.Format( paths["admin_ForumGroup"], forumID.ToString() );
		}

        public string AdminForumEdit ( int forumID ) {
            return string.Format(paths["admin_Forum_Edit"], forumID.ToString() );
        }

		public string AdminForumPermissions {
			get{ return paths["admin_Forum_Permissions"]; }
		}
		
		public string AdminForumPermissionEdit( int forumId, int roleId ) {
			return string.Format(paths["admin_Forum_Permissions_Edit"], forumId, roleId );
		}
        public string AdminHome {
            get { return paths["admin_Home"]; }
        }

        public string AdminManageUsers {
            get { return paths["admin_User_List"]; }
        }

        public string AdminManageRoles {
            get { return paths["admin_Roles"]; }
        }

        public string AdminUserRoles (int userID) {
            return string.Format(paths["admin_User_UserRolesAdmin"], userID.ToString()) ;
        }

        public string AdminUserPasswordChange (int userID) {
            return string.Format(paths["admin_User_ChangePassword"], userID.ToString()) ;
        }

        public string AdminUserPasswordAnswerChange (int userID) {
            return string.Format(paths["admin_User_ChangePasswordAnswer"], userID.ToString()) ;
        }

        public string AdminUserConfiguration 
        {
            get { return paths["admin_User_Configuration"]; }
        }

		public string AdminManageRanks {
			get{ return paths["admin_Ranks"]; }
		}

		public string AdminManageNames {
			get{ return paths["admin_Names"]; }
		}

		public string AdminManageCensorships {
			get{ return paths["admin_Censorships"];}
		}

		public string AdminManageSmilies {
			get{ return paths["admin_Smilies"]; }
		}

		public string AdminMassEmail {
			get{ return paths["admin_MassEmail"]; }
		}

		public string AdminRoleEdit( int roleID ) {
			return string.Format(paths["admin_Role_Edit"], roleID.ToString() );
		}

		public string AdminReportsBuiltIn {
			get { return paths["admin_Reports_BuiltIn"]; }
		}

        #endregion

        #region Posts

        // Post related properties
        //
        public string Post (int postID) {
            return Post(postID, Globals.GetSiteSettings().EnableSearchFriendlyURLs);
        }

        public string Post (int postID, bool searchFriendly) {
            if (searchFriendly)
                return string.Format( paths["searchFriendlyPost"], postID.ToString());
            else
                return string.Format( paths["post"], postID.ToString());
        }

        public string PrintPost (int postID) {
            return string.Format( paths["post_Print"], postID.ToString());
        }

        public string PostRating (int postID) {
            return string.Format( paths["post_Rating"], postID.ToString());
        }

        public string PostInPage (int postID, int postInPageID) {
            return string.Format( paths["post_InPage"], postID.ToString(), postInPageID.ToString());
        }

        public string PostAttachment (int postID) {
            return string.Format( paths["post_Attachment"], postID.ToString());
        }

        public string PostPaged (int postID, int page) {
            return string.Format( paths["post_Paged"], page.ToString(), postID.ToString(), postID.ToString());
        }

        public string PostCreate (int forumID) {
            return string.Format( paths["post_Create"], forumID.ToString());
        }

        public string PostReply (int postID) {
            return string.Format( paths["post_Reply"],  postID.ToString());
        }

        public string PostReply (int postID, bool isQuote) {
            return string.Format( paths["post_Quote"], postID.ToString(), isQuote.ToString());
        }

        public string PostEdit (int postID, string returnUrl) {
            return string.Format( paths["post_Edit"], postID.ToString(), returnUrl);
        }

        public string PostDelete (int postID, string returnUrl) {
            return string.Format( paths["post_Delete"], postID.ToString(), returnUrl);
        }

        public string PostsUnanswered {
            get { return paths["post_Unanswered"]; }
        }

        public string PostsActive {
            get { return paths["post_Active"]; }
        }
        #endregion

        #region Help
        public string HelpThreadIcons {
            get { return string.Format(paths["help_ThreadIcons"], Globals.Language); }
        }
        #endregion

        #region User
        // User related properties
        //
        public string UserProfile (int userID) {
            return UserProfile (userID, Globals.GetSiteSettings().EnableSearchFriendlyURLs);
        }

        public string UserProfile (int userID, bool searchFriendly) {
            if (searchFriendly)
                return string.Format( paths["searchFriendlyUser"], userID.ToString());
            else
                return string.Format( paths["user"], userID.ToString());
        }

        public string UserEditProfile {
            get { 
                string currentPath = forumContext.Context.Request.Url.PathAndQuery;
                string returnUrl = null;

                if (ExtractQueryParams(currentPath)["ReturnUrl"] != null)
                    returnUrl = ExtractQueryParams(currentPath)["ReturnUrl"];

                if ((returnUrl == null) || (returnUrl == string.Empty))
                    return string.Format(paths["user_EditProfile"], currentPath);
                else
                    return string.Format(paths["user_EditProfile"], returnUrl);
            }
                
        }

        public string UserMyForums {
            get { return paths["user_MyForums"]; }
        }

        public string UserChangePassword {
            get { return paths["user_ChangePassword"]; }
        }

        public string UserChangePasswordAnswer {
            get { return paths["user_ChangePasswordAnswer"]; }
        }

        public string UserForgotPassword 
        {
            get { return paths["user_ForgotPassword"]; }
        }
        
        public string UserRegister {
            get { 
                string currentPath = forumContext.Context.Request.Url.PathAndQuery;
                string returnUrl = null;

                if (ExtractQueryParams(currentPath)["ReturnUrl"] != null)
                    returnUrl = ExtractQueryParams(currentPath)["ReturnUrl"];

                if ((returnUrl == null) || (returnUrl == string.Empty))
                    return string.Format(paths["user_Register"], currentPath);
                else
                    return string.Format(paths["user_Register"], returnUrl);

            }
        }

        public string UserList {
            get { return paths["user_List"]; }
        }

        public string UserRoles (int roleID) {
            return string.Format( paths["user_Roles"], roleID.ToString() );
        }
        #endregion

        #region Private Messages
        // Private Messages
        //
        public string PrivateMessage (int userID) {
            return string.Format( paths["privateMessage"], userID.ToString());
        }

        public string UserPrivateMessages {
            get { return paths["privateMessages"]; }
        }

        #endregion

        #region Email
        // Email
        //
        public string EmailToUser (int userID) {
            return string.Format( paths["email_ToUser"], userID.ToString());
        }
        #endregion

        #region RSS
        // RSS
        //
        public string RssForum (int forumID, ThreadViewMode mode) {
            return string.Format(paths["rss"], forumID.ToString(), ((int) mode).ToString());
        }
        #endregion

        #region Search
        public string Search {
            get { return paths["search"]; }
        }

        public string SearchAdvanced {
            get { return paths["search_Advanced"]; }
        }

        public string SearchForText(string textToSearchFor) {
            return SearchForText (textToSearchFor, "", "");
        }

        public string SearchForText(string textToSearchFor, string forumsToSearch, string usersToSearch) {

            if (enableWaitPage)
                return string.Format( paths["wait"], (string.Format( paths["search_ForText"], textToSearchFor, forumsToSearch, usersToSearch)));
            else
                return string.Format( paths["search_ForText"], textToSearchFor, forumsToSearch, usersToSearch);
        }

        public string SearchByUser (int userID) {
            string encodedUserID = Spinit.Wpc.Forum.Search.ForumsToSearchEncode(userID.ToString());

            return SearchForText("", "", encodedUserID);
        }

        public string SearchForUser (string username) {
            return string.Format( paths["search_ForUser"], username);
        }

        public string SearchForUserAdmin (string username) {
            return string.Format( paths["search_ForUserAdmin"], username);
        }
        #endregion

        #region FAQ
        public string FAQ {
            get { return string.Format(paths["faq"], Globals.Language); }
        }
        #endregion

		public string TermsOfUse {
			get { 
				string termsOfUse = "";
				try {
					termsOfUse = paths["termsOfUse"];
				}
				catch {
					termsOfUse = "";
				}

				return termsOfUse;
			}
		}

        #region Login, Logout, Messages
        public string LoginReturnHome {
            get {
                return string.Format(paths["login"], Globals.ApplicationPath);
            }
        }

        public string Login {
            get { 
                string currentPath = forumContext.Context.Request.Url.PathAndQuery;
                string returnUrl = null;

                if (ExtractQueryParams(currentPath)["ReturnUrl"] != null)
                    returnUrl = ExtractQueryParams(currentPath)["ReturnUrl"];

                if ((returnUrl == null) || (returnUrl == string.Empty))
                    return string.Format(paths["login"], currentPath);
                else
                    return string.Format(paths["login"], returnUrl);
            }
        }

        public string Logout {
            get { return paths["logout"]; }
        }

        public string Message (ForumExceptionType exceptionType) {
            return string.Format( paths["message"], ((int) exceptionType).ToString());
        }

        public string Message (ForumExceptionType exceptionType, string returnUrl) {
            return string.Format( paths["message_return"], ((int) exceptionType).ToString(), returnUrl);
        }
        #endregion

        #region Report
        public string Report {
            get {
                return string.Format( paths["post_Create"], (int) DefaultForums.Reporting);
            }
        }
        #endregion

        #region Forum / ForumGroup

		public string Home { 
			get { return paths["home"]; }
		}

        public string Forum (int forumID) {
            return Forum(forumID, Globals.GetSiteSettings().EnableSearchFriendlyURLs);
        }

        public string Forum (int forumID, bool searchFriendly) {
            if (searchFriendly)
                return string.Format( paths["searchFriendlyForum"], forumID.ToString());
            else
                return string.Format( paths["forum"], forumID.ToString());
        }

        public string ForumGroup (int forumGroupID) {
            return ForumGroup (forumGroupID, Globals.GetSiteSettings().EnableSearchFriendlyURLs);
        }

        public string ForumGroup (int forumGroupID, bool searchFriendly) {
            if (searchFriendly)
                return string.Format( paths["searchFriendlyForumGroup"], forumGroupID.ToString());
            else
                return string.Format( paths["forumGroup"], forumGroupID.ToString());
        }
        #endregion

        #region Private static helper methods
        private static string ResolveUrl (string path) {

            if (Globals.ApplicationPath.Length > 0)
                return Globals.ApplicationPath + path;
            else
                return path;
        }

        public static string RemoveParameters (string url) {

            if (url == null)
                return string.Empty;

            int paramStart = url.IndexOf("?");

            if (paramStart > 0)
                return url.Substring(0, paramStart);

            return url;
        }

        private static NameValueCollection ExtractQueryParams(string url) {
            int startIndex = url.IndexOf("?");
            NameValueCollection values = new NameValueCollection();

            if (startIndex <= 0)
                return values;

            string[] nameValues = url.Substring(startIndex + 1).Split('&');

            foreach (string s in nameValues) {
                string[] pair = s.Split('=');

                string name = pair[0];
                string value = string.Empty;

                if (pair.Length > 1)
                    value = pair[1];

                values.Add(name, value);
            }

            return values;
        }

        public static string FormatUrlWithParameters(string url, string parameters) {
            if (url == null)
                return string.Empty;

            if (parameters.Length > 0)
                url = url + "?" + parameters;

            return url;

        }
        #endregion

        #region Public helper methods

        public struct ForumLocation {
            public string Description;
            public string UrlName;
            public int ForumID;
            public int ForumGroupID;
            public int PostID;
            public int UserID;
        }

        public static ForumLocation GetForumLocation (string encodedLocation) {

            // Decode the location
            ForumLocation location = new ForumLocation();
            string[] s = encodedLocation.Split(':');
            try {
                location.UrlName        = s[0];
                location.ForumGroupID   = int.Parse(s[1]);
                location.ForumID        = int.Parse(s[2]);
                location.PostID         = int.Parse(s[3]);
                location.UserID         = int.Parse(s[4]);
            } catch {}

            return location;

        }

        // Takes a URL used in the forums and performs a reverse
        // lookup to return a friendly name of the currently viewed
        // resource
        //
		public static string LocationKey () {

			ForumContext forumContext = ForumContext.Current;
			string url = forumContext.Context.Request.RawUrl;
			string retval = null;
        
			NameValueCollection reversePaths = null;

			if (Globals.ApplicationPath.Length > 0)
				url = url.Replace(Globals.ApplicationPath, "").ToLower();

			reversePaths = Globals.GetSiteUrls().ReversePaths;

			int forumGroupIDqs = -1;
			int forumIDqs = -1;
			int postIDqs = -1;
			int userIDqs = -1;
			int modeIDqs = -1;

			// Modify the url so we can perform a reverse lookup
			//
			try {
				for (int i = 0; i < forumContext.Context.Request.QueryString.Count; i++) {
					string key = forumContext.Context.Request.QueryString.Keys[i].ToLower();

					switch (key) {
						case "forumid":
							forumIDqs = int.Parse(forumContext.Context.Request.QueryString[key]);
							break;
						case "postid":
							postIDqs = int.Parse(forumContext.Context.Request.QueryString[key]);
							break;
						case "userid":
							userIDqs = int.Parse(forumContext.Context.Request.QueryString[key]);
							break;
						case "forumgroupid":
							forumGroupIDqs = int.Parse(forumContext.Context.Request.QueryString[key]);
							break;
						case "mode":
							modeIDqs = int.Parse(forumContext.Context.Request.QueryString[key]);
							break;
					}
					url = url.Replace(forumContext.Context.Request.QueryString[key], "{"+i+"}");
				}

			} catch {
				return "";
			}

			if (reversePaths != null)
				retval = reversePaths[url];

			if ((retval == null) || (retval == string.Empty))
				retval = "/";

			return retval + ":" + forumGroupIDqs + ":" + forumIDqs + ":" + postIDqs + ":" + userIDqs + ":" + modeIDqs;
			
		}
		#endregion

        #region Public properties
        public bool EnableWaitPage {
            get {
                return true;
            }
            set {
                enableWaitPage = value;
            }

        }

        public NameValueCollection ReversePaths {
            get {
                return reversePaths;
            }
        }
        #endregion

    }
}
