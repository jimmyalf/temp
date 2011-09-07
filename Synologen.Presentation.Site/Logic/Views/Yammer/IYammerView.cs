using System.Web;
using Spinit.Wpc.Synologen.Presentation.Site.Models.Yammer;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Site.Logic.Views.Yammer
{
    public interface IYammerView : IView<YammerListModel>
    {
        HttpApplicationState State { get; }
        int NumberOfMessages { get; set; }
        string Threaded { get; set; }
        bool ExcludeJoins { get; set; }
    }
}
