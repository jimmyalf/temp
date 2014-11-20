using System;
using System.Drawing;
using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Spinit.Wpc.Forum;
using Spinit.Wpc.Forum.Components;
using System.ComponentModel;
using System.IO;

namespace Spinit.Wpc.Forum.Controls
{

	// *********************************************************************
	//  DisplayTitle
	//
	/// <summary>
	/// This server control is used to display forums title, at the top any page.
	/// </summary>
	// ***********************************************************************/
	public class DisplayTitle : SkinnedForumWebControl {

		#region member varaibles
		ForumContext forumContext = ForumContext.Current;
		string skinFilename = "Skin-DisplayTitle.ascx";

		// *********************************************************************
		//  DisplayTitle
		//
		/// <summary>
		/// Constructor
		/// </summary>
		// ***********************************************************************/
		public DisplayTitle() : base() {

			if (SkinFilename == null)
				SkinFilename = skinFilename;

		}
		#endregion

		#region skin init
		// *********************************************************************
		//  InitializeSkin
		//
		/// <summary>
		/// Initialize the control template and populate the control with values
		/// </summary>
		// ***********************************************************************/
		override protected void InitializeSkin(Control skin) {

			Label description;
			HyperLink siteName;
			HyperLink domainName;

			// show/hide the descriptiong based off admin option
			//
			description = (Label) skin.FindControl("Description");
			if (description != null) {
				if (!Globals.GetSiteSettings().EnableForumDescription) {
					description.Visible = false;
				} else {
					description.Text = Globals.GetSiteSettings().SiteDescription;
					description.Visible = true;
				}
			}

			// link up the SiteName title
			//
			siteName = (HyperLink) skin.FindControl("SiteName");
			if (siteName != null) {
				siteName.Text = Globals.GetSiteSettings().SiteName;
				siteName.ToolTip = Globals.GetSiteSettings().SiteDescription;
				siteName.NavigateUrl = Globals.GetSiteUrls().Home;
				siteName.Visible = true;
			}

			// link up the DomainName title
			//
			domainName = (HyperLink) skin.FindControl("DomainName");
			if (domainName != null) {
				domainName.Text = Globals.GetSiteSettings().SiteDomain.ToLower();
				domainName.ToolTip = Globals.GetSiteSettings().SiteName;
				domainName.NavigateUrl = Globals.GetSiteUrls().Home;
				domainName.Visible = true;
			}
		}
		#endregion
	}
}
