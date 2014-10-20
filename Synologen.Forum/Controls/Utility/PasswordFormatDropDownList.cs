using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

using Spinit.Wpc.Forum;
using Spinit.Wpc.Forum.Components;
using Spinit.Wpc.Forum.Controls;

namespace Spinit.Wpc.Forum.Controls
{
	/// <summary>
	/// Summary description for PasswordFormatDropDownList.
	/// </summary>
	[DefaultProperty("Text"), 
		ToolboxData("<{0}:PasswordFormatDropDownList runat=server></{0}:PasswordFormatDropDownList>")]
	public class PasswordFormatDropDownList : DropDownList
	{
		public PasswordFormatDropDownList() {
			this.Items.Add( new ListItem( ResourceManager.GetString("MemberSettings_FormatMode_Clear"), Spinit.Wpc.Forum.Enumerations.UserPasswordFormat.ClearText.ToString() ));
			this.Items.Add( new ListItem( ResourceManager.GetString("MemberSettings_FormatMode_MD5"), Spinit.Wpc.Forum.Enumerations.UserPasswordFormat.MD5Hash.ToString() ));
			this.Items.Add( new ListItem( ResourceManager.GetString("MemberSettings_FormatMode_SHA1"), Spinit.Wpc.Forum.Enumerations.UserPasswordFormat.Sha1Hash.ToString() ));
			this.Items.Add( new ListItem( ResourceManager.GetString("MemberSettings_FormatMode_Encrypted"), Spinit.Wpc.Forum.Enumerations.UserPasswordFormat.Encyrpted.ToString() ));
		}
	}
}
