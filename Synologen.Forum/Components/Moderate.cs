using System;
using System.Web;
using System.Collections;
using Spinit.Wpc.Forum;
using Spinit.Wpc.Forum.Components;
using Spinit.Wpc.Forum.Enumerations;


namespace Spinit.Wpc.Forum {

    // *********************************************************************
    //  Moderate
    //
    /// <summary>
    /// This class contains methods that are helpful for moderating posts.
    /// </summary>
    /// 
    // ********************************************************************/ 
    public class Moderate {

        public static PostSet GetPosts(int forumID, int pageIndex, int pageSize, int sortBy, int sortOrder) {

            ForumsDataProvider dp = ForumsDataProvider.Instance();

            return dp.GetPostsToModerate(forumID, pageIndex, pageSize, sortBy, sortOrder, ForumContext.Current.User.UserID, true);

        }

        public static void TogglePostSettings (ModeratePostSetting setting, Post post, int moderatorID) {

            ForumsDataProvider dp = ForumsDataProvider.Instance();

            dp.TogglePostSettings(setting, post, moderatorID);
        }

        public static void ToggleUserSettings (ModerateUserSetting setting, User user, int moderatorID) {
            ForumsDataProvider dp = ForumsDataProvider.Instance();

            dp.ToggleUserSettings(setting, user, moderatorID);

        }

        // *********************************************************************
        //  CanEditPost
        //
        /// <summary>
        /// This method determines whether or not a user can edit a particular post.
        /// </summary>
        /// <param name="strUsername">The username of the user who you wish to know if he can edit
        /// the post.</param>
        /// <param name="iPostID">To post you wish to know if the user can edit.</param>
        /// <returns>A boolean value: true if the user can edit the post, false otherwise.</returns>
        /// <remarks>Moderators can edit posts that are still waiting for approval in the forum(s) they 
        /// are cleared to moderate.  Forum administrators may edit any post, awaiting approval or not,
        /// at any time.</remarks>
        /// 
        // ********************************************************************/ 
        public static bool CanEditPost(String Username, int PostID) {			
            // Create Instance of the ForumsDataProvider
            ForumsDataProvider dp = ForumsDataProvider.Instance();

            return dp.CanEditPost(Username, PostID);
        }

        // *********************************************************************
        //  GetQueueStatus
        //
        /// <summary>
        /// Returns details about the moderation queue
        /// </summary>
        /// <param name="username">The username making the request</param>
        /// <param name="forumId">Forum to check-on</param>
        /// <returns>A boolean value: true if the user can edit the post, false otherwise.</returns>
        /// <remarks>Moderators can edit posts that are still waiting for approval in the forum(s) they 
        /// are cleared to moderate.  Forum administrators may edit any post, awaiting approval or not,
        /// at any time.</remarks>
        /// 
        // ********************************************************************/ 
        public static ModerationQueueStatus GetQueueStatus(int forumID, string username) {
            // Create Instance of the ForumsDataProvider
            ForumsDataProvider dp = ForumsDataProvider.Instance();

            return dp.GetQueueStatus(forumID, username);
        }

        // *********************************************************************
        //  GetForumsByForumGroupId
        //
        /// <summary>
        /// Returns forums requiring moderation that the user can moderate by forum group id
        /// </summary>
        /// <param name="forumGroupID">Id of the forum group to return forums for</param>
        /// <returns>An ArrayList populated with Spinit.Wpc.Forum.Components.Forum objects that require moderation.</returns>
        /// <remarks>The Moderate permission on the forum must be set the AccessControlEntry.Allow for the forum to be returned.</remarks>
        // ********************************************************************/ 
        public static ArrayList GetForumsByForumGroupID (int forumGroupID) {

            ArrayList unfilteredForums = Moderate.GetForumsToModerate();
            ArrayList forums = new ArrayList();

            foreach (Spinit.Wpc.Forum.Components.Forum f in unfilteredForums)
            {

                if (f.ForumGroupID == forumGroupID)
                    if (f.PostsToModerate > 0)
                        forums.Add(f);
            }

            return forums;

        }

        #region Forums to Moderate
        public static ArrayList GetForumsToModerate () {
            return GetForumsToModerate(Globals.GetSiteSettings().SiteID, Users.GetUser().UserID);
        }

        public static ArrayList GetForumsToModerate (int siteID) {
            return GetForumsToModerate(siteID, Users.GetUser().UserID);
        }

        public static ArrayList GetForumsToModerate (int siteID, int userID) {
            string cacheKey = "ForumsToModerate-" + siteID + userID;

            if (ForumContext.Current.Context.Items[cacheKey] == null)
                ForumContext.Current.Context.Items[cacheKey] = ForumsDataProvider.Instance().GetForumsToModerate( siteID, userID);

            return (ArrayList) ForumContext.Current.Context.Items[cacheKey];
        }
        #endregion

        // *********************************************************************
        //  MovePost
        //
        /// <summary>
        /// Moves a post from one Forum to another.
        /// </summary>
        /// <param name="postID">The post to move.</param>
        /// <param name="moveToForumID">The forum to move the post to.</param>
        /// <param name="movedBy">The user who moved the post.</param>
        /// <param name="sendEmail">Send an email to the post author that the post was moved.</param>
        /// <returns>A MovedPostStatus enumeration value that indicates the status of the attempted move.
        /// This enumeration has four values: NotMoved, MovedButNotApproved, MovedAndApproved and MovedAlreadyApproved.</returns>
        /// <remarks>Moving an unapproved post from one forum to another will approve the post if the user
        /// has Moderate permission on the destination forum.</remarks>
        // ********************************************************************/
        public static MovedPostStatus MovePost(Post post, int moveToForumID, int movedBy, bool sendEmail) {

            // Create Instance of the ForumsDataProvider
            ForumsDataProvider dp = ForumsDataProvider.Instance();
            MovedPostStatus status = dp.MovePost(post.PostID, moveToForumID, movedBy);

            if (sendEmail) {
                if (status == MovedPostStatus.MovedAndApproved) {
                    // The post was moved to a new forum and automatically approved
                    // Send an email explaining what happened, and send the thread tracking emails
                    Emails.ForumTracking(post);

                    Emails.PostMovedAndApproved (post);
                }
                else if (status == MovedPostStatus.MovedAlreadyApproved) {
                    // The post was moved and has previously been approved.
                    Emails.PostMoved(post);
                }
            }

            return status;
        }

        // *********************************************************************
        //  ThreadSplit
        //
        /// <summary>
        /// Splits a thread into a new thread.
        /// </summary>
        /// <param name="postID">The post to split.</param>
        /// <param name="moveToForumID">The forum to move the post to.</param>
        /// <param name="movedBy">The user who moved the post.</param>
        /// <param name="sendEmail">Send an email to the post author that the post was moved.</param>
        // ********************************************************************/
        public static void ThreadSplit (Post post, int moveToForumID, int movedBy, bool sendEmail) {

            // Create Instance of the ForumsDataProvider
            ForumsDataProvider dp = ForumsDataProvider.Instance();
            dp.ThreadSplit(post.PostID, moveToForumID, movedBy);

            if (sendEmail)
                Emails.ForumTracking (post);

            return;
        }

        // *********************************************************************
        //  ThreadJoin
        //
        /// <summary>
        /// Joins two threads into one.
        /// </summary>
        /// <param name="postID">The post to split.</param>
        /// <param name="moveToForumID">The forum to move the post to.</param>
        /// <param name="movedBy">The user who moved the post.</param>
        /// <param name="sendEmail">Send an email to the post author that the post was moved.</param>
        // ********************************************************************/
        public static void ThreadJoin (Post parentThread, Post childThread, int joinedByUserID, bool sendEmail) {

            // Create Instance of the ForumsDataProvider
            ForumsDataProvider dp = ForumsDataProvider.Instance();
            dp.ThreadJoin(parentThread.ThreadID, childThread.ThreadID, joinedByUserID);

            if (sendEmail) {
                Emails.ThreadJoined(parentThread, childThread);
            }

            return;
        }

        // *********************************************************************
        //  DeletePost
        //
        /// <summary>
        /// This method deletes a post and all of it's replies by default.
        /// It also includes a reason why the post was deleted.
        /// </summary>
        /// <param name="postID">The post to delete.</param>
        /// <param name="deletedBy">The user who deleted the post.</param>
        /// <param name="reason">The reason why the post was deleted.</param>
        /// <param name="sendEmail">Send an email to the post author that the post was deleted.</param>
        // ********************************************************************/ 
        public static void DeletePost(Post post, User moderatedBy, string reason, bool deleteChildPosts) {

            // Was a reason provided?
            //
            if (reason.Length == 0)
                reason = "No Reason Given.";

            // Create Instance of the ForumsDataProvider
            //
            ForumsDataProvider dp = ForumsDataProvider.Instance();
            dp.ModeratorDeletePost(post.PostID, moderatedBy.UserID, reason, deleteChildPosts);

			// send the email if successful
			//
			Emails.PostRemoved(post, moderatedBy, reason);
        }

        // *********************************************************************
        //  ApprovePost
        //
        /// <summary>
        /// Approves a moderated post.
        /// </summary>
        /// <param name="postID">The post to approve.</param>
        /// <param name="approvedBy">Username approving the post.</param>
        /// <param name="updateUserAsTrusted">Approve the username as no longer requiring moderation</param>
        /// 
        // ********************************************************************/ 
        public static void ApprovePost (Post post, int moderatedBy) {
            bool approved = true;

            // Create Instance of the ForumsDataProvider
            //
            ForumsDataProvider dp = ForumsDataProvider.Instance();

            approved = dp.ApprovePost(post.PostID, moderatedBy);

            // Post hadn't been approved before, send confirmation
            if (approved) {
                Emails.ForumTracking(post);
				Emails.PostApproved(post);
            }

        }
		
        // *********************************************************************
        //  CanModerate
        //
        /// <summary>
        /// Determines whether or not a user can moderate any forums.
        /// </summary>  
        /// <param name="Username">The user who you are interested in determining has moderation
        /// capabilities.</param>
        /// <returns>A boolean, indicating whether or not the user can moderate.</returns>
        /// <remarks>This method does not indicate *what* forums a user can moderate, it just serves to
        /// determine IF a user can moderate at all.</remarks>
        /// 
        // ********************************************************************/ 
        public static bool CheckIfUserIsModerator (int userID, int forumID) {

            // Create Instance of the ForumsDataProvider
            ForumsDataProvider dp = ForumsDataProvider.Instance();

            return dp.CheckIfUserIsModerator(userID, forumID);

        }


        // *********************************************************************
        //  GetPostsAwaitingModeration
        //
        /// <summary>
        /// Returns a bool indicating whether the given user has posts to moderate
        /// </summary>
        /// <param name="username">The user name of the individual currently moderating.</param>
        /// 
        // ********************************************************************/ 
        public static bool UserHasPostsAwaitingModeration(String username) {
            // Create Instance of the ForumsDataProvider
            ForumsDataProvider dp = ForumsDataProvider.Instance();

            return dp.UserHasPostsAwaitingModeration(username);
        }

        // *********************************************************************
        //  GetForumModeratorRoles
        //
        /// <summary>
        /// Returns a list of roles that can moderate a particular forum
        /// </summary>
        /// <param name="username">The user name of the individual currently moderating.</param>
        /// 
        // ********************************************************************/ 
        public static ArrayList GetForumModeratorRoles (int forumID) {
            ForumContext forumContext = ForumContext.Current;
            string cacheKey = "ModeratorRoleList-" + forumID.ToString();

            if (forumContext.Context.Cache[cacheKey] == null) {
                // Create Instance of the ForumsDataProvider
                ForumsDataProvider dp = ForumsDataProvider.Instance();

                ArrayList roles = dp.GetForumModeratorRoles(forumID);

                forumContext.Context.Cache.Insert(cacheKey, roles, null, DateTime.Now.AddHours(6), TimeSpan.Zero);
            }

            return (ArrayList) forumContext.Context.Cache[cacheKey];

        }    
    }
}