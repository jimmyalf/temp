using System;
using System.Linq;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
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
    	private OrderCustomer _previousCustomer;
    	private string _customerNotFoundWithPersonalIdNumber;

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
                .När(AnvändarenFörsökerFortsättaTillNästaSteg)
				.Så(KundinformationSparas)
					.Och(FörflyttasAnvändarenTillVynFörNästaSteg)
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
        public void VisaFormulärFörBefintligKund()
        {
            SetupScenario(scenario => scenario
                .Givet(AttEnKundHittatsIFöregåendeSteg)
                .När(NärFormuläretLaddas)
                .Så(FyllsFormuläretMedKunduppgifter)
            );
        }

    	[Test]
        public void VisaFormulärFörIckeBefintligKund()
        {
            SetupScenario(scenario => scenario
                .Givet(AttEnKundEjHittatsIFöregåendeSteg)
                .När(NärFormuläretLaddas)
                .Så(FyllsFormuläretIMedPersonnummer)
					.Och(MeddelandeVisasAttKundSaknas)
            );
        }

    	private void MeddelandeVisasAttKundSaknas()
    	{
    		View.Model.DisplayCustomerMissingMessage.ShouldBe(true);
    	}

    	private void FyllsFormuläretIMedPersonnummer()
    	{
    		View.Model.PersonalIdNumber.ShouldBe(_customerNotFoundWithPersonalIdNumber);
    	}

    	private void AttEnKundEjHittatsIFöregåendeSteg()
    	{
    		_customerNotFoundWithPersonalIdNumber = "123456789";
    		HttpContext.SetupRequestParameter("personalIdNumber", _customerNotFoundWithPersonalIdNumber);
    	}

    	private void NärFormuläretLaddas()
    	{
    		_pickCustomerPresenter.View_Load(null, new EventArgs());
    	}

    	private void AttEnKundHittatsIFöregåendeSteg()
    	{
    		_previousCustomer = OrderFactory.GetCustomer();
			WithRepository<IOrderCustomerRepository>().Save(_previousCustomer);
    		HttpContext.SetupRequestParameter("customer", _previousCustomer.Id.ToString());
    	}

    	[Test]
        public void UppdateraBefintligKund()
        {
            SetupScenario(scenario => scenario
                .Givet(AttEnKundHittatsIFöregåendeSteg)
					.Och(AnvändarenUppdateratFormuläret)
                .När(AnvändarenFörsökerFortsättaTillNästaSteg)
                .Så(UppdaterasBefintligKund)
					.Och(FörflyttasAnvändarenTillVynFörNästaSteg)
            );
        }

    	private void AnvändarenUppdateratFormuläret()
    	{
    		_form = OrderFactory.GetOrderCustomerForm(_previousCustomer.Id);
    	}

    	private void UppdaterasBefintligKund()
    	{
    		var customer = WithRepository<IOrderCustomerRepository>().Get(_previousCustomer.Id);
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

		//private void EnBefintligKundsPersonuppgifterHämtats()
		//{
		//    _previousCustomer = OrderFactory.GetCustomer();
		//    WithRepository<IOrderCustomerRepository>().Save(_previousCustomer);

		//    _fetchByPersonalId = OrderFactory.GetPersonalIdForm();

		//    //_pickCustomerPresenter.FetchCustomerDataByPersonalIdNumber(null, _fetchByPersonalId);

		//    _form = OrderFactory.GetOrderCustomerForm(_previousCustomer.Id);
		//}

		//private void KundenFinnsSedanTidigare()
		//{
		//    _previousCustomer = OrderFactory.GetCustomer();
		//    WithRepository<IOrderCustomerRepository>().Save(_previousCustomer);
		//}

    	private void FyllsFormuläretMedKunduppgifter()
    	{
    		View.Model.FirstName.ShouldBe(_previousCustomer.FirstName);
			View.Model.LastName.ShouldBe(_previousCustomer.LastName);
			View.Model.PersonalIdNumber.ShouldBe(_previousCustomer.PersonalIdNumber);
			View.Model.Email.ShouldBe(_previousCustomer.Email);
			View.Model.MobilePhone.ShouldBe(_previousCustomer.MobilePhone);
			View.Model.Phone.ShouldBe(_previousCustomer.Phone);
			View.Model.AddressLineOne.ShouldBe(_previousCustomer.AddressLineOne);
			View.Model.AddressLineTwo.ShouldBe(_previousCustomer.AddressLineTwo);
			View.Model.City.ShouldBe(_previousCustomer.City);
			View.Model.PostalCode.ShouldBe(_previousCustomer.PostalCode);
			View.Model.Notes.ShouldBe(_previousCustomer.Notes);
    	}

		//private void AnvändarenKlickarHämta()
		//{
		//    _pickCustomerPresenter.FetchCustomerDataByPersonalIdNumber(null, _fetchByPersonalId);
		//}

		//private void AttEttPersonnummerÄrIfyllt()
		//{
		//    _fetchByPersonalId = OrderFactory.GetPersonalIdForm();
		//}

        private void FörflyttasAnvändarenTillVynFörNästaSteg()
        {
            //Assert redirect
			HttpContext.ResponseInstance.RedirectedUrl.ShouldBe(_testRedirectUrl);
        }

        private void AnvändarenFörsökerFortsättaTillNästaSteg()
        {
            _pickCustomerPresenter.View_Submit(null, _form);
        }

        private void AttFormuläretÄrKorrektIfyllt()
        {
        	_form = OrderFactory.GetOrderCustomerForm();
        }
    }
}