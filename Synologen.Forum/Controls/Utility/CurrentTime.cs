using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Spinit.Wpc.Forum.Components;

namespace Spinit.Wpc.Forum.Controls {

    // *********************************************************************
    //  StyleSkin
    //
    /// <summary>
    /// Encapsulated rendering of style based on the selected skin.
    /// </summary>
    // ********************************************************************/ 
    public class CurrentTime : Literal {

        string format = Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_CurrentTime_format");
        string dateFormat = Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_CurrentTime_dateFormat");
        DateTime time;

        protected override void Render (HtmlTextWriter writer) {

            if (!Globals.GetSiteSettings().EnableCurrentTime)
                return;

            // Set the time to the user's time
            //
            if (HttpContext.Current.Request.IsAuthenticated) {
                time = DateTime.Now.AddHours(Users.GetUser().Timezone - Globals.GetSiteSettings().TimezoneOffset);
                writer.Write( Format, time.ToString(dateFormat), Users.GetUser().Timezone.ToString() );
            } else {
                time = DateTime.Now;
                writer.Write( Format, time.ToString(dateFormat), Globals.GetSiteSettings().TimezoneOffset.ToString() );
            }


        }

        public string Format {
            get {
                return format;
            }
            set {
                format = value;
            }
        }

        public string DateFormat {
            get {
                return dateFormat;
            }
            set {
                dateFormat = value;
            }
        }
    }
}
