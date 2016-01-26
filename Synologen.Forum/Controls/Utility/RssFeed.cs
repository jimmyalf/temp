using System;
using System.Web;
using System.Web.UI;
using System.IO;
using Spinit.Wpc.Forum.Components;
using Spinit.Wpc.Forum.Enumerations;

namespace Spinit.Wpc.Forum.Controls {

    public class RssFeed : Control {
        ForumContext forumContext = ForumContext.Current;

        protected override void Render(HtmlTextWriter writer) {

            if (!Globals.GetSiteSettings().EnableRSS) {
                writer.Write("Error - RSS Is Not Enabled.");
                return;
            }

            int count = -1;
            ThreadViewMode mode = ThreadViewMode.Default;

            // Do we have a mode argument?
            if (forumContext.Context.Request.QueryString["mode"] != null) {
                mode = (ThreadViewMode) int.Parse(forumContext.Context.Request.QueryString["mode"]);
            }

            // Do we have a count?
            if (forumContext.Context.Request.QueryString["count"] != null) {
                count = int.Parse(forumContext.Context.Request.QueryString["count"]);
            }

            // Ensure we don't return anything for < 1
            if (forumContext.ForumID == 0) {
                writer.Write("Error - Unable to generate RSS: Invalid Forum ID.");
                return;
            }

            // Write the feed
            try {
                if (count > 0)
                    writer.Write( Rss.GetForumRss(forumContext.ForumID, mode, count));
                else
                    writer.Write( Rss.GetForumRss(forumContext.ForumID, mode));
            } catch {
                writer.Write("Error - Unable to generate RSS: Invalid Forum ID.");
            }

        }

    }

}
