using System.Web;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.Yammer;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Yammer
{
    public interface IYammerView : IView<YammerListModel>
    {
        HttpApplicationState State { get; }
        int NumberOfMessages { get; set; }
        int NewerThan { get; set; }
        string Threaded { get; set; }
        bool ExcludeJoins { get; set; }
    }
}
