using System;
using System.Collections.Generic;
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
        private readonly ISynologenMemberService _synologenMemberService;
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
			ISynologenMemberService synologenMemberService, 
			IArticleCategoryRepository articleCategoryRepository,
			IViewParser viewParser,
			IArticleSupplierRepository articleSupplierRepository,
			IArticleTypeRepository articleTypeRepository,
            IArticleRepository articleRepository,
            ILensRecipeRepository lensRecipeRepository
			) : base(view)
        {
            _orderCustomerRepository = orderCustomerRepository;
            _synologenMemberService = synologenMemberService;
        	_articleCategoryRepository = articleCategoryRepository;
        	_viewParser = viewParser;
        	_articleSupplierRepository = articleSupplierRepository;
        	_articleTypeRepository = articleTypeRepository;
        	_orderRepository = orderRepository;
            _articleRepository = articleRepository;
            _lensRecipeRepository = lensRecipeRepository;
        	View.Load += View_Load;
            View.Submit += View_Submit;
        	View.SelectedCategory += Selected_Category;
        	View.SelectedSupplier += Selected_Supplier;
            View.SelectedArticleType += Selected_ArticleType;
        }

    	public void Selected_Supplier(object sender, SelectedSupplierEventArgs e)
    	{
    		var criteria = new ArticlesBySupplierAndArticleType(e.SupplierId, e.ArticleTypeId);
    		var articles = _articleRepository.FindBy(criteria);
            var supplier = _articleSupplierRepository.Get(e.SupplierId);
    		View.Model.ShippingOptions = _viewParser.Parse(supplier.ShippingOptions);
			View.Model.OrderArticles = _viewParser.Parse(articles, article => new ListItem(article.Name, article.Id));
    	}

    	public void Selected_Category(object sender, SelectedCategoryEventArgs e)
    	{
    		var criteria = new ArticleTypesByCategory(e.SelectedCategoryId);
			var articleTypes = _articleTypeRepository.FindBy(criteria);
            View.Model.ArticleTypes = _viewParser.Parse(articleTypes, supplier => new ListItem(supplier.Name, supplier.Id));
    	}

        public void Selected_ArticleType(object sender, SelectedArticleTypeEventArgs e)
        {
            /*
            var criteria = new OrderArticlesByArticleType(e.SelectedArticleTypeId);
            var articles = _articleRepository.FindBy(criteria);
            View.Model.OrderArticles = _viewParser.Parse(articles, article => new ListItem(article.Name, article.Id));
             */

            var suppliers = _articleSupplierRepository.GetAll();
            var filteredSuppliers = suppliers.Where(articleSupplier => articleSupplier.Articles.Where(x => x.ArticleType.Id == e.SelectedArticleTypeId).ToList().Count > 0).ToList();

            View.Model.Suppliers = _viewParser.Parse(filteredSuppliers, supplier => new ListItem(supplier.Name, supplier.Id));


            //TODO: update shipping options!
        }
        
        public void Selected_Article(object o, SelectedArticleEventArgs e)
        {
            var article = _articleRepository.Get(e.SelectedArticleId);
            var options = article.Options;
            if(options == null) return;
            View.Model.PowerOptions = _viewParser.FillWithIncrementalValues(options.Power);
            View.Model.DiameterOptions = _viewParser.FillWithIncrementalValues(options.Diameter);
            View.Model.BaseCurveOptions = _viewParser.FillWithIncrementalValues(options.BaseCurve);
            View.Model.AxisOptions = _viewParser.FillWithIncrementalValues(options.Axis);
            View.Model.CylinderOptions = _viewParser.FillWithIncrementalValues(options.Cylinder);

            //TODO: how is this supposed to work? O_o
            View.Model.ItemQuantityOptions = Enumerable.Empty<ListItem>();
        }

        public override void ReleaseView()
        {
            View.Submit -= View_Submit;
            View.Load -= View_Load;
			View.SelectedCategory -= Selected_Category;
            View.SelectedArticleType -= Selected_ArticleType;
        }

        public void View_Submit(object o, CreateOrderEventArgs form)
        {
            var article = _articleRepository.Get(form.ArticleId);
            if (article == null) return;

			//TODO: Move parsing into parse view-parser method
			var lensRecipe = new LensRecipe
            {
                Axis = new EyeParameter { Left = form.LeftAxis, Right = form.RightAxis },
                BaseCurve = new EyeParameter { Left = form.LeftBaseCurve, Right = form.RightBaseCurve },
                Cylinder = new EyeParameter { Left = form.LeftCylinder, Right = form.RightCylinder },
                Diameter = new EyeParameter { Left = form.LeftDiameter, Right = form.RightDiameter },
                Power = new EyeParameter { Left = form.LeftPower, Right = form.RightPower }
            };
            _lensRecipeRepository.Save(lensRecipe);

            //TODO:Update save functionality to fit new order model structure
			//TODO: Move parsing into parse view-parser method
        	var customerId = HttpContext.Request.Params["customer"].ToInt();
        	var customer = _orderCustomerRepository.Get(customerId);
            var order = new Order
            {
                Article = article,
                LensRecipe = lensRecipe,
                ShippingType = (OrderShippingOption) form.ShipmentOption,
			    Customer = customer
			    //SupplierId = form.SupplierId,
			    //TypeId = form.TypeId
			};
			_orderRepository.Save(order);
            Redirect();
        }


        private void Redirect()
        {
            var url = _synologenMemberService.GetPageUrl(View.NextPageId);
            HttpContext.Response.Redirect(url);
        }

        public void View_Load(object o, EventArgs eventArgs)
        {
        	var customerIdParameter = HttpContext.Request.Params["customer"];
            var customerId = Convert.ToInt32(customerIdParameter);

        	var categories = _articleCategoryRepository.GetAll();
        	View.Model.Categories = _viewParser.Parse(categories, category => new ListItem(category.Name, category.Id));

            var customer = _orderCustomerRepository.Get(customerId);

            View.Model.CustomerId = customerId;
            View.Model.CustomerName = String.Format("{0} {1}", customer.FirstName, customer.LastName);
        }

        
    }


}