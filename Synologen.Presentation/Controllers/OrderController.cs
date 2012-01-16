using System;
using System.Web.Mvc;
using System.Web.Routing;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Helpers;
using Spinit.Wpc.Synologen.Presentation.Helpers.Extensions;
using Spinit.Wpc.Synologen.Presentation.Models.Order;

namespace Spinit.Wpc.Synologen.Presentation.Controllers
{
	public class OrderController : Controller
	{
		private readonly IOrderRepository _orderRepository;
		private readonly IAdminSettingsService _adminSettingsService;
		private readonly IArticleCategoryRepository _articleCategoryRepository;
		private readonly int _defaultPageSize;

		public OrderController(IOrderRepository orderRepository, IAdminSettingsService adminSettingsService, IArticleCategoryRepository articleCategoryRepository)
		{
			_orderRepository = orderRepository;
			_adminSettingsService = adminSettingsService;
			_articleCategoryRepository = articleCategoryRepository;
			_defaultPageSize = _adminSettingsService.GetDefaultPageSize();
		}

		[HttpGet]
		public ActionResult Orders(GridPageSortParameters pageSortParameters, string search = null)
		{
			var decodedSearchTerm = search.UrlDecode();
			var orders = _orderRepository.FindBy(new PageOfOrdersMatchingCriteria(decodedSearchTerm)
			{
				OrderBy = pageSortParameters.Column,
                Page = pageSortParameters.Page,
                PageSize = pageSortParameters.PageSize ?? _defaultPageSize,
                SortAscending = pageSortParameters.Direction == SortDirection.Ascending
			});
		 	var viewModel = new OrderListView(decodedSearchTerm, orders);
		 	return View(viewModel);
		}

		[HttpPost]
		public ActionResult Orders(OrderListView viewModel)
		{
			var routeValues = GetRouteValuesWithSearch(viewModel.SearchTerm);
			return RedirectToAction("Orders", routeValues);
		}

		public ActionResult EditOrder()
		{
			return View();
		}

		[HttpGet]
		public ActionResult Categories(GridPageSortParameters pageSortParameters, string search = null)
		{
			var decodedSearchTerm = search.UrlDecode();
			var categories = _articleCategoryRepository.FindBy(new PageOfCategoriesMatchingCriteria(decodedSearchTerm)
			{
				OrderBy = pageSortParameters.Column,
                Page = pageSortParameters.Page,
                PageSize = pageSortParameters.PageSize ?? _defaultPageSize,
                SortAscending = pageSortParameters.Direction == SortDirection.Ascending
			});
		 	var viewModel = new CategoryListView(decodedSearchTerm, categories);
			return View(viewModel);
		}

		[HttpPost]
		public ActionResult Categories(CategoryListView viewModel)
		{
			var routeValues = GetRouteValuesWithSearch(viewModel.SearchTerm);
			return RedirectToAction("Categories", routeValues);
		}

		public ActionResult EditCategory()
		{
			return View();
		}

		public ActionResult Suppliers()
		{
			return View();
		}

		public ActionResult ArticleTypes()
		{
			return View();
		}

		public ActionResult Articles()
		{
			return View();
		}


		private RouteValueDictionary GetRouteValuesWithSearch(string searchTerm)
		{
			var routeValues = ControllerContext.HttpContext.Request.QueryString
				.ToRouteValueDictionary()
				.BlackList("controller", "action");
			if(String.IsNullOrEmpty(searchTerm))
			{
			    routeValues.TryRemoveRouteValue("search");
			}
			else
			{
			    routeValues.AddOrReplaceRouteValue("search", searchTerm.UrlEncode());
			}
			return routeValues;
		}
	}
}