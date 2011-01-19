using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
		private readonly string _currentpageUrl;

		public When_loading_customer_list_view()
		{
			// Arrange
			_customersList = CustomerFactory.GetList().ToList();
			_editPageUrl = "/testPage";
			_currentpageUrl = "/currentPage";

			Context = () =>
			{
				MockedCustomerRepository.Setup(x => x.FindBy(It.IsAny<CustomersForShopMatchingCriteria>())).Returns(_customersList);
				MockedView.SetupGet(x => x.EditPageId).Returns(67);
				MockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(159);
				MockedSynologenMemberService.Setup(x => x.GetPageUrl(It.IsAny<int>())).Returns(_editPageUrl);
				
				MockedHttpContext.SetupSingleQuery("order", null);
				MockedHttpContext.SetupSingleQuery("sort", null);
				MockedHttpContext.SetupCurrentPathAndQuery(_currentpageUrl);

			};

			Because = presenter => presenter.View_Load(null, new EventArgs());
			
		}

		[Test]
		public void Presenter_creates_expected_criteria()
		{
			MockedCustomerRepository.Verify(x => x.FindBy(It.Is<CustomersForShopMatchingCriteria>(y => y.ShopId.Equals(159))));
			MockedCustomerRepository.Verify(x => x.FindBy(It.Is<CustomersForShopMatchingCriteria>(y => Equals(y.SearchTerm, null))));
			MockedCustomerRepository.Verify(x => x.FindBy(It.Is<CustomersForShopMatchingCriteria>(y => Equals(y.OrderBy, null))));
			MockedCustomerRepository.Verify(x => x.FindBy(It.Is<CustomersForShopMatchingCriteria>(y => Equals(y.SortAscending, true))));
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
				view.Model.FirstNameSortUrl.ShouldBe(_currentpageUrl + "?order=FirstName&sort=Asc");
				view.Model.LastNameSortUrl.ShouldBe(_currentpageUrl + "?order=LastName&sort=Asc");
				view.Model.PersonNumberSortUrl.ShouldBe(_currentpageUrl + "?order=PersonalIdNumber&sort=Asc");
				view.Model.SearchTerm.ShouldBe(null);
			});
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

		public When_searching_customer_list_view()
		{
			// Arrange
			_customersList = CustomerFactory.GetList().ToList();
			_editPageUrl = "/testPage";
			_currentpageUrl = "/currentPage";
			_searchTerm = "Test";

			Context = () =>
			{
				MockedView.SetupGet(x => x.EditPageId).Returns(67);
				MockedCustomerRepository.Setup(x => x.FindBy(It.IsAny<CustomersForShopMatchingCriteria>())).Returns(_customersList);
				MockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(159);
				MockedSynologenMemberService.Setup(x => x.GetPageUrl(It.IsAny<int>())).Returns(_editPageUrl);
				MockedHttpContext.SetupCurrentPathAndQuery(_currentpageUrl);
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
				criteria.ShopId.Equals(159) && 
				Equals(criteria.OrderBy, null) &&
				criteria.SortAscending.Equals(true)
			)), Times.Once());
			MockedCustomerRepository.Verify(x => x.FindBy(It.Is<CustomersForShopMatchingCriteria>( criteria =>
				Equals(criteria.SearchTerm, _searchTerm) &&
				criteria.ShopId.Equals(159) &&
				Equals(criteria.OrderBy, null) &&
				criteria.SortAscending.Equals(true)
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
				view.Model.FirstNameSortUrl.ShouldBe(_currentpageUrl + "?order=FirstName&sort=Asc&search=" + _searchTerm);
				view.Model.LastNameSortUrl.ShouldBe(_currentpageUrl + "?order=LastName&sort=Asc&search=" + _searchTerm);
				view.Model.PersonNumberSortUrl.ShouldBe(_currentpageUrl + "?order=PersonalIdNumber&sort=Asc&search=" + _searchTerm);
				view.Model.SearchTerm.ShouldBe(_searchTerm);
			});
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
				MockedView.SetupGet(x => x.EditPageId).Returns(67);
				MockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(159);
				MockedSynologenMemberService.Setup(x => x.GetPageUrl(It.IsAny<int>())).Returns(_editPageUrl);

				var coll = new NameValueCollection {{"order", _orderColumn}, {"sort", _sortOrder}};
				MockedHttpContext.SetupQueryString(coll);
				MockedHttpContext.SetupCurrentPathAndQuery(_currentpageUrl + "?order="+ _orderColumn + "&sort=" + _sortOrder);
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
			AssertUsing(view =>
			{
				view.Model.List.Count().ShouldBe(_customersList.Count());
				view.Model.FirstNameSortUrl.ShouldBe(_currentpageUrl + "?order=FirstName&sort=Desc");
				view.Model.LastNameSortUrl.ShouldBe(_currentpageUrl + "?order=LastName&sort=Asc");
				view.Model.PersonNumberSortUrl.ShouldBe(_currentpageUrl + "?order=PersonalIdNumber&sort=Asc");
				view.Model.SearchTerm.ShouldBe(null);
			});
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
				MockedView.SetupGet(x => x.EditPageId).Returns(67);
				MockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(159);
				MockedSynologenMemberService.Setup(x => x.GetPageUrl(It.IsAny<int>())).Returns(_editPageUrl);

				var coll = new NameValueCollection {{"order", _orderColumn}, {"sort", _sortOrder}};
				MockedHttpContext.SetupQueryString(coll);
				MockedHttpContext.SetupCurrentPathAndQuery(_currentpageUrl + "?order="+ _orderColumn + "&sort=" + _sortOrder);
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
			AssertUsing(view =>
			{
				view.Model.List.Count().ShouldBe(_customersList.Count());
				view.Model.FirstNameSortUrl.ShouldBe(_currentpageUrl + "?order=FirstName&sort=Asc");
				view.Model.LastNameSortUrl.ShouldBe(_currentpageUrl + "?order=LastName&sort=Asc");
				view.Model.PersonNumberSortUrl.ShouldBe(_currentpageUrl + "?order=PersonalIdNumber&sort=Asc");
				view.Model.SearchTerm.ShouldBe(null);
			});
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
				MockedView.SetupGet(x => x.EditPageId).Returns(67);
				MockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(159);
				MockedSynologenMemberService.Setup(x => x.GetPageUrl(It.IsAny<int>())).Returns(_editPageUrl);

				var coll = new NameValueCollection { { "order", _orderColumn }, { "sort", _sortOrder } };
				MockedHttpContext.SetupQueryString(coll);
				MockedHttpContext.SetupCurrentPathAndQuery(_currentpageUrl + "?order=" + _orderColumn + "&sort=" + _sortOrder);
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
			AssertUsing(view =>
			{
				view.Model.List.Count().ShouldBe(_customersList.Count());
				view.Model.FirstNameSortUrl.ShouldBe(_currentpageUrl + "?order=FirstName&sort=Asc");
				view.Model.LastNameSortUrl.ShouldBe(_currentpageUrl + "?order=LastName&sort=Desc");
				view.Model.PersonNumberSortUrl.ShouldBe(_currentpageUrl + "?order=PersonalIdNumber&sort=Asc");
				view.Model.SearchTerm.ShouldBe(null);
			});
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
				MockedView.SetupGet(x => x.EditPageId).Returns(67);
				MockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(159);
				MockedSynologenMemberService.Setup(x => x.GetPageUrl(It.IsAny<int>())).Returns(_editPageUrl);

				var coll = new NameValueCollection { { "order", _orderColumn }, { "sort", _sortOrder } };
				MockedHttpContext.SetupQueryString(coll);
				MockedHttpContext.SetupCurrentPathAndQuery(_currentpageUrl + "?order=" + _orderColumn + "&sort=" + _sortOrder);
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
			AssertUsing(view =>
			{
				view.Model.List.Count().ShouldBe(_customersList.Count());
				view.Model.FirstNameSortUrl.ShouldBe(_currentpageUrl + "?order=FirstName&sort=Asc");
				view.Model.LastNameSortUrl.ShouldBe(_currentpageUrl + "?order=LastName&sort=Asc");
				view.Model.PersonNumberSortUrl.ShouldBe(_currentpageUrl + "?order=PersonalIdNumber&sort=Asc");
				view.Model.SearchTerm.ShouldBe(null);
			});
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
				MockedView.SetupGet(x => x.EditPageId).Returns(67);
				MockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(159);
				MockedSynologenMemberService.Setup(x => x.GetPageUrl(It.IsAny<int>())).Returns(_editPageUrl);

				var coll = new NameValueCollection { { "order", _orderColumn }, { "sort", _sortOrder } };
				MockedHttpContext.SetupQueryString(coll);
				MockedHttpContext.SetupCurrentPathAndQuery(_currentpageUrl + "?order=" + _orderColumn + "&sort=" + _sortOrder);
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
			AssertUsing(view =>
			{
				view.Model.List.Count().ShouldBe(_customersList.Count());
				view.Model.FirstNameSortUrl.ShouldBe(_currentpageUrl + "?order=FirstName&sort=Asc");
				view.Model.LastNameSortUrl.ShouldBe(_currentpageUrl + "?order=LastName&sort=Asc");
				view.Model.PersonNumberSortUrl.ShouldBe(_currentpageUrl + "?order=PersonalIdNumber&sort=Desc");
				view.Model.SearchTerm.ShouldBe(null);
			});
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
				MockedView.SetupGet(x => x.EditPageId).Returns(67);
				MockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(159);
				MockedSynologenMemberService.Setup(x => x.GetPageUrl(It.IsAny<int>())).Returns(_editPageUrl);

				var coll = new NameValueCollection { { "order", _orderColumn }, { "sort", _sortOrder } };
				MockedHttpContext.SetupQueryString(coll);
				MockedHttpContext.SetupCurrentPathAndQuery(_currentpageUrl + "?order=" + _orderColumn + "&sort=" + _sortOrder);
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
			AssertUsing(view =>
			{
				view.Model.List.Count().ShouldBe(_customersList.Count());
				view.Model.FirstNameSortUrl.ShouldBe(_currentpageUrl + "?order=FirstName&sort=Asc");
				view.Model.LastNameSortUrl.ShouldBe(_currentpageUrl + "?order=LastName&sort=Asc");
				view.Model.PersonNumberSortUrl.ShouldBe(_currentpageUrl + "?order=PersonalIdNumber&sort=Asc");
				view.Model.SearchTerm.ShouldBe(null);
			});
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
				MockedView.SetupGet(x => x.EditPageId).Returns(67);
				MockedCustomerRepository.Setup(x => x.FindBy(It.IsAny<CustomersForShopMatchingCriteria>())).Returns(_customersList);
				MockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(159);
				MockedSynologenMemberService.Setup(x => x.GetPageUrl(It.IsAny<int>())).Returns(_editPageUrl);
				var coll = new NameValueCollection { { "order", _orderColumn }, { "sort", _sortOrder } };
				MockedHttpContext.SetupQueryString(coll);
				MockedHttpContext.SetupCurrentPathAndQuery(_currentpageUrl);
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
			AssertUsing(view =>
			{
				view.Model.List.Count().ShouldBe(_customersList.Count());
				view.Model.List.For((index, customerItem) =>
				{
					view.Model.List.ElementAt(index).FirstName.ShouldBe(_customersList.ElementAt(index).FirstName);
					view.Model.List.ElementAt(index).LastName.ShouldBe(_customersList.ElementAt(index).LastName);
					view.Model.List.ElementAt(index).PersonalIdNumber.ShouldBe(_customersList.ElementAt(index).PersonalIdNumber);
					view.Model.List.ElementAt(index).EditPageUrl.ShouldBe(_editPageUrl + "?customer=" + _customersList.ElementAt(index).Id);
				});
				view.Model.FirstNameSortUrl.ShouldBe(_currentpageUrl + "?order=FirstName&sort=Desc&search=" + _searchTerm);
				view.Model.LastNameSortUrl.ShouldBe(_currentpageUrl + "?order=LastName&sort=Asc&search=" + _searchTerm);
				view.Model.PersonNumberSortUrl.ShouldBe(_currentpageUrl + "?order=PersonalIdNumber&sort=Asc&search=" + _searchTerm);
				view.Model.SearchTerm.ShouldBe(_searchTerm);
			});
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
				MockedView.SetupGet(x => x.EditPageId).Returns(67);
				MockedCustomerRepository.Setup(x => x.FindBy(It.IsAny<CustomersForShopMatchingCriteria>())).Returns(_customersList);
				MockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(159);
				MockedSynologenMemberService.Setup(x => x.GetPageUrl(It.IsAny<int>())).Returns(_editPageUrl);
				var coll = new NameValueCollection { { "order", _orderColumn }, { "sort", _sortOrder } };
				MockedHttpContext.SetupQueryString(coll);
				MockedHttpContext.SetupCurrentPathAndQuery(_currentpageUrl);
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
			AssertUsing(view =>
			{
				view.Model.List.Count().ShouldBe(_customersList.Count());
				view.Model.List.For((index, customerItem) =>
				{
					view.Model.List.ElementAt(index).FirstName.ShouldBe(_customersList.ElementAt(index).FirstName);
					view.Model.List.ElementAt(index).LastName.ShouldBe(_customersList.ElementAt(index).LastName);
					view.Model.List.ElementAt(index).PersonalIdNumber.ShouldBe(_customersList.ElementAt(index).PersonalIdNumber);
					view.Model.List.ElementAt(index).EditPageUrl.ShouldBe(_editPageUrl + "?customer=" + _customersList.ElementAt(index).Id);
				});
				view.Model.FirstNameSortUrl.ShouldBe(_currentpageUrl + "?order=FirstName&sort=Asc&search=" + _searchTerm);
				view.Model.LastNameSortUrl.ShouldBe(_currentpageUrl + "?order=LastName&sort=Desc&search=" + _searchTerm);
				view.Model.PersonNumberSortUrl.ShouldBe(_currentpageUrl + "?order=PersonalIdNumber&sort=Asc&search=" + _searchTerm);
				view.Model.SearchTerm.ShouldBe(_searchTerm);
			});
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
				MockedView.SetupGet(x => x.EditPageId).Returns(67);
				MockedCustomerRepository.Setup(x => x.FindBy(It.IsAny<CustomersForShopMatchingCriteria>())).Returns(_customersList);
				MockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(159);
				MockedSynologenMemberService.Setup(x => x.GetPageUrl(It.IsAny<int>())).Returns(_editPageUrl);
				var coll = new NameValueCollection { { "order", _orderColumn }, { "sort", _sortOrder } };
				MockedHttpContext.SetupQueryString(coll);
				MockedHttpContext.SetupCurrentPathAndQuery(_currentpageUrl);
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
			AssertUsing(view =>
			{
				view.Model.List.Count().ShouldBe(_customersList.Count());
				view.Model.List.For((index, customerItem) =>
				{
					view.Model.List.ElementAt(index).FirstName.ShouldBe(_customersList.ElementAt(index).FirstName);
					view.Model.List.ElementAt(index).LastName.ShouldBe(_customersList.ElementAt(index).LastName);
					view.Model.List.ElementAt(index).PersonalIdNumber.ShouldBe(_customersList.ElementAt(index).PersonalIdNumber);
					view.Model.List.ElementAt(index).EditPageUrl.ShouldBe(_editPageUrl + "?customer=" + _customersList.ElementAt(index).Id);
				});
				view.Model.FirstNameSortUrl.ShouldBe(_currentpageUrl + "?order=FirstName&sort=Asc&search=" + _searchTerm);
				view.Model.LastNameSortUrl.ShouldBe(_currentpageUrl + "?order=LastName&sort=Asc&search=" + _searchTerm);
				view.Model.PersonNumberSortUrl.ShouldBe(_currentpageUrl + "?order=PersonalIdNumber&sort=Desc&search=" + _searchTerm);
				view.Model.SearchTerm.ShouldBe(_searchTerm);
			});
		}
	}

}
