using System;
using System.Web.UI.WebControls;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;
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
	[TestFixture, Category("Autogiro_Details")]
	public class When_loading_view_with_an_article_delivered_to_shop : AutogiroDetailsTestbase
	{
		private int _expectedOrderId;
		private Order _order;

		public When_loading_view_with_an_article_delivered_to_shop()
		{
			Context = () =>
			{
				_expectedOrderId = 5;
				_order = OrderFactory.GetOrder(selectedSubscriptionId: 22, shippingType: OrderShippingOption.ToStore);
				HttpContext.SetupRequestParameter("order", _expectedOrderId.ToString());
				A.CallTo(() => OrderRepository.Get(_expectedOrderId)).Returns(_order);
			};
			Because = presenter => presenter.View_Load(null, new EventArgs());
		}
		[Test]
		public void Auto_withdrawal_is_disabled()
		{
			View.Model.EnableAutoWithdrawal.ShouldBe(false);
		}
	}

	[TestFixture, Category("Autogiro_Details")]
	public class When_loading_view_with_an_article_delivered_to_customer : AutogiroDetailsTestbase
	{
		private int _expectedOrderId;
		private Order _order;

		public When_loading_view_with_an_article_delivered_to_customer()
		{
			Context = () =>
			{
				_expectedOrderId = 5;
				_order = OrderFactory.GetOrder(selectedSubscriptionId: 22, shippingType: OrderShippingOption.ToCustomer);
				HttpContext.SetupRequestParameter("order", _expectedOrderId.ToString());
				A.CallTo(() => OrderRepository.Get(_expectedOrderId)).Returns(_order);
			};
			Because = presenter => presenter.View_Load(null, new EventArgs());
		}
		[Test]
		public void Auto_withdrawal_is_enabled()
		{
			View.Model.EnableAutoWithdrawal.ShouldBe(true);
		}
	}

	public class AutogiroDetailsTestbase: PresenterTestbase<AutogiroDetailsPresenter,IAutogiroDetailsView,AutogiroDetailsModel>
	{
		protected IOrderRepository OrderRepository;
		protected IViewParser ViewParser;
		protected ISubscriptionRepository SubscriptionRepository;
		protected ISubscriptionItemRepository SubscriptionItemRepository;
		protected IRoutingService RoutingService;
		protected IShopRepository ShopRepository;
		protected ISynologenMemberService SynologenMemberService;

		protected AutogiroDetailsTestbase()
		{
			SetUp = () =>
			{
				OrderRepository = A.Fake<IOrderRepository>();
				RoutingService = A.Fake<IRoutingService>();
				ViewParser = new ViewParser();
				SubscriptionRepository = A.Fake<ISubscriptionRepository>();
				SubscriptionItemRepository = A.Fake<ISubscriptionItemRepository>();
				ShopRepository = A.Fake<IShopRepository>();
				SynologenMemberService = A.Fake<ISynologenMemberService>();
			};

			GetPresenter = () => new AutogiroDetailsPresenter(
				View,
				ViewParser,
				RoutingService,
				OrderRepository,
				SubscriptionRepository,
				SubscriptionItemRepository,
				ShopRepository,
				SynologenMemberService
			);
		}
	}
}
