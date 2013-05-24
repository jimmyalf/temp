using System;
using System.Web;
using System.IO;
using System.Web.UI.WebControls;
using Spinit.Wpc.Forum.Components;
using Spinit.Wpc.Forum.Enumerations;

namespace Spinit.Wpc.Forum.Controls {

    public class SearchModeDropDownList : DropDownList {

        public SearchModeDropDownList() {

            // Add search types
            //
            Items.Add(new ListItem(Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_SearchModeDropDownList_Simple"), SearchMode.Simple.ToString()));
            Items.Add(new ListItem(Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_SearchModeDropDownList_FullText"), SearchMode.SqlServerFullText.ToString()));
            Items.Add(new ListItem(Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_SearchModeDropDownList_ForumsIndexer"), SearchMode.ForumsIndexer.ToString()));


        }

    }
}
