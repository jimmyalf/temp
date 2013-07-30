using System;
using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Spinit.Wpc.Forum;
using Spinit.Wpc.Forum.Components;
using System.ComponentModel;
using System.IO;
using Spinit.Wpc.Forum.Enumerations;


namespace Spinit.Wpc.Forum.Controls {

    /// <summary>
    /// This Web control helps to change old user's password.
    /// </summary>
    [
    ParseChildren(true)
    ]
    public class ChangePassword : SkinnedForumWebControl {

        #region Member variables and contructor
        ForumContext forumContext = ForumContext.Current;
        protected string skinFilename = "Skin-ChangePassword.ascx";

        protected TextBox					currentPassword;
        protected TextBox					newPassword1;
        protected TextBox					newPassword2;
        protected Button					changePasswordButton;
        protected Label						userName;
		protected Panel						showCurrentPassword;
		protected RequiredFieldValidator	currentPasswordValidator;
		protected RequiredFieldValidator	newPasswordValidator;
		protected RequiredFieldValidator	validatePassword2;
		protected CompareValidator			newPasswordCompareVadlidator;
        

        User user = null;

        // *********************************************************************
        //  ChangePassword
        //
        /// <summary>
        /// Constructor
        /// </summary>
        // ***********************************************************************/
        public ChangePassword() : base() 
        {
            // Assign a default template name
            if (SkinFilename == null)
                SkinFilename = skinFilename;

            if (forumContext.UserID > 0 && forumContext.User.IsAdministrator) {
                // Change passwd from admin
                user = Users.GetUser(forumContext.UserID, true);
            } 
            else {
                // Change the user's password
                user = forumContext.User;
            }
        }
        #endregion

        #region Initialize skin
        // *********************************************************************
        //  Initializeskin
        //
        /// <summary>
        /// Initialize the control template and populate the control with values
        /// </summary>
        // ***********************************************************************/
        override protected void InitializeSkin(Control skin) {

            userName = (Label) skin.FindControl("UserName");
            if (forumContext.UserID > 0 && (forumContext.User.IsAdministrator || forumContext.User.IsModerator) ) {
                userName.Text = "- (" + user.Username + ")";
            }

            // Find the textbox controls
            //
			if ( (user.UserID != Users.GetUser().UserID) && (forumContext.User.IsAdministrator || forumContext.User.IsModerator) ) {
				showCurrentPassword = (Panel) skin.FindControl("showCurrentPassword");
				showCurrentPassword.Visible = false;
			} else {
				showCurrentPassword = (Panel) skin.FindControl("showCurrentPassword");
				showCurrentPassword.Visible = true;
				currentPassword = (TextBox) skin.FindControl("Password");

                // LN 5/26/04: Get validator reference.
                currentPasswordValidator = (RequiredFieldValidator) skin.FindControl("ValidatePassword");
                currentPasswordValidator.ErrorMessage = ResourceManager.GetString("ChangePassword_CurrentPasswordRequired");
			}

            newPassword1 = (TextBox) skin.FindControl("NewPassword1");
            newPassword2 = (TextBox) skin.FindControl("NewPassword2");

            // LN 5/26/04: Moved above on else condition.
            // Find the validators
            //
            /*
			if ( (user.UserID != Users.GetUser().UserID) && (forumContext.User.IsAdministrator || forumContext.User.IsModerator) ) {
				currentPasswordValidator = (RequiredFieldValidator) skin.FindControl("ValidatePassword");
				currentPasswordValidator.ErrorMessage = ResourceManager.GetString("ChangePassword_CurrentPasswordRequired");
			}
            */

            newPasswordValidator  = (RequiredFieldValidator) skin.FindControl("ValidatePassword1");
            newPasswordValidator.ErrorMessage = ResourceManager.GetString("ChangePassword_NewPasswordRequired");

            validatePassword2  = (RequiredFieldValidator) skin.FindControl("ValidatePassword2");
            validatePassword2.ErrorMessage = ResourceManager.GetString("ChangePassword_ReEnterNewPasswordRequired");

            newPasswordCompareVadlidator = (CompareValidator) skin.FindControl("ComparePassword");
            newPasswordCompareVadlidator.ErrorMessage = ResourceManager.GetString("ChangePassword_ReEnterNewPasswordInvalid");

            changePasswordButton = (Button) skin.FindControl("ChangePasswordButton");
            changePasswordButton.Text = ResourceManager.GetString("ChangePassword_ChangePassword");
            changePasswordButton.Click += new EventHandler(ChangePassword_Click);

			// panic capture
			//
            if (forumContext.User.IsAdministrator && forumContext.UserID == 0)
                changePasswordButton.Enabled = false;

        }
        #endregion

        #region Events
        void ChangePassword_Click (Object sender, EventArgs e) {
            bool status = false;
			string currentPass = "";


			if (currentPassword != null)
				if (currentPassword.Text != "")
					currentPass = currentPassword.Text;

            if (!Page.IsValid)
                return;
                        
            if (user != null)
                status = user.ChangePassword(currentPass, newPassword1.Text);

            if (status) {
                throw new ForumException( ForumExceptionType.UserPasswordChangeSuccess );
            } else {
                throw new ForumException( ForumExceptionType.UserPasswordChangeFailed );
            }
        }
        #endregion
    }
}

