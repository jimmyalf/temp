// TODO: Remove code that display help...

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
    //  ForumView
    //
    /// <summary>
    /// This server control is used to display top level threads. Note, a thread
    /// is a post with more information. The Thread class inherits from the Post
    /// class.
    /// </summary>
    /// 
    // ********************************************************************/
    public class ForumGroupView : SkinnedForumWebControl {
        ForumContext forumContext = ForumContext.Current;
        ForumMode mode = ForumMode.User;
        string skinFilename = "View-ForumGroupView.ascx";
        Repeater repeater;

        // *********************************************************************
        //  ForumGroupView
        //
        /// <summary>
        /// The constructor simply checks for a ForumID value passed in via the
        /// HTTP POST or GET.
        /// properties.
        /// </summary>
        /// 
        // ********************************************************************/
        public ForumGroupView() {

            // Assign a default template name
            if (SkinFilename == null)
                SkinFilename = skinFilename;

        }

        public override void DataBind() {
            ArrayList forumGroups = null;

            // Don't data bind if the data source is already populated
            //
            if (repeater.DataSource != null)
                return;

            // Get all forum groups if we're auto-databinding
            // we always cache
            //
            switch (mode) {

                case ForumMode.Administrator:
                    break;

                case ForumMode.Moderator:
                    forumGroups = ForumGroups.GetForumGroups(false);
                    break;

                default:
                    forumGroups = ForumGroups.GetForumGroups(true);
                    break;

            }

            // Have we asked for a particular forum group?
            //
            if (forumContext.ForumGroupID > 0) {

                ArrayList newForumGroups = new ArrayList();

                // Try to find the requested forum group
                foreach (ForumGroup f in forumGroups) {

                    if (f.ForumGroupID == forumContext.ForumGroupID) {
                        f.HideForums = false;
                        newForumGroups.Add(f);
                    }

                }

                forumGroups = newForumGroups;

            }

            // Databind if we have values
            //
            if (forumGroups.Count > 0) {
                repeater.DataSource = forumGroups;
                repeater.DataBind();
            }

        }

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

            repeater = (Repeater) skin.FindControl("forumGroupRepeater");

            // Add an item command to the forum group repeater
            //
            repeater.ItemCreated += new RepeaterItemEventHandler(OnItemCreated);

            // Databind the forum group repeater
            //
            DataBind();

        }

        public void OnItemCreated(Object sender, RepeaterItemEventArgs e) {

            ImageButton b = (ImageButton) e.Item.FindControl("ExpandCollapse");

            if (b != null) {
                if (forumContext.ForumGroupID > 0) {
                    b.Visible = false;
                } else {
                    b.Click += new ImageClickEventHandler(ExpandCollapse_Click);
                }
            }

        }

        public void ExpandCollapse_Click (Object sender, ImageClickEventArgs e) {

            ArrayList groups = (ArrayList) repeater.DataSource;
            int fgID = Convert.ToInt32( ((ImageButton) sender).CommandArgument );
            UserCookie userCookie = forumContext.User.GetUserCookie();

            // Run through each group and set the appropriate display
            //
            foreach (ForumGroup g in groups) {

                if (g.ForumGroupID == fgID) {

                    // Are the forums in this group hidden?
                    //
                    if (g.HideForums) {
                        g.HideForums = false;
                        userCookie.RemoveHiddenForumGroup(g.ForumGroupID);
                    } else {
                        userCookie.AddHiddenForumGroup(g.ForumGroupID);
                        g.HideForums = true;
                    }
                }

            }

            repeater.DataSource = groups;
            repeater.DataBind();

        }

//        public new ForumMode Mode {
//            get {
//                return mode;
//            }
//            set {
//                mode = value;
//            }
//        }
    }
}