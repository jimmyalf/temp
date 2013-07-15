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
    //  MoveThread
    //
    /// <summary>
    /// This sever control is used to move a thread from forum to forum.
    /// </summary>
    /// 
    // ********************************************************************/ 
    public class ThreadMove : SkinnedForumWebControl {

        #region Member Variables & constructor
        ForumContext forumContext = ForumContext.Current;
        string skinFilename = "Moderation\\View-ThreadMove.ascx";
        Label subject;
        Label hasReplies;
        Label postedBy;
        Label body;
        ForumListBox moveTo;
        CheckBox sendEmail;
        Button cancelMove;
        Button move;
        Post postToMove;

        // *********************************************************************
        //  MovePost
        //
        /// <summary>
        /// Constuctor
        /// </summary>
        /// 
        // ***********************************************************************/
        public ThreadMove() : base() {

            // Set the skin file
            if (SkinFilename == null)
                SkinFilename = skinFilename;

            if (forumContext.ReturnUrl == null) {
                throw new ForumException(ForumExceptionType.ReturnURLRequired);
            }
        }

        #endregion

        #region Initialize Skin
        // *********************************************************************
        //  InitializeSkin
        //
        /// <summary>
        /// Initialize the control template and populate the control with values
        /// </summary>
        /// <param name="skin">Control instance of the skin</param>
        /// 
        // ***********************************************************************/
        override protected void InitializeSkin(Control skin) {

            // Get the post we want to move
            postToMove = Posts.GetPost(forumContext.PostID, forumContext.User.UserID);

            // Display subject
            subject = (Label) skin.FindControl("Subject");
            if (null != subject) {
                subject.Text = postToMove.Subject;
            }

            // Has Replies?
            hasReplies = (Label) skin.FindControl("HasReplies");
            if (null != hasReplies) {
                
                if (postToMove.Replies > 0) {
                    hasReplies.Text = ResourceManager.GetString("Yes");
                } else {
                    hasReplies.Text = ResourceManager.GetString("No");
                }
            }

            // POsted By
            postedBy = (Label) skin.FindControl("PostedBy");
            if (null != postedBy) {
                postedBy.Text = postToMove.Username + " ";
            }

            // Display the move to drop down list
            moveTo = (ForumListBox) skin.FindControl("MoveTo");

            // Cancel
            cancelMove = (Button) skin.FindControl("CancelMove");
            if (null != cancelMove) {
                cancelMove.Click += new EventHandler(Cancel_Click);
                cancelMove.Text = ResourceManager.GetString("MovePost_CancelMove");
            }

            // Move Post
            move = (Button) skin.FindControl("MovePost");
            if (null != move) {
                move.Click += new System.EventHandler(MoveThread_Click);
                move.Text = ResourceManager.GetString("MovePost_MovePost");
            }

            // Send email
            sendEmail = (CheckBox) skin.FindControl("SendUserEmail");
            sendEmail.Text = ResourceManager.GetString("MovePost_SendUserEmail");
        }
        #endregion

        #region Move Event
        // *********************************************************************
        //  MoveThread_Click
        //
        /// <summary>
        /// Event handler for deleting a post
        /// </summary>
        /// 
        // ***********************************************************************/
        private void MoveThread_Click(Object sender, EventArgs e) {

            // Are we valid?
            Moderate.MovePost(postToMove, Convert.ToInt32(moveTo.SelectedItem.Value.Replace("f-", "")), forumContext.User.UserID, sendEmail.Checked);

            // Redirect the user to the return url.
            HttpContext.Current.Response.Redirect(forumContext.ReturnUrl);
            HttpContext.Current.Response.End();
        }

        void Cancel_Click (object sender, EventArgs e) {
            ForumContext.Current.Context.Response.Redirect(forumContext.ReturnUrl, true);
        }

        #endregion
    }
}
