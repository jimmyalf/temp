// TODO: Add logic to ensure only users in the Forum-Moderators group have access

using System;
using System.Drawing;
using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Spinit.Wpc.Forum;
using Spinit.Wpc.Forum.Components;
using Spinit.Wpc.Forum.Enumerations;
using System.ComponentModel;
using System.IO;
using System.Web.Security;

namespace Spinit.Wpc.Forum.Controls {

    // *********************************************************************
    //  DeletePost
    //
    /// <summary>
    /// This control is used by forum moderators to delete posts
    /// </summary>
    // ***********************************************************************/
    public class DeletePost : SkinnedForumWebControl {

        #region Member variables and constructor
        ForumContext forumContext = ForumContext.Current;
        string skinFilename = "View-DeletePost.ascx";
        RequiredFieldValidator ValidateReason;
        TextBox reasonForDelete;
        Label hasReplies;
        Post postToDelete;
        Button deleteButton;
        Button cancelButton;

        // *********************************************************************
        //  DeletePost
        //
        /// <summary>
        /// Constuctor
        /// </summary>
        // ***********************************************************************/
        public DeletePost() : base() {

            // Assign a default template name
            if (SkinFilename == null)
                SkinFilename = skinFilename;

            if (forumContext.ReturnUrl == null) {
                throw new ForumException(ForumExceptionType.ReturnURLRequired);
            }
        }
        #endregion

        #region Rendering
        // *********************************************************************
        //  Initializeskin
        //
        /// <summary>
        /// Initialize the control template and populate the control with values
        /// </summary>
        // ***********************************************************************/
        override protected void InitializeSkin(Control skin) {

            // Get the post we are deleting
            postToDelete = Posts.GetPost(forumContext.PostID, forumContext.User.UserID);

            // Check if the user has permission to delete the post
            //
            ForumPermission.AccessCheck(postToDelete.Forum, Permission.Delete, postToDelete);

            // Text box containing the reason why the post was deleted. This note will be
            // sent to the end user.
            reasonForDelete = (TextBox) skin.FindControl("DeleteReason");

            // Setup the title
            //
            ((Label) skin.FindControl("ForumName")).Text = ResourceManager.GetString("DeletePost_TitleName");
            ((Label) skin.FindControl("ForumDescription")).Text = ResourceManager.GetString("DeletePost_TitleDescription");

            // Setup the window title
            //
            ((Literal) skin.FindControl("DeletePost_Title")).Text = string.Format(ResourceManager.GetString("DeletePost_Title"), postToDelete.Subject);

            // Who is deleting the post
            //
            ((Label) skin.FindControl("DeletedBy")).Text = Users.GetUser().Username;

            // Does the post have any replies?
			hasReplies = (Label) skin.FindControl("HasReplies");
			if (null != hasReplies) {

				// check to see if the Moderator is deleting childposts
				//
				if (forumContext.QueryText == "childposts") {
					if (postToDelete.Replies > 0)
						hasReplies.Text = ResourceManager.GetString("Yes") + " (" + postToDelete.Replies.ToString() + ") ";
					else
						hasReplies.Text = ResourceManager.GetString("No");
				} else
						hasReplies.Text = ResourceManager.GetString("DeletePost_NA");
			}

            // Perform the delete
            deleteButton = (Button) skin.FindControl("DeletePost");
            if (null != deleteButton) {
                deleteButton.Click += new System.EventHandler(DeletePost_Click);
                deleteButton.Attributes["onclick"] = "return confirm('" + ResourceManager.GetString("DeletePost_PopupConfirmation").Replace("'", @"\'") + "');";								
                deleteButton.Text = ResourceManager.GetString("DeletePost_DeletePost");
            }

            // Cancel the delete
            cancelButton = (Button) skin.FindControl("CancelDelete");
            if (null != cancelButton) {
                cancelButton.Click += new EventHandler(CancelDelete_Click);
                cancelButton.Text = ResourceManager.GetString("DeletePost_CancelDelete");
            }

            // Validator for reason to delete
            ValidateReason = (RequiredFieldValidator) skin.FindControl("ValidateReason");
            ValidateReason.ErrorMessage = ResourceManager.GetString("DeletePost_ValidateReason");

        }
        #endregion


        #region Events
        // *********************************************************************
        //  DeletePost_Click
        //
        /// <summary>
        /// Event handler for deleting a post
        /// </summary>
        // ***********************************************************************/
        private void DeletePost_Click(Object sender, EventArgs e) {

            // Are we valid?
            if (ValidateReason.IsValid) {

                // Get the post we are going to delete
                //
				Post post = (Post) Posts.GetPost(forumContext.PostID, Users.GetUser().UserID);

				// get the old ForumID to redirect to, before we delete it and
				// loose this information.
				//
				int oldForumID = post.ForumID;

                // Perform the delete
                //
				if (forumContext.QueryText == "childposts")
					Moderate.DeletePost(post, Users.GetUser(), reasonForDelete.Text, true);
				else
					Moderate.DeletePost(post, Users.GetUser(), reasonForDelete.Text, false);

                // Did we delete a thread?
                //
                if (forumContext.ReturnUrl != String.Empty) {
                    forumContext.Context.Response.Redirect(forumContext.ReturnUrl, true);
                } else {
                    forumContext.Context.Response.Redirect(Globals.GetSiteUrls().Forum(oldForumID), true);
                }

            }

        }

        // *********************************************************************
        //  CancelDelete_Click
        //
        /// <summary>
        /// Event handler for canceling deletion of a post
        /// </summary>
        // ***********************************************************************/
        private void CancelDelete_Click(Object sender, EventArgs e) {
            forumContext.Context.Response.Clear();
            forumContext.Context.Response.Redirect(forumContext.ReturnUrl, true);
        }
        #endregion

    }
}
