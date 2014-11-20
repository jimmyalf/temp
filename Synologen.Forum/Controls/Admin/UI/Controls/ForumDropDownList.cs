using System;
using System.Web.UI.WebControls;

using Spinit.Wpc.Forum;
using Spinit.Wpc.Forum.Components;

namespace Spinit.Wpc.Forum.Controls {

    /// <summary>
    /// This server control displays a list of Date formatting options.
    /// </summary>
    public class ForumDropDownList : DropDownList {

        /// <remarks>
        /// Public constructor, internally populates the list of date formats.
        /// </remarks>
        public ForumDropDownList() {

            foreach (Spinit.Wpc.Forum.Components.Forum forum in Forums.GetForums())
            {

				Items.Add( new ListItem( forum.Name, forum.ForumID.ToString() ));
			}
        }
    }
}
