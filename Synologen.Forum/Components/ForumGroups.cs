using System;
using System.Web;
using Spinit.Wpc.Forum.Components;
using System.Collections;
using Spinit.Wpc.Forum.Enumerations;

namespace Spinit.Wpc.Forum {

    // *********************************************************************
    //  ForumGroups
    //
    /// <summary>
    /// This class contains methods for working with the Forum Groups.
    /// </summary>
    /// 
    // ********************************************************************/ 
    public class ForumGroups {

        // *********************************************************************
        //  GetForumGroupByForumID
        //
        /// <summary>
        /// Returns the forum group for a given forum id.
        /// </summary>
        /// <param name="forumID">Forum id used to lookup forum</param>
        /// 
        // ********************************************************************/ 
        public static ForumGroup GetForumGroupByForumID (int forumID) {
            return GetForumGroupByForumID (forumID, true, false);
        }

        public static ForumGroup GetForumGroupByForumID (int forumID, bool cacheable) {
            return GetForumGroupByForumID (forumID, cacheable, false);
        }

        public static ForumGroup GetForumGroupByForumID (int forumID, bool cacheable, bool ignorePermissions) {
            ForumContext forumContext = ForumContext.Current;
            int forumGroupID;
            Hashtable forumGroups;
            Spinit.Wpc.Forum.Components.Forum forum;

            // Get the related forum
            //
            forum = Forums.GetForum(forumID);

            // Get the forum groups hashtable
            //
            forumGroups = GetForumGroups(forumContext, cacheable, ignorePermissions, forumContext.User.UserID);

            // Get the forum group id
            //
            forumGroupID = forum.ForumGroupID;

            // Return the forum group
            //
            if (forumGroups[forumGroupID] == null)
                throw new ForumException(ForumExceptionType.ForumGroupNotFound, forumGroupID.ToString());

            return (ForumGroup) forumGroups[forumGroupID];
        }


        // *********************************************************************
        //  GetForumGroup
        //
        /// <summary>
        /// Returns the forum group for a given forum group id.
        /// </summary>
        /// <param name="forumGroupID">Forum group id to return a forum group for.</param>
        /// <returns>The total number of forums.</returns>
        /// 
        // ********************************************************************/ 
        public static ForumGroup GetForumGroup (int forumGroupID) {
            return GetForumGroup(forumGroupID, true, false);
        }

        public static ForumGroup GetForumGroup (int forumGroupID, bool cacheable) {
            return GetForumGroup(forumGroupID, cacheable, false);
        }

        public static ForumGroup GetForumGroup (int forumGroupID, bool cacheable, bool ignorePermissions) {
            ForumContext forumContext = ForumContext.Current;

            return GetForumGroup (forumGroupID, cacheable, ignorePermissions, forumContext.User.UserID);

        }

        public static ForumGroup GetForumGroup (int forumGroupID, bool cacheable, bool ignorePermissions, int userID) {
            ForumContext forumContext = ForumContext.Current;
            Hashtable forumGroups;

            // Get the forum groups hashtable
            //
            forumGroups = GetForumGroups(forumContext, cacheable, ignorePermissions, forumContext.User.UserID);

            // Return the forum group
            //
            if (forumGroups[forumGroupID] == null)
                throw new ForumException(ForumExceptionType.ForumGroupNotFound, forumGroupID.ToString());

            return (ForumGroup) forumGroups[forumGroupID];

        }

        // *********************************************************************
        //  ChangeForumGroupSortOrder
        //
        /// <summary>
        /// Used to change the sort order of forum groups
        /// </summary>
        /// <param name="forumGroupID">Id of forum group to move</param>
        /// <param name="moveUp">True to indicate that the forum is moving up.</param>
        /// 
        // ********************************************************************/ 
        public static void ChangeForumGroupSortOrder(int forumGroupID, bool moveUp) {
            ForumsDataProvider dp = ForumsDataProvider.Instance();

            dp.ChangeForumGroupSortOrder(forumGroupID, moveUp);
        }

        // *********************************************************************
        //  GetAllForumGroups
        //
        /// <summary>
        /// Returns a list of all forum groups
        /// </summary>
        /// <param name="displayForumGroupsNotApproved">If true returns all forum groups</param>
        /// <param name="allowFromCache">Whether or not the request can be satisfied from the Cache</param>
        /// 
        // ********************************************************************/ 
        public static ArrayList GetForumGroups (bool cacheable) {
            //int userID = Users.GetUser().UserID;			// switching to the forum's cache
			ForumContext forumContext = ForumContext.Current;

            return GetForumGroups(cacheable, false, forumContext.User.UserID);
        }

        public static ArrayList GetForumGroups (bool cacheable, bool ignorePermissions) {
            //int userID = Users.GetUser(true).UserID;		// switching to the forum's cache
			ForumContext forumContext = ForumContext.Current;

            return GetForumGroups(cacheable, ignorePermissions, forumContext.User.UserID);
        }

        public static ArrayList GetForumGroups (bool cacheable, bool ignorePermissions, int userID) {
            ForumContext forumContext = ForumContext.Current;
            ArrayList forumGroups = new ArrayList();
            Hashtable forumGroupsTable;

            // Get the ForumGroups as a hashtable
            //
            forumGroupsTable = GetForumGroups(forumContext, cacheable, ignorePermissions, userID);

            // Hide forum groups based on the user's cookie
            //
            forumGroupsTable = (Hashtable) HideForumGroups(forumGroupsTable);

            // Convert the hashtable to an arraylist
            //
            foreach(ForumGroup fg in forumGroupsTable.Values)
                forumGroups.Add( fg );

            // Sort the forum groups
            //
            forumGroups.Sort();

            return forumGroups;
        }

        private static Hashtable HideForumGroups (Hashtable forumGroups) {
            string[] hiddenForumGroups;

            // Process any hidden forums for this user. When a forum group's
            // HideForum property is set to true, the forum group is shown
            // but the contained forums are not displayed
            //
            hiddenForumGroups = Users.GetUser().GetUserCookie().HiddenForumGroups;

            for (int i = 0; i < hiddenForumGroups.Length; i++) {

                // Convert the value to an integer if we fail ignore
                //
                try {
                    int forumGroupID = Convert.ToInt32( hiddenForumGroups[i] );

                    // Look in the forum group hashtable and hide the forum group
                    //
                    if (forumGroups.ContainsKey(forumGroupID))
                        ((ForumGroup) forumGroups[forumGroupID]).HideForums = true;
                } catch {}

            }

            return forumGroups;
        }

        private static Hashtable GetForumGroups(ForumContext forumContext, bool cacheable, bool ignorePermissions, int userID) {
            string cacheKey = "AllForumGroups-" + Globals.GetSiteSettings().SiteID.ToString();
            Hashtable unfilteredForumGroups;
            Hashtable forumGroups;

            // Evict item from cache if not cacheable
            //
            if (!cacheable)
                forumContext.Context.Cache.Remove(cacheKey);

            // Get the raw forum groups
            //
            if ( forumContext.Context.Cache[cacheKey] == null ) {
                unfilteredForumGroups = ForumsDataProvider.Instance().GetForumGroups(Globals.GetSiteSettings().SiteID);

                if (cacheable)
                    forumContext.Context.Cache.Insert(cacheKey, unfilteredForumGroups, null, DateTime.Now.AddHours(1), TimeSpan.Zero);

            } else {
                unfilteredForumGroups = (Hashtable) forumContext.Context.Cache[cacheKey];
            }

            // Are we ignoring permissions?
            //
            if (ignorePermissions)
                return unfilteredForumGroups;

            // We need to create a new hashtable
            //
            forumGroups = new Hashtable();

            // Filter the list of forum groups to only show forums groups
            // this user is allowed to see, i.e. if all the forums in this
            // group are private and the user is not allowed to see them
            //
            foreach (ForumGroup f in unfilteredForumGroups.Values) {

                // Get the associated forums
                //
                if (!cacheable) 
                    forumGroups.Add(f.ForumGroupID, f);
                else if (Forums.GetForumsByForumGroupID(f.ForumGroupID, cacheable, ignorePermissions, userID).Count > 0)
                    forumGroups.Add(f.ForumGroupID, f);

            }

            return forumGroups;

        }

        
        
        // *********************************************************************
        //  AddForumGroup
        //
        /// <summary>
        /// Adds a new forum group
        /// </summary>
        /// <param name="forumGroupName">Name of new forum group to create.</param>
        /// 
        // ********************************************************************/ 
        public static int AddForumGroup(ForumGroup group) {
            // Create Instance of the ForumsDataProvider
            ForumsDataProvider dp = ForumsDataProvider.Instance();

            return dp.CreateUpdateDeleteForumGroup(group, DataProviderAction.Create);
        }

        // *********************************************************************
        //  UpdateForumGroup
        //
        /// <summary>
        /// Update a forum group name
        /// </summary>
        /// <param name="forumGroupName">new name value</param>
        /// <param name="forumGroupId">id of forum group to replace new name value with</param>
        // ********************************************************************/ 
        public static void UpdateForumGroup(ForumGroup group) {
            // Create Instance of the IDataProvider
            ForumsDataProvider dp = ForumsDataProvider.Instance();

            dp.CreateUpdateDeleteForumGroup(group, DataProviderAction.Update);
        }

        
        // *********************************************************************
        //  DeleteForumGroup
        //
        /// <summary>
        /// Deletes a forum group.
        /// </summary>
        /// <param name="forumGroupID">ID of forum group to delete.</param>
        // ********************************************************************/ 
        public static void DeleteForumGroup (int forumGroupID) {
            ForumsDataProvider dp = ForumsDataProvider.Instance();

            try {
                ForumGroup f = ForumGroups.GetForumGroup(forumGroupID, false);
                dp.CreateUpdateDeleteForumGroup(f, DataProviderAction.Delete);
            } catch (ForumException exception) {

                if (exception.ExceptionType == ForumExceptionType.ForumGroupNotFound)
                    return;
                else 
                    throw exception;
            }

        }

    }
}
