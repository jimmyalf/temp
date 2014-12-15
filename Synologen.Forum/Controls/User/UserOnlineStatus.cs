using System;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI;
using Spinit.Wpc.Forum.Components;

namespace Spinit.Wpc.Forum.Controls {

    public class UserOnlineStatus : PlaceHolder {
        ForumContext forumContext = ForumContext.Current;
        User user;

        protected override void Render(HtmlTextWriter writer) {
            Image image = new Image();
            DateTime date;

            if (user.IsAnonymous)
                return;

            // Personalize
            //
            date = user.LastActivity;
            if (forumContext.User.UserID > 0) {
                date = user.GetTimezone(date);
            }

            if (user.LastActivity > DateTime.Now.AddMinutes(-15)) {
                image.ImageUrl = Globals.GetSkinPath() + "/images/user_IsOnline.gif";
                image.AlternateText = user.Username + Spinit.Wpc.Forum.Components.ResourceManager.GetString("User_UserOnlineStatus_isOnline") + date;
            } else {
                image.ImageUrl = Globals.GetSkinPath() + "/images/user_IsOffline.gif";
                image.AlternateText = user.Username + Spinit.Wpc.Forum.Components.ResourceManager.GetString("User_UserOnlineStatus_isNotOnline") + date;
            }

            // Render the image
            //
            image.RenderControl(writer);


        }

        public User User {

            get {
                return user;
            }
            set {
                user = value;
            }

        }

    }

}
