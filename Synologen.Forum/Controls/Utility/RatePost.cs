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
    //  RatePost
    //
    /// <summary>
    /// </summary>
    /// 
    // ********************************************************************/
    public class RatePost : WebControl, INamingContainer {

        ForumContext forumContext = ForumContext.Current;
        DropDownList rating;
        Button submitRating;
        Label rateTopic;
        Post post;

        protected override void CreateChildControls() {
            if ((Users.GetUser().UserID == post.User.UserID) || (Users.GetUser().IsAnonymous))
                return;

            submitRating = new Button();
            rating = new DropDownList();
            rateTopic = new Label();

            // Add the list items
            rating.Items.Add(new ListItem( ResourceManager.GetString("PostRating_Five"), "5"));
            rating.Items.Add(new ListItem( ResourceManager.GetString("PostRating_Four"), "4"));
            rating.Items.Add(new ListItem( ResourceManager.GetString("PostRating_Three"), "3"));
            rating.Items.Add(new ListItem( ResourceManager.GetString("PostRating_Two"), "2"));
            rating.Items.Add(new ListItem( ResourceManager.GetString("PostRating_One"), "1"));
            rating.Items.Add(new ListItem( ResourceManager.GetString("PostRating_Zero"), "0"));

            rateTopic.Text = ResourceManager.GetString("PostRating_Rate");
            submitRating.Text = ResourceManager.GetString("PostRating_RateButton");
            submitRating.Click += new EventHandler(RateTopic_Click);

            Controls.Add(new LiteralControl("<br>"));
            Controls.Add(rateTopic);
            Controls.Add(rating);
            Controls.Add(new LiteralControl(" "));
            Controls.Add(submitRating);

        }

        void RateTopic_Click (object sender, EventArgs e) {

            Threads.Rate( post.ThreadID, Users.GetUser().UserID, int.Parse( rating.SelectedValue ));

        }


        public Post Post {
            get {
                return post;
            }
            set {
                post = value;
            }
        }

    }
}