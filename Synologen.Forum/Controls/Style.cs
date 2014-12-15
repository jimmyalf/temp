using System;
using System.Web;
using System.Web.UI;
using Spinit.Wpc.Forum.Components;
using Spinit.Wpc.Forum.Enumerations;

namespace Spinit.Wpc.Forum.Controls {

    // *********************************************************************
    //  StyleSkin
    //
    /// <summary>
    /// Encapsulated rendering of style based on the selected skin.
    /// </summary>
    // ********************************************************************/ 
    public class Style : LiteralControl {
        ForumContext forumContext = ForumContext.Current;
        string title = Globals.GetSiteSettings().SiteName;
        bool displayTitle = true;

        // Controls the style applied to the site
        public Style() {
            Post post = null;
            User user = null;
            string skinName = Globals.Skin;

            // Get the user if available we'll personalize the style
            if (HttpContext.Current.Request.IsAuthenticated) {
                user = Users.GetUser();
                skinName = user.Theme;
            }

            // Add the style sheet
            base.Text = "<link rel=\"stylesheet\" href=\"" + Globals.GetSkinPath() + "/style/default.css\" type=\"text/css\" />";

        }

    }
}
