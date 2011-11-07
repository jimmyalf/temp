using System;
using System.Web.UI.WebControls;
using Spinit.Wpc.Forum.Components;

namespace Spinit.Wpc.Forum.Controls {

    /// <summary>
    /// This server control displays a list of Date formatting options.
    /// </summary>
    public class EmailNotificationDropDownList : DropDownList {
        ForumContext forumContext = ForumContext.Current;
        Label label = new Label();

        /// <remarks>
        /// Public constructor, internally populates the list of date formats.
        /// </remarks>
        public EmailNotificationDropDownList() {

            // Set the default text
            //
            //label.Text  = ResourceManager.GetString("EmailNotificationDropDownList_When");

            // Add datetime formats
            //
            Items.Add(new ListItem(ResourceManager.GetString("EmailNotificationDropDownList_NeverSend"), "0"));
            Items.Add(new ListItem(ResourceManager.GetString("EmailNotificationDropDownList_NewThreads"), "1"));
            Items.Add(new ListItem(ResourceManager.GetString("EmailNotificationDropDownList_NewPosts"), "2"));

            // Set the selected index
            //
            SelectedIndex = Forums.GetForumSubscriptionType(forumContext.ForumID);

            SelectedIndexChanged += new EventHandler(Subscription_Change);

        }


        protected override void Render(System.Web.UI.HtmlTextWriter writer) {

            // If the user is already authenticated we have no work to do
            if (!Page.Request.IsAuthenticated)
                return;

            label.RenderControl(writer);

            base.Render (writer);
        }


        // *********************************************************************
        //  Subscription_Change
        //
        /// <summary>
        /// User wants to change their subscription method
        /// </summary>
        /// 
        // ********************************************************************/ 
        private void Subscription_Change (Object sender, EventArgs e) {
            Forums.SetForumSubscriptionType(forumContext.ForumID, Int32.Parse(SelectedItem.Value));
        }

        public string Text {
            get {
                return label.Text;
            }
            set {
                label.Text = value;
            }
        }

    }
}
