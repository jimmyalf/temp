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

    	//TODO: Consider removing -9999 "magic number" and replace with a named constant

        public CreateOrderPresenter(ICreateOrderView view, IOrderRepository orderRepository, IOrderCustomerRepository orderCustomerRepository, IRoutingService routingService, IArticleCategoryRepository articleCategoryRepository, IViewParser viewParser, IArticleSupplierRepository articleSupplierRepository, IArticleTypeRepository articleTypeRepository, IArticleRepository articleRepository, ILensRecipeRepository lensRecipeRepository, IShopRepository shopRepository, ISubscriptionRepository subscriptionRepository, ISynologenMemberService synologenMemberService) : base(view)
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
		}

        public void FillModel(object sender, SelectedSomethingEventArgs args)
        {
            if(args.SelectedCategoryId > 0)
            {
                var criteria = new ArticleTypesByCategory(args.SelectedCategoryId);
                var articleTypes = _articleTypeRepository.FindBy(criteria).Where(x => x.Active);
                View.Model.ArticleTypes = _viewParser.ParseWithDefaultItem(articleTypes, supplier => new ListItem(supplier.Name, supplier.Id));
            }
            if(args.SelectedArticleTypeId > 0)
            {
                var suppliers = _articleSupplierRepository.GetAll();
            	Func<ArticleSupplier, bool> activeSuppliersWithArticlesOfSelectedType = articleSupplier => articleSupplier.Articles.Any(x => x.ArticleType.Id == args.SelectedArticleTypeId) && articleSupplier.Active;
                var filteredSuppliers = suppliers.Where(activeSuppliersWithArticlesOfSelectedType).ToList();
                View.Model.Suppliers = _viewParser.ParseWithDefaultItem(filteredSuppliers, supplier => new ListItem(supplier.Name, supplier.Id));
            }
            if(args.SelectedSupplierId > 0)
            {
                var criteria = new ArticlesBySupplierAndArticleType(args.SelectedSupplierId, args.SelectedArticleTypeId);
                var articles = _articleRepository.FindBy(criteria).Where(x => x.Active);
                View.Model.OrderArticles = _viewParser.ParseWithDefaultItem(articles, article => new ListItem(article.Name, article.Id));

                var supplier = _articleSupplierRepository.Get(args.SelectedSupplierId);
                View.Model.ShippingOptions = _viewParser.Parse(supplier.ShippingOptions);
            }
            if(args.SelectedArticleId > 0)
            {
                var article = _articleRepository.Get(args.SelectedArticleId);
                var options = article.Options;
                if (options == null) return;
                View.Model.PowerOptions = _viewParser.FillWithIncrementalValues(options.Power);
                View.Model.DiameterOptions = _viewParser.FillWithIncrementalValues(options.Diameter);
                View.Model.BaseCurveOptions = _viewParser.FillWithIncrementalValues(options.BaseCurve);
                View.Model.AxisOptions = _viewParser.FillWithIncrementalValues(options.Axis);
				View.Model.AxisOptionsEnabled = !options.Axis.DisableDefinition;
                View.Model.CylinderOptions = _viewParser.FillWithIncrementalValues(options.Cylinder);
				View.Model.CylinderOptionsEnabled = !options.Cylinder.DisableDefinition;
                View.Model.AdditionOptions = _viewParser.FillWithIncrementalValues(options.Addition);
            	View.Model.AdditionOptionsEnabled = !options.Addition.DisableDefinition;
            }

            View.Model.SelectedCategoryId = args.SelectedCategoryId;
            View.Model.SelectedArticleTypeId = args.SelectedArticleTypeId;
            View.Model.SelectedSupplierId = args.SelectedSupplierId;
            View.Model.SelectedArticleId = args.SelectedArticleId;
            View.Model.SelectedShippingOption = args.SelectedShippingOption;

            View.Model.SelectedLeftPower = args.SelectedLeftPower;
            View.Model.SelectedLeftBaseCurve = args.SelectedLeftBaseCurve;
            View.Model.SelectedLeftDiameter = args.SelectedLeftDiameter;
            View.Model.SelectedLeftCylinder = args.SelectedLeftCylinder;
            View.Model.SelectedLeftAxis = args.SelectedLeftAxis;
            View.Model.SelectedLeftAddition = args.SelectedLeftAddition;
            View.Model.SelectedRightPower = args.SelectedRightPower;
            View.Model.SelectedRightBaseCurve = args.SelectedRightBaseCurve;
            View.Model.SelectedRightDiameter = args.SelectedRightDiameter;
            View.Model.SelectedRightCylinder = args.SelectedRightCylinder;
            View.Model.SelectedRightAxis = args.SelectedRightAxis;
            View.Model.SelectedRightAddition = args.SelectedRightAddition;
        }


        public void View_Submit(object o, CreateOrderEventArgs form)
        {
        	var orderId = RequestOrderId.HasValue 
				? UpdateExistingOrder(form, RequestOrderId.Value) 
				: CreateNewOrder(form);
        	Redirect(View.NextPageId, new {order = orderId});
        }

    	private int CreateNewOrder(CreateOrderEventArgs form)
        {
            var article = _articleRepository.Get(form.ArticleId);
			//TODO: Move parsing into view parser
            var lensRecipe = new LensRecipe
            {
                Axis = form.GetEyeParameter(x => x.LeftAxis, x => x.RightAxis),
                BaseCurve = form.GetEyeParameter(x => x.LeftBaseCurve, x => x.RightBaseCurve),
                Cylinder = form.GetEyeParameter(x => x.LeftCylinder, x => x.RightCylinder),
                Diameter = form.GetEyeParameter(x => x.LeftDiameter, x => x.RightDiameter),
                Power = form.GetEyeParameter(x => x.LeftPower, x => x.RightPower),
                Addition = form.GetEyeParameter(x => x.LeftAddition, x => x.RightAddition)
            };
            _lensRecipeRepository.Save(lensRecipe);

            var customer = _orderCustomerRepository.Get(RequestCustomerId.Value);
    		var shop = _shopRepository.Get(ShopId);
            var order = new Order
            {
                Article = article,
                LensRecipe = lensRecipe,
                ShippingType = (OrderShippingOption) form.ShipmentOption,
                Customer = customer,
				Shop = shop
            };
            _orderRepository.Save(order);
            return order.Id;
        }

        private int UpdateExistingOrder(CreateOrderEventArgs form, int orderId)
        {
            //TODO: security. make sure that the order updated is the one that should be updated, for instance by checking that the order belongs to the butik trying to update an order.
            var order = _orderRepository.Get(orderId);
            order.Article = _articleRepository.Get(form.ArticleId);
            order.ShippingType = (OrderShippingOption) form.ShipmentOption;
            _orderRepository.Save(order);

            var lensRecipe = order.LensRecipe;
        	lensRecipe.Axis = form.GetEyeParameter(x => x.LeftAxis, x => x.RightAxis);
            lensRecipe.BaseCurve = form.GetEyeParameter(x => x.LeftBaseCurve, x => x.RightBaseCurve);
            lensRecipe.Cylinder = form.GetEyeParameter(x => x.LeftCylinder, x => x.RightCylinder);
            lensRecipe.Diameter = form.GetEyeParameter(x => x.LeftDiameter, x => x.RightDiameter);
            lensRecipe.Power = form.GetEyeParameter(x => x.LeftPower, x => x.RightPower);
            lensRecipe.Addition = form.GetEyeParameter(x => x.LeftAddition, x => x.RightAddition);
            _lensRecipeRepository.Save(lensRecipe);

            return order.Id;
        }

        private void Redirect(int pageId, object routeData = null)
        {
            var url = _routingService.GetPageUrl(pageId, routeData);
        	HttpContext.Response.Redirect(url);
        }

        public void View_Load(object o, EventArgs eventArgs)
        {
            if(RequestOrderId.HasValue)
            {
                var order = _orderRepository.Get(RequestOrderId.Value);
                var args = new SelectedSomethingEventArgs
                {
                    SelectedArticleId = order.Article.Id,
                    SelectedArticleTypeId = order.Article.ArticleType.Id,
                    SelectedCategoryId = order.Article.ArticleType.Category.Id,
                    SelectedShippingOption = (int)order.ShippingType,
                    SelectedSupplierId = order.Article.ArticleSupplier.Id,

                    ExistingOrderId = order.Id,
                        
                    SelectedLeftAddition = GetEyeParameterValueOrDefault(order.LensRecipe.Addition, x => x.Left),
					SelectedLeftAxis = GetEyeParameterValueOrDefault(order.LensRecipe.Axis, x => x.Left),
                    SelectedLeftBaseCurve = GetEyeParameterValueOrDefault(order.LensRecipe.BaseCurve, x => x.Left),
                    SelectedLeftCylinder = GetEyeParameterValueOrDefault(order.LensRecipe.Cylinder, x => x.Left),
                    SelectedLeftDiameter = GetEyeParameterValueOrDefault(order.LensRecipe.Diameter, x => x.Left),
                    SelectedLeftPower = GetEyeParameterValueOrDefault(order.LensRecipe.Power, x => x.Left),

                    SelectedRightAddition = GetEyeParameterValueOrDefault(order.LensRecipe.Addition, x => x.Right),
                    SelectedRightAxis = GetEyeParameterValueOrDefault(order.LensRecipe.Axis, x => x.Right),
                    SelectedRightBaseCurve = GetEyeParameterValueOrDefault(order.LensRecipe.BaseCurve, x => x.Right),
                    SelectedRightCylinder = GetEyeParameterValueOrDefault(order.LensRecipe.Cylinder, x => x.Right),
                    SelectedRightDiameter = GetEyeParameterValueOrDefault(order.LensRecipe.Diameter, x => x.Right),
                    SelectedRightPower = GetEyeParameterValueOrDefault(order.LensRecipe.Power, x => x.Right),
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

		private float GetEyeParameterValueOrDefault(EyeParameter parameter, Func<EyeParameter,float?> value)
		{
			if (parameter == null) return -9999;
			return value(parameter) ?? -9999;
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
    		get { return HttpContext.Request.Params["order"].ToNullableInt();; }
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
        }
    }
}