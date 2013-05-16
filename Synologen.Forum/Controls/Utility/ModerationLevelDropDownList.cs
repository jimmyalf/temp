using System;
using System.Web.UI.WebControls;
using Spinit.Wpc.Forum.Enumerations;
using Spinit.Wpc.Forum.Components;

namespace Spinit.Wpc.Forum.Controls {
    
    public class ModerationLevelDropDownList : DropDownList {

        public ModerationLevelDropDownList() {

            // Add countries
            //
            Items.Add(new ListItem(Spinit.Wpc.Forum.Components.ResourceManager.GetString("ModerationLevel_Unmoderated"), ((int) ModerationLevel.Unmoderated).ToString()));
            Items.Add(new ListItem(string.Format(Spinit.Wpc.Forum.Components.ResourceManager.GetString("ModerationLevel_AutoUnmoderate"), Globals.GetSiteSettings().AutoUnmoderateAferNApprovedPosts.ToString()), ((int) ModerationLevel.AutoUnmoderate).ToString()));
            Items.Add(new ListItem(Spinit.Wpc.Forum.Components.ResourceManager.GetString("ModerationLevel_Moderated"), ((int) ModerationLevel.Moderated).ToString()));
            Items.Add(new ListItem(Spinit.Wpc.Forum.Components.ResourceManager.GetString("ModerationLevel_CannotBeUnmoderated"), ((int) ModerationLevel.CannotBeUnmoderated).ToString()));
            Items.Add(new ListItem(Spinit.Wpc.Forum.Components.ResourceManager.GetString("ModerationLevel_RequireGlobalModeratorApproval"), ((int) ModerationLevel.RequireGlobalModeratorApproval).ToString()));
            Items.Add(new ListItem(Spinit.Wpc.Forum.Components.ResourceManager.GetString("ModerationLevel_RequireAdminApproval"), ((int) ModerationLevel.RequireAdminApproval).ToString()));

        }

        public new ModerationLevel SelectedValue {
            get { return (ModerationLevel) int.Parse(base.SelectedValue); }
            set { base.SelectedValue = ((int) value).ToString(); }
        }

    }
}
