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
    [TestFixture, Category("Save_Customer")]
	public class When_picking_a_customer : SpecTestbase<SaveCustomerPresenter,ISaveCustomerView>
    {
        private SaveCustomerPresenter _saveCustomerPresenter;
    	private SaveCustomerEventArgs _form;
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
				_saveCustomerPresenter = GetPresenter();
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
                .N�r(Anv�ndarenF�rs�kerForts�ttaTillN�staSteg)
				.S�(KundinformationSparas)
					.Och(F�rflyttasAnv�ndarenTillVynF�rN�staSteg)
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
        public void VisaFormul�rF�rBefintligKund()
        {
            SetupScenario(scenario => scenario
                .Givet(AttEnKundHittatsIF�reg�endeSteg)
                .N�r(N�rFormul�retLaddas)
                .S�(FyllsFormul�retMedKunduppgifter)
            );
        }

    	[Test]
        public void VisaFormul�rF�rIckeBefintligKund()
        {
            SetupScenario(scenario => scenario
                .Givet(AttEnKundEjHittatsIF�reg�endeSteg)
                .N�r(N�rFormul�retLaddas)
                .S�(FyllsFormul�retIMedPersonnummer)
					.Och(MeddelandeVisasAttKundSaknas)
            );
        }


    	private void MeddelandeVisasAttKundSaknas()
    	{
    		View.Model.DisplayCustomerMissingMessage.ShouldBe(true);
    	}

    	private void FyllsFormul�retIMedPersonnummer()
    	{
    		View.Model.PersonalIdNumber.ShouldBe(_customerNotFoundWithPersonalIdNumber);
    	}

    	private void AttEnKundEjHittatsIF�reg�endeSteg()
    	{
    		_customerNotFoundWithPersonalIdNumber = "123456789";
    		HttpContext.SetupRequestParameter("personalIdNumber", _customerNotFoundWithPersonalIdNumber);
    	}

    	private void N�rFormul�retLaddas()
    	{
    		_saveCustomerPresenter.View_Load(null, new EventArgs());
    	}

    	private void AttEnKundHittatsIF�reg�endeSteg()
    	{
    		_previousCustomer = OrderFactory.GetCustomer();
			WithRepository<IOrderCustomerRepository>().Save(_previousCustomer);
    		HttpContext.SetupRequestParameter("customer", _previousCustomer.Id.ToString());
    	}

    	[Test]
        public void UppdateraBefintligKund()
        {
            SetupScenario(scenario => scenario
                .Givet(AttEnKundHittatsIF�reg�endeSteg)
					.Och(Anv�ndarenUppdateratFormul�ret)
                .N�r(Anv�ndarenF�rs�kerForts�ttaTillN�staSteg)
                .S�(UppdaterasBefintligKund)
					.Och(F�rflyttasAnv�ndarenTillVynF�rN�staSteg)
            );
        }

    	private void Anv�ndarenUppdateratFormul�ret()
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

    	private void FyllsFormul�retMedKunduppgifter()
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

        private void F�rflyttasAnv�ndarenTillVynF�rN�staSteg()
        {
            //Assert redirect
			HttpContext.ResponseInstance.RedirectedUrl.ShouldBe(_testRedirectUrl);
        }

        private void Anv�ndarenF�rs�kerForts�ttaTillN�staSteg()
        {
            _saveCustomerPresenter.View_Submit(null, _form);
        }

        private void AttFormul�ret�rKorrektIfyllt()
        {
        	_form = OrderFactory.GetOrderCustomerForm();
        }
    }
}