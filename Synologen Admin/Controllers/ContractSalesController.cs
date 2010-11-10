using System.Web.Mvc;
using Spinit.Wpc.Synologen.Presentation.Application.Services;

namespace Spinit.Wpc.Synologen.Presentation.Controllers
{
	public class ContractSalesController : Controller
	{
		private readonly IContractSalesViewService _viewService;

		public ContractSalesController(IContractSalesViewService viewService)
		{
			_viewService = viewService;
		}

		public ActionResult ViewSettlement(int id)
		{
			var viewModel = _viewService.GetSettlement(id);
			return View(viewModel);
		}
	}
}