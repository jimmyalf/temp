using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Spinit.Wpc.Forum.Enumerations;

namespace Spinit.Wpc.Forum.Controls {

    public class DateFilter : PlaceHolder, INamingContainer {

        #region Member variables and constructor
        DropDownList options = new DropDownList();
        string text = Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_DateFilter_text");
        ThreadDateFilterMode filterMode = ThreadDateFilterMode.TwoWeeks;
        bool addText = true;
        bool lineBreak = false;
        bool autoPostback = false;

        public DateFilter() {
            // Add options to the drop down list
            //
            options.Items.Add(new ListItem(Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_DateFilter_All"), ((int) ThreadDateFilterMode.All).ToString() ));
            options.Items.Add(new ListItem(Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_DateFilter_LastVisit"), ((int) ThreadDateFilterMode.LastVisit).ToString()));
            options.Items.Add(new ListItem(Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_DateFilter_1Day"), ((int) ThreadDateFilterMode.OneDay).ToString() ));
            options.Items.Add(new ListItem(Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_DateFilter_2Days"), ((int) ThreadDateFilterMode.TwoDays).ToString()));
            options.Items.Add(new ListItem(Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_DateFilter_3Days"), ((int) ThreadDateFilterMode.ThreeDays).ToString()));
            options.Items.Add(new ListItem(Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_DateFilter_1Week"), ((int) ThreadDateFilterMode.OneWeek).ToString() ));
            options.Items.Add(new ListItem(Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_DateFilter_2Weeks"), ((int) ThreadDateFilterMode.TwoWeeks).ToString() ));
            options.Items.Add(new ListItem(Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_DateFilter_1Month"), ((int) ThreadDateFilterMode.OneMonth).ToString() ));
            options.Items.Add(new ListItem(Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_DateFilter_2Months"), ((int) ThreadDateFilterMode.TwoMonths).ToString() ));
            options.Items.Add(new ListItem(Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_DateFilter_3Months"), ((int) ThreadDateFilterMode.ThreeMonths).ToString() ));
            options.Items.Add(new ListItem(Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_DateFilter_6Months"), ((int) ThreadDateFilterMode.SixMonths).ToString() ));
            options.Items.Add(new ListItem(Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_DateFilter_1Year"), ((int) ThreadDateFilterMode.OneYear).ToString() ));

        }
        #endregion

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

        #region Render functions and Databinding
        protected override void CreateChildControls() {
            options.AutoPostBack = autoPostback;

            options.SelectedIndexChanged += new EventHandler(DateChanged_Click);

            Controls.Add(options);

        }

        protected override void Render(HtmlTextWriter writer) {

            if (addText)
                writer.Write(text);

            options.RenderControl(writer);

            
            if (lineBreak) {
                LiteralControl l = new LiteralControl("<br>");
                l.RenderControl(writer);
            }

        }

        
        #endregion

        #region Events
        
        // *********************************************************************
        //  DateChanged
        //
        /// <summary>
        /// Event raised when a an index has been selected by the end user
        /// </summary>
        /// 
        // ********************************************************************/
        public event System.EventHandler DateChanged;

        // *********************************************************************
        //  PageIndex_Click
        //
        /// <summary>
        /// Event raised when a new index is selected from the paging control
        /// </summary>
        /// 
        // ********************************************************************/
        void DateChanged_Click(Object sender, EventArgs e) {

            if (null != DateChanged)
                DateChanged(sender, e);

        }
        #endregion
        
        #region Public Properties

        public ThreadDateFilterMode SelectedValue {
            get {
                return (ThreadDateFilterMode) int.Parse(options.SelectedValue);
            }
            set {
                options.SelectedValue = ((int) value).ToString();
            }
        }

        public ListItemCollection Items {
            get {
                return options.Items;
            }
        }

        public bool AutoPostBack {
            get {
                return autoPostback;
            }
            set {
                autoPostback = value;
            }
        }

        public DateTime SelectedDate {
            get {

                switch ( (ThreadDateFilterMode) int.Parse(options.SelectedItem.Value)) {
                    case ThreadDateFilterMode.LastVisit:
                        return DateTime.MaxValue;

                    case ThreadDateFilterMode.OneDay:
                        return DateTime.Now.AddDays(-1);

                    case ThreadDateFilterMode.TwoDays:
                        return DateTime.Now.AddDays(-2);

                    case ThreadDateFilterMode.ThreeDays:
                        return DateTime.Now.AddDays(-3);

                    case ThreadDateFilterMode.OneWeek:
                        return DateTime.Now.AddDays(-7);

                    case ThreadDateFilterMode.TwoWeeks:
                        return DateTime.Now.AddDays(-14);

                    case ThreadDateFilterMode.OneMonth:
                        return DateTime.Now.AddMonths(-1);

                    case ThreadDateFilterMode.TwoMonths:
                        return DateTime.Now.AddMonths(-2);

                    case ThreadDateFilterMode.ThreeMonths:
                        return DateTime.Now.AddMonths(-3);

                    case ThreadDateFilterMode.SixMonths:
                        return DateTime.Now.AddMonths(-6);

                    case ThreadDateFilterMode.OneYear:
                        return DateTime.Now.AddYears(-1);

                    default:
                        return DateTime.MinValue;


                }
            }
        }


        public bool AppendLineBreak {
            get {
                return lineBreak;
            }
            set {
                lineBreak = value;
            }
        }

        public bool AddText {
            get {
                return addText;
            }
            set {
                addText = value;
            }
        }
        #endregion
    }
}
