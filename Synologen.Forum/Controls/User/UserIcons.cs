using System;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI;
using Spinit.Wpc.Forum.Components;
using Spinit.Wpc.Forum.Enumerations;
using System.IO;
using System.Collections;

namespace Spinit.Wpc.Forum.Controls {

    public class UserIcons : PlaceHolder {
        ForumContext forumContext = ForumContext.Current;
        User user;

        protected override void Render(HtmlTextWriter writer) {
            LiteralControl literal = new LiteralControl("<br>");
            string imagePath = Globals.GetSkinPath() +"/images/";
            Image image = new Image();

            ArrayList roles = Roles.GetRoles(user.UserID);

            foreach (Role role in roles) {

                // Special case moderators
                //
                /*
                if (role.Name == "Forum-Moderators") {
                    image.ImageUrl = imagePath + "users_moderator.gif";
                    literal.RenderControl(writer);
                    image.RenderControl(writer);
                }
                */

                // Role/Icon association
                //
                string pathToImage = forumContext.Context.Server.MapPath(imagePath + role.Name.Replace(" ", "_") + ".gif");
                if (FileExists( pathToImage )) {
                    image.ImageUrl = imagePath + role.Name.Replace(" ", "_") + ".gif";
                    literal.RenderControl(writer);
                    image.RenderControl(writer);
                }
            }

        }

        public bool FileExists (string filename) {
            string cacheKey = "fileExistsLookupTable";
            Hashtable fileLookup = null;

            if (forumContext.Context.Cache[cacheKey] == null) {
                forumContext.Context.Cache[cacheKey] = new Hashtable();
            }

            // Get a reference to the object
            fileLookup = (Hashtable) forumContext.Context.Cache[cacheKey];

            if (fileLookup[filename] == null) {

                // Check if the file exists
                //
                fileLookup[filename] = File.Exists(filename);
            }

            return (bool) fileLookup[filename];

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
