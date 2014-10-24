using System;
using System.Web.UI.WebControls;

namespace Spinit.Wpc.Forum.Controls {
    /// <summary>
    /// Summary description for SearchForumsRadioButtonList.
    /// </summary>
    public class SearchForumsRadioButtonList : RadioButtonList {

        public SearchForumsRadioButtonList() {
            Items.Add(new ListItem("All Forums", true.ToString()));
            Items.Add(new ListItem("Selected Forums", false.ToString()));
        }

        public bool SearchAllForums {
            get {
                return bool.Parse(base.SelectedValue);
            }
        }

    }
}
