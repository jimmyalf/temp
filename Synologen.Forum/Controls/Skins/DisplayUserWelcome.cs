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

namespace Spinit.Wpc.Forum.Controls
{

	// *********************************************************************
	//  DisplayUserWelcome
	//
	/// <summary>
	/// This server control is used to display the user's info, as well
	/// as any anonymous information if they are not logged in.
	/// </summary>
	// ***********************************************************************/
	public class DisplayUserWelcome : SkinnedForumWebControl {
	
		#region Member Variables
		ForumContext forumContext = ForumContext.Current;
		string skinFilename = "Skin-DisplayUserWelcome.ascx";

		// *********************************************************************
		//  DisplayTitle
		//
		/// <summary>
		/// Constructor
		/// </summary>
		// ***********************************************************************/
		public DisplayUserWelcome() : base() {

			if (SkinFilename == null)
				SkinFilename = skinFilename;

		}
		#endregion

		#region Init Skin
		// *********************************************************************
		//  InitializeSkin
		//
		/// <summary>
		/// Initialize the control template and populate the control with values
		/// </summary>
		// ***********************************************************************/
		override protected void InitializeSkin(Control skin) {

			// setup the controls for verification
			//
			Label userWelcome = (Label) skin.FindControl("UserWelcome");
			Label userLastLogin = (Label) skin.FindControl("UserLastLogin");
			Label userNewPosts = (Label) skin.FindControl("UserNewPosts");
			Label userPrivateMessages = (Label) skin.FindControl("UserPrivateMessages");
			Label alternateUserWelcome = (Label) skin.FindControl("AlternateUserWelcome");
			Panel displayAvatar = (Panel) skin.FindControl("DisplayAvatar");
			UserAvatar avatar = (UserAvatar) skin.FindControl("Avatar");

			User user = Users.GetUser();	
			string[] info = new string[2];

			// displays a welcome message to users signed-in.
			// alternates to a different message if they are not, for anonymous.
			//
			if (!user.IsAnonymous) {
				if (userWelcome != null) {
					userWelcome.Text = String.Format( ResourceManager.GetString("DisplayUserWelcome_UserWelcome"), user.Username );
					userWelcome.Visible = true;
				}
				
				if (userLastLogin != null) {
					userLastLogin.Text = String.Format( ResourceManager.GetString("DisplayUserWelcome_UserLastLogin"), Formatter.FormatDate(user.LastLogin, true) );
					userLastLogin.Visible = true;
				}

				// TODO: Access DP to get posts since lastlogin,
				// should be using a filter of > user.LastLogin.  
				//
				// Until then, we are disabling any display.
				//
				if (userNewPosts != null) {
					//userNewPosts.Text = String.Format( ResourceManager.GetString("DisplayUserWelcome_UserNewPosts"), "0" );
					//userNewPosts.Visible = true;
					userNewPosts.Visible = false;
				}

				// TODO: Need to access the forums_ThreadRead and forums_PrivateMessages tables to obtain
				// this information.  New sproc, or modify existing ones to filter.
				//
				// Until then, we are just displaying the link.
				//
				if (userPrivateMessages != null) {
					//info[0] = "0";
					//info[1] = "<a href=\"" + Globals.GetSiteUrls().UserPrivateMessages + "\">" + ResourceManager.GetString("PrivateMessages_Title").ToLower() + "</a>";
					//userPrivateMessages.Text = String.Format( ResourceManager.GetString("DisplayUserWelcome_UserPrivateMessages"), info );
					userPrivateMessages.Text = "<a href=\"" + Globals.GetSiteUrls().UserPrivateMessages + "\">" + ResourceManager.GetString("Navigation_JumpDropDownList_PrivateMessages") + "</a>";
					userPrivateMessages.Visible = true;
				}

				// Display the user's avatar, if they have one / is enabled / approved
				//
				if ( (user.HasAvatar) && (user.EnableAvatar) && (user.IsAvatarApproved) ) { 
					displayAvatar.Visible = true;
					avatar.User = user;
					avatar.Visible = true;
				}

			} else {
				// TODO: make switching skins a passed param to this control?
				//
				if (alternateUserWelcome != null) {
					info[0] = Globals.GetSiteSettings().SiteName;
					info[1] = "<a href=\"" + Globals.GetSiteUrls().UserRegister + "\">" + ResourceManager.GetString("Utility_ForumAnchorType_MenuRegister").ToLower() + "</a>";
					alternateUserWelcome.Text = String.Format( ResourceManager.GetString("DisplayUserWelcome_AlternateUserWelcome"), info );
					alternateUserWelcome.Visible = true;
				}

			}
		}
		#endregion
	}
}
