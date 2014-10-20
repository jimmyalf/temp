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
using System.Web.Security;

namespace Spinit.Wpc.Forum.Controls {

    // *********************************************************************
    //  Logout
    //
    /// <summary>
    /// This server control renders and handles the login UI for the user.
    /// </summary>
    // ***********************************************************************/
    [
    ParseChildren(true)
    ]
    public class Logout : SkinnedForumWebControl {

        ForumContext forumContext = ForumContext.Current;
        string skinFilename = "Skin-Logout.ascx";
        TextBox username;
        TextBox password;
        Button loginButton;
        CheckBox autoLogin;

        // *********************************************************************
        //  Logout
        //
        /// <summary>
        /// Constructor
        /// </summary>
        // ***********************************************************************/
        public Logout() : base() {

            // Assign a default template name
            if (SkinFilename == null)
                SkinFilename = skinFilename;

        }

        // *********************************************************************
        //  Initializeskin
        //
        /// <summary>
        /// Initialize the control template and populate the control with values
        /// </summary>
        // ***********************************************************************/
        override protected void InitializeSkin(Control skin) {

            // log the user out
            FormsAuthentication.SignOut();
		
            // Nuke the roles cookie
            Spinit.Wpc.Forum.Components.Roles.SignOut();
        }

    }
}