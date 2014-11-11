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
using System.Text;

namespace Spinit.Wpc.Forum.Controls {

    // *********************************************************************
    //  SearchView
    //
    /// <summary>
    /// This server control is used to display search options and search results
    /// </summary>
    /// 
    // ********************************************************************/
    public class SearchView : SkinnedForumWebControl {

        #region Member variables and constructor
        ForumContext forumContext = ForumContext.Current;
        string skinFilename = "View-Search.ascx";
        ForumListBox forumList;
        Button searchKeywords;
        TextBox searchTextKeywords;
        SearchForumsRadioButtonList searchForum;
        TextBox users;

        // *********************************************************************
        //  SearchView
        //
        /// <summary>
        /// The constructor simply checks for a ForumID value passed in via the
        /// HTTP POST or GET.
        /// properties.
        /// </summary>
        /// 
        // ********************************************************************/
        public SearchView() {

            // Assign a default template name
            if (SkinFilename == null)
                SkinFilename = skinFilename;

        }
        #endregion

        #region Rendering
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

            searchKeywords = (Button) skin.FindControl("SearchKeyWordsButton");
            searchKeywords.Click += new EventHandler(SearchKeyWords_Click);
            searchKeywords.Text = ResourceManager.GetString("Search");

			searchTextKeywords = (TextBox) skin.FindControl("searchTextKeywords"); 
			searchTextKeywords.TextChanged += new System.EventHandler(SearchKeyWords_Click); 


            // Can we constrict by forum?
            searchForum = (SearchForumsRadioButtonList) skin.FindControl("SearchForums");
            if (searchForum != null) {
                searchForum.SelectedValue = true.ToString();
                searchForum.SelectedIndexChanged += new EventHandler( SearchForums_Changed );
            }

            // Can we constrict by user?
            users = (TextBox) skin.FindControl("SearchTextByUsers");

            searchTextKeywords = (TextBox) skin.FindControl("SearchTextKeyWords");

            forumList = (ForumListBox) skin.FindControl("SearchForumList");

        }
        #endregion

        #region Events
        public void SearchForums_Changed (object sender, EventArgs e) {

            if (forumList != null)
                if (forumList.Enabled == false)
                    forumList.Enabled = true;
                else
                    forumList.Enabled = false;

        }

		public void SearchKeyWords_Click (Object sender, EventArgs e) {

			try {
				string forumsToSearch = string.Empty;
				string usersToSearch = string.Empty;

				// Are we searching all or specific forums?
				//
				if ((searchForum != null) && (!searchForum.SearchAllForums)) {
                
					// Get a list of the forums we are searching
					//
					foreach (ListItem item in forumList.Items) {

						if ( (item.Selected == true) && (item.Value.StartsWith("f")) ) 
							forumsToSearch += item.Value.Replace("f-", "") + ",";

						if ( (item.Selected == true) && (item.Value.StartsWith("g")) ) {
							ForumGroup forumGroup = ForumGroups.GetForumGroup( int.Parse(item.Value.Replace("g-", "")) );

                            foreach (Spinit.Wpc.Forum.Components.Forum f in forumGroup.Forums)
								forumsToSearch += f.ForumID + ",";

						}
					}
				}

				// Are we search for posts by specific users?
				//
				if (users != null) {
					string[] usernames = users.Text.Split(',');

					foreach (string username in usernames) {

						// Attempt to get the user
						try {
							User user = Users.FindUserByUsername(username.TrimStart(' ').TrimEnd(' '));
							usersToSearch += user.UserID + ",";
						} catch {}
					}
				}

				// Do we need to encode the forums to search?
				//
				if (forumsToSearch != string.Empty)
					forumsToSearch = Spinit.Wpc.Forum.Search.ForumsToSearchEncode(forumsToSearch);

				// Do we need to encode the userid?
				//
				if (usersToSearch != string.Empty)
					usersToSearch = Spinit.Wpc.Forum.Search.ForumsToSearchEncode(usersToSearch);

				forumContext.Context.Response.Redirect( Globals.GetSiteUrls().SearchForText( forumContext.Context.Server.UrlEncode( searchTextKeywords.Text ) , forumsToSearch, usersToSearch ));
			}
			catch( Exception ex ) {
				ForumException fex = new ForumException( Spinit.Wpc.Forum.Enumerations.ForumExceptionType.SearchUnknownError, ex.Message, ex );
				fex.Log();
			}
		}
        #endregion
		
    }
}