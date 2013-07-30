using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Spinit.Wpc.Forum;
using Spinit.Wpc.Forum.Components;
using Spinit.Wpc.Forum.Enumerations;

namespace Spinit.Wpc.Forum.Controls.PostDisplay {

    public class TextPost : SkinnedForumWebControl {

        string skinFilename = "PostType-TextPost.ascx";
        Post post;
		User user;

        public TextPost() {

            // Assign a default template name
            if (SkinFilename == null)
                SkinFilename = skinFilename;

        }

        // *********************************************************************
        //  Initializeskin
        //
        /// <summary>
        /// Initializes the user control loaded in CreateChildControls. Initialization
        /// consists of finding well known control names and wiring up any necessary events.
        /// </summary>
        /// 
        // ********************************************************************/ 
        protected override void InitializeSkin(Control skin) {

			// very minimal code should be, to keep the speed of post display
			// quick and efficient.  item left to optimize are EditNotes.
			//

            Literal body;
            Literal signature;
            Literal editNotes;

            // Find the controls we need
            //
            body = (Literal) skin.FindControl("Body");
            signature = (Literal) skin.FindControl("Signature");
            editNotes = (Literal) skin.FindControl("EditNotes");

            body.Text = post.FormattedBody;

			// set the user's formatted signature
			//
			signature.Text = Globals.FormatSignature(post.User.SignatureFormatted);

			// construct the user object
			//
			user = Users.GetUser();	

			if (Globals.GetSiteSettings().DisplayEditNotesInPost) {
				// TDD 3/18/2004
				// This is another huge performance hit. Again we should only be processing the edit notes
				// once when they are initially created not for each view of the post.
				if (!user.IsAnonymous) {
					if (user.IsAdministrator || user.IsModerator) {
						if (post.EditNotes != null) {
							string formattedNotes = Globals.HtmlNewLine + Globals.HtmlNewLine + Globals.HtmlNewLine + "<table width=\"75%\" class=\"editTable\"><tr><td>" + Formatter.EditNotes(post.EditNotes) + "</td></tr></table>";
							editNotes.Text = formattedNotes.Replace("\n", Globals.HtmlNewLine);
							// editNotes.Text = post.EditNotes;
						}
					}
				}
			}
        }

        public Post Post {
            get {
                return post;
            }
            set {
                post = value;
            }
        }

    }

}
