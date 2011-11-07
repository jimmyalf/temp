using System;
using System.Web;
using System.Web.UI;
using Spinit.Wpc.Forum.Components;
using System.Web.UI.WebControls;

namespace Spinit.Wpc.Forum.Controls {
    /// <summary>
    /// Summary description for PostAttachment.
    /// </summary>
    public class PostAttachment : WebControl {
        Post post;
        //string attachment = "<b>Attachment</b>: <a href=\"{0}\">{1}</a>";
		string attachment = ResourceManager.GetString("PostFlatView_Attachment") + "<a target=\"_blank\" href=\"{0}\">{1}</a>";

        protected override void Render(HtmlTextWriter writer) {

            if (!post.HasAttachment)
                return;

            writer.RenderBeginTag(HtmlTextWriterTag.Br);
            writer.Write(attachment, Globals.GetSiteUrls().PostAttachment(post.PostID), post.AttachmentFilename);
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
