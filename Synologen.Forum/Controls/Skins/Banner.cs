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
	/// This server control is used to display the common banner for all pages.
	/// </summary>
	// ***********************************************************************/
	public class Banner : SkinnedForumWebControl {

		#region member varaibles
		ForumContext forumContext = ForumContext.Current;
		string skinFilename = "Skin-Banner.ascx";

		// *********************************************************************
		//  DisplayTitle
		//
		/// <summary>
		/// Constructor
		/// </summary>
		// ***********************************************************************/
		public Banner() : base() {

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

			// add attribute settings here as needed.
			//



		}
		#endregion
	}
}
