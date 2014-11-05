using System;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI;
using Spinit.Wpc.Forum.Components;

namespace Spinit.Wpc.Forum.Controls {

    public class UserAvatar : PlaceHolder {
        ForumContext forumContext = ForumContext.Current;
        User user;
        bool padImage = true;
		int border = 0;

        protected override void Render(HtmlTextWriter writer) {
            HtmlImage image = new HtmlImage();
            Literal literal = new Literal();

            if ( (User.HasAvatar) && (User.EnableAvatar) ) {

                if (padImage)
                    writer.Write(Formatter.Whitespace(2,0,true,true));


                if (!User.AvatarUrl.StartsWith("users/avatar.aspx")) {
					image.Border = border;
                    image.Src = user.AvatarUrl;
                    image.Height = Globals.GetSiteSettings().AvatarHeight;
                    image.Width = Globals.GetSiteSettings().AvatarWidth;
                } else {
                    image.Src = Globals.ApplicationPath + "/" + user.AvatarUrl;
                }

                // Render the image
                //
                image.RenderControl(writer);

            }

        }

        public User User {

            get {
                if (user == null)
                    user = Users.GetUser( ForumContext.Current.UserID, false);

                return user;
            }
            set {
                user = value;
            }
        }

        public bool PadImage {
            get { return padImage; }
            set { padImage = value; }
        }

		public int Border {
			get { 
				return border; 
			}
			set {
				border = value;
			}
		}
    }

}
