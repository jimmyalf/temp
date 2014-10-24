// TODO: Remove code that display help...

using System;
using System.Drawing;
using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Spinit.Wpc.Forum;
using Spinit.Wpc.Forum.Components;
using System.ComponentModel;
using System.IO;
using Spinit.Wpc.Forum.Enumerations;

namespace Spinit.Wpc.Forum.Controls {

    // *********************************************************************
    //  PrivateMessageView
    //
    /// <summary>
    /// This server control is used to display private messages to a user
    /// </summary>
    /// 
    // ********************************************************************/
    public class PrivateMessageView : ThreadView {

        #region Member variables and constructor
        string skinFilename              = "View-PrivateMessages.ascx";
        
        // *********************************************************************
        //  PrivateMessageView
        //
        /// <summary>
        /// The constructor simply checks for a ForumID value passed in via the
        /// HTTP POST or GET.
        /// properties.
        /// </summary>
        /// 
        // ********************************************************************/
        public PrivateMessageView() {

            base.ThreadViewMode = ThreadViewMode.PrivateMessages;

            SkinFilename = skinFilename;

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
            base.InitializeSkin(skin);

            base.announcements.ItemCommand +=new RepeaterCommandEventHandler(Messages_ItemCommand);
            base.threads.ItemCommand +=new RepeaterCommandEventHandler(Messages_ItemCommand);

        }

        #endregion

        #region Events
        public void Messages_ItemCommand (object sender, RepeaterCommandEventArgs e){
            ArrayList deleteList = new ArrayList();

            foreach (RepeaterItem i in announcements.Items) {
                CheckBox c = i.FindControl("BulkEdit") as CheckBox;

                if (c.Checked) {
                    HtmlGenericControl d = i.FindControl("ThreadID") as HtmlGenericControl;
                    deleteList.Add( int.Parse(d.InnerText) );
                }

            }

            foreach (RepeaterItem i in threads.Items) {
                CheckBox c = i.FindControl("BulkEdit") as CheckBox;

                if (c.Checked) {
                    HtmlGenericControl d = i.FindControl("ThreadID") as HtmlGenericControl;
                    deleteList.Add( int.Parse(d.InnerText) );
                }

            }

            PrivateMessages.DeletePrivateMessages(Users.GetUser().UserID, deleteList);

			// EAD: TODO, this is where the "New Private Message" button
			// processing goes.
			//

            DataBind();
        }

        #endregion

    }

}