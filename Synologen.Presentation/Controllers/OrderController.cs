using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Spinit.Data;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
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
		private readonly IArticleSupplierRepository _articleSupplierRepository;
		private readonly IArticleTypeRepository _articleTypeRepository;
		private readonly int _defaultPageSize;

		public OrderController(
			IOrderRepository orderRepository, 
			IAdminSettingsService adminSettingsService, 
			IArticleCategoryRepository articleCategoryRepository,
			IArticleSupplierRepository articleSupplierRepository,
			IArticleTypeRepository articleTypeRepository)
		{
			_orderRepository = orderRepository;
			_adminSettingsService = adminSettingsService;
			_articleCategoryRepository = articleCategoryRepository;
			_articleSupplierRepository = articleSupplierRepository;
			_articleTypeRepository = articleTypeRepository;
			_defaultPageSize = _adminSettingsService.GetDefaultPageSize();
		}

		#region Orders

		[HttpGet]
		public ActionResult Orders(GridPageSortParameters pageSortParameters, string search = null)
		{
			var decodedSearchTerm = search.UrlDecode();
			var orders = GetItemsByCriteria<Order, PageOfOrdersMatchingCriteria>(_orderRepository, pageSortParameters, decodedSearchTerm);
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

		#endregion

		#region Categories

		[HttpGet]
		public ActionResult Categories(GridPageSortParameters pageSortParameters, string search = null)
		{
			var decodedSearchTerm = search.UrlDecode();
			var categories = GetItemsByCriteria<ArticleCategory, PageOfCategoriesMatchingCriteria>(_articleCategoryRepository, pageSortParameters, decodedSearchTerm);
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

		#endregion

		#region Suppliers

		[HttpGet]
		public ActionResult Suppliers(GridPageSortParameters pageSortParameters, string search = null)
		{
			var decodedSearchTerm = search.UrlDecode();
			var suppliers = GetItemsByCriteria<ArticleSupplier, PageOfSuppliersMatchingCriteria>(_articleSupplierRepository, pageSortParameters, decodedSearchTerm);
		 	var viewModel = new SupplierListView(decodedSearchTerm, suppliers);
			return View(viewModel);
		}

		[HttpPost]
		public ActionResult Suppliers(SupplierListView viewModel)
		{
			var routeValues = GetRouteValuesWithSearch(viewModel.SearchTerm);
			return RedirectToAction("Suppliers", routeValues);
		}

		public ActionResult EditSupplier()
		{
			return View();
		}

		#endregion

		#region ArticleTypes

		[HttpGet]
		public ActionResult ArticleTypes(GridPageSortParameters pageSortParameters, string search = null)
		{
			var decodedSearchTerm = search.UrlDecode();
			var articleTypes = GetItemsByCriteria<ArticleType, PageOfArticleTypesMatchingCriteria>(_articleTypeRepository, pageSortParameters, decodedSearchTerm);
		 	var viewModel = new ArticleTypeListView(decodedSearchTerm, articleTypes);
			return View(viewModel);
		}

		[HttpPost]
		public ActionResult ArticleTypes(ArticleTypeListView viewModel)
		{
			var routeValues = GetRouteValuesWithSearch(viewModel.SearchTerm);
			return RedirectToAction("ArticleTypes", routeValues);
		}

		public ActionResult EditArticleType()
		{
			return View();
		}

		#endregion

		public ActionResult Articles()
		{
			return View();
		}

		private IEnumerable<TType> GetItemsByCriteria<TType,TCriteria>(IReadonlyRepository<TType> repo, GridPageSortParameters pageSortParameters, string search = null ) 
			where TType : class
			where TCriteria : SortedPagedSearchCriteria<TType>
		{
			var criteria = GetCriteria<TCriteria, TType>(search, pageSortParameters);
			return repo.FindBy(criteria);
		}

		private TCriteria GetCriteria<TCriteria,TType>(string search, GridPageSortParameters pageSortParameters) 
			where TType : class
			where TCriteria : SortedPagedSearchCriteria<TType>
		{
			var criteria = Activator.CreateInstance(typeof(TCriteria) ,search) as TCriteria;
			if (criteria == null) throw new ApplicationException("Cannot instantiate criteria of type " + typeof (TCriteria).Name);
			criteria.OrderBy = pageSortParameters.Column;
			criteria.Page = pageSortParameters.Page;
			criteria.PageSize = pageSortParameters.PageSize ?? _defaultPageSize;
			criteria.SortAscending = pageSortParameters.Direction == SortDirection.Ascending;
			return criteria;
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