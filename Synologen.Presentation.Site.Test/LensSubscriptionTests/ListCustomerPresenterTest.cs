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

		public When_loading_customer_list_view()
		{
			// Arrange


			_customersList = CustomerFactory.GetList().ToArray();

			var view = new Mock<IListCustomersView>();
			var customerRepository = new Mock<ICustomerRepository>();
			customerRepository.Setup(x => x.FindBy(It.IsAny<CustomersForShopMatchingCriteria>())).Returns(_customersList);

			view.SetupGet(x => x.Model).Returns(new ListCustomersModel());
			_view = view.Object;

			var synologenMemberService = new Mock<ISynologenMemberService>();
			synologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(159);
			_presenter = new ListCustomersPresenter(_view, customerRepository.Object, synologenMemberService.Object);

			//Act
			_presenter.View_Load(null, new EventArgs());
			
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
