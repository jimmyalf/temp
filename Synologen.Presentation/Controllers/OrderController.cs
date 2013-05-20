using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using NHibernate;
using NHibernate.Criterion;
using Spinit.Data;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Data.Extensions;
using Spinit.Wpc.Synologen.Presentation.Application;
using Spinit.Wpc.Synologen.Presentation.Application.Services;
using Spinit.Wpc.Synologen.Presentation.Helpers;
using Spinit.Wpc.Synologen.Presentation.Helpers.Extensions;
using Spinit.Wpc.Synologen.Presentation.Models.Order;
using Order = Spinit.Wpc.Synologen.Core.Domain.Model.Orders.Order;

namespace Spinit.Wpc.Synologen.Presentation.Controllers
{
	public class OrderController : BaseController
	{
		private readonly IOrderRepository _orderRepository;
		//private readonly IAdminSettingsService _adminSettingsService;
		private readonly IArticleCategoryRepository _articleCategoryRepository;
		private readonly IArticleSupplierRepository _articleSupplierRepository;
		private readonly IArticleTypeRepository _articleTypeRepository;
		private readonly IArticleRepository _articleRepository;
		private readonly IOrderViewParser _orderViewParser;

		public OrderController(
			IOrderRepository orderRepository, 
			IAdminSettingsService adminSettingsService, 
			IArticleCategoryRepository articleCategoryRepository,
			IArticleSupplierRepository articleSupplierRepository,
			IArticleTypeRepository articleTypeRepository,
			IArticleRepository articleRepository,
			IOrderViewParser orderViewParser,
			ISession session) : base(session, adminSettingsService)
		{
			_orderRepository = orderRepository;
			//_adminSettingsService = adminSettingsService;
			_articleCategoryRepository = articleCategoryRepository;
			_articleSupplierRepository = articleSupplierRepository;
			_articleTypeRepository = articleTypeRepository;
			_articleRepository = articleRepository;
			_orderViewParser = orderViewParser;
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

		[HttpGet]
		public ActionResult OrderView(int id)
		{
			var order = _orderRepository.Get(id);
			var viewModel = new OrderView(order);
			return View(viewModel);
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

		[HttpPost]
		public ActionResult DeleteCategory(int id)
		{
			var anyArticleTypeWithCategory = _articleTypeRepository.FindBy(new AllArticleTypesWithCategoryCriteria(id)).Any();
			if(anyArticleTypeWithCategory)
			{
				this.AddErrorMessage("Artikelkategorin kunde inte raderas då den är knuten till en eller flera artikeltyper");
				return RedirectToAction("Categories");
			}
			var category = _articleCategoryRepository.Get(id);
			_articleCategoryRepository.Delete(category);

			this.AddSuccessMessage("Artikelkategorin har raderats");
			return RedirectToAction("Categories");
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
			if (!ModelState.IsValid)
			{
				return View(viewModel.SetArticleCategories(_articleCategoryRepository.GetAll()));
			}
			var articleType = _orderViewParser.GetEntity(viewModel, _articleTypeRepository.Get, _articleCategoryRepository.Get);
			_articleTypeRepository.Save(articleType);
			return Redirect("ArticleTypes");
		}

		[HttpPost]
		public ActionResult DeleteArticleType(int id)
		{
			var anyArticleWithArticleType = _articleRepository.FindBy(new AllArticlesWithArticleTypeCriteria(id)).Any();
			if(anyArticleWithArticleType)
			{
				this.AddErrorMessage("Artikeltypen kunde inte raderas då den är knuten till en eller flera artiklar");
				return RedirectToAction("ArticleTypes");
			}
			var articleType = _articleTypeRepository.Get(id);
			_articleTypeRepository.Delete(articleType);

			this.AddSuccessMessage("Artikeltypen har raderats");
			return RedirectToAction("ArticleTypes");
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

		[HttpGet]
		public ActionResult SupplierForm(int? id = null)
		{
			var viewModel = _orderViewParser.GetSupplierFormView(id, _articleSupplierRepository.Get);
			return View(viewModel);
		}

		[HttpPost]
		public ActionResult SupplierForm(SupplierFormView viewModel)
		{
			if (!ModelState.IsValid)
			{
				return View(viewModel);
			}
			var supplier = _orderViewParser.GetEntity(viewModel, _articleSupplierRepository.Get);
			_articleSupplierRepository.Save(supplier);
			return Redirect("Suppliers");
		}

		[HttpPost]
		public ActionResult DeleteSupplier(int id)
		{
			var anyArticleWithSupplier = _articleRepository.FindBy(new AllArticlesWithSupplierCriteria(id)).Any();
			if(anyArticleWithSupplier)
			{
				this.AddErrorMessage("Leverantören kunde inte raderas då den är knuten till en eller flera artiklar");
				return RedirectToAction("Suppliers");
			}
			var supplier = _articleSupplierRepository.Get(id);
			_articleSupplierRepository.Delete(supplier);

			this.AddSuccessMessage("Leverantören har raderats");
			return RedirectToAction("Suppliers");
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

		[HttpGet]
		public ActionResult ArticleForm(int? id = null)
		{
			var viewModel = _orderViewParser.GetArticleFormView(id, _articleRepository.Get, _articleSupplierRepository.GetAll, _articleTypeRepository.GetAll);
			return View(viewModel);
		}

		[HttpPost]
		public ActionResult ArticleForm(ArticleFormView viewModel)
		{
			if (!ModelState.IsValid || viewModel.HasCustomValidationErrors)
			{
				var validationError = viewModel.GetCustomValidationErrors();
				ModelState.AddCustomValidationErrors(validationError);
				return View(viewModel
					.SetSuppliers(_articleSupplierRepository.GetAll())
					.SetTypes(_articleTypeRepository.GetAll())
				);
			}
			var article = _orderViewParser.GetEntity(viewModel, _articleRepository.Get, _articleSupplierRepository.Get, _articleTypeRepository.Get);
			_articleRepository.Save(article);
			return Redirect("Articles");
		}

		[HttpPost]
		public ActionResult DeleteArticle(int id)
		{
			var anyOrdersWithArticle = _orderRepository.FindBy(new AllOrdersWithArticleCriteria(id)).Any();
			if(anyOrdersWithArticle)
			{
				this.AddErrorMessage("Artikeln kunde inte raderas då den är knuten till en eller flera beställningar");
				return RedirectToAction("Articles");
			}
			var article = _articleRepository.Get(id);
			_articleRepository.Delete(article);

			this.AddSuccessMessage("Artikeln har raderats");
			return RedirectToAction("Articles");
		}

		#endregion

		#region Subscriptions

		[HttpGet]
		public ActionResult Subscriptions(GridPageSortParameters pageSortParameters, string search = null)
		{
			var decodedSearchTerm = search.UrlDecode();
			var query = GetPagedSortedQuery<Subscription>(pageSortParameters, criteria => criteria
				.CreateAlias(x => x.Customer)
				.CreateAlias(x => x.Shop)
				.SynologenFilterByAny(filter =>
				{
					filter.IfInt(decodedSearchTerm, parsedValue => filter.Equal(x => x.Id, parsedValue));
					filter.IfInt(decodedSearchTerm, parsedValue => filter.Equal(x => x.AutogiroPayerId, parsedValue));
					filter.Like(x => x.Shop.Name);
					filter.Like(x => x.Customer.FirstName);
					filter.Like(x => x.Customer.LastName);
					filter.Like(x => x.BankAccountNumber, matchMode: MatchMode.Start);
					filter.Like(func => func.Concat(x => x.Customer.FirstName).And(" ").And(x => x.Customer.LastName));

				}, decodedSearchTerm)
			);
			var suppliers = Query(query);
		 	var viewModel = new SubscriptionListView(decodedSearchTerm, suppliers);
			return View(viewModel);
		}

		[HttpPost]
		public ActionResult Subscriptions(SubscriptionListView viewModel)
		{
			var routeValues = GetRouteValuesWithSearch(viewModel.SearchTerm);
			return RedirectToAction("Subscriptions", routeValues);
		}

		[HttpGet]
		public ActionResult SubscriptionView(int id)
		{
			var subscription = WithSession(session => session
			    .CreateCriteriaOf<Subscription>()
			    .FilterEqual(x => x.Id, id)
			    .SetFetchMode(x => x.Customer, FetchMode.Join)
			    .SetFetchMode(x => x.Shop, FetchMode.Join)
			    .UniqueResult<Subscription>()
			);
			var viewModel = new SubscriptionView(subscription);
			return View(viewModel);
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
			criteria.PageSize = pageSortParameters.PageSize ?? DefaultPageSize;
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

		//private IExtendedEnumerable<TType> GetPagedSortedItems<TType>(GridPageSortParameters pageSortParameters, string defaultSortColumn = "Id", Func<ICriteria,ICriteria> additionalCriterias = null) 
		//    where TType : class
		//{
		//    var criteria =  WithSession(session => session
		//        .CreateCriteriaOf<TType>()
		//        .Page(pageSortParameters.Page, pageSortParameters.PageSize ?? _defaultPageSize)
		//        .Sort(pageSortParameters.Column ?? defaultSortColumn, pageSortParameters.Direction == SortDirection.Ascending));
		//    if(additionalCriterias != null)
		//    {
		//        criteria =  additionalCriterias(criteria);
		//    }
		//    return criteria.List<TType>();

		//    //.SetFetchMode("Customer", FetchMode.Join)
		//    //.SetFetchMode("Shop", FetchMode.Join)
		//    //.List<Subscription>()
		//}
	}
}