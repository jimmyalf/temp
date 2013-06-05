using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Spinit.Wpc.Forum.Controls {

    // ***************************************************
    // AddRemoveListBox
    //
    /// <summary>
    /// Custom server control used to add/remove items
    /// from a list box. The sourceListBox can be either another
    /// ListBox or a TextBox
    /// </summary>
    /// 
    /// <remarks>
    /// 2/26/03 - Rob Howard
    /// </remarks>
    ///
    public class AddRemoveListBox : WebControl, INamingContainer {
        ListBox source;
        ListBox target;
        object datasource;
        string dataTextField;
        string dataValueField;

        // ***************************************************
        // Controls
        //
        /// <summary>
        /// Override how this control handles its controls collection
        /// </summary>
        /// 
        public override ControlCollection Controls {
            get {
                EnsureChildControls();
                return base.Controls;
            }
        }


        protected override void CreateChildControls() {

            Controls.Clear();

            source = new ListBox();
            target = new ListBox();
            Button add = new Button();
            Button remove = new Button();

            // Set up auto-postback options for target list box
            //
            target.AutoPostBack = true;

            // Wire up events
            //
            target.SelectedIndexChanged += new EventHandler(Target_SelectedIndexChanged);
            add.Click += new EventHandler(Add_Click);
            remove.Click += new EventHandler(Remove_Click);

            // Display is rendered as a table
            //
            Table t = new Table();
            TableCell td;
            TableRow tr = new TableRow();

            t.CellSpacing = 0;

            // Add the listbox to the table
            //
            tr = new TableRow();
            td = new TableCell();
            td.Controls.Add(target);
            tr.Cells.Add(td);
            t.Rows.Add(tr);

            // Add the buttons to the table
            //
            td = new TableCell();
            td.Width = 1;
            td.VerticalAlign = VerticalAlign.Middle;

            // Create Add button
            //
            add.ID = "Add";
            add.Text = "<";
            td.Controls.Add(add);

            // Create Remove button
            //
            remove.ID = "Remove";
            remove.Text = ">";
            td.Controls.Add(remove);

            // Add cell to table
            //
            tr.Cells.Add(td);

            // Add the sourceListBox control
            //
            td = new TableCell();
            td.Controls.Add(source);
            tr.Cells.Add(td);

            // Add rows to table
            //
            t.Rows.Add(tr);

            Controls.Add(t);

            source.DataSource = datasource;
            source.DataTextField = dataTextField;
            source.DataValueField = dataValueField;
            source.DataBind();
        }
        
        
        // *********************************************************************
        //  SelectedIndexChanged
        //
        /// <summary>
        /// Event raised when the item in the target list box is changed
        /// </summary>
        /// 
        // ********************************************************************/
        public event System.EventHandler SelectedIndexChanged;

        // *********************************************************************
        //  Letter_Clicked
        //
        /// <summary>
        /// Event raised when a letter is clicked upon.
        /// </summary>
        /// 
        // ********************************************************************/
        private void Target_SelectedIndexChanged(Object sender, EventArgs e) {

            if (null != SelectedIndexChanged)
                SelectedIndexChanged(sender, e);

        }

        // ***************************************************
        // Add_Click
        //
        /// <summary>
        /// Internal event that handles the Add_Click event. This
        /// event is used to add items to the target list box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///
        protected void Add_Click (Object sender, EventArgs e) {

            // Find all the selected items to add
            //
            foreach (ListItem item in ((ListBox) source).Items) {

                if ((item.Selected == true) && (!target.Items.Contains(item))) {
                    item.Selected = false;
                    target.Items.Add(item);
                }

            }

            // Remove from the sourceListBox list box
            //
            foreach (ListItem item in target.Items) {

                if (source.Items.Contains(item))
                    source.Items.Remove(item);
            }

            // Set an item to be selected
            //
            //            targetListBox.Items[0].Selected = true;

            DataBind();

        }

        public ListItem TargetSelectedItem {
            get {
                return target.SelectedItem;
            }
        }

        // ***************************************************
        // Remove_Click
        //
        /// <summary>
        /// Internal event handler to remove items from the
        /// target list box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Remove_Click (Object sender, EventArgs e) {

            target.Items.Remove(target.SelectedItem);

            DataBind();

        }

        public string DataTextField {
            get {
                return dataTextField;
            }
            set {
                dataTextField = value;
            }
        }

        public string DataValueField {
            get {
                return dataValueField;
            }
            set {
                dataValueField = value;
            }
        }

        public object DataSource {
            get {
                return datasource;
            }
            set {
                datasource = value;
            }
        }

    }
}
