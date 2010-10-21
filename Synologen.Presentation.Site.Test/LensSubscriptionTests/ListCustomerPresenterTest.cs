using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.EventArguments.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Models.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Test.LensSubscriptionTests.Factories;

namespace Spinit.Wpc.Synologen.Presentation.Site.Test.LensSubscriptionTests
{
	
	[TestFixture]
	[Category("SubscriptionRepositoryTester")]
	public class When_loading_customer_list_view
	{
		protected ListCustomersPresenter _presenter;
		private readonly IListCustomersView _view;
		private readonly Customer[] _customersList;
		private Mock<ICustomerRepository> _customerRepository;
		private Mock<ISynologenMemberService> _synologenMemberService;
		private string _editPageUrl;

		public When_loading_customer_list_view()
		{
			// Arrange
			_customersList = CustomerFactory.GetList().ToArray();
			_editPageUrl = "/testPage";

			var view = new Mock<IListCustomersView>();
			_customerRepository = new Mock<ICustomerRepository>();
			_customerRepository.Setup(x => x.FindBy(It.IsAny<CustomersForShopMatchingCriteria>())).Returns(_customersList);

			view.SetupGet(x => x.Model).Returns(new ListCustomersModel());
			view.SetupGet(x => x.EditPageId).Returns(67);
			_view = view.Object;

			_synologenMemberService = new Mock<ISynologenMemberService>();
			_synologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(159);
			_synologenMemberService.Setup(x => x.GetPageUrl(It.IsAny<int>())).Returns(_editPageUrl);

			_presenter = new ListCustomersPresenter(_view, _customerRepository.Object, _synologenMemberService.Object);

			//Act
			_presenter.View_Load(null, new EventArgs());
			
		}

		[Test]
		public void Presenter_creates_expected_criteria()
		{
			_customerRepository.Verify(x => x.FindBy(It.Is<CustomersForShopMatchingCriteria>(y => y.ShopId.Equals(159))));
			_customerRepository.Verify(x => x.FindBy(It.Is<CustomersForShopMatchingCriteria>(y => Equals(y.SearchTerm, null))));
		}

		[Test]
		public void Presenter_gets_expected_url()
		{
			_synologenMemberService.Verify(x => x.GetPageUrl(It.Is<int>(y => y.Equals(67))));
		}

		[Test]
		public void Model_should_have_expected_values()
		{
			_view.Model.List.Count().ShouldBe(3);
			for (int i = 0; i < _customersList.Length; i++)
			{
				_view.Model.List.ToArray()[i].FirstName.ShouldBe(_customersList[i].FirstName);
				_view.Model.List.ToArray()[i].LastName.ShouldBe(_customersList[i].LastName);
				_view.Model.List.ToArray()[i].PersonalIdNumber.ShouldBe(_customersList[i].PersonalIdNumber);
				_view.Model.List.ToArray()[i].EditPageUrl.ShouldBe(_editPageUrl + "?customer=" + _customersList[i].Id);
			}
		}


	}


	[TestFixture]
	[Category("SubscriptionRepositoryTester")]
	public class When_searching_customer_list_view
	{
		protected ListCustomersPresenter _presenter;
		private readonly IListCustomersView _view;
		private readonly Customer[] _customersList;
		private Mock<ICustomerRepository> _customerRepository;

		public When_searching_customer_list_view()
		{
			// Arrange
			_customersList = CustomerFactory.GetList().ToArray();

			var view = new Mock<IListCustomersView>();
			_customerRepository = new Mock<ICustomerRepository>();
			_customerRepository.Setup(x => x.FindBy(It.IsAny<CustomersForShopMatchingCriteria>())).Returns(_customersList);

			view.SetupGet(x => x.Model).Returns(new ListCustomersModel());
			_view = view.Object;

			var synologenMemberService = new Mock<ISynologenMemberService>();
			synologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(159);
			_presenter = new ListCustomersPresenter(_view, _customerRepository.Object, synologenMemberService.Object);

			//Act
			_presenter.SearchList(null, new SearchEventArgs { SearchTerm = "Test" });

		}

		[Test]
		public void Presenter_creates_expected_criteria()
		{
			_customerRepository.Verify(x => x.FindBy(It.Is<CustomersForShopMatchingCriteria>(y => y.ShopId.Equals(159))));
			_customerRepository.Verify(x => x.FindBy(It.Is<CustomersForShopMatchingCriteria>(y => y.SearchTerm.Equals("Test"))));
		}

		[Test]
		public void Model_should_have_expected_values()
		{
			_view.Model.List.Count().ShouldBe(3);
			for (int i = 0; i < _customersList.Length; i++)
			{
				_view.Model.List.ToArray()[i].FirstName.ShouldBe(_customersList[i].FirstName);
				_view.Model.List.ToArray()[i].LastName.ShouldBe(_customersList[i].LastName);
				_view.Model.List.ToArray()[i].PersonalIdNumber.ShouldBe(_customersList[i].PersonalIdNumber);
			}
		}


	}
}
