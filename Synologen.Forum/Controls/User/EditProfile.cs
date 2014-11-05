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

namespace Spinit.Wpc.Forum.Controls {

	public class EditProfile : SkinnedForumWebControl {

        #region Member variables and constructor
	    string skinFilename = "Skin-EditProfile.ascx";
	    ForumContext forumContext = ForumContext.Current;

	    // Member variable controls
	    User userToEdit;
	    Literal username;
	    TimezoneDropDownList timezone;
	    TextBox location;
	    TextBox occupation;
	    TextBox interests;
	    TextBox webAddress;
	    TextBox webLog;
	    TextBox signature;
	    TextBox privateEmail;
	    TextBox publicEmail;
	    TextBox msnIM;
	    TextBox aolIM;
	    TextBox yahooIM;
	    TextBox icq;
	    TextBox avatarUrl;
        TextBox usernameEdit;
	    RadioButtonList enableAvatar;
	    RadioButtonList  hideReadPosts;
	    RadioButtonList enableEmailTracking;
	    RadioButtonList displayInMemberList;
	    RadioButtonList enableHtmlEmail;
        RadioButtonList enableEmail;
        GenderRadioButtonList gender;
	    LanguageDropDownList language;
	    DateFormatDropDownList dateFormat;
        DatePicker birthday;
	    Button updateButton;
        Button avatarUpdateButton;
        DateFilter defaultThreadView;
        RangeValidator signatureMaxLengthValidator;
        HtmlInputFile uploadedAvatar;
        HtmlAnchor changePassword;
        HtmlAnchor changePasswordAnswer;
        ModerationLevelDropDownList moderationLevel;
        YesNoRadioButtonList isAnonymous;
        YesNoRadioButtonList isAvatarApproved;
        YesNoRadioButtonList forceLogin;
        AccountStatusDropDownList accountStatus;
        ThemeDropDownList theme;
	    YesNoRadioButtonList enablePostPreviewPopup;
        HtmlTable userBanSection;
        UserBanDropDownList userBanPeriod;
        Label bannedUntilDate;
        HtmlTable userApprovalSection;
        YesNoRadioButtonList isApproved;

	    // *********************************************************************
	    //  EditUserInfo
	    //
	    /// <summary>
	    /// Constructor
	    /// </summary>
	    ///
	    // ********************************************************************/
	    public EditProfile() 
        {

			// Set the default skin
			//
			if (SkinFilename == null)
				SkinFilename = skinFilename;

		}
        #endregion

        #region Render functions
		// *********************************************************************
		//  Initializeskin
		//
		/// <summary>
		/// This method populates the user control used to edit a user's information
		/// </summary>
		/// <param name="control">Instance of the user control to populate</param>
		///
		// ***********************************************************************/
        override protected void InitializeSkin(Control skin) {

            // Get the user we are editing
            //
			if ((Mode == ForumMode.Administrator) || (Mode == ForumMode.Moderator)) {
				userToEdit = Users.GetUser(forumContext.UserID, false, false);
                
				// Check if ban has expired
				// LN: 5/21/04 : Changhed because when ban has expired 
				// the admin must be warned to change user's status. No more
				// auto settings.
				//                    if( userToEdit.IsBanned && DateTime.Now > userToEdit.BannedUntil ) {
				//                        // Update account status
				//                        userToEdit.AccountStatus = UserAccountStatus.Approved;
				//                        userToEdit.BannedUntil = DateTime.Now;
				//                    }
                
				// Save actual account status
				this.PrevAccountStatus = userToEdit.AccountStatus;
			} else {
				userToEdit = Users.GetUser(false);
            }

            // Find the controls we need
            //
            username = (Literal) skin.FindControl("Username");
            usernameEdit = (TextBox) skin.FindControl("UsernameEdit");
            timezone = (TimezoneDropDownList) skin.FindControl("Timezone");
            location = (TextBox) skin.FindControl("Location");
            occupation = (TextBox) skin.FindControl("Occupation");
            interests = (TextBox) skin.FindControl("Interests");
            webAddress = (TextBox) skin.FindControl("WebAddress");
            webLog = (TextBox) skin.FindControl("WebLog");
            privateEmail = (TextBox) skin.FindControl("PrivateEmail");
            publicEmail = (TextBox) skin.FindControl("PublicEmail");
            msnIM = (TextBox) skin.FindControl("MsnIM");
            aolIM = (TextBox) skin.FindControl("AolIM");
            yahooIM = (TextBox) skin.FindControl("YahooIM");
            icq = (TextBox) skin.FindControl("ICQ");
            avatarUrl = (TextBox) skin.FindControl("avatarUrl");
            enableAvatar = (RadioButtonList) skin.FindControl("EnableAvatar");
            hideReadPosts = (RadioButtonList) skin.FindControl("HideReadPosts");
            signature = (TextBox) skin.FindControl("Signature");
            displayInMemberList = (RadioButtonList) skin.FindControl("DisplayInMemberList");
            updateButton = (Button) skin.FindControl("UpdateButton");
            avatarUpdateButton = (Button) skin.FindControl("");
            language = (LanguageDropDownList) skin.FindControl("Language");
            dateFormat = (DateFormatDropDownList) skin.FindControl("DateFormat");
            defaultThreadView = (DateFilter) skin.FindControl("DefaultThreadView");
            signatureMaxLengthValidator = (RangeValidator) skin.FindControl("SignatureMaxLengthValidator");
            uploadedAvatar = (HtmlInputFile) skin.FindControl("AvatarUpload");
            avatarUpdateButton = (Button) skin.FindControl("SubmitAvatar");
            gender = (GenderRadioButtonList) skin.FindControl("Gender");
            changePassword = (HtmlAnchor) skin.FindControl("ChangePassword");
            changePasswordAnswer = (HtmlAnchor) skin.FindControl("ChangePasswordAnswer");
            birthday = (DatePicker) skin.FindControl("BirthDate");
            theme = (ThemeDropDownList) skin.FindControl("Theme");
            enableEmailTracking = (RadioButtonList) skin.FindControl("EnableEmailTracking");
            enableHtmlEmail = (RadioButtonList) skin.FindControl("EnableHtmlEmail");
            enableEmail = (RadioButtonList) skin.FindControl("ReceiveEmails");

            signatureMaxLengthValidator.ErrorMessage = string.Format(ResourceManager.GetString("EditProfile_SigMaxLength"), Globals.GetSiteSettings().UserSignatureMaxLength.ToString());

            // Change password link
            //
            changePassword.InnerText = ResourceManager.GetString("EditProfile_PasswordChange");
            if (Mode == ForumMode.User) {
                changePassword.HRef = Globals.GetSiteUrls().UserChangePassword;
            } else {
                changePassword.HRef = Globals.GetSiteUrls().AdminUserPasswordChange(userToEdit.UserID);
            }

            // Change password answer link
            //
            changePasswordAnswer.InnerText = ResourceManager.GetString("EditProfile_PasswordAnswerChange");
            if (Mode == ForumMode.User) {
                changePasswordAnswer.HRef = Globals.GetSiteUrls().UserChangePasswordAnswer;
            } 
            else {
                changePasswordAnswer.HRef = Globals.GetSiteUrls().AdminUserPasswordAnswerChange(userToEdit.UserID);
            }

            // Do we allow the user to edit signatures?
            if ( (Globals.GetSiteSettings().AllowUserSignatures) || (Mode == ForumMode.Administrator) || (Mode == ForumMode.Moderator))
                skin.FindControl("EnableSignature").Visible = true;
			else
				skin.FindControl("EnableSignature").Visible = false;
			
			// Do we allow the user to specify gender?
            if ( (Globals.GetSiteSettings().AllowGender) || (Mode == ForumMode.Administrator) || (Mode == ForumMode.Moderator) ) {
                skin.FindControl("EnableGender").Visible = true;
            }

            // Set form to the current values of logged on user
            //
            username.Text = Globals.HtmlDecode(userToEdit.Username);

            // Do we allow the user to edit his/her username?
            if ((Globals.GetSiteSettings().EnableUsernameChange) || (Mode == ForumMode.Administrator) || (Mode == ForumMode.Moderator) ) {
                username.Visible = false;
                usernameEdit.Visible = true;
                usernameEdit.Text = username.Text;
            }

            if ( timezone.Items.FindByValue( userToEdit.Timezone.ToString() ) != null)
			        timezone.Items.FindByValue( userToEdit.Timezone.ToString() ).Selected = true;

            // Find controls for Avatar/Post section
            //
            if ( (userToEdit.HasAvatar) && (userToEdit.EnableAvatar) && (userToEdit.IsAvatarApproved) ) {
                UserAvatar avatar = (UserAvatar) skin.FindControl("Avatar");
                avatar.User = userToEdit;

                avatar.Visible = true;
            }

            // If the admin has turned on post preview popups then we can turn it on
            this.enablePostPreviewPopup = (YesNoRadioButtonList) skin.FindControl("EnablePostPreviewPopup");
            if( Globals.GetSiteSettings().EnablePostPreviewPopup  && 
                enablePostPreviewPopup != null ) {
          
                enablePostPreviewPopup.Visible = true;
                enablePostPreviewPopup.Items.FindByValue( userToEdit.EnablePostPreviewPopup.ToString() ).Selected = true;
            }
            else {
                enablePostPreviewPopup.Visible = true;
            }

		    location.Text = Globals.HtmlDecode(userToEdit.Location);
		    occupation.Text = Globals.HtmlDecode(userToEdit.Occupation);
		    interests.Text = Globals.HtmlDecode(userToEdit.Interests);
		    webAddress.Text = Globals.HtmlDecode(userToEdit.WebAddress);
		    webLog.Text = Globals.HtmlDecode(userToEdit.WebLog);
		    signature.Text = Globals.HtmlDecode(userToEdit.Signature);
		    privateEmail.Text = userToEdit.Email;
		    publicEmail.Text = Globals.HtmlDecode(userToEdit.PublicEmail);
		    msnIM.Text = Globals.HtmlDecode(userToEdit.MsnIM);
		    aolIM.Text = Globals.HtmlDecode(userToEdit.AolIM);
		    yahooIM.Text = Globals.HtmlDecode(userToEdit.YahooIM);
		    icq.Text = Globals.HtmlDecode(userToEdit.IcqIM);
		    avatarUrl.Text = Globals.HtmlDecode(userToEdit.AvatarUrl);

            if (birthday != null)
                birthday.SelectedDate = userToEdit.BirthDate;

            theme.SelectedValue = userToEdit.Theme;

            // Set the gender
            gender.Items.FindByValue( ((int) userToEdit.Gender).ToString() ).Selected = true;

            // Language Drop Down List
            if (language.Items.FindByValue( userToEdit.Language ) != null)
                language.Items.FindByValue( userToEdit.Language ).Selected = true;

            if (dateFormat.Items.FindByValue( userToEdit.DateFormat ) != null)
			    dateFormat.Items.FindByValue( userToEdit.DateFormat ).Selected = true;

		    enableAvatar.Items.FindByValue( userToEdit.EnableAvatar.ToString() ).Selected = true;
		    enableEmailTracking.Items.FindByValue ( userToEdit.EnableThreadTracking.ToString() ).Selected = true;
		    enableHtmlEmail.Items.FindByValue ( userToEdit.EnableHtmlEmail.ToString() ).Selected = true;
            enableEmail.Items.FindByValue ( userToEdit.EnableEmail.ToString() ).Selected = true;
		    displayInMemberList.Items.FindByValue( userToEdit.EnableDisplayInMemberList.ToString()).Selected = true;
            ((Literal) skin.FindControl("DateCreated")).Text = Users.GetUser().GetTimezone( userToEdit.DateCreated ).ToString( Users.GetUser().DateFormat );
            ((Literal) skin.FindControl("LastLogin")).Text = Users.GetUser().GetTimezone( userToEdit.LastLogin ).ToString( Users.GetUser().DateFormat );
            ((Literal) skin.FindControl("LastActivity")).Text = Users.GetUser().GetTimezone( userToEdit.LastActivity ).ToString( Users.GetUser().DateFormat );

            if ((Mode == ForumMode.Administrator) || (Mode == ForumMode.Moderator)) {
                skin.FindControl("AdministratorMode").Visible = true;
                ((Literal) skin.FindControl("UserID")).Text = userToEdit.UserID.ToString();
                ((Literal) skin.FindControl("PasswordFormat")).Text = userToEdit.PasswordFormat.ToString();

                moderationLevel = (ModerationLevelDropDownList) skin.FindControl("ModerationLevel");
                moderationLevel.SelectedValue = userToEdit.ModerationLevel;

                isAnonymous = (YesNoRadioButtonList) skin.FindControl("IsAnonymous");
                isAnonymous.SelectedValue = userToEdit.IsAnonymous;

                forceLogin = (YesNoRadioButtonList) skin.FindControl("ForceLogin");
                forceLogin.SelectedValue = userToEdit.ForceLogin;

                isAvatarApproved = (YesNoRadioButtonList) skin.FindControl("IsAvatarApproved");
                isAvatarApproved.SelectedValue = userToEdit.IsAvatarApproved;
                
                // Account Status
                accountStatus = (AccountStatusDropDownList) skin.FindControl("AccountStatus");
                accountStatus.SelectedValue = userToEdit.AccountStatus;
                accountStatus.AutoPostBack = true;
                accountStatus.SelectedIndexChanged += new EventHandler(AccountStatus_Changed);

				// User Banning
				userBanSection = (HtmlTable) skin.FindControl("UserBanSection");
				userBanSection.Visible = (userToEdit.AccountStatus == UserAccountStatus.Banned) ? true : false;                                        
				bannedUntilDate = (Label) skin.FindControl("BannedUntilDate");
				bannedUntilDate.Text = this.GetBannedDateValue(ref userToEdit);
				// LN: 5/21/04 : Added to signal admin that a user's ban has expired.
				if (userToEdit.IsBanned && DateTime.Now > userToEdit.BannedUntil) {
					bannedUntilDate.Text += ResourceManager.GetString("EditProfile_BanExpired");
					bannedUntilDate.ForeColor = System.Drawing.Color.Red;
				}
				userBanPeriod = (UserBanDropDownList) skin.FindControl("UserBanPeriod");
				userBanPeriod.SelectedValue = this.GetRemainingBannedDays(userToEdit.BannedUntil);
                
                // User Approval
                userApprovalSection = (HtmlTable) skin.FindControl("UserApprovalSection");
                userApprovalSection.Visible = (userToEdit.AccountStatus == UserAccountStatus.ApprovalPending) ? true : false;                                        
                isApproved = (YesNoRadioButtonList) skin.FindControl("IsApproved");
                isApproved.SelectedValue = (userToEdit.AccountStatus == UserAccountStatus.Approved) ? true : false;
            }

		    // Give the update button some text
		    //
		    updateButton.Text = Spinit.Wpc.Forum.Components.ResourceManager.GetString("EditProfile_Update");

            // Set the avatar update button
            avatarUpdateButton.Text = Spinit.Wpc.Forum.Components.ResourceManager.GetString("Update");

		    // Wire-up any events
		    //
		    updateButton.Click += new EventHandler(Update_Click);
            avatarUpdateButton.Click += new EventHandler(UpdateAvatar_Click);
		}
        #endregion

        #region Events
        public void AccountStatus_Changed(Object sender, EventArgs e) {
            // User Approval
			if (userApprovalSection != null) {

				// show/hide the "Approve User" section
				//
				switch (userToEdit.AccountStatus) {
					case (UserAccountStatus.ApprovalPending):
						userApprovalSection.Visible = true;
						break;

					default:
						userApprovalSection.Visible = false;
						break;
				}


				// EAD 6/27/2004: this.PrevAccountStatus is Depreciated
				//
				//
					// Enable change back to pending but don't show the approve panel 
					// unless prevous account status was pending. 
					// Also preserve pending status untill it will be changed to 
					// Approved or Disapproved (other combinations are not allowed).
				//	if(this.PrevAccountStatus != UserAccountStatus.ApprovalPending)
				//		userApprovalSection.Visible = false;                                        
				//	else {
				//		accountStatus.SelectedValue = UserAccountStatus.ApprovalPending;
				//		userApprovalSection.Visible = true; //(accountStatus.SelectedValue == UserAccountStatus.ApprovalPending) ? true : false;                                        
				//	}
            }

            // User Banning (keep this code after Approval code)
            if (userBanSection != null)
                userBanSection.Visible = (accountStatus.SelectedValue == UserAccountStatus.Banned) ? true : false;
        }

        public void UpdateAvatar_Click (Object sender, EventArgs e) {
            try {
                Resources.UpdateAvatar(forumContext.User.UserID, uploadedAvatar.PostedFile.InputStream);

                avatarUrl.Text = "users/avatar.aspx?userid=" + forumContext.User.UserID.ToString();

            } catch {
                throw new ForumException(ForumExceptionType.UnknownError);
            }

            Update_Click(sender, e);
        }

		public void Update_Click (Object sender, EventArgs e) {

            // Check the max length on the signature
            //
            if (signature.ToString().Length > Globals.GetSiteSettings().UserSignatureMaxLength) {
                signatureMaxLengthValidator.IsValid = false;
                return;
            }

			    // Get the updated values from the form
			    //
            if (Globals.GetSiteSettings().EnableUsernameChange)
                userToEdit.Username = usernameEdit.Text;

            // Update gender?
            if (Globals.GetSiteSettings().AllowGender)
                userToEdit.Gender = (Gender) int.Parse(gender.SelectedItem.Value);

			    userToEdit.Timezone = double.Parse(timezone.SelectedItem.Value);
			    userToEdit.Location = location.Text;
			    userToEdit.Occupation = occupation.Text;
			    userToEdit.Interests = interests.Text;
			    userToEdit.WebAddress = webAddress.Text;
			    userToEdit.WebLog = webLog.Text;
			    userToEdit.Signature = Transforms.CensorPost( signature.Text );
			    userToEdit.Email = privateEmail.Text;
			    userToEdit.PublicEmail = publicEmail.Text;
			    userToEdit.MsnIM = msnIM.Text;
			    userToEdit.AolIM = aolIM.Text;
			    userToEdit.YahooIM = yahooIM.Text;
			    userToEdit.IcqIM = icq.Text;
			    userToEdit.AvatarUrl = avatarUrl.Text;
			    userToEdit.DateFormat = dateFormat.SelectedItem.Value;
			    userToEdit.EnableAvatar = bool.Parse(enableAvatar.SelectedItem.Value);
			    userToEdit.EnableThreadTracking = bool.Parse(enableEmailTracking.SelectedItem.Value);
			    userToEdit.EnableDisplayInMemberList = bool.Parse(displayInMemberList.SelectedItem.Value);
			    userToEdit.EnableHtmlEmail = bool.Parse(enableHtmlEmail.SelectedItem.Value);
                userToEdit.EnableEmail = bool.Parse(enableEmail.SelectedItem.Value);
                userToEdit.Language = language.SelectedItem.Value;

            if (enablePostPreviewPopup != null &&
                Globals.GetSiteSettings().EnablePostPreviewPopup)  {  				
                userToEdit.EnablePostPreviewPopup = bool.Parse( enablePostPreviewPopup.SelectedItem.Value );
            }	
       
            if (birthday != null)
                userToEdit.BirthDate = birthday.SelectedDate;

            userToEdit.Theme = theme.SelectedValue;

            if ((Mode == ForumMode.Administrator) || (Mode == ForumMode.Moderator)) {
                userToEdit.ModerationLevel = moderationLevel.SelectedValue;
				userToEdit.AccountStatus = accountStatus.SelectedValue;
				userToEdit.IsAnonymous = isAnonymous.SelectedValue;
                userToEdit.IsAvatarApproved = isAvatarApproved.SelectedValue;
                userToEdit.ForceLogin = forceLogin.SelectedValue;
                
				// User Banning
				if (accountStatus.SelectedValue == UserAccountStatus.Banned) {
					userToEdit.BannedUntil = DateTime.Now.AddDays((int) userBanPeriod.SelectedValue);
				}
				else {
					userToEdit.BannedUntil = DateTime.Now;
				}
                
                // User Approval
                if (userApprovalSection.Visible == true && 
                    accountStatus.SelectedValue == UserAccountStatus.ApprovalPending) {

                    if (isApproved.SelectedValue == true) {
                        // Change status to Approved
                        userToEdit.AccountStatus = UserAccountStatus.Approved;

                        // Send email ( moved after update! )
                        //Emails.UserAccountApproved(userToEdit);
                    }

                    if (isApproved.SelectedValue == false) {
                        // Change status to Disapproved
                        userToEdit.AccountStatus = UserAccountStatus.Disapproved;

                        // Send email ( moved after update! )
                        //Emails.UserAccountRejected(userToEdit, ForumContext.Current.User);
                    }
                }

            }


            /* TODO
            // Does the user have a user uploaded avatar?
            //
            if (uploadedAvatar.PostedFile.ContentLength > 0) {
                Stream fs = uploadedAvatar.PostedFile.InputStream;

                // Is the avatar an allowed size?
                //
                if (PostedFile.ContentLength > Globals.GetSiteSettings().AvatarMaxSize) {
                }

                // Is the avatar within the allowed dimensions?
                //
                fs = ForumImage.ResizeImage(fs, Globals.GetSiteSettings().AvatarHeight, Globals.GetSiteSettings().AvatarWidth);

                // Add the avatar to the database
                //
                ForumImage.AddUserAvatar(fs, userToEdit.Username);

                // Does the new avatar require approval
                //
                if (Globals.GetSiteSettings().EnableAvatarRequireApproval)
                    userToEdit.IsAvatarApproved = false;
                else
                    userToEdit.IsAvatarApproved = true;

                userToEdit.AvatarUrl = Globals.GetSiteUrls().ForumImage(userToEdit.Username);
            } */



			// Update the user
			//
			Users.UpdateUser(userToEdit);

            // Send notification emails after the update took place
            //
            if ((Mode == ForumMode.Administrator) || (Mode == ForumMode.Moderator)) {


                // User Approval
                if( this.PrevAccountStatus == UserAccountStatus.ApprovalPending &&
                    userToEdit.AccountStatus == UserAccountStatus.Approved )  {

                    Emails.UserAccountApproved(userToEdit);
                }

                if( this.PrevAccountStatus == UserAccountStatus.ApprovalPending &&
                    userToEdit.AccountStatus == UserAccountStatus.Disapproved ) {

                    Emails.UserAccountRejected(userToEdit, ForumContext.Current.User);
                }
            }   
         
            // Notify the End of Update
            //
            throw new ForumException(ForumExceptionType.UserProfileUpdated);
		}
        #endregion

        #region Helper Properties & Methods
        public UserAccountStatus PrevAccountStatus {

            get {
                if(ViewState != null && ViewState["PrevAccountStatus"] == null) 
                    return UserAccountStatus.Approved;
                else 
                    return (UserAccountStatus) ViewState["PrevAccountStatus"];
            }
            set {
                if(ViewState != null)
                    ViewState["PrevAccountStatus"] = value;
            }
        }

        private UserBanPeriod GetRemainingBannedDays(DateTime bannedUntil) {

            UserBanPeriod daysNo = UserBanPeriod.Permanent;
            if(DateTime.Now > bannedUntil)
                return daysNo;

            int diff = ((TimeSpan) (bannedUntil - DateTime.Now)).Days;
            if(diff <= (int) UserBanPeriod.OneDay) {
                daysNo = UserBanPeriod.OneDay;
            }
            else if(diff > (int) UserBanPeriod.OneDay && diff <= (int) UserBanPeriod.ThreeDays) {
                daysNo = UserBanPeriod.ThreeDays;
            }
            else if(diff > (int) UserBanPeriod.ThreeDays && diff <= (int) UserBanPeriod.FiveDays) {
                daysNo = UserBanPeriod.FiveDays;
            }
            else if(diff > (int) UserBanPeriod.FiveDays && diff <= (int) UserBanPeriod.OneWeek) {
                daysNo = UserBanPeriod.OneWeek;
            }
            else if(diff > (int) UserBanPeriod.OneWeek && diff <= (int) UserBanPeriod.TwoWeeks) {
                daysNo = UserBanPeriod.TwoWeeks;
            }
            else if(diff > (int) UserBanPeriod.TwoWeeks && diff <= (int) UserBanPeriod.OneMonth) {
                daysNo = UserBanPeriod.OneMonth;
            }
            else if(diff > (int) UserBanPeriod.OneMonth && diff <= (int) UserBanPeriod.Permanent) {
                daysNo = UserBanPeriod.Permanent;
            }

            return daysNo;
        }

        private string GetBannedDateValue(ref User userToEdit) {

            if(userToEdit != null && userToEdit.IsBanned)
                return Formatter.FormatDate(userToEdit.BannedUntil, true);
            else
                return ResourceManager.GetString("NotSet");
        }
        #endregion
	}
}
