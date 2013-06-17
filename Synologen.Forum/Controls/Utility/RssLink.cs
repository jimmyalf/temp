using System;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using Spinit.Wpc.Forum.Components;
using Spinit.Wpc.Forum.Enumerations;

namespace Spinit.Wpc.Forum.Controls {

    public class RssLink : HtmlAnchor {
        ForumContext forumContext = ForumContext.Current;
        ThreadViewMode mode = ThreadViewMode.Default;

        protected override void CreateChildControls() {

            if (!Globals.GetSiteSettings().EnableRSS)
                return;
				

			// TODO (EAD): Need to add forum-based permissions to allow
			// certain forums to provide RSS feeds, and others explicitly deny.
			//

            // Are we in default mode without a forumid?
			//
            if ( (mode == ThreadViewMode.Default) && (forumContext.ForumID < 1) )
                return;

            HtmlImage image = new HtmlImage();

			image.Alt = ResourceManager.GetString("ViewThreads_XML");
            image.Src = Globals.GetSkinPath() + "/images/xml.gif";
            image.Border = 0;

            base.HRef = Globals.GetSiteUrls().RssForum( forumContext.ForumID, mode );

            Controls.Add(image);

        }

        public ThreadViewMode Mode {
            get {
                return mode;
            }
            set {
                mode = value;
            }
        }
    }
}


