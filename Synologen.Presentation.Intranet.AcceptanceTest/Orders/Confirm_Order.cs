using System;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
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
        private Subscription _subscription;
        private CreateOrderConfirmationPresenter _presenter;
        private string _previousUrl;
        private string _submitUrl;
        private string _abortUrl;
        private Func<string, int, string> _redirectUrl;
        private int _expectedEmailId;

        public When_confirming_an_order()
        {
            Context = () =>
            {
                _presenter = GetPresenter();
                _shop = CreateShop<Shop>();
                _expectedEmailId = 1;
                _previousUrl = "/previous/page";
                _submitUrl = "/next/page";
                _abortUrl = "/abort/page";
  
                A.CallTo(() => SynologenMemberService.GetCurrentShopId()).Returns(_shop.Id);
                
                A.CallTo(() => SendOrderService.SendOrderByEmail(_order)).Returns(_expectedEmailId);

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
        public void EnSammanfattningAvBeställningenVisas()
        {
            SetupScenario(scenario => scenario
                .Givet(EnBeställningMedEttAbonnemangHarSkapats)
                .När(AnvändarenVisarFormuläretFörAttBekräfta)
                .Så(VisasEnSammanställningAvOrdern));
        }

        [Test]
        public void AvbrytBeställning()
        {
            SetupScenario(scenario => scenario
                .Givet(Ingenting)
                    .Och(EnBeställningMedEttAbonnemangHarSkapats)
                .När(AnvändarenAvbryterBeställningen)
                .Så(TasAbonnemangetBort)
                    .Och(TasBeställningenBort)
                    .Och(AnvändarenFlyttasTillIntranätsidan));
        }

        [Test]
        public void BeställningenSkickasTillLeverantör()
        {
            SetupScenario(scenario => scenario
                .Givet(EnBeställningMedEttAbonnemangHarSkapats)
                .När(AnvändarenBekräftarBeställningen)
				.Så(AnvändarenFlyttasTillSidaFörFärdigBeställning)
			);
        }

        #region Arrange
        private void Ingenting()
        {
        }

        private void EnBeställningMedEttAbonnemangHarSkapats()
        {
            _order = CreateOrderWithSubscription(_shop);
            _subscription = _order.SubscriptionPayment.Subscription;
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

        private void AnvändarenVisarFormuläretFörAttBekräfta()
        {
            _presenter.View_Load(null, new EventArgs());
        }

        #endregion

        #region Assert
        private void TasAbonnemangetBort()
        {
            WithRepository<ISubscriptionRepository>().Get(_subscription.Id).ShouldBe(null);
        }

        private void TasBeställningenBort()
        {
            WithRepository<IOrderRepository>().Get(_order.Id).ShouldBe(null);
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

            View.Model.LeftAddition.ShouldBe(order.LensRecipe.Addition.Left != null ? order.LensRecipe.Addition.Left.ToString() : "");
            View.Model.LeftAxis.ShouldBe(order.LensRecipe.Axis != null ? order.LensRecipe.Axis.Left.ToString() : "");
            View.Model.LeftPower.ShouldBe(order.LensRecipe.Power != null ? order.LensRecipe.Power.Left.ToString() : "");
            View.Model.LeftBaseCurve.ShouldBe(order.LensRecipe.BaseCurve.Left != null ? order.LensRecipe.BaseCurve.Left.ToString() : "");
            View.Model.LeftDiameter.ShouldBe(order.LensRecipe.Diameter.Left != null ? order.LensRecipe.Diameter.Left.ToString() : "");
            View.Model.LeftCylinder.ShouldBe(order.LensRecipe.Cylinder.Left != null ? order.LensRecipe.Cylinder.Left.ToString() : "");
            View.Model.RightAddition.ShouldBe(order.LensRecipe.Addition.Right != null ? order.LensRecipe.Addition.Right.ToString() : "");
            View.Model.RightAxis.ShouldBe(order.LensRecipe.Axis != null ? order.LensRecipe.Axis.Right.ToString() : "");
            View.Model.RightPower.ShouldBe(order.LensRecipe.Power != null ? order.LensRecipe.Power.Right.ToString() : "");
            View.Model.RightBaseCurve.ShouldBe(order.LensRecipe.BaseCurve.Right != null ? order.LensRecipe.BaseCurve.Right.ToString() : "");
            View.Model.RightDiameter.ShouldBe(order.LensRecipe.Diameter.Right != null ? order.LensRecipe.Diameter.Right.ToString() : "");
            View.Model.RightCylinder.ShouldBe(order.LensRecipe.Cylinder.Right != null ? order.LensRecipe.Cylinder.Right.ToString() : "");

            View.Model.Article.ShouldBe(order.Article.Name);
            //View.Model.PaymentOption.ShouldBe(order.SelectedPaymentOption.Type.ToString());
        	View.Model.CustomerName.ShouldBe(order.Customer.FirstName + " " + order.Customer.LastName);
            View.Model.DeliveryOption.ShouldBe(_presenter.GetDeliveryOptionString(order.ShippingType));
            View.Model.TaxedAmount.ShouldBe(order.SubscriptionPayment.TaxedAmount.ToString("C"));
            View.Model.TaxfreeAmount.ShouldBe(order.SubscriptionPayment.TaxFreeAmount.ToString("C"));
            View.Model.TotalWithdrawal.ShouldBe(order.OrderTotalWithdrawalAmount.Value.ToString("C"));
			View.Model.Monthly.ShouldBe(order.SubscriptionPayment.AmountForAutogiroWithdrawal.ToString("C"));
            View.Model.SubscriptionTime.ShouldBe(_presenter.GetSubscriptionTimeString(order.SubscriptionPayment.WithdrawalsLimit)); 
        }

		//private void FlaggasOrderFörAttBliSkickadSomEpost()
		//{
		//    WithRepository<IOrderRepository>().Get(_order.Id).SendEmailForThisOrder.ShouldBe(true);
		//}

        private void AnvändarenFlyttasTillSidaFörFärdigBeställning()
        {
            var expectedUrl = _redirectUrl(_submitUrl, _order.Id);
            HttpContext.ResponseInstance.RedirectedUrl.ShouldBe(expectedUrl);
        }

        #endregion

    }
}