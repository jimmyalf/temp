using System;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI;
using Spinit.Wpc.Forum.Components;
using Spinit.Wpc.Forum.Enumerations;

namespace Spinit.Wpc.Forum.Controls {

    public class UserImageButtons : PlaceHolder {

        #region Member variables
        User user;
        ForumContext forumContext = ForumContext.Current;
        UserImageButtonMode userImageButtonMode = UserImageButtonMode.All;
        #endregion

        #region Render logic
        protected override void Render(HtmlTextWriter writer) {

            switch (userImageButtonMode) {

                case (UserImageButtonMode.Email):
//                    Email(writer, false);
                    break;

                case (UserImageButtonMode.HomePage):
                    HomePage(writer, false);
                    break;

                case (UserImageButtonMode.PrivateMessages):
                    PrivateMessage(writer, false);
                    break;

                case (UserImageButtonMode.WebLog):
                    WebLog(writer, false);
                    break;

                case (UserImageButtonMode.Search):
                    Search(writer, false);
                    break;

                default: // All
//                    Email(writer, true);
                    PrivateMessage(writer, true);
                    HomePage(writer, true);
                    WebLog(writer, true);
                    Search(writer, true);

                    break;

            }
            // User's profile
            //
            //            l.ImageUrl = Globals.GetSkinPath() + "/images/post_button_profile.gif";
            //            l.NavigateUrl = Globals.GetSiteUrls().UserProfile(user.UserID);
            //            l.RenderControl(writer);

            // User's IM
            //
/* Currently disabled
            if (!user.IsAnonymous) {
                writer.Write(" ");
                l.ImageUrl = Globals.GetSkinPath() + "/images/post_button_buddy.gif";
                l.NavigateUrl = "TODO";
                l.RenderControl(writer);

                // User's stats
                //
                writer.Write(" ");
                l.ImageUrl = Globals.GetSkinPath() + "/images/post_button_stats.gif";
                l.NavigateUrl = "TODO";
                l.RenderControl(writer);
            }
*/
        }

        // User's email
        //
        void Email(HtmlTextWriter writer, bool whitespace) {
			if  (!user.IsAnonymous) 
			{

				HyperLink l = new HyperLink();

				if (whitespace)
					writer.Write(" ");

				l.ImageUrl = Globals.GetSkinPath() + "/images/post_button_email.gif";
				l.NavigateUrl = Globals.GetSiteUrls().EmailToUser(user.UserID);
				l.ToolTip = string.Format(ResourceManager.GetString("UserImageButtons_Email"), user.Username);
				l.RenderControl(writer);
			}
        }

        // User's Private Messages
        //
        void PrivateMessage(HtmlTextWriter writer, bool whitespace) {
			if  (!user.IsAnonymous) 
			{
				HyperLink l = new HyperLink();
                
				if (whitespace)
					writer.Write(" ");

				l.ImageUrl = Globals.GetSkinPath() + "/images/post_button_pm.gif";
				l.NavigateUrl = Globals.GetSiteUrls().PrivateMessage(user.UserID);
				l.ToolTip = string.Format(ResourceManager.GetString("UserImageButtons_PrivateMessage"), user.Username);
				l.RenderControl(writer);
			}
        }

        // User's home page
        //
        void HomePage(HtmlTextWriter writer, bool whitespace) {
            if ((user.WebAddress != String.Empty) && (!user.IsAnonymous)) {
                HyperLink l = new HyperLink();
                
                if (whitespace)
                    writer.Write(" ");
                
                l.Target = "_new";
                l.ImageUrl = Globals.GetSkinPath() + "/images/post_button_www.gif";
                l.NavigateUrl = user.WebAddress;
                l.ToolTip = string.Format(ResourceManager.GetString("UserImageButtons_HomePage"), user.Username);
                l.RenderControl(writer);
            }
        }

        // User's web log
        //
        void WebLog (HtmlTextWriter writer, bool whitespace) {
            if ((user.WebLog != String.Empty) && (!user.IsAnonymous)) {
                HyperLink l = new HyperLink();
                
                if (whitespace)
                    writer.Write(" ");
                
                l.Target = "_new";
                l.ImageUrl = Globals.GetSkinPath() + "/images/post_button_weblog.gif";
                l.NavigateUrl = user.WebLog;
                l.ToolTip = string.Format(ResourceManager.GetString("UserImageButtons_WebLog"), user.Username);
                l.RenderControl(writer);
            }
        }

        // Search
        //
        void Search (HtmlTextWriter writer, bool whitespace) {
            if ((user.TotalPosts > 0) && (!user.IsAnonymous)) {
                HyperLink l = new HyperLink();
                
                if (whitespace)
                    writer.Write(" ");
                
                l.ImageUrl = Globals.GetSkinPath() + "/images/post_button_search.gif";
                l.NavigateUrl = Globals.GetSiteUrls().SearchByUser( user.UserID);
                l.ToolTip = string.Format(ResourceManager.GetString("UserImageButtons_SearchFor"), user.Username);
                l.RenderControl(writer);
            }

        }

        #endregion

        #region Public properties
        public User User {
            get {
                return user;
            }
            set {
                user = value;
            }
        }

        public UserImageButtonMode Mode {
            get { return userImageButtonMode; }
            set { userImageButtonMode = value; }
        }
        #endregion
    }

}
