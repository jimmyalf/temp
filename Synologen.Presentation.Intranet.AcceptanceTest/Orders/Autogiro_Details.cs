using System;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Exceptions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.TestHelpers;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Enumerations;
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
    	private Shop _shop;
    	private DateTime _operationTime;
    	private Exception _thrownException;

    	public When_entering_autogiro_details()
        {
            Context = () =>
            {
            	_previousUrl = "/previous/page";
				_submitUrl = "/next/page";
				_abortUrl = "/abort/page";
            	_shop = CreateShop<Shop>();
            	_operationTime = new DateTime(2012, 02, 22);
            	A.CallTo(() => SynologenMemberService.GetCurrentShopId()).Returns(_shop.Id);
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
					.Och(KontoUppgifterSkallVaraIfyllbara)
			);
		}


		[Test]
		public void VisaSidaVidNavigeringTillbaka()
		{
			SetupScenario(scenario => scenario
				.Givet(EnBeställningMedAbonnemangHarSkapatsIFöregåendeSteg)
				.När(SidanVisas)
				.Så(SkallAGDetaljerVisas)
			);
		}

		[Test]
		public void VisaSidaVidNavigeringTillbakaMedTillsvidareAbonnemang()
		{
			SetupScenario(scenario => scenario
				.Givet(EnBeställningMedTillsvidareAbonnemangHarSkapatsIFöregåendeSteg)
				.När(SidanVisas)
				.Så(SkallAGDetaljerVisas)
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
					.Och(AttFormuläretÄrKorrektIfylltFörTidbegränsatAbonnemang)
                .När(AnvändarenFörsökerFortsättaTillNästaSteg)
				.Så(SkapasEttNyttKontoMedEttNyttDelAbonnemang)
					.Och(TotalUttagSparas)
					.Och(AnvändarenFörflyttasTillNästaSteg)
            );
        }

    	[Test]
        public void SparaDelAbonnemangPåBefintligtKonto()
        {
            SetupScenario(scenario => scenario
				.Givet(EnBeställningHarSkapatsIFöregåendeSteg)
					.Och(EttBefintligtKontoHarValtsIFöregåendeSteg)
					.Och(AttFormuläretÄrKorrektIfylltFörTidbegränsatAbonnemang)
                .När(AnvändarenFörsökerFortsättaTillNästaSteg)
				.Så(SkapasEttNyttDelAbonnemangPåBefintligtKonto)
					.Och(TotalUttagSparas)
					.Och(AnvändarenFörflyttasTillNästaSteg)
            );
        }


		[Test]
		public void SparaDelAbonnemangPåBefintligtKontoMedTillsvidareAbonnemang()
		{
			SetupScenario(scenario => scenario
				.Givet(EnBeställningHarSkapatsIFöregåendeSteg)
					.Och(EttBefintligtKontoHarValtsIFöregåendeSteg)
					.Och(AttFormuläretÄrKorrektIfylltFörTillsvidareAbonnemang)
				.När(AnvändarenFörsökerFortsättaTillNästaSteg)
				.Så(SkapasEttNyttDelAbonnemangPåBefintligtKonto)
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
        public void AvbrytBeställningMedPrenumeration()
        {
            SetupScenario(scenario => scenario
                .Givet(EnBeställningMedAbonnemangHarSkapatsIFöregåendeSteg)
                .När(AnvändarenAvbryterBeställningen)
                .Så(TasBeställningenBort)
                    .Och(AbonnebangTasBortOmDetSkapats)
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

        [Test]
        public void VisaSidaFörAnnanButiksOrder()
        {
            SetupScenario(scenario => scenario
                .Givet(EnOrderFörEnAnnanButik)
                .När(SidanVisas)
                .Så(KastasEttException));
        }


    	#region Arrange

        private void EnBeställningMedAbonnemangHarSkapatsIFöregåendeSteg()
        {
            _order = CreateOrderWithSubscription(_shop);
            _subscription = _order.SubscriptionPayment.Subscription;
            HttpContext.SetupRequestParameter("order", _order.Id.ToString());
        }

		private void EnBeställningMedTillsvidareAbonnemangHarSkapatsIFöregåendeSteg()
		{
            _order = CreateOrderWithSubscription(_shop, useOngoingSubscription:true);
			_subscription = _order.SubscriptionPayment.Subscription;
            HttpContext.SetupRequestParameter("order", _order.Id.ToString());			
		}

    	private void EnBeställningHarSkapatsIFöregåendeSteg()
        {
            _order = CreateOrder(_shop);
    		HttpContext.SetupRequestParameter("order", _order.Id.ToString());
        }

    	private void EnOrderFörEnAnnanButik()
    	{
    		var otherShop = CreateShop<Shop>();
            _order = CreateOrder(otherShop);
    		HttpContext.SetupRequestParameter("order", _order.Id.ToString());
    	}

        private void AttFormuläretÄrKorrektIfylltFörTidbegränsatAbonnemang()
        {
        	_form = OrderFactory.GetAutogiroDetailsEventArgs();
        }

    	private void AttFormuläretÄrKorrektIfylltFörTillsvidareAbonnemang()
    	{
    		_form = OrderFactory.GetAutogiroDetailsEventArgs(true);
    	}

    	private void EttNyttKontoHarValtsIFöregåendeSteg()
    	{
    		_order.SelectedPaymentOption = new PaymentOption {SubscriptionId = null, Type = PaymentOptionType.Subscription_Autogiro_New};
			WithRepository<IOrderRepository>().Save(_order);
    	}

    	private void EttBefintligtKontoHarValtsIFöregåendeSteg()
    	{
    		_subscription = CreateSubscription(_shop, _order.Customer);
    		_order.SelectedPaymentOption = new PaymentOption {SubscriptionId = _subscription.Id, Type = PaymentOptionType.Subscription_Autogiro_Existing};
    	    
			WithRepository<IOrderRepository>().Save(_order);
    	}
        #endregion

        #region Act
        private void AnvändarenAvbryterBeställningen()
        {
			SystemTime.InvokeWhileTimeIs(_operationTime, () => _presenter.View_Abort(null, new EventArgs()));
        }
        private void AnvändarenFörsökerFortsättaTillNästaSteg()
        {
            SystemTime.InvokeWhileTimeIs(_operationTime, () => _presenter.View_Submit(null, _form));
        }
    	private void AnvändarenFörsökerGåTillFöregåendeSteg()
    	{
    		SystemTime.InvokeWhileTimeIs(_operationTime, () => _presenter.View_Previous(null, new EventArgs()));
    	}
    	private void SidanVisas()
    	{
    		_thrownException = CatchExceptionWhile(() => 
				SystemTime.InvokeWhileTimeIs(_operationTime, () => 
					_presenter.View_Load(null, new EventArgs())
			));
    	}
        #endregion

        #region Assert
        private void TasBeställningenBort()
        {
            WithRepository<IOrderRepository>().Get(_order.Id).ShouldBe(null);
        }

        private void AbonnebangTasBortOmDetSkapats()
        {
            WithRepository<ISubscriptionRepository>().Get(_subscription.Id).ShouldBe(null);
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
    		AssertSubscriptionItemDetails(subscriptionItem);
			subscriptionItem.CreatedDate.ShouldBe(_operationTime);
			//Assert Subscription
			subscriptionItem.Subscription.ConsentedDate.ShouldBe(null);
			subscriptionItem.Subscription.Active.ShouldBe(false);
			subscriptionItem.Subscription.AutogiroPayerId.ShouldBe(null);
			subscriptionItem.Subscription.BankAccountNumber.ShouldBe(_form.BankAccountNumber);
			subscriptionItem.Subscription.ClearingNumber.ShouldBe(_form.ClearingNumber);
			subscriptionItem.Subscription.ConsentStatus.ShouldBe(SubscriptionConsentStatus.NotSent);
			subscriptionItem.Subscription.CreatedDate.Date.ShouldBe(_operationTime);
			subscriptionItem.Subscription.Customer.Id.ShouldBe(_order.Customer.Id);
			subscriptionItem.Subscription.Shop.Id.ShouldBe(_shop.Id);
    	}

    	private void SkapasEttNyttDelAbonnemangPåBefintligtKonto()
    	{
			//Assert Subscription Item
    		var subscriptionItem = WithRepository<IOrderRepository>().Get(_order.Id).SubscriptionPayment;
			AssertSubscriptionItemDetails(subscriptionItem);
			//Assert Subscription
			subscriptionItem.Subscription.Id.ShouldBe(_subscription.Id);
    	}

		private void AssertSubscriptionItemDetails(SubscriptionItem subscriptionItem)
		{
			if(_form.Type == SubscriptionType.Ongoing)
			{
				subscriptionItem.MonthlyWithdrawal.TaxFree.ShouldBe(_form.MonthlyFee.Value);
				subscriptionItem.MonthlyWithdrawal.Taxed.ShouldBe(_form.MonthlyProduct.Value);	
				subscriptionItem.WithdrawalsLimit.ShouldBe(null);
			}
			else
			{
				var expectedMonthlyWithdrawalFee = Math.Round(_form.FeePrice.Value / _form.Type.GetNumberOfWithdrawals(), 2);
				var expectedMonthlyWithdrawalProduct = Math.Round(_form.ProductPrice.Value / _form.Type.GetNumberOfWithdrawals(), 2);
				subscriptionItem.MonthlyWithdrawal.TaxFree.ShouldBe(expectedMonthlyWithdrawalFee);
				subscriptionItem.MonthlyWithdrawal.Taxed.ShouldBe(expectedMonthlyWithdrawalProduct);
				subscriptionItem.WithdrawalsLimit.ShouldBe(_form.Type.GetNumberOfWithdrawals());
			}
			subscriptionItem.PerformedWithdrawals.ShouldBe(0);
			subscriptionItem.Value.Taxed.ShouldBe(_form.ProductPrice.Value);
			subscriptionItem.Value.TaxFree.ShouldBe(_form.FeePrice.Value);
			subscriptionItem.CreatedDate.ShouldBe(_operationTime);			
		}

		private void TotalUttagSparas()
		{
			var order = WithRepository<IOrderRepository>().Get(_order.Id);
			order.OrderWithdrawalAmount.TaxFree.ShouldBe(_form.FeePrice.Value);
			order.OrderWithdrawalAmount.Taxed.ShouldBe(_form.ProductPrice.Value);
		}

    	private void KontoUppgifterSkallVaraIfyllbara()
    	{
    		View.Model.IsNewSubscription.ShouldBe(true);
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

    	private void SkallAGDetaljerVisas()
    	{
			View.Model.ProductPrice.ShouldBe(_order.SubscriptionPayment.Value.Taxed.ToString("0.00"));
			View.Model.FeePrice.ShouldBe(_order.SubscriptionPayment.Value.TaxFree.ToString("0.00"));
			View.Model.TotalWithdrawal.ShouldBe(_order.SubscriptionPayment.Value.Total.ToString("0.00"));
			View.Model.Montly.ShouldBe(_order.SubscriptionPayment.MonthlyWithdrawal.Total.ToString("0.00"));
			if(_order.SubscriptionPayment.WithdrawalsLimit == null) // Is ongoing
			{
				View.Model.SelectedSubscriptionOption.ShouldBe(SubscriptionType.Ongoing);
				View.Model.CustomMonthlyFeeAmount.ShouldBe(_order.SubscriptionPayment.MonthlyWithdrawal.TaxFree.ToString("0.00"));
				View.Model.CustomMonthlyProductAmount.ShouldBe(_order.SubscriptionPayment.MonthlyWithdrawal.Taxed.ToString("0.00"));
			}
			else
			{
				var expectedSubscriptionType = SubscriptionType.GetFromWithdrawalsLimit(_order.SubscriptionPayment.WithdrawalsLimit);
				View.Model.SelectedSubscriptionOption.ShouldBe(expectedSubscriptionType);
			}
    	}

    	private void KastasEttException()
    	{
    		_thrownException.ShouldBeTypeOf<AccessDeniedException>();
    	}

        #endregion
    }
}