using System;
using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Core.Utility;
using Spinit.Wpc.Synologen.Presentation.Site.AcceptanceTest.LensSubscription.TestHelpers;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views.LensSubscription;

namespace Spinit.Wpc.Synologen.Presentation.Site.AcceptanceTest.LensSubscription
{
	[TestFixture, Category("View_Shop_Subscription_Summary")]
	public class When_loading_subscription_view_for_a_shop : SpecTestbase<ShopSubscriptionsPresenter,IShopSubscriptionsView>
	{
		private ShopSubscriptionsPresenter _presenter;
		private Customer _customer;
		private IList<Subscription> _subscriptions;
		private IList<SubscriptionTransaction> _transactions;
		private int _customerDetailsPageId, _subscriptionDetailsPageId;
		private string _customerDetailsPageUrl, _subscriptionDetailsPageUrl;
		protected readonly Func<string, string, int, string> RenderUrl = (url, parameter, id) => "{url}?{parameter}={id}".ReplaceWith(new {url, parameter, id});

		public When_loading_subscription_view_for_a_shop()
		{
			Context = () =>
			{
				_presenter = GetPresenter();
			};
			Story = () =>
			{
				return new Berättelse("Visa abonnemangs-översikt för butik")
					.FörAtt("Snabbt få en överblick över butikens abonnemang")
					.Som("inloggad butikspersonal")
					.VillJag("se en lista över butikens samtliga abonnamang");
			};
		}

		[Test]
		public void ButikenHarAbonnemang()
		{
			SetupScenario(scenario => scenario
				.Givet(AttButikenHarEttEllerFlerLinsabonnemang)
				.När(ButikpersonalenVisarStartsidan)
				.Så(VisasEnListaMedSamtligaLinsabonnemang)
			);
		}

		private void AttButikenHarEttEllerFlerLinsabonnemang()
		{
			var countryToUse = WithRepository<ICountryRepository>().Get(SwedenCountryId);
			var shopToUse = WithRepository<IShopRepository>().Get(TestShopId);
			var otherShop = WithRepository<IShopRepository>().Get(OtherShopId);

			_customer = Factory.CreateCustomer(countryToUse, shopToUse);
			var otherCustomer = Factory.CreateCustomer(countryToUse, otherShop);
			WithRepository<ICustomerRepository>().Save(_customer);
			WithRepository<ICustomerRepository>().Save(otherCustomer);

			_subscriptions = Factory.CreateSubscriptions(_customer, otherCustomer);
			_subscriptions.Each(subscription => WithRepository<ISubscriptionRepository>().Save(subscription));

			var article = Factory.CreateTransactionArticle();
			WithRepository<ITransactionArticleRepository>().Save(article);

			_transactions = Factory.CreateTransactions(article, _subscriptions);
			_transactions.Each(transaction => WithRepository<ITransactionRepository>().Save(transaction));


			_customerDetailsPageId = 34;
			_subscriptionDetailsPageId = 15;
			_customerDetailsPageUrl = "/test/customer";
			_subscriptionDetailsPageUrl = "/test/subscription";

			A.CallTo(() => View.CustomerDetailsPageId).Returns(_customerDetailsPageId);
			A.CallTo(() => View.SubscriptionDetailsPageId).Returns(_subscriptionDetailsPageId);

			A.CallTo(() => SynologenMemberService.GetPageUrl(_customerDetailsPageId)).Returns(_customerDetailsPageUrl);
			A.CallTo(() => SynologenMemberService.GetPageUrl(_subscriptionDetailsPageId)).Returns(_subscriptionDetailsPageUrl);
			
			A.CallTo(() => SynologenMemberService.GetCurrentShopId()).Returns(TestShopId);
		}

		private void ButikpersonalenVisarStartsidan()
		{
			_presenter.View_Load(null, null);
		}

		private void VisasEnListaMedSamtligaLinsabonnemang()
		{
			//var expectedSubscriptions = _subscriptions.Where(x => x.Customer.Id == _customer.Id);
			var expectedSubscriptions = _subscriptions.Where(x => x.Customer.Shop.Id == TestShopId).OrderByDescending(x => x.Id);
			View.Model.List.Count().ShouldBe(expectedSubscriptions.Count());
			View.Model.List.And(expectedSubscriptions).Do((viewModel, subscription) =>
			{
				var transactions = _transactions.Where(x => x.Subscription.Id == subscription.Id);
				viewModel.CurrentBalance.ShouldBe(GetExpectedCurrentBalance(transactions).ToString("N2"));
				viewModel.CustomerDetailsUrl.ShouldBe(RenderUrl(_customerDetailsPageUrl, "customer", subscription.Customer.Id));
				viewModel.CustomerName.ShouldBe(subscription.Customer.ParseName(x => x.FirstName, x => x.LastName));
				viewModel.MonthlyAmount.ShouldBe(subscription.PaymentInfo.MonthlyAmount.ToString("N2"));
				viewModel.Status.ShouldBe(GetStatusMessage(subscription));
				viewModel.SubscriptionDetailsUrl.ShouldBe(RenderUrl(_subscriptionDetailsPageUrl, "subscription", subscription.Id));
			});
		}

		public decimal GetExpectedCurrentBalance(IEnumerable<SubscriptionTransaction> transactions)
		{
			Func<SubscriptionTransaction, bool> isWithdrawal = transaction => (transaction.Reason == TransactionReason.Withdrawal || transaction.Reason == TransactionReason.Correction) && transaction.Type == TransactionType.Withdrawal;
			Func<SubscriptionTransaction, bool> isDeposit = transaction => (transaction.Reason == TransactionReason.Payment || transaction.Reason == TransactionReason.Correction) && transaction.Type == TransactionType.Deposit;
			var withdrawals = transactions.Where(isWithdrawal).Sum(x => x.Amount);
			var deposits = transactions.Where(isDeposit).Sum(x => x.Amount);
			return deposits - withdrawals;
		}

		protected string GetStatusMessage(Subscription subscriptionInput)
		{
			return Switch.On(subscriptionInput, "Okänd status")
				.Case(s => !s.Active, "Inaktivt")
				.Case(s => s.Errors != null && s.Errors.Any(e => !e.IsHandled), "Har ohanterade fel")
				.Case(s => s.ConsentStatus == SubscriptionConsentStatus.Accepted, "Aktivt")
				.Case(s => s.ConsentStatus == SubscriptionConsentStatus.Denied, "Ej medgivet")
				.Case(s => s.ConsentStatus == SubscriptionConsentStatus.NotSent, "Medgivande ej skickat")
				.Case(s => s.ConsentStatus == SubscriptionConsentStatus.Sent, "Skickat för medgivande")
				.Evaluate();
		}

	}
}
