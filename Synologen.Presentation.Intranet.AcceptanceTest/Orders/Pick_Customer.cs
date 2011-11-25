using System;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.TestHelpers;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.Orders.TestHelpers
{
    [TestFixture, Category("View_Shop_Subscription_Summary")]
	public class When_picking_a_customer : SpecTestbase<PickCustomerPresenter,IPickCustomerView>
    {
        private PickCustomerPresenter _pickCustomerPresenter;

        public When_picking_a_customer()
		{
            
			Context = () =>
			{
				_pickCustomerPresenter = GetPresenter();
			};
			Story = () =>
			{
                return new Ber�ttelse("V�lja kund")
                    .F�rAtt("v�lja en kund att knyta till nytt abonnemang")
                    .Som("inloggad anv�ndare p� intran�tet")
                    .VillJag("kunna spara n�dv�ndig information om anv�ndaren");
			};
            
		}
        [Test]
        public void Informationen�rKorrektIfylld()
        {
            SetupScenario(scenario => scenario
                .Givet(AttFormul�ret�rKorrektIfyllt)
                .N�r(Anv�ndarenF�rs�kerForts�ttaTillStegTv�)
                .S�(F�rflyttasAnv�ndarenTillVynF�rStegTv�)
            );
        }

        private void F�rflyttasAnv�ndarenTillVynF�rStegTv�()
        {
            throw new NotImplementedException();
        }

        private void Anv�ndarenF�rs�kerForts�ttaTillStegTv�()
        {
            throw new NotImplementedException();
        }

        private void AttFormul�ret�rKorrektIfyllt()
        {
            throw new NotImplementedException();
        }
    }   
}