using System;
using System.Web.Mvc;
using System.Web.Routing;
using Spinit.Wpc.Core.UI;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Application.Services;
using Spinit.Wpc.Synologen.Presentation.Code;
using Spinit.Wpc.Synologen.Presentation.Helpers.Extensions;
using StructureMap;

namespace Spinit.Wpc.Synologen.Presentation.Controllers
{
	public partial class ContractSalesController : Controller
	{
		private readonly IContractSalesViewService _viewService;
		private readonly IContractSalesCommandService _contractSalesCommandService;

		public ContractSalesController(IContractSalesViewService viewService, IContractSalesCommandService contractSalesCommandService)
		{
			_viewService = viewService;
			_contractSalesCommandService = contractSalesCommandService;
		}

		public ActionResult ViewSettlement(int id)
		{
			var viewModel = _viewService.GetSettlement(id);
			return View(viewModel);
		}

		public ActionResult Settlements()
		{
			var viewModel = _viewService.GetSettlements();
			return View(viewModel);
		}

		[HttpGet]
		public ActionResult ManageOrder(int id)
		{
			var viewModel = _viewService.GetOrder(id);
			return View(viewModel);
		}

		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult CancelOrder(int id)
		{
			try
			{
				_contractSalesCommandService.CancelOrder(id);
				MessageQueue.SetMessage("Fakturan har makulerats");
				return Redirect(ComponentPages.Orders);
			}
			catch(Exception ex)
			{
				this.AddErrorMessage("Ett fel uppstod vid fakturamakulering: " + ex.Message + "<br/>" + ex.StackTrace);
				return RedirectToAction("ManageOrder", new {id});
			}
		}

		[HttpPost, ActionName("Settlements"), ValidateAntiForgeryToken]
		public ActionResult CreateSettlement()
		{
			var settlementId = _viewService.CreateSettlement();
			return RedirectToAction("ViewSettlement", new { id = settlementId });
		}
	}
}