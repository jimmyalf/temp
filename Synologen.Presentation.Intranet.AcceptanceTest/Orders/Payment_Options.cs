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
                return new Ber�ttelse("Ange betalningss�tt")
                .F�rAtt("Ange betalningss�tt f�r best�llningen")
                .Som("inloggad anv�ndare p� intran�tet")
                .VillJag("kunna v�lja betalningss�tt");
            };
        }

        [Test]
        public void AngeBetalningss�tt()
        {
            SetupScenario(scenario => scenario
                .Givet(AttFormul�ret�rKorrektIfyllt)
                    .Och(EnBest�llningHarSkapatsIF�reg�endeSteg)
                .N�r(Anv�ndarenF�rs�kerForts�ttaTillSteg4)
                .S�(F�rflyttasAnv�ndarenTillVynF�rSteg4));
        }

        [Test]
        public void AvbrytBest�llning()
        {
            SetupScenario(scenario => scenario
                .Givet(AttAnv�ndarenSt�rIVynF�rAttAngeBetalningss�tt)
                    .Och(EnBest�llningHarSkapatsIF�reg�endeSteg)
                .N�r(Anv�ndarenAvbryterBest�llningen)
                .S�(TasBest�llningenBort)
                    .Och(Anv�ndarenFlyttasTillIntran�tsidan));
        }

        #region Arrange
        private void AttAnv�ndarenSt�rIVynF�rAttAngeBetalningss�tt()
        {
            throw new NotImplementedException();
        }
        private void EnBest�llningHarSkapatsIF�reg�endeSteg()
        {
            throw new NotImplementedException();
        }
        private void AttFormul�ret�rKorrektIfyllt()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Act
        private void Anv�ndarenAvbryterBest�llningen()
        {
            throw new NotImplementedException();
        }
        private void Anv�ndarenF�rs�kerForts�ttaTillSteg4()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Assert
        private void Anv�ndarenFlyttasTillIntran�tsidan()
        {
            throw new NotImplementedException();
        }

        private void TasBest�llningenBort()
        {
            throw new NotImplementedException();
        }

        private void F�rflyttasAnv�ndarenTillVynF�rSteg4()
        {
            throw new NotImplementedException();
        }
        #endregion


    }
}