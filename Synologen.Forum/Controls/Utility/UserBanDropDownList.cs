using System;
using System.Web.UI.WebControls;
using Spinit.Wpc.Forum.Enumerations;
using Spinit.Wpc.Forum.Components;

namespace Spinit.Wpc.Forum.Controls {
    
    public class UserBanDropDownList : DropDownList {

        public UserBanDropDownList() {

            // Add countries
            //
            Items.Add(new ListItem(Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_UserBan_Permanent"), ((int) UserBanPeriod.Permanent).ToString()));
            Items.Add(new ListItem(Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_UserBan_1Day"), ((int) UserBanPeriod.OneDay).ToString()));
            Items.Add(new ListItem(Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_UserBan_3Days"), ((int) UserBanPeriod.ThreeDays).ToString()));
            Items.Add(new ListItem(Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_UserBan_5Days"), ((int) UserBanPeriod.FiveDays).ToString()));
            Items.Add(new ListItem(Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_UserBan_1Week"), ((int) UserBanPeriod.OneWeek).ToString()));
            Items.Add(new ListItem(Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_UserBan_2Weeks"), ((int) UserBanPeriod.TwoWeeks).ToString()));
            Items.Add(new ListItem(Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_UserBan_1Month"), ((int) UserBanPeriod.OneMonth).ToString()));

        }

        public new UserBanPeriod SelectedValue {
            get { return (UserBanPeriod) int.Parse(base.SelectedValue); }
            set { base.SelectedValue = ((int) value).ToString(); }
        }

    }
}
