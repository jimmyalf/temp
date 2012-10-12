using System;
using System.Linq;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Exceptions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.TestHelpers;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.Orders
{
    [TestFixture, Category("Confirm_Order")]
    public class When_confirming_an_order : OrderSpecTestbase<CreateOrderConfirmationPresenter, ICreateOrderConfirmationView>
    {
        private Shop _shop;
        private Order _order;
        private CreateOrderConfirmationPresenter _presenter;
        private string _previousUrl;
        private string _submitUrl;
        private string _abortUrl;
        private Func<string, int, string> _redirectUrl;
    	private Exception _thrownException;
    	private int _subscriptionCutoffDate;
    	private int _subscriptionWithdrawalDate;

    	public When_confirming_an_order()
        {
            Context = () =>
            {
                _presenter = GetPresenter();
                _shop = CreateShop<Shop>();
                _previousUrl = "/previous/page";
                _submitUrl = "/next/page";
                _abortUrl = "/abort/page";
            	_subscriptionCutoffDate = 15;
            	_subscriptionWithdrawalDate = 28;
            	A.CallTo(() => SynologenSettingsService.SubscriptionConsentCutoffDay).Returns(_subscriptionCutoffDate);
				A.CallTo(() => SynologenSettingsService.SubscriptionWithdrawalDay).Returns(_subscriptionWithdrawalDate);
                A.CallTo(() => SynologenMemberService.GetCurrentShopId()).Returns(_shop.Id);
                SetupNavigationEvents(_previousUrl, _abortUrl, _submitUrl);
                _redirectUrl = (url, orderId) => "{url}?order={orderId}".ReplaceWith(new { url, orderId });
            };

            Story = () => new Berättelse("Bekräfta beställning")
				.FörAtt("Bekräfta och skicka iväg en beställning")
				.Som("inloggad användare på intranätet")
				.VillJag("kunna se en sammanfattning av beställningen")
					.Och("bekräfta den");
        }

        [Test]
        public void EnSammanfattningAvBegränsadBeställningVisas()
        {
            SetupScenario(scenario => scenario
                .Givet(EnBeställningMedEttBegränsatAbonnemangHarSkapats)
                .När(SidanVisas)
                .Så(VisasEnSammanställningAvOrdern));
        }

        [Test]
        public void EnSammanfattningAvLöpandeBeställningVisas()
        {
            SetupScenario(scenario => scenario
                .Givet(EnBeställningMedEttLöpandeAbonnemangHarSkapats)
                .När(SidanVisas)
                .Så(VisasEnSammanställningAvOrdern));
        }

        [Test]
        public void AvbrytBeställning()
        {
            SetupScenario(scenario => scenario
                .Givet(EnBeställningMedEttBegränsatAbonnemangHarSkapats)
                .När(AnvändarenAvbryterBeställningen)
                .Så(TasAbonnemangetBort)
                    .Och(TasBeställningenBort)
                    .Och(AnvändarenFlyttasTillIntranätsidan));
        }

        [Test]
        public void SparaBeställningMedNyttAbonnemang()
        {
            SetupScenario(scenario => scenario
                .Givet(EnBeställningMedEttBegränsatAbonnemangHarSkapats)
                .När(AnvändarenBekräftarBeställningen)
				.Så(SkapasEnTotalTransaktion)
					.Och(AnvändarenFlyttasTillSidaFörFärdigBeställning)
					.Och(BeställningenByterStatusTillRedoFörAttSkickas)
					.Och(NyttAbonnemangAktiveras)
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

        private void EnBeställningMedEttBegränsatAbonnemangHarSkapats()
        {
            _order = CreateOrderWithSubscription(_shop, paymentOptionType: PaymentOptionType.Subscription_Autogiro_New);
            HttpContext.SetupRequestParameter("order", _order.Id.ToString());
        }

		private void EnBeställningMedEttLöpandeAbonnemangHarSkapats()
		{
			_order = CreateOrderWithSubscription(_shop, paymentOptionType: PaymentOptionType.Subscription_Autogiro_New, useOngoingSubscription: true);
            HttpContext.SetupRequestParameter("order", _order.Id.ToString());			
		}

    	private void EnOrderFörEnAnnanButik()
    	{
    		var otherShop = CreateShop<Shop>();
            _order = CreateOrderWithSubscription(otherShop, paymentOptionType: PaymentOptionType.Subscription_Autogiro_New);
            HttpContext.SetupRequestParameter("order", _order.Id.ToString());
    	}

        #endregion

        #region Act

        private void AnvändarenBekräftarBeställningen()
        {
            _presenter.View_Submit(null, new EventArgs());
        }

        private void AnvändarenAvbryterBeställningen()
        {
            _presenter.View_Abort(null, new EventArgs());
        }

        private void SidanVisas()
        {
            _thrownException = CatchExceptionWhile(() => _presenter.View_Load(null, new EventArgs()));
        }

        #endregion

        #region Assert
        private void TasAbonnemangetBort()
        {
			Get<Subscription>(_order.SubscriptionPayment.Subscription.Id).ShouldBe(null);
        }

        private void TasBeställningenBort()
        {
            Get<Order>(_order.Id).ShouldBe(null);
        }

        private void AnvändarenFlyttasTillIntranätsidan()
        {
            HttpContext.ResponseInstance.RedirectedUrl.ShouldBe(_abortUrl);
        }

        private void VisasEnSammanställningAvOrdern()
        {
            var order = WithRepository<IOrderRepository>().Get(_order.Id);
            View.Model.Address.ShouldBe(String.Format("{0} {1}", order.Customer.AddressLineOne ?? "", order.Customer.AddressLineTwo ?? ""));
            View.Model.City.ShouldBe(order.Customer.City);
            View.Model.Email.ShouldBe(order.Customer.Email ?? "");
            View.Model.FirstName.ShouldBe(order.Customer.FirstName);
            View.Model.LastName.ShouldBe(order.Customer.LastName);
            View.Model.MobilePhone.ShouldBe(order.Customer.MobilePhone ?? "");
            View.Model.PersonalIdNumber.ShouldBe(order.Customer.PersonalIdNumber);
            View.Model.PostalCode.ShouldBe(order.Customer.PostalCode);
            View.Model.Telephone.ShouldBe(order.Customer.Phone ?? "");

            View.Model.LeftAddition.ShouldBe(order.LensRecipe.Addition.Left);
            View.Model.LeftAxis.ShouldBe(order.LensRecipe.Axis != null ? order.LensRecipe.Axis.Left : "");
            View.Model.LeftPower.ShouldBe(order.LensRecipe.Power != null ? order.LensRecipe.Power.Left : "");
            View.Model.LeftBaseCurve.ShouldBe(order.LensRecipe.BaseCurve.Left != null ? order.LensRecipe.BaseCurve.Left.ToString() : "");
            View.Model.LeftDiameter.ShouldBe(order.LensRecipe.Diameter.Left != null ? order.LensRecipe.Diameter.Left.ToString() : "");
            View.Model.LeftCylinder.ShouldBe(order.LensRecipe.Cylinder.Left ?? "");
            View.Model.RightAddition.ShouldBe(order.LensRecipe.Addition.Right ?? "");
            View.Model.RightAxis.ShouldBe(order.LensRecipe.Axis != null ? order.LensRecipe.Axis.Right : "");
            View.Model.RightPower.ShouldBe(order.LensRecipe.Power != null ? order.LensRecipe.Power.Right : "");
            View.Model.RightBaseCurve.ShouldBe(order.LensRecipe.BaseCurve.Right != null ? order.LensRecipe.BaseCurve.Right.ToString() : "");
            View.Model.RightDiameter.ShouldBe(order.LensRecipe.Diameter.Right != null ? order.LensRecipe.Diameter.Right.ToString() : "");
            View.Model.RightCylinder.ShouldBe(order.LensRecipe.Cylinder.Right);

            View.Model.ArticleLeft.ShouldBe(order.LensRecipe.Article.Left.Name);
			View.Model.ArticleRight.ShouldBe(order.LensRecipe.Article.Right.Name);
        	View.Model.CustomerName.ShouldBe(order.Customer.FirstName + " " + order.Customer.LastName);
            View.Model.DeliveryOption.ShouldBe(order.ShippingType.GetEnumDisplayName());
            View.Model.ProductPrice.ShouldBe(order.SubscriptionPayment.Value.Taxed.ToString("C"));
            View.Model.FeePrice.ShouldBe(order.SubscriptionPayment.Value.TaxFree.ToString("C"));
            View.Model.TotalWithdrawal.ShouldBe(order.WithdrawalAmount.Total.ToString("C"));
			View.Model.Monthly.ShouldBe(order.SubscriptionPayment.MonthlyWithdrawal.Total.ToString("C"));
			if(_order.SubscriptionPayment.IsOngoing)
			{
				View.Model.SubscriptionTime.ShouldBe("Löpande");
			}
			else
			{
				View.Model.SubscriptionTime.ShouldBe(order.SubscriptionPayment.WithdrawalsLimit + " månader");
			}
			View.Model.QuantityLeft.ShouldBe(order.LensRecipe.Quantity.Left);
			View.Model.QuantityRight.ShouldBe(order.LensRecipe.Quantity.Right);

			View.Model.ExpectedFirstWithdrawalDate.ShouldBe(GetExpectedFirstWithdrawalDate(_order.SubscriptionPayment));
        }

		private string GetExpectedFirstWithdrawalDate(SubscriptionItem subscriptionItem)
		{
			var returnDate = new DateTime(
				subscriptionItem.CreatedDate.Year, 
				subscriptionItem.CreatedDate.Month, 
				_subscriptionWithdrawalDate);
			return subscriptionItem.CreatedDate.Day <= _subscriptionCutoffDate
				? returnDate.ToString("yyyy-MM-dd") 
				: returnDate.AddMonths(1).ToString("yyyy-MM-dd");
		}


    	private void AnvändarenFlyttasTillSidaFörFärdigBeställning()
        {
            var expectedUrl = _redirectUrl(_submitUrl, _order.Id);
            HttpContext.ResponseInstance.RedirectedUrl.ShouldBe(expectedUrl);
        }

    	private void SkapasEnTotalTransaktion()
    	{
    		var transaction = GetAll<SubscriptionTransaction>().Single();
			transaction.GetAmount().ShouldBe(_order.WithdrawalAmount);
			transaction.CreatedDate.Date.ShouldBe(DateTime.Now.Date);
			transaction.Reason.ShouldBe(TransactionReason.Withdrawal);
			transaction.SettlementId.ShouldBe(null);
			transaction.Subscription.Id.ShouldBe(_order.SubscriptionPayment.Subscription.Id);
			transaction.Type.ShouldBe(TransactionType.Withdrawal);
    	}

    	private void BeställningenByterStatusTillRedoFörAttSkickas()
    	{
			Get<Order>(_order.Id).Status.ShouldBe(OrderStatus.Confirmed);
    	}

    	private void NyttAbonnemangAktiveras()
    	{
			Get<Subscription>(_order.SubscriptionPayment.Subscription.Id).Active.ShouldBe(true);
    	}

    	private void KastasEttException()
    	{
    		_thrownException.ShouldBeTypeOf<AccessDeniedException>();
    	}

        #endregion

    }
}