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
    [TestFixture, Category("Pick_Customer")]
	public class When_picking_a_customer : SpecTestbase<PickCustomerPresenter,IPickCustomerView>
    {
        private PickCustomerPresenter _pickCustomerPresenter;
    	private PickCustomerEventArgs _form;
    	private string _testRedirectUrl;

    	public When_picking_a_customer()
		{
            
			Context = () =>
			{
				_testRedirectUrl = "/test/page";
				View.NextPageId = 55;
				A.CallTo(() => SynologenMemberService.GetPageUrl(View.NextPageId)).Returns(_testRedirectUrl);
				_pickCustomerPresenter = GetPresenter();
			};
			Story = () => new Ber�ttelse("Spara kund")
			              	.F�rAtt("v�lja en kund att knyta till nytt abonnemang")
			              	.Som("inloggad anv�ndare p� intran�tet")
			              	.VillJag("spara information om anv�ndaren");
            
		}
        [Test]
        public void SparaNyKund()
        {
            SetupScenario(scenario => scenario
                .Givet(AttFormul�ret�rKorrektIfyllt)
                .N�r(Anv�ndarenF�rs�kerForts�ttaTillStegTv�)
				.S�(KundinformationSparas)
					.Och(F�rflyttasAnv�ndarenTillVynF�rStegTv�)
            );
        }

    	private void KundinformationSparas()
    	{
    		var customer = WithRepository<IOrderCustomerRepository>().GetAll().First();
    		customer.AddressLineOne.ShouldBe(_form.AddressLineOne);
			customer.AddressLineTwo.ShouldBe(_form.AddressLineTwo);
			customer.City.ShouldBe(_form.City);
			customer.Email.ShouldBe(_form.Email);
			customer.FirstName.ShouldBe(_form.FirstName);
			customer.LastName.ShouldBe(_form.LastName);
			customer.MobilePhone.ShouldBe(_form.MobilePhone);
			customer.Notes.ShouldBe(_form.Notes);
			customer.PersonalIdNumber.ShouldBe(_form.PersonalIdNumber);
			customer.Phone.ShouldBe(_form.Phone);
			customer.PostalCode.ShouldBe(_form.PostalCode);
    	}

    	[Test]
        public void UppdateraBefintligKund()
        {
            SetupScenario(scenario => scenario
                .Givet(AttEttPersonnummer�rIfyllt)
					.Och(KundenFinnsSedanTidigare)
                .N�r(Anv�ndarenKlickarH�mta)
                .S�(FyllsFormul�retMedKunduppgifter)
            );
        }

    	private void KundenFinnsSedanTidigare()
    	{
    		throw new NotImplementedException();
    	}

    	private void FyllsFormul�retMedKunduppgifter()
        {
            throw new NotImplementedException();
        }

        private void Anv�ndarenKlickarH�mta()
        {
            throw new NotImplementedException();
        }

        private void AttEttPersonnummer�rIfyllt()
        {
            throw new NotImplementedException();
        }

        private void F�rflyttasAnv�ndarenTillVynF�rStegTv�()
        {
            //Assert redirect
			HttpContext.ResponseInstance.RedirectedUrl.ShouldBe(_testRedirectUrl);
        }

        private void Anv�ndarenF�rs�kerForts�ttaTillStegTv�()
        {
            _pickCustomerPresenter.View_Submit(null, _form);
        }

        private void AttFormul�ret�rKorrektIfyllt()
        {
        	_form = OrderFactory.GetOrderCustomerForm();
        }
    }

	public static class OrderFactory
	{
		public static PickCustomerEventArgs GetOrderCustomerForm()
		{
			return new PickCustomerEventArgs
			{
				AddressLineOne = "Box 123",
				AddressLineTwo = "Datav�gen 2",
				City = "Askim",
				Email = "adam.bertil@testbolaget.se",
				FirstName = "Adam",
				LastName = "Bertil",
				MobilePhone = "0701-123456",
				Notes = "Anteckningar ABC",
				PersonalIdNumber = "197001013239",
				Phone = "0317483000",
				PostalCode = "43632",
			};
		}
	}
}