using System;
using System.Web.Mvc;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Application.Services;
using Spinit.Wpc.Synologen.Presentation.Helpers;
using Spinit.Wpc.Synologen.Presentation.Helpers.Extensions;
using Spinit.Wpc.Synologen.Presentation.Models.LensSubscription;

namespace Spinit.Wpc.Synologen.Presentation.Controllers
{
	public class LensSubscriptionController : Controller
	{
		private readonly ILensSubscriptionViewService _lensSubscriptionViewService;
		private readonly int DefaultPageSize;

		public LensSubscriptionController(ILensSubscriptionViewService lensSubscriptionViewService, IAdminSettingsService adminSettingsService)
		{
			DefaultPageSize = adminSettingsService.GetDefaultPageSize();
			_lensSubscriptionViewService = lensSubscriptionViewService;
		}

		[HttpGet]
		public ActionResult Index(string search, GridPageSortParameters pageSortParameters)
		{
			var decodedSearchTerm = search.UrlDecode();
			var criteria = new PageOfSubscriptionsMatchingCriteria(decodedSearchTerm)
			{
				OrderBy = pageSortParameters.Column,
                Page = pageSortParameters.Page,
                PageSize = pageSortParameters.PageSize ?? DefaultPageSize,
                SortAscending = pageSortParameters.Direction == SortDirection.Ascending
			};
			var model = new SubscriptionListView { List = _lensSubscriptionViewService.GetSubscriptions(criteria), SearchTerm = decodedSearchTerm};
			
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Index(SubscriptionListView inModel)
		{
			var routeValues = ControllerContext.HttpContext.Request.QueryString
				.ToRouteValueDictionary()
				.BlackList("controller", "action");
			if(String.IsNullOrEmpty(inModel.SearchTerm))
			{
			    routeValues.TryRemoveRouteValue("search");
			}
			else
			{
			    routeValues.AddOrReplaceRouteValue("search", inModel.SearchTerm.UrlEncode());
			}
			return RedirectToAction("Index", routeValues);
		}

		[HttpGet]
		public ActionResult ViewSubscription(int id)
		{
			var viewModel = _lensSubscriptionViewService.GetSubscription(id);
			return View(viewModel);
		}

		[HttpGet]
		public ActionResult TransactionArticles(string search, GridPageSortParameters pageSortParameters) 
		{
			var decodedSearchTerm = search.UrlDecode();
			var criteria = new PageOfTransactionArticlesMatchingCriteria(decodedSearchTerm)
			{
				OrderBy = pageSortParameters.Column,
                Page = pageSortParameters.Page,
                PageSize = pageSortParameters.PageSize ?? DefaultPageSize,
                SortAscending = pageSortParameters.Direction == SortDirection.Ascending
			};
			var viewModel = new TransactionArticleListView { Articles = _lensSubscriptionViewService.GetTransactionArticles(criteria), SearchTerm = decodedSearchTerm };
			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult TransactionArticles(TransactionArticleListView inModel)
		{
			var routeValues = ControllerContext.HttpContext.Request.QueryString
				.ToRouteValueDictionary()
				.BlackList("controller", "action");
			if(String.IsNullOrEmpty(inModel.SearchTerm))
			{
			    routeValues.TryRemoveRouteValue("search");
			}
			else
			{
			    routeValues.AddOrReplaceRouteValue("search", inModel.SearchTerm.UrlEncode());
			}
			return RedirectToAction("TransactionArticles", routeValues);
		}
	}
}