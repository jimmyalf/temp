using System;
using System.Web.UI.WebControls;
using Spinit.Wpc.Forum.Components;
using Spinit.Wpc.Forum.Enumerations;

namespace Spinit.Wpc.Forum.Controls {

    public class ThreadStatusImage : Image {

        Thread thread;

        protected override void Render(System.Web.UI.HtmlTextWriter writer) {

            switch (thread.Status) {

                case ThreadStatus.Open:
                    ImageUrl = Globals.GetSkinPath() + "/images/status_Open.gif";
                    AlternateText = ResourceManager.GetString("Status_Open");
                    break;

                case ThreadStatus.Resolved:
                    ImageUrl = Globals.GetSkinPath() + "/images/status_Resolved.gif";
                    AlternateText = ResourceManager.GetString("Status_Resolved");
                    break;

                case ThreadStatus.Closed:
                    ImageUrl = Globals.GetSkinPath() + "/images/status_Closed.gif";
                    AlternateText = ResourceManager.GetString("Status_Closed");
                    break;

                default:
                    return;
            }
            base.Render (writer);
        }


        #region Public Properties
        public Thread Thread {
            get {
                return thread;
            }
            set {
                thread = value;
            }
        }
        #endregion


    }
}
