using System;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;

namespace Spinit.Wpc.Forum.Controls {

    /// <summary>
    /// Summary description for CurrentPage.
    /// </summary>
    public class CurrentPage : Label {
        public string text = Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_CurrentPage_text");

        protected override void Render(HtmlTextWriter writer) {

			Control skin = (Control) this.Parent;
			Panel displayPager = (Panel) skin.FindControl("DisplayPager");

            // Do we have data?
            //
			//if (TotalPages <= 1)	{
			//	if (displayPager != null)
			//		displayPager.Visible = false;
			//	return;
			//}

			if (displayPager != null)
				displayPager.Visible = true;

            this.Text = String.Format(text, PageIndex, TotalPages.ToString("n0"), TotalRecords.ToString("n0") );

            base.Render(writer);

        }

        public string TextFormat {
            get {
                return text;
            }
            set {
                text = value;
            }
        }

        public int PageIndex {
            get {
                int pageIndex = Convert.ToInt32(ViewState["PageIndex"]);

                if (pageIndex == 0)
                    return 1;

                return pageIndex;
            }
            set {
                ViewState["PageIndex"] = value + 1;
            }
        }

        public int TotalPages {
            get {
                int totalPages = Convert.ToInt32(ViewState["TotalPages"]);

                if (totalPages == 0)
                    return 1;

                return totalPages;
            }
            set {
                ViewState["TotalPages"] = value;
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

    }
}
