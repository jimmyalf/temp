using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Spinit.Wpc.Forum.Components;
using Spinit.Wpc.Forum;

namespace Spinit.Wpc.Forum.Controls {

    public class HideReadPostsDropDownList : DropDownList {
        User user;

        public HideReadPostsDropDownList() {
            user = Users.GetUser();

            // Add options to the drop down list
            //
            Items.Add(new ListItem(Spinit.Wpc.Forum.Components.ResourceManager.GetString("ViewThreads_ShowRead"), "False"));
            Items.Add(new ListItem(Spinit.Wpc.Forum.Components.ResourceManager.GetString("ViewThreads_HideRead"), "True"));

        }

        public new bool SelectedValue {
            get { return bool.Parse(base.SelectedValue); }
            set { 
                // Deselect current item
                Items.FindByValue( base.SelectedValue ).Selected = false;
                Items.FindByValue( value.ToString() ).Selected = true; 
            }
        }

    }
}
