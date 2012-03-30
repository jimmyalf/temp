using System;
using System.Linq;
using System.Web.Mvc;
using NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Data.Commands.Finance;
using Spinit.Wpc.Synologen.Data.Queries;
using Spinit.Wpc.Synologen.Data.Queries.Finance;
using Spinit.Wpc.Synologen.Presentation.Application.Services;
using Spinit.Wpc.Synologen.Presentation.Code;
using Spinit.Wpc.Synologen.Presentation.Helpers.Extensions;
using Spinit.Wpc.Synologen.Presentation.Models.ContractSales;

namespace Spinit.Wpc.Synologen.Presentation.Controllers
{
	public partial class ContractSalesController : BaseController
	{
		private readonly IContractSalesViewService _viewService;
		private readonly IContractSalesCommandService _contractSalesCommandService;
		private readonly IAdminSettingsService _settingsService;

		public ContractSalesController(
			IContractSalesViewService viewService, 
			IContractSalesCommandService contractSalesCommandService,
			IAdminSettingsService settingsService,
			ISession session) : base(session)
		{
			_viewService = viewService;
			_contractSalesCommandService = contractSalesCommandService;
			_settingsService = settingsService;
		}

		public ActionResult ViewSettlement(int id)
		{
			var viewModel = _viewService.GetSettlement(id);
			return View(viewModel);
		}

		public ActionResult Settlements()
		{
			var settlements = Query(new All<Settlement>());
			var contractSaleStatusReadyForSettlement = _settingsService.GetContractSalesReadyForSettlementStatus();
			var contractSalesReadyForSettlement = Query(new ContractSalesWithStatus(contractSaleStatusReadyForSettlement));
			var viewModel = new SettlementListView(settlements)
			{
			    NumberOfContractSalesReadyForInvocing = contractSalesReadyForSettlement.Count(),
			    NumberOfOldTransactionsReadyForInvocing = Query(new GetOldAutogiroTransactionsReadyForSettlement()).Count(),
				NumberOfNewTransactionsReadyForInvocing = Query(new GetNewAutogiroTransactionsReadyForSettlement()).Count()
			};
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
			Execute(new SettleOldAutogiroTransactions(settlementId));
			Execute(new SettleNewAutogiroTransactions(settlementId));
			return RedirectToAction("ViewSettlement", new { id = settlementId });
		}
	}
}