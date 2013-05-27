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
	//  DisplayUserWelcome
	//
	/// <summary>
	/// This server control is used to display the Forum Legend
	/// </summary>
	// ***********************************************************************/
	public class DisplayLegendForum : SkinnedForumWebControl {

		#region member vars
		ForumContext forumContext = ForumContext.Current;
		string skinFilename = "Skin-DisplayLegendForum.ascx";

		// *********************************************************************
		//  DisplayTitle
		//
		/// <summary>
		/// Constructor
		/// </summary>
		// ***********************************************************************/
		public DisplayLegendForum() : base() {

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

			// TODO: create a Formatter for these items.
			// use Formatter.StatusIcon()
			//

		}
		#endregion
	}
}
