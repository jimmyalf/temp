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
        public void VisaFormulärFörBefintligKundViaOrderId()
        {
            SetupScenario(scenario => scenario
                .Givet(EnOrder)
                .När(NärFormuläretLaddas)
                .Så(FyllsFormuläretMedKunduppgifter));
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

        [Test]
        public void AvbrytBeställning()
        {
            SetupScenario(scenario => scenario
                .Givet(AttAnvändarenStårIVynFörAttSparaKund)
                .När(AnvändarenAvbryterBeställningen)
                .Och(FlyttasAnvändarenTillAvbrytSidan));
        }

        [Test]
        public void AvbrytBeställningMedExisterandeOrder()
        {
            SetupScenario(scenario => scenario
                .Givet(AttAnvändarenStårIVynFörAttSparaKund)
                    .Och(EnOrder)
                .När(AnvändarenAvbryterBeställningen)
                .Så(TasOrdernBort)
                    .Och(FlyttasAnvändarenTillAvbrytSidan));
        }

        [Test]
        public void GåTillFöregåendeSteg()
        {
            SetupScenario(scenario => scenario
                .Givet(AttAnvändarenStårIVynFörAttSparaKund)
                .När(AnvändarenKlickarPåFöregåendeSteg)
                .Så(FörflyttasAnvändarenTillVynFörFöregåendeSteg));
        }

        [Test]
        public void GåTillFöregåendeStegMedExisterandeOrder()
        {
            SetupScenario(scenario => scenario
                .Givet(AttAnvändarenStårIVynFörAttSparaKund)
                    .Och(EnOrder)
                .När(AnvändarenKlickarPåFöregåendeSteg)
                .Så(TasOrdernBort)
                    .Och(FörflyttasAnvändarenTillVynFörFöregåendeSteg));
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

        [Test]
        public void UppdateraBefintligKundMedBefintligOrder()
        {
            SetupScenario(scenario => scenario
                .Givet(EnOrder)
                    .Och(AnvändarenUppdateratFormuläret)
                .När(AnvändarenFörsökerFortsättaTillNästaSteg)
                .Så(UppdaterasBefintligKund)
                    .Och(FörflyttasAnvändarenTillVynFörNästaStegMedOrderIdAngivet));
        }

        [Test]
        public void VisaSidaFörAnnanButiksOrder()
        {
            SetupScenario(scenario => scenario
                .Givet(EnOrderFörEnAnnanButik)
                .När(NärFormuläretLaddas)
                .Så(KastasEttException));
        }

    	#region Arrange
        private void AttEnKundEjHittatsIFöregåendeSteg()
        {
            _customerNotFoundWithPersonalIdNumber = "123456789";
            HttpContext.SetupRequestParameter("personalIdNumber", _customerNotFoundWithPersonalIdNumber);
        }
        private void AttEnKundHittatsIFöregåendeSteg()
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

    	private void EnOrderFörEnAnnanButik()
    	{
    		var otherShop = CreateShop<Shop>();
            _customer = CreateCustomer(otherShop);
            _order = CreateOrder(otherShop, _customer);
            HttpContext.SetupRequestParameter("order", _order.Id.ToString());
    	}

        private void AttAnvändarenStårIVynFörAttSparaKund()
        {
        }

        private void AnvändarenUppdateratFormuläret()
        {
            _form = OrderFactory.GetOrderCustomerForm(/*_customer.Id*/);
        }
        private void AttFormuläretÄrKorrektIfyllt()
        {
            _form = OrderFactory.GetOrderCustomerForm();
        }
        #endregion
        
        #region Act
        private void NärFormuläretLaddas()
        {
			_thrownException = CatchExceptionWhile(() => _saveCustomerPresenter.View_Load(null, new EventArgs()));
        }
        private void AnvändarenAvbryterBeställningen()
        {
            _saveCustomerPresenter.View_Abort(null, new EventArgs());
        }
        private void AnvändarenKlickarPåFöregåendeSteg()
        {
            _saveCustomerPresenter.View_Previous(null, new EventArgs());
        }
        private void AnvändarenFörsökerFortsättaTillNästaSteg()
        {
            _saveCustomerPresenter.View_Submit(null, _form);
        }
        #endregion

        #region Assert
        private void FyllsFormuläretIMedPersonnummer()
        {
            View.Model.PersonalIdNumber.ShouldBe(_customerNotFoundWithPersonalIdNumber);
        }
        private void MeddelandeVisasAttKundSaknas()
        {
            View.Model.DisplayCustomerMissingMessage.ShouldBe(true);
        }
        private void FlyttasAnvändarenTillAvbrytSidan()
        {
            HttpContext.ResponseInstance.RedirectedUrl.ShouldBe(_abortRedirectUrl);
        }

        private void FörflyttasAnvändarenTillVynFörFöregåendeSteg()
        {
			HttpContext.ResponseInstance.RedirectedUrl.ShouldBe(_previousRedirectUrl);
        }

        private void FörflyttasAnvändarenTillVynFörNästaSteg()
        {
        	var expectedUrl = _submitRedirectUrl + "?customer=" + _customer.Id;
			HttpContext.ResponseInstance.RedirectedUrl.ShouldBe(expectedUrl);
        }

        private void FörflyttasAnvändarenTillVynFörNästaStegMedOrderIdAngivet()
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
        private void FyllsFormuläretMedKunduppgifter()
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