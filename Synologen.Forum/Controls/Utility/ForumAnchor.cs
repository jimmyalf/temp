using System;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using Spinit.Wpc.Forum.Components;
using Spinit.Wpc.Forum.Enumerations;

namespace Spinit.Wpc.Forum.Controls {

    public class ForumAnchor : HtmlAnchor {

        ForumContext forumContext = ForumContext.Current;
        ForumAnchorType anchorType = ForumAnchorType.PostsUnanswered;
		bool showIcon = true;
		int nbspacing = 0;

        protected override void CreateChildControls() {
			string nbspPadding = "";

			// loop through and create the amount of spacing
			//
			if (nbspacing > 0) {
				for (int i = 0; i < nbspacing; i++) {
					nbspPadding += "&nbsp;";
				}
			}
            
            switch (anchorType) {

                case ForumAnchorType.MenuHome:
                    this.HRef = Globals.GetSiteUrls().Home;
					
					if (showIcon)
						this.InnerHtml = "<img border=\"0\" src=\"" + Globals.GetSkinPath() + "/images/icon_mini_home.gif\" style=\"vertical-align: middle;\">" + Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_ForumAnchorType_MenuHome") + nbspPadding;
					else
						this.InnerHtml = Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_ForumAnchorType_MenuHome") + nbspPadding;
                    break;

                case ForumAnchorType.MenuSearch:
                    this.HRef = Globals.GetSiteUrls().Search;
					
					if (showIcon)
						this.InnerHtml = "<img border=\"0\" src=\"" + Globals.GetSkinPath() +"/images/icon_mini_search.gif\" style=\"vertical-align: middle;\">" + Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_ForumAnchorType_MenuSearch") + nbspPadding;
					else
						this.InnerHtml = Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_ForumAnchorType_MenuSearch") + nbspPadding;
                    break;

                case ForumAnchorType.MenuEditProfile:
                    this.HRef = Globals.GetSiteUrls().UserEditProfile;
					
					if (showIcon)
						this.InnerHtml = "<img border=\"0\" src=\"" + Globals.GetSkinPath() + "/images/icon_mini_profile.gif\" style=\"vertical-align: middle;\">" + Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_ForumAnchorType_MenuEditProfile") + nbspPadding;
					else
						this.InnerHtml = Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_ForumAnchorType_MenuEditProfile") + nbspPadding;
                    break;

                case ForumAnchorType.MenuLogin:
                    this.HRef = Globals.GetSiteUrls().Login;
					
					if (showIcon)
						this.InnerHtml = "<img border=\"0\" src=\"" + Globals.GetSkinPath() + "/images/icon_mini_login.gif\" style=\"vertical-align: middle;\">" + Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_ForumAnchorType_MenuLogin") + nbspPadding;
					else
						this.InnerHtml = Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_ForumAnchorType_MenuLogin") + nbspPadding;					
                    break;

                case ForumAnchorType.MenuLogout:
					this.HRef = Globals.GetSiteUrls().Logout;
					
					if (showIcon)
						this.InnerHtml = "<img border=\"0\" src=\"" + Globals.GetSkinPath() + "/images/icon_mini_login.gif\" style=\"vertical-align: middle;\">" + Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_ForumAnchorType_MenuLogout") + nbspPadding;
					else
						this.InnerHtml = Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_ForumAnchorType_MenuLogout") + nbspPadding;
                    break;

                case ForumAnchorType.MenuRegister:
                    this.HRef = Globals.GetSiteUrls().UserRegister;
					
					if (showIcon)
						this.InnerHtml = "<img border=\"0\" src=\"" + Globals.GetSkinPath() + "/images/icon_mini_register.gif\" style=\"vertical-align: middle;\">" + Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_ForumAnchorType_MenuRegister") + nbspPadding;
					else
						this.InnerHtml = Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_ForumAnchorType_MenuRegister") + nbspPadding;
                    break;

                case ForumAnchorType.MenuMemberList:
                    this.HRef = Globals.GetSiteUrls().UserList;
					
					if (showIcon)
						this.InnerHtml = "<img border=\"0\" src=\"" + Globals.GetSkinPath() + "/images/icon_mini_memberlist.gif\" style=\"vertical-align: middle;\">" + Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_ForumAnchorType_MenuMemberList") + nbspPadding;
					else
						InnerHtml = Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_ForumAnchorType_MenuMemberList") + nbspPadding;
                    break;

                case ForumAnchorType.MenuMyForums:
					this.HRef = Globals.GetSiteUrls().UserMyForums;
					
					if (showIcon)
						this.InnerHtml = "<img border=\"0\" src=\"" + Globals.GetSkinPath() + "/images/icon_mini_myforums.gif\" style=\"vertical-align: middle;\">" + Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_ForumAnchorType_MenuMyForums") + nbspPadding;
					else
						this.InnerHtml = Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_ForumAnchorType_MenuMyForums") + nbspPadding;
                    break;

                case ForumAnchorType.MenuFaq:
                    this.HRef = Globals.GetSiteUrls().FAQ;

					User user = ForumContext.Current.User;
					Spinit.Wpc.Forum.Configuration.ForumConfiguration config = Spinit.Wpc.Forum.Configuration.ForumConfiguration.GetConfig();

					if( user			!= null &&
						config			!= null &&
						user.Language	!= null &&
						user.Language	!= String.Empty &&
						config.DefaultLanguage != null &&
						config.DefaultLanguage != String.Empty ) {

						this.HRef = this.HRef.Replace(config.DefaultLanguage, ForumContext.Current.User.Language );
					}
					
					if (showIcon)
						this.InnerHtml = "<img border=\"0\" src=\"" + Globals.GetSkinPath() + "/images/icon_mini_faq.gif\" style=\"vertical-align: middle;\">" + Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_ForumAnchorType_MenuFaq") + nbspPadding;
					else
						this.InnerHtml = Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_ForumAnchorType_MenuFaq") + nbspPadding;
                    break;

                case ForumAnchorType.MenuModerate:
                    this.HRef = Globals.GetSiteUrls().Moderate;
					
					if (showIcon)
						this.InnerHtml = "<img border=\"0\" src=\"" + Globals.GetSkinPath() + "/images/icon_mini_moderate.gif\" style=\"vertical-align: middle;\">" + Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_ForumAnchorType_MenuModerate") + nbspPadding;
					else
						this.InnerHtml = Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_ForumAnchorType_MenuModerate") + nbspPadding;
                    break;

                case ForumAnchorType.MenuAdmin:
                    this.HRef = Globals.GetSiteUrls().Admin;
					
					if (showIcon)
						this.InnerHtml = "<img border=\"0\" src=\"" + Globals.GetSkinPath() + "/images/icon_mini_admin.gif\" style=\"vertical-align: middle;\">" + Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_ForumAnchorType_MenuAdmin") + nbspPadding;
					else
						this.InnerHtml = Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_ForumAnchorType_MenuAdmin") + nbspPadding;
                    break;

                case ForumAnchorType.PostsActive:
                    this.HRef = Globals.GetSiteUrls().PostsActive;
                    this.InnerText = Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_ForumAnchorType_PostsActive");
                    break;

                case ForumAnchorType.PostsUnanswered:
                    this.HRef = Globals.GetSiteUrls().PostsUnanswered;
                    this.InnerText = Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_ForumAnchorType_PostsUnanswered");
                    break;

                case ForumAnchorType.UserForgotPassword:
                    this.HRef = Globals.GetSiteUrls().UserForgotPassword;
                    this.InnerText = Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_ForumAnchorType_UserForgotPassword");
                    break;

                case ForumAnchorType.UserRegister:
                    this.HRef = Globals.GetSiteUrls().UserRegister;
                    this.InnerText = Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_ForumAnchorType_UserRegister");
                    break;

                case ForumAnchorType.Login:
                    this.HRef = Globals.GetSiteUrls().Login;
                    this.InnerText = Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_ForumAnchorType_Login");
                    break;

            }
        }

        public ForumAnchorType AnchorType {
            get {
                return anchorType;
            }
            set {
                anchorType = value;
            }
        }

		// used for showing/hiding the icon.
		//
		public bool ShowIcon {
			get { 
				return showIcon; 
			}
			set { 
				showIcon = value;
			}
		}

		// used for inserting &nbsp;s around the text.
		// this is because we hide/show them as needed,
		// and nbsps don't format properly in the skin.
		//
		public int nbSpacing {
			get { 
				return nbspacing; 
			}
			set { 
				nbspacing = value;
			}
		}
    }
}


