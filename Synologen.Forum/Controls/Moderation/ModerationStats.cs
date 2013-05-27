using System;
using System.Drawing;
using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Spinit.Wpc.Forum;
using Spinit.Wpc.Forum.Controls;
using Spinit.Wpc.Forum.Components;
using System.ComponentModel;
using System.IO;

namespace Spinit.Wpc.Forum.Controls {

    // *********************************************************************
    //  ModerationStats
    //
    /// <summary>
    /// This server control provides statistics about moderators and moderator
    /// actions within the forums.
    /// </summary>
    // ********************************************************************/ 
    public class ModerationStats : SkinnedForumWebControl {
        ForumContext forumContext = ForumContext.Current;
        string skinFilename = "Moderation/Skin-Statistics.ascx";

        // *********************************************************************
        //  ModerationStats
        //
        /// <summary>
        /// Constructor
        /// </summary>
        // ***********************************************************************/
        public ModerationStats() {

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
            Label label;
            Repeater repeater;

            // Get the statistics
            SiteStatistics siteStats = forumContext.Statistics;


            // Most active moderator
            repeater = (Repeater) skin.FindControl("TopModerators");
            if (null != repeater) {
                repeater.DataSource = siteStats.ActiveModerators;
                repeater.DataBind();
            }

            // Moderator actions
            repeater = (Repeater) skin.FindControl("ModerationAction");
            if (null != repeater) {
                repeater.DataSource = siteStats.ModerationActions;
                repeater.DataBind();
            }

            // Total Moderators
            label = (Label) skin.FindControl("TotalModerators");
            if (null != label)
                label.Text = siteStats.TotalModerators.ToString("n0");

            // Total Moderated Posts
            label = (Label) skin.FindControl("TotalModeratedPosts");
            if (null != label)
                label.Text = siteStats.TotalModeratedPosts.ToString("n0");

        }

    }
}
