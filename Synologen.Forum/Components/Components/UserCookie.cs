using System;
using System.Web;
using Spinit.Wpc.Forum.Enumerations;

namespace Spinit.Wpc.Forum.Components {
    /// <summary>
    /// Summary description for UserCookie.
    /// </summary>
    public class UserCookie {

        #region Member variables and constructor
        HttpCookie userCookie;
        string cookieName = "AspNetForums-UserCookie";
        ForumContext forumContext = ForumContext.Current;
        HttpContext context = null;
        User user;

        public UserCookie( User user ) {

            context = forumContext.Context;
            userCookie = context.Request.Cookies[cookieName];
            this.user = user;

            // Did we find a cookie?
            //
            if (userCookie == null) {
                userCookie = new HttpCookie(cookieName);
            } else {
                userCookie = context.Request.Cookies[cookieName];
            }

        }
        #endregion

        #region Private helper functions
        void WriteCookie() {
            userCookie.Expires = DateTime.Now.AddYears(1);
            context.Response.Cookies.Add(userCookie);
        }
        #endregion

        #region Last Visit
        public DateTime LastVisit {
            get {
                if (userCookie["lvd"] == null) {
                    userCookie["lvd"] = DateTime.Parse("1/1/1999").ToString();
                } else if (DateTime.Parse(userCookie["sd"]) < DateTime.Now.AddMinutes(20)) {
                    userCookie["lvd"] = userCookie["sd"];
                }

                userCookie["sd"] = DateTime.Now.ToString();

                // Write the updated cookie
                WriteCookie();

                return DateTime.Parse(userCookie["lvd"]);
            }
        }
        #endregion

        #region Forum Filter/View Settings
        public void SaveForumOptions (int forumID, string settings) {
            userCookie["fo" + forumID] = settings;

            // Write the updated cookie
            WriteCookie();
        }

        public ForumUserOptions GetForumOptions (int forumID) {
            return new ForumUserOptions(userCookie["fo" + forumID]);
        }
        #endregion

        #region Hidden forum groups
        public string[] HiddenForumGroups {

            get {
                // Do we have values?
                //
                if (userCookie.Values["hfg"] != null)
                    return userCookie.Values["hfg"].Split(',');

                return new string[0];
            }
        }

        public void RemoveHiddenForumGroup(int forumGroupID) {

            string hfg = "," + userCookie.Values["hfg"] + ",";

            // Add some details to the string to do a simple replace
            //
            hfg = hfg.Replace("," + forumGroupID + ",", ",");

            // Trim the string
            //
            hfg = hfg.TrimStart(',').TrimEnd(',');

            userCookie.Values["hfg"] = hfg;

            WriteCookie();
        }

        public void AddHiddenForumGroup(int forumGroupID) {
            string[] s = HiddenForumGroups;
            string hfg = string.Empty;

            // Do we already have this forum group
            //
            for (int i = 0; i < s.Length; i++)
                if (s[i] == forumGroupID.ToString())
                    return;

            string[] s1 = new String[s.Length + 1];
            s.CopyTo(s1, 0);
            s1[s1.Length - 1] = forumGroupID.ToString();
                
            for (int i=0; i<s1.Length; i++)
                hfg = hfg + s1[i] + ",";

            // Trim off the extra comma
            //
            hfg = hfg.TrimStart(',').TrimEnd(',');

            userCookie.Values["hfg"] = hfg;

            WriteCookie();
        }
        #endregion

    }

    public class ForumUserOptions {
        public SortThreadsBy SortBy = SortThreadsBy.LastPost;
        public SortOrder SortOrder = SortOrder.Descending;
        public ThreadUsersFilter UserFilter = ThreadUsersFilter.All;
        public ThreadDateFilterMode DateFilter = ThreadDateFilterMode.All;
        public bool HideReadPosts = false;
        public bool HasSettings = false;

        public ForumUserOptions(string settings) {

            if ((settings == null) || (settings == string.Empty))
                return;

            HasSettings = true;

            try {
                // Serialized format is: [SortBy]:[SortOrder]:[DateFilter]:[HideReadPosts]:[UserFilter]
                string[] s = settings.Split(':');

                SortBy = (SortThreadsBy) int.Parse(s[0]);
                SortOrder = (SortOrder) int.Parse(s[1]);
                DateFilter = (ThreadDateFilterMode) int.Parse(s[2]);

                if (s[3] == "T")
                    HideReadPosts = true;
                else
                    HideReadPosts = false;

                UserFilter = (ThreadUsersFilter) int.Parse(s[4]);
            } catch {}

        }

    }

}
