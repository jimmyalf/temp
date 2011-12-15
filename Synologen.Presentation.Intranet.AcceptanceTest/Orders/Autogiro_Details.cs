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
                return new Berättelse("Ange autogirodetaljer")
                .FörAtt("Ange detaljer för autogiro")
                .Som("inloggad användare på intranätet")
                .VillJag("kunna ange detaljer för autogiro");
            };
        }

        [Test]
        public void AngeDetaljerFörAutogiro()
        {
            SetupScenario(scenario => scenario
                .Givet(AttFormuläretÄrKorrektIfyllt)
                .När(AnvändarenFörsökerFortsättaTillSteg5)
                .Så(FörflyttasAnvändarenTillVynFörSteg5)
            );
        }

        [Test]
        public void AvbrytBeställning()
        {
            SetupScenario(scenario => scenario
                .Givet(AttAnvändarenStårIVynFörAttAngeAutogirodetaljer)
                    .Och(EnBeställningHarSkapats)
                .När(AnvändarenAvbryterBeställningen)
                .Så(TasBeställningenBort)
                    .Och(AnvändarenFlyttasTillIntranätsidan));
        }

        #region Arrange
        private void AttAnvändarenStårIVynFörAttAngeAutogirodetaljer()
        {
            throw new NotImplementedException();
        }

        private void EnBeställningHarSkapats()
        {
            throw new NotImplementedException();
        }
        private void AttFormuläretÄrKorrektIfyllt()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Act
        private void AnvändarenAvbryterBeställningen()
        {
            throw new NotImplementedException();
        }
        private void AnvändarenFörsökerFortsättaTillSteg5()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Assert
        private void TasBeställningenBort()
        {
            throw new NotImplementedException();
        }

        private void AnvändarenFlyttasTillIntranätsidan()
        {
            throw new NotImplementedException();
        }
        private void FörflyttasAnvändarenTillVynFörSteg5()
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}