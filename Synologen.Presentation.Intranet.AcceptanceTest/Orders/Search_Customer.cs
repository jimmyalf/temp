using System;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.TestHelpers;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.Orders
{
	[TestFixture, Category("Search_Customer")]
	public class Search_Customer_Specs : OrderSpecTestbase<SearchCustomerPresenter,ISearchCustomerView>
	{
		private SearchCustomerPresenter _presenter;
		private string _nextUrl, _abortUrl;
		private OrderCustomer _customer;
		private SearchCustomerEventArgs _searchEventArgs;
		private Shop _shop;
		private Shop _otherShop;

		public Search_Customer_Specs()
		{
			Context = () =>
			{
				_nextUrl = "/test/next";
				_abortUrl = "/test/abort";
				_shop = CreateShop<Shop>();
				_otherShop = CreateShop<Shop>();
				CreateCustomer(_otherShop);
				A.CallTo(() => SynologenMemberService.GetCurrentShopId()).Returns(_shop.Id);
				SetupNavigationEvents(abortPageUrl:_abortUrl, nextPageUrl: _nextUrl);
				_presenter = GetPresenter();
				
			};

			Story = () => new Berättelse("Sök kund")
			    .FörAtt("välja en kund att knyta till nytt abonnemang")
			    .Som("inloggad användare på intranätet")
			    .VillJag("söka eventuell befintlig kund");
		}

		[Test]
		public void KundExisterar()
		{
			SetupScenario(scenario => scenario
				.Givet(BefintligKund)
					.Och(AttAnvändarenAngettKundensPersonnummer)
				.När(AnvändarenKlickarPåSök)
				.Så(FlyttasAnvändarenTillKundformulärMedKundUppgifterIfyllda)
			);
		}


		[Test]
        public void KundExisterarEj()
        {
            SetupScenario(scenario => scenario
                .Givet(AttAnvändarenAngerEttNyttPersonnummer)
                .När(AnvändarenKlickarPåSök)
                .Så(FlyttasAnvändarenTillKundformulärMedPersonnummerIfyllt)
            );
        }

		[Test]
        public void AvbrytBeställning()
        {
            SetupScenario(scenario => scenario
                .Givet(AttAnvändarenStårIVynFörAttSökaKund)
                .När(AnvändarenAvbryterBeställningen)
                .Så(FlyttasAnvändarenTillAvbrytSidan));
        }


	    #region Arrange

		private void BefintligKund()
		{
			_customer = CreateCustomer(_shop);
		}

		private void AttAnvändarenAngettKundensPersonnummer()
		{
            _searchEventArgs = OrderFactory.GetSearchCustomerEventArgs(_customer.PersonalIdNumber);
		}

		private void AttAnvändarenAngerEttNyttPersonnummer()
		{
			_searchEventArgs = OrderFactory.GetSearchCustomerEventArgs("1234" /* non existing personal id number*/);
		}

        private void AttAnvändarenStårIVynFörAttSökaKund()
        {
        	
        }

        #endregion

        #region Act

        private void AnvändarenKlickarPåSök()
        {
            _presenter.View_Submit(null, _searchEventArgs);
        }

        private void AnvändarenAvbryterBeställningen()
        {
        	_presenter.View_Abort(null, new EventArgs());
        }

        #endregion

        #region Assert
        private void FlyttasAnvändarenTillKundformulärMedPersonnummerIfyllt()
        {
        	var expectedUrl = "{Url}?personalIdNumber={PersonalIdNumber}".ReplaceWith(new {Url = _nextUrl, _searchEventArgs.PersonalIdNumber});
            HttpContext.ResponseInstance.RedirectedUrl.ShouldBe(expectedUrl);
        }

		private void FlyttasAnvändarenTillKundformulärMedKundUppgifterIfyllda()
		{
			var expectedUrl = "{Url}?customer={CustomerId}".ReplaceWith(new {Url = _nextUrl, CustomerId = _customer.Id});
            HttpContext.ResponseInstance.RedirectedUrl.ShouldBe(expectedUrl);			
		}

        private void FlyttasAnvändarenTillAvbrytSidan()
        {
            HttpContext.ResponseInstance.RedirectedUrl.ShouldBe(_abortUrl);
        }
        #endregion

	}
}