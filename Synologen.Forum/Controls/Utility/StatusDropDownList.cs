using System;
using System.Web.UI.WebControls;
using Spinit.Wpc.Forum.Components;
using Spinit.Wpc.Forum.Enumerations;

namespace Spinit.Wpc.Forum.Controls {

    
    public class StatusDropDownList : DropDownList {

        public StatusDropDownList() {
            Items.Add(new ListItem(ResourceManager.GetString("Status_NotSet"), ((int) ThreadStatus.NotSet).ToString()));
            Items.Add(new ListItem(ResourceManager.GetString("Status_Open"), ((int) ThreadStatus.Open).ToString()));
            Items.Add(new ListItem(ResourceManager.GetString("Status_Resolved"), ((int) ThreadStatus.Resolved).ToString()));
            Items.Add(new ListItem(ResourceManager.GetString("Status_Closed"), ((int) ThreadStatus.Closed).ToString()));
        }
    
    }
}
