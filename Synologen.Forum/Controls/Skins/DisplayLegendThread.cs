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
	//  DisplayLegendThread
	//
	/// <summary>
	/// This server control is used to display the Thread Legend
	/// </summary>
	// ***********************************************************************/
	public class DisplayLegendThread : SkinnedForumWebControl {

		#region member vars
		ForumContext forumContext = ForumContext.Current;
		string skinFilename = "Skin-DisplayLegendThread.ascx";

		// *********************************************************************
		//  DisplayLegendThread
		//
		/// <summary>
		/// Constructor
		/// </summary>
		// ***********************************************************************/
		public DisplayLegendThread() : base() {

			if (SkinFilename == null)
				SkinFilename = skinFilename;

		}
		#endregion

		#region skin init (none)
		// *********************************************************************
		//  InitializeSkin
		//
		/// <summary>
		/// Initialize the control template and populate the control with values
		/// </summary>
		// ***********************************************************************/
		override protected void InitializeSkin(Control skin) {

		}
		#endregion
	}
}
