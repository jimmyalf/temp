using System;
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

        public CreateOrderPresenter(
			ICreateOrderView view, 
			IOrderRepository orderRepository, 
			IOrderCustomerRepository orderCustomerRepository, 
			ISynologenMemberService synologenMemberService, 
			IArticleCategoryRepository articleCategoryRepository,
			IViewParser viewParser,
			IArticleSupplierRepository articleSupplierRepository,
			IArticleTypeRepository articleTypeRepository,
            IArticleRepository articleRepository
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
        	View.Load += View_Load;
            View.Submit += View_Submit;
        	View.SelectedCategory += Selected_Category;
        	View.SelectedSupplier += Selected_Supplier;
            View.SelectedArticleType += Selected_ArticleType;
        }

    	public void Selected_Supplier(object sender, SelectedSupplierEventArgs e)
    	{
    		var criteria = new ArticleTypesBySupplier(e.SupplierId);
    		var articleTypes = _articleTypeRepository.FindBy(criteria);
    		var supplier = _articleSupplierRepository.Get(e.SupplierId);
    		View.Model.ShippingOptions = _viewParser.Parse(supplier.ShippingOptions);
			View.Model.ArticleTypes = _viewParser.Parse(articleTypes, articleType => new ListItem(articleType.Name, articleType.Id));
    	}

    	public void Selected_Category(object sender, SelectedCategoryEventArgs e)
    	{
    		var criteria = new SuppliersByCategory(e.SelectedCategoryId);
			var suppliers = _articleSupplierRepository.FindBy(criteria);
			View.Model.Suppliers = _viewParser.Parse(suppliers, supplier => new ListItem(supplier.Name, supplier.Id));
    	}

        public void Selected_ArticleType(object sender, SelectedArticleTypeEventArgs e)
        {
            var criteria = new OrderArticlesByArticleType(e.SelectedArticleTypeId);
            var articles = _articleRepository.FindBy(criteria);
            View.Model.OrderArticles = _viewParser.Parse(articles, article => new ListItem(article.Name, article.Id));
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
			//TODO:Update save functionality to fit new order model structure
			//var order = new Order
			//{
			//    ArticleId = form.ArticleId,
			//    CategoryId = form.CategoryId,
			//    LeftBaseCurve = form.LeftBaseCurve,
			//    LeftDiameter = form.LeftDiameter,
			//    LeftPower = form.LeftPower,
			//    RightBaseCurve = form.RightBaseCurve,
			//    RightDiameter = form.RightDiameter,
			//    RightPower = form.RightPower,
			//    ShipmentOption = form.ShipmentOption,
			//    SupplierId = form.SupplierId,
			//    TypeId = form.TypeId
			//};
			//_orderRepository.Save(order);

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