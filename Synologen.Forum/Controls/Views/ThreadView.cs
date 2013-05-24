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
    //  ThreadView
    //
    /// <summary>
    /// This server control is used to display top level threads. Note, a thread
    /// is a post with more information. The Thread class inherits from the Post
    /// class.
    /// </summary>
    /// 
    // ********************************************************************/
    public class ThreadView : SkinnedForumWebControl {

        #region Member variables and constructor
        string skinFilename              = "View-Threads.ascx";
        
        ForumContext forumContext = ForumContext.Current;
        internal Spinit.Wpc.Forum.Components.Forum forum;
        Control threadDisplay;
        Control noThreadsToDisplay;
        internal Repeater announcements;
        internal Repeater threads;
        Pager pager;
        CurrentPage currentPage;
        ThreadSet threadSet;
        DateFilter dateFilter;
        SortThreadsBy sortBy = SortThreadsBy.LastPost;
        SortOrder sortOrder = SortOrder.Ascending;
        ThreadViewMode mode = ThreadViewMode.Default;
        Label forumName;
        Label forumDescription;
		EmailNotificationDropDownList ddlTracking;
        ThreadSortDropDownList threadSortddl;
        SortOrderDropDownList sortOrderddl;
        Button sortButton;
        Button rememberSettingsButton;
        HideReadPostsDropDownList hideReadPosts;
        FilterUsersDropDownList filterUsers;
		

        // *********************************************************************
        //  ThreadView
        //
        /// <summary>
        /// The constructor simply checks for a ForumID value passed in via the
        /// HTTP POST or GET.
        /// properties.
        /// </summary>
        /// 
        // ********************************************************************/
        public ThreadView() {

            try {

                int modeFromQS = Int32.Parse(forumContext.Context.Request.QueryString["mode"]);

                // What mode are we running in?
                //
                if (modeFromQS > 0)
                    mode = (ThreadViewMode) modeFromQS;

            } catch {
                mode = ThreadViewMode.Default;
            }

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

            // Find the controls we need to databind
            //
            forumName = (Label) skin.FindControl("ForumName");
            forumDescription = (Label) skin.FindControl("ForumDescription");
            threadDisplay = skin.FindControl("ThreadDisplay");
            noThreadsToDisplay = skin.FindControl("NoThreadsToDisplay");
            filterUsers = (FilterUsersDropDownList) skin.FindControl("FilterUsers");
            threads = (Repeater) skin.FindControl("Threads");
            announcements = (Repeater) skin.FindControl("Announcements");
            hideReadPosts = (HideReadPostsDropDownList) skin.FindControl("HideReadPosts");
            threadSortddl = (ThreadSortDropDownList) skin.FindControl("SortThreads");
            sortOrderddl = (SortOrderDropDownList) skin.FindControl("SortOrder");

            // Ensure we have a valid forum
            //
            try {
                switch (mode) {
                    case ThreadViewMode.Active:
                        forum = new Spinit.Wpc.Forum.Components.Forum(-1);
                        forumName.Text = ResourceManager.GetString("ViewActiveThreads_Title");
                        forumDescription.Text = ResourceManager.GetString("ViewActiveThreads_Description");
                        break;

                    case ThreadViewMode.Unanswered:
                        forum = new Spinit.Wpc.Forum.Components.Forum(-1);
                        forumName.Text = ResourceManager.GetString("ViewUnansweredThreads_Title");
                        forumDescription.Text = ResourceManager.GetString("ViewUnansweredThreads_Description");
                        break;

                    case ThreadViewMode.PrivateMessages:
                        forum = Forums.GetForum(0);
                        forumName.Text = ResourceManager.GetString("PrivateMessages_Title");
                        forumDescription.Text = ResourceManager.GetString("PrivateMessages_Description");
                        break;

                    default:
                        int forumID = forumContext.ForumID;

                        if (forumID > 0) {
                            forum = Forums.GetForum(forumID);
                            forumName.Text = forum.Name;
                            forumDescription.Text = forum.Description;
                        }
                        break;

                }
            } catch {

                throw new ForumException(ForumExceptionType.ForumNotFound, forumContext.ForumID.ToString());

            }

            // Ensure we have an authenticated user if the user is asking for the private messages
            //
            if ((forumContext.ForumID == 0) && (forumContext.User.IsAnonymous))
                throw new ForumException(ForumExceptionType.ForumNotFound, forumContext.ForumID.ToString());

            sortButton = (Button) skin.FindControl("SortThreadsButton");
            if (sortButton != null) {
                sortButton.Text = ResourceManager.GetString("ViewThreads_SortThreads");
                sortButton.Click += new EventHandler(Sort_Changed);
            }


            rememberSettingsButton = (Button) skin.FindControl("RememberSettingsButton");
            if (rememberSettingsButton != null) {
                rememberSettingsButton.Text = ResourceManager.GetString("ViewThreads_RememberSettings");
                rememberSettingsButton.Click += new EventHandler(RememberSortSettings_Click);
            }

            // Subscribe to the date filter's changed event
            //
            dateFilter = (DateFilter) skin.FindControl("DateFilter");

            if (dateFilter != null)
                dateFilter.DateChanged += new EventHandler(Sort_Changed);
            else
                dateFilter = new DateFilter();

            // Find the Pager and current page controls
            //
            pager = (Pager) skin.FindControl("Pager");
            currentPage = (CurrentPage) skin.FindControl("CurrentPage");

            // Do we have any sortBy / sortOrder data?
            //
            SetSort();

            // Subscribe to the pager's Index Changed event
            //
            pager.IndexChanged += new EventHandler(Sort_Changed);

            // Do we have any rembered settings to apply?
            if (!forumContext.User.IsAnonymous) {
                UserCookie cookie = new UserCookie(forumContext.User);

                ForumUserOptions options = cookie.GetForumOptions(forumContext.ForumID);

                if (options.HasSettings) {
                    
                    dateFilter.Items.FindByValue( ((int) options.DateFilter).ToString() ).Selected = true;
                    
                    if (hideReadPosts != null)
                        hideReadPosts.SelectedValue = options.HideReadPosts;

                    if (filterUsers != null)
                        filterUsers.SelectedValue = options.UserFilter;

                    threadSortddl.SelectedValue = options.SortBy;
                    sortOrderddl.SelectedValue = options.SortOrder;

                }

            } else if (mode != ThreadViewMode.Default) {
                if (!forumContext.User.IsAnonymous)
                    ddlTracking.Visible = false;
            }

            if (forumContext.User.IsAnonymous) {
                filterUsers.Visible = false;
                hideReadPosts.Visible = false;
                rememberSettingsButton.Visible = false;
                dateFilter.Items.FindByValue( ((int) Globals.GetSiteSettings().DefaultThreadDateFilter).ToString() );
            }

			
			// User permissions on new post button
			//
			ForumPermission p = forum.Permission;
			User user = Users.GetUser();
			if (p.Post == AccessControlEntry.Deny)
			{
				ForumImageButton newPostButton = (ForumImageButton) skin.FindControl("NewPostButton");
				if (newPostButton != null)
				{
					newPostButton.Visible = false;
				}
			}			


            DataBind();


        }
        #endregion

        #region Internal helper methods
        private void SetSort() {

            HttpContext context = HttpContext.Current;

            int _sortBy = ForumContext.GetIntFromQueryString(context, "sb");
            int _sortOrder = ForumContext.GetIntFromQueryString(context, "d");

            if (_sortBy > -1)
                sortBy = (SortThreadsBy) _sortBy;

            if (_sortOrder > -1)
                sortOrder = (SortOrder) _sortOrder;

        }
        #endregion

        #region Events
        public void Sort_Changed (Object sender, EventArgs e) {

            DataBind();
        }

        public void RememberSortSettings_Click (Object sender, EventArgs e) {
            string forumSettings = (int) threadSortddl.SelectedValue + ":" + (int) sortOrderddl.SelectedValue + ":" + (int) dateFilter.SelectedValue + ":" + hideReadPosts.SelectedValue.ToString().Substring(0,1) + ":" + (int) filterUsers.SelectedValue;
            UserCookie userCookie = new UserCookie( forumContext.User );
            userCookie.SaveForumOptions(forumContext.ForumID, forumSettings);
            Sort_Changed(sender, e);
        }

        #endregion

        #region Databinding
        public override void DataBind() {
            DateTime dateFilterValue;
            int forumID = -1;

            if ((forum != null) && (forum.ForumID > 0))
                forumID = forum.ForumID;

            if (dateFilter.SelectedValue == ThreadDateFilterMode.LastVisit)
                dateFilterValue = Users.GetUser().GetUserCookie().LastVisit;
            else
                dateFilterValue = dateFilter.SelectedDate;


            // Get a populated thread set
            //
            switch (mode) {

                case ThreadViewMode.Active:
                    threadSet = Threads.GetThreads(forumID, pager.PageIndex, pager.PageSize, Users.GetUser().UserID, dateFilterValue, threadSortddl.SelectedValue, sortOrderddl.SelectedValue, ThreadStatus.NotSet, filterUsers.SelectedValue, true, hideReadPosts.SelectedValue, false, true);

                    // Do we have data?
                    //
                    if (threadSet.Threads.Count == 0) {
                        throw new ForumException(ForumExceptionType.ForumNoActivePosts);
                    }
                    
                    break;

                case ThreadViewMode.Unanswered:
                    
                    threadSet = Threads.GetThreads(forumID, pager.PageIndex, pager.PageSize, Users.GetUser().UserID, dateFilterValue, threadSortddl.SelectedValue, sortOrderddl.SelectedValue, ThreadStatus.NotSet, filterUsers.SelectedValue, false, hideReadPosts.SelectedValue, true, true);

                    // Do we have data?
                    //
                    if (threadSet.Threads.Count == 0) {
                        throw new ForumException(ForumExceptionType.ForumNoUnansweredPosts);
                    }

                    break;

                case ThreadViewMode.PrivateMessages:
                    threadSet = Threads.GetThreads(0, pager.PageIndex, pager.PageSize, forumContext.User.UserID, dateFilterValue, threadSortddl.SelectedValue, sortOrderddl.SelectedValue, ThreadStatus.NotSet, ThreadUsersFilter.All, false, false, false,  true);
                    break;

                default:
                    if (forumID > 0)
                        threadSet = Threads.GetThreads(forumID, pager.PageIndex, pager.PageSize, Users.GetUser().UserID, dateFilterValue, threadSortddl.SelectedValue, sortOrderddl.SelectedValue, ThreadStatus.NotSet, filterUsers.SelectedValue, false, hideReadPosts.SelectedValue, false, true);
                    else
                        throw new ForumException(ForumExceptionType.ForumNotFound);

                    break;

            }

            if ((threadSet.Announcements.Count > 0) && (mode != ThreadViewMode.Unanswered) && (mode != ThreadViewMode.Active) ) {
                announcements.DataSource = threadSet.Announcements;
                announcements.DataBind();
            } else if ( (mode != ThreadViewMode.Unanswered) && (mode != ThreadViewMode.Active) )  {
                announcements.DataSource = null;
                announcements.DataBind();
            }

            // Do we have posts to display?
            //
            if (threadSet.Threads.Count > 0) {
                threads.DataSource = threadSet.Threads;
                threads.DataBind();
            } else {
                threads.DataSource = null;
                threads.DataBind();
            }

            // Do we have announcements or threads?
            //
            if ((threads.DataSource == null) && (announcements.DataSource == null)) {
                noThreadsToDisplay.Visible = true;
            } else {
                noThreadsToDisplay.Visible = false;
            }


            pager.TotalRecords = currentPage.TotalRecords = threadSet.TotalRecords;
            currentPage.TotalPages = pager.CalculateTotalPages();
            currentPage.PageIndex = pager.PageIndex;

        }
        #endregion

        #region Public Properties
        /// <value>
        /// Controls the mode that the thread view control displays
        /// </value>
        public ThreadViewMode ThreadViewMode {
            get {
                return mode;
            }
            set {
                mode = value;
            }
        }
        #endregion


    }

}