using System;
using System.Web.UI.WebControls;
using Spinit.Wpc.Forum.Components;

namespace Spinit.Wpc.Forum.Controls {

    /// <summary>
    /// This server control displays a list of Date formatting options.
    /// </summary>
    public class ReportDropDownList : DropDownList {

        /// <remarks>
        /// Public constructor, internally populates the list of date formats.
        /// </remarks>
        public ReportDropDownList() {

			foreach( Report report in Reports.GetReports() ) {

				Items.Add( new ListItem( report.ReportName, report.ReportId.ToString() ));
			}
        }
    }
}
