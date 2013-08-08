using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Spinit.Wpc.Forum.Components;

namespace Spinit.Wpc.Forum.Controls {

    public class MarkAllRead : Button {
        ForumContext forumContext = ForumContext.Current;

        public MarkAllRead() {
            Text = ResourceManager.GetString("MarkAllRead_Threads");
            Click += new EventHandler(MarkAllRead_Click);
        }


        // *********************************************************************
        //  CreateChildControls
        //
        /// <summary>
        /// This event handler adds the children controls.
        /// </summary>
        // ***********************************************************************/
        protected override void Render(HtmlTextWriter writer) {

            // If the user is already authenticated we have no work to do
            if (!Page.Request.IsAuthenticated)
                return;

            if (forumContext.ForumID < 0)
                return;

            base.Render(writer);

        }

        public void MarkAllRead_Click(Object sender, EventArgs e) {

            // Mark all fourms as read
            //
            if ((forumContext.ForumGroupID == 0) && (forumContext.ForumID == 0) )
                Forums.MarkAllForumsRead(forumContext.User.UserID, 0, 0, false);

            // Mark all forums in a particular forum group as read
            //
            else if ( (forumContext.ForumGroupID > 0) && (forumContext.ForumID == 0) )
                Forums.MarkAllForumsRead(forumContext.User.UserID, forumContext.ForumGroupID, 0, false);


            // Mark all threads in a forum as read
            //
            else if ( forumContext.ForumID > 0 )
                Forums.MarkAllForumsRead(forumContext.User.UserID, 0, forumContext.ForumID, true);

        }

    }
}
