using System;
using System.Web;
using System.Web.UI;
using Spinit.Wpc.Forum;
using Spinit.Wpc.Forum.Enumerations;
using Spinit.Wpc.Forum.Components;
using System.Web.UI.WebControls;

namespace Spinit.Wpc.Forum.Controls {
	/// <summary>
	/// Summary description for PostAttachment.
	/// </summary>
	public class DownloadPostAttachment : Control {
		ForumContext forumContext = ForumContext.Current;

		protected override void Render(HtmlTextWriter writer) {
			
			// Get the forum the attachment is in
			//
            Spinit.Wpc.Forum.Components.Forum forum = Forums.GetForumByPostID(forumContext.PostID, Users.GetUser().UserID, false);

			if (forum == null) {
				throw new ForumException(ForumExceptionType.ForumNotFound, forumContext.ForumID.ToString());
			}

			// Get the ForumPermission
			//
			ForumPermission permission = forum.Permission;

			// if user is not allowed to read this post, return
			//
			if (permission.Read == AccessControlEntry.Deny) {
				throw new ForumException(ForumExceptionType.GeneralAccessDenied, forumContext.ForumID.ToString());
			}

			Spinit.Wpc.Forum.Components.PostAttachment attachment = Posts.GetAttachment(forumContext.PostID);
	
			// Todo: make the context display in browser for browser-friendly items (images, videos, etc)
			//
			System.Web.HttpContext.Current.Response.Clear();	
			System.Web.HttpContext.Current.Response.ContentType = attachment.ContentType;
			System.Web.HttpContext.Current.Response.AppendHeader("content-disposition", "attachment; filename=\"" + attachment.FileName + "\"");	
			System.Web.HttpContext.Current.Response.OutputStream.Write(attachment.Content, 0, attachment.Length);
			System.Web.HttpContext.Current.Response.End();
		}

	}
}
