using System;
using System.Web.UI.WebControls;
using Spinit.Wpc.Forum.Enumerations;

namespace Spinit.Wpc.Forum.Controls {
    
    public class ThreadSortDropDownList : DropDownList {

        public ThreadSortDropDownList() {

            // Add countries
            //
            Items.Add(new ListItem(Spinit.Wpc.Forum.Components.ResourceManager.GetString("ThreadSortDropDownList_LastPost"), ((int) SortThreadsBy.LastPost).ToString()));
            Items.Add(new ListItem(Spinit.Wpc.Forum.Components.ResourceManager.GetString("ThreadSortDropDownList_StartedBy"), ((int) SortThreadsBy.ThreadAuthor).ToString()));
            Items.Add(new ListItem(Spinit.Wpc.Forum.Components.ResourceManager.GetString("ThreadSortDropDownList_Ratings"), ((int) SortThreadsBy.TotalRatings).ToString()));
            Items.Add(new ListItem(Spinit.Wpc.Forum.Components.ResourceManager.GetString("ThreadSortDropDownList_Views"), ((int) SortThreadsBy.TotalViews).ToString()));
            Items.Add(new ListItem(Spinit.Wpc.Forum.Components.ResourceManager.GetString("ThreadSortDropDownList_Replies"), ((int) SortThreadsBy.TotalReplies).ToString()));

        }

        public new SortThreadsBy SelectedValue {
            get { return (SortThreadsBy) int.Parse(base.SelectedValue); }
            set { 
                // Deselect current item
                Items.FindByValue( base.SelectedValue ).Selected = false;
                Items.FindByValue( ((int) value).ToString() ).Selected = true; 
            }
        }
    
    }
}
