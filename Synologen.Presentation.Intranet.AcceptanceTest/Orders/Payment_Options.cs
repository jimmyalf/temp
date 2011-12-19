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

            Story = () => new Ber�ttelse("Ange betalningss�tt")
                .F�rAtt("Ange betalningss�tt f�r best�llningen")
                .Som("inloggad anv�ndare p� intran�tet")
                .VillJag("kunna v�lja betalningss�tt");
        }

        [Test]
        public void AngeBetalningss�ttMedNyttKonto()
        {
            SetupScenario(scenario => scenario
				.Givet(EnBest�llningHarSkapatsIF�reg�endeSteg)
					.Och(AttAnv�ndarenValtAttBetalaMedNyttKonto)
                .N�r(Anv�ndarenF�rs�kerForts�ttaTillN�staSteg)
				.S�(SparasValtBetalningsAlternativ)
					.Och(F�rflyttasAnv�ndarenTillN�staSteg)
            );
        }

        [Test]
        public void AngeBetalningss�ttMedBefintligtKonto()
        {
            SetupScenario(scenario => scenario
				.Givet(EnBest�llningHarSkapatsIF�reg�endeSteg)
					.Och(AttAnv�ndarenValtAttBetalaBefintligtKonto)
                .N�r(Anv�ndarenF�rs�kerForts�ttaTillN�staSteg)
				.S�(SparasValtBetalningsAlternativ)
					.Och(F�rflyttasAnv�ndarenTillN�staSteg)
            );
        }

    	[Test]
        public void AvbrytBest�llning()
        {
            SetupScenario(scenario => scenario
                .Givet(EnBest�llningHarSkapatsIF�reg�endeSteg)
                .N�r(Anv�ndarenAvbryterBest�llningen)
                .S�(TasBest�llningenBort)
                    .Och(Anv�ndarenFlyttasTillAvbrytsidan));
        }

    	[Test]
        public void Bak�t()
        {
            SetupScenario(scenario => scenario
                .Givet(EnBest�llningHarSkapatsIF�reg�endeSteg)
                .N�r(Anv�ndarenF�rs�kerG�TillF�reg�endeSteg)
                .S�(F�rflyttasAnv�ndarenTillF�reg�endeSteg));
        }

    	#region Arrange
        private void EnBest�llningHarSkapatsIF�reg�endeSteg()
        {
        	var article = CreateWithRepository<IArticleRepository, Article>(() => OrderFactory.GetArticle(null));
        	_order = CreateWithRepository<IOrderRepository, Order>(() => OrderFactory.GetOrder(article));
        	HttpContext.SetupRequestParameter("order", _order.Id.ToString());

        }
    	private void AttAnv�ndarenValtAttBetalaMedNyttKonto()
    	{
    		_submitEventArgs = new PaymentOptionsEventArgs();
    		_subsciption = null;
    	}

    	private void AttAnv�ndarenValtAttBetalaBefintligtKonto()
    	{
    		var customer = CreateWithRepository<IOrderCustomerRepository,OrderCustomer>(() => OrderFactory.GetCustomer());
    		_subsciption = CreateWithRepository<ISubscriptionRepository,Subscription>(() => OrderFactory.GetSubscription(customer));
    		_submitEventArgs = new PaymentOptionsEventArgs{ SubscriptionId = _subsciption.Id};
    	}
        #endregion

        #region Act
        private void Anv�ndarenAvbryterBest�llningen()
        {
            _presenter.View_Abort(null, new EventArgs());
        }
    	private void Anv�ndarenF�rs�kerForts�ttaTillN�staSteg()
    	{
    		_presenter.View_Submit(null, _submitEventArgs);
    	}
    	private void Anv�ndarenF�rs�kerG�TillF�reg�endeSteg()
    	{
    		_presenter.View_Previous(null, new EventArgs());
    	}
        #endregion

        #region Assert
        private void Anv�ndarenFlyttasTillAvbrytsidan()
        {
			var expectedUrl = "{Url}?order={OrderId}".ReplaceWith(new {Url = _abortPageUrl, OrderId = _order.Id});
        	HttpContext.ResponseInstance.RedirectedUrl.ShouldBe(expectedUrl);
        }

        private void TasBest�llningenBort()
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

    	private void F�rflyttasAnv�ndarenTillN�staSteg()
    	{
    		var expectedUrl = "{Url}?order={OrderId}".ReplaceWith(new {Url = _nextPageUrl, OrderId = _order.Id});
    		HttpContext.ResponseInstance.RedirectedUrl.ShouldBe(expectedUrl);
    	}

    	private void F�rflyttasAnv�ndarenTillF�reg�endeSteg()
    	{
    		var expectedUrl = "{Url}?order={OrderId}".ReplaceWith(new {Url = _previousPageUrl, OrderId = _order.Id});
    		HttpContext.ResponseInstance.RedirectedUrl.ShouldBe(expectedUrl);
    	}
        #endregion


    }
}