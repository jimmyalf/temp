using System;
using System.Web;
using System.Web.UI.WebControls;
using Spinit.Wpc.Forum.Components;
using Spinit.Wpc.Forum.Enumerations;

namespace Spinit.Wpc.Forum.Controls {

    /// <summary>
    /// Summary description for FilterUsersDropDownList.
    /// </summary>
    public class FilterUsersDropDownList : DropDownList {

        public FilterUsersDropDownList() {
            Items.Add( new ListItem(ResourceManager.GetString("UserFilterDropDownList_All"), ((int) ThreadUsersFilter.All).ToString() ));
            Items.Add( new ListItem(ResourceManager.GetString("UserFilterDropDownList_ParticipatedIn"), ((int) ThreadUsersFilter.HideTopicsParticipatedIn).ToString() ));
            Items.Add( new ListItem(ResourceManager.GetString("UserFilterDropDownList_NotParticipatedIn"), ((int) ThreadUsersFilter.HideTopicsNotParticipatedIn).ToString() ));
            Items.Add( new ListItem(ResourceManager.GetString("UserFilterDropDownList_HideAnonymous"), ((int) ThreadUsersFilter.HideTopicsByAnonymousUsers).ToString() ));
            Items.Add( new ListItem(ResourceManager.GetString("UserFilterDropDownList_HideNonAnonymous"), ((int) ThreadUsersFilter.HideTopicsByNonAnonymousUsers).ToString() ));
        }

        public new ThreadUsersFilter SelectedValue {
            get { return (ThreadUsersFilter) int.Parse(base.SelectedValue); }
            set { 
                // Deselect current item
                Items.FindByValue( base.SelectedValue ).Selected = false;
                Items.FindByValue( ((int) value).ToString() ).Selected = true; 
            }
        }

    }
}
