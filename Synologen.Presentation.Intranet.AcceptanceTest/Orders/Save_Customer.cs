using System;
using System.Linq;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Exceptions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.TestHelpers;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.Orders
{
    [TestFixture, Category("Save_Customer")]
	public class When_picking_a_customer : OrderSpecTestbase<SaveCustomerPresenter,ISaveCustomerView>
    {
        private SaveCustomerPresenter _saveCustomerPresenter;
    	private SaveCustomerEventArgs _form;
    	private string _submitRedirectUrl, _abortRedirectUrl, _previousRedirectUrl;
    	private OrderCustomer _customer;
        private Order _order;
    	private string _customerNotFoundWithPersonalIdNumber;
    	private Shop _shop;
    	private Exception _thrownException;

    	public When_picking_a_customer()
		{
			Context = () =>
			{
				_submitRedirectUrl = "/submit/page";
				_abortRedirectUrl = "/abort/page";
				_previousRedirectUrl = "/previous/page";
				_shop = CreateShop<Shop>();
				A.CallTo(() => SynologenMemberService.GetCurrentShopId()).Returns(_shop.Id);
				SetupNavigationEvents(_previousRedirectUrl,_abortRedirectUrl, _submitRedirectUrl);
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
        public void VisaFormul�rF�rBefintligKundViaOrderId()
        {
            SetupScenario(scenario => scenario
                .Givet(EnOrder)
                .N�r(N�rFormul�retLaddas)
                .S�(FyllsFormul�retMedKunduppgifter));
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

        [Test]
        public void AvbrytBest�llning()
        {
            SetupScenario(scenario => scenario
                .Givet(AttAnv�ndarenSt�rIVynF�rAttSparaKund)
                .N�r(Anv�ndarenAvbryterBest�llningen)
                .Och(FlyttasAnv�ndarenTillAvbrytSidan));
        }

        [Test]
        public void AvbrytBest�llningMedExisterandeOrder()
        {
            SetupScenario(scenario => scenario
                .Givet(AttAnv�ndarenSt�rIVynF�rAttSparaKund)
                    .Och(EnOrder)
                .N�r(Anv�ndarenAvbryterBest�llningen)
                .S�(TasOrdernBort)
                    .Och(FlyttasAnv�ndarenTillAvbrytSidan));
        }

        [Test]
        public void G�TillF�reg�endeSteg()
        {
            SetupScenario(scenario => scenario
                .Givet(AttAnv�ndarenSt�rIVynF�rAttSparaKund)
                .N�r(Anv�ndarenKlickarP�F�reg�endeSteg)
                .S�(F�rflyttasAnv�ndarenTillVynF�rF�reg�endeSteg));
        }

        [Test]
        public void G�TillF�reg�endeStegMedExisterandeOrder()
        {
            SetupScenario(scenario => scenario
                .Givet(AttAnv�ndarenSt�rIVynF�rAttSparaKund)
                    .Och(EnOrder)
                .N�r(Anv�ndarenKlickarP�F�reg�endeSteg)
                .S�(TasOrdernBort)
                    .Och(F�rflyttasAnv�ndarenTillVynF�rF�reg�endeSteg));
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

        [Test]
        public void UppdateraBefintligKundMedBefintligOrder()
        {
            SetupScenario(scenario => scenario
                .Givet(EnOrder)
                    .Och(Anv�ndarenUppdateratFormul�ret)
                .N�r(Anv�ndarenF�rs�kerForts�ttaTillN�staSteg)
                .S�(UppdaterasBefintligKund)
                    .Och(F�rflyttasAnv�ndarenTillVynF�rN�staStegMedOrderIdAngivet));
        }

        [Test]
        public void VisaSidaF�rAnnanButiksOrder()
        {
            SetupScenario(scenario => scenario
                .Givet(EnOrderF�rEnAnnanButik)
                .N�r(N�rFormul�retLaddas)
                .S�(KastasEttException));
        }

    	#region Arrange
        private void AttEnKundEjHittatsIF�reg�endeSteg()
        {
            _customerNotFoundWithPersonalIdNumber = "123456789";
            HttpContext.SetupRequestParameter("personalIdNumber", _customerNotFoundWithPersonalIdNumber);
        }
        private void AttEnKundHittatsIF�reg�endeSteg()
        {
        	_customer = CreateCustomer(_shop);
            HttpContext.SetupRequestParameter("customer", _customer.Id.ToString());
        }

		//private void AttEnKundHittatsViaOrderId()
		//{
		//    _customer = CreateCustomer(_shop);
		//    _order = CreateOrder(_shop, _customer);
		//    HttpContext.SetupRequestParameter("order", _order.Id.ToString());
		//}

        private void EnOrder()
        {
            _customer = CreateCustomer(_shop);
            _order = CreateOrder(_shop, _customer);
            HttpContext.SetupRequestParameter("order", _order.Id.ToString());
        }

    	private void EnOrderF�rEnAnnanButik()
    	{
    		var otherShop = CreateShop<Shop>();
            _customer = CreateCustomer(otherShop);
            _order = CreateOrder(otherShop, _customer);
            HttpContext.SetupRequestParameter("order", _order.Id.ToString());
    	}

        private void AttAnv�ndarenSt�rIVynF�rAttSparaKund()
        {
        }

        private void Anv�ndarenUppdateratFormul�ret()
        {
            _form = OrderFactory.GetOrderCustomerForm(/*_customer.Id*/);
        }
        private void AttFormul�ret�rKorrektIfyllt()
        {
            _form = OrderFactory.GetOrderCustomerForm();
        }
        #endregion
        
        #region Act
        private void N�rFormul�retLaddas()
        {
			_thrownException = CatchExceptionWhile(() => _saveCustomerPresenter.View_Load(null, new EventArgs()));
        }
        private void Anv�ndarenAvbryterBest�llningen()
        {
            _saveCustomerPresenter.View_Abort(null, new EventArgs());
        }
        private void Anv�ndarenKlickarP�F�reg�endeSteg()
        {
            _saveCustomerPresenter.View_Previous(null, new EventArgs());
        }
        private void Anv�ndarenF�rs�kerForts�ttaTillN�staSteg()
        {
            _saveCustomerPresenter.View_Submit(null, _form);
        }
        #endregion

        #region Assert
        private void FyllsFormul�retIMedPersonnummer()
        {
            View.Model.PersonalIdNumber.ShouldBe(_customerNotFoundWithPersonalIdNumber);
        }
        private void MeddelandeVisasAttKundSaknas()
        {
            View.Model.DisplayCustomerMissingMessage.ShouldBe(true);
        }
        private void FlyttasAnv�ndarenTillAvbrytSidan()
        {
            HttpContext.ResponseInstance.RedirectedUrl.ShouldBe(_abortRedirectUrl);
        }

        private void F�rflyttasAnv�ndarenTillVynF�rF�reg�endeSteg()
        {
			HttpContext.ResponseInstance.RedirectedUrl.ShouldBe(_previousRedirectUrl);
        }

        private void F�rflyttasAnv�ndarenTillVynF�rN�staSteg()
        {
        	var expectedUrl = _submitRedirectUrl + "?customer=" + _customer.Id;
			HttpContext.ResponseInstance.RedirectedUrl.ShouldBe(expectedUrl);
        }

        private void F�rflyttasAnv�ndarenTillVynF�rN�staStegMedOrderIdAngivet()
        {
            var expectedUrl = _submitRedirectUrl + "?order=" + _order.Id;
            HttpContext.ResponseInstance.RedirectedUrl.ShouldBe(expectedUrl);
        }

        private void KundinformationSparas()
        {
            _customer = WithRepository<IOrderCustomerRepository>().GetAll().First();
            _customer.AddressLineOne.ShouldBe(_form.AddressLineOne);
            _customer.AddressLineTwo.ShouldBe(_form.AddressLineTwo);
            _customer.City.ShouldBe(_form.City);
            _customer.Email.ShouldBe(_form.Email);
            _customer.FirstName.ShouldBe(_form.FirstName);
            _customer.LastName.ShouldBe(_form.LastName);
            _customer.MobilePhone.ShouldBe(_form.MobilePhone);
            _customer.Notes.ShouldBe(_form.Notes);
            _customer.PersonalIdNumber.ShouldBe(_form.PersonalIdNumber);
            _customer.Phone.ShouldBe(_form.Phone);
            _customer.PostalCode.ShouldBe(_form.PostalCode);
			_customer.Shop.Id.ShouldBe(_shop.Id);
        }

        private void UppdaterasBefintligKund()
        {
            var customer = WithRepository<IOrderCustomerRepository>().Get(_customer.Id);
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
            View.Model.FirstName.ShouldBe(_customer.FirstName);
            View.Model.LastName.ShouldBe(_customer.LastName);
            View.Model.PersonalIdNumber.ShouldBe(_customer.PersonalIdNumber);
            View.Model.Email.ShouldBe(_customer.Email);
            View.Model.MobilePhone.ShouldBe(_customer.MobilePhone);
            View.Model.Phone.ShouldBe(_customer.Phone);
            View.Model.AddressLineOne.ShouldBe(_customer.AddressLineOne);
            View.Model.AddressLineTwo.ShouldBe(_customer.AddressLineTwo);
            View.Model.City.ShouldBe(_customer.City);
            View.Model.PostalCode.ShouldBe(_customer.PostalCode);
            View.Model.Notes.ShouldBe(_customer.Notes);
        }

        private void TasOrdernBort()
        {
            WithRepository<IOrderRepository>().Get(_order.Id).ShouldBe(null);
        }

    	private void KastasEttException()
    	{
    		_thrownException.ShouldBeTypeOf<AccessDeniedException>();
    	}

        #endregion

    }
}