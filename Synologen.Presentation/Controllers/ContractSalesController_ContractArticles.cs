using System.Web.Mvc;
using Spinit.Wpc.Synologen.Presentation.Application.Services;
using Spinit.Wpc.Synologen.Presentation.Code;
using Spinit.Wpc.Synologen.Presentation.Models.ContractSales;

namespace Spinit.Wpc.Synologen.Presentation.Controllers
{
	public partial class ContractSalesController
	{
		[HttpGet]
		public ActionResult AddContractArticle(int contractId)
		{
			var viewModel = _viewService.GetContractArticleView(contractId);
			return View(viewModel);
		}

		[HttpPost]
		public ActionResult UpdateAddContractArticle(ContractArticleView contractArticleView)
		{
			var viewModel = _viewService.UpdateContractArticleView(contractArticleView, (selectedArticle, postedView) => selectedArticle.SPCSAccountNumber);
			return View("AddContractArticle", viewModel);
		}

		[HttpPost]
		public ActionResult AddContractArticle(ContractArticleView contractArticleView)
		{
			if(!ModelState.IsValid)
			{
				var viewModel = _viewService.UpdateContractArticleView(contractArticleView, (selectedArticle, postedView) => postedView.SPCSAccountNumber);
				return View(viewModel);
			}
			var redirectUrl = "{Url}?contractId={ContractId}"
				.Replace("{Url}", ComponentPages.ContractArticles.Replace("~", ""))
				.Replace("{ContractId}", contractArticleView.ContractId.ToString());
			var contractArticle = _viewService.ParseContractArticle(contractArticleView);
			_contractSalesCommandService.AddContractArticle(contractArticle);
			MessageQueue.SetMessage("En ny artikel har kopplats till avtalet");
			return Redirect(redirectUrl);
		}
	}
}
