using System;
using System.Web;
using System.Web.Caching;
using Spinit.Wpc.Forum;

namespace Spinit.Wpc.Forum.Components.HttpHandler {

    public class AvatarHttpHandler : IHttpHandler {

        public void ProcessRequest (HttpContext context) {

            try {
                Avatar userAvatar = Resources.GetAvatar( int.Parse(context.Request.QueryString["UserID"]) );

                context.Response.ContentType = userAvatar.ContentType;
                context.Response.OutputStream.Write(userAvatar.Content, 0, userAvatar.Length);

                context.Response.Cache.SetCacheability(HttpCacheability.Public);
                // Terry Denham 7/16/2004
				// changing default cache for avatars from 1 day to 30 minutes
				context.Response.Cache.SetExpires(DateTime.Now.AddMinutes(30));
                context.Response.Cache.SetAllowResponseInBrowserHistory(true);
                context.Response.Cache.SetValidUntilExpires(true);
                context.Response.Cache.VaryByParams["UserID"] = true;
            } catch {}

        }

        public bool IsReusable {
            get {
                return false;
            }
        }
    }
}
