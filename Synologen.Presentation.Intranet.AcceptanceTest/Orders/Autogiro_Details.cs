using System;
using NUnit.Framework;
using Shouldly;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.TestHelpers;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.Orders
{
    [TestFixture, Category("Autogiro_Details")]
    public class When_entering_autogiro_details : OrderSpecTestbase<AutogiroDetailsPresenter, IAutogiroDetailsView>
    {
        private AutogiroDetailsPresenter _presenter;
    	private Func<string,int,string> _redirectUrl;
    	private string _previousUrl, _submitUrl, _abortUrl;
    	private Order _order;
    	private AutogiroDetailsEventArgs _form;
    	private Subscription _subscription;

    	public When_entering_autogiro_details()
        {
            Context = () =>
            {
            	_previousUrl = "/previous/page";
				_submitUrl = "/next/page";
				_abortUrl = "/abort/page";
            	SetupNavigationEvents(_previousUrl, _abortUrl, _submitUrl);
            	_redirectUrl = (url, orderId) => "{url}?order={orderId}".ReplaceWith(new {url, orderId});
                _presenter = GetPresenter();
            };

            Story = () => new Berättelse("Ange autogirodetaljer")
                          	.FörAtt("Ange detaljer för autogiro")
                          	.Som("inloggad användare på intranätet")
                          	.VillJag("kunna ange detaljer för autogiro");
        }

		[Test]
		public void VisaSidaFörNyttKonto()
		{
			SetupScenario(scenario => scenario
				.Givet(EnBeställningHarSkapatsIFöregåendeSteg)
					.Och(EttNyttKontoHarValtsIFöregåendeSteg)
				.När(SidanVisas)
				.Så(SkallKundNamnVisas)
					.Och(ArtikelNamnVisas)
					.Och(KontoUppgifterSkallVaraIfyllbara)
			);
		}

    	[Test]
		public void VisaSidaFörBefintligtKonto()
		{
			SetupScenario(scenario => scenario
				.Givet(EnBeställningHarSkapatsIFöregåendeSteg)
					.Och(EttBefintligtKontoHarValtsIFöregåendeSteg)
				.När(SidanVisas)
				.Så(SkallKundNamnVisas)
					.Och(ArtikelNamnVisas)
					.Och(KontoUppgifterSkallEjVaraIfyllbara)
					.Och(KontoUppgifterSkallVaraIfyllda)
			);
		}

    	[Test]
        public void SparaDelAbonnemangPåNyttKonto()
        {
            SetupScenario(scenario => scenario
				.Givet(EnBeställningHarSkapatsIFöregåendeSteg)
					.Och(EttNyttKontoHarValtsIFöregåendeSteg)
					.Och(AttFormuläretÄrKorrektIfyllt)
                .När(AnvändarenFörsökerFortsättaTillNästaSteg)
				.Så(SkapasEttNyttKontoMedEttNyttDelAbonnemang)
					.Och(AnvändarenFörflyttasTillNästaSteg)
            );
        }

    	[Test]
        public void SparaDelAbonnemangPåBefintligtKonto()
        {
            SetupScenario(scenario => scenario
				.Givet(EnBeställningHarSkapatsIFöregåendeSteg)
					.Och(EttBefintligtKontoHarValtsIFöregåendeSteg)
					.Och(AttFormuläretÄrKorrektIfyllt)
                .När(AnvändarenFörsökerFortsättaTillNästaSteg)
				.Så(SkapasEttNyttDelAbonnemangPåBefintligtKonto)
					.Och(AnvändarenFörflyttasTillNästaSteg)
            );
        }

    	[Test]
        public void AvbrytBeställning()
        {
            SetupScenario(scenario => scenario
                .Givet(EnBeställningHarSkapatsIFöregåendeSteg)
                .När(AnvändarenAvbryterBeställningen)
                .Så(TasBeställningenBort)
                    .Och(AnvändarenFlyttasTillAvbrytSidan)
			);
        }

    	[Test]
        public void Tillbaka()
        {
            SetupScenario(scenario => scenario
                .Givet(EnBeställningHarSkapatsIFöregåendeSteg)
                .När(AnvändarenFörsökerGåTillFöregåendeSteg)
                .Så(AnvändarenFlyttasTillFöregåendeSteg)
			);
        }

    	#region Arrange
    	private void EnBeställningHarSkapatsIFöregåendeSteg()
        {
            _order = CreateOrder();
    		HttpContext.SetupRequestParameter("order", _order.Id.ToString());
        }
        private void AttFormuläretÄrKorrektIfyllt()
        {
        	_form = OrderFactory.GetAutogiroDetailsEventArgs();
        }
    	private void EttNyttKontoHarValtsIFöregåendeSteg()
    	{
    		_order.SelectedPaymentOption = new PaymentOption {SubscriptionId = null, Type = PaymentOptionType.Subscription_Autogiro_New};
			WithRepository<IOrderRepository>().Save(_order);
    	}

    	private void EttBefintligtKontoHarValtsIFöregåendeSteg()
    	{
    		_subscription = CreateSubscription(_order.Customer);
    		_order.SelectedPaymentOption = new PaymentOption {SubscriptionId = _subscription.Id, Type = PaymentOptionType.Subscription_Autogiro_Existing};
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
            _presenter.View_Submit(null, _form);
        }
    	private void AnvändarenFörsökerGåTillFöregåendeSteg()
    	{
    		_presenter.View_Previous(null, new EventArgs());
    	}
    	private void SidanVisas()
    	{
    		_presenter.View_Load(null, new EventArgs());
    	}
        #endregion

        #region Assert
        private void TasBeställningenBort()
        {
            WithRepository<IOrderRepository>().Get(_order.Id).ShouldBe(null);
        }

        private void AnvändarenFlyttasTillAvbrytSidan()
        {
			HttpContext.ResponseInstance.RedirectedUrl.ShouldBe(_abortUrl);
        }

    	private void AnvändarenFlyttasTillFöregåendeSteg()
    	{
        	var expectedUrl = _redirectUrl(_previousUrl, _order.Id);
			HttpContext.ResponseInstance.RedirectedUrl.ShouldBe(expectedUrl);
    	}

        private void AnvändarenFörflyttasTillNästaSteg()
        {
        	var expectedUrl = _redirectUrl(_submitUrl, _order.Id);
			HttpContext.ResponseInstance.RedirectedUrl.ShouldBe(expectedUrl);
        }

    	private void SkallKundNamnVisas()
    	{
    		View.Model.CustomerName.ShouldBe(_order.Customer.FirstName + " " + _order.Customer.LastName);
    	}

    	private void SkapasEttNyttKontoMedEttNyttDelAbonnemang()
    	{
			//Assert Subscription Item
    		var subscriptionItem = WithRepository<IOrderRepository>().Get(_order.Id).SubscriptionPayment;
			subscriptionItem.AmountForAutogiroWithdrawal.ShouldBe(_form.TaxFreeAmount + _form.TaxedAmount);
			subscriptionItem.Description.ShouldBe(_form.Description);
			subscriptionItem.Notes.ShouldBe(_form.Notes);
			subscriptionItem.NumberOfPayments.ShouldBe(_form.NumberOfPayments);
			subscriptionItem.NumberOfPaymentsLeft.ShouldBe(_form.NumberOfPayments);
			subscriptionItem.TaxFreeAmount.ShouldBe(_form.TaxFreeAmount);
			subscriptionItem.TaxedAmount.ShouldBe(_form.TaxedAmount);
			//Assert Subscription
			subscriptionItem.Subscription.ActivatedDate.ShouldBe(null);
			subscriptionItem.Subscription.Active.ShouldBe(true);
			subscriptionItem.Subscription.AutogiroPayerId.ShouldBe(null);
			subscriptionItem.Subscription.BankAccountNumber.ShouldBe(_form.BankAccountNumber);
			subscriptionItem.Subscription.ClearingNumber.ShouldBe(_form.ClearingNumber);
			subscriptionItem.Subscription.ConsentStatus.ShouldBe(SubscriptionConsentStatus.NotSent);
			subscriptionItem.Subscription.CreatedDate.Date.ShouldBe(DateTime.Now.Date);
			subscriptionItem.Subscription.Customer.Id.ShouldBe(_order.Customer.Id);
    	}

    	private void SkapasEttNyttDelAbonnemangPåBefintligtKonto()
    	{
			//Assert Subscription Item
    		var subscriptionItem = WithRepository<IOrderRepository>().Get(_order.Id).SubscriptionPayment;
			subscriptionItem.AmountForAutogiroWithdrawal.ShouldBe(_form.TaxFreeAmount + _form.TaxedAmount);
			subscriptionItem.Description.ShouldBe(_form.Description);
			subscriptionItem.Notes.ShouldBe(_form.Notes);
			subscriptionItem.NumberOfPayments.ShouldBe(_form.NumberOfPayments);
			subscriptionItem.NumberOfPaymentsLeft.ShouldBe(_form.NumberOfPayments);
			subscriptionItem.TaxFreeAmount.ShouldBe(_form.TaxFreeAmount);
			subscriptionItem.TaxedAmount.ShouldBe(_form.TaxedAmount);
			//Assert Subscription
			subscriptionItem.Subscription.Id.ShouldBe(_subscription.Id);
    	}

    	private void KontoUppgifterSkallVaraIfyllbara()
    	{
    		View.Model.IsNewSubscription.ShouldBe(true);
    	}

    	private void ArtikelNamnVisas()
    	{
    		View.Model.SelectedArticleName.ShouldBe(_order.Article.Name);
    	}

		private void KontoUppgifterSkallEjVaraIfyllbara()
    	{
    		View.Model.IsNewSubscription.ShouldBe(false);
    	}

    	private void KontoUppgifterSkallVaraIfyllda()
    	{
    		View.Model.BankAccountNumber.ShouldBe(_subscription.BankAccountNumber);
			View.Model.ClearingNumber.ShouldBe(_subscription.ClearingNumber);
    	}
        #endregion
    }
}