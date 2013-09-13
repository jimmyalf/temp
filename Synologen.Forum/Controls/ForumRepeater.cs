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

namespace Spinit.Wpc.Forum.Controls {

    [
    ParseChildren(true)	
    ]
    /// <summary>
    /// Summary description for Summary.
    /// </summary>
    public class ForumRepeater : Repeater {
        ForumContext forumContext = ForumContext.Current;
        ForumMode mode = ForumMode.User;
        string skinName = null;
        bool hideForums = false;
        int forumGroupID = -1;
        bool ignorePermissions = false;

        // *********************************************************************
        //  ForumRepeaterControl
        //
        /// <summary>
        /// Constructor
        /// </summary>
        // ***********************************************************************/
        public ForumRepeater() {

            // If we're in design-time we simply return
            if (null == HttpContext.Current)
                return;

            // Is the user not availabe - must be anonymous
            if (forumContext.User.UserID == 0)
                Users.TrackAnonymousUsers();

            // Set the siteStyle for the page
            if (forumContext.User.UserID > 0)
                skinName = forumContext.User.Theme;
            else
                skinName = Globals.Skin;
        }

        // *********************************************************************
        //  CreateChildControls
        //
        /// <summary>
        /// Override create child controls
        /// </summary>
        /// 
        // ********************************************************************/   
        protected override void CreateChildControls() {
            EnableViewState = false;
            ArrayList forums = null;

            // Do we need to show the forums?
            //
            if (HideForums)
                return;

            // Bind the repeater to the collection returned by the GetForums()
            //
            try {

                // Are we in normal mode or moderation mode?
                //
                switch (mode) {

                    case ForumMode.Moderator:
                        forums = Moderate.GetForumsByForumGroupID (ForumGroupID);
                        break;

                    default:
                        // Do we have a forumID specified?
                        //
                        if ( (ForumGroupID == -1) && (forumContext.ForumID > 0) ) {

                            // Does the specified forum have any sub-forums?
                            //
                            Spinit.Wpc.Forum.Components.Forum forum =
                                (Spinit.Wpc.Forum.Components.Forum)forumContext.ForumLookupTable[forumContext.ForumID];

                            if (forum.Forums.Count > 0) {

                                // Get the forums for the named group
                                //
                                forum = Forums.GetForum(forumContext.ForumID);

                                // Now get the sub-forums for the current forum
                                //
                                forums = forum.Forums;
                            }

                        } else if (ForumGroupID == -1) {
                            return;
                        } else {
                            if (mode == ForumMode.Administrator) {
                                forums = Forums.GetForumsByForumGroupID(ForumGroupID, false, IgnorePermissions);
                            } else {
                                forums = Forums.GetForumsByForumGroupID(ForumGroupID, true, IgnorePermissions);
                            }
                        }
                        break;
                }

            } catch {
                throw new ForumException(ForumExceptionType.ForumNotFound);
            }

            if (( forums != null) && (forums.Count != 0 )) {
                this.DataSource = forums;
                this.DataBind();
            }
        }
        
        // *********************************************************************
        //  ForumGroupID
        //
        /// <summary>
        /// If available returns the forum group id value read from the querystring.
        /// </summary>
        /// 
        // ********************************************************************/ 
        public int ForumGroupID  {
            get  {
                return forumGroupID;
            }
            set  {
                forumGroupID = value;
            }
        }

        // *********************************************************************
        //  HideForums
        //
        /// <summary>
        /// When set to true no databinding occurs
        /// </summary>
        /// 
        // ********************************************************************/ 
        public bool HideForums  {
            get  {
                return hideForums;
            }
            set  {
                hideForums = value;
            }
        }

        public bool IgnorePermissions {
            get {
                return ignorePermissions;
            }
            set {
                ignorePermissions = value;
            }
        }

        public ForumMode Mode {
            get {
                return mode;
            }
            set {
                mode = value;
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
