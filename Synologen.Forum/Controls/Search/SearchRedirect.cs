using System;
using System.Drawing;
using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Spinit.Wpc.Forum;
using Spinit.Wpc.Forum.Components;
using System.ComponentModel;
using System.IO;
using System.Web.Security;

namespace Spinit.Wpc.Forum.Controls {

    [
    ParseChildren(true)	
    ]
    /// <summary>
    /// Summary description for Summary.
    /// </summary>
    public class SearchRedirect : SkinnedForumWebControl {

        string skinFilename = "Skin-SearchRedirect.ascx";
        Button button;
        TextBox textbox;

        // *********************************************************************
        //  SearchRedirect
        //
        /// <summary>
        /// Constructor
        /// </summary>
        // ***********************************************************************/
        public SearchRedirect() : base() {

            if (SkinFilename == null)
                SkinFilename = skinFilename;

        }

        // *********************************************************************
        //  InitializeSkin
        //
        /// <summary>
        /// Initialize the control template and populate the control with values
        /// </summary>
        // ***********************************************************************/
        override protected void InitializeSkin(Control skin) {

            // Find the search text box
            //
            textbox = (TextBox) skin.FindControl("SearchText");
			textbox.TextChanged += new System.EventHandler(SearchButton_Click);

            // Find the search button
            //
            button = (Button) skin.FindControl("SearchButton");
            button.Click += new System.EventHandler(SearchButton_Click);
            button.Text = Spinit.Wpc.Forum.Components.ResourceManager.GetString("SearchRedirect_SearchButton");
            
        }


        // *********************************************************************
        //  SearchButton_Click
        //
        /// <summary>
        /// Event handler to handle the login button click event
        /// </summary>
        // ***********************************************************************/
        public void SearchButton_Click(Object sender, EventArgs e) {
            string forumsToSearch = "";

            if (ForumContext.Current.ForumID > 0)
                forumsToSearch = Spinit.Wpc.Forum.Search.ForumsToSearchEncode(ForumContext.Current.ForumID.ToString());

            Page.Response.Redirect(Globals.GetSiteUrls().SearchForText(Globals.HtmlEncode(textbox.Text), forumsToSearch, "") );
            Page.Response.End();

        }

    }
}
