using System;
using System.Web;
using System.Web.UI;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters.Yammer;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views.Yammer;
using Spinit.Wpc.Synologen.Presentation.Site.Models.Yammer;
using WebFormsMvp;
using WebFormsMvp.Web;

namespace Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen.Yammer
{
    [PartialCaching(120)]
    [PresenterBinding(typeof(YammerPresenter))]
    public partial class YammerFeed : MvpUserControl<YammerListModel>, IYammerView
    {
        protected void Page_Load(object sender, EventArgs e) { }

        public HttpApplicationState State { get { return Application; } }
    }
}