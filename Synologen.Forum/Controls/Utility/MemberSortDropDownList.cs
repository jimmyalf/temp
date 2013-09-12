using System;
using System.Web.UI.WebControls;
using Spinit.Wpc.Forum.Enumerations;

namespace Spinit.Wpc.Forum.Controls {
    
    public class MemberSortDropDownList : DropDownList {

        public MemberSortDropDownList() {

            // Add countries
            //
            Items.Add(new ListItem(Spinit.Wpc.Forum.Components.ResourceManager.GetString("MemberSortDropDownList_DateLastActive"), ((int) SortUsersBy.LastActiveDate).ToString()));
            Items.Add(new ListItem(Spinit.Wpc.Forum.Components.ResourceManager.GetString("MemberSortDropDownList_DateJoined"), ((int) SortUsersBy.JoinedDate).ToString()));
            Items.Add(new ListItem(Spinit.Wpc.Forum.Components.ResourceManager.GetString("MemberSortDropDownList_NumPosts"), ((int) SortUsersBy.Posts).ToString()));
            Items.Add(new ListItem(Spinit.Wpc.Forum.Components.ResourceManager.GetString("MemberSortDropDownList_Username"), ((int) SortUsersBy.Username).ToString()));

        }

        public SortUsersBy SelectedSortOrder {

            get {
                return (SortUsersBy) int.Parse(SelectedItem.Value);
            }
        }
    
    }
}
