using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

using Spinit.Wpc.Forum;
using Spinit.Wpc.Forum.Components;
using Spinit.Wpc.Forum.Controls;
using Spinit.Wpc.Forum.Enumerations;

namespace Spinit.Wpc.Forum.Controls
{
	/// <summary>
	/// Summary description for PasswordRecoveryRadioButtonList.
	/// </summary>
	[DefaultProperty("Text"), 
		ToolboxData("<{0}:PasswordRecoveryRadioButtonList runat=server></{0}:PasswordRecoveryRadioButtonList>")]
	public class PasswordRecoveryRadioButtonList : RadioButtonList
	{
		public PasswordRecoveryRadioButtonList() {

//			this.Items.Add( new ListItem( ResourceManager.GetString("MemberSettings_RecoveryMode_Email"), Enumerations.PasswordRecovery.Email.ToString() ));
            this.Items.Add( new ListItem( ResourceManager.GetString("MemberSettings_RecoveryMode_Reset"), Enumerations.PasswordRecovery.Reset.ToString() ));
            this.Items.Add( new ListItem( ResourceManager.GetString("MemberSettings_RecoveryMode_Question"), Enumerations.PasswordRecovery.QuestionAndAnswer.ToString() ));
		}
	}
}
