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

namespace Spinit.Wpc.Forum.Controls {

    // *********************************************************************
    //  WhoIsOnline
    //
    /// <summary>
    /// This server control is used to display a list of users currently online.
    /// </summary>
    // ***********************************************************************/
    public class WhoIsOnline : SkinnedForumWebControl {

        ForumContext forumContext = ForumContext.Current;
        int minutesToCheckForUsersOnline = 15;
        string skinFilename = "Skin-WhoIsOnline.ascx";

        // *********************************************************************
        //  WhoIsOnline
        //
        /// <summary>
        /// Constructor
        /// </summary>
        // ***********************************************************************/
        public WhoIsOnline() : base() {

            if (SkinFilename == null)
                SkinFilename = skinFilename;

        }

        // *********************************************************************
        //  CreateChildControls
        //
        /// <summary>
        /// This event handler adds the children controls.
        /// </summary>
        // ***********************************************************************/
        protected override void CreateChildControls() {

            // If the user is already authenticated we have no work to do
            if (!Globals.GetSiteSettings().EnableWhoIsOnline)
                return;

            base.CreateChildControls();
        }

        // *********************************************************************
        //  InitializeSkin
        //
        /// <summary>
        /// Initialize the control template and populate the control with values
        /// </summary>
        // ***********************************************************************/
        override protected void InitializeSkin(Control skin) {
            Literal userCount;
            Repeater usersOnline;
            Literal guestUsers;
            ArrayList users;
			System.Web.UI.WebControls.Image img;

			// Find the stats image
			img = (System.Web.UI.WebControls.Image) skin.FindControl("StatsImg");
			if (null != img)
				img.ImageUrl = Globals.GetSkinPath() + "/images/user_IsOnline.gif";

            // Get the statistics
            //
            SiteStatistics siteStats = forumContext.Statistics;

            // Get the users for the past n minutes
            //
            users = Users.GetUsersOnline(Minutes);

            // Display total users
            //
            userCount = (Literal) skin.FindControl("UsersOnlineCount");
            if (null != userCount)
                userCount.Text = String.Format(Spinit.Wpc.Forum.Components.ResourceManager.GetString("WhoIsOnline_UsersOnlineCount"), users.Count.ToString("n0"), siteStats.TotalUsers.ToString("n0")); 

            // Display users online
            //
            usersOnline = (Repeater) skin.FindControl("UsersOnline");
            if (null != usersOnline) {
                usersOnline.DataSource = users;
                usersOnline.DataBind();
            }

            // Guests online
            //
            guestUsers = (Literal) skin.FindControl("GuestUsers");
            if (null != guestUsers)
                guestUsers.Text = String.Format(Spinit.Wpc.Forum.Components.ResourceManager.GetString("WhoIsOnline_GuestUsers"), siteStats.CurrentAnonymousUsers.ToString("n0"));

        }


        // *********************************************************************
        //  Minutes
        //
        /// <summary>
        /// Controls have often we poll for updates.
        /// </summary>
        // ***********************************************************************/
        public int Minutes {
            get { return minutesToCheckForUsersOnline; }
            set { minutesToCheckForUsersOnline = value; }
        }

    }
}
