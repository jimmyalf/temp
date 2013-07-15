using System;
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

    public class UserProfile : SkinnedForumWebControl {
        User user;
        ForumContext forumContext = ForumContext.Current;
        string skinFilename = "View-UserProfile.ascx";

        // *********************************************************************
        //  UserProfile
        //
        /// <summary>
        /// Constructor
        /// </summary>
        /// 
        // ********************************************************************/
        public UserProfile() {
            
            // Do we have a valid UserID?
            //
            if(forumContext.UserID <= 0) {
              throw new ForumException(ForumExceptionType.UserNotFound);
            }
            
            user = Users.GetUser(forumContext.UserID, false, true);
            
            // Set the skin
            //
            if (SkinFilename == null)
                SkinFilename = skinFilename;
        }
	
        // *********************************************************************
        //  Initializeskin
        //
        /// <summary>
        /// This method populates the user control used to edit a user's information
        /// </summary>
        /// <param name="control">Instance of the user control to populate</param>
        /// 
        // ***********************************************************************/
        override protected void InitializeSkin(Control skin) {

            // Find controls for About section
            //
            ((Literal) skin.FindControl("Username")).Text = string.Format(ResourceManager.GetString("ViewUserProfile_Description"), user.Username);
            ((Literal) skin.FindControl("TimeZone")).Text = user.Timezone.ToString();
            ((Literal) skin.FindControl("Location")).Text = user.Location.ToString();
            ((Literal) skin.FindControl("Occupation")).Text = user.Occupation.ToString();
            ((Literal) skin.FindControl("Interests")).Text = user.Interests.ToString();

            // Format the output
            //
            if (!forumContext.User.IsAnonymous) {
                User u = forumContext.User;

                ((Literal) skin.FindControl("JoinedDate")).Text = user.DateCreated.ToString( user.DateFormat );
                ((Literal) skin.FindControl("LastLoginDate")).Text = user.LastLogin.ToString( user.DateFormat );
                ((Literal) skin.FindControl("LastActivityDate")).Text = user.LastActivity.ToString( user.DateFormat );
            } else {
                ((Literal) skin.FindControl("JoinedDate")).Text = user.DateCreated.ToString();
                ((Literal) skin.FindControl("LastLoginDate")).Text = user.LastLogin.ToString();
                ((Literal) skin.FindControl("LastActivityDate")).Text = user.LastActivity.ToString();
            }

            
            // Does the user have a web url?
            //
            if (user.WebAddress.Length > 0) {
                HyperLink url = (HyperLink) skin.FindControl("WebURL");
                url.NavigateUrl = user.WebAddress;
                url.Text = user.WebAddress;
            }

            // Does the user have a blog url?
            //
            if (user.WebLog.Length > 0) {
                HyperLink url = (HyperLink) skin.FindControl("BlogURL");
                url.NavigateUrl = user.WebLog;
                url.Text = user.WebLog;
            }

            // Does the user have a public email?
            if (user.PublicEmail.Length > 0) {
                HyperLink url = (HyperLink) skin.FindControl("Email");
                url.NavigateUrl = "mailto:" + user.PublicEmail;
                url.Text = user.PublicEmail;
            }

            // Find controls for Avatar/Post section
            //
            if ( (user.HasAvatar) && (user.EnableAvatar) && (user.IsAvatarApproved) ) {
                UserAvatar avatar = (UserAvatar) skin.FindControl("Avatar");

                if (avatar != null)
                    avatar.Visible = true;
            }

            ((Literal) skin.FindControl("Skin")).Text = user.Theme;
            //((Literal) skin.FindControl("Signature")).Text = Transforms.FormatPost(user.Signature, PostType.BBCode);
			((Literal) skin.FindControl("Signature")).Text = user.SignatureFormatted;


            // Find controls in Contact section
            //
            ((Literal) skin.FindControl("MSNIM")).Text = user.MsnIM;
            ((Literal) skin.FindControl("AOLIM")).Text = user.AolIM;
            ((Literal) skin.FindControl("YahooIM")).Text = user.YahooIM;
            ((Literal) skin.FindControl("ICQ")).Text = user.IcqIM;


            // Post statistics rank, etc.
            //
            ((Literal) skin.FindControl("TotalPosts")).Text = user.TotalPosts.ToString("n0");

			// User's Private Messages
			//
			if ((user.UserID != Users.GetUser().UserID) && (!user.IsAnonymous)) {	
				// LN 5/27/04: Changed to search for private message button 
				// only in user mode.
				if (Mode == ForumMode.User) {
					HyperLink PrivateMessageButton = (HyperLink) skin.FindControl("PrivateMessageButton");
					PrivateMessageButton.ToolTip = String.Format(ResourceManager.GetString("UserImageButtons_PrivateMessage"), user.Username);
					PrivateMessageButton.ImageUrl = Globals.GetSkinPath() + "/images/post_button_pm.gif";
					PrivateMessageButton.NavigateUrl = Globals.GetSiteUrls().PrivateMessage(user.UserID);
					PrivateMessageButton.Visible = true;
                    
                    HyperLink emailUserButton = (HyperLink) skin.FindControl("EmailUserButton");
                    emailUserButton.ImageUrl = Globals.GetSkinPath() + "/images/post_button_email.gif";
                    emailUserButton.NavigateUrl = Globals.GetSiteUrls().EmailToUser(user.UserID);
                    emailUserButton.Visible = true;
                }
			}

            if ((Mode == ForumMode.User) && (!user.IsAnonymous)) {
                // JD 06/15/04: added a button for searching for all posts by a user
                HyperLink SearchForPostsByUserButton = (HyperLink) skin.FindControl("SearchForPostsByUserButton");
                SearchForPostsByUserButton.ToolTip = String.Format(ResourceManager.GetString("UserImageButtons_SearchFor"), user.Username);
                SearchForPostsByUserButton.ImageUrl = Globals.GetSkinPath() + "/images/post_button_search.gif";
					
                SearchForPostsByUserButton.NavigateUrl = Globals.GetSiteUrls().SearchForText( string.Empty, string.Empty, Spinit.Wpc.Forum.Search.ForumsToSearchEncode(user.UserID.ToString()) );
                SearchForPostsByUserButton.Visible = true;
            }
        }

        // *********************************************************************
        //  RenderTopPosts
        //
        /// <summary>
        /// Renders the top n posts used in the DisplayViewControl
        /// </summary>
        /// <param name="control">Table of posts</param>
        /// 
        // ********************************************************************/
        private void DisplayMostRecentPost(int postsToRender, Control skin) {

        }

    }
}
