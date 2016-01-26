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
	/// Summary description for AccountActivationRadioButtonList.
	/// </summary>
	[DefaultProperty("Text"), 
		ToolboxData("<{0}:AccountActivationRadioButtonList runat=server></{0}:AccountActivationRadioButtonList>")]
	public class AccountActivationRadioButtonList : RadioButtonList
	{
		public AccountActivationRadioButtonList() {
			this.Items.Add( new ListItem( ResourceManager.GetString("MemberSettings_ActivateMode_Automatic"), Enumerations.AccountActivation.Automatic.ToString() ));
			this.Items.Add( new ListItem( ResourceManager.GetString("MemberSettings_ActivateMode_Email"), Enumerations.AccountActivation.Email.ToString() ));
			this.Items.Add( new ListItem( ResourceManager.GetString("MemberSettings_ActivateMode_Admin"), Enumerations.AccountActivation.AdminApproval.ToString() ));
		}
	}
}
