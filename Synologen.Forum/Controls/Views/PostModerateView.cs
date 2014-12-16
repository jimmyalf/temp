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
    //  PostModerateView
    //
    /// <summary>
    /// This server control is used to display top level threads. Note, a thread
    /// is a post with more information. The Thread class inherits from the Post
    /// class.
    /// </summary>
    /// 
    // ********************************************************************/
    public class PostModerateView : SkinnedForumWebControl {
        string skinFilename = "/Moderation/View-ModerateForum.ascx";
        ForumContext forumContext = ForumContext.Current;
        Repeater postRepeater;

        // *********************************************************************
        //  PostModerateView
        //
        /// <summary>
        /// The constructor simply checks for a ForumID value passed in via the
        /// HTTP POST or GET.
        /// properties.
        /// </summary>
        /// 
        // ********************************************************************/
        public PostModerateView() {

            // Assign a default template name
            if (SkinFilename == null)
                SkinFilename = skinFilename;

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

            // Get the cached forum information
            //
            Spinit.Wpc.Forum.Components.Forum forum = 
                (Spinit.Wpc.Forum.Components.Forum)Forums.GetForum(forumContext.ForumID);
            
            // Set the title
            //
            ((Label) skin.FindControl("ForumTitle")).Text = ResourceManager.GetString("Moderate_PostsToModerateTitle");
            ((Label) skin.FindControl("ForumDescription")).Text = string.Format(ResourceManager.GetString("Moderate_PostsToModerateDescription"), forum.Name);

            // Find the post repeater
            //
            postRepeater = (Repeater) skin.FindControl("PostRepeater");

            DataBind();
        }

        public void Pager_IndexChanged (Object sender, EventArgs e) {
            DataBind();
        }

        public override void DataBind() {

            // Get a populated thread set
            //
            PostSet p = Moderate.GetPosts(forumContext.ForumID, 0, 25, 0, 0);

            if (p.Posts.Count == 0) {
                forumContext.Context.Response.Redirect(Globals.GetSiteUrls().Moderate, true);
            } else {
                postRepeater.DataSource = p.Posts;
                postRepeater.DataBind();
            }

        }

    }
}