using System;
using System.Web.UI.WebControls;
using Spinit.Wpc.Forum.Components;

namespace Spinit.Wpc.Forum.Controls {

    /// <summary>
    /// This server control displays a list of Date formatting options.
    /// </summary>
    public class RoleDropDownList : DropDownList {

        /// <remarks>
        /// Public constructor, internally populates the list of date formats.
        /// </remarks>
        public RoleDropDownList() {

			foreach( Role role in Roles.GetRoles() ) {

				Items.Add( new ListItem( role.Name, role.RoleID.ToString() ));
			}
        }
    }
}
