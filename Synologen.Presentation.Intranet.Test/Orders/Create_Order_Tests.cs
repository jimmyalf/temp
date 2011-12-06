using System;
using System.Collections.Generic;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;
using Spinit.Data;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Services;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models;
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
			View.Model.Categories.And(_expectedCategories).Do((viewModelItem, domainItem) =>
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

	[TestFixture]
	[Category("Create Order Tests")]
	public class When_category_is_selected : CreateOrderTestbase
	{
		private SelectedCategoryEventArgs _eventArgs;
		private IEnumerable<ArticleSupplier> _suppliers;
		private int _selectedCategoryId;

		public When_category_is_selected()
		{
			Context = () =>
			{
				_selectedCategoryId = 6;
				_eventArgs = new SelectedCategoryEventArgs(_selectedCategoryId);
				_suppliers = OrderFactory.GetSuppliers();
				A.CallTo(() => ArticleSupplierRepository.FindBy(A<SuppliersByCategory>.That.Matches(criteria => criteria.SelectedCategoryId.Equals(_selectedCategoryId)).Argument)).Returns(_suppliers);

			};
			Because = presenter => Presenter.Selected_Category(null, _eventArgs);
		}

		[Test]
		public void Suppliers_are_loaded()
		{
			View.Model.Suppliers.And(_suppliers).Do((viewSupplier, domainSupplier) =>
			{
				viewSupplier.Value.ShouldBe(domainSupplier.Id.ToString());
				viewSupplier.Text.ShouldBe(domainSupplier.Name);
			});
		}
	}

    [TestFixture]
    [Category("Create Order Tests")]
    public class When_article_type_is_selected : CreateOrderTestbase
    {
        private SelectedArticleTypeEventArgs _eventArgs;
        private IEnumerable<Article> _articles;
        private int _selectedArticleTypeId;

        public When_article_type_is_selected()
        {
            Context = () =>
                          {
                              _selectedArticleTypeId = 2;
                              _eventArgs = new SelectedArticleTypeEventArgs(_selectedArticleTypeId);
                              _articles = OrderFactory.GetArticles();
                              A.CallTo(() => ArticleRepository.FindBy(A<OrderArticlesByArticleType>.That.Matches(criteria => criteria.ArticleTypeId.Equals(_selectedArticleTypeId)).Argument)).Returns(_articles);
                          };

            Because = presenter => Presenter.Selected_ArticleType(null, _eventArgs);
        }

        [Test]
        public void Articles_are_loaded()
        {
            View.Model.OrderArticles.And(_articles).Do((viewArticle, domainArticle) =>
            {
                viewArticle.Value.ShouldBe(domainArticle.Id.ToString());
                viewArticle.Text.ShouldBe(domainArticle.Name);
            });
        }
    }

    [TestFixture]
    [Category("Create Order Tests")]
    public class When_article_is_selected : CreateOrderTestbase
    {
        private int _selectedArticleId;
        private Article _article;
        private ArticleOptions _options;

        private IEnumerable<float> _powerOptions;
        private IEnumerable<float> _baseCurveOptions;
        private IEnumerable<float> _diameterOptions;
        private IEnumerable<float> _axisOptions;
        private IEnumerable<float> _cylinderOptions;

        private SelectedArticleEventArgs _eventArgs;

        public When_article_is_selected()
        {
            Context = () =>
                          {
                              _selectedArticleId = 2;
                              _eventArgs = new SelectedArticleEventArgs(_selectedArticleId);
                              _article = OrderFactory.GetArticle(_selectedArticleId);
                              A.CallTo(() => ArticleRepository.Get(_selectedArticleId)).Returns(_article);
                              _options = _article.Options;

                              _powerOptions     = OrderFactory.GetOptionsList(_options.Power.Min, _options.Power.Max, _options.Power.Increment);
                              _baseCurveOptions = OrderFactory.GetOptionsList(_options.BaseCurve.Min, _options.BaseCurve.Max, _options.BaseCurve.Increment);
                              _diameterOptions  = OrderFactory.GetOptionsList(_options.Diameter.Min, _options.Diameter.Max, _options.Diameter.Increment);
                              _axisOptions      = OrderFactory.GetOptionsList(_options.Axis.Min, _options.Axis.Max, _options.Axis.Increment);
                              _cylinderOptions  = OrderFactory.GetOptionsList(_options.Cylinder.Min, _options.Cylinder.Max, _options.Cylinder.Increment);
                          };
            Because = presenter => Presenter.Selected_Article(null, _eventArgs);
        }

        [Test]
        public void Power_options_are_loaded()
        {
            View.Model.PowerOptions.And(_powerOptions).Do((viewOptions, domainOptions) =>
            {
                viewOptions.Value.ShouldBe(domainOptions.ToString());
                viewOptions.Text.ShouldBe(domainOptions.ToString());
            });  
        }

        [Test]
        public void BaseCurve_options_are_loaded()
        {
            View.Model.BaseCurveOptions.And(_baseCurveOptions).Do((viewOptions, domainOptions) =>
            {
                viewOptions.Value.ShouldBe(domainOptions.ToString());
                viewOptions.Text.ShouldBe(domainOptions.ToString());
            });
        }

        [Test]
        public void Diameter_options_are_loaded()
        {
            View.Model.DiameterOptions.And(_diameterOptions).Do((viewOptions, domainOptions) =>
            {
                viewOptions.Value.ShouldBe(domainOptions.ToString());
                viewOptions.Text.ShouldBe(domainOptions.ToString());
            });
        }

        [Test]
        public void Axis_options_are_loaded()
        {
            View.Model.AxisOptions.And(_axisOptions).Do((viewOptions, domainOptions) =>
            {
                viewOptions.Value.ShouldBe(domainOptions.ToString());
                viewOptions.Text.ShouldBe(domainOptions.ToString());
            });
        }

        [Test]
        public void Cylinder_options_are_loaded()
        {
            View.Model.CylinderOptions.And(_cylinderOptions).Do((viewOptions, domainOptions) =>
            {
                viewOptions.Value.ShouldBe(domainOptions.ToString());
                viewOptions.Text.ShouldBe(domainOptions.ToString());
            });
        }
    }

    [TestFixture]
	[Category("Create Order Tests")]
	public class When_supplier_with_all_shipping_options_is_selected : CreateOrderTestbase
	{
		private IEnumerable<ArticleType> _articleTypes;
		private ArticleSupplier _supplier;

		public When_supplier_with_all_shipping_options_is_selected()
		{
			Context = () =>
			{
				_articleTypes = OrderFactory.CreateArticleTypes();
				_supplier = OrderFactory.GetSupplier(6);
				A.CallTo(() => ArticleTypeRepository.FindBy(
				    A<ArticleTypesBySupplier>.That.Matches(
				        criteria => criteria.SelectedSupplierId.Equals(_supplier.Id)
				    ).Argument
				)).Returns(_articleTypes);
				A.CallTo(() => ArticleSupplierRepository.Get(_supplier.Id)).Returns(_supplier);

			};
			Because = presenter => Presenter.Selected_Supplier(null, new SelectedSupplierEventArgs(_supplier.Id));
		}

		[Test]
		public void ArticleTypes_are_loaded()
		{
			View.Model.ArticleTypes.And(_articleTypes).Do((viewArticleType, domainArticleType) =>
			{
				viewArticleType.Value.ShouldBe(domainArticleType.Id.ToString());
				viewArticleType.Text.ShouldBe(domainArticleType.Name);
			});
		}

		[Test]
		public void ShippingOptions_are_loaded()
		{
			View.Model.ShippingOptions.ShouldContain(option => 
				option.Text.Equals("Till Butik")
				&& option.Value.Equals(OrderShippingOption.ToStore.ToInteger().ToString())
			);
			View.Model.ShippingOptions.ShouldContain(option => 
				option.Text.Equals("Till Kund")
				&& option.Value.Equals(OrderShippingOption.ToCustomer.ToInteger().ToString())
			);
			View.Model.ShippingOptions.ShouldContain(option => 
				option.Text.Equals("Leverans i butik")
				&& option.Value.Equals(OrderShippingOption.DeliveredInStore.ToInteger().ToString())
			);
		}
		
	}

	[TestFixture]
	[Category("Create Order Tests")]
	public class When_supplier_with_two_shipping_options_is_selected : CreateOrderTestbase
	{
		private IEnumerable<ArticleType> _articleTypes;
		private ArticleSupplier _supplier;

        public When_supplier_with_two_shipping_options_is_selected()
		{
			Context = () =>
			{
				_articleTypes = OrderFactory.CreateArticleTypes();
				_supplier = OrderFactory.GetSupplier(6, OrderShippingOption.ToCustomer | OrderShippingOption.DeliveredInStore);
				A.CallTo(() => ArticleTypeRepository.FindBy(
				    A<ArticleTypesBySupplier>.That.Matches(
				        criteria => criteria.SelectedSupplierId.Equals(_supplier.Id)
				    ).Argument
				)).Returns(_articleTypes);
				A.CallTo(() => ArticleSupplierRepository.Get(_supplier.Id)).Returns(_supplier);

			};
			Because = presenter => Presenter.Selected_Supplier(null, new SelectedSupplierEventArgs(_supplier.Id));
		}


		[Test]
		public void ShippingOptions_are_loaded()
		{
			View.Model.ShippingOptions.ShouldNotContain(option => 
				option.Text.Equals("Till Butik")
				&& option.Value.Equals(OrderShippingOption.ToStore.ToInteger().ToString())
			);
			View.Model.ShippingOptions.ShouldContain(option => 
				option.Text.Equals("Till Kund")
				&& option.Value.Equals(OrderShippingOption.ToCustomer.ToInteger().ToString())
			);
			View.Model.ShippingOptions.ShouldContain(option => 
				option.Text.Equals("Leverans i butik")
				&& option.Value.Equals(OrderShippingOption.DeliveredInStore.ToInteger().ToString())
			);
		}
		
	}


	public class CreateOrderTestbase: PresenterTestbase<CreateOrderPresenter,ICreateOrderView,CreateOrderModel>
	{
		protected IOrderRepository OrderRepository;
	    protected IOrderCustomerRepository OrderCustomerRepository;
		protected ISynologenMemberService SynologenMemberService;
		protected IArticleCategoryRepository ArticleCategoryRepository;
		protected IViewParser ViewParser;
		protected IArticleSupplierRepository ArticleSupplierRepository;
		protected IArticleTypeRepository ArticleTypeRepository;
	    protected IArticleRepository ArticleRepository;

		protected CreateOrderTestbase()
		{
			SetUp = () =>
			{
				OrderRepository = A.Fake<IOrderRepository>();
				SynologenMemberService = A.Fake<ISynologenMemberService>();
				OrderCustomerRepository = A.Fake<IOrderCustomerRepository>();
				ArticleCategoryRepository = A.Fake<IArticleCategoryRepository>();
				ViewParser = new ViewParser();
				ArticleSupplierRepository = A.Fake<IArticleSupplierRepository>();
				ArticleTypeRepository = A.Fake<IArticleTypeRepository>();
			    ArticleRepository = A.Fake<IArticleRepository>();
			};

			GetPresenter = () => new CreateOrderPresenter(
				View, 
				OrderRepository, 
				OrderCustomerRepository, 
				SynologenMemberService,
				ArticleCategoryRepository,
				ViewParser,
				ArticleSupplierRepository,
				ArticleTypeRepository,
                ArticleRepository
			);
		}
	}
}
