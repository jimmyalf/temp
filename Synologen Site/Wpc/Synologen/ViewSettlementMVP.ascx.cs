using Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters.ContractSales;
using Spinit.Wpc.Synologen.Presentation.Site.Models.ContractSales;
using WebFormsMvp;
using WebFormsMvp.Web;

namespace Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen 
{
	[PresenterBinding(typeof(ViewSettlementPresenter))]
	public partial class ViewSettlementMVP : MvpUserControl<ViewSettlementModel> { }
}