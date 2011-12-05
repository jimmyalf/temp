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
                return new Berättelse("Bekräfta beställning")
                .FörAtt("Bekräfta och skicka iväg en beställning")
                .Som("inloggad användare på intranätet")
                .VillJag("kunna se en sammanfattning av beställningen")
                .Och("bekräfta den");
            };
        }

        [Test]
        public void EnSammanfattningAvBeställningenVisas()
        {
            Assert.Inconclusive("TODO");
        }

        //TODO: Det här är en abstraktion av den funktionalitet som triggas när man bekräftar beställningen. Bryt ner i delar.
        [Test]
        public void BeställningenBekräftas()
        {
            Assert.Inconclusive("TODO");
        }
    }
}