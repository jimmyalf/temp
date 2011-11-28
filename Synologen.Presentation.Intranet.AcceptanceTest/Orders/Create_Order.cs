using System;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.TestHelpers;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.Orders
{
    [TestFixture, Category("Create_Order")]
    public class When_creating_an_order : SpecTestbase<CreateOrderPresenter, ICreateOrderView>
    {
        private CreateOrderPresenter _createOrderPresenter;

        public When_creating_an_order()
        {
            Context = () =>
            {
                _createOrderPresenter = GetPresenter();
            };

            Story = () =>
            {
                return new Berättelse("Skapa beställning")
                .FörAtt("skapa en ny beställning")
                .Som("inloggad användare på intranätet")
                .VillJag("välja vad beställningen ska innehålla");
            };
        }

        [Test]
        public void SkapaNyBeställning()
        {
            SetupScenario(scenario => scenario
                .Givet(AttFormuläretÄrKorrektIfyllt)
                .När(AnvändarenFörsökerFortsättaTillSteg3)
                .Så(FörflyttasAnvändarenTillVynFörSteg3));
        }

        private void FörflyttasAnvändarenTillVynFörSteg3()
        {
            throw new NotImplementedException();
        }

        private void AnvändarenFörsökerFortsättaTillSteg3()
        {
            throw new NotImplementedException();
        }

        private void AttFormuläretÄrKorrektIfyllt()
        {
            throw new NotImplementedException();
        }
    }
}
