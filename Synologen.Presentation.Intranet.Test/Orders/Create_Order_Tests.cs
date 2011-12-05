using System;
using System.Collections.Generic;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;
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
	public class When_supplier_is_selected_with_all_shipping_options : CreateOrderTestbase
	{
		private IEnumerable<ArticleType> _articleTypes;
		private ArticleSupplier _supplier;

		public When_supplier_is_selected_with_all_shipping_options()
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
	public class When_supplier_is_selected_with_two_shipping_options : CreateOrderTestbase
	{
		private IEnumerable<ArticleType> _articleTypes;
		private ArticleSupplier _supplier;

		public When_supplier_is_selected_with_two_shipping_options()
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
			};

			GetPresenter = () => new CreateOrderPresenter(
				View, 
				OrderRepository, 
				OrderCustomerRepository, 
				SynologenMemberService,
				ArticleCategoryRepository,
				ViewParser,
				ArticleSupplierRepository,
				ArticleTypeRepository
			);
		}
	}
}
