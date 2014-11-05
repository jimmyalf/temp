using System;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using Spinit.Wpc.Forum.Components;
using Spinit.Wpc.Forum.Enumerations;

namespace Spinit.Wpc.Forum.Controls {

    public class ForumImageButton : HtmlAnchor {

        ForumContext forumContext = ForumContext.Current;
        ForumButtonType buttonType = ForumButtonType.NewPost;
        Post post;

        protected override void Render(HtmlTextWriter writer) {
            Spinit.Wpc.Forum.Components.Forum forum = null;

            // Are we looking up the forum by forum id or post id
            //
            if (forumContext.ForumID > 0)
                forum = Forums.GetForum(forumContext.ForumID);
            else if (forumContext.PostID > 0)
                forum = Forums.GetForumByPostID(forumContext.PostID);
            else
                return;

            ForumPermission forumPermission = forum.Permission;

            string url = string.Empty;
            string imagePath = string.Empty;

            // What type of button are we displaying?
            //
            switch (ButtonType) {

                    // New Thread
                case ForumButtonType.NewPost:

                    try {
                        ForumPermission.AccessCheck(forum, Permission.Post, post);
                    } catch (ForumException) {
                        return;
                    }

					// EAD - switched to use the definition "forum" above due to
					// forumContext() is NOT set at the PostView level and we needed
					// the ForumID for creating new posts.
					//
                    //url = Globals.GetSiteUrls().PostCreate(forumContext.ForumID);
					url = Globals.GetSiteUrls().PostCreate(forum.ForumID);
                    imagePath = Globals.GetSkinPath() + "/images/newtopic.gif";
                    break;

                // Reply
                case ForumButtonType.Reply:

                    // If this is a locked post we display the special locked icon
                    //
                    if (post.IsLocked) {
                        writer.AddAttribute(HtmlTextWriterAttribute.Src, Globals.GetSkinPath() + "/images/post_button_locked.gif");
                        writer.RenderBeginTag(HtmlTextWriterTag.Img);

                        writer.RenderEndTag();

                        return;
                    }

                    try {
                        ForumPermission.AccessCheck(forum, Permission.Reply, post);
                    } catch (ForumException) {
                        return;
                    }

                    url = Globals.GetSiteUrls().PostReply(post.PostID);
                    imagePath = Globals.GetSkinPath() + "/images/newpost.gif";
                    break;
                    
                // Quote
                case ForumButtonType.Quote:

                    try {
                        ForumPermission.AccessCheck(forum, Permission.Reply, post);
                    } catch (ForumException) {
                        return;
                    }

                    url = Globals.GetSiteUrls().PostReply(post.PostID, true);
                    imagePath = Globals.GetSkinPath() + "/images/quote.gif";
                    break;

                // Delete
                case ForumButtonType.Delete:

                    try {
                        ForumPermission.AccessCheck(forum, Permission.Delete, post);
                    } catch (ForumException) {
                        return;
                    }

                    url = Globals.GetSiteUrls().PostDelete(post.PostID, HttpUtility.UrlEncode(Globals.GetSiteUrls().Forum(post.ForumID)));
                    imagePath = Globals.GetSkinPath() + "/images/post_button_delete.gif";
                    break;

                // Edit
                case ForumButtonType.Edit:

                    try {
                        ForumPermission.AccessCheck(forum, Permission.Edit, post);
                    } catch (ForumException) {
                        return;
                    }

                    url = Globals.GetSiteUrls().PostEdit(post.PostID, HttpUtility.UrlEncode(HttpContext.Current.Request.Url.PathAndQuery));
                    imagePath = Globals.GetSkinPath() + "/images/post_button_edit.gif";
                    break;

            }

            writer.AddAttribute(HtmlTextWriterAttribute.Href, url);
            writer.RenderBeginTag(HtmlTextWriterTag.A);

            writer.AddAttribute(HtmlTextWriterAttribute.Src, imagePath);
            writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
            writer.RenderBeginTag(HtmlTextWriterTag.Img);

            writer.RenderEndTag();
            writer.RenderEndTag();

        }

        public ForumButtonType ButtonType {

            get {
                return buttonType;
            }
            set {
                buttonType = value;
            }

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

    public enum ForumButtonType {
        NewPost,
        Reply,
        Quote,
        Delete,
        Edit
    }
}
