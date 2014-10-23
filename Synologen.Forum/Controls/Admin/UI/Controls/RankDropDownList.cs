using System;
using System.Web.UI.WebControls;
using Spinit.Wpc.Forum.Components;

namespace Spinit.Wpc.Forum.Controls {

    /// <summary>
    /// This server control displays a list of Date formatting options.
    /// </summary>
    public class RankDropDownList : DropDownList {

        /// <remarks>
        /// Public constructor, internally populates the list of date formats.
        /// </remarks>
        public RankDropDownList() {

			foreach( Rank rank in Ranks.GetRanks() ) {

				Items.Add( new ListItem( rank.RankName, rank.RankId.ToString() ));
			}
        }
    }
}
