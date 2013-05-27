using System;
using System.Web.UI.WebControls;
using Spinit.Wpc.Forum.Components;

namespace Spinit.Wpc.Forum.Controls {
    /// <summary>
    /// Summary description for YesNoRadioButtonList.
    /// </summary>
    public class YesNoRadioButtonList : RadioButtonList {

        public YesNoRadioButtonList() {
            Items.Add( new ListItem( Spinit.Wpc.Forum.Components.ResourceManager.GetString("Yes"), true.ToString() ) );
            Items.Add( new ListItem( Spinit.Wpc.Forum.Components.ResourceManager.GetString("No"), false.ToString() ) );
        }

        public new bool SelectedValue {
            get { return bool.Parse( base.SelectedValue ); }
            set { base.SelectedValue = value.ToString(); }
        }

    }
}
