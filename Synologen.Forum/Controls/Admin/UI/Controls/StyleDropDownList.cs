using System;
using System.Web.UI.WebControls;
using Spinit.Wpc.Forum.Components;

namespace Spinit.Wpc.Forum.Controls {

    /// <summary>
    /// This server control displays a list of Date formatting options.
    /// </summary>
    public class StyleDropDownList : DropDownList {

        /// <remarks>
        /// Public constructor, internally populates the list of date formats.
        /// </remarks>
        public StyleDropDownList() {

			foreach( Spinit.Wpc.Forum.Components.Style style in Styles.GetStyles() ) {

				Items.Add( new ListItem( style.StyleName, style.StyleId.ToString() ));
			}
        }
    }
}
