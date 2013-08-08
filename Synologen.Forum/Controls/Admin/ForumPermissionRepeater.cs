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
using System.Web.Security;
using Spinit.Wpc.Forum.Enumerations;

namespace Spinit.Wpc.Forum.Controls {

	[
	ParseChildren(true)	
	]
	/// <summary>
	/// Summary description for Summary.
	/// </summary>
	public class ForumPermissionRepeater : Repeater {
		ForumContext forumContext = ForumContext.Current;
		string skinName = null;
		int forumID = -1;

		// *********************************************************************
		//  ForumRepeaterControl
		//
		/// <summary>
		/// Constructor
		/// </summary>
		// ***********************************************************************/
		public ForumPermissionRepeater() {

			// If we're in design-time we simply return
			if (null == HttpContext.Current)
				return;
		}

		// *********************************************************************
		//  CreateChildControls
		//
		/// <summary>
		/// Override create child controls
		/// </summary>
		/// 
		// ********************************************************************/   
		protected override void CreateChildControls() {
			EnableViewState = false;
			ArrayList roles = null;

			roles = ForumPermissions.GetForumPermissions(forumID);

			if (( roles != null) && (roles.Count != 0 )) {
				this.DataSource = roles;
				this.DataBind();
			}
		}
        
		// *********************************************************************
		//  ForumID  
		//
		/// <summary>
		/// Used to get the appropriate roles
		/// </summary>
		/// 
		// ********************************************************************/ 
		public int ForumID  {
			get  {
				return forumID;
			}
			set  {
				forumID = value;
			}
		}
        
		// *********************************************************************
		//  SkinName
		//
		/// <summary>
		/// Used to construct paths to images, etc. within controls.
		/// </summary>
		/// 
		// ********************************************************************/ 
		protected string SkinName {
			get {
				return skinName;
			}
			set {
				skinName = value;
			}
		}

	}
}
