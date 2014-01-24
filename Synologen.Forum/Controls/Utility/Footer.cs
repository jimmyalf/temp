using System;
using System.Web.UI.WebControls;
using Spinit.Wpc.Forum.Components;

namespace Spinit.Wpc.Forum.Controls {

    public class Footer : WebControl {

        protected override void Render(System.Web.UI.HtmlTextWriter writer) {

            // Please leave this reference to the Community Server :: Forums 
            // to help more users find out about this powerful forums system.
			//
			// Removal of copywrite is available also with support options at
			// http://www.telligentsystems.com/Solutions/Forums/
			//
            writer.Write("<p align=\"center\" class=\"txt4\">");
            writer.Write( "<a target=\"_blank\" href=\"http://www.telligentsystems.com/Solutions/Forums/\"><img alt=\"" + string.Format(ResourceManager.GetString("Footer_PoweredBy"), "Community Server :: Forums") + "\" border=\"0\" src=\"" + Globals.GetSiteUrls().Home + "utility/EULA.GIF" + "\"></a><br>");
            //writer.Write( "<a href=\"mailto:" + Globals.GetSiteSettings().AdminEmailAddress + "\">" + ResourceManager.GetString("Footer_QuestionsProblems") + "</a> | <a href=\"" + Globals.GetSiteUrls().TermsOfUse +"\">" + ResourceManager.GetString("Footer_TermsOfUse") + "</a>");
            writer.Write("</p>");

        }

    }

}
