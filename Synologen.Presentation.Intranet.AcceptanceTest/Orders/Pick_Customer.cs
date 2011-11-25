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
			Story = () => new Berättelse("Spara kund")
			              	.FörAtt("välja en kund att knyta till nytt abonnemang")
			              	.Som("inloggad användare på intranätet")
			              	.VillJag("spara information om användaren");
            
		}
        [Test]
        public void SparaNyKund()
        {
            SetupScenario(scenario => scenario
                .Givet(AttFormuläretÄrKorrektIfyllt)
                .När(AnvändarenFörsökerFortsättaTillStegTvå)
				.Så(KundinformationSparas)
					.Och(FörflyttasAnvändarenTillVynFörStegTvå)
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
                .Givet(AttEttPersonnummerÄrIfyllt)
					.Och(KundenFinnsSedanTidigare)
                .När(AnvändarenKlickarHämta)
                .Så(FyllsFormuläretMedKunduppgifter)
            );
        }

    	private void KundenFinnsSedanTidigare()
    	{
    		throw new NotImplementedException();
    	}

    	private void FyllsFormuläretMedKunduppgifter()
        {
            throw new NotImplementedException();
        }

        private void AnvändarenKlickarHämta()
        {
            throw new NotImplementedException();
        }

        private void AttEttPersonnummerÄrIfyllt()
        {
            throw new NotImplementedException();
        }

        private void FörflyttasAnvändarenTillVynFörStegTvå()
        {
            //Assert redirect
			HttpContext.ResponseInstance.RedirectedUrl.ShouldBe(_testRedirectUrl);
        }

        private void AnvändarenFörsökerFortsättaTillStegTvå()
        {
            _pickCustomerPresenter.View_Submit(null, _form);
        }

        private void AttFormuläretÄrKorrektIfyllt()
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
				AddressLineTwo = "Datavägen 2",
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