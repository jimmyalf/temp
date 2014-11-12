using System;
using System.Web.UI.WebControls;
using Spinit.Wpc.Forum.Enumerations;
using Spinit.Wpc.Forum.Components;

namespace Spinit.Wpc.Forum.Controls {
    
    public class AccountStatusDropDownList : DropDownList {

        public AccountStatusDropDownList() {

            // Add countries
            //
            Items.Add(new ListItem(Spinit.Wpc.Forum.Components.ResourceManager.GetString("AccountStatus_Approved"), ((int) UserAccountStatus.Approved).ToString()));
            Items.Add(new ListItem(Spinit.Wpc.Forum.Components.ResourceManager.GetString("AccountStatus_ApprovalPending"), ((int) UserAccountStatus.ApprovalPending).ToString()));
            Items.Add(new ListItem(Spinit.Wpc.Forum.Components.ResourceManager.GetString("AccountStatus_Banned"), ((int) UserAccountStatus.Banned).ToString()));
            Items.Add(new ListItem(Spinit.Wpc.Forum.Components.ResourceManager.GetString("AccountStatus_Disapproved"), ((int) UserAccountStatus.Disapproved).ToString()));

        }

        public new UserAccountStatus SelectedValue {
            get { return (UserAccountStatus) int.Parse(base.SelectedValue); }
            set { base.SelectedValue = ((int) value).ToString(); }
        }

    }
}
