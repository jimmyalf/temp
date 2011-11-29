using System;
using System.Linq;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.TestHelpers;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.Orders
{
    [TestFixture, Category("Create_Order")]
    public class When_creating_an_order : SpecTestbase<CreateOrderPresenter, ICreateOrderView>
    {
        private CreateOrderPresenter _createOrderPresenter;
        private CreateOrderEventArgs _form;
        private string _testRedirectUrl;

        public When_creating_an_order()
        {
            Context = () =>
            {
                _testRedirectUrl = "/test/page";
                View.NextPageId = 56;
                A.CallTo(() => SynologenMemberService.GetPageUrl(View.NextPageId)).Returns(_testRedirectUrl);
                _createOrderPresenter = GetPresenter();
            };

            Story = () =>
            {
                return new Berättelse("Spara beställning")
                .FörAtt("skapa en ny beställning")
                .Som("inloggad användare på intranätet")
                .VillJag("spara innehållet i den nya beställningen");
            };
        }

        [Test]
        public void SparaNyBeställning()
        {
            SetupScenario(scenario => scenario
                .Givet(AttFormuläretÄrKorrektIfyllt)
                .När(AnvändarenFörsökerFortsättaTillNästaSteg)
                .Så(SparasBeställningen)
                    .Och(AnvändarenFörflyttasTillVynFörNästaSteg));
        }
        
        private void AttFormuläretÄrKorrektIfyllt()
        {
            _form = OrderFactory.GetOrder();
        }
        
        private void AnvändarenFörsökerFortsättaTillNästaSteg()
        {
            _createOrderPresenter.View_Submit(null, _form);
        }

        private void SparasBeställningen()
        {
            var order = WithRepository<IOrderRepository>().GetAll().First();

            order.ArticleId.ShouldBe(_form.ArticleId);
            order.CategoryId.ShouldBe(_form.CategoryId);
            order.LeftBaseCurve.ShouldBe(_form.LeftBaseCurve);
            order.LeftDiameter.ShouldBe(_form.LeftDiameter);
            order.LeftPower.ShouldBe(_form.LeftPower);
            order.RightBaseCurve.ShouldBe(_form.RightBaseCurve);
            order.RightDiameter.ShouldBe(_form.RightDiameter);
            order.RightPower.ShouldBe(_form.RightPower);
            order.ShipmentOption.ShouldBe(_form.ShipmentOption);
            order.SupplierId.ShouldBe(_form.SupplierId);
            order.TypeId.ShouldBe(_form.TypeId);
        }

        private void AnvändarenFörflyttasTillVynFörNästaSteg()
        {
            HttpContext.ResponseInstance.RedirectedUrl.ShouldBe(_testRedirectUrl);
        }
    }
}
