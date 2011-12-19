using System;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.TestHelpers;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.Orders
{
    [TestFixture, Category("Payment_Options")]
    public class When_selecting_payment_options : SpecTestbase<PaymentOptionsPresenter, IPaymentOptionsView>
    {
        private PaymentOptionsPresenter _presenter;
    	private PaymentOptionsEventArgs _submitEventArgs;
    	private Order _order;
    	private string _abortPageUrl, _nextPageUrl, _previousPageUrl;
    	private Subscription _subsciption;

    	public When_selecting_payment_options()
        {
            Context = () =>
            {
            	_abortPageUrl = "/test/abort";
				_nextPageUrl = "/test/next";
            	_previousPageUrl = "/test/previous";
            	A.CallTo(() => View.PreviousPageId).Returns(1);
				A.CallTo(() => View.AbortPageId).Returns(2);
				A.CallTo(() => View.NextPageId).Returns(3);
				A.CallTo(() => SynologenMemberService.GetPageUrl(View.PreviousPageId)).Returns(_previousPageUrl);
            	A.CallTo(() => SynologenMemberService.GetPageUrl(View.AbortPageId)).Returns(_abortPageUrl);
				A.CallTo(() => SynologenMemberService.GetPageUrl(View.NextPageId)).Returns(_nextPageUrl);
                _presenter = GetPresenter();
            };

            Story = () => new Berättelse("Ange betalningssätt")
                .FörAtt("Ange betalningssätt för beställningen")
                .Som("inloggad användare på intranätet")
                .VillJag("kunna välja betalningssätt");
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
        public void AvbrytBeställning()
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

    	#region Arrange
        private void EnBeställningHarSkapatsIFöregåendeSteg()
        {
        	var article = CreateWithRepository<IArticleRepository, Article>(() => OrderFactory.GetArticle(null));
        	_order = CreateWithRepository<IOrderRepository, Order>(() => OrderFactory.GetOrder(article));
        	HttpContext.SetupRequestParameter("order", _order.Id.ToString());

        }
    	private void AttAnvändarenValtAttBetalaMedNyttKonto()
    	{
    		_submitEventArgs = new PaymentOptionsEventArgs();
    		_subsciption = null;
    	}

    	private void AttAnvändarenValtAttBetalaBefintligtKonto()
    	{
    		var customer = CreateWithRepository<IOrderCustomerRepository,OrderCustomer>(() => OrderFactory.GetCustomer());
    		_subsciption = CreateWithRepository<ISubscriptionRepository,Subscription>(() => OrderFactory.GetSubscription(customer));
    		_submitEventArgs = new PaymentOptionsEventArgs{ SubscriptionId = _subsciption.Id};
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
        #endregion

        #region Assert
        private void AnvändarenFlyttasTillAvbrytsidan()
        {
			var expectedUrl = "{Url}?order={OrderId}".ReplaceWith(new {Url = _abortPageUrl, OrderId = _order.Id});
        	HttpContext.ResponseInstance.RedirectedUrl.ShouldBe(expectedUrl);
        }

        private void TasBeställningenBort()
        {
            WithRepository<IOrderRepository>().Get(_order.Id).ShouldBe(null);
        }

    	private void SparasValtBetalningsAlternativ()
    	{
    		var expectedType = (_subsciption == null) ? PaymentOptionType.Subscription_Autogiro_New : PaymentOptionType.Subscription_Autogiro_Existing;
			var expectedSubscriptionId = (_subsciption == null) ? null : (int?)_subsciption.Id;

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
        #endregion


    }
}