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
using Spinit.Wpc.Forum.Enumerations;

namespace Spinit.Wpc.Forum.Controls {

    // *********************************************************************
    //  WhoIsOnlineView
    //
    /// <summary>
    /// This server control is used to display users that are currently 
    /// browsing the forums.
    /// </summary>
    /// 
    // ********************************************************************/
    public class WhoIsOnlineView : SkinnedForumWebControl {

        #region Member variables and constructor
        string skinFilename              = "View-WhoIsOnline.ascx";
        Repeater repeater;
        
        ForumContext forumContext = ForumContext.Current;
		

        // *********************************************************************
        //  WhoIsOnlineView
        //
        /// <summary>
        /// Constructor
        /// </summary>
        /// 
        // ********************************************************************/
        public WhoIsOnlineView() {


            // Assign a default template name
            if (SkinFilename == null)
                SkinFilename = skinFilename;

        }
        #endregion

        #region Skin initialization
        // *********************************************************************
        //  Initializeskin
        //
        /// <summary>
        /// Initializes the user control loaded in CreateChildControls. Initialization
        /// consists of finding well known control names and wiring up any necessary events.
        /// </summary>
        /// 
        // ********************************************************************/ 
        protected override void InitializeSkin(Control skin) {
            SiteStatistics siteStats = forumContext.Statistics;
            Literal userCount;
            Literal guestUsers;

            // Display members
            repeater = (Repeater) skin.FindControl("MembersOnlineRepeater");
            repeater.DataSource = Users.GetUsersOnline(15);
            repeater.DataBind();

            // Display guests
            repeater = (Repeater) skin.FindControl("GuestsOnlineRepeater");
            repeater.DataSource = Users.GetGuestsOnline(15);
            repeater.DataBind();

            userCount = (Literal) skin.FindControl("UsersOnlineCount");
            if (null != userCount)
                userCount.Text = String.Format(Spinit.Wpc.Forum.Components.ResourceManager.GetString("WhoIsOnlineView_UsersOnlineCount"), Users.GetUsersOnline(15).Count.ToString(), siteStats.TotalUsers.ToString("n0")); 

            guestUsers = (Literal) skin.FindControl("GuestUsers");
            if (null != guestUsers)
                guestUsers.Text = String.Format(Spinit.Wpc.Forum.Components.ResourceManager.GetString("WhoIsOnlineView_GuestUsers"), Users.GetGuestsOnline(15).Count.ToString("n0"));


        }
        #endregion

        #region Databinding
        public override void DataBind() {

        }
        #endregion


    }

}