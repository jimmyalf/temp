using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Spinit.Wpc.Forum.Enumerations;
using Spinit.Wpc.Forum.Components;

namespace Spinit.Wpc.Forum.Controls {
    /// <summary>
    /// Summary description for SortColumn.
    /// </summary>
    public class SortableThreadColumn : Control {
        ForumContext forumContext = (ForumContext) HttpContext.Current.Items["ForumContext"];
        HtmlAnchor button = new HtmlAnchor();
        SortThreadsBy sortBy = SortThreadsBy.LastPost;
        string text;

        protected override void Render(HtmlTextWriter writer) {

            string sortImgDesc = "<img border=\"0\" src=\"" + Globals.GetSkinPath() +"/images/sort_up.gif\" />";
            string sortImg = "<img border=\"0\" src=\"" + Globals.GetSkinPath() +"/images/sort_down.gif\" />";
            string sortHref = Globals.GetSiteUrls().Forum(forumContext.ForumID) + "&sb=" + (int) SortBy + "&d=" + (int) SortOrder.Ascending;
            string sortHrefDesc = Globals.GetSiteUrls().Forum(forumContext.ForumID) + "&sb=" + (int) SortBy + "&d=" + (int) SortOrder.Descending;
            string href = sortHref;

            // Are we sorting by this item?
            //
            if (IsSelectedSortBy) {

                // Set the image to display
                //
                if (IsSelectedSortOrderDescending) {
                    text = Text + sortImgDesc;
                    href = sortHref;
                } else {
                    text = Text + sortImg;
                    href = sortHrefDesc;
                }

            } else {
                text = Text;
            }

            button.HRef = href;
            button.InnerHtml = text;
            button.RenderControl(writer);

        }

        public bool IsSelectedSortOrderDescending {
            get {
                int _sortOrder = ForumContext.GetIntFromQueryString(HttpContext.Current, "d");

                if (_sortOrder > -1)
                    if ( ((SortOrder) _sortOrder) == SortOrder.Descending)
                        return true;

                return false;
            }
        }


        public bool IsSelectedSortBy {

            get {
                int _sortBy = ForumContext.GetIntFromQueryString(HttpContext.Current, "sb");

                if (_sortBy > -1)
                    if ( ((SortThreadsBy) _sortBy) == SortBy)
                        return true;

                return false;
            }

        }

        public string Text {
            get {
                return text;
            }
            set {
                text = value;
            }
        }

        public SortThreadsBy SortBy {
            get {
                return sortBy;
            }
            set {
                sortBy = value;
            }
        }

        public SortOrder Direction {
            get {
                SortOrder returnValue = SortOrder.Ascending;

                if (ViewState["SortOrder"] != null)
                    returnValue = (SortOrder) ViewState["SortOrder"];

                return returnValue;
            }
            set {
                ViewState["SortOrder"] = value;
            }
        }

        private void ToggleDirection(SortOrder sortOrder) {

            if (sortOrder == SortOrder.Ascending)
                Direction = SortOrder.Descending;
            else
                Direction = SortOrder.Ascending;
        }

        public string CssClass {
            get {
                return button.Attributes["Class"];
            }
            set {
                button.Attributes["Class"] = value;
            }
        }

    }
}
