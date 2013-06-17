using System;
using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.TestHelpers;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.Orders
{
	[TestFixture, Category("View_Shop_Subscription_Summary")]
	public class When_loading_subscription_view_for_a_shop : GeneralOrderSpecTestbase<ShopSubscriptionsPresenter,IShopSubscriptionsView>
	{
		private ShopSubscriptionsPresenter _presenter;
		private IList<Subscription> _subscriptions;
		private IList<SubscriptionTransaction> _transactions;
		private int _customerDetailsPageId, _subscriptionDetailsPageId;
		private string _customerDetailsPageUrl, _subscriptionDetailsPageUrl;
		private readonly Func<string, string, int, string> _renderUrl = (url, parameter, id) => "{url}?{parameter}={id}".ReplaceWith(new {url, parameter, id});
		private Shop _shop1, _shop2;

		public When_loading_subscription_view_for_a_shop()
		{	
			Context = () =>
			{
				_shop1 = CreateShop<Shop>("Testbutik 1");
				_shop2 = CreateShop<Shop>("Testbutik 2");

				_customerDetailsPageId = 34;
				_subscriptionDetailsPageId = 15;
				_customerDetailsPageUrl = "/test/customer";
				_subscriptionDetailsPageUrl = "/test/subscription";

				A.CallTo(() => View.CustomerDetailsPageId).Returns(_customerDetailsPageId);
				A.CallTo(() => View.SubscriptionDetailsPageId).Returns(_subscriptionDetailsPageId);
				A.CallTo(() => SynologenMemberService.GetCurrentShopId()).Returns(_shop1.Id);
				RoutingService.AddRoute(_customerDetailsPageId, _customerDetailsPageUrl);
				RoutingService.AddRoute(_subscriptionDetailsPageId, _subscriptionDetailsPageUrl);
				_presenter = GetPresenter();
			};
			Story = () => new Berättelse("Visa abonnemangs-översikt för butik")
			    .FörAtt("Snabbt få en överblick över butikens abonnemang")
			    .Som("inloggad butikspersonal")
			    .VillJag("se en lista över butikens samtliga abonnamang");
		}

		[Test]
		public void ButikenHarAbonnemang()
		{
			SetupScenario(scenario => scenario
				.Givet(AttButikenHarEttEllerFlerAbonnemang)
				.När(ButikpersonalenVisarStartsidan)
				.Så(VisasEnListaMedSamtligaLinsabonnemang)
			);
		}

		private void AttButikenHarEttEllerFlerAbonnemang()
		{
			_subscriptions = CreateSubscriptions(_shop1).ToList();
			CreateSubscriptions(_shop2); //Create subscriptions for other shop
			_transactions = StoreItems(() => OrderFactory.GetTransactions(_subscriptions)).ToList();
			_subscriptions.Each(s => CreateSubscriptionItem(s));
		}

		private void ButikpersonalenVisarStartsidan()
		{
			_presenter.View_Load(null, null);
		}

		private void VisasEnListaMedSamtligaLinsabonnemang()
		{
			var expectedSubscriptions = GetAll<Subscription>().Where(x => x.Customer.Shop.Id == _shop1.Id).OrderByDescending(x => x.Id);
			View.Model.List.Count().ShouldBe(expectedSubscriptions.Count());
			View.Model.List.And(expectedSubscriptions).Do((viewModel, subscription) =>
			{
			    var transactions = _transactions.Where(x => x.Subscription.Id == subscription.Id).ToList();
			    viewModel.CurrentBalance.ShouldBe(GetExpectedCurrentBalance(transactions).ToString("N2"));
			    viewModel.CustomerDetailsUrl.ShouldBe(_renderUrl(_customerDetailsPageUrl, "customer", subscription.Customer.Id));
			    viewModel.CustomerName.ShouldBe(subscription.Customer.ParseName(x => x.FirstName, x => x.LastName));
				viewModel.BankAccountNumber.ShouldBe(subscription.BankAccountNumber);
			    viewModel.MonthlyAmount.ShouldBe(subscription.SubscriptionItems.Where(x => x.IsActive).Sum(x => x.MonthlyWithdrawal.Total).ToString("N2"));
			    viewModel.Status.ShouldBe(GetStatusMessage(subscription));
			    viewModel.SubscriptionDetailsUrl.ShouldBe(_renderUrl(_subscriptionDetailsPageUrl, "subscription", subscription.Id));
			});
		}
	}
}
