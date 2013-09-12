using System;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using Spinit.Wpc.Forum.Components;

namespace Spinit.Wpc.Forum.Controls {

    /// <summary>
    /// Summary description for Pager.
    /// </summary>
    public class Pager : Label, INamingContainer {

        #region Member variables and constructor
        ForumContext forumContext = ForumContext.Current;
        LinkButton previousButton;
        LinkButton nextButton;
        LinkButton firstButton;
        LinkButton lastButton;
        LinkButton[] pagingLinkButtons;

        // ***************************************************
        // Controls
        //
        /// <summary>
        /// Override how this control handles its controls collection
        /// </summary>
        /// 
        public override ControlCollection Controls {

            get {
                EnsureChildControls();
                return base.Controls;
            }

        }

        // *********************************************************************
        //  CreateChildControls
        //
        /// <summary>
        /// This event handler adds the children controls and is resonsible
        /// for determining the template type used for the control.
        /// </summary>
        /// 
        // ********************************************************************/ 
        protected override void CreateChildControls() {
            Controls.Clear();

            // Add Page buttons
            //
            AddPageButtons();

            // Add Previous Next child controls
            //
            AddPreviousNextLinkButtons();

            // Add First Last child controls
            //
            AddFirstLastLinkButtons();

        }
        #endregion

        #region Render functions
        protected override void Render(HtmlTextWriter writer) {

            int totalPages = CalculateTotalPages();

            // Do we have data?
            //
            if (totalPages <= 1)
                return;

            AddAttributesToRender(writer);


            // Render the paging buttons
            //
            writer.AddAttribute(HtmlTextWriterAttribute.Class, this.CssClass, false);
            //writer.RenderBeginTag(HtmlTextWriterTag.Span);

            // Render the first button
            //
            RenderFirst(writer);

            // Render the previous button
            //
            RenderPrevious(writer);
            
            // Render the page button(s)
            //
            RenderPagingButtons(writer);

            // Render the next button
            //
            RenderNext(writer);

            // Render the last button
            //
            RenderLast(writer);

            //writer.RenderEndTag();

        }

        void RenderFirst (HtmlTextWriter writer) {

            int totalPages = CalculateTotalPages();

            if ((PageIndex >= 3) && (totalPages > 5)) {
                firstButton.RenderControl(writer);

                LiteralControl l = new LiteralControl("&nbsp;...&nbsp;");
                l.RenderControl(writer);
            }
        }

        void RenderLast (HtmlTextWriter writer) {

            int totalPages = CalculateTotalPages();

            if (((PageIndex + 3) < totalPages) && (totalPages > 5)) {
                LiteralControl l = new LiteralControl("&nbsp;...&nbsp;");
                l.RenderControl(writer);

                lastButton.RenderControl(writer);
            }

        }

        void RenderPrevious (HtmlTextWriter writer) {
            Literal l;

            if (HasPrevious) {
                previousButton.RenderControl(writer);

                l = new Literal();
                l.Text = "&nbsp;";
                l.RenderControl(writer);
            }

        }

        void RenderNext(HtmlTextWriter writer) {
            Literal l;

            if (HasNext) {

                l = new Literal();
                l.Text = "&nbsp;";
                l.RenderControl(writer);

                nextButton.RenderControl(writer);
            }

        }

        void RenderButtonRange(int start, int end, HtmlTextWriter writer) {

            for (int i = start; i < end; i++) {

                // Are we working with the selected index?
                //
                if (PageIndex == i) {

                    // Render different output
                    //
                    Literal l = new Literal();
                    //l.Text = "<span class=\"currentPage\">[" + (i + 1).ToString() + "]</span>";
					l.Text = (i + 1).ToString();

                    l.RenderControl(writer);
                } else {
                    pagingLinkButtons[i].RenderControl(writer);
                }
                if ( i < (end - 1) )
                    writer.Write(" ");

            }

        }

        void RenderPagingButtons(HtmlTextWriter writer) {
            int totalPages;

            // Get the total pages available
            //
            totalPages = CalculateTotalPages();

            // If we have <= 5 pages display all the pages and exit
            //
            if ( totalPages <= 5) {
                RenderButtonRange(0, totalPages, writer);
            } else {

                int lowerBound = (PageIndex - 2);
                int upperBound = (PageIndex + 3);

                if (lowerBound <= 0) 
                    lowerBound = 0;

                if (PageIndex == 0)
                    RenderButtonRange(0, 5, writer);

                else if (PageIndex == 1)
                    RenderButtonRange(0, (PageIndex + 4), writer);

                else if (PageIndex < (totalPages - 2))
                    RenderButtonRange(lowerBound, (PageIndex + 3), writer);

                else if (PageIndex == (totalPages - 2))
                    RenderButtonRange((totalPages - 5), (PageIndex + 2), writer);

                else if (PageIndex == (totalPages - 1))
                    RenderButtonRange((totalPages - 5), (PageIndex + 1), writer);
            }
        }
        #endregion

        #region ControlTree functions
        void AddPageButtons() {

            // First add the lower page buttons
            //
            pagingLinkButtons = new LinkButton[CalculateTotalPages()];

            // Create the buttons and add them to 
            // the Controls collection
            //
            for (int i = 0; i < pagingLinkButtons.Length; i++) {

                pagingLinkButtons[i] = new LinkButton();
                pagingLinkButtons[i].EnableViewState = false;
                pagingLinkButtons[i].Text = (i + 1).ToString();
                pagingLinkButtons[i].ID = i.ToString();
                pagingLinkButtons[i].CommandArgument = i.ToString();
                pagingLinkButtons[i].Click += new System.EventHandler(PageIndex_Click);
                Controls.Add(pagingLinkButtons[i]);

            }


        }

        void AddFirstLastLinkButtons() {

            // Create the previous button if necessary
            //
            firstButton = new LinkButton();
            firstButton.ID = "First";
            firstButton.Text = ResourceManager.GetString("Utility_Pager_firstButton");
            firstButton.CommandArgument = "0";
            firstButton.Click += new System.EventHandler(PageIndex_Click);
            Controls.Add(firstButton);

            // Create the next button if necessary
            //
            lastButton = new LinkButton();
            lastButton.ID = "Last";
            lastButton.Text = ResourceManager.GetString("Utility_Pager_lastButton");
            lastButton.CommandArgument = (CalculateTotalPages() - 1).ToString();
            lastButton.Click += new System.EventHandler(PageIndex_Click);
            Controls.Add(lastButton);

        }

        void AddPreviousNextLinkButtons() {

            // Create the previous button if necessary
            //
            previousButton = new LinkButton();
            previousButton.ID = "Prev";
            previousButton.Text = ResourceManager.GetString("Utility_Pager_previousButton");
            previousButton.CommandArgument = (PageIndex - 1).ToString();
            previousButton.Click += new System.EventHandler(PageIndex_Click);
            Controls.Add(previousButton);

            // Create the next button if necessary
            //
            nextButton = new LinkButton();
            nextButton.ID = "Next";
            nextButton.Text = ResourceManager.GetString("Utility_Pager_nextButton");
            nextButton.CommandArgument = (PageIndex + 1).ToString();
            nextButton.Click += new System.EventHandler(PageIndex_Click);
            Controls.Add(nextButton);

        }
        #endregion

        #region Private Properties
        private bool HasPrevious {
            get {
                if (PageIndex > 0)
                    return true;

                return false;
            }
        }

        private bool HasNext {
            get {
                if (PageIndex + 1 < CalculateTotalPages() )
                    return true;

                return false;
            }
        }
        #endregion

        #region Events
        // *********************************************************************
        //  IndexChanged
        //
        /// <summary>
        /// Event raised when a an index has been selected by the end user
        /// </summary>
        /// 
        // ********************************************************************/
        public event System.EventHandler IndexChanged;

        // *********************************************************************
        //  PageIndex_Click
        //
        /// <summary>
        /// Event raised when a new index is selected from the paging control
        /// </summary>
        /// 
        // ********************************************************************/
        void PageIndex_Click(Object sender, EventArgs e) {

            PageIndex = Convert.ToInt32(((LinkButton) sender).CommandArgument);

            if (null != IndexChanged)
                IndexChanged(sender, e);

        }
        #endregion

        #region Helper methods and Public Properties
        // *********************************************************************
        //  CalculateTotalPages
        //
        /// <summary>
        /// Static that caculates the total pages available.
        /// </summary>
        /// 
        // ********************************************************************/
        public int CalculateTotalPages() {
            int totalPagesAvailable;

            if (TotalRecords == 0)
                return 0;

            // First calculate the division
            //
            totalPagesAvailable = TotalRecords / PageSize;

            // Now do a mod for any remainder
            //
            if ((TotalRecords % PageSize) > 0)
                totalPagesAvailable++;

            return totalPagesAvailable;

        }

        public int PageIndex {
            get {
                int _pageIndex = 0;

                if ((!Page.IsPostBack) && (forumContext.PageIndex > 0))
                    _pageIndex = forumContext.PageIndex;
                else
                    _pageIndex = Convert.ToInt32(ViewState["PageIndex"]);

                if (_pageIndex < 0)
                    return 0;
                else
                    return _pageIndex;
            }
            set {
                ViewState["PageIndex"] = value;
            }
        }

        public int PageSize {
            get {
                int pageSize = Convert.ToInt32(ViewState["PageSize"]);

                if (pageSize == 0) {
                    if (forumContext.PostID > 0) {
                        return Globals.GetSiteSettings().PostsPerPage;
                    } else {
                        return Globals.GetSiteSettings().ThreadsPerPage;
                    }
                }

                return pageSize;
            }
            set {
                ViewState["PageSize"] = value;
            }

        }

        public int TotalRecords {
            get {
                return Convert.ToInt32(ViewState["TotalRecords"]);
            }
            set {
                ViewState["TotalRecords"] = value;
            }
        }
        #endregion

    }
}
