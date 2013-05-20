using System;
using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Spinit.Wpc.Forum;
using Spinit.Wpc.Forum.Components;
using System.ComponentModel;
using System.IO;
using System.Drawing;
using System.Xml;

namespace Spinit.Wpc.Forum.Controls.PostDisplay {

    /// <summary>
    /// The Vote control is allows for users to vote on topics. The results are stored in 
    /// a SQL Server table and a HTML graph is used to display the selections/percentages.
    /// </summary>
    [
    ToolboxData("<{0}:Vote runat=\"server\" />")
    ]
    public class PollPost : SkinnedForumWebControl {

        string skinFilename = "Skin-Poll.ascx";
        RadioButtonList radioButtonList;
        VoteDetails voteDetails;
        VoteResultCollection voteResults;
        Color barBackColor;
        Color barForeColor;
        string buttonText = Spinit.Wpc.Forum.Components.ResourceManager.GetString("PostDisplay_PollPost_buttonText");
        int postID;

        // *********************************************************************
        //  PollPost
        //
        /// <summary>
        /// Constructor
        /// </summary>
        // ***********************************************************************/
        public PollPost(Post post) : base() {

            postID = post.PostID;
            voteResults = Votes.GetVoteResults(postID);
            voteDetails = new VoteDetails(postID, post.Subject, post.Body);

            // Assign a default template name
            if (SkinFilename == null)
                SkinFilename = skinFilename;

        }

        // *********************************************************************
        //  Initializeskin
        //
        /// <summary>
        /// Initialize the control template and populate the control with values
        /// </summary>
        // ***********************************************************************/
        override protected void InitializeSkin(Control skin) {

            PlaceHolder vote;
            PlaceHolder results;
            Button voteButton;

            // Find the place holder control
            //
            vote = (PlaceHolder) skin.FindControl("Vote");
            vote.Controls.Add( RenderVoteOptions() );

            // Find the results place holder control
            //
            results = (PlaceHolder) skin.FindControl("Results");
            results.Controls.Add( RenderVoteResults() );

            // Wire up the vote button
            //
            voteButton = (Button) skin.FindControl("VoteButton");
            voteButton.Click += new EventHandler(VoteButton_Click);

        }

        protected Control RenderVoteOptions () {

            // Member variables
            //
            Table t = new Table();
            t.CellPadding = 3;
            t.CellSpacing = 0;
            t.ID = "HasNotVoted";
            TableRow choicesRow = new TableRow();
            TableRow buttonRow = new TableRow();
            TableCell choicesCell = new TableCell();
            TableCell buttonCell = new TableCell();
            radioButtonList = new RadioButtonList();
            radioButtonList.CssClass = "normalTextSmallBold";

            // Populate the radioButtonList
            //
            foreach (string key in voteDetails.VoteOptions.Keys) {
                radioButtonList.Items.Add(new ListItem((string)voteDetails.VoteOptions[key], key));
                radioButtonList.CssClass = "txt3";
            }

            // Insert the radio button into the cell
            //
            choicesCell.Controls.Add(radioButtonList);

            // Add row/cell to table
            //
            choicesRow.Cells.Add(choicesCell);
            t.Rows.Add(choicesRow);

            return t;
        }

        protected Control RenderVoteResults() {

            // Member Variables
            //
            int sum = 0;
            Table t = new Table();
            t.CellPadding = 3;
            t.CellSpacing = 0;
            TableRow resultsRow = new TableRow();
            TableRow footerRow = new TableRow();
            TableCell resultsCell = new TableCell();
            TableCell footer = new TableCell();

            // Format the table
            //
            t.BorderWidth = 0;

            // Calculate the sum
            //
            foreach (string key in voteResults.Keys)
                sum = sum + voteResults[key].VoteCount;

            // Calculate percentage
            //
            foreach (string key in voteDetails.VoteOptions.Keys) {

                // Internal variables/instances
                //
                double d = 0;
                int pollValue = 0;
                TableRow row = new TableRow();
                TableCell progressCell = new TableCell();
                TableCell percentageCell = new TableCell();
                ProgressBar bar = new ProgressBar();
				
                // Get the poll value
                //
                if (null != voteResults[key])
                    pollValue = voteResults[key].VoteCount;

                // Percentage for this poll value
                //
                d = ((double)pollValue / (double)sum) * 100;

                // Set properties for each bar
                //
                bar.PercentageOfProgress = (int)d;

                // Add the bar and set properties of the cell
                //
                progressCell.Controls.Add(bar);
                progressCell.HorizontalAlign = HorizontalAlign.Right;
                progressCell.VerticalAlign = VerticalAlign.Top;
                progressCell.Width = 100;

                // Special case 0
                //
                Label percentageText = new Label();
                percentageText.CssClass = "normalTextSmallBold";
                if ((double.IsNaN(d)) || (0 == d))
                    percentageText.Text = "(0%)";
                else
                    percentageText.Text = "(" + d.ToString("##.#") + "%)";

                percentageCell.Controls.Add(percentageText);
                percentageCell.CssClass = "txt3";

                // Format percentage cell
                //
                percentageCell.HorizontalAlign = HorizontalAlign.Left;
                percentageCell.VerticalAlign = VerticalAlign.Top;

                // Add the cells to the row
                //
                row.Cells.Add(progressCell);
                row.Cells.Add(percentageCell);

                // Add the row to the table
                //
                t.Rows.Add(row);

            }

            // What you voted for
            //
            resultsCell.ColumnSpan = 3;
            resultsCell.CssClass = "txt3";
            resultsCell.HorizontalAlign = HorizontalAlign.Right;

            if (radioButtonList.SelectedItem != null) {
                Label resultsText = new Label();
                resultsText.Text = Spinit.Wpc.Forum.Components.ResourceManager.GetString("PostDisplay_PollPost_resultsText") + "<b>" + voteDetails.VoteOptions[radioButtonList.SelectedItem.Value] + "</b>";
                resultsCell.Controls.Add(resultsText);

                // Add results cell/row to table
                //
                resultsRow.Cells.Add(resultsCell);
                t.Rows.Add(resultsRow);
            }
			
            // Set footer properties
            //
            Label totalVotes = new Label();
            totalVotes.Text = Spinit.Wpc.Forum.Components.ResourceManager.GetString("PostDisplay_PollPost_totalVotes") + "<b>" + sum + "</b>";
            totalVotes.CssClass = "txt3";
            footer.Controls.Add(totalVotes);
            footer.ColumnSpan = 3;
            footer.HorizontalAlign = HorizontalAlign.Right;

            // Add footer cell and add to table
            //
            footerRow.Cells.Add(footer);
            t.Rows.Add(footerRow);

            return t;
        }

        void VoteButton_Click(Object sender, EventArgs e) {
            Votes.Vote(postID, radioButtonList.SelectedItem.Value);
            
            /*
            // Increment our internal poll results
            //
            if (null == voteResults[radioButtonList.SelectedItem.Value])
                pollresults[radioButtonList.SelectedItem.Value] = 1;
            else
                pollresults[radioButtonList.SelectedItem.Value] = (int)pollresults[radioButtonList.SelectedItem.Value] + 1;
            */

            // Clear the controls collection
            //
            Control remove = FindControl("HasNotVoted");
            Controls.Remove(remove);

            // User has voted
            //
            voteDetails.SetHtasVotedCookie(radioButtonList.SelectedItem.Value);

            // Change the display to HasVoted
            //
            RenderVoteResults();

        }

        // *********************************************************************
        //  ButtonText
        //
        /// <summary>
        /// Used to control the text on the button. Default is 'Vote'
        /// </summary>
        // ***********************************************************************/        
        [
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
        PersistenceMode(PersistenceMode.Attribute)
        ]
        public String ButtonText {
            get {
                return buttonText;
            }
            set {
                buttonText = value;
            }
        }

        // *********************************************************************
        //  BarBackColor
        //
        /// <summary>
        /// Controls the back ground color of the rendered bar(s).
        /// </summary>
        // ***********************************************************************/        
        [
        Category("Style"),
        PersistenceMode(PersistenceMode.Attribute)
        ]
        public Color BarBackColor {
            get {
                return barBackColor;
            }
            set {
                barBackColor = value;
            }
        }

        // *********************************************************************
        //  BarForeColor
        //
        /// <summary>
        /// Controls the foreground color of the rendered bar(s).
        /// </summary>
        // ***********************************************************************/        
        [
        Category("Style"),
        PersistenceMode(PersistenceMode.Attribute)
        ]
        public Color BarForeColor {
            get {
                return barForeColor;
            }
            set {
                barForeColor = value;
            }
        }    
    }
}
