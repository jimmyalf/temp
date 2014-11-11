using System;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI;
using Spinit.Wpc.Forum.Components;
using Spinit.Wpc.Forum.Enumerations;

namespace Spinit.Wpc.Forum.Controls {

    public class UserAttribute : PlaceHolder {
        ForumContext forumContext = ForumContext.Current;
        User user;
        string text;
        UserAttributes attribute;

        protected override void Render(HtmlTextWriter writer) {
            Literal l = new Literal();

            if (user.IsAnonymous)
                return;

            // Note, we could have used reflection, but that 
            // would have been slower
            //
            switch (attribute) {

                case UserAttributes.Joined:
                    l.Text = String.Format(text, ResourceManager.GetString("PostFlatView_Joined") + user.GetTimezone(user.DateCreated).ToString(forumContext.User.DateFormat));
                    break;

                case UserAttributes.Location:
                    if (user.Location == string.Empty)
                        return;

                    l.Text = String.Format(text, ResourceManager.GetString("PostFlatView_Location") + user.Location);
                    break;

                case UserAttributes.Posts:
                    l.Text = String.Format(text, ResourceManager.GetString("PostFlatView_Posts") + user.TotalPosts.ToString(ResourceManager.GetString("NumberFormat")));
                    break;

            }

            l.RenderControl(writer);

           
        }

        public User User {

            get {
                return user;
            }
            set {
                user = value;
            }

        }

        public string FormatString {
            get {
                return text;
            }
            set {
                text = value;
            }
        }

        public UserAttributes Attribute {
            get {
                return attribute;
            }
            set {
                attribute = value;
            }
        }

    }

}
