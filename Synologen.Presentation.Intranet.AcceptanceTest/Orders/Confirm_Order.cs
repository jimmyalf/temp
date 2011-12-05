using System;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.TestHelpers;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.Orders
{
    [TestFixture, Category("Confirm_Order")]
    public class When_confirming_an_order : SpecTestbase<CreateOrderConfirmationPresenter, ICreateOrderConfirmationView>
    {
        private CreateOrderConfirmationPresenter _createOrderConfirmationPresenter;

        public When_confirming_an_order()
        {
            Context = () =>
            {
                _createOrderConfirmationPresenter = GetPresenter();
            };

            Story = () =>
            {
                return new Ber�ttelse("Bekr�fta best�llning")
                .F�rAtt("Bekr�fta och skicka iv�g en best�llning")
                .Som("inloggad anv�ndare p� intran�tet")
                .VillJag("kunna se en sammanfattning av best�llningen")
                .Och("bekr�fta den");
            };
        }

        [Test]
        public void EnSammanfattningAvBest�llningenVisas()
        {
            Assert.Inconclusive("TODO");
        }

        //TODO: Det h�r �r en abstraktion av den funktionalitet som triggas n�r man bekr�ftar best�llningen. Bryt ner i delar.
        [Test]
        public void Best�llningenBekr�ftas()
        {
            Assert.Inconclusive("TODO");
        }
    }
}