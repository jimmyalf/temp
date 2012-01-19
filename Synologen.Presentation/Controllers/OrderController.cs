using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Spinit.Data;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Application.Services;
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
		private readonly IArticleRepository _articleRepository;
		private readonly IOrderViewParser _orderViewParser;
		private readonly int _defaultPageSize;

		public OrderController(
			IOrderRepository orderRepository, 
			IAdminSettingsService adminSettingsService, 
			IArticleCategoryRepository articleCategoryRepository,
			IArticleSupplierRepository articleSupplierRepository,
			IArticleTypeRepository articleTypeRepository,
			IArticleRepository articleRepository,
			IOrderViewParser orderViewParser)
		{
			_orderRepository = orderRepository;
			_adminSettingsService = adminSettingsService;
			_articleCategoryRepository = articleCategoryRepository;
			_articleSupplierRepository = articleSupplierRepository;
			_articleTypeRepository = articleTypeRepository;
			_articleRepository = articleRepository;
			_orderViewParser = orderViewParser;
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

		[HttpGet]
		public ActionResult CategoryForm(int? id = null)
		{
			var viewModel = _orderViewParser.GetCategoryFormView(id, _articleCategoryRepository.Get);
			return View(viewModel);
		}

		[HttpPost]
		public ActionResult CategoryForm(CategoryFormView viewModel)
		{
			if (!ModelState.IsValid) return View(viewModel);
			var category = _orderViewParser.GetEntity(viewModel, _articleCategoryRepository.Get);
			_articleCategoryRepository.Save(category);
			return Redirect("Categories");
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

		[HttpGet]
		public ActionResult ArticleTypeForm(int? id = null)
		{
			var viewModel = _orderViewParser.GetArticleTypeFormView(id, _articleTypeRepository.Get, _articleCategoryRepository.GetAll);
			return View(viewModel);
		}

		[HttpPost]
		public ActionResult ArticleTypeForm(ArticleTypeFormView viewModel)
		{
			if (!ModelState.IsValid) return View(viewModel);
			var articleType = _orderViewParser.GetEntity(viewModel, _articleTypeRepository.Get, _articleCategoryRepository.Get);
			_articleTypeRepository.Save(articleType);
			return Redirect("ArticleTypes");
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

		public ActionResult SupplierForm(int? id = null)
		{
			return View();
		}

		#endregion

		#region Articles

		[HttpGet]
		public ActionResult Articles(GridPageSortParameters pageSortParameters, string search = null)
		{
			var decodedSearchTerm = search.UrlDecode();
			var articleTypes = GetItemsByCriteria<Article, PageOfArticlesMatchingCriteria>(_articleRepository, pageSortParameters, decodedSearchTerm);
		 	var viewModel = new ArticleListView(decodedSearchTerm, articleTypes);
			return View(viewModel);
		}

		[HttpPost]
		public ActionResult Articles(ArticleListView viewModel)
		{
			var routeValues = GetRouteValuesWithSearch(viewModel.SearchTerm);
			return RedirectToAction("Articles", routeValues);
		}

		public ActionResult ArticleForm(int? id = null)
		{
			return View();
		}

		#endregion

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