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

    public class PostIcons: PlaceHolder {

        #region Member variables
        ForumContext forumContext = ForumContext.Current;
        User user;
        #endregion

        #region Render functions
        protected override void Render(HtmlTextWriter writer) {
            string img = "<br><img alt=\"{0}\" src=\"" +  Globals.GetSkinPath() + "/images/rankicons/{1}.gif" + "\">";

            if (user.IsAnonymous)
                return;

            switch (user.PostRank[0]) {
                case 1:
                    writer.Write( string.Format(img, Spinit.Wpc.Forum.Components.ResourceManager.GetString("User_PostIcons_Top10"), "rankTop10"));
                    break;

                case 2:
                    writer.Write( string.Format(img, Spinit.Wpc.Forum.Components.ResourceManager.GetString("User_PostIcons_Top25"), "rankTop25"));
                    break;

                case 4:
                    writer.Write( string.Format(img, Spinit.Wpc.Forum.Components.ResourceManager.GetString("User_PostIcons_Top50"), "rankTop50"));
                    break;

                case 8:
                    writer.Write( string.Format(img, Spinit.Wpc.Forum.Components.ResourceManager.GetString("User_PostIcons_Top75"), "rankTop75"));
                    break;

                case 16:
                    writer.Write( string.Format(img, Spinit.Wpc.Forum.Components.ResourceManager.GetString("User_PostIcons_Top100"), "rankTop100"));
                    break;

                case 32:
                    writer.Write( string.Format(img, Spinit.Wpc.Forum.Components.ResourceManager.GetString("User_PostIcons_Top150"), "rankTop150"));
                    break;

                case 64:
                    writer.Write( string.Format(img, Spinit.Wpc.Forum.Components.ResourceManager.GetString("User_PostIcons_Top200"), "rankTop200"));
                    break;

                case 128:
                    writer.Write( string.Format(img, Spinit.Wpc.Forum.Components.ResourceManager.GetString("User_PostIcons_Top500"), "rankTop500"));
                    break;

                default:
                    writer.Write( string.Format(img, Spinit.Wpc.Forum.Components.ResourceManager.GetString("User_PostIcons_NotRanked"), "rank0"));
                    break;
            }

            // Do we have a gender for the user?
            //
            if ((Globals.GetSiteSettings().AllowGender) && (user.Gender != Gender.NotSet))
                if (user.Gender == Gender.Male)
                    writer.Write( string.Format(img, Spinit.Wpc.Forum.Components.ResourceManager.GetString("Male"), "GenderMale"));
                else
                    writer.Write( string.Format(img, Spinit.Wpc.Forum.Components.ResourceManager.GetString("Female"), "GenderFemale"));



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
