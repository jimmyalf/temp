using System;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.TestHelpers;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.Orders
{
    [TestFixture, Category("Payment_Options")]
    public class When_selecting_payment_options : SpecTestbase<PaymentOptionsPresenter, IPaymentOptionsView>
    {
        private PaymentOptionsPresenter _paymentOptionsPresenter;

        public When_selecting_payment_options()
        {
            Context = () =>
            {
                _paymentOptionsPresenter = GetPresenter();
            };
            Story = () =>
            {
                return new Berättelse("Ange betalningssätt")
                .FörAtt("Ange betalningssätt för beställningen")
                .Som("inloggad användare på intranätet")
                .VillJag("kunna välja betalningssätt");
            };
        }

        [Test]
        public void AngeBetalningssätt()
        {
            SetupScenario(scenario => scenario
                .Givet(AttFormuläretÄrKorrektIfyllt)
                .När(AnvändarenFörsökerFortsättaTillSteg4)
                .Så(FörflyttasAnvändarenTillVynFörSteg4)
            );
        }

        private void FörflyttasAnvändarenTillVynFörSteg4()
        {
            throw new NotImplementedException();
        }

        private void AnvändarenFörsökerFortsättaTillSteg4()
        {
            throw new NotImplementedException();
        }

        private void AttFormuläretÄrKorrektIfyllt()
        {
            throw new NotImplementedException();
        }
    }
}