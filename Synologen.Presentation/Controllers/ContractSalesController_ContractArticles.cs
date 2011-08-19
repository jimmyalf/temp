using System.Web.Mvc;
using Spinit.Wpc.Synologen.Presentation.Application.Services;
using Spinit.Wpc.Synologen.Presentation.Models.ContractSales;

namespace Spinit.Wpc.Synologen.Presentation.Controllers
{
	public partial class ContractSalesController
	{
		[HttpGet]
		public ActionResult AddContractArticle(int contractId)
		{
			var viewModel = _viewService.GetAddContractArticleView(contractId);
			return View(viewModel);
		}

		[HttpGet]
		public ActionResult GetArticle(int articleId, string format)
		{
			var article = _viewService.GetArticle(articleId);
			if (format == "json") return Json(article, JsonRequestBehavior.AllowGet);
			return View(article);
		}

		[HttpPost]
		public ActionResult AddContractArticle(AddContractArticleView addContractArticleView)
		{
			if(!ModelState.IsValid)
			{
				var viewModel = _viewService.UpdateContractArticleView(addContractArticleView, (selectedArticle, postedView) => postedView.SPCSAccountNumber);
				return View(viewModel);
			}
			var redirectUrl = _viewService.GetContractArticleRoute(addContractArticleView.ContractId);
			var contractArticle = _viewService.ParseContractArticle(addContractArticleView);
			_contractSalesCommandService.AddContractArticle(contractArticle);
			MessageQueue.SetMessage("En ny artikel har kopplats till avtalet");
			return Redirect(redirectUrl);
		}

		[HttpGet]
		public ActionResult EditContractArticle(int contractArticleId)
		{
			var viewModel = _viewService.GetEditContractArticleView(contractArticleId);
			return View(viewModel);
		}

		[HttpPost]
		public ActionResult EditContractArticle(EditContractArticleView contractArticleView)
		{
			if(!ModelState.IsValid)
			{
				var viewModel = _viewService.UpdateContractArticleView(contractArticleView);
				return View(viewModel);
			}
			var contractArticle = _viewService.ParseContractArticle(contractArticleView);
			_contractSalesCommandService.UpdateContractArticle(contractArticle);
			var redirectUrl = _viewService.GetContractArticleRoute(contractArticle.ContractCustomerId);
			MessageQueue.SetMessage("Avtalsartikeln har uppdaterats");
			return Redirect(redirectUrl);
		}
	}


}
