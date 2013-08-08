using System;
using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
using System.IO;

using Spinit.Wpc.Forum;
using Spinit.Wpc.Forum.Components;
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
    public class ForgottenPassword : SkinnedForumWebControl {

        #region Member variables and contructor
        string skinFilename = "Skin-ForgottenPassword.ascx";
        ForumContext forumContext = ForumContext.Current;
        TextBox emailAddress;
        Button sendPasswordButton;
        RequiredFieldValidator emailValidator;
        RegularExpressionValidator emailRegexValidator;
        ValidationSummary validationMsg;
        Button nextButton;
        HtmlTable step1Panel;
        HtmlTable step2Panel;
        Label sQuestion;
        TextBox sAnswer;
        TextBox newPassword;
        TextBox retypePassword;
        RequiredFieldValidator sAnswerValidator;
        RegularExpressionValidator sAnswerRegexValidator;
        RequiredFieldValidator newPasswordValidator;
        RequiredFieldValidator retypePasswordValidator;
        CompareValidator comparePassword;

        Spinit.Wpc.Forum.Enumerations.PasswordRecovery recoveryMethod;

        // *********************************************************************
        //  CreateUser
        //
        /// <summary>
        /// Constructor
        /// </summary>
        // ***********************************************************************/
        public ForgottenPassword() : base() {

            // Assign a default template name
            if (SkinFilename == null)
                SkinFilename = skinFilename;
            
            this.recoveryMethod = Globals.GetSiteSettings().PasswordRecovery;
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

            step1Panel = (HtmlTable) skin.FindControl("Step1Panel");
            step2Panel = (HtmlTable) skin.FindControl("Step2Panel");

            // Text box for email address
            emailAddress = (TextBox) skin.FindControl("EmailAddress");

            // Button to submit
            sendPasswordButton = (Button) skin.FindControl("SendPassword");
            sendPasswordButton.Text = ResourceManager.GetString("ForgottenPassword_SendButton");
            sendPasswordButton.Click += new EventHandler (SendPassword_Click);

            // Validators
            emailValidator = (RequiredFieldValidator) skin.FindControl("EmailValidator");
            emailValidator.ErrorMessage = ResourceManager.GetString("ForgottenPassword_RequiredField");
            emailRegexValidator = (RegularExpressionValidator) skin.FindControl("EmailRegexValidator");
            emailRegexValidator.ErrorMessage = ResourceManager.GetString("ForgottenPassword_InvalidEmailAddress");          
            validationMsg = (ValidationSummary) skin.FindControl("ValidationMsg");
            
            // Next Button
            nextButton = (Button) skin.FindControl("Next");
            nextButton.Text = ResourceManager.GetString("ForgottenPassword_NextButton");
            nextButton.Click += new EventHandler (Next_Click);
            
            sQuestion = (Label) skin.FindControl("SQuestion");

            sAnswer = (TextBox) skin.FindControl("SAnswer");
            newPassword = (TextBox) skin.FindControl("NewPassword");
            retypePassword = (TextBox) skin.FindControl("RetypePassword");

            sAnswerValidator = (RequiredFieldValidator) skin.FindControl("SAnswerValidator");
            sAnswerValidator.ErrorMessage = ResourceManager.GetString("ForgottenPassword_SecretAnswerRequired");          

            sAnswerRegexValidator = (RegularExpressionValidator) skin.FindControl("SAnswerRegexValidator");
            sAnswerRegexValidator.ErrorMessage = ResourceManager.GetString("ForgottenPassword_InvalidSecretAnswer");          

            newPasswordValidator = (RequiredFieldValidator) skin.FindControl("NewPasswordValidator");
            newPasswordValidator.ErrorMessage = ResourceManager.GetString("ForgottenPassword_PasswordRequired");          

            retypePasswordValidator = (RequiredFieldValidator) skin.FindControl("RetypePasswordValidator");
            retypePasswordValidator.ErrorMessage = ResourceManager.GetString("ForgottenPassword_PasswordRequired");          

            comparePassword = (CompareValidator) skin.FindControl("ComparePassword");
            comparePassword.ErrorMessage = ResourceManager.GetString("ForgottenPassword_WrongPassword");          

            // Reset common validators
            emailValidator.IsValid = true;
            emailRegexValidator.IsValid = true;

            if (recoveryMethod == Spinit.Wpc.Forum.Enumerations.PasswordRecovery.Reset)
            {
                // Reset
                step1Panel.Visible = true;
                step2Panel.Visible = false;

                nextButton.Visible = false;
                sendPasswordButton.Visible = true;
            }
            else {
                // Q & A
                if (this.Step == 1) {        
                    step1Panel.Visible = true;
                    step2Panel.Visible = false;
         
                    nextButton.Visible = true;
                    sendPasswordButton.Visible = false;   
                } 
                else if (this.Step == 2) {
                    step1Panel.Visible = true;
                    step2Panel.Visible = true;

                    nextButton.Visible = false;
                    sendPasswordButton.Visible = true;
                    
                    // Reset few more validators
                    sAnswerValidator.IsValid = true;
                    sAnswerRegexValidator.IsValid = true;
                    newPasswordValidator.IsValid = true;
                    retypePasswordValidator.IsValid = true;
                    comparePassword.IsValid = true;
                }
            }
        }
        #endregion

        #region Events
        void Next_Click(Object sender, EventArgs e) {
            
            User user = ValidateUserOnStep(this.Step);

            // If null there was a validation error
            //
            if (user == null)
                return;

            // If PasswordQuestion is not set, then stop here 
            // and Reset user's password.
            //
            if (user.PasswordQuestion == null || 
                user.PasswordQuestion == string.Empty) {

                // Set recovery method on Reset
                    this.recoveryMethod = Spinit.Wpc.Forum.Enumerations.PasswordRecovery.Reset;
                // Update user's password in datastore & display operation's status
                this.UpdatePassword(ref user);

                return;
            }

            // Go on to next step
            //
            this.Step = 2;   
            sQuestion.Text = user.PasswordQuestion;
 
            step1Panel.Visible = true;
            step2Panel.Visible = true;     
   
            nextButton.Visible = false;
            sendPasswordButton.Visible = true;
        }

        void SendPassword_Click(Object sender, EventArgs e) {

            User user = null;

            // Validate on current step
            user = ValidateUserOnStep(this.Step);
                        
            // If null there was a validation error
            if(user == null)
                return;

            // Set user object with new password
            user.Password = newPassword.Text.Trim();

            // Update user's password in datastore & dispaly operation's status
            this.UpdatePassword(ref user);
        }
        #endregion
        
        #region Helper Methods & Properties
        public int Step {
            get {
                if (ViewState["RecoveryStep"] != null)
                    return int.Parse(ViewState["RecoveryStep"].ToString());
                else
                    return 1;
            }
            set {
                ViewState["RecoveryStep"] = value;
            }
        }

        private void UpdatePassword(ref User user) {

            bool updatePassword = false;

            // Now set a new password
            //
            updatePassword = user.ForgotPassword(this.recoveryMethod);

            // Display operation status to user
            //
            if (updatePassword == false) {
                throw new ForumException(ForumExceptionType.UserPasswordChangeFailed);
            } 
            else {
                throw new ForumException(ForumExceptionType.UserPasswordChangeSuccess);
            }
        }

        private User ValidateUserOnStep(int step) {
            
            // No mather what recovery method we use.
            // We check step no. instead.
            //
            User user = null;

            if (step == 1 || step == 2) {
                // Validate step 1 and 2
                //
                
                // Fire Validators
                emailValidator.Validate();
                emailRegexValidator.Validate();

                if (!emailValidator.IsValid || !emailRegexValidator.IsValid)
                    return null;

                // Look up the user
                user = Users.FindUserByEmail(emailAddress.Text);

                // Is valid?
                if (user == null || (user != null && user.UserID <= 0)) {
                    emailValidator.ErrorMessage = ResourceManager.GetString("ForgottenPassword_EmailNotFound");
                    emailValidator.IsValid = false;
                    return null;
                }
            }

            if (step == 2) {
                // Validate step 2 only
                //
                
                // Fire Validators
                sAnswerValidator.Validate();
                sAnswerRegexValidator.Validate();
                newPasswordValidator.Validate();
                retypePasswordValidator.Validate();
                comparePassword.Validate();

                if (!sAnswerValidator.IsValid || 
                    !sAnswerRegexValidator.IsValid ||
                    !newPasswordValidator.IsValid ||
                    !retypePasswordValidator.IsValid ||
                    !comparePassword.IsValid)
                    return null;

                // We have a valid user,
                // so get the password answer and validate it.
                user.PasswordAnswer = sAnswer.Text.Trim();

                if (!Users.ValidPasswordAnswer(user)) {
                    sAnswerRegexValidator.IsValid = false;
                    return null;
                }
            }

            return user;
        }
        #endregion
    }
}
