using System;
using System.Web.UI.HtmlControls;
using Spinit.Wpc.Forum;
using Spinit.Wpc.Forum.Enumerations;
using Spinit.Wpc.Forum.Components;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Spinit.Wpc.Forum.Controls {
    public class RatingImageButton : PlaceHolder {

        #region Member variables
        Thread thread;
        double rating;
        int totalRatings;
        bool moreDetail = true;
        ForumContext forumContext = ForumContext.Current;
        #endregion

        #region Render logic
        protected override void Render(HtmlTextWriter writer) {

            if (thread != null) {
                rating = thread.ThreadRating;
                totalRatings = thread.TotalRatings;
            }

            // Do we have data to display
            //
            if ((rating == 0) && (totalRatings == 0) && (moreDetail)) {
                return;
            }

            HtmlAnchor anchor = new HtmlAnchor();
            HtmlImage image = new HtmlImage();
            string text;

            if (rating <= 0.25) {
                image.Src = Globals.GetSkinPath() + "/images/Star0.gif";
                text = ResourceManager.GetString("PostRating_Zero");
            } else if (rating <= 0.5) {
                image.Src = Globals.GetSkinPath() + "/images/StarHalf.gif";
                text = ResourceManager.GetString("PostRating_One");
            } else if (rating <= 1) {
                image.Src = Globals.GetSkinPath() + "/images/Star1.gif";
                text = ResourceManager.GetString("PostRating_One");
            } else if (rating <= 1.5) {
                image.Src = Globals.GetSkinPath() + "/images/Star1half.gif";
                text = ResourceManager.GetString("PostRating_Two");
            } else if (rating <= 2) {
                image.Src = Globals.GetSkinPath() + "/images/Star2.gif";
                text = ResourceManager.GetString("PostRating_Two");
            } else if (rating <= 2.5) {
                image.Src = Globals.GetSkinPath() + "/images/Star2half.gif";
                text = ResourceManager.GetString("PostRating_Three");
            } else if (rating <= 3) {
                image.Src = Globals.GetSkinPath() + "/images/Star3.gif";
                text = ResourceManager.GetString("PostRating_Three");
            } else if (rating <= 3.5) {
                image.Src = Globals.GetSkinPath() + "/images/Star3half.gif";
                text = ResourceManager.GetString("PostRating_Four");
            } else if (rating <= 4) {
                image.Src = Globals.GetSkinPath() + "/images/Star4.gif";
                text = ResourceManager.GetString("PostRating_Four");
            } else if (rating <= 4.5) {
                image.Src = Globals.GetSkinPath() + "/images/Star4Half.gif";
                text = ResourceManager.GetString("PostRating_Five");
            } else {
                image.Src = Globals.GetSkinPath() + "/images/Star5.gif";
                text = ResourceManager.GetString("PostRating_Five");
            }


            image.Border = 0;

            if (moreDetail) {
                if (rating == 0)
                    image.Alt = string.Format(Spinit.Wpc.Forum.Components.ResourceManager.GetString("PostRating_AltText"), text, "0", thread.TotalRatings);
                else
                    image.Alt = string.Format(Spinit.Wpc.Forum.Components.ResourceManager.GetString("PostRating_AltText"), text, rating.ToString("#.##"), thread.TotalRatings);

                anchor.Controls.Add(image);
                anchor.HRef = "javascript:OpenWindow('" + Globals.GetSiteUrls().PostRating(thread.PostID) + "')";
                anchor.RenderControl(writer);
            } else {
                if (rating == 0)
                    image.Alt = string.Format(Spinit.Wpc.Forum.Components.ResourceManager.GetString("Rating_AltText"), text, "0");
                else
                    image.Alt = string.Format(Spinit.Wpc.Forum.Components.ResourceManager.GetString("Rating_AltText"), text, rating.ToString("#.##"));

                image.RenderControl(writer);
            }
        }
        #endregion

        #region PUblic Properties
        public Thread Thread {
            set { thread = value; }
        }

        public double Rating {
            get { return rating; }
            set { rating = value; }
        }

        public bool MoreDetail {
            get { return moreDetail; }
            set { moreDetail = value; }
        }
        #endregion

    }
}