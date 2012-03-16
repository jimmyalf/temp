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

    	public When_confirming_an_order()
        {
            Context = () =>
            {
                _presenter = GetPresenter();
                _shop = CreateShop<Shop>();
                _previousUrl = "/previous/page";
                _submitUrl = "/next/page";
                _abortUrl = "/abort/page";
  
                A.CallTo(() => SynologenMemberService.GetCurrentShopId()).Returns(_shop.Id);
                SetupNavigationEvents(_previousUrl, _abortUrl, _submitUrl);
                _redirectUrl = (url, orderId) => "{url}?order={orderId}".ReplaceWith(new { url, orderId });
            };

            Story = () => new Ber�ttelse("Bekr�fta best�llning")
				.F�rAtt("Bekr�fta och skicka iv�g en best�llning")
				.Som("inloggad anv�ndare p� intran�tet")
				.VillJag("kunna se en sammanfattning av best�llningen")
					.Och("bekr�fta den");
        }

        [Test]
        public void EnSammanfattningAvBest�llningenVisas()
        {
            SetupScenario(scenario => scenario
                .Givet(EnBest�llningMedEttAbonnemangHarSkapats)
                .N�r(SidanVisas)
                .S�(VisasEnSammanst�llningAvOrdern));
        }

        [Test]
        public void AvbrytBest�llning()
        {
            SetupScenario(scenario => scenario
                .Givet(EnBest�llningMedEttAbonnemangHarSkapats)
                .N�r(Anv�ndarenAvbryterBest�llningen)
                .S�(TasAbonnemangetBort)
                    .Och(TasBest�llningenBort)
                    .Och(Anv�ndarenFlyttasTillIntran�tsidan));
        }

        [Test]
        public void SparaBest�llningMedNyttAbonnemang()
        {
            SetupScenario(scenario => scenario
                .Givet(EnBest�llningMedEttAbonnemangHarSkapats)
                .N�r(Anv�ndarenBekr�ftarBest�llningen)
				.S�(SkapasEnTotalTransaktion)
					.Och(Anv�ndarenFlyttasTillSidaF�rF�rdigBest�llning)
					.Och(Best�llningenByterStatusTillRedoF�rAttSkickas)
					.Och(NyttAbonnemangAktiveras)
			);
        }

        [Test]
        public void VisaSidaF�rAnnanButiksOrder()
        {
            SetupScenario(scenario => scenario
                .Givet(EnOrderF�rEnAnnanButik)
                .N�r(SidanVisas)
                .S�(KastasEttException));
        }

    	#region Arrange

        private void EnBest�llningMedEttAbonnemangHarSkapats()
        {
            _order = CreateOrderWithSubscription(_shop, paymentOptionType: PaymentOptionType.Subscription_Autogiro_New);
            HttpContext.SetupRequestParameter("order", _order.Id.ToString());
        }

    	private void EnOrderF�rEnAnnanButik()
    	{
    		var otherShop = CreateShop<Shop>();
            _order = CreateOrderWithSubscription(otherShop, paymentOptionType: PaymentOptionType.Subscription_Autogiro_New);
            HttpContext.SetupRequestParameter("order", _order.Id.ToString());
    	}

        #endregion

        #region Act

        private void Anv�ndarenBekr�ftarBest�llningen()
        {
            _presenter.View_Submit(null, new EventArgs());
        }

        private void Anv�ndarenAvbryterBest�llningen()
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

        private void TasBest�llningenBort()
        {
            Get<Order>(_order.Id).ShouldBe(null);
        }

        private void Anv�ndarenFlyttasTillIntran�tsidan()
        {
            HttpContext.ResponseInstance.RedirectedUrl.ShouldBe(_abortUrl);
        }

        private void VisasEnSammanst�llningAvOrdern()
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
            View.Model.ProductPrice.ShouldBe(order.SubscriptionPayment.ProductPrice.ToString("C"));
            View.Model.FeePrice.ShouldBe(order.SubscriptionPayment.FeePrice.ToString("C"));
            View.Model.TotalWithdrawal.ShouldBe(order.OrderTotalWithdrawalAmount.ToString("C"));
			View.Model.Monthly.ShouldBe(order.SubscriptionPayment.MonthlyWithdrawalAmount.ToString("C"));
            View.Model.SubscriptionTime.ShouldBe(order.SubscriptionPayment.WithdrawalsLimit + " m�nader");
			View.Model.QuantityLeft.ShouldBe(order.LensRecipe.Quantity.Left);
			View.Model.QuantityRight.ShouldBe(order.LensRecipe.Quantity.Right);
        }


    	private void Anv�ndarenFlyttasTillSidaF�rF�rdigBest�llning()
        {
            var expectedUrl = _redirectUrl(_submitUrl, _order.Id);
            HttpContext.ResponseInstance.RedirectedUrl.ShouldBe(expectedUrl);
        }

    	private void SkapasEnTotalTransaktion()
    	{
    		var transaction = GetAll<SubscriptionTransaction>().Single();
			transaction.Amount.ShouldBe(_order.OrderTotalWithdrawalAmount);
			transaction.CreatedDate.Date.ShouldBe(DateTime.Now.Date);
			transaction.Reason.ShouldBe(TransactionReason.Withdrawal);
			transaction.SettlementId.ShouldBe(null);
			transaction.Subscription.Id.ShouldBe(_order.SubscriptionPayment.Subscription.Id);
			transaction.Type.ShouldBe(TransactionType.Withdrawal);
    	}

    	private void Best�llningenByterStatusTillRedoF�rAttSkickas()
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