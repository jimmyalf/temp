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
    public class SearchResultsView : SkinnedForumWebControl {

        #region Constructor and Member Variables
        ForumContext forumContext = ForumContext.Current;
        string skinFilename = "View-SearchResults.ascx";
        string searchQuery;
        string[] searchForums;
        string[] searchUsers;
        TextBox searchTextTop;
        TextBox searchTextBottom;
        Button searchButtonTop;
        Button searchButtonBottom;
        DataList searchDataList;
        Literal resultTotal;
        Literal searchDuration;
        Pager pager;

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
        public SearchResultsView() {

            // Assign a default template name
            if (SkinFilename == null)
                SkinFilename = skinFilename;

            searchForums = Spinit.Wpc.Forum.Search.ForumsToSearchDecode ( forumContext.Context.Request.QueryString["f"] ) ;
            searchUsers = Spinit.Wpc.Forum.Search.UsersToDecode (forumContext.Context.Request.QueryString["u"] );

        }
        #endregion

        #region Render
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

            // Find all the controls
            //
            searchTextTop           = (TextBox) skin.FindControl("SearchTextTop");
            searchTextBottom        = (TextBox) skin.FindControl("SearchTextBottom");
            searchButtonTop         = (Button) skin.FindControl("SearchButtonTop");
            searchButtonBottom      = (Button) skin.FindControl("SearchButtonBottom");
            searchButtonTop.Text    = ResourceManager.GetString("Search");
            searchButtonBottom.Text = ResourceManager.GetString("Search");
            searchDataList          = (DataList) skin.FindControl("SearchDataList");
            pager                   = (Pager) skin.FindControl("Pager");
            resultTotal             = (Literal) skin.FindControl("TotalResults");
            resultTotal.Text        = ResourceManager.GetString("SearchResults_TotalResults");
            searchDuration          = (Literal) skin.FindControl("SearchDuration");
            searchDuration.Text     = ResourceManager.GetString("SearchResults_SearchDuration");

            // Set up the OnItemCommand for the DataList
            //
            searchDataList.ItemCommand += new DataListCommandEventHandler(Display_Item);

            searchButtonTop.Click += new EventHandler(Search_Click);
            searchButtonBottom.Click += new EventHandler(Search_Click);

            // Set the pager size
            //
            pager.PageSize = Globals.GetSiteSettings().ThreadsPerPage;

            // Setup the pager event
            //
            pager.IndexChanged += new EventHandler(Index_Changed);

            searchQuery = forumContext.Context.Request.QueryString["q"];

            // Set the values in our textboxes
            //
            searchTextTop.Text = searchQuery;
            searchTextBottom.Text = searchQuery;

            DataBind();

        }
        #endregion

        public void Search_Click (Object sender, EventArgs e) {
            bool isSearchTextInTopTextBox = true;

            // Which search text is being used?
            //
            if ( ((Button) sender).ID != "SearchButtonTop" )
                isSearchTextInTopTextBox = false;

            // Set the text to the correct values
            //
            if (isSearchTextInTopTextBox)
                searchTextBottom.Text = searchTextTop.Text;
            else
                searchTextTop.Text = searchTextBottom.Text;

            // Reset the page index
            //
            pager.PageIndex = 0;

            DataBind();
        }

        public override void DataBind() {
            SearchResultSet searchResult;
            int resultCountUpper, resultCountLower;

            // Get the search results
            // 
            searchResult = Spinit.Wpc.Forum.Search.GetSearchResults(pager.PageIndex, pager.PageSize, Users.GetUser().UserID, searchForums, searchUsers, searchTextTop.Text);

            // Do we have search results?
            //
            if (!searchResult.HasResults) {
//                NoResults();
                return;
            }

            // Set the TotalRecords in the pager
            //    
            pager.TotalRecords = searchResult.TotalRecords;

            // Databind
            //
            searchDataList.DataSource = searchResult.Posts;
            searchDataList.DataBind();

            resultCountLower = 1 + (pager.PageIndex * pager.PageSize);

            // Set the upper bounds of the search results text
            //
            if (pager.TotalRecords > pager.PageSize) 
                resultCountUpper = pager.PageSize + (pager.PageSize * pager.PageIndex);
            else
                resultCountUpper = searchResult.TotalRecords;

            searchDuration.Text = string.Format(searchDuration.Text, searchResult.SearchDuration);
            resultTotal.Text = string.Format(resultTotal.Text, resultCountLower.ToString("n0"), resultCountUpper.ToString("n0"), pager.TotalRecords.ToString("n0"));

        }

        public void Index_Changed (Object sender, EventArgs e) {
            DataBind();
        }


        public void Display_Item (Object sender, DataListCommandEventArgs e) {

            HttpContext.Current.Response.Write( e.CommandArgument );
            //    SearchResults.SelectedIndex = e.Item.ItemIndex;
            DataBind();

        }
		
    }
}