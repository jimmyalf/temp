// TODO: Remove code that display help...

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
using Spinit.Wpc.Forum.Enumerations;

namespace Spinit.Wpc.Forum.Controls {

    // *********************************************************************
    //  BuiltInReportsView
    //
    /// <summary>
    /// This server control is used to display built in reports
    /// </summary>
    /// 
    // ********************************************************************/
    public class BuiltInReportsView : SkinnedForumWebControl {

        #region Member variables and constructor
        DropDownList domain;
        DropDownList filter1;
        DropDownList filter2;
        ReportsViewMode reportMode      = ReportsViewMode.AllExceptions;
        Repeater reportRepeater;
        Button deleteSelected;
        Button deleteAll;
        
        ForumContext forumContext = ForumContext.Current;
		

        // *********************************************************************
        //  BuiltInReportsView
        //
        /// <summary>
        /// The constructor simply checks for a ForumID value passed in via the
        /// HTTP POST or GET.
        /// properties.
        /// </summary>
        /// 
        // ********************************************************************/
        public BuiltInReportsView() {

            reportMode = (ReportsViewMode) ForumContext.GetIntFromQueryString(forumContext.Context, "ReportMode");

            switch (reportMode) {

                default:
                    SkinFilename = "Admin/view-Report-ForumExceptions.ascx";
                    break;
            }

        }
        #endregion

        #region Skin initialization
         // *********************************************************************
        //  Initializeskin
        //
        /// <summary>
        /// Initializes the user control loaded in CreateChildControls. Initialization
        /// consists of finding well known control names and wiring up any necessary events.
        /// </summary>
        /// 
        // ********************************************************************/ 
        protected override void InitializeSkin(Control skin) {

            domain = (DropDownList) skin.FindControl("Domain");
            ArrayList applications = SiteSettings.AllSiteSettings();
            domain.DataSource = applications;
            domain.DataTextField = "SiteDomain";
            domain.DataValueField = "SiteID";
            domain.DataBind();

            ((Button) skin.FindControl("SelectDomain")).Click += new EventHandler(ChangeApplication_Click);

            switch (reportMode) {

                default:
                    filter1 = (DropDownList) skin.FindControl("ExceptionType");
                    filter1.Items.Add(new ListItem("All Exceptions", "-1"));

                    // Populate the filter
                    for (int i = 0; i < 1000; i++) {
                        if (Enum.GetName(typeof(ForumExceptionType), ((ForumExceptionType) i)) != null)
                            filter1.Items.Add(new ListItem( ((ForumExceptionType) i).ToString(), i.ToString() ));
                    }

                    filter2 = (DropDownList) skin.FindControl("MinFrequency");
                    ((Label) skin.FindControl("ForumName")).Text = "Report: Forum Exceptions";
                    ((Label) skin.FindControl("ForumDescription")).Text = "Exceptions that have occurred";
                    break;

            }

            reportRepeater  = (Repeater) skin.FindControl("ReportRepeater");

            reportRepeater.ItemCommand += new RepeaterCommandEventHandler(Repeater_ItemCommand);

        }
        #endregion

        #region Events
        public void ChangeApplication_Click (object sender, EventArgs e) {

            DataBind();

        }

        public void Repeater_ItemCommand (object sender, RepeaterCommandEventArgs e){
            ArrayList deleteList = new ArrayList();

            if ( ((Button) e.CommandSource).ID == "DeleteAll") {
                deleteList = null;
            } else {
                foreach (RepeaterItem i in reportRepeater.Items) {
                    CheckBox c = i.FindControl("BulkEdit") as CheckBox;

                    if (c.Checked) {
                        deleteList.Add( int.Parse(c.Attributes["Value"]) );
                    }

                }
            }

            ForumException.DeleteExceptions(int.Parse(domain.SelectedValue), deleteList);

            DataBind();
        }

        #endregion

        #region Databinding
        public override void DataBind() {

            switch (reportMode) {
                default:
                    reportRepeater.DataSource = ForumException.GetExceptions( int.Parse(domain.SelectedValue), int.Parse(filter1.SelectedValue), int.Parse(filter2.SelectedValue) );
                    break;

            }

            if ( ((ArrayList) reportRepeater.DataSource).Count > 0) {
                reportRepeater.Visible = true;
                reportRepeater.DataBind();
            } else {
                reportRepeater.Visible = false;
            }

        }
        #endregion

        #region Public Properties

        /// <value>
        /// Controls the mode that the thread view control displays
        /// </value>
        public ReportsViewMode ReportMode {
            get {
                return reportMode;
            }
            set {
                reportMode = value;
            }
        }
        #endregion


    }

}