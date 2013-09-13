using System;
using Spinit.Wpc.Forum.Enumerations;
using Spinit.Wpc.Forum.Components;

namespace Spinit.Wpc.Forum.Components {

    /// <summary>
    /// Summary description for ForumPermission.
    /// </summary>
    public class ForumPermission {

        #region Member variables
        string roleName;
        int roleID;
        int forumID;

        AccessControlEntry view        = AccessControlEntry.NotSet;
        AccessControlEntry read        = AccessControlEntry.NotSet;
        AccessControlEntry post        = AccessControlEntry.NotSet;
        AccessControlEntry delete      = AccessControlEntry.NotSet;
        AccessControlEntry reply       = AccessControlEntry.NotSet;
        AccessControlEntry edit        = AccessControlEntry.NotSet;
        AccessControlEntry sticky      = AccessControlEntry.NotSet;
        AccessControlEntry announce    = AccessControlEntry.NotSet;
        AccessControlEntry createPoll  = AccessControlEntry.NotSet;
        AccessControlEntry vote        = AccessControlEntry.NotSet;
        AccessControlEntry moderate    = AccessControlEntry.NotSet;
        AccessControlEntry attachment  = AccessControlEntry.NotSet;

        #endregion

        #region Public properties
        public int ForumID {
            get {
                return forumID;
            }
            set {
                forumID = value;
            }
        }

        public string Name {
            get {
                return roleName;
            }
            set {
                roleName = value;
            }
        }
       
        public int RoleID {
            get {
                return roleID;
            }
            set {
                roleID = value;
            }
        }

        public AccessControlEntry View {
            get { return view; }
            set { view = value; }
        }

        public AccessControlEntry Read {
            get { return read; }
            set { read = value; }
        }
        
        public AccessControlEntry Post {
            get { return post; }
            set { post = value; }
        }
        
        public AccessControlEntry Reply {
            get { return reply; }
            set { reply = value; }
        }
        
        public AccessControlEntry Edit {
            get { return edit; }
            set { edit = value; }
        }
        
        public AccessControlEntry Delete {
            get { return delete; }
            set { delete = value; }
        }

        public AccessControlEntry Sticky {
            get { return sticky; }
            set { sticky = value; }
        }
        
        public AccessControlEntry Announce {
            get { return announce; }
            set { announce = value; }
        }
        
        public AccessControlEntry CreatePoll {
            get { return createPoll; }
            set { createPoll = value; }
        }

        public AccessControlEntry Vote {
            get { return vote; }
            set { vote = value; }
        }

        public AccessControlEntry Moderate {
            get { return moderate; }
            set { moderate = value; }
        }

        public AccessControlEntry Attachment {
            get { return attachment; }
            set { attachment = value; }
        }
        #endregion

        #region Public Methods
        public static void AccessCheck (Forum forum, Permission permission) {
            AccessCheck (forum, permission, null);
        }
        public static void AccessCheck (Forum forum, Permission permission, Post post) {
            ForumContext forumContext = ForumContext.Current;

            if (Users.GetUser().IsAdministrator)
                return;

            /*
			 * Not sure when this was commented out, but it seems
			 * this was done to lower the overhead of making a trip
			 * to the DP.  Needs to check against the forum permission instead.
			 * 
            // Moderators can delete posts
            //
            if ( (Users.GetUser().IsModerator) && (Moderate.CheckIfUserIsModerator(Users.GetUser().UserID, post.ForumID)) )
                return;

            */

            switch (permission) {

                case Permission.Post:
                    if ((!forum.EnableAnonymousPosting) && (forumContext.User.IsAnonymous))
                        if (!forumContext.Context.Request.IsAuthenticated)
                            throw new ForumException(ForumExceptionType.PostAccessDenied);

                    if (forum.Permission.Post == AccessControlEntry.Deny)
                        throw new ForumException(ForumExceptionType.PostAccessDenied);
                    break;

                case Permission.Edit:

                    if (forumContext.User.IsAnonymous)
                        if (!forumContext.Context.Request.IsAuthenticated)
                            throw new ForumException(ForumExceptionType.PostEditAccessDenied);

                    if (forum.Permission.Edit == AccessControlEntry.Deny)
                        throw new ForumException(ForumExceptionType.PostEditAccessDenied);

                    if (post == null)
                        throw new Exception("Post parameter is required for Edit check");

                    // Ensure the user that created this post is the user attempting to delete it
                    //
                    if (forumContext.User.UserID != post.User.UserID)
                        throw new ForumException(ForumExceptionType.PostEditAccessDenied);

                    // Has the time limit been exceeded for this user to delete the post?
                    //
                    if (Globals.GetSiteSettings().PostEditBodyAgeInMinutes > 0)
                        if (post.PostDate < DateTime.Now.AddMinutes(Globals.GetSiteSettings().PostEditBodyAgeInMinutes))
                            throw new ForumException(ForumExceptionType.PostEditPermissionExpired);

                    break;

                case Permission.Reply:
                    if (forum.Permission.Reply == AccessControlEntry.Deny) 
                        throw new ForumException(ForumExceptionType.PostReplyAccessDenied);

                    if ((forumContext.User.IsAnonymous) && (!forum.EnableAnonymousPosting))
                        if (!forumContext.Context.Request.IsAuthenticated)
                            throw new ForumException(ForumExceptionType.PostReplyAccessDenied);

                    // Ensure we have a post
                    //
                    if (post == null)
                        throw new Exception("Post parameter is required for Reply check");

                    // Can't reply if locked
                    //
                    if (post.IsLocked)
                        throw new ForumException(ForumExceptionType.PostLocked);

                    break;

                case Permission.Announce:
                    if (forumContext.User.IsAnonymous)
                        if (!forumContext.Context.Request.IsAuthenticated)
                            throw new ForumException(ForumExceptionType.PostEditAccessDenied);

                    if (forum.Permission.Announce == AccessControlEntry.Deny)
                        throw new ForumException(ForumExceptionType.PostAnnounceAccessDenied);
                    break;

                case Permission.Delete:

                    if (forumContext.User.IsAnonymous)
                        if (!forumContext.Context.Request.IsAuthenticated)
                            throw new ForumException(ForumExceptionType.PostDeleteAccessDenied);

                    // If the user is denied delete return
                    if (forum.Permission.Delete == AccessControlEntry.Deny)
                        throw new ForumException(ForumExceptionType.PostDeleteAccessDenied);

                    // Ensure we have a post
                    //
                    if (post == null)
                        throw new Exception("Post parameter is required for Delete check");

                    // Ensure the user that created this post is the user attempting to delete it
                    //
                    if (forumContext.User.UserID != post.User.UserID)
                        throw new ForumException(ForumExceptionType.PostDeleteAccessDenied);

                    // Does the post have children, if so the user can't delete
                    //
                    if (post.Replies > 0)
                        throw new ForumException(ForumExceptionType.PostDeleteAccessDenied);

                    // Has the time limit been exceeded for this user to delete the post?
                    //
                    if (Globals.GetSiteSettings().PostDeleteAgeInMinutes > 0)
                        if (post.PostDate < DateTime.Now.AddMinutes(Globals.GetSiteSettings().PostDeleteAgeInMinutes))
                            throw new ForumException(ForumExceptionType.PostDeletePermissionExpired);
                    break;


            }

        }
        #endregion

    }
}
