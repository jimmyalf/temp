using System;
using System.Drawing;
using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Spinit.Wpc.Forum;
using Spinit.Wpc.Forum.Components;
using System.ComponentModel;
using System.IO;
using System.Web.Security;
using Spinit.Wpc.Forum.Enumerations;

namespace Spinit.Wpc.Forum.Controls.BaseClasses {

    [
    ParseChildren(true)	
    ]
    public abstract class ForumDataListControl : DataList {
        User user = null;
        string skinName = null;

        public ForumDataListControl() {

            // Attempt to get the current user
            user = Users.GetUser();

            // Is the user not availabe - must be anonymous
            if (user == null)
                Users.TrackAnonymousUsers();

            // Set the siteStyle for the page
            if (user != null)
                skinName = user.Theme;
            else
                skinName = Globals.Skin;

        }

        // *********************************************************************
        //  ForumUser
        //
        /// <summary>
        /// Returns an instance of User or null if the user is not logged in.
        /// </summary>
        // ***********************************************************************/
        protected User ForumUser {
            get {
                return user;
            }
        }
        
        // *********************************************************************
        //  SkinName
        //
        /// <summary>
        /// Used to construct paths to images, etc. within controls.
        /// </summary>
        /// 
        // ********************************************************************/ 
        protected string SkinName {
            get {
                return skinName;
            }
            set {
                skinName = value;
            }
        }
    }
}
