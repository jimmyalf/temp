using System;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI;
using Spinit.Wpc.Forum.Components;
using Spinit.Wpc.Forum.Enumerations;

namespace Spinit.Wpc.Forum.Controls {

    public class UserPermissions : PlaceHolder {
        ForumContext forumContext = ForumContext.Current;

		// EAD - 5/15/2004
		// This overall fit-n-finish needs some serious overhauling.
		// Suggesting maybe a ToolTip, or possible not even displaying
		// items they "can not" perform and only thins they can.
		//

        string postStats     = ResourceManager.GetString("User_UserPermissions_postStats") + "<br>";
        string modStatus     = ResourceManager.GetString("User_UserPermissions_forumModeration") + "<br>";
        string postNewTopics = ResourceManager.GetString("User_UserPermissions_postNewTopics") + "<br>";
        string replyToTopics = ResourceManager.GetString("User_UserPermissions_replyToTopics") + "<br>";
        string deletePosts   = ResourceManager.GetString("User_UserPermissions_deletePosts") + "<br>";
        string editPosts     = ResourceManager.GetString("User_UserPermissions_editPosts") + "<br>";
        string createPolls   = ResourceManager.GetString("User_UserPermissions_createPolls") + "<br>";
        string vote          = ResourceManager.GetString("User_UserPermissions_vote") + "<br>";
        string attachment    = ResourceManager.GetString("User_UserPermissions_attachment") + "<br>";
        string allow         = "<b>" + ResourceManager.GetString("User_UserPermissions_allow") + "</b>";
        string deny          = "<b>" + ResourceManager.GetString("User_UserPermissions_deny") + "</b>";
        string enabled       = "<b>" + ResourceManager.GetString("Enabled") + "</b>";
        string disabled      = "<b>" + ResourceManager.GetString("Disabled") + "</b>";
        string moderated     = "<b>" + ResourceManager.GetString("User_UserPermissions_moderated") + "</b>";
        string unmoderated   = "<b>" + ResourceManager.GetString("User_UserPermissions_unmoderated") + "</b>";

        protected override void Render(HtmlTextWriter writer) {
            Spinit.Wpc.Forum.Components.Forum forum;

            // Are we looking up the forum by forum id or post id
            //
            if (forumContext.ForumID > 0)
                forum = Forums.GetForum(forumContext.ForumID, true, true, Users.GetUser().UserID);
            else if (forumContext.PostID > 0)
                forum = Forums.GetForumByPostID(forumContext.PostID, Users.GetUser().UserID, false);
            else
                return;

            if (forum == null)
                throw new ForumException(ForumExceptionType.ForumNotFound, forumContext.ForumID.ToString());

            // Get the ForumPermission
            //
            ForumPermission p = forum.Permission;

            // Display what the user can/cannot do based on their permissions
            //

            // Post Attachments
            //
            if (p.Attachment == AccessControlEntry.Deny)
                writer.Write( String.Format(attachment, deny) );
            else
                writer.Write( String.Format(attachment, allow) );

            // Post new threads
            //
            if (p.Post == AccessControlEntry.Deny)
                writer.Write( String.Format(postNewTopics, deny) );
            else
                writer.Write( String.Format(postNewTopics, allow) );

            // Reply to threads
            //
            if (p.Reply == AccessControlEntry.Deny)
                writer.Write( String.Format(replyToTopics, deny) );
            else
                writer.Write( String.Format(replyToTopics, allow) );

            // Delete Posts
            //
            if (p.Delete == AccessControlEntry.Deny)
                writer.Write( String.Format(deletePosts, deny) );
            else
                writer.Write( String.Format(deletePosts, allow) );

            // Edit Posts
            //
            if (p.Edit == AccessControlEntry.Deny)
                writer.Write( String.Format(editPosts, deny) );
            else
                writer.Write( String.Format(editPosts, allow) );
            
            // Create Polls
            //
            if (p.CreatePoll == AccessControlEntry.Deny)
                writer.Write( String.Format(createPolls, deny) );
            else
                writer.Write( String.Format(createPolls, allow) );
                        
            // Vote in polls
            //
            if (p.Vote == AccessControlEntry.Deny)
                writer.Write( String.Format(vote, deny) );
            else
                writer.Write( String.Format(vote, allow) );

            // Are we tracking post stats
            //
            if (forum.EnablePostStatistics) 
                writer.Write( String.Format(postStats, enabled) );
            else
                writer.Write( String.Format(postStats, disabled) );

            // Is the forum moderated
            //
            if (forum.IsModerated) 
                writer.Write( String.Format(modStatus, moderated) );
            else
                writer.Write( String.Format(modStatus, unmoderated) );


        }

    }

}
