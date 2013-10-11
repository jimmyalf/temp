
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Web.UI;
using System.Web.Caching;
using System.Web.UI.Design;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Spinit.Wpc.Forum;
using Spinit.Wpc.Forum.Components;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace Spinit.Wpc.Forum.Controls {

    // *********************************************************************
    //  NavigationMenu
    //
    /// <summary>
    /// This control renders a navigation menu used to navigate the hierarchy
    /// of the Discussion Board. It's links are generated from paths named
    /// in the web.config file.
    /// </summary>
    // ********************************************************************/ 
    public class NavigationMenu : SkinnedForumWebControl {
        ForumContext forumContext = ForumContext.Current;
        string skinFilename = "Skin-NavigationMenu.ascx";

        // *********************************************************************
        //  NavigationMenu
        //
        /// <summary>
        /// Constructor
        /// </summary>
        // ***********************************************************************/
        public NavigationMenu() : base() {

            if (SkinFilename == null)
                SkinFilename = skinFilename;

        }

        // *********************************************************************
        //  InitializeSkin
        //
        /// <summary>
        /// Initialize the control template and populate the control with values
        /// </summary>
        // ***********************************************************************/
        override protected void InitializeSkin(Control skin) {

            HtmlAnchor anchor = null;
            int userID = forumContext.User.UserID;
			                    

            // Navigation elements are enabled and disabled based on who the user is
            //
            if (userID > 0) {

                // User profile link
                //
                anchor = (ForumAnchor) skin.FindControl("Profile");
				if (anchor != null)
					anchor.Visible = true;

                // Logout link
                //
				anchor = (ForumAnchor) skin.FindControl("Logout");
				if (forumContext.Context.User.Identity.AuthenticationType.Equals("Forms"))
					if (anchor != null)
						anchor.Visible = true;
				else
					if (anchor != null)
						anchor.Visible = true;

				// My Forums link
				//
                anchor = (ForumAnchor) skin.FindControl("MyForums");
				if (anchor != null)
					anchor.Visible = true;


                // Is the user a moderator?
                //
                if (forumContext.User.IsModerator) {
					if (anchor != null) {
						anchor = (ForumAnchor) skin.FindControl("Moderate");
						anchor.Target = "_parent";
						anchor.Visible = true;
					}
                }

                // Is the user an administrator?
                //
                if (forumContext.User.IsAdministrator) {
                    anchor = (ForumAnchor) skin.FindControl("Admin");
					if (anchor != null) {
						anchor.Target = "_parent";
						anchor.Visible = true;
					}
                }
            } else {

                // Display the login
                //
                anchor = (ForumAnchor) skin.FindControl("Login");
				if (anchor != null)
					anchor.Visible = true;

                // Display register
                //
                anchor = (ForumAnchor) skin.FindControl("Register");
				if (anchor != null)
					anchor.Visible = true;

            }

            // Are displaying the search option?
            //
            anchor = (ForumAnchor) skin.FindControl("Search");
			if (anchor != null)
				anchor.Visible = true;


            // FAQ
            //
            anchor = (ForumAnchor) skin.FindControl("Faq");
			if (anchor != null)
				anchor.Visible = true;

            anchor = (ForumAnchor) skin.FindControl("Search");
			if (anchor != null)
				anchor.Visible = true;


            // Special case the member list
            //
            if (Globals.GetSiteSettings().EnablePublicMemberList) {
 
                // Display member list
                //
                anchor = (ForumAnchor) skin.FindControl("MemberList");
				if (anchor != null)
					anchor.Visible = true;

            }

            // Databind
            //
            skin.DataBind();
        }

        // *********************************************************************
        //  Controls
        //
        /// <summary>
        /// Design time hack to get the control to render.
        /// </summary>
        /// 
        // ********************************************************************/ 
        public override ControlCollection Controls {
			get {
				EnsureChildControls();
				return base.Controls;
			}
		}		
    }
}