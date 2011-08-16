using System.Web.Mvc;
using Spinit.Wpc.Synologen.Presentation.Application.Services;
using Spinit.Wpc.Synologen.Presentation.Code;
using Spinit.Wpc.Synologen.Presentation.Models.ContractSales;

namespace Spinit.Wpc.Synologen.Presentation.Controllers
{
	public partial class ContractSalesController
	{
		[HttpGet]
		public ActionResult AddArticle()
		{
			return View(_viewService.SetArticleViewDefaults(new ArticleView(), "Skapa ny artikel"));
		}

		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult AddArticle(ArticleView articleView)
		{
			if (!ModelState.IsValid)
			{
				return View(_viewService.SetArticleViewDefaults(articleView, "Skapa ny artikel"));
			}
			var article = _viewService.ParseArticle(articleView);
			_contractSalesCommandService.AddArticle(article);
			MessageQueue.SetMessage("En ny artikel har sparats");
			return Redirect(ComponentPages.Articles.Replace("~",""));
		}

		[HttpGet]
		public ActionResult EditArticle(int id)
		{
			var articleView = _viewService.GetArticleView(id, "Redigera artikel");
			return View(articleView);
		}

		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult EditArticle(ArticleView articleView)
		{
			if (!ModelState.IsValid)
			{
				return View(_viewService.SetArticleViewDefaults(articleView, "Redigera artikel"));
			}
			var article = _viewService.ParseArticle(articleView);
			_contractSalesCommandService.UpdateArticle(article);
			MessageQueue.SetMessage("Artikel \"{ArticleName}\" har uppdaterats".Replace("{ArticleName}", articleView.Name));
			return Redirect(ComponentPages.Articles.Replace("~",""));
		}
	}
}