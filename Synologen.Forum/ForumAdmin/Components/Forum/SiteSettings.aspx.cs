using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

//using AspNetForums;
using Spinit.Wpc.Forum.Components;
using System.IO;
using Spinit.Wpc.Forum.Enumerations;
using Spinit.Wpc.Forum.Controls;


namespace Spinit.Wpc.Forum.Presentation.Components.Forum
{
	/// <summary>
	/// Summary description for SiteSettings.
	/// </summary>
	public class SiteSettings : System.Web.UI.Page
	{
		#region Member Elements
		protected Spinit.Wpc.Forum.Components.SiteSettings settings;

		protected string generalSettings              = "/Admin/View-SiteSettings.ascx";
		protected string memberSettings               = "/Admin/View-MemberSettings.ascx";

		protected SiteSettingsMode viewMode = SiteSettingsMode.GeneralSettings;
		
		protected System.Web.UI.WebControls.Button SelectDomain;
		protected System.Web.UI.WebControls.RadioButtonList Disabled;
		protected System.Web.UI.WebControls.TextBox SiteName;
		protected System.Web.UI.WebControls.TextBox SiteDescription;
		protected Spinit.Wpc.Forum.Controls.TimezoneDropDownList Timezone;
		protected System.Web.UI.WebControls.TextBox ThreadsPerPage;
		protected System.Web.UI.WebControls.TextBox PostsPerPage;
		protected System.Web.UI.WebControls.TextBox AdminEmailAddress;
		protected System.Web.UI.WebControls.RegularExpressionValidator RegularExpressionValidator1;
		protected System.Web.UI.WebControls.TextBox CompanyName;
		protected System.Web.UI.WebControls.TextBox CompanyEmailAddress;
		protected System.Web.UI.WebControls.RegularExpressionValidator RegularExpressionValidator2;
		protected System.Web.UI.WebControls.TextBox CompanyFaxNumber;
		protected System.Web.UI.WebControls.TextBox CompanyAddress;
		protected System.Web.UI.WebControls.RadioButtonList DisplayForumDescription;
		protected System.Web.UI.WebControls.RadioButtonList DisplayBirthdays;
		protected System.Web.UI.WebControls.RadioButtonList DisplayCurrentTime;
		protected System.Web.UI.WebControls.RadioButtonList DisplayWhoIsOnline;
		protected System.Web.UI.WebControls.RadioButtonList EnableForumRSS;
		protected System.Web.UI.WebControls.RadioButtonList EnableAttachments;
		protected System.Web.UI.WebControls.TextBox AllowedAttachmentTypes;
		protected System.Web.UI.WebControls.TextBox MaxAttachmentSize;
		protected System.Web.UI.WebControls.RadioButtonList EnablePostReporting;
		protected System.Web.UI.WebControls.RadioButtonList EnableAnonymousReporting;
		protected System.Web.UI.WebControls.TextBox ReportingForum;
		protected System.Web.UI.WebControls.TextBox RSSDefaultThreadsPerFeed;
		protected System.Web.UI.WebControls.TextBox RSSMaxThreadsPerFeed;
		protected System.Web.UI.WebControls.TextBox RssCacheWindowInMinutes;
		protected System.Web.UI.WebControls.RadioButtonList PublicMemberList;
		protected System.Web.UI.WebControls.TextBox MemberListPageSize;
		protected System.Web.UI.WebControls.TextBox MemberListTopPostersToDisplay;
		protected System.Web.UI.WebControls.RadioButtonList AllowDuplicatePosts;
		protected System.Web.UI.WebControls.TextBox DuplicatePostIntervalInMinutes;
		protected System.Web.UI.WebControls.RadioButtonList EnableEmoticons;
		protected System.Web.UI.WebControls.TextBox PopularPostThresholdPosts;
		protected System.Web.UI.WebControls.TextBox PopularPostThresholdViews;
		protected System.Web.UI.WebControls.TextBox PopularPostThresholdDays;
		protected System.Web.UI.WebControls.RadioButtonList AutoPostDelete;
		protected System.Web.UI.WebControls.RadioButtonList AutoUserDelete;
		protected System.Web.UI.WebControls.RadioButtonList EnableFloodInterval;
		protected System.Web.UI.WebControls.TextBox PostInterval;
		protected System.Web.UI.WebControls.RadioButtonList EnableTrackPostsByIP;
		protected System.Web.UI.WebControls.RadioButtonList DisplayPostIP;
		protected System.Web.UI.WebControls.RadioButtonList DisplayPostIPAdminsModeratorsOnly;
		protected System.Web.UI.WebControls.TextBox TimeFormat;
		protected System.Web.UI.WebControls.TextBox TimeFormatUserJoined;
		protected System.Web.UI.WebControls.TextBox PostEditBodyAgeInMinutes;
		protected System.Web.UI.WebControls.RadioButtonList EnableSearchFriendlyURLs;
		protected System.Web.UI.WebControls.RadioButtonList EnableRolesCookie;
		protected System.Web.UI.WebControls.RadioButtonList EnableAnonymousTracking;
		protected System.Web.UI.WebControls.TextBox RolesCookieName;
		protected System.Web.UI.WebControls.TextBox RolesCookieExpiration;
		protected System.Web.UI.WebControls.TextBox AnonymousCookieName;
		protected System.Web.UI.WebControls.TextBox AnonymousCookieExpiration;
		protected System.Web.UI.WebControls.TextBox CookieDomain;
		protected System.Web.UI.WebControls.RadioButtonList AnonymousPosting;
		protected System.Web.UI.WebControls.TextBox AnonymousUserOnlineTimeWindow;
		protected System.Web.UI.WebControls.RadioButtonList EnableEmail;
		protected System.Web.UI.WebControls.TextBox SmtpServer;
		protected System.Web.UI.WebControls.Button Save;
		protected System.Web.UI.WebControls.Button Reset;

		protected DomainDropDownList Domain;	
		protected DateFormatDropDownList DateFormat;
		protected YesNoRadioButtonList MemberListAdvancedSearch;
		protected YesNoRadioButtonList DisplayStatistics;
		protected System.Web.UI.WebControls.TextBox SmtpServerUserName;
		protected System.Web.UI.WebControls.TextBox SmtpServerPassword;
		protected YesNoRadioButtonList DisplayEditNotes;
		protected System.Web.UI.HtmlControls.HtmlTable newVersionAvailable;
		protected System.Web.UI.HtmlControls.HtmlTable versionModified;
		protected System.Web.UI.HtmlControls.HtmlTable unableToRetrieveVersionData;
		protected YesNoRadioButtonList SmtpServerRequiredLogin;
		protected YesNoRadioButtonList EnablePostPreviewPopup;
		protected YesNoRadioButtonList AllowAutoRegistration;
		protected YesNoRadioButtonList StripDomainName;
		#endregion

		#region Page_Load
		private void Page_Load(object sender, System.EventArgs e) {

			this.SelectDomain.Click += new System.EventHandler(SelectDomain_Click);
			this.Save.Click         += new EventHandler(Save_Click);
			this.Reset.Click        += new EventHandler(Reset_Click);

			this.SelectDomain.Text	= ResourceManager.GetString("Select");
			this.Save.Text          = ResourceManager.GetString("Save");
			this.Reset.Text         = ResourceManager.GetString("Reset");

			Save.Attributes["onclick"] = "return confirm('" + string.Format(Spinit.Wpc.Forum.Components.ResourceManager.GetString("Cache_Warning"), Globals.GetSiteSettings().SiteSettingsCacheWindowInMinutes).Replace("'", @"\'") + "');";								
			
			// EAD TODO: Localize this after 2.0.0
			//
			Reset.Attributes["onclick"] = "return confirm('This will reset and save the forum back to the default settings.  Continue?');";								

			if( !Page.IsPostBack )
				DataBind();
		}
		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		#region Events

			#region SelectDomain
		private void SelectDomain_Click(object sender, System.EventArgs e) {

			// EAD TODO: Change this function for the admin homepage
			// for verison 2.1.0 for SiteID.
			//
			DataBind();		
		}
		#endregion
	
			#region Reset
		public void Reset_Click (object sender, EventArgs e) {

			settings = new Spinit.Wpc.Forum.Components.SiteSettings();
			settings.SiteDomain = Domain.SelectedDomain;

			settings.Save();
			
			DataBind();
		}
		#endregion

			#region Save
		public void Save_Click (object sender, EventArgs e) {

			settings = Spinit.Wpc.Forum.Components.SiteSettings.GetSiteSettings(Domain.SelectedDomain);
			settings.SiteDomain = Domain.SelectedDomain;

			settings.SiteName                   = SiteName.Text;
			settings.SiteDescription            = SiteDescription.Text;
			settings.ThreadsPerPage             = Convert.ToInt32( ThreadsPerPage.Text );
			settings.PostsPerPage               = Convert.ToInt32( PostsPerPage.Text );
			settings.EnablePostPreviewPopup		= Boolean.Parse( EnablePostPreviewPopup.SelectedItem.Value );
//			settings.AllowAutoRegistration		= Boolean.Parse( AllowAutoRegistration.SelectedItem.Value );
			settings.StripDomainName			= Boolean.Parse( StripDomainName.SelectedItem.Value );

			#region Forums Disabled
			settings.ForumsDisabled                 = Boolean.Parse( Disabled.SelectedItem.Value );
			#endregion

			#region Contact Us
			settings.AdminEmailAddress              = AdminEmailAddress.Text;
			settings.CompanyName                    = CompanyName.Text;
			settings.CompanyAddress                 = CompanyAddress.Text;
			settings.CompanyContactUs               = CompanyEmailAddress.Text;
			settings.CompanyFaxNumber               = CompanyFaxNumber.Text;
			#endregion

			#region Home Page Menus & Sections
			settings.EnablePublicMemberList     = Boolean.Parse( PublicMemberList.SelectedItem.Value );
			settings.EnableWhoIsOnline          = Boolean.Parse( DisplayWhoIsOnline.SelectedItem.Value);
			settings.EnableBirthdays            = Boolean.Parse( DisplayBirthdays.SelectedItem.Value);
			settings.EnableCurrentTime          = Boolean.Parse( DisplayCurrentTime.SelectedItem.Value);
			settings.EnableForumDescription     = Boolean.Parse( DisplayForumDescription.SelectedItem.Value);
			settings.EnableSiteStatistics       = DisplayStatistics.SelectedValue;
			#endregion

			#region RSS
			settings.EnableRSS                  = Boolean.Parse( EnableForumRSS.SelectedItem.Value );
			settings.RssCacheWindowInMinutes    = int.Parse(RssCacheWindowInMinutes.Text);
			settings.RssDefaultThreadsPerFeed   = int.Parse(RSSDefaultThreadsPerFeed.Text);
			settings.RssMaxThreadsPerFeed       = int.Parse(RSSMaxThreadsPerFeed.Text);
			#endregion

			#region User Posting
			settings.EnableDuplicatePosts           = Boolean.Parse( AllowDuplicatePosts.SelectedItem.Value );
			settings.DuplicatePostIntervalInMinutes = int.Parse(DuplicatePostIntervalInMinutes.Text);
			settings.PostInterval                   = Convert.ToInt32( PostInterval.Text );
			#endregion

			#region IP Address Tracking
			settings.EnableTrackPostsByIP               = Boolean.Parse( EnableTrackPostsByIP.SelectedItem.Value );
			settings.DisplayPostIP                      = Boolean.Parse( DisplayPostIP.SelectedItem.Value);
			settings.DisplayPostIPAdminsModeratorsOnly  = Boolean.Parse( DisplayPostIPAdminsModeratorsOnly.SelectedItem.Value);
			#endregion

			#region Date & Times
			settings.TimezoneOffset                 = Convert.ToInt32( Timezone.SelectedItem.Value );
			settings.TimeFormat                     = TimeFormat.Text;
			settings.DateFormat                     = DateFormat.SelectedItem.Value;
			#endregion

			#region Posts
			settings.PopularPostLevelPosts          = Convert.ToInt32( PopularPostThresholdPosts.Text );
			settings.PopularPostLevelViews          = Convert.ToInt32( PopularPostThresholdViews.Text );
			settings.PopularPostLevelDays           = Convert.ToInt32( PopularPostThresholdDays.Text );
			#endregion

			#region Post Attachments
			settings.EnableAttachments				= Boolean.Parse( EnableAttachments.SelectedItem.Value );
			settings.AllowedAttachmentTypes			= AllowedAttachmentTypes.Text;
			settings.MaxAttachmentSize				= Convert.ToInt32( MaxAttachmentSize.Text );
			#endregion

			#region Post Flooding
			settings.EnableFloodIntervalChecking	= Boolean.Parse( this.EnableFloodInterval.SelectedItem.Value );
			settings.MinimumTimeBetweenPosts		= int.Parse( this.PostInterval.Text );
			#endregion

	// TDD TODO the following is commented until support is added for post 2.0 release
//					#region Post Reporting
//					settings.EnablePostReporting			= Boolean.Parse( EnablePostReporting.SelectedItem.Value );
//					settings.EnableAnonymousReporting		= Boolean.Parse( EnableAnonymousReporting.SelectedItem.Value );
//					settings.ReportingForum					= int.Parse( ReportingForum.Text );
//					#endregion
	// TDD TODO end 

			#region Post Editing
			settings.DisplayEditNotesInPost         = DisplayEditNotes.SelectedValue;
			settings.PostEditBodyAgeInMinutes       = int.Parse(PostEditBodyAgeInMinutes.Text);
			//settings.PostEditTitleAgeInMinutes      = int.Parse(editPostTitleAge.Text);
			#endregion

			#region Forum URLs
			settings.EnableSearchFriendlyURLs       = Boolean.Parse( EnableSearchFriendlyURLs.SelectedItem.Value );
			#endregion

			#region Member list and Top Posters
			settings.MaxTopPostersToDisplay     = int.Parse(MemberListTopPostersToDisplay.Text);
			settings.MembersPerPage             = int.Parse(MemberListPageSize.Text);
			settings.EnablePublicAdvancedMemberSearch   = MemberListAdvancedSearch.SelectedValue;
			#endregion

			#region Emoticons
			settings.EnableEmoticons                = Boolean.Parse( EnableEmoticons.SelectedItem.Value);
			settings.EnableAutoPostDelete           = Boolean.Parse( AutoPostDelete.SelectedItem.Value );
			settings.EnableAutoUserDelete           = Boolean.Parse( AutoUserDelete.SelectedItem.Value );
			#endregion
    
			#region Roles & Cookies
			settings.EnableRoleCookie               = Boolean.Parse( EnableRolesCookie.SelectedItem.Value);
			settings.EnableAnonymousUserTracking    = Boolean.Parse( EnableAnonymousTracking.SelectedItem.Value);
			settings.RoleCookieName                 = RolesCookieName.Text;
			settings.RoleCookieExpiration           = int.Parse( RolesCookieExpiration.Text);
			settings.AnonymousCookieName            = AnonymousCookieName.Text;
			settings.AnonymousCookieExpiration      = int.Parse( AnonymousCookieExpiration.Text );
			settings.CookieDomain                   = CookieDomain.Text; 
			#endregion

			#region Anonymous Users
			settings.EnableAnonymousUserPosting     = Boolean.Parse( AnonymousPosting.SelectedItem.Value );
			settings.AnonymousUserOnlineTimeWindow  = int.Parse( AnonymousUserOnlineTimeWindow.Text );
			#endregion
    
			#region SMTP Server
			settings.SmtpServer                     = SmtpServer.Text;
			settings.EnableEmail                    = Boolean.Parse( EnableEmail.SelectedItem.Value );
			settings.SmtpServerPassword = SmtpServerPassword.Text;
			settings.SmtpServerUserName = SmtpServerUserName.Text;
			settings.SmtpServerRequiredLogin = Boolean.Parse(SmtpServerRequiredLogin.SelectedItem.Value);
			#endregion
		
			// Save the settings
			//
			settings.Save();
		
			// Done with the update, repopulate the form
			//
			DataBind();
		}
		#endregion

		#endregion

		#region DataBind
		public override void DataBind() {

			settings = Spinit.Wpc.Forum.Components.SiteSettings.GetSiteSettings(Domain.SelectedDomain);

			#region Forums Disabled
			Disabled.Items.FindByValue(settings.ForumsDisabled.ToString()).Selected = true;
			#endregion

			#region Contact Us
			AdminEmailAddress.Text          = settings.AdminEmailAddress;
			CompanyAddress.Text             = settings.CompanyAddress;
			CompanyFaxNumber.Text           = settings.CompanyFaxNumber;
			CompanyEmailAddress.Text        = settings.CompanyContactUs;
			CompanyName.Text                = settings.CompanyName;
			#endregion

			#region Home Page Menus & Sections
			PublicMemberList.Items.FindByValue(settings.EnablePublicMemberList.ToString()).Selected = true;
			DisplayWhoIsOnline.Items.FindByValue(settings.EnableWhoIsOnline.ToString()).Selected = true;
			DisplayBirthdays.Items.FindByValue(settings.EnableBirthdays.ToString()).Selected = true;
			DisplayCurrentTime.Items.FindByValue(settings.EnableCurrentTime.ToString()).Selected = true;
			DisplayForumDescription.Items.FindByValue(settings.EnableForumDescription.ToString()).Selected = true;
			DisplayStatistics.SelectedValue = settings.EnableSiteStatistics;
			#endregion

			#region RSS
			EnableForumRSS.Items.FindByValue(settings.EnableRSS.ToString()).Selected = true;
			RssCacheWindowInMinutes.Text = settings.RssCacheWindowInMinutes.ToString();
			RSSDefaultThreadsPerFeed.Text = settings.RssDefaultThreadsPerFeed.ToString();
			RSSMaxThreadsPerFeed.Text = settings.RssMaxThreadsPerFeed.ToString();
			#endregion

			#region User Posting
			AllowDuplicatePosts.Items.FindByValue(settings.EnableDuplicatePosts.ToString()).Selected = true;
			DuplicatePostIntervalInMinutes.Text = settings.DuplicatePostIntervalInMinutes.ToString();
			#endregion

			#region Date & Times
			Timezone.SelectedValue = settings.TimezoneOffset.ToString();
			TimeFormat.Text = settings.TimeFormat;
			
			if (DateFormat.Items.FindByValue( settings.DateFormat ) != null) {
			
				ListItem item = DateFormat.Items.FindByValue( settings.DateFormat );
				DateFormat.SelectedIndex = DateFormat.Items.IndexOf(item);

				//DateFormat.Items.FindByValue( settings.DateFormat ).Selected = true;
			}
			#endregion

			#region Posts
			PopularPostThresholdPosts.Text = settings.PopularPostLevelPosts.ToString();
			PopularPostThresholdViews.Text = settings.PopularPostLevelViews.ToString();
			PopularPostThresholdDays.Text = settings.PopularPostLevelDays.ToString();
			#endregion

			#region Post Editing
			DisplayEditNotes.SelectedValue    = settings.DisplayEditNotesInPost;
			PostEditBodyAgeInMinutes.Text     = settings.PostEditBodyAgeInMinutes.ToString();
			//PostEditTitleAgeInMinutes.Text          = settings.PostEditTitleAgeInMinutes.ToString();
			#endregion

			#region  Post Attachments
			EnableAttachments.Items.FindByValue( settings.EnableAttachments.ToString() ).Selected = true;
			AllowedAttachmentTypes.Text = settings.AllowedAttachmentTypes.ToString();
			MaxAttachmentSize.Text = settings.MaxAttachmentSize.ToString();
			#endregion

			#region Flood Checking
			EnableFloodInterval.Items.FindByValue( settings.EnableFloodIntervalChecking.ToString() ).Selected = true;
			PostInterval.Text = settings.MinimumTimeBetweenPosts.ToString();
			#endregion

			#region  Reporting Posts
			EnablePostReporting.Items.FindByValue(settings.EnablePostReporting.ToString()).Selected = true;
			EnableAnonymousReporting.Items.FindByValue(settings.EnableAnonymousReporting.ToString()).Selected = true;
			ReportingForum.Text = settings.ReportingForum.ToString();
			#endregion

			#region IP Address Tracking
			EnableTrackPostsByIP.Items.FindByValue(settings.EnableTrackPostsByIP.ToString()).Selected = true;
			DisplayPostIP.Items.FindByValue(settings.DisplayPostIP.ToString()).Selected = true;
			DisplayPostIPAdminsModeratorsOnly.Items.FindByValue(settings.DisplayPostIPAdminsModeratorsOnly.ToString()).Selected = true;
			#endregion

			#region Forum URLs
			EnableSearchFriendlyURLs.Items.FindByValue(settings.EnableSearchFriendlyURLs.ToString()).Selected = true;
			#endregion

			#region Member list and Top Posters
			MemberListTopPostersToDisplay.Text     = settings.MaxTopPostersToDisplay.ToString();
			MemberListPageSize.Text                 = settings.MembersPerPage.ToString();
			MemberListAdvancedSearch.SelectedValue = settings.EnablePublicAdvancedMemberSearch;
			#endregion

			#region Site Name/Desc
			SiteName.Text = settings.SiteName;
			SiteDescription.Text = settings.SiteDescription;
			#endregion

			#region threading and posting
			ThreadsPerPage.Text = settings.ThreadsPerPage.ToString();
			PostsPerPage.Text = settings.PostsPerPage.ToString();
			#endregion

			#region Post Settings & Emoticons
			EnableEmoticons.Items.FindByValue(settings.EnableEmoticons.ToString()).Selected = true;
			PostInterval.Text = settings.PostInterval.ToString();
			AutoPostDelete.Items.FindByValue(settings.EnableAutoPostDelete.ToString()).Selected = true;
			AutoUserDelete.Items.FindByValue(settings.EnableAutoUserDelete.ToString()).Selected = true;

			EnablePostPreviewPopup.Items.FindByValue( settings.EnablePostPreviewPopup.ToString() ).Selected = true;
//			AllowAutoRegistration.Items.FindByValue( settings.AllowAutoRegistration.ToString() ).Selected = true;
			StripDomainName.Items.FindByValue( settings.StripDomainName.ToString() ).Selected = true;
			#endregion
    
			#region Roles & Cookies
			RolesCookieName.Text = settings.RoleCookieName;
			RolesCookieExpiration.Text = settings.RoleCookieExpiration.ToString();
			EnableRolesCookie.Items.FindByValue( settings.EnableRoleCookie.ToString() ).Selected = true;
			AnonymousCookieName.Text = settings.AnonymousCookieName;
			AnonymousCookieExpiration.Text = settings.AnonymousCookieExpiration.ToString();
			CookieDomain.Text = settings.CookieDomain.ToString(); 
			#endregion

			#region Anonymouse Users
			EnableAnonymousTracking.Items.FindByValue(settings.EnableAnonymousUserTracking.ToString()).Selected = true;
			AnonymousPosting.Items.FindByValue(settings.EnableAnonymousUserPosting.ToString()).Selected = true;
			AnonymousUserOnlineTimeWindow.Text = settings.AnonymousUserOnlineTimeWindow.ToString();
			#endregion
    
			#region SMTP Server
			SmtpServer.Text = settings.SmtpServer;
			EnableEmail.Items.FindByValue( settings.EnableEmail.ToString() ).Selected = true;

			SmtpServerPassword.Text = settings.SmtpServerPassword;
			SmtpServerUserName.Text = settings.SmtpServerUserName;
			SmtpServerRequiredLogin.Items.FindByValue(settings.SmtpServerRequiredLogin.ToString() ).Selected = true;
			#endregion

		}
		#endregion

		#region ViewMode
		public SiteSettingsMode ViewMode {
			get { return viewMode; }
			set { viewMode = value; }
		}
		#endregion

	}
}

#region Old Code
/*
using System;
using System.Drawing;
using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AspNetForums;
using AspNetForums.Components;
using System.ComponentModel;
using System.IO;
using AspNetForums.Enumerations;
using AspNetForums.Controls;

namespace AspNetForums.Controls {

	// *********************************************************************
	//  SiteSettingsView
	//
	/// <summary>
	/// This server control is used to display Site Settings information.
	/// </summary>
	/// 
	// ********************************************************************
	public class SiteSettingsView : SkinnedForumWebControl {
		SiteSettings settings;

		string generalSettings              = "/Admin/View-SiteSettings.ascx";
		string memberSettings               = "/Admin/View-MemberSettings.ascx";

		SiteSettingsMode viewMode = SiteSettingsMode.GeneralSettings;

		// Form Elements
		//
		TextBox siteName;
		TextBox siteDescription;
		TextBox smtpServer;
		RadioButtonList enableEmail;
		RadioButtonList enableAnonymousTracking;
		RadioButtonList disabled;
		TextBox threadsPerPage;
		TextBox postsPerPage;
		RadioButtonList enableRolesCookie;
		RadioButtonList autoPostDelete;
		RadioButtonList autoUserDelete;
		RadioButtonList anonymousPosting;
		RadioButtonList publicMemberList;
		RadioButtonList publicCurrentTime;
		RadioButtonList publicForumDescription;
		RadioButtonList enableEmoticons;
		RadioButtonList enableAvatars;
		RadioButtonList enableRemoteAvatars;
		RadioButtonList publicWhoIsOnline;
		RadioButtonList publicTodaysBirthdays;
		TextBox avatarHeight;
		TextBox avatarWidth;
		TextBox rolesCookieName;
		TextBox rolesCookieExpiration;
		TextBox anonymousCookieName;
		TextBox anonymousCookieExpiration;
		TextBox cookieDomain;
		TextBox anonymousUserOnlineTimeWindow;
		TextBox adminEmailAddress;
		TextBox companyAddress;
		TextBox companyFaxNumber;
		TextBox companyEmailAddress;
		TextBox companyName;
		Button save;
		Button reset;
		Button applicationSelect;

		#region Home Page Menus & Sections
		YesNoRadioButtonList publicSiteStatistics;
		#endregion

		#region Application to manage
		DomainDropDownList domain;
		#endregion

		#region RSS
		RadioButtonList enableRss;
		TextBox rssDefaultThreadsPerFeed;
		TextBox rssMaxThreadsPerFeed;
		TextBox rssCacheWindowInMinutes;
		#endregion

		#region User Registration
		RadioButtonList accountActivationOption;
		DropDownList passwordFormat;
		RadioButtonList passwordRecovery;
		RadioButtonList enableUsernameChange;
		TextBox signatureMaxLength;
		RadioButtonList allowSignatures;
		RadioButtonList enableSignatures;
		RadioButtonList allowNewUserRegistration;
		RadioButtonList allowLogin;
		ModerationLevelDropDownList newUserModerationLevel;
		RadioButtonList enableNewUsersModerated;
		RadioButtonList allowGender;
		#endregion

		#region User Posting
		RadioButtonList allowDuplicatePosts;
		TextBox duplicatePostIntervalInMinutes;
		TextBox postInterval;
		#endregion

		#region IP Address Tracking
		RadioButtonList trackPostsByIP;
		RadioButtonList displayPostIP;
		RadioButtonList displayPostIPAdminsModeratorsOnly;
		#endregion

		#region Date & Times
		TimezoneDropDownList timeZone;
		DateFormatDropDownList dateFormat;
		TextBox timeFormat;
		#endregion

		#region Posts
		TextBox popularPostThresholdPosts;
		TextBox popularPostThresholdViews;
		TextBox popularPostThresholdDays;
		#endregion

		#region Member list and Top Posters
		TextBox topPostMaxMembersToDisplay;
		TextBox membersPerPage;
		YesNoRadioButtonList enablePublicAdvancedMemberSearch;
		#endregion


		#region Post Editing
		YesNoRadioButtonList enableDisplayEditNotes;
		TextBox editPostBodyAge;
		TextBox editPostTitleAge;
		#endregion

		#region Forum URLs
		RadioButtonList enableSearchFriendlyURLs;
		#endregion

		protected override void CreateChildControls() {
			switch (viewMode) {
				case SiteSettingsMode.GeneralSettings:
					SkinFilename = generalSettings;
					break;

				case SiteSettingsMode.MemberSettings:
					SkinFilename = memberSettings;
					break;
			}

			base.CreateChildControls();
		}

		#region Initialize Skin
		// *********************************************************************
		//  Initializeskin
		//
		/// <summary>
		/// Initializes the user control loaded in CreateChildControls. Initialization
		/// consists of finding well known control names and wiring up any necessary events.
		/// </summary>
		/// 
		// ********************************************************************
		protected override void InitializeSkin(Control skin) {

			// TextBox
			//
			siteName                        = (TextBox) skin.FindControl("SiteName");
			siteDescription                 = (TextBox) skin.FindControl("SiteDescription");
			smtpServer                      = (TextBox) skin.FindControl("SmtpServer");
			threadsPerPage                  = (TextBox) skin.FindControl("ThreadsPerPage");
			postsPerPage                    = (TextBox) skin.FindControl("PostsPerPage");
			avatarHeight                    = (TextBox) skin.FindControl("AvatarHeight");
			avatarWidth                     = (TextBox) skin.FindControl("AvatarWidth");
			rolesCookieName                 = (TextBox) skin.FindControl("RolesCookieName");
			anonymousCookieName             = (TextBox) skin.FindControl("AnonymousCookieName");
			anonymousUserOnlineTimeWindow   = (TextBox) skin.FindControl("AnonymousUserOnlineTimeWindow");
			rolesCookieExpiration           = (TextBox) skin.FindControl("RolesCookieExpiration");
			anonymousCookieExpiration       = (TextBox) skin.FindControl("AnonymousCookieExpiration");
			cookieDomain                    = (TextBox) skin.FindControl("CookieDomain"); 

			#region Application to manage
			domain                          = (DomainDropDownList) skin.FindControl("Domain");
			applicationSelect               = (Button) skin.FindControl("SelectDomain");
			applicationSelect.Click         += new EventHandler(ChangeApplication_Click);
			#endregion

			#region Forums Disabled
			disabled                        = (RadioButtonList) skin.FindControl("Disabled");
			#endregion

			#region Contact Us
			adminEmailAddress               = (TextBox) skin.FindControl("AdminEmailAddress");
			companyAddress                  = (TextBox) skin.FindControl("CompanyAddress");
			companyFaxNumber                = (TextBox) skin.FindControl("CompanyFaxNumber");
			companyEmailAddress             = (TextBox) skin.FindControl("CompanyEmailAddress");
			companyName                     = (TextBox) skin.FindControl("CompanyName");
			#endregion

			#region Home Page Menus & Sections
			publicMemberList                = (RadioButtonList) skin.FindControl("PublicMemberList");
			publicWhoIsOnline               = (RadioButtonList) skin.FindControl("DisplayWhoIsOnline");
			publicTodaysBirthdays           = (RadioButtonList) skin.FindControl("DisplayBirthdays");
			publicCurrentTime               = (RadioButtonList) skin.FindControl("DisplayCurrentTime");
			publicForumDescription          = (RadioButtonList) skin.FindControl("DisplayForumDescription");
			publicSiteStatistics            = (YesNoRadioButtonList) skin.FindControl("DisplayStatistics");
			#endregion

			#region RSS
			enableRss                       = (RadioButtonList) skin.FindControl("EnableForumRSS");
			rssDefaultThreadsPerFeed        = (TextBox) skin.FindControl("RSSDefaultThreadsPerFeed");
			rssMaxThreadsPerFeed            = (TextBox) skin.FindControl("RSSMaxThreadsPerFeed");
			rssCacheWindowInMinutes         = (TextBox) skin.FindControl("RssCacheWindowInMinutes");
			#endregion

			#region User settings and Registration
			signatureMaxLength              = (TextBox) skin.FindControl("SignatureMaxLength");
			allowSignatures                 = (RadioButtonList) skin.FindControl("AllowSignatures");
			enableSignatures                = (RadioButtonList) skin.FindControl("EnableSignatures");
			accountActivationOption         = (RadioButtonList) skin.FindControl("AccountActivation");
			passwordRecovery                = (RadioButtonList) skin.FindControl("PasswordRecovery");
			passwordFormat                  = (DropDownList) skin.FindControl("PasswordFormat");
			enableUsernameChange            = (RadioButtonList) skin.FindControl("EnableUsernameChange");            
			allowNewUserRegistration        = (RadioButtonList) skin.FindControl("AllowNewUserRegistration");            
			allowLogin                      = (RadioButtonList) skin.FindControl("AllowLogin");            
			newUserModerationLevel          = (ModerationLevelDropDownList) skin.FindControl("NewUserModerationLevel");            
			allowGender                     = (RadioButtonList) skin.FindControl("AllowGender");            
			#endregion

			#region Date & Times
			dateFormat                      = (DateFormatDropDownList) skin.FindControl("DateFormat");
			timeFormat                      = (TextBox) skin.FindControl("TimeFormat");
			timeZone                        = (TimezoneDropDownList) skin.FindControl("Timezone");
			#endregion

			#region User Posting
			allowDuplicatePosts             = (RadioButtonList) skin.FindControl("AllowDuplicatePosts");
			duplicatePostIntervalInMinutes  = (TextBox) skin.FindControl("DuplicatePostIntervalInMinutes");
			postInterval                    = (TextBox) skin.FindControl("PostInterval");
			#endregion

			#region IP Address Tracking
			trackPostsByIP                      = (RadioButtonList) skin.FindControl("EnableTrackPostsByIP");
			displayPostIP                       = (RadioButtonList) skin.FindControl("DisplayPostIP");
			displayPostIPAdminsModeratorsOnly   = (RadioButtonList) skin.FindControl("DisplayPostIPAdminsModeratorsOnly");
			#endregion

			#region Member list and Top Posters
			topPostMaxMembersToDisplay          = (TextBox) skin.FindControl("MemberListTopPostersToDisplay");
			membersPerPage                      = (TextBox) skin.FindControl("MemberListPageSize");
			enablePublicAdvancedMemberSearch    = (YesNoRadioButtonList) skin.FindControl("MemberListAdvancedSearch");
			#endregion

			#region Posts
			popularPostThresholdPosts       = (TextBox) skin.FindControl("PopularPostThresholdPosts");
			popularPostThresholdViews       = (TextBox) skin.FindControl("PopularPostThresholdViews");
			popularPostThresholdDays        = (TextBox) skin.FindControl("PopularPostThresholdDays");
			#endregion

			#region Post Editing
			enableDisplayEditNotes          = (YesNoRadioButtonList) skin.FindControl("DisplayEditNotes");
			editPostBodyAge                 = (TextBox) skin.FindControl("PostEditBodyAgeInMinutes");
			editPostTitleAge                = (TextBox) skin.FindControl("PostEditTitleAgeInMinutes");
			#endregion

			#region Forum URLs
			enableSearchFriendlyURLs        = (RadioButtonList ) skin.FindControl("EnableSearchFriendlyURLs");
			#endregion


			// DropDownLists & RadioButtonLists
			//
			autoPostDelete                  = (RadioButtonList) skin.FindControl("AutoPostDelete");
			autoUserDelete                  = (RadioButtonList) skin.FindControl("AutoUserDelete");
			anonymousPosting                = (RadioButtonList) skin.FindControl("AnonymousPosting");
			enableEmoticons                 = (RadioButtonList) skin.FindControl("EnableEmoticons");
			enableAvatars                   = (RadioButtonList) skin.FindControl("EnableAvatars");
			enableRemoteAvatars             = (RadioButtonList) skin.FindControl("EnableRemoteAvatars");
			enableRolesCookie               = (RadioButtonList) skin.FindControl("EnableRolesCookie");
			enableAnonymousTracking         = (RadioButtonList) skin.FindControl("EnableAnonymousTracking");
			enableEmail                     = (RadioButtonList) skin.FindControl("EnableEmail");

			// Buttons
			//
			save                            = (Button) skin.FindControl("Save");
			reset                           = (Button) skin.FindControl("Reset");

			save.Attributes["onclick"] = "return confirm('" + string.Format(AspNetForums.Components.ResourceManager.GetString("Cache_Warning"), Globals.GetSiteSettings().SiteSettingsCacheWindowInMinutes) + "');";								

			// Wire-up buttons
			//
			save.Click += new EventHandler(Save_Click);
			reset.Click += new EventHandler(Reset_Click);

			if (!Page.IsPostBack)
				DataBind();

		}
		#endregion

		#region Reset Event
		public void Reset_Click (Object sender, EventArgs e) {
			settings = new SiteSettings();
			settings.SiteDomain = domain.SelectedDomain;

			settings.Save();

			DataBind();
		}
		#endregion

		#region Save Event
		public void Save_Click (Object sender, EventArgs e) {

			settings = SiteSettings.GetSiteSettings(domain.SelectedDomain);
			settings.SiteDomain = domain.SelectedDomain;

			switch (viewMode) {

				case SiteSettingsMode.MemberSettings:
					#region User Registration
					settings.AllowUserSignatures            = Boolean.Parse( allowSignatures.SelectedItem.Value);
					settings.EnableUserSignatures           = Boolean.Parse( enableSignatures.SelectedItem.Value);
					settings.UserSignatureMaxLength         = int.Parse( signatureMaxLength.Text);
					settings.AccountActivation              = (AccountActivation) Enum.Parse( typeof(AccountActivation), accountActivationOption.SelectedItem.Value);
					settings.PasswordRecovery               = (PasswordRecovery) Enum.Parse( typeof(PasswordRecovery), passwordRecovery.SelectedItem.Value);
					settings.PasswordFormat                 = (UserPasswordFormat) Enum.Parse( typeof(UserPasswordFormat), passwordFormat.SelectedItem.Value);
					settings.EnableUsernameChange           = Boolean.Parse( enableUsernameChange.SelectedItem.Value);
					settings.AllowNewUserRegistration       = Boolean.Parse( allowNewUserRegistration.SelectedItem.Value);
					settings.AllowLogin                     = Boolean.Parse( allowLogin.SelectedItem.Value);
					settings.NewUserModerationLevel         = newUserModerationLevel.SelectedValue;
					settings.AllowGender                    = Boolean.Parse( allowGender.SelectedItem.Value);
					#endregion

					#region User Avatars
					settings.EnableAvatars                  = Boolean.Parse( enableAvatars.SelectedItem.Value);
					settings.EnableRemoteAvatars            = Boolean.Parse( enableRemoteAvatars.SelectedItem.Value);
					settings.AvatarHeight                   = int.Parse( avatarHeight.Text);
					settings.AvatarWidth                    = int.Parse( avatarWidth.Text);
					#endregion

					break;

				default:
					// Tab 1
					settings.SiteName                       = siteName.Text;
					settings.SiteDescription                = siteDescription.Text;
					settings.ThreadsPerPage                 = Convert.ToInt32( threadsPerPage.Text );
					settings.PostsPerPage                   = Convert.ToInt32( postsPerPage.Text );

					#region Forums Disabled
					settings.ForumsDisabled                 = Boolean.Parse( disabled.SelectedItem.Value );
					#endregion

					#region Contact Us
					settings.AdminEmailAddress              = adminEmailAddress.Text;
					settings.CompanyName                    = companyName.Text;
					settings.CompanyAddress                 = companyAddress.Text;
					settings.CompanyContactUs               = companyEmailAddress.Text;
					settings.CompanyFaxNumber               = companyFaxNumber.Text;
					#endregion

					#region Home Page Menus & Sections
					settings.EnablePublicMemberList     = Boolean.Parse( publicMemberList.SelectedItem.Value );
					settings.EnableWhoIsOnline          = Boolean.Parse( publicWhoIsOnline.SelectedItem.Value);
					settings.EnableBirthdays            = Boolean.Parse( publicTodaysBirthdays.SelectedItem.Value);
					settings.EnableCurrentTime          = Boolean.Parse( publicCurrentTime.SelectedItem.Value);
					settings.EnableForumDescription     = Boolean.Parse( publicForumDescription.SelectedItem.Value);
					settings.EnableSiteStatistics       = publicSiteStatistics.SelectedValue;
					#endregion

					#region RSS
					settings.EnableRSS                  = Boolean.Parse( enableRss.SelectedItem.Value );
					settings.RssCacheWindowInMinutes    = int.Parse(rssCacheWindowInMinutes.Text);
					settings.RssDefaultThreadsPerFeed   = int.Parse(rssDefaultThreadsPerFeed.Text);
					settings.RssMaxThreadsPerFeed       = int.Parse(rssMaxThreadsPerFeed.Text);
					#endregion

					#region User Posting
					settings.EnableDuplicatePosts           = Boolean.Parse( allowDuplicatePosts.SelectedItem.Value );
					settings.DuplicatePostIntervalInMinutes = int.Parse(duplicatePostIntervalInMinutes.Text);
					settings.PostInterval                   = Convert.ToInt32( postInterval.Text );
					#endregion

					#region IP Address Tracking
					settings.EnableTrackPostsByIP               = Boolean.Parse( trackPostsByIP.SelectedItem.Value );
					settings.DisplayPostIP                      = Boolean.Parse( displayPostIP.SelectedItem.Value);
					settings.DisplayPostIPAdminsModeratorsOnly  = Boolean.Parse( displayPostIPAdminsModeratorsOnly.SelectedItem.Value);
					#endregion

					#region Date & Times
					settings.TimezoneOffset                 = Convert.ToInt32( timeZone.SelectedItem.Value );
					settings.TimeFormat                     = timeFormat.Text;
					settings.DateFormat                     = dateFormat.SelectedItem.Value;
					#endregion

					#region Posts
					settings.PopularPostLevelPosts          = Convert.ToInt32( popularPostThresholdPosts.Text );
					settings.PopularPostLevelViews          = Convert.ToInt32( popularPostThresholdViews.Text );
					settings.PopularPostLevelDays           = Convert.ToInt32( popularPostThresholdDays.Text );
					#endregion

					#region Post Editing
					settings.DisplayEditNotesInPost         = enableDisplayEditNotes.SelectedValue;
					settings.PostEditBodyAgeInMinutes       = int.Parse(editPostBodyAge.Text);
					//settings.PostEditTitleAgeInMinutes      = int.Parse(editPostTitleAge.Text);
					#endregion

					#region Forum URLs
					settings.EnableSearchFriendlyURLs       = Boolean.Parse( enableSearchFriendlyURLs.SelectedItem.Value );
					#endregion

					#region Member list and Top Posters
					settings.MaxTopPostersToDisplay     = int.Parse(topPostMaxMembersToDisplay.Text);
					settings.MembersPerPage             = int.Parse(membersPerPage.Text);
					settings.EnablePublicAdvancedMemberSearch   = enablePublicAdvancedMemberSearch.SelectedValue;
					#endregion

					// Tab 3
					settings.EnableEmoticons                = Boolean.Parse( enableEmoticons.SelectedItem.Value);
					settings.EnableAutoPostDelete           = Boolean.Parse( autoPostDelete.SelectedItem.Value );
					settings.EnableAutoUserDelete           = Boolean.Parse( autoUserDelete.SelectedItem.Value );
            
					// Tab 5
					settings.EnableRoleCookie               = Boolean.Parse( enableRolesCookie.SelectedItem.Value);
					settings.EnableAnonymousUserTracking    = Boolean.Parse( enableAnonymousTracking.SelectedItem.Value);
					settings.RoleCookieName                 = rolesCookieName.Text;
					settings.RoleCookieExpiration           = int.Parse( rolesCookieExpiration.Text);
					settings.AnonymousCookieName            = anonymousCookieName.Text;
					settings.AnonymousCookieExpiration      = int.Parse( anonymousCookieExpiration.Text );
					settings.CookieDomain                   = cookieDomain.Text; 

					// Tab 6
					settings.EnableAnonymousUserPosting     = Boolean.Parse( anonymousPosting.SelectedItem.Value );
					settings.AnonymousUserOnlineTimeWindow  = int.Parse( anonymousUserOnlineTimeWindow.Text );
            
					// Tab 7
					settings.SmtpServer                     = smtpServer.Text;
					settings.EnableEmail                    = Boolean.Parse( enableEmail.SelectedItem.Value );
					break;

			}

		
			// Save the settings
			//
			settings.Save();
		
			// Done with the update, repopulate the form
			//
			DataBind();

		}
		#endregion
        
		public override void DataBind() {

			settings = SiteSettings.GetSiteSettings(domain.SelectedDomain);

			switch (viewMode) {

				case SiteSettingsMode.MemberSettings:
					#region User Registration
					accountActivationOption.Items.FindByValue( settings.AccountActivation.ToString() ).Selected = true;
					passwordRecovery.Items.FindByValue(settings.PasswordRecovery.ToString()).Selected = true;
					passwordFormat.Items.FindByValue(settings.PasswordFormat.ToString()).Selected = true;
					enableUsernameChange.Items.FindByValue(settings.EnableUsernameChange.ToString()).Selected = true;
					signatureMaxLength.Text = settings.UserSignatureMaxLength.ToString();
					allowSignatures.Items.FindByValue(settings.AllowUserSignatures.ToString()).Selected = true;
					enableSignatures.Items.FindByValue(settings.EnableUserSignatures.ToString()).Selected = true;
					allowNewUserRegistration.Items.FindByValue(settings.AllowNewUserRegistration.ToString()).Selected = true;
					allowLogin.Items.FindByValue(settings.AllowLogin.ToString()).Selected = true;
					newUserModerationLevel.SelectedValue = settings.NewUserModerationLevel;
					allowGender.Items.FindByValue(settings.AllowGender.ToString()).Selected = true;
					#endregion

					#region Avatars
					enableAvatars.Items.FindByValue(settings.EnableAvatars.ToString()).Selected = true;
					enableRemoteAvatars.Items.FindByValue(settings.EnableRemoteAvatars.ToString()).Selected = true;
					avatarHeight.Text = settings.AvatarHeight.ToString();
					avatarWidth.Text = settings.AvatarWidth.ToString();
					#endregion
					break;

				default:
					#region Forums Disabled
					disabled.Items.FindByValue(settings.ForumsDisabled.ToString()).Selected = true;
					#endregion

					#region Contact Us
					adminEmailAddress.Text          = settings.AdminEmailAddress;
					companyAddress.Text             = settings.CompanyAddress;
					companyFaxNumber.Text           = settings.CompanyFaxNumber;
					companyEmailAddress.Text        = settings.CompanyContactUs;
					companyName.Text                = settings.CompanyName;
					#endregion

					#region Home Page Menus & Sections
					publicMemberList.Items.FindByValue(settings.EnablePublicMemberList.ToString()).Selected = true;
					publicWhoIsOnline.Items.FindByValue(settings.EnableWhoIsOnline.ToString()).Selected = true;
					publicTodaysBirthdays.Items.FindByValue(settings.EnableBirthdays.ToString()).Selected = true;
					publicCurrentTime.Items.FindByValue(settings.EnableCurrentTime.ToString()).Selected = true;
					publicForumDescription.Items.FindByValue(settings.EnableForumDescription.ToString()).Selected = true;
					publicSiteStatistics.SelectedValue = settings.EnableSiteStatistics;
					#endregion

					#region RSS
					enableRss.Items.FindByValue(settings.EnableRSS.ToString()).Selected = true;
					rssCacheWindowInMinutes.Text = settings.RssCacheWindowInMinutes.ToString();
					rssDefaultThreadsPerFeed.Text = settings.RssDefaultThreadsPerFeed.ToString();
					rssMaxThreadsPerFeed.Text = settings.RssMaxThreadsPerFeed.ToString();
					#endregion

					#region User Posting
					allowDuplicatePosts.Items.FindByValue(settings.EnableDuplicatePosts.ToString()).Selected = true;
					duplicatePostIntervalInMinutes.Text = settings.DuplicatePostIntervalInMinutes.ToString();
					#endregion

					#region Date & Times
					timeZone.SelectedValue = settings.TimezoneOffset.ToString();
					timeFormat.Text = settings.TimeFormat;
					if (dateFormat.Items.FindByValue( settings.DateFormat ) != null)
						dateFormat.Items.FindByValue( settings.DateFormat ).Selected = true;
					#endregion

					#region Posts
					popularPostThresholdPosts.Text = settings.PopularPostLevelPosts.ToString();
					popularPostThresholdViews.Text = settings.PopularPostLevelViews.ToString();
					popularPostThresholdDays.Text = settings.PopularPostLevelDays.ToString();
					#endregion

					#region IP Address Tracking
					trackPostsByIP.Items.FindByValue(settings.EnableTrackPostsByIP.ToString()).Selected = true;
					displayPostIP.Items.FindByValue(settings.DisplayPostIP.ToString()).Selected = true;
					displayPostIPAdminsModeratorsOnly.Items.FindByValue(settings.DisplayPostIPAdminsModeratorsOnly.ToString()).Selected = true;
					#endregion

					#region Post Editing
					enableDisplayEditNotes.SelectedValue    = settings.DisplayEditNotesInPost;
					editPostBodyAge.Text                    = settings.PostEditBodyAgeInMinutes.ToString();
					editPostTitleAge.Text                   = settings.PostEditTitleAgeInMinutes.ToString();
					#endregion

					#region Forum URLs
					enableSearchFriendlyURLs.Items.FindByValue(settings.EnableSearchFriendlyURLs.ToString()).Selected = true;
					#endregion

					#region Member list and Top Posters
					topPostMaxMembersToDisplay.Text     = settings.MaxTopPostersToDisplay.ToString();
					membersPerPage.Text                 = settings.MembersPerPage.ToString();
					enablePublicAdvancedMemberSearch.SelectedValue = settings.EnablePublicAdvancedMemberSearch;
					#endregion

					// Tab 1
					siteName.Text = settings.SiteName;
					siteDescription.Text = settings.SiteDescription;

					threadsPerPage.Text = settings.ThreadsPerPage.ToString();
					postsPerPage.Text = settings.PostsPerPage.ToString();

					// Tab 3
					enableEmoticons.Items.FindByValue(settings.EnableEmoticons.ToString()).Selected = true;
					postInterval.Text = settings.PostInterval.ToString();
					autoPostDelete.Items.FindByValue(settings.EnableAutoPostDelete.ToString()).Selected = true;
					autoUserDelete.Items.FindByValue(settings.EnableAutoUserDelete.ToString()).Selected = true;
            
					// Tab 5
					enableAnonymousTracking.Items.FindByValue(settings.EnableAnonymousUserTracking.ToString()).Selected = true;
					rolesCookieName.Text = settings.RoleCookieName;
					rolesCookieExpiration.Text = settings.RoleCookieExpiration.ToString();
					enableRolesCookie.Items.FindByValue( settings.EnableRoleCookie.ToString() ).Selected = true;
					anonymousCookieName.Text = settings.AnonymousCookieName;
					anonymousCookieExpiration.Text = settings.AnonymousCookieExpiration.ToString();
					cookieDomain.Text = settings.CookieDomain.ToString(); 

            
					// Tab 6
					anonymousPosting.Items.FindByValue(settings.EnableAnonymousUserPosting.ToString()).Selected = true;
					anonymousUserOnlineTimeWindow.Text = settings.AnonymousUserOnlineTimeWindow.ToString();
            
					// Tab 7
					smtpServer.Text = settings.SmtpServer;
					enableEmail.Items.FindByValue( settings.EnableEmail.ToString() ).Selected = true;

					break;
			}

		}

		public void ChangeApplication_Click (object sender, EventArgs e) {
			DataBind();
		}

		public SiteSettingsMode ViewMode {
			get { return viewMode; }
			set { viewMode = value; }
		}

	}
//		public void ChangeApplication_Click (object sender, EventArgs e) {
//			DataBind();
//		}	

}
*/
#endregion
