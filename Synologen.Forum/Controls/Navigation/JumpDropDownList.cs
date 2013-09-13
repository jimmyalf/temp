using System;
using System.Drawing;
using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Spinit.Wpc.Forum;
using Spinit.Wpc.Forum.Components;
using Spinit.Wpc.Forum.Enumerations;
using System.ComponentModel;
using System.IO;

namespace Spinit.Wpc.Forum.Controls {

    [
    ParseChildren(true)
    ]
    public class JumpDropDownList : DropDownList {

        string displayText = ResourceManager.GetString("Navigation_JumpDropDownList_displayText");

        public JumpDropDownList() {

            // Set up some default property values
            //
            AutoPostBack = true;
            SelectedIndexChanged += new System.EventHandler(Location_Changed);

			// head of the drop down
			//
            Items.Add(new ListItem( ResourceManager.GetString("Navigation_JumpDropDownList_Title"), "" ));
			Items.Add(new ListItem( ResourceManager.GetString("Navigation_JumpDropDownList_Seperator") ));

			// standard "home" links
			//
            Items.Add(new ListItem( ResourceManager.GetString("Navigation_JumpDropDownList_Home"), Globals.ApplicationPath));
            Items.Add(new ListItem( ResourceManager.GetString("Navigation_JumpDropDownList_Indent1") + ResourceManager.GetString("Navigation_JumpDropDownList_Search"), Globals.GetSiteUrls().Search));
			Items.Add(new ListItem( ResourceManager.GetString("Navigation_JumpDropDownList_Indent1") + ResourceManager.GetString("ViewActiveThreads_Title"), Globals.GetSiteUrls().PostsActive));
			Items.Add(new ListItem( ResourceManager.GetString("Navigation_JumpDropDownList_Indent1") + ResourceManager.GetString("ViewUnansweredThreads_Title"), Globals.GetSiteUrls().PostsUnanswered));

			// User Options to display, based if the user is signed in or not.
			//
			if (Users.GetUser().IsAdministrator || Users.GetUser().IsModerator) {
				Items.Add(new ListItem( ResourceManager.GetString("Navigation_JumpDropDownList_Seperator") ));
				Items.Add(new ListItem( ResourceManager.GetString("Navigation_JumpDropDownList_AdminOptions") ));
				if ( Users.GetUser().IsAdministrator  )
					Items.Add(new ListItem( ResourceManager.GetString("Navigation_JumpDropDownList_Indent1") + ResourceManager.GetString("Navigation_JumpDropDownList_AdminHome"), Globals.GetSiteUrls().AdminHome ));
				Items.Add(new ListItem( ResourceManager.GetString("Navigation_JumpDropDownList_Indent1") + ResourceManager.GetString("Navigation_JumpDropDownList_ModeratorHome"), Globals.GetSiteUrls().ModerationHome ));
			}            

			// seperator
			//
			Items.Add(new ListItem( ResourceManager.GetString("Navigation_JumpDropDownList_Seperator") ));

			// User Options to display, based if the user is signed in or not.
			//
			if (!Users.GetUser().IsAnonymous) {
				Items.Add(new ListItem( ResourceManager.GetString("Navigation_JumpDropDownList_UserOptions") ));
				Items.Add(new ListItem( ResourceManager.GetString("Navigation_JumpDropDownList_Indent1") + ResourceManager.GetString("Navigation_JumpDropDownList_Profile"), Globals.GetSiteUrls().UserEditProfile ));
				Items.Add(new ListItem( ResourceManager.GetString("Navigation_JumpDropDownList_Indent1") + ResourceManager.GetString("Navigation_JumpDropDownList_PrivateMessages"), Globals.GetSiteUrls().UserPrivateMessages ));
				Items.Add(new ListItem( ResourceManager.GetString("Navigation_JumpDropDownList_Indent1") + ResourceManager.GetString("Navigation_JumpDropDownList_MyThreads"), Globals.GetSiteUrls().UserMyForums ));
			} else {
				Items.Add(new ListItem( ResourceManager.GetString("Navigation_JumpDropDownList_UserOptions") ));
				Items.Add(new ListItem( ResourceManager.GetString("Navigation_JumpDropDownList_Indent1") + ResourceManager.GetString("Navigation_JumpDropDownList_Login"), Globals.GetSiteUrls().Login ));
				Items.Add(new ListItem( ResourceManager.GetString("Navigation_JumpDropDownList_Indent1") + ResourceManager.GetString("Navigation_JumpDropDownList_CreateAccount"), Globals.GetSiteUrls().UserRegister ));
				Items.Add(new ListItem( ResourceManager.GetString("Navigation_JumpDropDownList_Indent1") + ResourceManager.GetString("Navigation_JumpDropDownList_ForgotPassword"), Globals.GetSiteUrls().UserForgotPassword ));
			}

			// seperator
			//
			Items.Add(new ListItem( ResourceManager.GetString("Navigation_JumpDropDownList_Seperator") ));

            // Get all forum groups and the remaining items are displayed
			// as ForumGroups and Forums.
			//
            ArrayList forumGroups = ForumGroups.GetForumGroups(true);

            foreach (ForumGroup group in forumGroups) {
                
                // Add the forum group
                //
                Items.Add(new ListItem(group.Name, "g-" + group.ForumGroupID));

				// Add all forums recursively.
				//
                RecursiveAddForum (0, group.Forums);

                // Add the forum group
                //
                Items.Add(new ListItem( ResourceManager.GetString("Navigation_JumpDropDownList_Seperator") ));
            }
        }

        void RecursiveAddForum (int depth, ArrayList forums) {

            foreach (Spinit.Wpc.Forum.Components.Forum forum in forums)
            {
                // We only go 3 deep
                //

                switch (depth) {
                    case 0:

                        Items.Add(new ListItem(ResourceManager.GetString("Navigation_JumpDropDownList_Indent1") + forum.Name, "f-" + forum.ForumID.ToString()));
                        if (forum.Forums.Count > 0)
                            RecursiveAddForum((depth + 1), forum.Forums);
                        break;

                    case 1:
                        Items.Add(new ListItem(ResourceManager.GetString("Navigation_JumpDropDownList_Indent2") + forum.Name, "f-" + forum.ForumID.ToString()));
                        if (forum.Forums.Count > 0)
                            RecursiveAddForum((depth + 1), forum.Forums);
                        break;

                    case 2:
                        Items.Add(new ListItem(ResourceManager.GetString("Navigation_JumpDropDownList_Indent3") + forum.Name, "f-" + forum.ForumID.ToString()));
                        if (forum.Forums.Count > 0)
                            RecursiveAddForum((depth + 1), forum.Forums);
                        break;

                    default:
                        return;

                }
            }
        }


        // *********************************************************************
        //  Location_Changed
        //
        /// <summary>
        /// User wants to jump to a new location
        /// </summary>
        /// 
        // ********************************************************************/ 
        private void Location_Changed(Object sender, EventArgs e) {

            DropDownList jumpLocation = (DropDownList) sender;
            string jumpValue = jumpLocation.SelectedItem.Value;

            if (jumpValue.StartsWith("/")) {
                Page.Response.Redirect(jumpValue);
            } else if (jumpValue.StartsWith("g")) {
                int forumGroupId = 0;
                forumGroupId = Convert.ToInt32(jumpValue.Substring(jumpValue.IndexOf("-") + 1));
                Page.Response.Redirect(Globals.GetSiteUrls().ForumGroup( forumGroupId)  );
            } else if (jumpValue.StartsWith("f")) {
                int forumId = 0;
                forumId = Convert.ToInt32(jumpValue.Substring(jumpValue.IndexOf("-") + 1));
                Page.Response.Redirect(Globals.GetSiteUrls().Forum(forumId) );
            } else {
                Page.Response.Redirect(Globals.ApplicationPath);
            }

            // End the response
            Page.Response.End();
        }

        // *********************************************************************
        //  DisplayText
        //
        /// <summary>
        /// Text preceding the drop down list of options
        /// </summary>
        /// 
        // ********************************************************************/ 
        public string DisplayText {
            get { return displayText;  }
            set { displayText = value; }
        }

    }
}