using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using Spinit.Wpc.Forum.Components;
using Spinit.Wpc.Forum.Enumerations;
using System.Collections;

namespace Spinit.Wpc.Forum.Controls {

    public class ActiveUsers : Label {
        bool guestMode = false;

        ForumContext forumContext = ForumContext.Current;

        protected override void CreateChildControls() {
            ArrayList list = null;
            Label label = new Label();
            
            Controls.Add(label);

            if (guestMode) {
                int guestCount = 0;
                list = Users.GetGuestsOnline(30);

                if ((guestCount == 0) && (!forumContext.Context.Request.IsAuthenticated))
                    guestCount = 1;


                for (int i = 0; i < list.Count; i++) {

                    SiteUrls.ForumLocation location = SiteUrls.GetForumLocation( ((User) list[i]).LastAction );

                    if (
                        ((location.ForumID == forumContext.ForumID) && (location.ForumID > -1)) ||
                        ((location.ForumGroupID == forumContext.ForumGroupID) && (location.ForumGroupID > -1)) ||
                        ((location.PostID == forumContext.PostID) && (location.PostID > -1))
                        ) {

                        guestCount++;
                    }

                }

                label.Text = string.Format(ResourceManager.GetString("ViewThreads_ActiveGuests"), guestCount);

            } else {
                label.Text = ResourceManager.GetString("ViewThreads_ActiveUsers");
                list = Users.GetUsersOnline(30);

            
                for (int i = 0; i < list.Count; i++) {

                    SiteUrls.ForumLocation location = SiteUrls.GetForumLocation( ((User) list[i]).LastAction );

                    if (
                        ((location.ForumID == forumContext.ForumID) && (location.ForumID > -1)) ||
                        ((location.ForumGroupID == forumContext.ForumGroupID) && (location.ForumGroupID > -1)) ||
                        ((location.PostID == forumContext.PostID) && (location.PostID > -1)) ||
                        ( ((User) list[i]).UserID == ForumContext.Current.UserID)
                        ) {

                        HtmlAnchor anchor = new HtmlAnchor();
                        anchor.InnerText = ((User) list[i]).Username;
                        anchor.HRef = Globals.GetSiteUrls().UserProfile( ((User) list[i]).UserID );

                        Controls.Add(anchor);

                        Controls.Add(new LiteralControl(", "));
                    }

                }

                // Remove the last comma
                Controls.RemoveAt( Controls.Count - 1 );

            }

        }

        public bool GuestMode {
            get {
                return guestMode;
            }
            set {
                guestMode = value;
            }
        }
    }
}


