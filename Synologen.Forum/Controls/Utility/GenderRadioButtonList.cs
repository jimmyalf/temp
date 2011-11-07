using System;
using System.Web.UI.WebControls;
using Spinit.Wpc.Forum.Components;
using Spinit.Wpc.Forum.Enumerations;

namespace Spinit.Wpc.Forum.Controls {
    /// <summary>
    /// Summary description for YesNoRadioButtonList.
    /// </summary>
    public class GenderRadioButtonList : RadioButtonList {

        public GenderRadioButtonList () {
            Items.Add( new ListItem( Spinit.Wpc.Forum.Components.ResourceManager.GetString("NotSet"), ((int) Gender.NotSet).ToString() ) );
            Items.Add( new ListItem( Spinit.Wpc.Forum.Components.ResourceManager.GetString("Male"), ((int) Gender.Male).ToString() ) );
            Items.Add( new ListItem( Spinit.Wpc.Forum.Components.ResourceManager.GetString("Female"), ((int) Gender.Female).ToString() ) );
        }

        public new Gender SelectedValue {
            get { return (Gender) int.Parse( base.SelectedValue ); }
            set { base.SelectedValue = ((int) value).ToString(); }
        }

    }
}
