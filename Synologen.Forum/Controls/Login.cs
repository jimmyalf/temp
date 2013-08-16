using System;
using System.Drawing;
using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Spinit.Wpc.Forum;
using Spinit.Wpc.Forum.Components;
using Spinit.Wpc.Forum.Enumerations;
using System.ComponentModel;
using System.Web.Security;

namespace Spinit.Wpc.Forum.Controls {

    // *********************************************************************
    //  Login
    //
    /// <summary>
    /// This server control renders and handles the login UI for the user.
    /// </summary>
    // ***********************************************************************/
    [
    ParseChildren(true)
    ]
    public class Login : SkinnedForumWebControl {

        ForumContext forumContext = ForumContext.Current;
        string skinFilename = "Skin-Login.ascx";
        TextBox username;
        TextBox password;
        Button loginButton;
        CheckBox autoLogin;

        // *********************************************************************
        //  Login
        //
        /// <summary>
        /// Constructor
        /// </summary>
        // ***********************************************************************/
        public Login() : base() {

            // Assign a default template name
            if (SkinFilename == null)
                SkinFilename = skinFilename;

        }

        // *********************************************************************
        //  CreateChildControls
        //
        /// <summary>
        /// This event handler adds the children controls.
        /// </summary>
        // ***********************************************************************/
        protected override void CreateChildControls() {

            // If the user is already authenticated we have no work to do
            if (Page.Request.IsAuthenticated) {

                // If the URL is for the login page and the user is already logged in
                // we need to throw an access denied exception
                if (Globals.GetSiteUrls().Login.StartsWith(forumContext.Context.Request.Path)) 
                    throw new ForumException (ForumExceptionType.AccessDenied, forumContext.ReturnUrl);

                return;
            }

            base.CreateChildControls();
        }

        // *********************************************************************
        //  Initializeskin
        //
        /// <summary>
        /// Initialize the control template and populate the control with values
        /// </summary>
        // ***********************************************************************/
        override protected void InitializeSkin(Control skin) {

            // Find the username control
            username = (TextBox) skin.FindControl("username");

            // Find the password control
            password = (TextBox) skin.FindControl("password");

            // Find the login button
            loginButton = (Button) skin.FindControl("loginButton");
            loginButton.Click += new System.EventHandler(LoginButton_Click);
            loginButton.Text = ResourceManager.GetString("LoginSmall_Button");

            // Find the autologin checkbox
            autoLogin = (CheckBox) skin.FindControl("autoLogin");
            autoLogin.Text = ResourceManager.GetString("LoginSmall_AutoLogin");

        }


        // *********************************************************************
        //  LoginButton_Click
        //
        /// <summary>
        /// Event handler to handle the login button click event
        /// </summary>
        // ***********************************************************************/
        public void LoginButton_Click(Object sender, EventArgs e) {
            User userToLogin = new User();
            string redirectUrl = "";

            if (!Page.IsValid)
                return;

            userToLogin.Username = username.Text;
            userToLogin.Password = password.Text;
            
            LoginUserStatus loginStatus = Users.ValidUser(userToLogin);

            if( loginStatus == LoginUserStatus.Success ) {

                // Are we allowing login?
                // TODO -- this could be better optimized
                if (!Globals.GetSiteSettings().AllowLogin) {
                    bool allowed = false;

                    int userid = Users.FindUserByUsername( userToLogin.Username ).UserID;
                    ArrayList roles = Spinit.Wpc.Forum.Components.Roles.GetRoles(userid);

                    foreach (Role role in roles) {
                        if (role.Name == "Site Administrators" || role.Name == "Global Administrators") {
                            allowed = true;
                            break;
                        }
                    }

                    // Check the user is in the administrator role
                    if (!allowed) {
                        throw new ForumException(ForumExceptionType.UserLoginDisabled);
                    }
                }

                FormsAuthentication.SetAuthCookie(userToLogin.Username, autoLogin.Checked);

				        // Check to ensure we aren't redirecting back to a Message prompt
				        //
				        if (redirectUrl.Length > 0)
					        redirectUrl = (Page.Request.QueryString["ReturnUrl"].IndexOf("MessageID") == 0 ? Page.Request.QueryString["ReturnUrl"] : Globals.ApplicationPath);
				        else
					        redirectUrl = Globals.ApplicationPath;

                if (redirectUrl != null)  {
                    Page.Response.Redirect(redirectUrl, true);
                } else {
                    Page.Response.Redirect(Globals.ApplicationPath, true);
                }
            } 
            else if(loginStatus == LoginUserStatus.InvalidCredentials) { // Invalid Credentials
                throw new ForumException(ForumExceptionType.UserInvalidCredentials, userToLogin.Username);
            } 
            else if(loginStatus == LoginUserStatus.AccountPending) { // Account not approved yet
                throw new ForumException(ForumExceptionType.UserAccountPending);
            }
            else if(loginStatus == LoginUserStatus.AccountBanned) { // Account banned
                throw new ForumException(ForumExceptionType.UserAccountBanned, userToLogin.Username);
            }
            else if(loginStatus == LoginUserStatus.AccountDisapproved) { // Account disapproved
              throw new ForumException(ForumExceptionType.UserAccountDisapproved, userToLogin.Username);
            }
            else if(loginStatus == LoginUserStatus.UnknownError) 
            { // Unknown error because of miss-syncronization of internal data
                throw new ForumException(ForumExceptionType.UserUnknownLoginError);
            }
        }
    }
}
