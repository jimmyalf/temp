using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Spinit.Wpc.Forum;
using Spinit.Wpc.Forum.Controls;
using Spinit.Wpc.Forum.Components;
using Spinit.Wpc.Forum.Enumerations;

namespace Spinit.Wpc.Forum.Controls {

    // *********************************************************************
    //  ModerationMenu
    //
    /// <summary>
    /// This control renders a moderation menu used by forum moderators
    /// to moderate new posts.
    /// </summary>
    // ********************************************************************/ 	
    public class ModerationMenu : SkinnedForumWebControl {
        
        #region Member variables and constructor
        ForumContext forumContext = ForumContext.Current;
        string skinFilename = "Moderation/Skin-ModerationMenu.ascx";
        Button Approve;
        Button ApproveReply;
        Button ApproveEdit;
        LinkButton ToggleLockUnlockPost;
        LinkButton ToggleModerateUnModerateUser;
        HyperLink Move;
        HyperLink MergeSplit;
        HyperLink DeletePost;
        HyperLink EditPost;
        HyperLink EditUser;
        HyperLink ModerationHistory;
        Post post;

        // *********************************************************************
        //  ModerationMenu
        //
        /// <summary>
        /// Constructor
        /// </summary>
        // ***********************************************************************/
        public ModerationMenu() : base() {

            if (SkinFilename == null)
                SkinFilename = skinFilename;

        }
        #endregion

        #region Skinning and render functions
        // *********************************************************************
        //  CreateChildControls
        //
        /// <summary>
        /// This event handler adds the children controls.
        /// </summary>
        // ***********************************************************************/
        protected override void CreateChildControls() {

            // If the current user does not have moderate permissions on the post, return.
            //
            if ((!Users.GetUser().IsAdministrator) && (post.Forum.Permission.Moderate != AccessControlEntry.Allow))
                return;

            base.CreateChildControls();
        }

        // *********************************************************************
        //  Initializeskin
        //
        /// <summary>
        /// Initialize the control template and populate the control with values
        /// </summary>
        /// <param name="skin">Control instance of the skin</param>
        // ***********************************************************************/
        override protected void InitializeSkin(Control skin) {

            Label PostID = (Label) skin.FindControl("PostID");
            if (null != PostID) {
                PostID.Text = post.PostID.ToString();
            }

            // Add the Approve click event handler.
            //
            Approve = (Button) skin.FindControl("Approve");
            if (null != Approve) {
				Approve.Text = ResourceManager.GetString("ModerationMenu_Approve");
                Approve.Click += new EventHandler(Approve_Click);
            }

            // Add the ApproveReply click event handler.
            //
            ApproveReply = (Button) skin.FindControl("ApproveReply");
            if (null != ApproveReply) {
			    ApproveReply.Text = ResourceManager.GetString("ModerationMenu_ApproveReply");
                ApproveReply.Click += new EventHandler(ApproveReply_Click);
            }

            // Add the ApproveEdit click event handler.
            //
            ApproveEdit = (Button) skin.FindControl("ApproveEdit");
            if (null != ApproveEdit) {
				ApproveEdit.Text = ResourceManager.GetString("ModerationMenu_ApproveEdit");
                ApproveEdit.Click += new EventHandler(ApproveEdit_Click);
            }

            // Set the EditPost url to the ModeratePostEdit url.
            //
            EditPost = (HyperLink) skin.FindControl("EditPost");
            if (null != EditPost) {
				// Set the text for the hyperlink
				EditPost.Text = ResourceManager.GetString("ModeratePost_EditPost");
                EditPost.NavigateUrl = Globals.GetSiteUrls().ModeratePostEdit(post.PostID, HttpContext.Current.Request.Url.PathAndQuery);
            }

            // set the EditUser url
            //
            EditUser = (HyperLink) skin.FindControl("EditUser");
            if (null != EditUser) {

                // Set the text for the hyperlink
                EditUser.Text = ResourceManager.GetString("ModeratePost_EditUser");
                EditUser.NavigateUrl = Globals.GetSiteUrls().AdminUserEdit(post.User.UserID);
            }


            // Set the DeleteApprovedPost url based on the post level of the post.
            //
            DeletePost = (HyperLink) skin.FindControl("DeletePost");
            if (null != DeletePost) {
                // Set the text for the hyperlink

                if (post.PostLevel == 1) {
                    DeletePost.Text = ResourceManager.GetString("ModerationMenu_DeleteThread");
                    DeletePost.NavigateUrl = Globals.GetSiteUrls().ModeratePostDelete(post.PostID, Globals.GetSiteUrls().Forum(post.ForumID));
                } else {
                    DeletePost.Text = ResourceManager.GetString("ModerationMenu_DeletePost");
                    DeletePost.NavigateUrl = Globals.GetSiteUrls().ModeratePostDelete(post.PostID, Globals.GetSiteUrls().Post(post.ThreadID));
                }
            }

            // Set the Move url to the ModeratePostMove url if the post level is 1, otherwise disable the move control.
            //
            Move = (HyperLink) skin.FindControl("Move");
            if (null != Move) {
                if (post.PostLevel == 1) {
					Move.Text = ResourceManager.GetString("ModeratePost_Move");
                    Move.NavigateUrl = Globals.GetSiteUrls().ModeratePostMove(post.PostID, HttpContext.Current.Request.Url.PathAndQuery);
                }
            }

            // Set the Split url to the ModerateThreadSplit url
            //
            MergeSplit = (HyperLink) skin.FindControl("MergeSplit");
            if (null != MergeSplit) {
                if (post.PostLevel > 1) {
                    MergeSplit.Text = ResourceManager.GetString("ModerateThread_Split");
                    MergeSplit.NavigateUrl = Globals.GetSiteUrls().ModerateThreadSplit(post.PostID, HttpContext.Current.Request.Url.PathAndQuery);
                } else {
                    MergeSplit.NavigateUrl = Globals.GetSiteUrls().ModerateThreadJoin(post.PostID, HttpContext.Current.Request.Url.PathAndQuery);
                    MergeSplit.Text = ResourceManager.GetString("ModerateThread_Join");
                }
            }

            // Set the ModerationHistory url.
            //
            ModerationHistory = (HyperLink) skin.FindControl("History");
            if (null != ModerationHistory) {
                ModerationHistory.Text = ResourceManager.GetString("ModeratePost_ModerationHistory");
				//ModerationHistory.NavigateUrl = "TODO";
				
				// *** remove this line when this function is enabled.
				// it's disabled for now, because it does nothing.
				ModerationHistory.Enabled = false;
				// ***
            }

            // Toggle Lock / Unlock Post
            //
            ToggleLockUnlockPost = (LinkButton) skin.FindControl("ToggleLockUnlockPost");
            if (null != ToggleLockUnlockPost) {

                ToggleLockUnlockPost.Attributes.Add("OnClick", "alert('" + ResourceManager.GetString("ModerateThread_CachedAction").Replace("'", @"\'") + "')");
                if (post.PostLevel > 1) {
                    if (post.IsLocked) {
                        ToggleLockUnlockPost.Text = ResourceManager.GetString("ModerateThread_UnLock_Post");
                    } else {
                        ToggleLockUnlockPost.Text = ResourceManager.GetString("ModerateThread_Lock_Post");
                    }
                } else {
                    if (post.IsLocked) {
                        ToggleLockUnlockPost.Text = ResourceManager.GetString("ModerateThread_UnLock_Thread");
                    } else {
                        ToggleLockUnlockPost.Text = ResourceManager.GetString("ModerateThread_Lock_Thread");
                    }
                }

                ToggleLockUnlockPost.Click += new EventHandler(LockUnlock_Click);
                ToggleLockUnlockPost.CommandArgument = post.PostID.ToString();

            }

            // Toggle Moderate / Unmoderate User
            //
            ToggleModerateUnModerateUser = (LinkButton) skin.FindControl("ToggleModerateUnModerateUser");
            if (null != ToggleModerateUnModerateUser) {

                ToggleModerateUnModerateUser.Attributes.Add("OnClick", "alert('" + Spinit.Wpc.Forum.Components.ResourceManager.GetString("ModerateThread_CachedAction").Replace("'", @"\'") + "')");
                switch (post.User.ModerationLevel) {
                    case ModerationLevel.Unmoderated:
                        ToggleModerateUnModerateUser.Text = ResourceManager.GetString("ModerateThread_Moderate_User");
                        break;

                    case ModerationLevel.Moderated:
                        ToggleModerateUnModerateUser.Text = ResourceManager.GetString("ModerateThread_UnModerate_User");
                        break;

                    default:
                        ToggleModerateUnModerateUser.Enabled = false;
                        break;

                }

                ToggleModerateUnModerateUser.Click += new EventHandler(ModerateUnModerateUser_Click);
                ToggleModerateUnModerateUser.CommandArgument = post.User.UserID.ToString();

            }


        }
        #endregion

        #region Event Handlers
        // *********************************************************************
        //  Approve_Click
        //
        /// <summary>
        /// Event handler for approving a post
        /// </summary>
        // ***********************************************************************/
        private void Approve_Click(object sender, EventArgs e) {
            
            // Approve the post.
            Moderate.ApprovePost(post, forumContext.UserID);

            // Redirect the user to the moderate forum page if the forum still contains posts to moderate.
            if (Forums.GetForum(forumContext.ForumID).PostsToModerate != 0) {
                Context.Response.Redirect(Globals.GetSiteUrls().ModerateForum(forumContext.ForumID));
            } else {
                Context.Response.Redirect(Globals.GetSiteUrls().Moderate);
            }

            Context.Response.End();

        }

        
        // *********************************************************************
        //  UnModerateUser_Click
        //
        /// <summary>
        /// Event handler for approving a post and replying to the post.
        /// </summary>
        // ***********************************************************************/
        void ModerateUnModerateUser_Click (object sender, EventArgs e) {

            LinkButton b = (LinkButton) sender;

            User user = Users.GetUser(int.Parse(b.CommandArgument), false, false);
            Moderate.ToggleUserSettings( ModerateUserSetting.ToggleModerate, user, Users.GetUser().UserID);

        }

        // *********************************************************************
        //  LockUnlock_Click
        //
        /// <summary>
        /// Event handler for approving a post and replying to the post.
        /// </summary>
        // ***********************************************************************/
        void LockUnlock_Click (object sender, EventArgs e) {

            LinkButton b = (LinkButton) sender;

            int postID = int.Parse(b.CommandArgument);
            Post post = Posts.GetPost(postID, Users.GetUser().UserID);

            Moderate.TogglePostSettings(ModeratePostSetting.ToggleLock, post, Users.GetUser().UserID);
        }

        // *********************************************************************
        //  ApproveReply_Click
        //
        /// <summary>
        /// Event handler for approving a post and replying to the post.
        /// </summary>
        // ***********************************************************************/
        private void ApproveReply_Click(object sender, EventArgs e) {

            // Approve the post.
            Moderate.ApprovePost(post, forumContext.UserID);

            // Redirect the user to the PostReply url.
            Context.Response.Redirect(Globals.GetSiteUrls().PostReply(post.PostID));
            Context.Response.End();
        }

        // *********************************************************************
        //  ApproveEdit_Click
        //
        /// <summary>
        /// Event handler for approving a post and editing the post.
        /// </summary>
        // ***********************************************************************/
        private void ApproveEdit_Click(object sender, EventArgs e) {

            // Approve the post.
            Moderate.ApprovePost(post, forumContext.UserID);

            // Redirect the user to the edit post url.
            Context.Response.Redirect(Globals.GetSiteUrls().ModeratePostEdit(post.PostID, Context.Request.Url.PathAndQuery));
            Context.Response.End();
        }
        #endregion

        #region Public properties
        // *********************************************************************
        //  Post
        //
        /// <summary>
        /// Gets or sets the post to moderate.
        /// </summary>
        // ***********************************************************************/
        public Post Post {
            get {
                return post;
            }
            set {
                post = value;
            }
        }
        #endregion
    }
}