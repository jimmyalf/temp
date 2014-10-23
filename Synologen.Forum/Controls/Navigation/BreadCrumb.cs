using System;
using System.Drawing;
using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Spinit.Wpc.Forum;
using Spinit.Wpc.Forum.Enumerations;
using Spinit.Wpc.Forum.Components;
using System.ComponentModel;
using System.IO;

namespace Spinit.Wpc.Forum.Controls {
    
    public class BreadCrumb : Literal {
        ForumContext forumContext = ForumContext.Current;
        private bool showHome = false;
        private bool enableLinks = true;
        Queue crumbs = new Queue();
        HtmlAnchor anchor;


        protected override void CreateChildControls() {

            // Do we need to add the home link?
            //
            if (ShowHome) {
                crumbs.Enqueue( GetAnchor(Globals.GetSiteSettings().SiteName, Globals.GetSiteUrls().Home) );
            }

            // Is a Forum Group ID specified?
            //
            if (forumContext.ForumGroupID > 0)
                AddForumGroup(forumContext.ForumGroupID);

            // Is a Forum ID specified?
            //
            if (forumContext.ForumID > 0)
                AddForum(forumContext.ForumID);

            // Is a PostID specified?
            //
            if (forumContext.PostID > 0)
                AddPost(forumContext.PostID);

            // We can't find anything useful on the query string
            // so we need to parse the actual URL
            //
            AddByRawURL(forumContext.Context.Request.RawUrl);
        }

        private void AddByRawURL(string url) {
            string comparisonUrl;

            // Unanswered posts
            //
            if (url == Globals.GetSiteUrls().PostsUnanswered) {
                crumbs.Enqueue( GetAnchor(ResourceManager.GetString("ViewUnansweredThreads_Title"), Globals.GetSiteUrls().PostsUnanswered ) );
                return;
            }
    
            // Active posts
            //
            if (url == Globals.GetSiteUrls().PostsActive) {
                crumbs.Enqueue( GetAnchor(ResourceManager.GetString("ViewActiveThreads_Title"), Globals.GetSiteUrls().PostsActive ) );
                return;
            }

            // Moderation
            //
            if (url == Globals.GetSiteUrls().Moderate) {
                crumbs.Enqueue( GetAnchor(ResourceManager.GetString("BreadCrumb_Moderation"), Globals.GetSiteUrls().Moderate) );
                return;
            }

            // User Profile
            //
            comparisonUrl = Globals.GetSiteUrls().UserProfile(forumContext.UserID);
            if ( url == comparisonUrl) {
                crumbs.Enqueue( GetAnchor( string.Format(ResourceManager.GetString("BreadCrumb_UserProfile"), Users.GetUser(forumContext.UserID, false, true).Username), Globals.GetSiteUrls().UserProfile(forumContext.UserID)) );
                return;
            }

            // User Profile
            //
            if ( url == Globals.GetSiteUrls().UserEditProfile) {
                crumbs.Enqueue( GetAnchor(ResourceManager.GetString("BreadCrumb_EditUserProfile"), Globals.GetSiteUrls().UserEditProfile) );
                return;
            }

            // Search
            //
            if ( url == Globals.GetSiteUrls().Search) {
                crumbs.Enqueue( GetAnchor(ResourceManager.GetString("BreadCrumb_Search"), Globals.GetSiteUrls().UserEditProfile) );
                return;
            }

			// Private Messages
			//
			// TODO (eduncan911):
			//		* When Folders are added for Inbox, Sent Items, etc; need to change
			//		  string replace below to just equal the url normally.  For now, there
			//		  is no sub-directory of PrivateMessages and it's being parsed.
			//		* I hate to hard coding this value, but I needed a way to fix this until the Inbox re-write.
			//
			url = url.Replace("/User/PrivateMessages/Default.aspx", "/User/PrivateMessages/");
			if ( url == Globals.GetSiteUrls().UserPrivateMessages) {
				crumbs.Enqueue( GetAnchor(Spinit.Wpc.Forum.Components.ResourceManager.GetString("PrivateMessages_Title"), Globals.GetSiteUrls().UserPrivateMessages) );
				return;
			}
        }

        private void AddPost(int postID) {

            // Get the Post
            //
            Post p = Posts.GetPost(postID, forumContext.User.UserID);

            // Get the forum
            //
            Spinit.Wpc.Forum.Components.Forum f =
                (Spinit.Wpc.Forum.Components.Forum)forumContext.ForumLookupTable[p.ForumID];

            // Add the forum group, only if not viewing Private Messages
            //
			if (f.ForumID > 0)
			{
				AddForumGroup(f.ForumGroupID);

				// Recursivley add the forums
				//
				RecursivlyAddForums(f);
			} 
			else 
			{
				// We add an anchor to return to the Private Messages
				//
				crumbs.Enqueue( GetAnchor(Spinit.Wpc.Forum.Components.ResourceManager.GetString("PrivateMessages_Title"), Globals.GetSiteUrls().UserPrivateMessages) );
			}


            // Finally add the post
            //
            crumbs.Enqueue( GetAnchor(p.Subject, Globals.GetSiteUrls().Post(p.PostID)) );

        }

        private void RecursivlyAddForums(Spinit.Wpc.Forum.Components.Forum f) {

            // Recurse
            //
            if (f.ParentID > 0) {
                Spinit.Wpc.Forum.Components.Forum parent = forumContext.GetForumFromForumLookupTable(f.ParentID);
                RecursivlyAddForums( parent );
            }

            // Next add forums
            //
            crumbs.Enqueue( GetAnchor(f.Name, Globals.GetSiteUrls().Forum(f.ForumID)) );

        }

        private void AddForum(int forumID) {

            // Get the forum
            //
            Spinit.Wpc.Forum.Components.Forum f = forumContext.GetForumFromForumLookupTable(forumID);

            // Only add the forum group if the user is allowed to see it
            //
            try {
                ForumPermission.AccessCheck(f, Permission.View );
                ForumPermission.AccessCheck(f, Permission.Read );

                // First add the forum group
                //
                AddForumGroup(f.ForumGroupID);
            } catch {}

            // Recursively add forums
            //
            RecursivlyAddForums(f);

        }

        private void AddForumGroup(int forumGroupID) {

            // Get the forum group
            //
            ForumGroup f = ForumGroups.GetForumGroup(forumGroupID);

            crumbs.Enqueue( GetAnchor(f.Name, Globals.GetSiteUrls().ForumGroup(forumGroupID) ) );
        }

        private Control GetAnchor(string innerText, string href) {
            anchor = new HtmlAnchor();
            anchor.InnerHtml = innerText;
            anchor.Attributes["class"] = "lnk3";
            anchor.HRef = href;

            return anchor;
        }

        protected override void Render(HtmlTextWriter writer) {

            while(crumbs.Count > 0) {
                HtmlAnchor a = (HtmlAnchor) crumbs.Dequeue();

                if (crumbs.Count == 0)
                    a.HRef = "";

                a.RenderControl(writer);

                if (crumbs.Count > 0)
                    writer.Write(ResourceManager.GetString("BreadCrumb_Seperator"));

            }
            
        }

        /****************************************************************
        // ShowHome
        //
        /// <summary>
        /// Controls whether or not the root element for the home is shown
        /// </summary>
        //
        ****************************************************************/
        public bool ShowHome {
            get {return showHome; }
            set {showHome = value; }
        }

        /****************************************************************
        // EnableLinks
        //
        /// <summary>
        ///  Determines whether or not links are hook-ed up.
        /// </summary>
        //
        ****************************************************************/
        public bool EnableLinks {
            get {return enableLinks; }
            set {enableLinks = value; }
        }
    }
}


