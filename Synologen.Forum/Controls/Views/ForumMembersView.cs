using System;
using System.Drawing;
using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Spinit.Wpc.Forum;
using Spinit.Wpc.Forum.Components;
using System.ComponentModel;
using System.IO;
using Spinit.Wpc.Forum.Enumerations;

namespace Spinit.Wpc.Forum.Controls {

    /// <summary>
    /// This server control is used to display all the members of the current forum.
    /// </summary>
    [
    ParseChildren(true)
    ]
    public class ForumMembersView : SkinnedForumWebControl {
    
        #region Member variables and constructor
        protected Repeater userList;
        protected Pager pager;
        protected AlphaPicker alphaPicker;
        protected CurrentPage currentPage;
        protected TextBox searchForUser;
        protected Button searchButton;
        protected MemberSortDropDownList sort;
        protected SortOrderDropDownList sortOrder;
        protected Label sectionTitle;
        protected Label sectionDescription;
        bool enableAlphaPicker = true;
        bool includeEmailInFilter = false;
        UserAccountStatus accountStatus = UserAccountStatus.Approved;
        ForumContext forumContext = ForumContext.Current;

        HtmlTableRow accountStatusRow = null;
        AccountStatusDropDownList currentAccountStatus = null;
//        HtmlTableRow noRecordsRow = null;

        UserSet userSet = null;

        // Define the default skin for this control
        private const string skinFilename = "View-ForumMembers.ascx";
        
        // *********************************************************************
        //  ForumMembersView
        //
        /// <summary>
        /// Constructor
        /// </summary>
        /// 
        // ********************************************************************/
        public ForumMembersView() {

            // Assign a default template name
            if (SkinFilename == null)
                SkinFilename = skinFilename;

        }
        #endregion

        #region Render functions
        // *********************************************************************
        //  InitializeControlTemplate
        //
        /// <summary>
        /// Initializes the user control loaded in CreateChildControls. Initialization
        /// consists of finding well known control names and wiring up any necessary events.
        /// </summary>
        /// 
        // ********************************************************************/ 
        override protected void InitializeSkin(Control skin) {

            // LN 5/20/04: Use AccountStatus Selector for Admin Mode only
            //
            if (Mode == ForumMode.Administrator || Mode == ForumMode.Moderator) {

                accountStatusRow = (HtmlTableRow) skin.FindControl("AccountStatusPanel");

                if (accountStatusRow != null) {
                    accountStatusRow.Visible = true;

                    currentAccountStatus = (AccountStatusDropDownList) skin.FindControl("CurrentAccountStatus");

                    if (currentAccountStatus != null) {

                        currentAccountStatus.AutoPostBack = true;
                        currentAccountStatus.SelectedIndexChanged += new EventHandler(FilterChanged_Click);
                    
						if (!Page.IsPostBack)
                            currentAccountStatus.Items.FindByValue(accountStatus.ToString());
                    }
                }

            }
            
            // Find the user list repeater
            //
            userList = (Repeater) skin.FindControl("UserList");
            // LN 5/20/04: Added to dynamicaly control footer's content.
            userList.ItemDataBound += new RepeaterItemEventHandler(UserList_ItemDataBound);

            // Find the sort drop down list
            //
            sort = (MemberSortDropDownList) skin.FindControl("Sort");
            sort.AutoPostBack = true;
            sort.SelectedIndexChanged += new EventHandler(FilterChanged_Click);

            // Find the sort order drop down list
            //
            sortOrder = (SortOrderDropDownList) skin.FindControl("SortOrder");
            sortOrder.AutoPostBack = true;
            sortOrder.SelectedIndexChanged += new EventHandler(FilterChanged_Click);

            // Find the search textbox/button
            //
            searchForUser = (TextBox) skin.FindControl("SearchForUser");
            searchButton = (Button) skin.FindControl("SearchButton");

            // Do we have search text?
            //
            if ((!Page.IsPostBack) && 
				(forumContext.QueryText != null) ) {
                SearchText = forumContext.QueryText;
                IsSearchMode = true;
            }

            // Set text
            searchButton.Text = Spinit.Wpc.Forum.Components.ResourceManager.GetString("ForumMembers_SearchButton");

            // Find the Pager and current page controls
            //
            pager = (Pager) skin.FindControl("Pager");
            currentPage = (CurrentPage) skin.FindControl("CurrentPage");

            // Event wire-up
            //
            alphaPicker = (AlphaPicker) skin.FindControl("AlphaPicker");
            alphaPicker.Letter_Changed += new EventHandler(FilterChanged_Click);

            if (!EnableAlphaPicker)
                alphaPicker.Visible = false;

            pager.IndexChanged += new EventHandler(PageIndex_Changed);
            searchButton.Click += new EventHandler(Search_Click);

            // Set the title and description if they haven't been set by a derived class
            //
            if ((sectionTitle == null) && (sectionDescription == null)) {
                
                // Swith the displayed text depending on the mode the control is in
                //
                sectionTitle = (Label) skin.FindControl("SectionTitle");
                if (Mode == ForumMode.User)
                    sectionTitle.Text = ResourceManager.GetString("ForumMembers_Inline1");
                else if (Mode == ForumMode.Administrator)
                    sectionTitle.Text = ResourceManager.GetString("ForumMembers_AdminTitle");

                sectionDescription = (Label) skin.FindControl("SectionDescription");
                if (Mode == ForumMode.User)
                    sectionDescription.Text = ResourceManager.GetString("ForumMembers_Inline2");
                else if (Mode == ForumMode.Administrator)
                    sectionDescription.Text = ResourceManager.GetString("ForumMembers_AdminDescription");
            }

            // Only databind if we're not posting back
            //
            this.DataBind();
        }
        #endregion

        #region Events
        // *********************************************************************
        //  UserList_ItemDataBound
        //
        /// <summary>
        /// Handle repeater's ItemDataBound event.
        /// </summary>
        /// 
        // ********************************************************************/
        private void UserList_ItemDataBound(Object sender, RepeaterItemEventArgs e) 
        {
            // LN 5/20/04: Enable/Disable NoRecordsRow control from Footer
            //
            if (e.Item.ItemType == ListItemType.Footer) {     
                e.Item.EnableViewState = false; // Do not comment this line!

                HtmlTableRow noRecordsRow = e.Item.FindControl("NoRecordsPanel") as HtmlTableRow;
                if (noRecordsRow != null) {
                    if (userSet != null && userSet.HasResults == false) {                
                        noRecordsRow.Visible = true;                                        
                    }
                    else {
                        noRecordsRow.Visible = false;                                        
                    }
                }
            }
        }

        // *********************************************************************
        //  Search_Click
        //
        /// <summary>
        /// Event raised when searching for a user
        /// </summary>
        /// 
        // ********************************************************************/
        private void Search_Click(Object sender, EventArgs e) 
        {

            if (Mode == ForumMode.User) {
                if (!Globals.GetSiteSettings().EnablePublicAdvancedMemberSearch) {
                    if (searchForUser.Text.IndexOf("*") > 0)
                        throw new ForumException(ForumExceptionType.UserSearchNotFound);
                }

                forumContext.Context.Response.Redirect( Globals.GetSiteUrls().SearchForUser( searchForUser.Text ));
                forumContext.Context.Response.End();
            } else if (Mode == ForumMode.Administrator) {
                forumContext.Context.Response.Redirect( Globals.GetSiteUrls().SearchForUserAdmin( searchForUser.Text ));
                forumContext.Context.Response.End();
            }

        }

        // *********************************************************************
        //  FilterChanged_Click
        //
        /// <summary>
        /// Event raised when the filter conditions set during post-back change
        /// </summary>
        /// 
        // ********************************************************************/
        private void FilterChanged_Click(Object sender, EventArgs e) {

            // We are not in search mode
            //
            IsSearchMode = false;

            // Clear the search text
            //
            searchForUser.Text = "";

            // LN 5/20/04: Get selected account status in Admin Mode only
            //
            if ((Mode == ForumMode.Administrator || Mode == ForumMode.Moderator) && 
                currentAccountStatus != null) {
                accountStatus = currentAccountStatus.SelectedValue;
            }

            // Reset the pager
            //
            pager.PageIndex = 0;
            currentPage.PageIndex = 0;

            DataBind();

        }

        // *********************************************************************
        //  PageIndex_Changed
        //
        /// <summary>
        /// Event raised when the selected index of the page has changed.
        /// </summary>
        /// 
        // ********************************************************************/
        private void PageIndex_Changed(Object sender, EventArgs e) {
            DataBind();
        }

        #endregion

        #region Databinding

        public override void DataBind() {

			bool showHiddenUsers = false;
			if (this.Mode == Spinit.Wpc.Forum.Enumerations.ForumMode.Administrator || this.Mode == Spinit.Wpc.Forum.Enumerations.ForumMode.Moderator)
				showHiddenUsers = true;

            if (Mode != ForumMode.User)
                includeEmailInFilter = true;

            pager.PageSize = Globals.GetSiteSettings().MembersPerPage;
            
            // Are we in search mode?
            //
            if (IsSearchMode) {

                // Does the text box have data?
                //
                if (searchForUser.Text == string.Empty) {
                    searchForUser.Text = SearchText;
                } else {
                    SearchText = searchForUser.Text;
                }

				// TDD 3/6/04 
				// force a pull of all users if we are in admin mode
				//
				// EAD 6/27/04
				// only cache if not admin/moderator
				//
                userSet = Users.GetUsers(pager.PageIndex, pager.PageSize, sort.SelectedSortOrder, sortOrder.SelectedValue, SearchText, includeEmailInFilter, (showHiddenUsers) ? false : true, accountStatus, true, showHiddenUsers );

            } else {
				// TDD 3/6/04
				// force a pull of all users if we are in admin mode
				//
				// EAD 6/27/04
				// only cache if not admin/moderator
				//
                userSet = Users.GetUsers(pager.PageIndex, pager.PageSize, sort.SelectedSortOrder, sortOrder.SelectedValue, alphaPicker.SelectedLetter, includeEmailInFilter, (showHiddenUsers) ? false : true, accountStatus, true, showHiddenUsers);

            }

            // Do we have data to display?
            // LN 5/20/04: Changed to perform old behavior only in User mode.
            // Otherwise we need to have a "no records" display line in footer.
            // Please see UserList_ItemDataBound(...).
            //
            if (!userSet.HasResults && Mode == ForumMode.User) {
                throw new ForumException(ForumExceptionType.UserSearchNotFound);
            }

			// EAD 6/27/2004: If in admin/moderator mode, we can not edit the user.
			// This should show a list, even if just 1, so the user cna search again.  Do not redirect.
			//
            // If this is a user search and we only have a single results, redirect to that user
            //
//            if ((IsSearchMode) && (userSet.TotalRecords == 1)) {
//                Page.Response.Redirect( Globals.GetSiteUrls().UserProfile( ((User) userSet.Users[0]).UserID ));
//                Page.Response.End();
//            }

			// remove the Anonymous user from being listed for administrators.
			//
			if (showHiddenUsers) {
				foreach (User testAnonUser in userSet.Users) {
					if (testAnonUser.UserID < 1) {
						userSet.Users.Remove(testAnonUser);
						break;
					}
				}
			}

            userList.DataSource = userSet.Users;
            userList.DataBind();

            pager.TotalRecords = currentPage.TotalRecords = userSet.TotalRecords;
            currentPage.TotalPages = pager.CalculateTotalPages();
            currentPage.PageIndex = pager.PageIndex;
            
        }
        #endregion

        #region Private properties

        // *********************************************************************
        //  IsSearchMode
        //
        /// <summary>
        /// Private property to determine if we're in search mode or doing a linear
        /// walkthrough of users
        /// </summary>
        /// 
        // ********************************************************************/ 
        protected bool IsSearchMode {
            get {
                if (ViewState["SearchMode"] == null)
                    return false;

                return (bool) ViewState["SearchMode"];
            }
            set {
                ViewState["SearchMode"] = value;
            }
        }

        // *********************************************************************
        //  SearchText
        //
        /// <summary>
        /// Private property u
        /// </summary>
        /// 
        // ********************************************************************/ 
        protected string SearchText {
            get {
                return (string) ViewState["SearchText"];
            }
            set {
                ViewState["SearchText"] = value;
            }
        }
        #endregion

        #region Public properties

        public bool EnableAlphaPicker {
            get { return enableAlphaPicker; }
            set { enableAlphaPicker = value; }
        }

        #endregion

    }
}