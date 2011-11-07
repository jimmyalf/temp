using System;
using System.Web.UI.WebControls;
using Spinit.Wpc.Forum.Components;

namespace Spinit.Wpc.Forum.Controls {

    /// <summary>
    /// This server control displays a list of Date formatting options.
    /// </summary>
    public class ServiceDropDownList : DropDownList {

        /// <remarks>
        /// Public constructor, internally populates the list of date formats.
        /// </remarks>
        public ServiceDropDownList() {

			foreach( Service service in Services.GetServices() ) {

				Items.Add( new ListItem( service.ServiceName, service.ServiceId.ToString() ));
			}
        }
    }
}
