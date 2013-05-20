using System;
using System.Web;
using System.Web.UI;
using Spinit.Wpc.Forum.Components;
using Spinit.Wpc.Forum.Enumerations;

namespace Spinit.Wpc.Forum.Controls {

	// *********************************************************************
	//  StyleSkin
	//
	/// <summary>
	/// Encapsulated rendering of style based on the selected skin.
	/// </summary>
	// ********************************************************************/ 
	public class PageTitle : LiteralControl {
		ForumContext forumContext = ForumContext.Current;
		string title = Globals.GetSiteSettings().SiteName;
		bool displayTitle = true;

		// Controls the style applied to the site
		public PageTitle() {

			if (DisplayTitle) {

                Spinit.Wpc.Forum.Components.Forum forum = null;
				Post post = null;
				User user = null;
				string skinName = Globals.Skin;
				string seperator = " - ";

				// Get the user if available we'll personalize the title
				//
				if (HttpContext.Current.Request.IsAuthenticated) {
					user = Users.GetUser();
				}


				// the following are a list of checks 
           
				// Do we have a ForumID?
				//
				if (forumContext.ForumID > 0) {
					forum = Forums.GetForum(forumContext.ForumID);

					if( forum != null 
					&&	forum.Name != null ) {
						Title = forum.Name + seperator + Title;
					}
				}

				// Do we have a PostID?
				//
				if (forumContext.PostID > 0) {
					post = Posts.GetPost(forumContext.PostID, forumContext.User.UserID);

					if( post != null 
					&&	post.Subject != null ) {
						Title = post.Subject + seperator + Title;
					}
				}

				if (user != null)
					base.Text = "<title>" + Title + " (" + user.Username + ")</title>\n";
				else
					base.Text = "<title>" + Title + "</title>\n";
			}
		}

		// Used to set the title of the page the control is rendered on
		public string Title {
			get { return title; }
			set { title = value; }
		}

		public bool DisplayTitle {
			get { return displayTitle; }
			set { displayTitle = value; }
		}

	}
}
