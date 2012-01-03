using System;
using System.Linq;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Services;
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
    	private readonly IOrderCustomerRepository _orderCustomerRepository;
        private readonly IArticleRepository _articleRepository;
        private readonly ILensRecipeRepository _lensRecipeRepository;

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
            ILensRecipeRepository lensRecipeRepository
			) : base(view)
        {
            _orderCustomerRepository = orderCustomerRepository;
            _routingService = routingService;
        	_articleCategoryRepository = articleCategoryRepository;
        	_viewParser = viewParser;
        	_articleSupplierRepository = articleSupplierRepository;
        	_articleTypeRepository = articleTypeRepository;
        	_orderRepository = orderRepository;
            _articleRepository = articleRepository;
            _lensRecipeRepository = lensRecipeRepository;
        	View.Load += View_Load;
            View.Submit += View_Submit;
            View.Abort += View_Abort;

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
                var articleTypes = _articleTypeRepository.FindBy(criteria);
                View.Model.ArticleTypes = _viewParser.ParseWithDefaultItem(articleTypes, supplier => new ListItem(supplier.Name, supplier.Id));
            }
            if(args.SelectedArticleTypeId > 0)
            {
                var suppliers = _articleSupplierRepository.GetAll();
                var filteredSuppliers = suppliers.Where(articleSupplier => articleSupplier.Articles.Where(x => x.ArticleType.Id == args.SelectedArticleTypeId).ToList().Count > 0).ToList();
                View.Model.Suppliers = _viewParser.ParseWithDefaultItem(filteredSuppliers, supplier => new ListItem(supplier.Name, supplier.Id));
            }
            if(args.SelectedSupplierId > 0)
            {
                var criteria = new ArticlesBySupplierAndArticleType(args.SelectedSupplierId, args.SelectedArticleTypeId);
                var articles = _articleRepository.FindBy(criteria);
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
                if (View.Model.PowerOptions.ToList().Count > 1)
                {
                    View.Model.PowerOptionsEnabled = true;
                }

                View.Model.DiameterOptions = _viewParser.FillWithIncrementalValues(options.Diameter);
                if (View.Model.DiameterOptions.ToList().Count > 1)
                {
                    View.Model.DiameterOptionsEnabled = true;
                }
                
                View.Model.BaseCurveOptions = _viewParser.FillWithIncrementalValues(options.BaseCurve);
                if (View.Model.BaseCurveOptions.ToList().Count > 1)
                {
                    View.Model.BaseCurveOptionsEnabled = true;
                }

                View.Model.AxisOptions = _viewParser.FillWithIncrementalValues(options.Axis);
                if (View.Model.AxisOptions.ToList().Count > 1)
                {
                    View.Model.AxisOptionsEnabled = true;
                }

                View.Model.CylinderOptions = _viewParser.FillWithIncrementalValues(options.Cylinder);
                if (View.Model.CylinderOptions.ToList().Count > 1)
                {
                    View.Model.CylinderOptionsEnabled = true;
                }

                View.Model.AdditionOptions = _viewParser.FillWithIncrementalValues(options.Addition);
                if (View.Model.AdditionOptions.ToList().Count > 1)
                {
                    View.Model.AdditionOptionsEnabled = true;
                }
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

        public override void ReleaseView()
        {
            View.Submit -= View_Submit;
            View.Load -= View_Load;
			View.SelectedCategory -= FillModel;
            View.SelectedArticle -= FillModel;
            View.SelectedArticleType -= FillModel;
            View.SelectedSupplier -= FillModel;
        }

        public void View_Submit(object o, CreateOrderEventArgs form)
        {
            var article = _articleRepository.Get(form.ArticleId);
            if (article == null) return;

			var lensRecipe = new LensRecipe
            {
                Axis = new EyeParameter
                {
                    Left = form.LeftAxis != -9999 ? form.LeftAxis : (float?) null,
                    Right = form.RightAxis != -9999 ? form.RightAxis : (float?)null
                },

                BaseCurve = new EyeParameter
                {
                    Left = form.LeftBaseCurve != -9999 ? form.LeftBaseCurve : (float?)null,
                    Right = form.RightBaseCurve != -9999 ? form.RightBaseCurve : (float?)null
                },

                Cylinder = new EyeParameter
                {
                    Left = form.LeftCylinder != -9999 ? form.LeftCylinder : (float?)null,
                    Right = form.RightCylinder != -9999 ? form.RightCylinder : (float?)null
                },

                Diameter = new EyeParameter
                {
                    Left = form.LeftDiameter != -9999 ? form.LeftDiameter : (float?)null,
                    Right = form.RightDiameter != -9999 ? form.RightDiameter : (float?)null
                },

                Power = new EyeParameter
                {
                    Left = form.LeftPower != -9999 ? form.LeftPower : (float?)null,
                    Right = form.RightPower != -9999 ? form.RightPower : (float?)null
                },

                Addition = new EyeParameter
                {
                    Left = form.LeftAddition != -9999 ? form.LeftAddition : (float?)null,
                    Right = form.RightAddition != -9999 ? form.RightAddition : (float?)null
                }
            };
            _lensRecipeRepository.Save(lensRecipe);

        	var customerId = HttpContext.Request.Params["customer"].ToInt();
        	var customer = _orderCustomerRepository.Get(customerId);
            var order = new Order
            {
                Article = article,
                LensRecipe = lensRecipe,
                ShippingType = (OrderShippingOption) form.ShipmentOption,
			    Customer = customer
			};
			_orderRepository.Save(order);
            Redirect(View.NextPageId, String.Format("?order={0}", order.Id));
        }

        private void Redirect(int pageId, string queryString="")
        {
            var url = _routingService.GetPageUrl(pageId);
            HttpContext.Response.Redirect(String.Format("{0}{1}", url, queryString));
        }

        public void View_Load(object o, EventArgs eventArgs)
        {
        	var customerIdParameter = HttpContext.Request.Params["customer"];
            var customerId = Convert.ToInt32(customerIdParameter);

        	var categories = _articleCategoryRepository.GetAll();
            var parsedCategories = _viewParser.ParseWithDefaultItem(categories, category => new ListItem(category.Name, category.Id));
            View.Model.Categories = parsedCategories;

            var customer = _orderCustomerRepository.Get(customerId);

            View.Model.CustomerId = customerId;
            View.Model.CustomerName = String.Format("{0} {1}", customer.FirstName, customer.LastName);

        }   

        public void View_Abort(object o, EventArgs eventArgs)
        {
            Redirect(View.AbortPageId);
        }

        public void View_Previous(object o, EventArgs eventArgs)
        {
            var customerId = HttpContext.Request.Params["customer"].ToInt();
            Redirect(View.PreviousPageId, String.Format("?customer={0}", customerId));
        }
    }
}