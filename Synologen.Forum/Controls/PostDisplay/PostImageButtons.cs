using System;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Text;
using Spinit.Wpc.Forum.Components;
using Spinit.Wpc.Forum.Enumerations;

namespace Spinit.Wpc.Forum.Controls {

    public class PostImageButtons : PlaceHolder {
        User user;
        ForumContext forumContext = ForumContext.Current;

		protected override void OnPreRender(EventArgs e) {
			/*
			StringBuilder im_Script = new StringBuilder();
			im_Script.Append( "<script language=\"javascript\" type=\"text/javascript\">" + "\n" );
			im_Script.Append( " function forums_PopWindow(url,w,h) {" + "\n" );
			im_Script.Append( " parameters = \"width=\" + w +\",height=\" + h + \",titlebar=1,resizable=no\";"  + "\n" );
			im_Script.Append( " popupWin = window.open(url, \"new_pop\", parameters);" + "\n" );
			im_Script.Append( "}" + "\n" );
			im_Script.Append( "</script>"  + "\n" );
			this.Page.RegisterClientScriptBlock("forums_IMPopups", im_Script.ToString() );	
			*/

			base.OnPreRender (e);
		}


        protected override void Render(HtmlTextWriter writer) {
            HyperLink l = new HyperLink();

            // User's profile
            //
            //            l.ImageUrl = Globals.GetSkinPath() + "/images/post_button_profile.gif";
            //            l.NavigateUrl = Globals.GetSiteUrls().UserProfile(user.UserID);
            //            l.RenderControl(writer);

            // Search
            //
            //            writer.Write(" ");
            //            l.ImageUrl = Globals.GetSkinPath() + "/images/post_button_search.gif";
            //            l.NavigateUrl = Globals.GetSiteUrls().Search;
            //            l.RenderControl(writer);
			

			User poster = user;
			User reader = Users.GetUser();

            // User's email
            //
            if ((!reader.IsAnonymous) && (!poster.IsAnonymous)) {
                writer.Write(" ");
                l.ImageUrl = Globals.GetSkinPath() + "/images/post_button_email.gif";
                l.NavigateUrl = Globals.GetSiteUrls().EmailToUser(poster.UserID);
                l.RenderControl(writer);
            }

            // User's Private Messages
            //
            if ((!reader.IsAnonymous) && (!poster.IsAnonymous)) {
                writer.Write(" ");
                l.ImageUrl = Globals.GetSkinPath() + "/images/post_button_pm.gif";
                l.NavigateUrl = Globals.GetSiteUrls().PrivateMessage(poster.UserID);
                l.RenderControl(writer);
            }

            // User's home page
            //
            if ((poster.WebAddress != String.Empty) && (!reader.IsAnonymous)) {
                writer.Write(" ");
                l.Target = "_blank";
                l.ImageUrl = Globals.GetSkinPath() + "/images/post_button_www.gif";
                l.NavigateUrl = user.WebAddress;
                l.RenderControl(writer);
            }

            // User's web log
            //
            if ((poster.WebLog != String.Empty) && (!reader.IsAnonymous)) {
                writer.Write(" ");
                l.Target = "_blank";
                l.ImageUrl = Globals.GetSkinPath() + "/images/post_button_weblog.gif";
                l.NavigateUrl = user.WebLog;
                l.RenderControl(writer);
            }


			/* POST RELEASE ------
			 * 
			 * 
			// Yahoo IM
			//
			if ((poster.YahooIM != String.Empty) && (!reader.IsAnonymous)) {
				writer.Write(" ");
				l.Target = "_blank";
				l.ImageUrl = Globals.GetSkinPath() + "/images/post_button_yahoo.gif";
				l.NavigateUrl = "http://edit.yahoo.com/config/send_webmesg?.target=" + poster.YahooIM + "&src=pg";
				l.RenderControl(writer);
			}
			// AOL IM
			//
			if ((poster.AolIM != String.Empty) && (!reader.IsAnonymous)) {
				HyperLink lAIM = new HyperLink();
				writer.Write(" ");
				lAIM.ImageUrl = Globals.GetSkinPath() + "/images/post_button_aim.gif";
				lAIM.NavigateUrl = "javascript:forums_PopWindow('" + Globals.ApplicationPath + "/utility/PopUp_AIM.aspx?id=" + poster.AolIM + "',155,300)";	
				//lAIM.Attributes["onclick"] = "return confirm('" + string.Format(Spinit.Wpc.Forum.Components.ResourceManager.GetString("IM_Warning"), "AOL Instant Messenger") + "');";								
				lAIM.RenderControl(writer);
			}   
			// MSN IM
			//
			if ((poster.MsnIM != String.Empty) && (!reader.IsAnonymous)) {
				HyperLink lMSN = new HyperLink();
				writer.Write(" ");
				//l.Target = "_blank";
				lMSN.ImageUrl = Globals.GetSkinPath() + "/images/post_button_msnm.gif";
				lMSN.NavigateUrl = "javascript:forums_PopWindow('" + Globals.ApplicationPath + "/utility/PopUp_MSN.aspx?id=" + user.MsnIM + "&n=" + user.Username + "',155,200)";	
				//lMSN.Attributes["onclick"] = "return confirm('" + string.Format(Spinit.Wpc.Forum.Components.ResourceManager.GetString("IM_Warning"), "MSN Messenger") + "');";								
				lMSN.RenderControl(writer);
			}
			// ICQ IM
			//
			if ((user.IcqIM != String.Empty) && (!reader.IsAnonymous)) {
				HyperLink lICQ = new HyperLink(); 
				writer.Write(" ");
				lICQ.ImageUrl = Globals.GetSkinPath() + "/images/post_button_icq.gif";
				lICQ.NavigateUrl = "javascript:forums_PopWindow('" + Globals.ApplicationPath + "/utility/PopUp_ICQ.aspx?id=" + user.IcqIM + "&t=icq&n=" + user.Username + "',450,450)";	
				lICQ.RenderControl(writer);
			}
			*/

            // User's IM
            //
/* Currently disabled
            if (!user.IsAnonymous) {
                writer.Write(" ");
                l.ImageUrl = Globals.GetSkinPath() + "/images/post_button_buddy.gif";
                l.NavigateUrl = "TODO";
                l.RenderControl(writer);

                // User's stats
                //
                writer.Write(" ");
                l.ImageUrl = Globals.GetSkinPath() + "/images/post_button_stats.gif";
                l.NavigateUrl = "TODO";
                l.RenderControl(writer);
            }
*/
        }

        public User User {
            get {
                return user;
            }
            set {
                user = value;
            }
        }
    }

}
