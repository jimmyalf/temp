using System;
using System.Web.UI.WebControls;

namespace Spinit.Wpc.Forum.Controls {

    /// <summary>
    /// This server control displays a list of Date formatting options.
    /// </summary>
    public class DateFormatDropDownList : DropDownList {

        /// <remarks>
        /// Public constructor, internally populates the list of date formats.
        /// </remarks>
        public DateFormatDropDownList() {

            // Add datetime formats
            //
            Items.Add(new ListItem(Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_DateFormatDropDownList_F1Text"), Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_DateFormatDropDownList_F1")));
            Items.Add(new ListItem(Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_DateFormatDropDownList_F2Text"), Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_DateFormatDropDownList_F2")));
            Items.Add(new ListItem(Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_DateFormatDropDownList_F3Text"), Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_DateFormatDropDownList_F3")));
            Items.Add(new ListItem(Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_DateFormatDropDownList_F4Text"), Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_DateFormatDropDownList_F4")));
            Items.Add(new ListItem(Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_DateFormatDropDownList_F5Text"), Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_DateFormatDropDownList_F5")));
            Items.Add(new ListItem(Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_DateFormatDropDownList_F6Text"), Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_DateFormatDropDownList_F6")));
            Items.Add(new ListItem(Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_DateFormatDropDownList_F7Text"), Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_DateFormatDropDownList_F7")));

        }
    }
}
