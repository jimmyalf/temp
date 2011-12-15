using System;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.TestHelpers;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.Orders
{
    [TestFixture, Category("Autogiro_Details")]
    public class When_entering_autogiro_details : SpecTestbase<AutogiroDetailsPresenter, IAutogiroDetailsView>
    {
        private AutogiroDetailsPresenter _autogiroDetailsPresenter;

        public When_entering_autogiro_details()
        {
            Context = () =>
            {
                _autogiroDetailsPresenter = GetPresenter();
            };

            Story = () =>
            {
                return new Ber�ttelse("Ange autogirodetaljer")
                .F�rAtt("Ange detaljer f�r autogiro")
                .Som("inloggad anv�ndare p� intran�tet")
                .VillJag("kunna ange detaljer f�r autogiro");
            };
        }

        [Test]
        public void AngeDetaljerF�rAutogiro()
        {
            SetupScenario(scenario => scenario
                .Givet(AttFormul�ret�rKorrektIfyllt)
                .N�r(Anv�ndarenF�rs�kerForts�ttaTillSteg5)
                .S�(F�rflyttasAnv�ndarenTillVynF�rSteg5)
            );
        }

        [Test]
        public void AvbrytBest�llning()
        {
            SetupScenario(scenario => scenario
                .Givet(AttAnv�ndarenSt�rIVynF�rAttAngeAutogirodetaljer)
                    .Och(EnBest�llningHarSkapats)
                .N�r(Anv�ndarenAvbryterBest�llningen)
                .S�(TasBest�llningenBort)
                    .Och(Anv�ndarenFlyttasTillIntran�tsidan));
        }

        #region Arrange
        private void AttAnv�ndarenSt�rIVynF�rAttAngeAutogirodetaljer()
        {
            throw new NotImplementedException();
        }

        private void EnBest�llningHarSkapats()
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
        private void Anv�ndarenF�rs�kerForts�ttaTillSteg5()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Assert
        private void TasBest�llningenBort()
        {
            throw new NotImplementedException();
        }

        private void Anv�ndarenFlyttasTillIntran�tsidan()
        {
            throw new NotImplementedException();
        }
        private void F�rflyttasAnv�ndarenTillVynF�rSteg5()
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}