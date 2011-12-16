using System;
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
	[TestFixture, Category("Search_Customer")]
	public class Search_Customer_Specs : SpecTestbase<SearchCustomerPresenter,ISearchCustomerView>
	{
		private SearchCustomerPresenter _presenter;
		private string _expectedRedirectUrl;
		private OrderCustomer _customer;
		private SearchCustomerEventArgs _searchEventArgs;
		private int _editCustomerPageId;

		public Search_Customer_Specs()
		{
			Context = () =>
			{
				_editCustomerPageId = 55;
				_expectedRedirectUrl = "/test/page";
				A.CallTo(() => View.NextPageId).Returns(_editCustomerPageId);
				A.CallTo(() => SynologenMemberService.GetPageUrl(_editCustomerPageId)).Returns(_expectedRedirectUrl);
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
					.Och(KundIdÄrIfyllt)
			);
		}

        [Test]
        public void KundExisterarEj()
        {
            SetupScenario(scenario => scenario
                .Givet(AttKundInteFinnsSedanTidigare)
                .När(AnvändarenKlickarPåSök)
                .Så(FlyttasAnvändarenTillKundformulär)
                    .Och(PersonnummerÄrIfyllt)
            );
        }

        [Test]
        public void AvbrytBeställning()
        {
            SetupScenario(scenario => scenario
                .Givet(AttAnvändarenStårIVynFörAttSökaKund)
                .När(AnvändarenAvbryterBeställningen)
                .Så(FlyttasAnvändarenTillIntranätsidan));
        }


	    #region Arrange
        private void AttKundFinnsSedanTidigare()
        {
            _customer = OrderFactory.GetCustomer();
            WithRepository<IOrderCustomerRepository>().Save(_customer);
        }
        private void AttKundInteFinnsSedanTidigare()
        {
            _searchEventArgs = OrderFactory.GetSearchCustomerEventArgs("1234" /* non existing personal id number*/);
        }
        private void AttAnvändarenStårIVynFörAttSökaKund()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Act
        private void AnvändarenKlickarPåSök()
        {
            _searchEventArgs = OrderFactory.GetSearchCustomerEventArgs(_customer.PersonalIdNumber);
            _presenter.ViewSubmit(null, _searchEventArgs);
        }
        private void AnvändarenAvbryterBeställningen()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Assert
        private void KundIdÄrIfyllt()
        {
            HttpContext.ResponseInstance.RedirectedUrl.ShouldEndWith("?customer=" + _customer.Id);
        }

        private void FlyttasAnvändarenTillKundformulär()
        {
            HttpContext.ResponseInstance.RedirectedUrl.ShouldStartWith(_expectedRedirectUrl);
        }

        private void PersonnummerÄrIfyllt()
        {
            HttpContext.ResponseInstance.RedirectedUrl.ShouldEndWith("?personalIdNumber=" + _searchEventArgs.PersonalIdNumber);
        }
        private void FlyttasAnvändarenTillIntranätsidan()
        {
            throw new NotImplementedException();
        }
        #endregion

	}
}