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
    //  RatingSummaryView
    //
    /// <summary>
    /// This server control is used to display who rated a post
    /// </summary>
    /// 
    // ********************************************************************/
    public class RatingSummaryView : SkinnedForumWebControl {

        #region Member variables and constructor
        string skinFilename = "View-ThreadRatingSummary.ascx";
        Post post;
        Repeater ratings;
        
        ForumContext forumContext = ForumContext.Current;

        // *********************************************************************
        //  RatingSummaryView
        //
        /// <summary>
        /// The constructor simply checks for a ForumID value passed in via the
        /// HTTP POST or GET.
        /// properties.
        /// </summary>
        /// 
        // ********************************************************************/
        public RatingSummaryView() {

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

            // Get the post that we're rating
            //
            post = Posts.GetPost(forumContext.PostID, Users.GetUser().UserID);

            ratings = (Repeater) skin.FindControl("Ratings");

            ratings.DataSource = post.Ratings;
            ratings.DataBind();

        }
        #endregion

        #region Databinding
        public override void DataBind() {
        }
        #endregion

    }

}