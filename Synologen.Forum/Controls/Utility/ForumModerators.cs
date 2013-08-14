using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using Spinit.Wpc.Forum.Components;
using Spinit.Wpc.Forum.Enumerations;
using System.Collections;

namespace Spinit.Wpc.Forum.Controls {

    public class ForumModerators : Label {

        ForumContext forumContext = ForumContext.Current;

        protected override void CreateChildControls() {
            // List roles that can moderate this forum
            ArrayList roles = Moderate.GetForumModeratorRoles ( forumContext.ForumID );


            if (roles.Count <= 0)
                return;

            Label label = new Label();
            label.Text = ResourceManager.GetString("ViewThreads_Moderators");
            
            Controls.Add(label);

            for (int i = 0; i < roles.Count; i++) {
                HtmlAnchor anchor = new HtmlAnchor();
                anchor.InnerText = ((Role) roles[i]).Name;
                anchor.HRef = Globals.GetSiteUrls().UserRoles( ((Role) roles[i]).RoleID );

                Controls.Add(anchor);

                if (i + 1 != roles.Count)
                    Controls.Add(new LiteralControl(", "));
            }


        }

    }
}


