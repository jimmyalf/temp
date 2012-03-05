using System;
using System.Linq;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Services;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.Orders;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders
{
    public class CreateOrderPresenter : Presenter<ICreateOrderView>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IRoutingService _routingService;
    	private readonly IArticleCategoryRepository _articleCategoryRepository;
    	private readonly IViewParser _viewParser;
    	private readonly IArticleSupplierRepository _articleSupplierRepository;
    	private readonly IArticleTypeRepository _articleTypeRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;
    	private readonly IOrderCustomerRepository _orderCustomerRepository;
        private readonly IArticleRepository _articleRepository;
        private readonly ILensRecipeRepository _lensRecipeRepository;
    	private readonly IShopRepository _shopRepository;
    	private readonly ISynologenMemberService _synologenMemberService;
    	private int DefaultEyeParameterValue = CreateOrderModel.DefaultOptionValue;
		private int DefaultSelectionValue = 0;

        public CreateOrderPresenter(
			ICreateOrderView view, 
			IOrderRepository orderRepository, 
			IOrderCustomerRepository orderCustomerRepository, 
			IRoutingService routingService, 
			IArticleCategoryRepository articleCategoryRepository, 
			IViewParser viewParser, 
			IArticleSupplierRepository articleSupplierRepository, 
			IArticleTypeRepository articleTypeRepository, 
			IArticleRepository articleRepository, 
			ILensRecipeRepository lensRecipeRepository, 
			IShopRepository shopRepository, 
			ISubscriptionRepository subscriptionRepository, 
			ISynologenMemberService synologenMemberService) : base(view)
        {
            _orderCustomerRepository = orderCustomerRepository;
            _routingService = routingService;
        	_articleCategoryRepository = articleCategoryRepository;
        	_viewParser = viewParser;
        	_articleSupplierRepository = articleSupplierRepository;
        	_articleTypeRepository = articleTypeRepository;
            _subscriptionRepository = subscriptionRepository;
        	_orderRepository = orderRepository;
            _articleRepository = articleRepository;
            _lensRecipeRepository = lensRecipeRepository;
        	_shopRepository = shopRepository;
        	_synologenMemberService = synologenMemberService;
        	WireupEvents();
        }

		private void WireupEvents()
		{
        	View.Load += View_Load;
            View.Submit += View_Submit;
            View.Abort += View_Abort;
            View.Previous += View_Previous;
            View.SelectedCategory += FillModel;
            View.SelectedArticleType += FillModel;
            View.SelectedSupplier += FillModel;
            View.SelectedArticle += FillModel;
			View.SelectedOnlyOneEye += FillModel;
		}

        public void View_Load(object o, EventArgs eventArgs)
        {
            if(RequestOrderId.HasValue)
            {
                var order = _orderRepository.Get(RequestOrderId.Value);
                var args = new OrderChangedEventArgs
                {
                    
                    SelectedArticleTypeId = order.LensRecipe.ArticleType.Id,
                    SelectedCategoryId = order.LensRecipe.ArticleCategory.Id,
					SelectedSupplierId = order.LensRecipe.ArticleSupplier.Id,
                    SelectedShippingOption = (int)order.ShippingType,

					SelectedArticleId = new EyeParameter<int?>
					{
						Left = order.LensRecipe.Article.With(x => x.Left).Return(x => x.Id, (int?) null),
						Right = order.LensRecipe.Article.With(x => x.Right).Return(x => x.Id, (int?) null),
					},
					SelectedPower = order.LensRecipe.Power ?? new EyeParameter<string>(),
                    SelectedAddition = order.LensRecipe.Addition ?? new EyeParameter<string>(),
					SelectedAxis = order.LensRecipe.Axis ?? new EyeParameter<string>(),
					SelectedCylinder = order.LensRecipe.Cylinder ?? new EyeParameter<string>(),
					SelectedQuantity = order.LensRecipe.Quantity ?? new EyeParameter<string>(),
                    SelectedBaseCurve = order.LensRecipe.BaseCurve ?? new EyeParameter<decimal?>(),
                    SelectedDiameter = order.LensRecipe.Diameter ?? new EyeParameter<decimal?>(),
                    SelectedReference = order.Reference,
					OnlyUse = new EyeParameter<bool>
					{
						Left = order.LensRecipe.Article.Right == null,
						Right = order.LensRecipe.Article.Left == null
					}
                };

				View.Model.CustomerName = order.Customer.ParseName(x => x.FirstName, x => x.LastName);
                FillModel(this, args);
            }

            var categories = _articleCategoryRepository.GetAll().Where(x => x.Active);
            var parsedCategories = _viewParser.ParseWithDefaultItem(categories, category => new ListItem(category.Name, category.Id));
            View.Model.Categories = parsedCategories; 

			if(RequestCustomerId.HasValue)
			{
				var customer = _orderCustomerRepository.Get(RequestCustomerId.Value);
				View.Model.CustomerName = customer.ParseName(x => x.FirstName, x => x.LastName);
			}
        }

    	public void FillModel(object sender, OrderChangedEventArgs args)
    	{
    		FillModelBasedOnCategory(args);
    		FillModelBasedOnArticleType(args);
    		FillModelBasedOnSupplier(args);
    		FillModelBasedOnArticleLeft(args);
			FillModelBasedOnArticleRight(args);

            View.Model.SelectedCategoryId = args.SelectedCategoryId ?? DefaultSelectionValue;
            View.Model.SelectedArticleTypeId = args.SelectedArticleTypeId ?? DefaultSelectionValue;
            View.Model.SelectedSupplierId = args.SelectedSupplierId ?? DefaultSelectionValue;
			View.Model.SelectedShippingOption = args.SelectedShippingOption ?? DefaultSelectionValue;
    		View.Model.SelectedArticleId = GetValuesOrDefault(args.SelectedArticleId, DefaultSelectionValue);

            View.Model.SelectedPower = args.SelectedPower;
        	View.Model.SelectedBaseCurve = GetValuesOrDefault(args.SelectedBaseCurve, DefaultEyeParameterValue);
        	View.Model.SelectedDiameter = GetValuesOrDefault(args.SelectedDiameter, DefaultEyeParameterValue);
            View.Model.SelectedCylinder = args.SelectedCylinder;
            View.Model.SelectedAxis = args.SelectedAxis;
            View.Model.SelectedAddition = args.SelectedAddition;
        	View.Model.Reference = args.SelectedReference;
        	View.Model.Quantity = args.SelectedQuantity;
        	View.Model.OnlyUse = args.OnlyUse;
        }

    	private void FillModelBasedOnArticleLeft(OrderChangedEventArgs args)
    	{
    		if (!args.SelectedArticleId.Left.HasValue) return;
    		var articleLeft = _articleRepository.Get(args.SelectedArticleId.Left.Value);
    		if (articleLeft.Options == null) return;
    		View.Model.DiameterOptions.Left = _viewParser.FillWithIncrementalValues(articleLeft.Options.Diameter);
    		View.Model.BaseCurveOptions.Left = _viewParser.FillWithIncrementalValues(articleLeft.Options.BaseCurve);
    		View.Model.AxisOptionsEnabled.Left = articleLeft.Options.EnableAxis;
    		View.Model.CylinderOptionsEnabled.Left = articleLeft.Options.EnableCylinder;
    		View.Model.AdditionOptionsEnabled.Left = articleLeft.Options.EnableAddition;
    	}

    	private void FillModelBasedOnArticleRight(OrderChangedEventArgs args)
    	{
    		if (!args.SelectedArticleId.Right.HasValue) return;
    		var articleRight = _articleRepository.Get(args.SelectedArticleId.Right.Value);
    		if (articleRight.Options == null) return;
    		View.Model.DiameterOptions.Right = _viewParser.FillWithIncrementalValues(articleRight.Options.Diameter);
    		View.Model.BaseCurveOptions.Right = _viewParser.FillWithIncrementalValues(articleRight.Options.BaseCurve);
    		View.Model.AxisOptionsEnabled.Right = articleRight.Options.EnableAxis;
    		View.Model.CylinderOptionsEnabled.Right = articleRight.Options.EnableCylinder;
    		View.Model.AdditionOptionsEnabled.Right = articleRight.Options.EnableAddition;
    	}

		private void FillModelBasedOnSupplier(OrderChangedEventArgs args)
		{
			if (!args.SelectedSupplierId.HasValue || !args.SelectedArticleTypeId.HasValue) return;
			var criteria = new ArticlesBySupplierAndArticleType(args.SelectedSupplierId.Value, args.SelectedArticleTypeId.Value);
			var articles = _articleRepository.FindBy(criteria).Where(x => x.Active);
			View.Model.OrderArticles = _viewParser.ParseWithDefaultItem(articles, item => new ListItem(item.Name, item.Id));

			var supplier = _articleSupplierRepository.Get(args.SelectedSupplierId.Value);
			View.Model.ShippingOptions = _viewParser.Parse(supplier.ShippingOptions);
		}

		private void FillModelBasedOnArticleType(OrderChangedEventArgs args)
		{
			if (!args.SelectedArticleTypeId.HasValue) return;
			var suppliers = _articleSupplierRepository.GetAll();
			Func<ArticleSupplier, bool> activeSuppliersWithArticlesOfSelectedType = articleSupplier => articleSupplier.Articles.Any(x => x.ArticleType.Id == args.SelectedArticleTypeId) && articleSupplier.Active;
			var filteredSuppliers = suppliers.Where(activeSuppliersWithArticlesOfSelectedType).ToList();
			View.Model.Suppliers = _viewParser.ParseWithDefaultItem(filteredSuppliers, item => new ListItem(item.Name, item.Id));
		}

		private void FillModelBasedOnCategory(OrderChangedEventArgs args)
		{
			if (!args.SelectedCategoryId.HasValue) return;
			var criteria = new ArticleTypesByCategory(args.SelectedCategoryId.Value);
			var articleTypes = _articleTypeRepository.FindBy(criteria).Where(x => x.Active);
			View.Model.ArticleTypes = _viewParser.ParseWithDefaultItem(articleTypes, item => new ListItem(item.Name, item.Id));
		}

    	public void View_Submit(object o, OrderChangedEventArgs form)
        {
        	var orderId = RequestOrderId.HasValue 
				? UpdateExistingOrder(form, RequestOrderId.Value) 
				: CreateNewOrder(form);
        	Redirect(View.NextPageId, new {order = orderId});
        }

    	private int CreateNewOrder(OrderChangedEventArgs form)
        {

            var lensRecipe = new LensRecipe
            {
                Axis = form.SelectedAxis,
                BaseCurve = form.SelectedBaseCurve,
                Cylinder = form.SelectedCylinder,
                Diameter = form.SelectedDiameter,
                Power = form.SelectedPower,
                Addition = form.SelectedAddition,
				Quantity = form.SelectedQuantity,
				Article = new EyeParameter<Article>
				{
					Left = form.SelectedArticleId.Left.HasValue ? _articleRepository.Get(form.SelectedArticleId.Left.Value) : null,
					Right = form.SelectedArticleId.Right.HasValue ? _articleRepository.Get(form.SelectedArticleId.Right.Value) : null
				},
				ArticleCategory = _articleCategoryRepository.Get(form.SelectedCategoryId.Value),
				ArticleSupplier = _articleSupplierRepository.Get(form.SelectedSupplierId.Value),
				ArticleType = _articleTypeRepository.Get(form.SelectedArticleTypeId.Value),
            };
            _lensRecipeRepository.Save(lensRecipe);

            var customer = _orderCustomerRepository.Get(RequestCustomerId.Value);
    		var shop = _shopRepository.Get(ShopId);
            var order = new Order
            {
                LensRecipe = lensRecipe,
                ShippingType = (OrderShippingOption) form.SelectedShippingOption.Value,
                Customer = customer,
				Shop = shop,
				Reference = form.SelectedReference
            };
            _orderRepository.Save(order);
            return order.Id;
        }

        private int UpdateExistingOrder(OrderChangedEventArgs form, int orderId)
        {
            var order = _orderRepository.Get(orderId);
            order.ShippingType = (OrderShippingOption) form.SelectedShippingOption.Value;
        	order.Reference = form.SelectedReference;
            _orderRepository.Save(order);

            var lensRecipe = order.LensRecipe;
        	lensRecipe.Axis = form.SelectedAxis;
            lensRecipe.BaseCurve = form.SelectedBaseCurve;
            lensRecipe.Cylinder = form.SelectedCylinder;
            lensRecipe.Diameter = form.SelectedDiameter;
            lensRecipe.Power = form.SelectedPower;
            lensRecipe.Addition = form.SelectedAddition;
        	lensRecipe.Quantity = form.SelectedQuantity;
			lensRecipe.Article = new EyeParameter<Article>
			{
				Left = form.SelectedArticleId.Left.HasValue ? _articleRepository.Get(form.SelectedArticleId.Left.Value) : null,
				Right = form.SelectedArticleId.Right.HasValue ? _articleRepository.Get(form.SelectedArticleId.Right.Value) : null
			};
        	lensRecipe.ArticleCategory = _articleCategoryRepository.Get(form.SelectedCategoryId.Value);
        	lensRecipe.ArticleSupplier = _articleSupplierRepository.Get(form.SelectedSupplierId.Value);
        	lensRecipe.ArticleType = _articleTypeRepository.Get(form.SelectedArticleTypeId.Value);
            _lensRecipeRepository.Save(lensRecipe);

            return order.Id;
        }

        private void Redirect(int pageId, object routeData = null)
        {
            var url = _routingService.GetPageUrl(pageId, routeData);
        	HttpContext.Response.Redirect(url);
        }

        public void View_Abort(object o, EventArgs eventArgs)
        {
            if(RequestOrderId.HasValue)
            {
                var order = _orderRepository.Get(RequestOrderId.Value);
                var isNewSubscription = order.SelectedPaymentOption.Type.Equals(PaymentOptionType.Subscription_Autogiro_New);

                var subscription = order.SubscriptionPayment != null ? order.SubscriptionPayment.Subscription : null;

                _orderRepository.DeleteOrderAndSubscriptionItem(order);
                if (isNewSubscription && subscription != null) _subscriptionRepository.Delete(subscription);
            }

            Redirect(View.AbortPageId);
        }

        public void View_Previous(object o, EventArgs eventArgs)
        {
			if (RequestOrderId.HasValue) Redirect(View.PreviousPageId, new { order = RequestOrderId });
			if (RequestCustomerId.HasValue) Redirect(View.PreviousPageId, new { customer = RequestCustomerId.Value });
			else Redirect(View.PreviousPageId);
        }

    	private int? RequestOrderId
    	{
    		get { return HttpContext.Request.Params["order"].ToNullableInt(); }
    	}

    	private int? RequestCustomerId
    	{
    		get { return HttpContext.Request.Params["customer"].ToNullableInt(); }
    	}

		private int ShopId { get { return _synologenMemberService.GetCurrentShopId(); } }

        public override void ReleaseView()
        {
            View.Submit -= View_Submit;
            View.Load -= View_Load;
            View.Abort -= View_Abort;
            View.Previous -= View_Previous;
			View.SelectedCategory -= FillModel;
            View.SelectedArticle -= FillModel;
            View.SelectedArticleType -= FillModel;
            View.SelectedSupplier -= FillModel;
        	View.SelectedOnlyOneEye -= FillModel;
        }

    	private EyeParameter<T> GetValuesOrDefault<T>(EyeParameter<T?> parameter, T defaltValue) where T:struct
    	{
    		return new EyeParameter<T>
    		{
    			Left = parameter.Left ?? defaltValue,
				Right = parameter.Right ?? defaltValue,
    		};
    	}
    }
}