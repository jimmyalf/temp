using Spinit.Wpc.Synologen.Presentation.Site.Models;
using Spinit.Wpc.Synologen.Presentation.Site.Presenters;
using WebFormsMvp;
using WebFormsMvp.Web;

namespace Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen 
{
	[PresenterBinding(typeof(MVPTestPresenter))] 
	public partial class MVPTestControl : MvpUserControl<MVPTestModel>{ }
}