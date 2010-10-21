using System;
using System.Collections.Specialized;
using System.Web;
using Moq;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Models.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Test.LensSubscriptionTests.Factories;

namespace Spinit.Wpc.Synologen.Presentation.Site.Test.LensSubscriptionTests
{
	[TestFixture]
	[Category("CreateLensSubscriptionPresenterTester")]
	public class When_loading_create_subscription_view 
	{
		protected CreateLensSubscriptionPresenter presenter;
		private readonly ICreateLensSubscriptionView view;
		private readonly Mock<ICustomerRepository> _mockedCustomerRepository;


		public When_loading_create_subscription_view()
		{
			//Arrange
			var mockedHttpContext = new Mock<HttpContextBase>();
			mockedHttpContext.SetupGet(x => x.Request.Params).Returns(new NameValueCollection{{"customer","5"}});

			var mockedView = new Mock<ICreateLensSubscriptionView>();
			mockedView.SetupGet(x => x.Model).Returns(new CreateLensSubscriptionModel());
			view = mockedView.Object;

			_mockedCustomerRepository = new Mock<ICustomerRepository>();
			_mockedCustomerRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(CustomerFactory.Get());
			presenter = new CreateLensSubscriptionPresenter(view, _mockedCustomerRepository.Object) {HttpContext = mockedHttpContext.Object};

			//Act
			presenter.View_Load(null,new EventArgs());
		}

		[Test]
		public void Presenter_gets_expected_customer()
		{
			_mockedCustomerRepository.Verify(x => x.Get(It.Is<int>(id => id.Equals(5))));
		}

		[Test]
		public void Model_should_have_expected_values()
		{
			view.Model.CustomerName.ShouldBe("Eva Bergström");
		}
	}
}