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
    /// This Web control displays the textboxes needed to create a new user account.
    /// The user can enter their username and email, and a warning message is displayed if
    /// either the username or email message is already taken.  Once the user enters an available
    /// username and password, they will be sent a confirmation email with their password.
    /// </summary>
    [
        ParseChildren(true)
    ]
    public class CreateUser : SkinnedForumWebControl {

		#region Member Variables
        AccountActivation activation = Globals.GetSiteSettings().AccountActivation;
        ForumContext forumContext = ForumContext.Current;

        bool redirect = true;
        string skinFilename = "Skin-CreateNewAccount.ascx";
        TextBox username;
        TextBox password;
        TextBox emailAddress;
        Button cancelButton;
        Button createButton;
        RequiredFieldValidator usernameValidator;
        RequiredFieldValidator passwordValidator;
        RequiredFieldValidator emailValidator;
        RequiredFieldValidator password2Validator;
        RequiredFieldValidator email2Validator;
        RegularExpressionValidator emailRegExValidator;
        RequiredFieldValidator placeHolderValidator;
        RegularExpressionValidator usernameRegExValidator;
        CompareValidator compareEmail;
        CompareValidator comparePassword;
		#endregion

		#region Base class
        // *********************************************************************
        //  CreateUser
        //
        /// <summary>
        /// Constructor
        /// </summary>
        // ***********************************************************************/
        public CreateUser() : base() {

            // If the user is an Administrator or moderator, they can create accounts
            //
			User user = Users.GetUser();
            if (user.IsAdministrator || user.IsModerator) {
                activation = AccountActivation.Automatic;
            } else {
                if (!Globals.GetSiteSettings().AllowNewUserRegistration)
                    throw new ForumException(ForumExceptionType.UserAccountRegistrationDisabled);
            }

            // Assign a default template name
            if (SkinFilename == null)
                SkinFilename = skinFilename;

        }
		#endregion

		#region Init Skin
        // *********************************************************************
        //  Initializeskin
        //
        /// <summary>
        /// Initialize the control template and populate the control with values
        /// </summary>
        // ***********************************************************************/
        override protected void InitializeSkin(Control skin) {

            // What account activation mode are we in?
            //
            switch (activation) {

                case (AccountActivation.Automatic) :
                    skin.FindControl("AccountActivationAutomatic").Visible = true;
                    skin.FindControl("AccountActivationAutomatic2").Visible = true;
                  break;
                case (AccountActivation.Email):
                  break;
                case (AccountActivation.AdminApproval):
                    skin.FindControl("AccountActivationAutomatic").Visible = true;
                    skin.FindControl("AccountActivationAutomatic2").Visible = true;
                  break;

            }

            // Find the button on the user control and wire-it up to the CreateUser_Click event in this class
            createButton = (Button) skin.FindControl("CreateAccount");
            createButton.Text = Spinit.Wpc.Forum.Components.ResourceManager.GetString("CreateNewAccount_CreateAccount");
            createButton.Click += new System.EventHandler(CreateUser_Click);

            // Find the cancel button on the user control and wire-it up to the Cancel_Click event in this class
            cancelButton = (Button) skin.FindControl("Cancel");
            cancelButton.Text = Spinit.Wpc.Forum.Components.ResourceManager.GetString("Cancel");
            cancelButton.Click += new System.EventHandler(Cancel_Click);

            // Find the other controls
            username = (TextBox) skin.FindControl("Username");
            password = (TextBox) skin.FindControl("Password");
            
            emailAddress = (TextBox) skin.FindControl("Email");
            usernameValidator = (RequiredFieldValidator) skin.FindControl("usernameValidator");
            usernameValidator.ErrorMessage = Spinit.Wpc.Forum.Components.ResourceManager.GetString("CreateNewAccount_usernameValidator").Replace("'", @"\'");
            usernameRegExValidator = (RegularExpressionValidator) skin.FindControl("usernameRegExValidator");
            usernameRegExValidator.ErrorMessage = Spinit.Wpc.Forum.Components.ResourceManager.GetString("CreateNewAccount_usernameRegExValidator").Replace("'", @"\'");
            passwordValidator = (RequiredFieldValidator) skin.FindControl("passwordValidator");
            passwordValidator.ErrorMessage = Spinit.Wpc.Forum.Components.ResourceManager.GetString("CreateNewAccount_passwordValidator").Replace("'", @"\'");
            emailValidator = (RequiredFieldValidator) skin.FindControl("emailValidator");
            emailValidator.ErrorMessage = Spinit.Wpc.Forum.Components.ResourceManager.GetString("CreateNewAccount_emailValidator").Replace("'", @"\'");
            emailRegExValidator = (RegularExpressionValidator) skin.FindControl("emailRegExValidator");
            emailRegExValidator.ErrorMessage = Spinit.Wpc.Forum.Components.ResourceManager.GetString("CreateNewAccount_emailRegExValidator").Replace("'", @"\'");
            placeHolderValidator = (RequiredFieldValidator) skin.FindControl("placeHolderValidator");
            comparePassword = (CompareValidator) skin.FindControl("ComparePassword");
            comparePassword.ErrorMessage = Spinit.Wpc.Forum.Components.ResourceManager.GetString("ChangePassword_ReEnterNewPasswordRequired").Replace("'", @"\'");
            compareEmail = (CompareValidator) skin.FindControl("CompareEmail");
            compareEmail.ErrorMessage = Spinit.Wpc.Forum.Components.ResourceManager.GetString("CreateNewAccount_emailNoMatch").Replace("'", @"\'");
            password2Validator = (RequiredFieldValidator) skin.FindControl("password2Validator");
            password2Validator.ErrorMessage = Spinit.Wpc.Forum.Components.ResourceManager.GetString("CreateNewAccount_passwordValidator").Replace("'", @"\'");
            email2Validator = (RequiredFieldValidator) skin.FindControl("email2Validator");
            email2Validator.ErrorMessage = Spinit.Wpc.Forum.Components.ResourceManager.GetString("CreateNewAccount_emailValidator").Replace("'", @"\'");

        }
		#endregion

        #region Events
        // *********************************************************************
        //  CreateUser_Click
        //
        /// <summary>
        /// This event handler fires when the submit button is clicked and the
        /// form posts back.  It is responsible for updating the user's info
        /// </summary>
        //
        // ********************************************************************/
        private void CreateUser_Click(Object sender, EventArgs e) {
            CreateUserStatus status = CreateUserStatus.UnknownFailure;

            // Is valid?
            if (!Page.IsValid)
                return;

            // Reset the placeHolderValidator
            //
            placeHolderValidator.IsValid = true;

            // try to create the new user account
            User user = new User();
            user.Username = username.Text;			
            user.Email = emailAddress.Text;

            // What account activation mode are we in?
            //
			switch (activation) {
				case (AccountActivation.AdminApproval): 
					user.Password = String.Empty;
					break;

				case (AccountActivation.Email):
					user.Password = String.Empty;
					break;

				case (AccountActivation.Automatic):
					user.Password = password.Text.Trim();
					break;

				default:
					user.Password = String.Empty;
					break;					
			}

            // Does the user require approval?
            //
            if (activation == AccountActivation.AdminApproval)
                user.AccountStatus = UserAccountStatus.ApprovalPending;
            else
                user.AccountStatus = UserAccountStatus.Approved;
            
            // Set the Anonymous flag to false
            //
            user.IsAnonymous = false;

            // Attempt to create the user
            //
			if (user.Username == "Anonymous") {
				status = CreateUserStatus.DuplicateUsername;
			} else {

				// EAD TODO 6/26/2004: We need to move this to the Admin panel to make the admin
				// decide to send an email or not.
				//
				// For now, we're changing to "true" send email to everyone for any
				// reason.  Including new users with auto-activation.  This logic is
				// handled in the Users.Create() method.
				//
				status = Users.Create(user, true);
			}

            // Determine if the account was created successfully
            //
            switch (status) {

                // Username already exists!
                case CreateUserStatus.DuplicateUsername:
                    placeHolderValidator.ErrorMessage = Spinit.Wpc.Forum.Components.ResourceManager.GetString("CreateNewAccount_CreateUserStatus_DuplicateUsername");
                    placeHolderValidator.IsValid = false;
                    break;

                // Email already exists!
                case CreateUserStatus.DuplicateEmailAddress:
                    placeHolderValidator.ErrorMessage = Spinit.Wpc.Forum.Components.ResourceManager.GetString("CreateNewAccount_CreateUserStatus_DuplicateEmailAddress");
                    placeHolderValidator.IsValid = false;
                    break;

                // Unknown failure has occurred!
                case CreateUserStatus.UnknownFailure:
                    placeHolderValidator.ErrorMessage = Spinit.Wpc.Forum.Components.ResourceManager.GetString("CreateNewAccount_CreateUserStatus_UnknownFailure");
                    placeHolderValidator.IsValid = false;
                    break;

                // Username is disallowed
                case CreateUserStatus.DisallowedUsername:
                    placeHolderValidator.ErrorMessage = Spinit.Wpc.Forum.Components.ResourceManager.GetString("CreateNewAccount_CreateUserStatus_DisallowedUsername");
                    placeHolderValidator.IsValid = false;
                    break;

                // Everything went off fine, good
                case CreateUserStatus.Created:

                    if (!redirect) {
                        placeHolderValidator.ErrorMessage = Spinit.Wpc.Forum.Components.ResourceManager.GetString("CreateNewAccount_CreateUserStatus_Created");
                        placeHolderValidator.IsValid = false;
                    } else {

                      if (activation == AccountActivation.AdminApproval)
                          throw new ForumException(ForumExceptionType.UserAccountPending);
                      if(activation == AccountActivation.Email)
                          throw new ForumException(ForumExceptionType.UserAccountCreated);
                      if(activation == AccountActivation.Automatic)
                          throw new ForumException(ForumExceptionType.UserAccountCreatedAuto);
                    }
                    break;
            }			
        }

        void Cancel_Click (Object sender, EventArgs e) {
            // send the user back to from where they came
            Context.Response.Redirect(forumContext.ReturnUrl);
            Context.Response.End();

        }
        #endregion

		#region public properties
        // *********************************************************************
        //  Redirect
        //
        /// <summary>
        /// Optionally don't perform redirect when creating a new user
        /// </summary>
        // ***********************************************************************/
        public bool Redirect {
            get {
                return redirect;
            }
            set {
                redirect = value;
            }
        }
		#endregion
    }
}