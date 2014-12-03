using System;
using System.Web.UI.WebControls;
using Spinit.Wpc.Forum.Enumerations;

namespace Spinit.Wpc.Forum.Controls {
    
    public class SortOrderDropDownList : DropDownList {

        public SortOrderDropDownList() {

            // Add countries
            //
            Items.Add(new ListItem(Spinit.Wpc.Forum.Components.ResourceManager.GetString("SortOrderDropDownList_Desc"), ((int) SortOrder.Descending).ToString()));
            Items.Add(new ListItem(Spinit.Wpc.Forum.Components.ResourceManager.GetString("SortOrderDropDownList_Asc"), ((int) SortOrder.Ascending).ToString()));

        }

        public new SortOrder SelectedValue {
            get { return (SortOrder) int.Parse(base.SelectedValue); }
            set { 
                // Deselect current item
                Items.FindByValue( base.SelectedValue ).Selected = false;
                Items.FindByValue( ((int) value).ToString() ).Selected = true; 
            }
        }

    
    }
}
