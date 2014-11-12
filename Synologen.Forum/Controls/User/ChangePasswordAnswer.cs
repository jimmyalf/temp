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
    /// This Web control helps to change old user's secret question and answer.
    /// </summary>
    [
    ParseChildren(true)
    ]
    public class ChangePasswordAnswer : SkinnedForumWebControl {

        #region Member variables and contructor
        ForumContext forumContext = ForumContext.Current;
        string skinFilename = "Skin-ChangePasswordAnswer.ascx";

        TextBox currentAnswer;
        TextBox newAnswer1;
        TextBox newAnswer2;
        RequiredFieldValidator newAnswerValidator;
        CompareValidator newAnswerCompareVadlidator;
        RequiredFieldValidator validateAnswer2;
        Button changeAnswerButton;
        RegularExpressionValidator answerRegexValidator;
        RegularExpressionValidator answer1RegrexValidator;
        Label currentQuestion;
        QuestionsDropDownList newQuestion;
        Label userName;
        Panel showCurrentPasswordAnswer;

        User user = null;

        // *********************************************************************
        //  ChangePassword
        //
        /// <summary>
        /// Constructor
        /// </summary>
        // ***********************************************************************/
        public ChangePasswordAnswer() : base() 
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
            if (forumContext.UserID > 0 && forumContext.User.IsAdministrator) {
                userName.Text = "- (" + user.Username + ")";
            }

            // Find the textbox controls
            //
            if ( (user.UserID != Users.GetUser().UserID) && (forumContext.User.IsAdministrator || forumContext.User.IsModerator) ) {
                showCurrentPasswordAnswer = (Panel) skin.FindControl("showCurrentPasswordAnswer");
                showCurrentPasswordAnswer.Visible = false;
            } else {
                showCurrentPasswordAnswer = (Panel) skin.FindControl("showCurrentPasswordAnswer");
                showCurrentPasswordAnswer.Visible = true;
                currentQuestion = (Label) skin.FindControl("CurrentQuestion");
                currentAnswer = (TextBox) skin.FindControl("Answer");

                answerRegexValidator = (RegularExpressionValidator) skin.FindControl("AnswerRegexValidator");
                answerRegexValidator.ErrorMessage = ResourceManager.GetString("ChangePasswordAnswer_RegExInvalidAnswer");
            }

            // Find the textbox controls
            //
            newAnswer1 = (TextBox) skin.FindControl("NewAnswer1");
            newAnswer2 = (TextBox) skin.FindControl("NewAnswer2");

            // Find the validators
            //
            newAnswerValidator  = (RequiredFieldValidator) skin.FindControl("ValidateAnswer1");
            newAnswerValidator.ErrorMessage = ResourceManager.GetString("ChangePasswordAnswer_NewAnswerRequired");

            answer1RegrexValidator = (RegularExpressionValidator) skin.FindControl("Answer1RegrexValidator");
            answer1RegrexValidator.ErrorMessage = ResourceManager.GetString("ChangePasswordAnswer_RegExInvalidAnswer1");

            newAnswerCompareVadlidator = (CompareValidator) skin.FindControl("CompareAnswer");
            newAnswerCompareVadlidator.ErrorMessage = ResourceManager.GetString("ChangePasswordAnswer_ReEnterNewAnswerRequired");

            validateAnswer2 = (RequiredFieldValidator) skin.FindControl("ValidateAnswer2");
            validateAnswer2.ErrorMessage = ResourceManager.GetString("ChangePasswordAnswer_ReEnterAnswerRequired");

            newQuestion = (QuestionsDropDownList) skin.FindControl("NewQuestion");
            
            try {
                if (user.PasswordQuestion == string.Empty || 
                    user.PasswordQuestion == null) {
                    currentQuestion.Text = ResourceManager.GetString("ChangePasswordAnswer_NotAvailable");
                    currentQuestion.ForeColor = System.Drawing.Color.Red;
                } else {
                    currentQuestion.Text = user.PasswordQuestion;
                    currentQuestion.Font.Bold = true;
                }
            } catch {}

            changeAnswerButton = (Button) skin.FindControl("ChangeAnswerButton");
            changeAnswerButton.Text = ResourceManager.GetString("ChangePasswordAnswer_ChangeAnswer");
            changeAnswerButton.Click += new EventHandler(ChangePasswordAnswer_Click);

            // panic code :D
            if (forumContext.User.IsAdministrator && forumContext.UserID == 0)
                changeAnswerButton.Enabled = false;
        }
        #endregion

        #region Events
        void ChangePasswordAnswer_Click (Object sender, EventArgs e) {
            bool status = false;
            string currAnswer = "";

            if (currentAnswer != null)
                if (currentAnswer.Text != "")
                    currAnswer = currentAnswer.Text;

            if (!Page.IsValid)
                return;
            
            if (user != null)
                status = user.ChangePasswordAnswer(currAnswer, newQuestion.SelectedValue, newAnswer1.Text);
            
            if (status) {
                throw new ForumException(ForumExceptionType.UserPasswordAnswerChangeSuccess);
            } else {
                throw new ForumException(ForumExceptionType.UserPasswordAnswerChangeFailed);
            }
        }
        #endregion
    }
}

