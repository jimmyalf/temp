using System;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Exceptions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.TestHelpers;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.Orders
{
    [TestFixture, Category("Edit_Customer")]
	public class Edit_Customer : GeneralOrderSpecTestbase<EditCustomerPresenter, IEditCustomerView>
	{
    	private Shop _shop, _anotherShop;
    	private OrderCustomer _customer;
    	private EditCustomerPresenter _presenter;
    	private EditCustomerEventArgs _form;
    	private string _expectedNextPageUrl;
    	private Exception _thrownException;

    	public Edit_Customer()
    	{
    		Context = () =>
    		{
				_shop = CreateShop<Shop>();
				_anotherShop = CreateShop<Shop>();
				_presenter = GetPresenter();

				View.ReturnPageId = 54;
    			_expectedNextPageUrl = "test/next/page";
				RoutingService.AddRoute(View.ReturnPageId, _expectedNextPageUrl);
    			A.CallTo(() => SynologenMemberService.GetCurrentShopId()).Returns(_shop.Id);
    		};
			Story = () => new Berättelse("Redigera kund")
                .FörAtt("ändra kunduppgifter")
                .Som("inloggad användare på intranätet")
                .VillJag("ändra och spara kunduppgifter");
    	}

		[Test]
		public void KundUppgifterVisas()
		{
			SetupScenario(scenario => scenario
				.Givet(AttEnKundFinns)
				.När(SidanVisas)
				.Så(SkallKundUppgifterVisasIFormuläret)
					.Och(TillbakaLänkHarRättRoute)
			);
		}

    	[Test]
		public void KundTillhörInteAktuellButik()
		{
			SetupScenario(scenario => scenario
				.Givet(AttEnKundSomTillhörEnAnnanButikFinns)
				.När(SidanVisas)
				.Så(SkallEttExceptionKastas)
			);
		}

    	[Test]
		public void SparaKund()
		{
			SetupScenario(scenario => scenario
				.Givet(AttEnKundFinns)
					.Och(FormuläretÄrIfyllt)
				.När(AnvändarenSparar)
				.Så(SkallKundUppgifterUppdateras)
					.Och(VidarebefodranSkerTillKonfigureradSida)
			);
		}

    	#region Arrange

    	private void AttEnKundFinns()
    	{
    		_customer = CreateCustomer(_shop);
			HttpContext.SetupRequestParameter("customer", _customer.Id.ToString());
    	}

    	private void FormuläretÄrIfyllt()
    	{
			_form = new EditCustomerEventArgs
			{
				AddressLineOne = "Vägen 123",
				AddressLineTwo = "Box 18347",
				City = "Norrköping",
				Email = "berit.andren@testbolaget.se",
				FirstName = "Berit",
				LastName = "Andrén",
				MobilePhone = "0701 - 22 33 44",
				Notes = "Lorem ipsum 1234",
				PersonalIdNumber = "195001012233",
				Phone = "+46 (0) 11 - 123 45 67",
				PostalCode = "601 81"
			};
    	}

    	private void AttEnKundSomTillhörEnAnnanButikFinns()
    	{
    		_customer = CreateCustomer(_anotherShop);
			HttpContext.SetupRequestParameter("customer", _customer.Id.ToString());
    	}

		#endregion

		#region Act

		private void SidanVisas()
		{
			_thrownException = CatchExceptionWhile(() => _presenter.View_Load(null, new EventArgs()));
		}

    	private void AnvändarenSparar()
    	{
			_thrownException = CatchExceptionWhile(() => _presenter.View_Submit(null, _form));
    	}

		#endregion

		#region Assert

    	private void SkallKundUppgifterVisasIFormuläret()
    	{
    		View.Model.AddressLineOne.ShouldBe(_customer.AddressLineOne);
			View.Model.AddressLineTwo.ShouldBe(_customer.AddressLineTwo);
			View.Model.City.ShouldBe(_customer.City);
			View.Model.Email.ShouldBe(_customer.Email);
			View.Model.FirstName.ShouldBe(_customer.FirstName);
			View.Model.LastName.ShouldBe(_customer.LastName);
			View.Model.MobilePhone.ShouldBe(_customer.MobilePhone);
			View.Model.Notes.ShouldBe(_customer.Notes);
			View.Model.PersonalIdNumber.ShouldBe(_customer.PersonalIdNumber);
			View.Model.Phone.ShouldBe(_customer.Phone);
			View.Model.PostalCode.ShouldBe(_customer.PostalCode);
    	}

    	private void SkallKundUppgifterUppdateras()
    	{
    		var customer = Get<OrderCustomer>(_customer.Id);
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

    	private void VidarebefodranSkerTillKonfigureradSida()
    	{
			HttpContext.ResponseInstance.RedirectedUrl.ShouldBe(_expectedNextPageUrl);
    	}

    	private void SkallEttExceptionKastas()
    	{
    		_thrownException.ShouldBeTypeOf<AccessDeniedException>();
    	}

    	private void TillbakaLänkHarRättRoute()
    	{
    		View.Model.ReturnUrl.ShouldBe(_expectedNextPageUrl);
    	}

		#endregion
	}
}