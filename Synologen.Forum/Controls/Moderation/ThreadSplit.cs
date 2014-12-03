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
    //  ThreadSplit
    //
    /// <summary>
    /// This sever control is used to split a thread into a new thread.
    /// </summary>
    /// 
    // ********************************************************************/ 
    public class ThreadSplit : SkinnedForumWebControl {

        #region Member Variables & constructor
        ForumContext forumContext = ForumContext.Current;
        string skinFilename = "Moderation\\View-ThreadSplit.ascx";
        Post postToMove = null;
        TextBox subject;
        Label hasReplies;
        Label postedBy;
        Label body;
        ForumListBox moveTo;
        CheckBox sendEmail;
        HyperLink cancelMove;
        LinkButton move;

        // *********************************************************************
        //  ThreadSplit
        //
        /// <summary>
        /// Constuctor
        /// </summary>
        /// 
        // ***********************************************************************/
        public ThreadSplit() : base() {

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
            subject = (TextBox) skin.FindControl("Subject");
            if (null != subject) {
                subject.Text = postToMove.Subject;
            }

            // Has Replies?
            hasReplies = (Label) skin.FindControl("HasReplies");
            if (null != hasReplies) {
                
                if (postToMove.Replies > 0) {
                    hasReplies.Text = ResourceManager.GetString("Yes") + " (" + postToMove.Replies + ") ";
                } else {
                    hasReplies.Text = ResourceManager.GetString("No");
                }
            }

            // POsted By
            postedBy = (Label) skin.FindControl("PostedBy");
            if (null != postedBy) {
                postedBy.Text = postToMove.Username + " ";
            }

            // Body
            body = (Label) skin.FindControl("Body");
            if (null != body) {
                body.Text = Transforms.FormatPost(postToMove.Body) + " ";
            }

            // Display the move to drop down list
            moveTo = (ForumListBox) skin.FindControl("MoveTo");

            // Cancel
            cancelMove = (HyperLink) skin.FindControl("CancelMove");
            if (null != cancelMove) {
                cancelMove.NavigateUrl = forumContext.ReturnUrl;
                cancelMove.Text = ResourceManager.GetString("MovePost_CancelMove");
            }

            // Move Post
            move = (LinkButton) skin.FindControl("MovePost");
            if (null != move) {
                move.Click += new System.EventHandler(SplitThread_Click);
                move.Text = ResourceManager.GetString("ThreadSplit_SplitThread");
            }

            // Send email
            sendEmail = (CheckBox) skin.FindControl("SendUserEmail");
            sendEmail.Text = ResourceManager.GetString("ThreadSplit_SendUserEmail");
        }
        #endregion

        #region Split Event
        // *********************************************************************
        //  SplitThread_Click
        //
        /// <summary>
        /// Event handler for splitting a thread
        /// </summary>
        /// 
        // ***********************************************************************/
        private void SplitThread_Click(Object sender, EventArgs e) {

            // Did the subject change?
            if (subject.Text != postToMove.Subject) {
                postToMove.Subject = subject.Text;

                Posts.UpdatePost(postToMove, forumContext.User.UserID);
            }

            // Split the thread
            Moderate.ThreadSplit (postToMove, Convert.ToInt32(moveTo.SelectedItem.Value.Replace("f-", "")), forumContext.User.UserID, sendEmail.Checked);

            // Redirect the user to the return url.
            HttpContext.Current.Response.Redirect( Globals.GetSiteUrls().Post(forumContext.PostID) );
            HttpContext.Current.Response.End();

        }
        #endregion
    }
}
