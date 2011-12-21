using System;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
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
		private string _expectedNextRedirectUrl, _expectedAbortRedirectUrl;
		private OrderCustomer _customer;
		private SearchCustomerEventArgs _searchEventArgs;

		public Search_Customer_Specs()
		{
			Context = () =>
			{
				_expectedNextRedirectUrl = "/test/next";
				_expectedAbortRedirectUrl = "/test/abort";
				A.CallTo(() => View.NextPageId).Returns(55);
				A.CallTo(() => View.AbortPageId).Returns(22);
				A.CallTo(() => SynologenMemberService.GetPageUrl(View.NextPageId)).Returns(_expectedNextRedirectUrl);
				A.CallTo(() => SynologenMemberService.GetPageUrl(View.AbortPageId)).Returns(_expectedAbortRedirectUrl);
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
				.Givet(AttKundFinnsSedanTidigare)
				.När(AnvändarenKlickarPåSök)
				.Så(FlyttasAnvändarenTillKundformulär)
			);
		}

        [Test]
        public void KundExisterarEj()
        {
            SetupScenario(scenario => scenario
                .Givet(AttKundInteFinnsSedanTidigare)
                .När(AnvändarenKlickarPåSök)
                .Så(FlyttasAnvändarenTillKundformulär)
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
        private void AttKundFinnsSedanTidigare()
        {
        	_customer = CreateCustomer();
        }
        private void AttKundInteFinnsSedanTidigare()
        {
        	_customer = null;
        }
        private void AttAnvändarenStårIVynFörAttSökaKund()
        {
            
        }
        #endregion

        #region Act
        private void AnvändarenKlickarPåSök()
        {
            _searchEventArgs = (_customer == null) 
				? OrderFactory.GetSearchCustomerEventArgs("1234" /* non existing personal id number*/)
				: OrderFactory.GetSearchCustomerEventArgs(_customer.PersonalIdNumber);
            _presenter.View_Submit(null, _searchEventArgs);
        }
        private void AnvändarenAvbryterBeställningen()
        {
        	_presenter.View_Abort(null, new EventArgs());
        }
        #endregion

        #region Assert
        private void FlyttasAnvändarenTillKundformulär()
        {
			var expectedUrl = (_customer == null)
				? "{Url}?personalIdNumber={PersonalIdNumber}".ReplaceWith(new {Url = _expectedNextRedirectUrl, _searchEventArgs.PersonalIdNumber}) 
				: "{Url}?customer={CustomerId}".ReplaceWith(new {Url = _expectedNextRedirectUrl, CustomerId = _customer.Id});
        	
            HttpContext.ResponseInstance.RedirectedUrl.ShouldBe(expectedUrl);
        }

        private void FlyttasAnvändarenTillAvbrytSidan()
        {
            HttpContext.ResponseInstance.RedirectedUrl.ShouldBe(_expectedAbortRedirectUrl);
        }
        #endregion

	}
}