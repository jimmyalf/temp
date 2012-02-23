using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Intranet.Test.LensSubscriptionTests.Factories;
using Spinit.Wpc.Synologen.Presentation.Intranet.Test.LensSubscriptionTests.TestHelpers;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Test.LensSubscriptionTests
{
	[TestFixture, Category("ShopSubscriptionsPresenterTests")]
	public class When_loading_shop_subscriptions_view : ShopSubscriptionsPresenterTestbase
	{
		private IEnumerable<Subscription> _subscriptions;
		private Customer _customer;
		private int _shopId;
		private string _customerDetailsPage;
		private string _subscriptionDetailsPage;
		private int _customerDetailsPageId;
		private int _subscriptionDetailsPageId;

		public When_loading_shop_subscriptions_view()
		{
			Context = () =>
			{
				_customer = CustomerFactory.Get(55);
				_subscriptions = SubscriptionFactory.GetListWithTransactions(_customer);
				_customerDetailsPage = "/path1/path2/customer.aspx";
				_subscriptionDetailsPage = "/path1/path2/subscription.aspx";
				_shopId = 99;
				_customerDetailsPageId = 20;
				_subscriptionDetailsPageId = 21;
				A.CallTo(() => SynologenMemberService.GetCurrentShopId()).Returns(_shopId);
				A.CallTo(() => RoutingService.GetPageUrl(_customerDetailsPageId)).Returns(_customerDetailsPage);
				A.CallTo(() => RoutingService.GetPageUrl(_subscriptionDetailsPageId)).Returns(_subscriptionDetailsPage);
				A.CallTo(() => SubscriptionRepository.FindBy(A<AllSubscriptionsForShopCriteria>.That.Matches(x => x.ShopId.Equals(_shopId)))).Returns(_subscriptions);
				A.CallTo(() => View.CustomerDetailsPageId).Returns(_customerDetailsPageId);
				A.CallTo(() => View.SubscriptionDetailsPageId).Returns(_subscriptionDetailsPageId);
			};
			Because = presenter => presenter.View_Load(null, null);
		}

		[Test]
		public void View_model_contains_expected_number_of_subscriptions()
		{
			View.Model.List.Count().ShouldBe(_subscriptions.Count());
		}

		[Test]
		public void View_model_has_expected_data()
		{
			View.Model.List.And(_subscriptions).Do((viewItem, domainItem) =>
			{
				viewItem.CustomerName.ShouldBe(domainItem.Customer.ParseName(x => x.FirstName, x => x.LastName));
				viewItem.MonthlyAmount.ShouldBe(domainItem.PaymentInfo.MonthlyAmount.ToString("N2"));
				viewItem.CurrentBalance.ShouldBe(domainItem.GetCurrentAccountBalance().ToString("N2"));
				viewItem.Status.ShouldBe(GetExpectedSubscriptionStatus(domainItem));
				viewItem.CustomerDetailsUrl.ShouldBe("{Url}?customer={CustomerId}".ReplaceWith(new {Url = _customerDetailsPage, CustomerId = domainItem.Customer.Id}));
				viewItem.SubscriptionDetailsUrl.ShouldBe("{Url}?subscription={SubscriptionId}".ReplaceWith(new {Url = _subscriptionDetailsPage, SubscriptionId = domainItem.Id}));
			});
		}

		public string GetExpectedSubscriptionStatus(Subscription subscription)
		{
			if(!subscription.Active)
			{
				return "Inaktivt";
			}
			if(HasError(subscription))
			{
				return "Har ohanterade fel";
			}
			if(subscription.ConsentStatus ==  SubscriptionConsentStatus.NotSent)
			{
				return "Medgivande ej skickat";
			}
			if(subscription.ConsentStatus ==  SubscriptionConsentStatus.Sent)
			{
				return "Skickat för medgivande";
			}
			if(subscription.ConsentStatus ==  SubscriptionConsentStatus.Accepted)
			{
				return "Aktivt";
			}
			if(subscription.ConsentStatus ==  SubscriptionConsentStatus.Denied)
			{
				return "Ej medgivet";
			}
			return "Okänd status";
		}

		private static bool HasError(Subscription subscription)
		{
			return (subscription == null || subscription.Errors == null) 
				? false 
				: subscription.Errors.Any(x => !x.IsHandled);
		}

	}
}
