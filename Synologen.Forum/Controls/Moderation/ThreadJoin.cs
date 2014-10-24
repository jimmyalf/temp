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
    //  ThreadJoin
    //
    /// <summary>
    /// This sever control is used to join two threads into a single thread.
    /// </summary>
    /// 
    // ********************************************************************/ 
    public class ThreadJoin : SkinnedForumWebControl {

        #region Member Variables & constructor
        ForumContext forumContext = ForumContext.Current;
        string skinFilename = "Moderation\\View-ThreadJoin.ascx";
        HyperLink childThread;
        HyperLink parentThread;
        Button validateParent;
        CheckBox sendEmail;
        TextBox parentThreadID;
        HyperLink cancelMove;
        LinkButton move;
        CheckBox parentIsValid;
        Post postToJoin;

        // *********************************************************************
        //  ThreadJoin
        //
        /// <summary>
        /// Constuctor
        /// </summary>
        /// 
        // ***********************************************************************/
        public ThreadJoin() : base() {

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
            postToJoin = Posts.GetPost(forumContext.PostID, forumContext.User.UserID);

            // Find the parent threadid
            parentThreadID = (TextBox) skin.FindControl("ParentThreadID");

            // Display child thread
            childThread = (HyperLink) skin.FindControl("ChildThread");
            childThread.Text = postToJoin.Subject + " (" + postToJoin.ThreadID + ")";
            childThread.NavigateUrl = Globals.GetSiteUrls().Post(postToJoin.PostID);

            parentThread = (HyperLink) skin.FindControl("ParentThread");

            // Validate parent
            validateParent = (Button) skin.FindControl("ValidateParentThread");
            validateParent.Text = ResourceManager.GetString("ThreadJoin_ValidateButton");
            validateParent.Click += new EventHandler(ValidateThread_Click);
            
            parentIsValid = (CheckBox) skin.FindControl("ParentThreadIsValid");
            parentIsValid.Text = ResourceManager.GetString("ThreadJoin_IsValid");
            parentIsValid.Enabled = false;

            // Cancel
            cancelMove = (HyperLink) skin.FindControl("CancelMove");
            if (null != cancelMove) {
                cancelMove.NavigateUrl = forumContext.ReturnUrl;
                cancelMove.Text = ResourceManager.GetString("MovePost_CancelMove");
            }

            // Join threads
            move = (LinkButton) skin.FindControl("MovePost");
            if (null != move) {
                move.Click += new System.EventHandler(SplitThread_Click);
                move.Text = ResourceManager.GetString("ThreadJoin_JoinThread");
            }

            // Send email
            sendEmail = (CheckBox) skin.FindControl("SendUserEmail");
            sendEmail.Text = ResourceManager.GetString("ThreadJoin_SendUserEmail");
        }
        #endregion

        #region Move Event
        // *********************************************************************
        //  SplitThread_Click
        //
        /// <summary>
        /// Event handler for splitting a thread
        /// </summary>
        /// 
        // ***********************************************************************/
        private void SplitThread_Click(Object sender, EventArgs e) {

            // Are we valid?
            Moderate.ThreadJoin ( Posts.GetPost(int.Parse(parentThreadID.Text), forumContext.User.UserID), postToJoin, forumContext.User.UserID, sendEmail.Checked);

            // Redirect the user to the return url.
            HttpContext.Current.Response.Redirect(forumContext.ReturnUrl);
            HttpContext.Current.Response.End();
        }

        private void ValidateThread_Click (Object sender, EventArgs e) {
            Post p = null;

            // Check to ensure that a post exists
            try {
                p = Posts.GetPost(int.Parse(parentThreadID.Text), forumContext.User.UserID);
            } catch {
                return;
            }

            if (p == null)
                return;

            parentThreadID.Text = p.PostID.ToString();
            parentIsValid.Checked = true;
            parentThread.Text = p.Subject + " (" + p.ThreadID + ")";
            parentThread.NavigateUrl = Globals.GetSiteUrls().Post(p.PostID);


        }

        #endregion
    }
}
