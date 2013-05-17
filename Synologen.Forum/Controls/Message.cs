using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Spinit.Wpc.Forum.Components;
using Spinit.Wpc.Forum.Enumerations;
using System.IO;

namespace Spinit.Wpc.Forum.Controls {

    // *********************************************************************
    //  Message
    //
    /// <summary>
    /// Renders the appropriate error message passed in by a query string id value.
    /// </summary>
    // ********************************************************************/ 

    public class Message : SkinnedForumWebControl {

        ForumContext forumContext = ForumContext.Current;
        string skinFilename = "Skin-Message.ascx";
        ForumMessage message;
        ForumExceptionType exceptionType;

        // *********************************************************************
        //  Message
        //
        /// <summary>
        /// Constructor
        /// </summary>
        //
        // ********************************************************************/
        public Message() : base() {

            // Assign a default template name
            if (SkinFilename == null)
                SkinFilename = skinFilename;

            exceptionType = (ForumExceptionType) forumContext.MessageID;

            message = ResourceManager.GetMessage( exceptionType );

        }

        // *********************************************************************
        //  Initializeskin
        //
        /// <summary>
        /// Initialize the control template and populate the control with values
        /// </summary>
        // ***********************************************************************/
        override protected void InitializeSkin(Control skin) {
            Label title;
            Label body;

            // Do some processing on the messages
            message.Body = message.Body.Replace("[HomeUrl]", "<a href=\"" + Globals.ApplicationPath + "\">" + Globals.GetSiteSettings().SiteName + Spinit.Wpc.Forum.Components.ResourceManager.GetString("A_Home") +"</a>");
            message.Body = message.Body.Replace("[LoginUrl]", "<a href=\"" + Globals.GetSiteUrls().Login + "\">" + Globals.GetSiteSettings().SiteName + Spinit.Wpc.Forum.Components.ResourceManager.GetString("A_Login") +"</a>");
            message.Body = message.Body.Replace("[ProfileUrl]", "<a href=\"" + Globals.GetSiteUrls().UserEditProfile + "\">" + Spinit.Wpc.Forum.Components.ResourceManager.GetString("ViewUserProfile_Title") + "</a>");

            // Handle duplicate post messages or moderation messages
            if ((message.Body.IndexOf("[DuplicatePost]") > -1) || (message.Body.IndexOf("[PendingModeration]") > -1)) {

                if (forumContext.ForumID > 0) {
                    message.Body = message.Body.Replace("[DuplicatePost]", "<a href=\"" + Globals.GetSiteUrls().Forum(forumContext.ForumID) + "\">" + Spinit.Wpc.Forum.Components.ResourceManager.GetString("A_ReturnToForum") + "</a>");
                    message.Body = message.Body.Replace("[PendingModeration]", "<a href=\"" + Globals.GetSiteUrls().Forum(forumContext.ForumID) + "\">" + Spinit.Wpc.Forum.Components.ResourceManager.GetString("A_ReturnToForum") + "</a>");
                } else if (forumContext.PostID > 0) {
                    message.Body = message.Body.Replace("[DuplicatePost]", "<a href=\"" + Globals.GetSiteUrls().Post(forumContext.PostID) + "\">" + Spinit.Wpc.Forum.Components.ResourceManager.GetString("A_ReturnToPost")+ "</a>");
                    message.Body = message.Body.Replace("[PendingModeration]", "<a href=\"" + Globals.GetSiteUrls().Post(forumContext.PostID) + "\">" + Spinit.Wpc.Forum.Components.ResourceManager.GetString("A_ReturnToForum") + "</a>");
                }
            }

            // Find the title
            title = (Label) skin.FindControl("MessageTitle");
            if (title != null) {
                title.Text = message.Title;
            }

            // Find the title
            body = (Label) skin.FindControl("MessageBody");
            if (body != null) {
                body.Text = message.Body;
            }

        }

    }
}