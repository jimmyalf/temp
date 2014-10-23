using System;
using System.Web;
using System.Web.Caching;
using System.Collections;
using System.Xml;

namespace Spinit.Wpc.Forum.Components {

    // *********************************************************************
    //  Statistics
    //
    /// <summary>
    /// This class contains is used to get statistics about the running ASP.NET Forum
    /// </summary>
    // ***********************************************************************/
    public class SiteStatistics {
        int currentAnonymousUsers;
        ArrayList activeUsers = new ArrayList();
        ArrayList activeModerators = new ArrayList();
        ArrayList moderationActions = new ArrayList();

        int totalUsers;
        int totalPosts;
        int totalThreads;
        int totalModerators;
        int totalModeratedPosts;
        int totalAnonymousUsers;
        int newPostsInPast24Hours;
        int newThreadsInPast24Hours;
        int newUsersInPast24Hours;
        int mostViewsPostId;
        string mostViewsSubject			= "";
        int mostActivePostId;
        string mostActiveSubject		= "";
        int mostReadPostId;
        string mostReadPostSubject		= "";
        string mostActiveUser			= "";
        int mostActiveUserID;
        string newestUser				= "";
        int newestUserID;
        static Version version;

        public static SiteStatistics LoadSiteStatistics(HttpContext context, bool cacheable, int cacheTimeInHours) {
#if DEBUG_NOCACHE
            cacheable = false;
#endif

            // Cached lookup
            //
            if ( HttpRuntime.Cache["SiteStatistics"] != null) {

                return (SiteStatistics) HttpRuntime.Cache["SiteStatistics"];

            } else {

                // Create Instance of the ForumsDataProvider
                ForumsDataProvider dp = ForumsDataProvider.Instance();

                SiteStatistics stats = dp.LoadSiteStatistics(cacheTimeInHours);

                if (cacheable)
                    HttpRuntime.Cache.Insert("SiteStatistics", stats, null, DateTime.Now.AddMinutes(10), TimeSpan.Zero);

                return stats;
            }
        }

        public static Version AspNetForumsVersion {
            get {
                if (version == null) {
                    Type t = Type.GetType("Spinit.Wpc.Forum.Components.ForumsDataProvider");
                    version = t.Assembly.GetName().Version;
                }

                return version;
            }
        }

        public static Version CheckVersion() {
            ForumContext forumContext = ForumContext.Current;
            Version version = null;
            string cacheKey = "versionCheck";

            if (Globals.GetSiteSettings().EnableVersionCheck) {

                if (forumContext.Context.Cache[cacheKey] == null) {

                    try {
                        XmlTextReader reader = new XmlTextReader("http://www.communityserver.org/forums/builds/version.xml");

                        XmlDocument doc = new XmlDocument();
 
                        doc.Load(reader);

                        // Find the most recent version
                        //
                        version = new Version( doc.SelectSingleNode("root/mostRecentVersion").InnerText );

                        forumContext.Context.Cache.Insert(cacheKey, version, null, DateTime.Now.AddDays(1), TimeSpan.Zero);

                    } catch {

#if RELEASE
                        forumContext.Cache.Insert(cacheKey, 0, null, DateTime.Now.AddDays(1), TimeSpan.Zero);
#endif

                        throw new Exception("Unable to perform version check.");
                    }

                }
                
                version = (Version) forumContext.Context.Cache[cacheKey];

            } else {
                throw new Exception("Version checking is not enabled.");
            }

            return version;
        }

        public static bool NewVersionAvailable (Version versionToCompare) {

            string[] runningVersion = SiteStatistics.AspNetForumsVersion.ToString(3).Split('.');
            string[] compareVersion = versionToCompare.ToString(3).Split('.');

            if (( int.Parse(compareVersion[0]) > int.Parse(runningVersion[0]) ) ||
                ( 
					int.Parse(compareVersion[0]) == int.Parse(runningVersion[0]) &&
					int.Parse(compareVersion[1]) > int.Parse(runningVersion[1]) ) ||
				( 
					int.Parse(compareVersion[0]) == int.Parse(runningVersion[0]) &&
					int.Parse(compareVersion[1]) == int.Parse(runningVersion[1]) &&
					int.Parse(compareVersion[2]) > int.Parse(runningVersion[2]) ) )
                return true;

            return false;

        }

        public static bool RunningModedVersion (Version versionToCompare) {

            string[] runningVersion = SiteStatistics.AspNetForumsVersion.ToString(3).Split('.');
            string[] compareVersion = versionToCompare.ToString(3).Split('.');


            if (!NewVersionAvailable(versionToCompare)) {
                if ( int.Parse(compareVersion[2]) < int.Parse(runningVersion[2]) )
                    return true;
            }

            return false;

        }

        public ArrayList ActiveUsers {
            get { return activeUsers; }
            set { activeUsers = value; }
        }

        public ArrayList ActiveModerators {
            get { return activeModerators; }
            set { activeModerators = value; }
        }

        public ArrayList ModerationActions {
            get { return moderationActions; }
            set { moderationActions = value; }
        }

        public int CurrentAnonymousUsers {
            get { return currentAnonymousUsers; }
            set { currentAnonymousUsers = value; }
        }

        public int TotalUsers {
            get { return totalUsers; }
            set {
                if (value < 0)
                    totalUsers = 0;
                else
                    totalUsers = value;
            }
        }

        public int TotalAnonymousUsers {
            get { return totalAnonymousUsers; }
            set { totalAnonymousUsers = value; }
        }

        public int TotalModerators {
            get { return totalModerators; }
            set {
                if (value < 0)
                    totalModerators = 0;
                else
                    totalModerators = value;
            }
        }

        public int TotalModeratedPosts {
            get { return totalModeratedPosts; }
            set {
                if (value < 0)
                    totalModeratedPosts = 0;
                else
                    totalModeratedPosts = value;
            }
        }

        // *********************************************************************
        //  TotalPosts
        //
        /// <summary>
        /// Specifies the total number of posts made to the board.
        /// </summary>
        // ***********************************************************************/
        public int TotalPosts {
            get { return totalPosts; }
            set {
                if (value < 0)
                    totalPosts = 0;
                else
                    totalPosts = value;
            }
        }

        // *********************************************************************
        //  TotalThreads
        //
        /// <summary>
        /// Specifies the total number of threads (top-level posts) made.
        /// </summary>
        // ***********************************************************************/
        public int TotalThreads {
            get { return totalThreads; }
            set {
                if (value < 0)
                    totalThreads = 0;
                else
                    totalThreads = value;
            }
        }

        // *********************************************************************
        //  NewPostsInPast24Hours
        //
        /// <summary>
        /// Specifies the number of posts made in the last 24 hours.
        /// </summary>
        // ***********************************************************************/
        public int NewPostsInPast24Hours {
            get { return newPostsInPast24Hours; }
            set {
                if (value < 0)
                    newPostsInPast24Hours = 0;
                else
                    newPostsInPast24Hours = value;
            }
        }

        // *********************************************************************
        //  NewUsersInPast24Hours
        //
        /// <summary>
        /// Specifies the number of users added in the last 24 hours.
        /// </summary>
        // ***********************************************************************/
        public int NewUsersInPast24Hours {
            get { return newUsersInPast24Hours; }
            set {
                if (value < 0)
                    newUsersInPast24Hours = 0;
                else
                    newUsersInPast24Hours = value;
            }
        }

        // *********************************************************************
        //  NewThreadsInPast24Hours
        //
        /// <summary>
        /// Specifies the number of threads (top-level posts) made in the last 24 hours.
        /// </summary>
        // ***********************************************************************/
        public int NewThreadsInPast24Hours {
            get { return newThreadsInPast24Hours; }
            set {
                if (value < 0)
                    newThreadsInPast24Hours = 0;
                else
                    newThreadsInPast24Hours = value;
            }
        }

        // *********************************************************************
        //  MostViewsPostID
        //
        /// <summary>
        /// The Post with the most number of views in the past 3 days
        /// </summary>
        // ***********************************************************************/
        public int MostViewsPostID {
            get { return mostViewsPostId; }
            set {
                if (value < 0)
                    mostViewsPostId = 0;
                else
                    mostViewsPostId = value;
            }
        }

        // *********************************************************************
        //  MostViewsSubject
        //
        /// <summary>
        /// The Post with the most number of views in the past 3 days
        /// </summary>
        // ***********************************************************************/
        public String MostViewsSubject {
            get { return mostViewsSubject; }
            set {
                mostViewsSubject = value;
            }
        }

        // *********************************************************************
        //  MostActivePostID
        //
        /// <summary>
        /// The Post with the most replies in the past 3 days.
        /// </summary>
        // ***********************************************************************/
        public int MostActivePostID {
            get { return mostActivePostId; }
            set {
                if (value < 0)
                    mostActivePostId = 0;
                else
                    mostActivePostId = value;
            }
        }

        // *********************************************************************
        //  MostViewsSubject
        //
        /// <summary>
        /// The Post with the most replies in the past 3 days.
        /// </summary>
        // ***********************************************************************/
        public String MostActiveSubject {
            get { return mostActiveSubject; }
            set {
                mostActiveSubject = value;
            }
        }

        // *********************************************************************
        //  MostReadPostID
        //
        /// <summary>
        /// The Post the most number of users have read in the past 3 days
        /// </summary>
        // ***********************************************************************/
        public int MostReadPostID {
            get { return mostReadPostId; }
            set {
                if (value < 0)
                    mostReadPostId = 0;
                else
                    mostReadPostId = value;
            }
        }

        // *********************************************************************
        //  MostReadPostSubject
        //
        /// <summary>
        /// The Post the most number of users have read in the past 3 days
        /// </summary>
        // ***********************************************************************/
        public String MostReadPostSubject {
            get { return mostReadPostSubject; }
            set {
                mostReadPostSubject = value;
            }
        }

        // *********************************************************************
        //  MostActiveUser
        //
        /// <summary>
        /// The most active user
        /// </summary>
        // ***********************************************************************/
        public String MostActiveUser {
            get { return mostActiveUser; }
            set {
                mostActiveUser = value;
            }
        }

        public int MostActiveUserID {
            get {
                return mostActiveUserID;
            }
            set {
                mostActiveUserID = value;
            }
        }

        // *********************************************************************
        //  NewestUser
        //
        /// <summary>
        /// The newest user to join
        /// </summary>
        // ***********************************************************************/
        public String NewestUser {
            get { return newestUser; }
            set {
                newestUser = value;
            }
        }

        // *********************************************************************
        //  NewestUserID
        //
        /// <summary>
        /// The newest user to join
        /// </summary>
        // ***********************************************************************/
        public int NewestUserID {
            get { return newestUserID; }
            set {
                newestUserID = value;
            }
        }
    }
}
