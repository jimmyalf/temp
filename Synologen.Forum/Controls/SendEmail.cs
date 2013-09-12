using System;
using System.Drawing;
using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Spinit.Wpc.Forum;
using Spinit.Wpc.Forum.Components;
using System.ComponentModel;
using System.IO;
using Spinit.Wpc.Forum.Enumerations;
using Spinit.Wpc.Forum.Configuration;
using FreeTextBoxControls;

namespace Spinit.Wpc.Forum.Controls {

    [
    ParseChildren(true)	
    ]

    public class SendEmail : SkinnedForumWebControl {

        #region Member Variables
        ForumContext forumContext   = ForumContext.Current;
        string skinFilename         = "View-SendEmail.ascx";
        TextBox subject;
        FreeTextBox message;
        #endregion

        #region Constructor
        /// <remarks>
        /// Class contructor used to determine the mode of the server control.
        /// </remarks>
        public SendEmail() {

            // Assign a default template name
            //
            if (SkinFilename == null)
                SkinFilename = skinFilename;

        }

        protected override void InitializeSkin(Control skin) {
            Label label;
            Button button;
            RequiredFieldValidator validator;

            // Who the email is from
            label = (Label) skin.FindControl("FromUser");
            label.Text = forumContext.User.Username + " (" + forumContext.User.Email + ")";
            
            // Who the email is to
            label = (Label) skin.FindControl("ToUser");
            User toUser = Users.GetUser(forumContext.UserID, false);
            if (toUser != null && toUser.IsRegistered) {
                label.Text = toUser.Username;
            } else {
                throw new ForumException(ForumExceptionType.UserNotFound);
            }

            // Find the subject and body textboxes
            subject = (TextBox) skin.FindControl("Subject");
            message = (FreeTextBox) skin.FindControl("Message");

			if (Globals.GetSiteUrls().Home == "/")
				message.SupportFolder = "~/FreeTextBox/";
			else
				message.SupportFolder = "~" + ForumConfiguration.GetConfig().ForumFilesPath + "/FreeTextBox/";

            // Find the validators
            validator = (RequiredFieldValidator) skin.FindControl("SubjectValidator");
            validator.ErrorMessage = ResourceManager.GetString("SendEmail_Validator");

            validator = (RequiredFieldValidator) skin.FindControl("BodyValidator");
            validator.ErrorMessage = ResourceManager.GetString("SendEmail_Validator");

            // Find the post button
            button = (Button) skin.FindControl("PostButton");
            button.Text = ResourceManager.GetString("SendEmail_Send");
            button.Click += new EventHandler(SendEmail_Click);

            button = (Button) skin.FindControl("Cancel");
            button.Text = ResourceManager.GetString("SendEmail_Cancel");
            button.Click += new EventHandler(CancelEmail_Click);

        }

        #endregion

        #region Event Handlers
        void SendEmail_Click (Object sender, EventArgs e) {
			
			// use a post for email formatting
			Post post = new Post();
			post.Subject = subject.Text;
			post.Body = message.Text;
			post.PostType = FreeTextBoxControls.FreeTextBox.IsRichCapable(Context) ? PostType.HTML : PostType.BBCode;
			post.FormattedBody = Transforms.FormatPost(message.Text, post.PostType);			
			post.PostDate = DateTime.Now;
			post.User = Users.GetUser();
			post.Username = Users.GetUser().Username;

			User toUser = Users.GetUser( forumContext.UserID, true );

			if( toUser != null )
				Emails.UserToUser(forumContext.User, toUser, post);

			throw new ForumException(ForumExceptionType.EmailSentToUser);
        }

        void CancelEmail_Click (Object sender, EventArgs e) {
            string redirectUrl = string.Empty;

            if (forumContext.UserID > 0)
                redirectUrl = Globals.GetSiteUrls().UserProfile(forumContext.UserID, true);
            else
                redirectUrl = Globals.GetSiteUrls().Home;

            Page.Response.Redirect(redirectUrl);
            Page.Response.End();
        }
        #endregion
   }
}