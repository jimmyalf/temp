using System;
using System.Web;
using System.Collections;
using Spinit.Wpc.Forum.Components;
using System.Web.UI.WebControls;
using Spinit.Wpc.Forum.Enumerations;

namespace Spinit.Wpc.Forum {

    // *********************************************************************
    //  Forums
    //
    /// <summary>
    /// This class contains methods for working with the Forums.
    /// </summary>
    /// 
    // ********************************************************************/ 
    public class Forums {

        public static void MarkAllForumsRead(int userID, int forumGroupID, int forumID, bool markThreadsRead) {

            ForumsDataProvider dp = ForumsDataProvider.Instance();

            dp.MarkAllForumsRead(userID, forumGroupID, forumID, markThreadsRead);

        }

        // *********************************************************************
        //  GetForums
        //
        /// <summary>
        /// Returns all of the forums in the database.
        /// </summary>
        /// <param name="ShowAllForums">If ShowAllForums is true, ALL forums, active and nonactive,
        /// are returned.  If ShowAllForums is false, just the active forums are returned.</param>
        /// <returns>A ArrayList with all of the active forums, or all of the active and nonactive
        /// forums, depending on the value of the ShowAllForums property.</returns>
        /// 
        // ***********************************************************************/
        public static ArrayList GetForums() {
            return GetForums(0, false, true);
        }

        public static ArrayList GetForums(int userID, bool ignorePermissions) {
            return GetForums(userID, ignorePermissions, true);
        }

        public static ArrayList GetForums(int userID, bool ignorePermissions, bool cacheable) {
            ForumContext forumContext = ForumContext.Current;
            ArrayList forums = new ArrayList();
            Hashtable forumsTable;

            // Get the ForumGroups as a hashtable
            //
            forumsTable = GetForums(forumContext, userID, ignorePermissions, cacheable);

            // Convert the hashtable to an arraylist
            //
            foreach (Spinit.Wpc.Forum.Components.Forum forum in forumsTable.Values)
                if (forum.ParentID == 0)
                    forums.Add( forum );

            // Sort the forum groups
            //
            forums.Sort();

            return forums;
        }

        public static Hashtable GetForums(ForumContext forumContext, int userID, bool ignorePermissions, bool cacheable) {
            Hashtable unfilteredForums;
            Hashtable forums;
            string cacheKey = "Forums-" + userID + Globals.GetSiteSettings().SiteID.ToString() + ignorePermissions.ToString() + cacheable.ToString();

#if DEBUG_NOCACHE
            cacheable = false;
#endif
            // Have we already fetched for this request?
            //
            if (forumContext.Context.Items[cacheKey] != null)
                return (Hashtable) forumContext.Context.Items[cacheKey];

            // Ensure it's not in the cache
            if ((!cacheable) && (forumContext.Context.Items[cacheKey] != null))
                HttpRuntime.Cache[cacheKey] = null;

            // Get the raw forum groups
            //
            if ( HttpRuntime.Cache[cacheKey] == null ) {
                unfilteredForums = ForumsDataProvider.Instance().GetForums(Globals.GetSiteSettings().SiteID, userID, ignorePermissions);

                // Dynamically add the special forum for private messages
                //
                unfilteredForums.Add( 0, PrivateForum() );

                // Cache if we can
                //
                if (cacheable)
                    HttpRuntime.Cache.Insert(cacheKey, unfilteredForums, null, DateTime.Now.AddSeconds(30), TimeSpan.Zero);

            } else {
                unfilteredForums = (Hashtable) HttpRuntime.Cache[cacheKey];
            }

            // Are we ignoring permissions?
            //
            if (ignorePermissions)
                return unfilteredForums;

            // We need to create a new hashtable
            //
            forums = new Hashtable();

            // Filter the list of forums to only show forums this user 
            // is allowed to see
            //
            foreach (Spinit.Wpc.Forum.Components.Forum f in unfilteredForums.Values)
            {

                // The forum is added if the user can View, Read
                //
                if (( f.Permission.View != AccessControlEntry.Deny ) 
					||	( f.Permission.Read != AccessControlEntry.Deny ) 
					||	(ignorePermissions) ) {
                    
					forums.Add(f.ForumID, f);

                }
            }

            // Insert into request cache
            //
            forumContext.Context.Items.Add(cacheKey, forums);

            return forums;

        }

        // *********************************************************************
        //  PrivateForum
        //
        /// <summary>
        /// Used to create and return the special private messages forum
        /// </summary>
        /// 
        // ***********************************************************************/
        private static Spinit.Wpc.Forum.Components.Forum PrivateForum()
        {
            Spinit.Wpc.Forum.Components.Forum f = new Spinit.Wpc.Forum.Components.Forum(0);

            // Set the forum name
            //
            f.Name = "Private Messages";
            f.IsActive = true;
            f.IsPrivate = true;

            // Create a permission set to allow all actions
            //
            f.Permission = new ForumPermission();

			// EAD 7/14/2004 : The new permissions now allow all,
			// where we must explicitly Deny access to items we do not want.
			//
			/*
            f.Permission.Announce = AccessControlEntry.Allow;
            f.Permission.CreatePoll = AccessControlEntry.Allow;
            f.Permission.Delete = AccessControlEntry.Allow;
            f.Permission.Edit = AccessControlEntry.Allow;
            f.Permission.Post = AccessControlEntry.Allow;
            f.Permission.Read = AccessControlEntry.Allow;
            f.Permission.Reply = AccessControlEntry.Allow;
            f.Permission.View = AccessControlEntry.Allow;
            f.Permission.Vote = AccessControlEntry.Allow;
			*/
			f.Permission.Attachment = AccessControlEntry.Deny;
			f.Permission.Moderate = AccessControlEntry.Deny;
			f.Permission.Sticky = AccessControlEntry.Deny;

            return f;
        }

        // *********************************************************************
        //  GetForumsByForumGroupID
        //
        /// <summary>
        /// Used to return a narrow collection of forums that belong to a given forum id.
        /// The username is provied for personalization, e.g. if the user has new
        /// posts in the forum
        /// </summary>
        /// <param name="forumGroupId">Forum Group ID to retrieve forums for</param>
        /// <param name="username">Username making the request</param>
        /// 
        // ***********************************************************************/
        public static ArrayList GetForumsByForumGroupID (int forumGroupID) {
            return GetForumsByForumGroupID(forumGroupID, true, false);
        }

        public static ArrayList GetForumsByForumGroupID (int forumGroupID, bool cacheable) {

            return GetForumsByForumGroupID (forumGroupID, cacheable, false);
        }

        public static ArrayList GetForumsByForumGroupID (int forumGroupID, bool cacheable, bool ignorePermissions) {
            ForumContext forumContext = ForumContext.Current;

            return GetForumsByForumGroupID (forumGroupID, cacheable, ignorePermissions, forumContext.User.UserID);
        }

        public static ArrayList GetForumsByForumGroupID (int forumGroupID, bool cacheable, bool ignorePermissions, int userID) {		
			ArrayList allForums = Forums.GetForums(userID, ignorePermissions, cacheable);
            ArrayList forums = new ArrayList();

            foreach (Spinit.Wpc.Forum.Components.Forum f in allForums)
            {
                if (f.ForumGroupID == forumGroupID)
                    forums.Add(f);
            }

            return forums;

        }



        // *********************************************************************
        //  GetForum
        //
        /// <summary>
        /// Returns information on a particular forum.
        /// </summary>
        /// <param name="ForumID">The ID of the Forum that you are interested in.</param>
        /// <returns>A Forum object with information about the specified forum.</returns>
        /// <remarks>If the passed in ForumID is not found, a ForumNotFoundException exception is
        /// thrown.</remarks>
        /// 
        // ***********************************************************************/
        public static Spinit.Wpc.Forum.Components.Forum GetForum(int forumID)
        {
            return GetForum(forumID, true, false);
        }

        public static Spinit.Wpc.Forum.Components.Forum GetForum(int forumID, bool cacheable)
        {
            return GetForum(forumID, cacheable, false);
        }

        public static Spinit.Wpc.Forum.Components.Forum GetForum(int forumID, bool cacheable, bool ignorePermissions)
        {
            ForumContext forumContext = ForumContext.Current;

            return GetForum(forumID, cacheable, ignorePermissions, forumContext.User.UserID);
        }

        public static Spinit.Wpc.Forum.Components.Forum GetForum(int forumID, bool cacheable, bool ignorePermissions, int userID)
        {
            ForumContext forumContext = ForumContext.Current;

            return (Spinit.Wpc.Forum.Components.Forum)
                GetForums(forumContext, userID, ignorePermissions, cacheable)[forumID];
        }

        public static Spinit.Wpc.Forum.Components.Forum GetForumByPostID(int postID)
        {
            return GetForumByPostID(postID, Users.GetUser(true).UserID);
        }

        public static Spinit.Wpc.Forum.Components.Forum GetForumByPostID(int postID, int userID)
        {
            return GetForumByPostID(postID, userID, true);
        }

        public static Spinit.Wpc.Forum.Components.Forum GetForumByPostID(int postID, int userID, bool cacheable)
        {

            Hashtable postToForumLookUpTable = null;
            ForumContext forumContext = ForumContext.Current;
            string cacheKey = "PostToForumLookUpTable";
            Spinit.Wpc.Forum.Components.Forum forum;

            // First check if we have the postid in our hashtable
            //
            if (cacheable)
                postToForumLookUpTable = (Hashtable) HttpRuntime.Cache[cacheKey];

            if (postToForumLookUpTable == null) {
                HttpRuntime.Cache.Insert(cacheKey, new Hashtable());

                postToForumLookUpTable = (Hashtable) HttpRuntime.Cache[cacheKey];
            }

            if (postToForumLookUpTable[postID] != null)
                return (Spinit.Wpc.Forum.Components.Forum)postToForumLookUpTable[postID];

            // We don't have it in our lookup table
            //
            ForumsDataProvider dp = ForumsDataProvider.Instance();

            // Retrieve from the database
            //
            forum = Forums.GetForum( dp.GetForumIDByPostID(userID, postID) );
            
            // Store in the hashtable
            //
            postToForumLookUpTable[postID] = forum;

            return forum;

        }

        // *********************************************************************
        //  DeleteForum
        //
        /// <summary>
        /// Deletes a forum and all of the posts in the forum.
        /// </summary>
        /// <param name="ForumID">The ID of the forum to delete.</param>
        /// <remarks>Be very careful when using this method.  The specified forum and ALL of its posts
        /// will be deleted.</remarks>
        /// 
        // ***********************************************************************/
        public static void DeleteForum (int forumID) {
            Spinit.Wpc.Forum.Components.Forum forum = new Spinit.Wpc.Forum.Components.Forum(forumID);

            CreateUpdateDeleteForum(forum, DataProviderAction.Delete);
        }

        // *********************************************************************
        //  AddForum
        //
        /// <summary>
        /// Creates a new forum.
        /// </summary>
        /// <param name="forum">Specifies information about the forum to create.</param>
        /// 
        // ***********************************************************************/
        public static int AddForum(Spinit.Wpc.Forum.Components.Forum forum)
        {
            return CreateUpdateDeleteForum(forum, DataProviderAction.Create);
        }


        // *********************************************************************
        //  UpdateForum
        //
        /// <summary>
        /// Updates a particular forum.
        /// </summary>
        /// <param name="forum">Specifies information about the forum to update.  The ForumID property
        /// indicates what forum it is that you wish to update.</param>
        /// 
        // ***********************************************************************/
        public static int UpdateForum(Spinit.Wpc.Forum.Components.Forum forum)
        {
            return CreateUpdateDeleteForum(forum, DataProviderAction.Update);
        }

        private static int CreateUpdateDeleteForum(Spinit.Wpc.Forum.Components.Forum forum, DataProviderAction action)
        {
            // Create Instance of the ForumsDataProvider
            ForumsDataProvider dp = ForumsDataProvider.Instance();

            return dp.CreateUpdateDeleteForum(forum, action);
        }

        public static void SetForumSubscriptionType(int ForumID, int SubscriptionType) {
            ForumContext forumContext = ForumContext.Current;
            ForumsDataProvider dp = ForumsDataProvider.Instance();
            dp.SetForumSubscriptionType(ForumID, forumContext.User.UserID, SubscriptionType);
        }

        public static int GetForumSubscriptionType(int ForumID) {
            ForumContext forumContext = ForumContext.Current;

            if (forumContext.User.IsAnonymous)
                return 0;

            ForumsDataProvider dp = ForumsDataProvider.Instance();
            return dp.GetForumSubscriptionType(ForumID, forumContext.User.UserID);
        }

        // ********************************************************************* 
        //  ChangeForumSortOrder 
        // 
        /// <summary> 
        /// Used to change the sort order of forums 
        /// </summary> 
        /// <param name="forumGroupID">Id of forum to move</param> 
        /// <param name="moveUp">True to indicate that the forum is moving up.</param> 
        /// 
        // ********************************************************************/ 
        public static void ChangeForumSortOrder(int forumID, bool moveUp) { 
            ForumsDataProvider dp = ForumsDataProvider.Instance(); 

            dp.ChangeForumSortOrder(forumID, moveUp); 
        } 


    }
}
