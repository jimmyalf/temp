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

    public class RoleIcons : PlaceHolder {

        #region Member variables
        ForumContext forumContext = ForumContext.Current;
        User user;
        #endregion

        #region Render functions
        protected override void Render(HtmlTextWriter writer) {

            if (user.IsAnonymous)
                return;

            string imagePath = Globals.GetSkinPath() +"/images/roleicons/";
            LiteralControl literal = new LiteralControl("<br>");
            HtmlAnchor anchor;
            Image image;

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
				
				// old "role name" method
				//string pathToImage = forumContext.Context.Server.MapPath(imagePath + role.Name.Replace(" ", "_") + ".gif");

				// look into the new path of images/roleicons/ for the roleID.gif
				string pathToImage = forumContext.Context.Server.MapPath(imagePath + role.RoleID.ToString() + ".gif");
                if (FileExists( pathToImage )) {
                    writer.Write(Formatter.Whitespace(2,0,false,true));
                    image = new Image();
                    anchor = new HtmlAnchor();

					image.AlternateText = role.Name + "\n\n" + role.Description;
                    image.ImageUrl = imagePath + role.RoleID.ToString() + ".gif";
                    literal.RenderControl(writer);

                    anchor.Controls.Add(image);
                    anchor.HRef = Globals.GetSiteUrls().UserRoles( role.RoleID );

                    anchor.RenderControl(writer);
                }
            }

        }
        #endregion

        #region Private methods
        private bool FileExists (string filename) {
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
        #endregion

    }

}
