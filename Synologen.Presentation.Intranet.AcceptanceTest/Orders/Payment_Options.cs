using System;
using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Exceptions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.TestHelpers;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.Orders
{
    [TestFixture, Category("Payment_Options")]
    public class When_selecting_payment_options : OrderSpecTestbase<PaymentOptionsPresenter, IPaymentOptionsView>
    {
        private PaymentOptionsPresenter _presenter;
    	private PaymentOptionsEventArgs _submitEventArgs;
    	private Order _order;
    	private string _abortPageUrl, _nextPageUrl, _previousPageUrl;
    	private int _selectedSubscriptionId;
    	private IEnumerable<Subscription> _subscriptions;
    	private OrderCustomer _customer;
    	private Shop _shop;
    	private Exception _thrownException;

    	public When_selecting_payment_options()
        {
            Context = () =>
            {
				_shop = CreateShop<Shop>();
				SetupDataContext();
            	_abortPageUrl = "/test/abort";
				_nextPageUrl = "/test/next";
            	_previousPageUrl = "/test/previous";
            	A.CallTo(() => SynologenMemberService.GetCurrentShopId()).Returns(_shop.Id);
				SetupNavigationEvents(_previousPageUrl, _abortPageUrl, _nextPageUrl);
                _presenter = GetPresenter();
            };

            Story = () => new Berättelse("Ange betalningssätt")
                .FörAtt("Ange betalningssätt för beställningen")
                .Som("inloggad användare på intranätet")
                .VillJag("kunna välja betalningssätt");
        }

    	private void SetupDataContext()
    	{
			var otherCustomer = CreateCustomer(_shop);
			CreateSubscriptions(_shop, otherCustomer);
			CreateOrder(_shop, otherCustomer);
		}

		[Test]
		public void VisaBefintligaKontonOchKundNamn()
		{
			SetupScenario(scenario => scenario
				.Givet(EnBeställningHarSkapatsIFöregåendeSteg)
				.När(SidanVisas)
				.Så(SkallKundensBefintligaKontonListas)
					.Och(KundNamnVisas)
			);
		}

        [Test]
        public void FyllFormuläretMedValtBetalningsAlternativ()
        {
            SetupScenario(scenario => scenario
				.Givet(EnBeställningHarSkapatsIFöregåendeSteg)
					.Och(AttBetalningssättValts)
                .När(SidanVisas)
                .Så(BockasValtAlternativI)
			);
        }

    	[Test]
        public void AngeBetalningssättMedNyttKonto()
        {
            SetupScenario(scenario => scenario
				.Givet(EnBeställningHarSkapatsIFöregåendeSteg)
					.Och(AttAnvändarenValtAttBetalaMedNyttKonto)
                .När(AnvändarenFörsökerFortsättaTillNästaSteg)
				.Så(SparasValtBetalningsAlternativ)
					.Och(FörflyttasAnvändarenTillNästaSteg)
            );
        }

        [Test]
        public void AngeBetalningssättMedBefintligtKonto()
        {
            SetupScenario(scenario => scenario
				.Givet(EnBeställningHarSkapatsIFöregåendeSteg)
					.Och(AttAnvändarenValtAttBetalaBefintligtKonto)
                .När(AnvändarenFörsökerFortsättaTillNästaSteg)
				.Så(SparasValtBetalningsAlternativ)
					.Och(FörflyttasAnvändarenTillNästaSteg)
            );
        }

    	[Test]
        public void Avbryt()
        {
            SetupScenario(scenario => scenario
                .Givet(EnBeställningHarSkapatsIFöregåendeSteg)
                .När(AnvändarenAvbryterBeställningen)
                .Så(TasBeställningenBort)
                    .Och(AnvändarenFlyttasTillAvbrytsidan));
        }

        [Test]
        public void Bakåt()
        {
            SetupScenario(scenario => scenario
                .Givet(EnBeställningHarSkapatsIFöregåendeSteg)
                .När(AnvändarenFörsökerGåTillFöregåendeSteg)
                .Så(FörflyttasAnvändarenTillFöregåendeSteg));
        }

        [Test]
        public void VisaSidaFörAnnanButiksOrder()
        {
            SetupScenario(scenario => scenario
                .Givet(EnOrderFörEnAnnanButik)
                .När(SidanVisas)
                .Så(KastasEttException));
        }

    	#region Arrange
        private void EnBeställningHarSkapatsIFöregåendeSteg()
        {
    		_customer = CreateCustomer(_shop);
    		var subsciptions = CreateSubscriptions(_shop, _customer).ToList();
            foreach (var subscription in subsciptions)
            {
                CreateSubscriptionItems(subscription);
            }
            _subscriptions = GetAll<Subscription>(subsciptions.Select(x => x.Id));
        	_order = CreateOrder(_shop, _customer);
        	HttpContext.SetupRequestParameter("order", _order.Id.ToString());
        }

    	private void EnOrderFörEnAnnanButik()
    	{
    		var otherShop = CreateShop<Shop>();
			var otherCustomer = CreateCustomer(otherShop);
    		_order = CreateOrder(otherShop, otherCustomer);
			HttpContext.SetupRequestParameter("order", _order.Id.ToString());
    	}

    	private void AttAnvändarenValtAttBetalaMedNyttKonto()
    	{
			_selectedSubscriptionId = default(int);
    		_submitEventArgs = new PaymentOptionsEventArgs();
    	}

    	private void AttAnvändarenValtAttBetalaBefintligtKonto()
    	{
    		_selectedSubscriptionId = _subscriptions.First().Id;
    		_submitEventArgs = new PaymentOptionsEventArgs{ SubscriptionId = _selectedSubscriptionId};
    	}

        private void AttBetalningssättValts()
        {
        	_order.SelectedPaymentOption = new PaymentOption
        	{
        		Type = PaymentOptionType.Subscription_Autogiro_Existing,
				SubscriptionId = _subscriptions.First().Id
        	};
            WithRepository<IOrderRepository>().Save(_order);
        }

        #endregion

        #region Act
        private void AnvändarenAvbryterBeställningen()
        {
            _presenter.View_Abort(null, new EventArgs());
        }
    	private void AnvändarenFörsökerFortsättaTillNästaSteg()
    	{
    		_presenter.View_Submit(null, _submitEventArgs);
    	}
    	private void AnvändarenFörsökerGåTillFöregåendeSteg()
    	{
    		_presenter.View_Previous(null, new EventArgs());
    	}
    	private void SidanVisas()
    	{
    		_thrownException = CatchExceptionWhile(() => _presenter.View_Load(null, new EventArgs()));
    	}
        #endregion

        #region Assert
        private void AnvändarenFlyttasTillAvbrytsidan()
        {
        	HttpContext.ResponseInstance.RedirectedUrl.ShouldBe(_abortPageUrl);
        }

        private void TasBeställningenBort()
        {
            WithRepository<IOrderRepository>().Get(_order.Id).ShouldBe(null);
        }

    	private void SparasValtBetalningsAlternativ()
    	{
    		var expectedType = (_selectedSubscriptionId == default(int)) ? PaymentOptionType.Subscription_Autogiro_New : PaymentOptionType.Subscription_Autogiro_Existing;
			var expectedSubscriptionId = (_selectedSubscriptionId == default(int)) ? null : (int?) _selectedSubscriptionId;

			WithRepository<IOrderRepository>().Get(_order.Id).SelectedPaymentOption.Type.ShouldBe(expectedType);
			WithRepository<IOrderRepository>().Get(_order.Id).SelectedPaymentOption.SubscriptionId.ShouldBe(expectedSubscriptionId);
    	}

    	private void FörflyttasAnvändarenTillNästaSteg()
    	{
    		var expectedUrl = "{Url}?order={OrderId}".ReplaceWith(new {Url = _nextPageUrl, OrderId = _order.Id});
    		HttpContext.ResponseInstance.RedirectedUrl.ShouldBe(expectedUrl);
    	}

    	private void FörflyttasAnvändarenTillFöregåendeSteg()
    	{
    		var expectedUrl = "{Url}?order={OrderId}".ReplaceWith(new {Url = _previousPageUrl, OrderId = _order.Id});
    		HttpContext.ResponseInstance.RedirectedUrl.ShouldBe(expectedUrl);
    	}

    	private void SkallKundensBefintligaKontonListas()
    	{
			var viewModelsubscriptionItems = View.Model.SubscriptionsItems.Skip(1).ToList();
    		var matchingSubscriptionsItems = _subscriptions.Where(x => x.Active).SelectMany(x => x.SubscriptionItems).ToList();
            viewModelsubscriptionItems.And(matchingSubscriptionsItems).Do((viewItem, domainItem) =>
    		{
    			var expectedText = GetExpectedSubscriptionAccountText(domainItem.Subscription);
				viewItem.Title.ShouldBe(expectedText);
    			viewItem.SubscriptionId.ShouldBe(domainItem.Subscription.Id);
    		    viewItem.Name.ShouldBe(domainItem.Title);
                viewItem.Created.ShouldBe(domainItem.CreatedDate.ToShortDateString());                
                viewItem.Withdrawals.ShouldBe(GetExpectedWithdrawalText(domainItem));
    		});
			View.Model.SubscriptionsItems.First().SubscriptionId.ShouldBe(0);
            View.Model.SubscriptionsItems.First().Title.ShouldBe("Skapa nytt konto");
            View.Model.SelectedOption.ShouldBe(0);
    	}

        private string GetExpectedWithdrawalText(SubscriptionItem subscriptionItem)
        {
            return subscriptionItem.IsOngoing
                ? subscriptionItem.PerformedWithdrawals.ToString()
                : string.Format("{0}/{1}",
                subscriptionItem.PerformedWithdrawals,
                subscriptionItem.WithdrawalsLimit.Value);
        }

        private string GetExpectedSubscriptionAccountText(Subscription subscription)
		{
			return subscription.BankAccountNumber + " (" + subscription.ConsentStatus.GetEnumDisplayName() + ")";
		}

		private void KundNamnVisas()
		{
			View.Model.CustomerName.ShouldBe("{FirstName} {LastName}".ReplaceWith(new {_customer.FirstName, _customer.LastName}));
		}

        private void BockasValtAlternativI()
        {
			if (!_order.SelectedPaymentOption.SubscriptionId.HasValue) throw new AssertionException("SubscriptionId has not been set");
            View.Model.SelectedOption.ShouldBe(_order.SelectedPaymentOption.SubscriptionId.Value);
        }

    	private void KastasEttException()
    	{
    		_thrownException.ShouldBeTypeOf<AccessDeniedException>();
    	}

        #endregion
    }
}