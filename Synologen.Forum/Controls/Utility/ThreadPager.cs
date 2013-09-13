using System;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using Spinit.Wpc.Forum.Components;

namespace Spinit.Wpc.Forum.Controls {

    /// <summary>
    /// Summary description for Pager.
    /// </summary>
    public class ThreadPager : Label, INamingContainer {

        int replies = 0;
        int threadID = 0;

        protected override void Render(HtmlTextWriter writer) {
            int totalPages = CalculateTotalPages();

            // Do we have data?
            //
            if (totalPages <= 1)
                return;

			// TODO: Move HTML out of here!
			//
//            writer.Write("<br>");
            writer.RenderBeginTag(HtmlTextWriterTag.Span);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, this.CssClass, false);
            AddAttributesToRender(writer);

            writer.Write(Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_ThreadPager_Page"));

            // Render the paging buttons
            //
            RenderPagingButtons(writer, totalPages);

            writer.RenderEndTag();

        }

        void RenderButtonRange(int start, int end, HtmlTextWriter writer) {

            for (int i = start; i < end; i++) {

                HtmlAnchor anchor = new HtmlAnchor();

                anchor.HRef = Globals.GetSiteUrls().PostPaged(ThreadID, (i + 1) );
                anchor.InnerText = (i + 1).ToString();

                anchor.RenderControl(writer);

                if (i != (end - 1))
                    writer.Write(", ");

            }

        }

        void RenderPagingButtons(HtmlTextWriter writer, int totalPages) {

            // If we have < 8 pages display all the pages and exit
            //
            if ( totalPages <= 8) {
                RenderButtonRange(0, totalPages, writer);
                return;
            } else {
                RenderButtonRange(0, 2, writer);
                writer.Write(" ... ");
                RenderButtonRange(totalPages - 3, totalPages, writer);
            }

        }


        // *********************************************************************
        //  CalculateTotalPages
        //
        /// <summary>
        /// Static that caculates the total pages available.
        /// </summary>
        /// 
        // ********************************************************************/
        private int CalculateTotalPages() {
            int totalPagesAvailable;
            int pageSize = Globals.GetSiteSettings().PostsPerPage;

            if (Replies == 0)
                return 0;

            // First calculate the division
            //
            totalPagesAvailable = Replies / pageSize;

            // Now do a mod for any remainder
            //
            if ((Replies % pageSize) > 0)
                totalPagesAvailable++;

            return totalPagesAvailable;

        }

        public int Replies {
            get {
                return replies;
            }
            set {
                // Increment by 1 to include the post
                //
                replies = (value + 1);
            }
        }

        public int ThreadID {
            get {
                return threadID;
            }
            set {
                threadID = value;
            }

        }

    }
}
