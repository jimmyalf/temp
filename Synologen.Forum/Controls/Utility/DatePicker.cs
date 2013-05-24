using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Spinit.Wpc.Forum.Controls {

    /// <summary>
    /// Summary description for DatePicker.
    /// </summary>
    public class DatePicker : WebControl, INamingContainer {
        DropDownList days = new DropDownList();
        DropDownList months = new DropDownList();
        DropDownList years = new DropDownList();
        Label label = new Label();
        DateTime selectedDate = new DateTime();

        protected override void CreateChildControls() {

            // Add default text
            days.Items.Add(new ListItem("Day", ""));
            months.Items.Add(new ListItem("Month", ""));
            years.Items.Add(new ListItem("Year", ""));

            for (int i=1; i<32; i++)
                days.Items.Add(new ListItem(i.ToString(), i.ToString()));

            for (int i=1; i<13; i++)
                months.Items.Add(new ListItem(i.ToString(), i.ToString()));

            for (int i= (DateTime.Now.Year); i>(DateTime.Now.Year - 100); i--)
                years.Items.Add(new ListItem(i.ToString(), i.ToString()));

            try {
                if (selectedDate > DateTime.MinValue) {
                    days.SelectedValue = selectedDate.Day.ToString();
                    months.SelectedValue = selectedDate.Month.ToString();
                    years.SelectedValue = selectedDate.Year.ToString();
                }
            } catch {}

            Controls.Add(months);
            Controls.Add(new LiteralControl(" "));
            Controls.Add(days);
            Controls.Add(new LiteralControl(" "));
            Controls.Add(years);
            Controls.Add(label);

        }

        public DateTime SelectedDate {
            get { 
                try {
                    return DateTime.Parse(months.SelectedValue + "/" + days.SelectedValue + "/" + years.SelectedValue);
                } catch {
                    return DateTime.MinValue;
                }
            }
            set { 
                selectedDate = value; 
            }
        }

    }
}
