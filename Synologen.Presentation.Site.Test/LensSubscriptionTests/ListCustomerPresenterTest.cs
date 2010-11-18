using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.EventArguments.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Test.LensSubscriptionTests.Factories;
using Spinit.Wpc.Synologen.Presentation.Site.Test.LensSubscriptionTests.TestHelpers;

namespace Spinit.Wpc.Synologen.Presentation.Site.Test.LensSubscriptionTests
{
	
	[TestFixture]
	[Category("SubscriptionListPresenterTester")]
	public class When_loading_customer_list_view : ListCustomersTestbase
	{
		private readonly IList<Customer> _customersList;
		private readonly string _editPageUrl;

		public When_loading_customer_list_view()
		{
			// Arrange
			_customersList = CustomerFactory.GetList().ToList();
			_editPageUrl = "/testPage";

			Context = () =>
			{
				MockedCustomerRepository.Setup(x => x.FindBy(It.IsAny<CustomersForShopMatchingCriteria>())).Returns(_customersList);
				MockedView.SetupGet(x => x.EditPageId).Returns(67);
				MockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(159);
				MockedSynologenMemberService.Setup(x => x.GetPageUrl(It.IsAny<int>())).Returns(_editPageUrl);
			};

			Because = presenter => presenter.View_Load(null, new EventArgs());
			
		}

		[Test]
		public void Presenter_creates_expected_criteria()
		{
			MockedCustomerRepository.Verify(x => x.FindBy(It.Is<CustomersForShopMatchingCriteria>(y => y.ShopId.Equals(159))));
			MockedCustomerRepository.Verify(x => x.FindBy(It.Is<CustomersForShopMatchingCriteria>(y => Equals(y.SearchTerm, null))));
		}

		[Test]
		public void Presenter_asks_for_expected_page_url_and_shop_id()
		{
			MockedSynologenMemberService.Verify(x => x.GetPageUrl(It.Is<int>(y => y.Equals(67))));
			MockedSynologenMemberService.Verify(x => x.GetCurrentShopId());
		}

		[Test]
		public void Model_should_have_expected_values()
		{
			AssertUsing( view =>
			{
				view.Model.List.Count().ShouldBe(_customersList.Count());
				view.Model.List.For((index, customerItem) =>
				{
					view.Model.List.ElementAt(index).FirstName.ShouldBe(_customersList.ElementAt(index).FirstName);
					view.Model.List.ElementAt(index).LastName.ShouldBe(_customersList.ElementAt(index).LastName);
					view.Model.List.ElementAt(index).PersonalIdNumber.ShouldBe(_customersList.ElementAt(index).PersonalIdNumber);
					view.Model.List.ElementAt(index).EditPageUrl.ShouldBe(_editPageUrl + "?customer=" + _customersList.ElementAt(index).Id);
				});	
			});
		}
	}


	[TestFixture]
	[Category("SubscriptionListPresenterTester")]
	public class When_searching_customer_list_view : ListCustomersTestbase
	{
		private readonly IList<Customer> _customersList;
		private readonly string _editPageUrl;

		public When_searching_customer_list_view()
		{
			// Arrange
			_customersList = CustomerFactory.GetList().ToList();
			_editPageUrl = "/testPage";

			Context = () =>
			{
				MockedView.SetupGet(x => x.EditPageId).Returns(67);
				MockedCustomerRepository.Setup(x => x.FindBy(It.IsAny<CustomersForShopMatchingCriteria>())).Returns(_customersList);
				MockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(159);
				MockedSynologenMemberService.Setup(x => x.GetPageUrl(It.IsAny<int>())).Returns(_editPageUrl);
			};

			Because = presenter => {
				presenter.View_Load(null, new EventArgs());
				presenter.SearchList(null, new SearchEventArgs { SearchTerm = "Test" });
			};
		}

		[Test]
		public void Presenter_creates_expected_criteria()
		{
			MockedCustomerRepository.Verify(x => x.FindBy(It.Is<CustomersForShopMatchingCriteria>( criteria => 
				Equals(criteria.SearchTerm, null) && 
				criteria.ShopId.Equals(159)
			)), Times.Once());
			MockedCustomerRepository.Verify(x => x.FindBy(It.Is<CustomersForShopMatchingCriteria>( criteria => 
				Equals(criteria.SearchTerm, "Test") &&
				criteria.ShopId.Equals(159)
			)), Times.Once());
		}

		[Test]
		public void Model_should_have_expected_values()
		{
			AssertUsing( view =>
			{
				view.Model.List.Count().ShouldBe(_customersList.Count());
				view.Model.List.For((index, customerItem) =>
				{
					view.Model.List.ElementAt(index).FirstName.ShouldBe(_customersList.ElementAt(index).FirstName);
					view.Model.List.ElementAt(index).LastName.ShouldBe(_customersList.ElementAt(index).LastName);
					view.Model.List.ElementAt(index).PersonalIdNumber.ShouldBe(_customersList.ElementAt(index).PersonalIdNumber);
					view.Model.List.ElementAt(index).EditPageUrl.ShouldBe(_editPageUrl + "?customer=" + _customersList.ElementAt(index).Id);
				});	
			});
		}


	}
}
