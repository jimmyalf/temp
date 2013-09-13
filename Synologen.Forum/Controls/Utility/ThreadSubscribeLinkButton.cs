using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;
using Spinit.Wpc.Forum.Components;

namespace Spinit.Wpc.Forum.Controls {

    public class ThreadSubscribeLinkButton : LinkButton {
        ForumContext forumContext = ForumContext.Current;

        public ThreadSubscribeLinkButton() {
            Click += new EventHandler(ToggleEmailReplies);
        }

        protected override void CreateChildControls() {

            if (forumContext.User.IsAnonymous)
                return;

            // Get the post we are currently working with
            //
            Post p = Posts.GetPost(forumContext.PostID, forumContext.UserID);

            base.CssClass = "columnText";

            // Image
            Image image = new Image();
            image.ImageUrl = Globals.GetSkinPath() + "/images/icon_subscribe.gif";
            Controls.Add(image);

            if (p.IsTracked)
                Controls.Add(new LiteralControl( ResourceManager.GetString("PostFlatView_EnableThreadTrackingOn")));
            else
                Controls.Add(new LiteralControl( ResourceManager.GetString("PostFlatView_EnableThreadTrackingOff")));

        }

        void ToggleEmailReplies (Object sender, EventArgs e) {

            Posts.ReverseThreadTrackingOptions(forumContext.User.UserID, forumContext.PostID);

            forumContext.Context.Response.Redirect( Globals.GetSiteUrls().Post(forumContext.PostID), true);

        }

    }

}
