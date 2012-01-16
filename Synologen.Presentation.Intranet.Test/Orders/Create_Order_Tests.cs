using System;
using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Services;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Test.TestHelpers;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Test.Orders
{
	[TestFixture]
	[Category("Create Order Tests")]
	public class When_loading_create_order_view : CreateOrderTestbase
	{
		private EventArgs _args;
		private IEnumerable<ArticleCategory> _expectedCategories;
		private int _expectedCustomerId;
		private OrderCustomer _customer;

		public When_loading_create_order_view()
		{
			Context = () =>
			{
				_expectedCustomerId = 33;
				_customer = OrderFactory.GetCustomer();
				_expectedCategories = OrderFactory.GetCategories();
				HttpContext.SetupRequestParameter("customer", _expectedCustomerId.ToString());
				A.CallTo(() => ArticleCategoryRepository.GetAll()).Returns(_expectedCategories);
				A.CallTo(() => OrderCustomerRepository.Get(_expectedCustomerId)).Returns(_customer);
				_args = new EventArgs();
			};

			Because = presenter => presenter.View_Load(null, _args);
		}

		[Test]
		public void Categories_are_loaded()
		{
		    var viewModelCategories = View.Model.Categories.ToList();
            viewModelCategories.RemoveAt(0);
			viewModelCategories.And(_expectedCategories).Do((viewModelItem, domainItem) =>
			{
				viewModelItem.Value.ShouldBe(domainItem.Id.ToString());
				viewModelItem.Text.ShouldBe(domainItem.Name);
			});
		}

		[Test]
		public void Customer_name_is_displayed()
		{
			var customerName = "{FirstName} {LastName}".ReplaceWith(new {_customer.FirstName, _customer.LastName});
			View.Model.CustomerName.ShouldBe(customerName);
		}
	}


	public abstract class CreateOrderTestbase: PresenterTestbase<CreateOrderPresenter,ICreateOrderView,CreateOrderModel>
	{
		protected IOrderRepository OrderRepository;
	    protected IOrderCustomerRepository OrderCustomerRepository;
		protected ISynologenMemberService SynologenMemberService;
		protected IArticleCategoryRepository ArticleCategoryRepository;
		protected IViewParser ViewParser;
		protected IArticleSupplierRepository ArticleSupplierRepository;
		protected IArticleTypeRepository ArticleTypeRepository;
	    protected IArticleRepository ArticleRepository;
	    protected ILensRecipeRepository LensRecipeRepository;
	    protected IRoutingService RoutingService;
		protected IShopRepository ShopRepository;

		protected CreateOrderTestbase()
		{
			SetUp = () =>
			{
				OrderRepository = A.Fake<IOrderRepository>();
				RoutingService = A.Fake<IRoutingService>();
				OrderCustomerRepository = A.Fake<IOrderCustomerRepository>();
				ArticleCategoryRepository = A.Fake<IArticleCategoryRepository>();
				ViewParser = new ViewParser();
				ArticleSupplierRepository = A.Fake<IArticleSupplierRepository>();
				ArticleTypeRepository = A.Fake<IArticleTypeRepository>();
			    ArticleRepository = A.Fake<IArticleRepository>();
			    LensRecipeRepository = A.Fake<ILensRecipeRepository>();
				ShopRepository = A.Fake<IShopRepository>();
				SynologenMemberService = A.Fake<ISynologenMemberService>();
			};

			GetPresenter = () => new CreateOrderPresenter(
				View, 
				OrderRepository, 
				OrderCustomerRepository, 
				RoutingService,
				ArticleCategoryRepository,
				ViewParser,
				ArticleSupplierRepository,
				ArticleTypeRepository,
                ArticleRepository,
                LensRecipeRepository,
				ShopRepository,
				SynologenMemberService
			);
		}
	}
}
