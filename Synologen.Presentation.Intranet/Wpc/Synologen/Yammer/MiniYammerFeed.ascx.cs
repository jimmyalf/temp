using System;
using System.Web;
using System.Web.UI;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Yammer;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Yammer;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.Yammer;
using WebFormsMvp;
using WebFormsMvp.Web;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Yammer
{
    [PartialCaching(900)]
    [PresenterBinding(typeof(YammerPresenter))]
    public partial class MiniYammerFeed : MvpUserControl<YammerListModel>, IYammerView
    {
        public HttpApplicationState State { get { return Application; } }

        public int NumberOfMessages { get; set; }
        public int NewerThan { get; set; }
        public bool ExcludeJoins { get; set; }
        public string Threaded { get; set; }
    }
}