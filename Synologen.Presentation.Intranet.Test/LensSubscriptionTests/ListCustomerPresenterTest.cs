using System;
using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using Moq;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Intranet.Test.LensSubscriptionTests.Factories;
using Spinit.Wpc.Synologen.Presentation.Intranet.Test.LensSubscriptionTests.TestHelpers;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Test.LensSubscriptionTests
{
	
	[TestFixture]
	[Category("SubscriptionListPresenterTester")]
	public class When_loading_customer_list_view : ListCustomersTestbase
	{
		private readonly IList<Customer> _customersList;
		private readonly string _editPageUrl;
		private readonly string _currentpageUrl;
		private readonly string _defaultOrderParameter;

		public When_loading_customer_list_view()
		{
			// Arrange
			_customersList = CustomerFactory.GetList().ToList();
			_editPageUrl = "/testPage";
			_currentpageUrl = "/currentPage";
			_defaultOrderParameter = "LastName";

			Context = () =>
			{
				MockedCustomerRepository.Setup(x => x.FindBy(It.IsAny<CustomersForShopMatchingCriteria>())).Returns(_customersList);
				A.CallTo(() => View.EditPageId).Returns(67);
				MockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(159);
				//MockedSynologenMemberService.Setup(x => x.GetPageUrl(It.IsAny<int>())).Returns(_editPageUrl);
				A.CallTo(() => RoutingService.GetPageUrl(A<int>.Ignored)).Returns(_editPageUrl);
				HttpContext.SetupRequestParameter("order", null);
				HttpContext.SetupRequestParameter("sort", null);
				HttpContext.SetupVirtualPathAndQuery(_currentpageUrl);

			};

			Because = presenter => presenter.View_Load(null, new EventArgs());
			
		}

		[Test]
		public void Presenter_creates_expected_criteria()
		{
			MockedCustomerRepository.Verify(x => x.FindBy(It.Is<CustomersForShopMatchingCriteria>(y => y.ShopId.Equals(159))));
			MockedCustomerRepository.Verify(x => x.FindBy(It.Is<CustomersForShopMatchingCriteria>(y => Equals(y.SearchTerm, null))));
			MockedCustomerRepository.Verify(x => x.FindBy(It.Is<CustomersForShopMatchingCriteria>(y => Equals(y.OrderBy, _defaultOrderParameter))));
			MockedCustomerRepository.Verify(x => x.FindBy(It.Is<CustomersForShopMatchingCriteria>(y => Equals(y.SortAscending, true))));
		}

		[Test]
		public void Presenter_asks_for_expected_page_url_and_shop_id()
		{
			A.CallTo(() => RoutingService.GetPageUrl(67)).MustHaveHappened();
			//MockedSynologenMemberService.Verify(x => x.GetPageUrl(It.Is<int>(y => y.Equals(67))));
			MockedSynologenMemberService.Verify(x => x.GetCurrentShopId());
		}

		[Test]
		public void Model_should_have_expected_values()
		{
			View.Model.List.Count().ShouldBe(_customersList.Count());
			View.Model.List.For((index, customerItem) =>
			{
				View.Model.List.ElementAt(index).FirstName.ShouldBe(_customersList.ElementAt(index).FirstName);
				View.Model.List.ElementAt(index).LastName.ShouldBe(_customersList.ElementAt(index).LastName);
				View.Model.List.ElementAt(index).PersonalIdNumber.ShouldBe(_customersList.ElementAt(index).PersonalIdNumber);
				View.Model.List.ElementAt(index).EditPageUrl.ShouldBe(_editPageUrl + "?customer=" + _customersList.ElementAt(index).Id);
			});
			View.Model.FirstNameSortUrl.ShouldBe(_currentpageUrl + "?order=FirstName&sort=Asc");
			View.Model.LastNameSortUrl.ShouldBe(_currentpageUrl + "?order=LastName&sort=Asc");
			View.Model.PersonNumberSortUrl.ShouldBe(_currentpageUrl + "?order=PersonalIdNumber&sort=Asc");
			View.Model.SearchTerm.ShouldBe(null);
		}
	}


	[TestFixture]
	[Category("SubscriptionListPresenterTester")]
	public class When_searching_customer_list_view : ListCustomersTestbase
	{
		private readonly IList<Customer> _customersList;
		private readonly string _editPageUrl;
		private readonly string _currentpageUrl;
		private readonly string _searchTerm;
		private readonly string _defaultOrderParameter;

		public When_searching_customer_list_view()
		{
			// Arrange
			_customersList = CustomerFactory.GetList().ToList();
			_editPageUrl = "/testPage";
			_currentpageUrl = "/currentPage";
			_searchTerm = "Test";
			_defaultOrderParameter = "LastName";

			Context = () =>
			{
				A.CallTo(() => View.EditPageId).Returns(67);
				MockedCustomerRepository.Setup(x => x.FindBy(It.IsAny<CustomersForShopMatchingCriteria>())).Returns(_customersList);
				MockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(159);
				//MockedSynologenMemberService.Setup(x => x.GetPageUrl(It.IsAny<int>())).Returns(_editPageUrl);
				A.CallTo(() => RoutingService.GetPageUrl(A<int>.Ignored)).Returns(_editPageUrl);
				HttpContext.SetupVirtualPathAndQuery(_currentpageUrl);
				HttpContext.SetupVirtualPathAndQuery(_currentpageUrl);
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
				Equals(criteria.SearchTerm, _searchTerm) &&
				criteria.ShopId.Equals(159) &&
				Equals(criteria.OrderBy, _defaultOrderParameter) &&
				criteria.SortAscending.Equals(true)
			)), Times.Once());
		}

		[Test]
		public void Model_should_have_expected_values()
		{
			View.Model.List.Count().ShouldBe(_customersList.Count());
			View.Model.List.For((index, customerItem) =>
			{
				View.Model.List.ElementAt(index).FirstName.ShouldBe(_customersList.ElementAt(index).FirstName);
				View.Model.List.ElementAt(index).LastName.ShouldBe(_customersList.ElementAt(index).LastName);
				View.Model.List.ElementAt(index).PersonalIdNumber.ShouldBe(_customersList.ElementAt(index).PersonalIdNumber);
				View.Model.List.ElementAt(index).EditPageUrl.ShouldBe(_editPageUrl + "?customer=" + _customersList.ElementAt(index).Id);
			});
			View.Model.FirstNameSortUrl.ShouldBe(_currentpageUrl + "?order=FirstName&sort=Asc&search=" + _searchTerm);
			View.Model.LastNameSortUrl.ShouldBe(_currentpageUrl + "?order=LastName&sort=Asc&search=" + _searchTerm);
			View.Model.PersonNumberSortUrl.ShouldBe(_currentpageUrl + "?order=PersonalIdNumber&sort=Asc&search=" + _searchTerm);
			View.Model.SearchTerm.ShouldBe(_searchTerm);
		}

	}


	[TestFixture]
	[Category("SubscriptionListPresenterTester")]
	public class When_ordering_customer_list_view_by_firstname_asc : ListCustomersTestbase
	{
		private readonly IList<Customer> _customersList;
		private readonly string _editPageUrl;
		private readonly string _currentpageUrl;
		private readonly string _orderColumn;
		private readonly string _sortOrder;

		public When_ordering_customer_list_view_by_firstname_asc()
		{
			// Arrange
			_customersList = CustomerFactory.GetList().ToList();
			_editPageUrl = "/testPage";
			_currentpageUrl = "/currentPage";
			_orderColumn = "FirstName";
			_sortOrder = "Asc";

			Context = () =>
			{
				MockedCustomerRepository.Setup(x => x.FindBy(It.IsAny<CustomersForShopMatchingCriteria>())).Returns(_customersList);
				A.CallTo(() => View.EditPageId).Returns(67);
				MockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(159);
				A.CallTo(() => RoutingService.GetPageUrl(A<int>.Ignored)).Returns(_editPageUrl);
				//MockedSynologenMemberService.Setup(x => x.GetPageUrl(It.IsAny<int>())).Returns(_editPageUrl);

				HttpContext.SetupRequestParameter("order", _orderColumn);
				HttpContext.SetupRequestParameter("sort", _sortOrder);
				HttpContext.SetupVirtualPathAndQuery(_currentpageUrl + "?order="+ _orderColumn + "&sort=" + _sortOrder);
			};

			Because = presenter => presenter.View_Load(null, new EventArgs());

		}

		[Test]
		public void Presenter_creates_expected_criteria()
		{
			MockedCustomerRepository.Verify(x => x.FindBy(It.Is<CustomersForShopMatchingCriteria>(y => y.ShopId.Equals(159))));
			MockedCustomerRepository.Verify(x => x.FindBy(It.Is<CustomersForShopMatchingCriteria>(y => Equals(y.SearchTerm, null))));
			MockedCustomerRepository.Verify(x => x.FindBy(It.Is<CustomersForShopMatchingCriteria>(y => Equals(y.OrderBy, _orderColumn))));
			MockedCustomerRepository.Verify(x => x.FindBy(It.Is<CustomersForShopMatchingCriteria>(y => Equals(y.SortAscending, true))));
		}

		[Test]
		public void Model_should_have_expected_values()
		{
			View.Model.List.Count().ShouldBe(_customersList.Count());
			View.Model.FirstNameSortUrl.ShouldBe(_currentpageUrl + "?order=FirstName&sort=Desc");
			View.Model.LastNameSortUrl.ShouldBe(_currentpageUrl + "?order=LastName&sort=Asc");
			View.Model.PersonNumberSortUrl.ShouldBe(_currentpageUrl + "?order=PersonalIdNumber&sort=Asc");
			View.Model.SearchTerm.ShouldBe(null);
		}
	}

	[TestFixture]
	[Category("SubscriptionListPresenterTester")]
	public class When_ordering_customer_list_view_by_firstname_desc : ListCustomersTestbase
	{
		private readonly IList<Customer> _customersList;
		private readonly string _editPageUrl;
		private readonly string _currentpageUrl;
		private readonly string _orderColumn;
		private readonly string _sortOrder;

		public When_ordering_customer_list_view_by_firstname_desc()
		{
			// Arrange
			_customersList = CustomerFactory.GetList().ToList();
			_editPageUrl = "/testPage";
			_currentpageUrl = "/currentPage";
			_orderColumn = "FirstName";
			_sortOrder = "Desc";

			Context = () =>
			{
				MockedCustomerRepository.Setup(x => x.FindBy(It.IsAny<CustomersForShopMatchingCriteria>())).Returns(_customersList);
				A.CallTo(() => View.EditPageId).Returns(67);
				MockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(159);
				A.CallTo(() => RoutingService.GetPageUrl(A<int>.Ignored)).Returns(_editPageUrl);
				//MockedSynologenMemberService.Setup(x => x.GetPageUrl(It.IsAny<int>())).Returns(_editPageUrl);

				HttpContext.SetupRequestParameter("order", _orderColumn);
				HttpContext.SetupRequestParameter("sort", _sortOrder);

				HttpContext.SetupVirtualPathAndQuery(_currentpageUrl + "?order="+ _orderColumn + "&sort=" + _sortOrder);
			};

			Because = presenter => presenter.View_Load(null, new EventArgs());

		}

		[Test]
		public void Presenter_creates_expected_criteria()
		{
			MockedCustomerRepository.Verify(x => x.FindBy(It.Is<CustomersForShopMatchingCriteria>(y => y.ShopId.Equals(159))));
			MockedCustomerRepository.Verify(x => x.FindBy(It.Is<CustomersForShopMatchingCriteria>(y => Equals(y.SearchTerm, null))));
			MockedCustomerRepository.Verify(x => x.FindBy(It.Is<CustomersForShopMatchingCriteria>(y => Equals(y.OrderBy, _orderColumn))));
			MockedCustomerRepository.Verify(x => x.FindBy(It.Is<CustomersForShopMatchingCriteria>(y => Equals(y.SortAscending, false))));
		}

		[Test]
		public void Model_should_have_expected_values()
		{
			View.Model.List.Count().ShouldBe(_customersList.Count());
			View.Model.FirstNameSortUrl.ShouldBe(_currentpageUrl + "?order=FirstName&sort=Asc");
			View.Model.LastNameSortUrl.ShouldBe(_currentpageUrl + "?order=LastName&sort=Asc");
			View.Model.PersonNumberSortUrl.ShouldBe(_currentpageUrl + "?order=PersonalIdNumber&sort=Asc");
			View.Model.SearchTerm.ShouldBe(null);
		}
	}

	[TestFixture]
	[Category("SubscriptionListPresenterTester")]
	public class When_ordering_customer_list_view_by_lastname_asc : ListCustomersTestbase
	{
		private readonly IList<Customer> _customersList;
		private readonly string _editPageUrl;
		private readonly string _currentpageUrl;
		private readonly string _orderColumn;
		private readonly string _sortOrder;

		public When_ordering_customer_list_view_by_lastname_asc()
		{
			// Arrange
			_customersList = CustomerFactory.GetList().ToList();
			_editPageUrl = "/testPage";
			_currentpageUrl = "/currentPage";
			_orderColumn = "LastName";
			_sortOrder = "Asc";

			Context = () =>
			{
				MockedCustomerRepository.Setup(x => x.FindBy(It.IsAny<CustomersForShopMatchingCriteria>())).Returns(_customersList);
				A.CallTo(() => View.EditPageId).Returns(67);
				MockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(159);
				//MockedSynologenMemberService.Setup(x => x.GetPageUrl(It.IsAny<int>())).Returns(_editPageUrl);
				A.CallTo(() => RoutingService.GetPageUrl(A<int>.Ignored)).Returns(_editPageUrl);

				HttpContext.SetupRequestParameter("order", _orderColumn);
				HttpContext.SetupRequestParameter("sort", _sortOrder);
				HttpContext.SetupVirtualPathAndQuery(_currentpageUrl + "?order=" + _orderColumn + "&sort=" + _sortOrder);
			};

			Because = presenter => presenter.View_Load(null, new EventArgs());

		}

		[Test]
		public void Presenter_creates_expected_criteria()
		{
			MockedCustomerRepository.Verify(x => x.FindBy(It.Is<CustomersForShopMatchingCriteria>(y => y.ShopId.Equals(159))));
			MockedCustomerRepository.Verify(x => x.FindBy(It.Is<CustomersForShopMatchingCriteria>(y => Equals(y.SearchTerm, null))));
			MockedCustomerRepository.Verify(x => x.FindBy(It.Is<CustomersForShopMatchingCriteria>(y => Equals(y.OrderBy, _orderColumn))));
			MockedCustomerRepository.Verify(x => x.FindBy(It.Is<CustomersForShopMatchingCriteria>(y => Equals(y.SortAscending, true))));
		}

		[Test]
		public void Model_should_have_expected_values()
		{
			View.Model.List.Count().ShouldBe(_customersList.Count());
			View.Model.FirstNameSortUrl.ShouldBe(_currentpageUrl + "?order=FirstName&sort=Asc");
			View.Model.LastNameSortUrl.ShouldBe(_currentpageUrl + "?order=LastName&sort=Desc");
			View.Model.PersonNumberSortUrl.ShouldBe(_currentpageUrl + "?order=PersonalIdNumber&sort=Asc");
			View.Model.SearchTerm.ShouldBe(null);
		}
	}

	[TestFixture]
	[Category("SubscriptionListPresenterTester")]
	public class When_ordering_customer_list_view_by_lastname_desc : ListCustomersTestbase
	{
		private readonly IList<Customer> _customersList;
		private readonly string _editPageUrl;
		private readonly string _currentpageUrl;
		private readonly string _orderColumn;
		private readonly string _sortOrder;

		public When_ordering_customer_list_view_by_lastname_desc()
		{
			// Arrange
			_customersList = CustomerFactory.GetList().ToList();
			_editPageUrl = "/testPage";
			_currentpageUrl = "/currentPage";
			_orderColumn = "LastName";
			_sortOrder = "Desc";

			Context = () =>
			{
				MockedCustomerRepository.Setup(x => x.FindBy(It.IsAny<CustomersForShopMatchingCriteria>())).Returns(_customersList);
				A.CallTo(() => View.EditPageId).Returns(67);
				MockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(159);
				//MockedSynologenMemberService.Setup(x => x.GetPageUrl(It.IsAny<int>())).Returns(_editPageUrl);
				A.CallTo(() => RoutingService.GetPageUrl(A<int>.Ignored)).Returns(_editPageUrl);

				HttpContext.SetupRequestParameter("order", _orderColumn);
				HttpContext.SetupRequestParameter("sort", _sortOrder);

				HttpContext.SetupVirtualPathAndQuery(_currentpageUrl + "?order=" + _orderColumn + "&sort=" + _sortOrder);
			};

			Because = presenter => presenter.View_Load(null, new EventArgs());

		}

		[Test]
		public void Presenter_creates_expected_criteria()
		{
			MockedCustomerRepository.Verify(x => x.FindBy(It.Is<CustomersForShopMatchingCriteria>(y => y.ShopId.Equals(159))));
			MockedCustomerRepository.Verify(x => x.FindBy(It.Is<CustomersForShopMatchingCriteria>(y => Equals(y.SearchTerm, null))));
			MockedCustomerRepository.Verify(x => x.FindBy(It.Is<CustomersForShopMatchingCriteria>(y => Equals(y.OrderBy, _orderColumn))));
			MockedCustomerRepository.Verify(x => x.FindBy(It.Is<CustomersForShopMatchingCriteria>(y => Equals(y.SortAscending, false))));
		}

		[Test]
		public void Model_should_have_expected_values()
		{
			View.Model.List.Count().ShouldBe(_customersList.Count());
			View.Model.FirstNameSortUrl.ShouldBe(_currentpageUrl + "?order=FirstName&sort=Asc");
			View.Model.LastNameSortUrl.ShouldBe(_currentpageUrl + "?order=LastName&sort=Asc");
			View.Model.PersonNumberSortUrl.ShouldBe(_currentpageUrl + "?order=PersonalIdNumber&sort=Asc");
			View.Model.SearchTerm.ShouldBe(null);
		}
	}


	[TestFixture]
	[Category("SubscriptionListPresenterTester")]
	public class When_ordering_customer_list_view_by_personidnumber_asc : ListCustomersTestbase
	{
		private readonly IList<Customer> _customersList;
		private readonly string _editPageUrl;
		private readonly string _currentpageUrl;
		private readonly string _orderColumn;
		private readonly string _sortOrder;

		public When_ordering_customer_list_view_by_personidnumber_asc()
		{
			// Arrange
			_customersList = CustomerFactory.GetList().ToList();
			_editPageUrl = "/testPage";
			_currentpageUrl = "/currentPage";
			_orderColumn = "PersonalIdNumber";
			_sortOrder = "Asc";

			Context = () =>
			{
				MockedCustomerRepository.Setup(x => x.FindBy(It.IsAny<CustomersForShopMatchingCriteria>())).Returns(_customersList);
				A.CallTo(() => View.EditPageId).Returns(67);
				MockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(159);
				//MockedSynologenMemberService.Setup(x => x.GetPageUrl(It.IsAny<int>())).Returns(_editPageUrl);
				A.CallTo(() => RoutingService.GetPageUrl(A<int>.Ignored)).Returns(_editPageUrl);

				HttpContext.SetupRequestParameter("order", _orderColumn);
				HttpContext.SetupRequestParameter("sort", _sortOrder);
				HttpContext.SetupVirtualPathAndQuery(_currentpageUrl + "?order=" + _orderColumn + "&sort=" + _sortOrder);
			};

			Because = presenter => presenter.View_Load(null, new EventArgs());

		}

		[Test]
		public void Presenter_creates_expected_criteria()
		{
			MockedCustomerRepository.Verify(x => x.FindBy(It.Is<CustomersForShopMatchingCriteria>(y => y.ShopId.Equals(159))));
			MockedCustomerRepository.Verify(x => x.FindBy(It.Is<CustomersForShopMatchingCriteria>(y => Equals(y.SearchTerm, null))));
			MockedCustomerRepository.Verify(x => x.FindBy(It.Is<CustomersForShopMatchingCriteria>(y => Equals(y.OrderBy, _orderColumn))));
			MockedCustomerRepository.Verify(x => x.FindBy(It.Is<CustomersForShopMatchingCriteria>(y => Equals(y.SortAscending, true))));
		}

		[Test]
		public void Model_should_have_expected_values()
		{
			View.Model.List.Count().ShouldBe(_customersList.Count());
			View.Model.FirstNameSortUrl.ShouldBe(_currentpageUrl + "?order=FirstName&sort=Asc");
			View.Model.LastNameSortUrl.ShouldBe(_currentpageUrl + "?order=LastName&sort=Asc");
			View.Model.PersonNumberSortUrl.ShouldBe(_currentpageUrl + "?order=PersonalIdNumber&sort=Desc");
			View.Model.SearchTerm.ShouldBe(null);
		}
	}

	[TestFixture]
	[Category("SubscriptionListPresenterTester")]
	public class When_ordering_customer_list_view_by_personidnumber_desc : ListCustomersTestbase
	{
		private readonly IList<Customer> _customersList;
		private readonly string _editPageUrl;
		private readonly string _currentpageUrl;
		private readonly string _orderColumn;
		private readonly string _sortOrder;

		public When_ordering_customer_list_view_by_personidnumber_desc()
		{
			// Arrange
			_customersList = CustomerFactory.GetList().ToList();
			_editPageUrl = "/testPage";
			_currentpageUrl = "/currentPage";
			_orderColumn = "PersonalIdNumber";
			_sortOrder = "Desc";

			Context = () =>
			{
				MockedCustomerRepository.Setup(x => x.FindBy(It.IsAny<CustomersForShopMatchingCriteria>())).Returns(_customersList);
				A.CallTo(() => View.EditPageId).Returns(67);
				MockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(159);
				//MockedSynologenMemberService.Setup(x => x.GetPageUrl(It.IsAny<int>())).Returns(_editPageUrl);
				A.CallTo(() => RoutingService.GetPageUrl(A<int>.Ignored)).Returns(_editPageUrl);

				HttpContext.SetupRequestParameter("order", _orderColumn);
				HttpContext.SetupRequestParameter("sort", _sortOrder);
				HttpContext.SetupVirtualPathAndQuery(_currentpageUrl + "?order=" + _orderColumn + "&sort=" + _sortOrder);
			};

			Because = presenter => presenter.View_Load(null, new EventArgs());

		}

		[Test]
		public void Presenter_creates_expected_criteria()
		{
			MockedCustomerRepository.Verify(x => x.FindBy(It.Is<CustomersForShopMatchingCriteria>(y => y.ShopId.Equals(159))));
			MockedCustomerRepository.Verify(x => x.FindBy(It.Is<CustomersForShopMatchingCriteria>(y => Equals(y.SearchTerm, null))));
			MockedCustomerRepository.Verify(x => x.FindBy(It.Is<CustomersForShopMatchingCriteria>(y => Equals(y.OrderBy, _orderColumn))));
			MockedCustomerRepository.Verify(x => x.FindBy(It.Is<CustomersForShopMatchingCriteria>(y => Equals(y.SortAscending, false))));
		}

		[Test]
		public void Model_should_have_expected_values()
		{
			View.Model.List.Count().ShouldBe(_customersList.Count());
			View.Model.FirstNameSortUrl.ShouldBe(_currentpageUrl + "?order=FirstName&sort=Asc");
			View.Model.LastNameSortUrl.ShouldBe(_currentpageUrl + "?order=LastName&sort=Asc");
			View.Model.PersonNumberSortUrl.ShouldBe(_currentpageUrl + "?order=PersonalIdNumber&sort=Asc");
			View.Model.SearchTerm.ShouldBe(null);
		}
	}

	[TestFixture]
	[Category("SubscriptionListPresenterTester")]
	public class When_searching_customer_list_view_order_by_firstname : ListCustomersTestbase
	{
		private readonly IList<Customer> _customersList;
		private readonly string _editPageUrl;
		private readonly string _currentpageUrl;
		private readonly string _searchTerm;
		private readonly string _orderColumn;
		private readonly string _sortOrder;
		
		public When_searching_customer_list_view_order_by_firstname()
		{
			// Arrange
			_customersList = CustomerFactory.GetList().ToList();
			_editPageUrl = "/testPage";
			_currentpageUrl = "/currentPage";
			_searchTerm = "Test";
			_orderColumn = "FirstName";
			_sortOrder = "Asc";

			Context = () =>
			{
				A.CallTo(() => View.EditPageId).Returns(67);
				MockedCustomerRepository.Setup(x => x.FindBy(It.IsAny<CustomersForShopMatchingCriteria>())).Returns(_customersList);
				MockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(159);
				//MockedSynologenMemberService.Setup(x => x.GetPageUrl(It.IsAny<int>())).Returns(_editPageUrl);
				A.CallTo(() => RoutingService.GetPageUrl(A<int>.Ignored)).Returns(_editPageUrl);
				HttpContext.SetupRequestParameter("order", _orderColumn);
				HttpContext.SetupRequestParameter("sort", _sortOrder);
				HttpContext.SetupVirtualPathAndQuery(_currentpageUrl);
			};

			Because = presenter =>
			{
				presenter.SearchList(null, new SearchEventArgs { SearchTerm = "Test" });
			};
		}

		[Test]
		public void Presenter_creates_expected_criteria()
		{
			MockedCustomerRepository.Verify(x => x.FindBy(It.Is<CustomersForShopMatchingCriteria>(criteria =>
				Equals(criteria.SearchTerm, _searchTerm) &&
				criteria.ShopId.Equals(159) &&
				Equals(criteria.OrderBy, _orderColumn) &&
				criteria.SortAscending.Equals(true)
			)), Times.Once());
		}

		[Test]
		public void Model_should_have_expected_values()
		{
			View.Model.List.Count().ShouldBe(_customersList.Count());
			View.Model.List.For((index, customerItem) =>
			{
				View.Model.List.ElementAt(index).FirstName.ShouldBe(_customersList.ElementAt(index).FirstName);
				View.Model.List.ElementAt(index).LastName.ShouldBe(_customersList.ElementAt(index).LastName);
				View.Model.List.ElementAt(index).PersonalIdNumber.ShouldBe(_customersList.ElementAt(index).PersonalIdNumber);
				View.Model.List.ElementAt(index).EditPageUrl.ShouldBe(_editPageUrl + "?customer=" + _customersList.ElementAt(index).Id);
			});
			View.Model.FirstNameSortUrl.ShouldBe(_currentpageUrl + "?order=FirstName&sort=Desc&search=" + _searchTerm);
			View.Model.LastNameSortUrl.ShouldBe(_currentpageUrl + "?order=LastName&sort=Asc&search=" + _searchTerm);
			View.Model.PersonNumberSortUrl.ShouldBe(_currentpageUrl + "?order=PersonalIdNumber&sort=Asc&search=" + _searchTerm);
			View.Model.SearchTerm.ShouldBe(_searchTerm);
		}

	}


	[TestFixture]
	[Category("SubscriptionListPresenterTester")]
	public class When_searching_customer_list_view_order_by_lastname : ListCustomersTestbase
	{
		private readonly IList<Customer> _customersList;
		private readonly string _editPageUrl;
		private readonly string _currentpageUrl;
		private readonly string _searchTerm;
		private readonly string _orderColumn;
		private readonly string _sortOrder;

		public When_searching_customer_list_view_order_by_lastname()
		{
			// Arrange
			_customersList = CustomerFactory.GetList().ToList();
			_editPageUrl = "/testPage";
			_currentpageUrl = "/currentPage";
			_searchTerm = "Test";
			_orderColumn = "LastName";
			_sortOrder = "Asc";

			Context = () =>
			{
				A.CallTo(() => View.EditPageId).Returns(67);
				MockedCustomerRepository.Setup(x => x.FindBy(It.IsAny<CustomersForShopMatchingCriteria>())).Returns(_customersList);
				MockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(159);
				//MockedSynologenMemberService.Setup(x => x.GetPageUrl(It.IsAny<int>())).Returns(_editPageUrl);
				A.CallTo(() => RoutingService.GetPageUrl(A<int>.Ignored)).Returns(_editPageUrl);
				HttpContext.SetupRequestParameter("order", _orderColumn);
				HttpContext.SetupRequestParameter("sort", _sortOrder);
				HttpContext.SetupVirtualPathAndQuery(_currentpageUrl);
			};

			Because = presenter =>
			{
				presenter.SearchList(null, new SearchEventArgs { SearchTerm = "Test" });
			};
		}

		[Test]
		public void Presenter_creates_expected_criteria()
		{
			MockedCustomerRepository.Verify(x => x.FindBy(It.Is<CustomersForShopMatchingCriteria>(criteria =>
				Equals(criteria.SearchTerm, _searchTerm) &&
				criteria.ShopId.Equals(159) &&
				Equals(criteria.OrderBy, _orderColumn) &&
				criteria.SortAscending.Equals(true)
			)), Times.Once());
		}

		[Test]
		public void Model_should_have_expected_values()
		{
			View.Model.List.Count().ShouldBe(_customersList.Count());
			View.Model.List.For((index, customerItem) =>
			{
				View.Model.List.ElementAt(index).FirstName.ShouldBe(_customersList.ElementAt(index).FirstName);
				View.Model.List.ElementAt(index).LastName.ShouldBe(_customersList.ElementAt(index).LastName);
				View.Model.List.ElementAt(index).PersonalIdNumber.ShouldBe(_customersList.ElementAt(index).PersonalIdNumber);
				View.Model.List.ElementAt(index).EditPageUrl.ShouldBe(_editPageUrl + "?customer=" + _customersList.ElementAt(index).Id);
			});
			View.Model.FirstNameSortUrl.ShouldBe(_currentpageUrl + "?order=FirstName&sort=Asc&search=" + _searchTerm);
			View.Model.LastNameSortUrl.ShouldBe(_currentpageUrl + "?order=LastName&sort=Desc&search=" + _searchTerm);
			View.Model.PersonNumberSortUrl.ShouldBe(_currentpageUrl + "?order=PersonalIdNumber&sort=Asc&search=" + _searchTerm);
			View.Model.SearchTerm.ShouldBe(_searchTerm);
		}
	}

	[TestFixture]
	[Category("SubscriptionListPresenterTester")]
	public class When_searching_customer_list_view_order_by_personidnumber : ListCustomersTestbase
	{
		private readonly IList<Customer> _customersList;
		private readonly string _editPageUrl;
		private readonly string _currentpageUrl;
		private readonly string _searchTerm;
		private readonly string _orderColumn;
		private readonly string _sortOrder;

		public When_searching_customer_list_view_order_by_personidnumber()
		{
			// Arrange
			_customersList = CustomerFactory.GetList().ToList();
			_editPageUrl = "/testPage";
			_currentpageUrl = "/currentPage";
			_searchTerm = "Test";
			_orderColumn = "PersonalIdNumber";
			_sortOrder = "Asc";

			Context = () =>
			{
				A.CallTo(() => View.EditPageId).Returns(67);
				MockedCustomerRepository.Setup(x => x.FindBy(It.IsAny<CustomersForShopMatchingCriteria>())).Returns(_customersList);
				MockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(159);
				//MockedSynologenMemberService.Setup(x => x.GetPageUrl(It.IsAny<int>())).Returns(_editPageUrl);
				A.CallTo(() => RoutingService.GetPageUrl(A<int>.Ignored)).Returns(_editPageUrl);
				HttpContext.SetupRequestParameter("order", _orderColumn);
				HttpContext.SetupRequestParameter("sort", _sortOrder);
				HttpContext.SetupVirtualPathAndQuery(_currentpageUrl);
			};

			Because = presenter =>
			{
				presenter.SearchList(null, new SearchEventArgs { SearchTerm = "Test" });
			};
		}

		[Test]
		public void Presenter_creates_expected_criteria()
		{
			MockedCustomerRepository.Verify(x => x.FindBy(It.Is<CustomersForShopMatchingCriteria>(criteria =>
				Equals(criteria.SearchTerm, _searchTerm) &&
				criteria.ShopId.Equals(159) &&
				Equals(criteria.OrderBy, _orderColumn) &&
				criteria.SortAscending.Equals(true)
			)), Times.Once());
		}

		[Test]
		public void Model_should_have_expected_values()
		{
			View.Model.List.Count().ShouldBe(_customersList.Count());
			View.Model.List.For((index, customerItem) =>
				{
				View.Model.List.ElementAt(index).FirstName.ShouldBe(_customersList.ElementAt(index).FirstName);
				View.Model.List.ElementAt(index).LastName.ShouldBe(_customersList.ElementAt(index).LastName);
				View.Model.List.ElementAt(index).PersonalIdNumber.ShouldBe(_customersList.ElementAt(index).PersonalIdNumber);
				View.Model.List.ElementAt(index).EditPageUrl.ShouldBe(_editPageUrl + "?customer=" + _customersList.ElementAt(index).Id);
				});
			View.Model.FirstNameSortUrl.ShouldBe(_currentpageUrl + "?order=FirstName&sort=Asc&search=" + _searchTerm);
			View.Model.LastNameSortUrl.ShouldBe(_currentpageUrl + "?order=LastName&sort=Asc&search=" + _searchTerm);
			View.Model.PersonNumberSortUrl.ShouldBe(_currentpageUrl + "?order=PersonalIdNumber&sort=Desc&search=" + _searchTerm);
			View.Model.SearchTerm.ShouldBe(_searchTerm);
		}
	}

}
