using System;
using System.Collections.Specialized;
using System.Web;
using Moq;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.EventArguments.LensSubscription;
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
		private readonly Mock<ISubscriptionRepository> _mockedSubscriptionRepository;
		private readonly Mock<ICustomerRepository> _mockedCustomerRepository;


		public When_loading_create_subscription_view()
		{
			//Arrange
			const int customerId = 5;
			var mockedHttpContext = new Mock<HttpContextBase>();
			mockedHttpContext.SetupGet(x => x.Request.Params).Returns(new NameValueCollection{{"customer",customerId.ToString()}});

			var mockedView = new Mock<ICreateLensSubscriptionView>();
			mockedView.SetupGet(x => x.Model).Returns(new CreateLensSubscriptionModel());
			view = mockedView.Object;

			_mockedCustomerRepository = new Mock<ICustomerRepository>();
			_mockedCustomerRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(CustomerFactory.Get(customerId));

			_mockedSubscriptionRepository = new Mock<ISubscriptionRepository>();

			presenter = new CreateLensSubscriptionPresenter(view, _mockedCustomerRepository.Object, _mockedSubscriptionRepository.Object) {HttpContext = mockedHttpContext.Object};

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

	[TestFixture]
	[Category("CreateLensSubscriptionPresenterTester")]
	public class When_submitting_create_subscription_view
	{
		protected CreateLensSubscriptionPresenter presenter;
		private readonly ICreateLensSubscriptionView view;
		private readonly Mock<ISubscriptionRepository> _mockedSubscriptionRepository;
		private readonly SaveSubscriptionEventArgs _saveEventArgs;


		public When_submitting_create_subscription_view()
		{
			//Arrange
			const int customerId = 5;
			var mockedHttpContext = new Mock<HttpContextBase>();
			mockedHttpContext.SetupGet(x => x.Request.Params).Returns(new NameValueCollection{{"customer",customerId.ToString()}});

			var mockedView = new Mock<ICreateLensSubscriptionView>();
			mockedView.SetupGet(x => x.Model).Returns(new CreateLensSubscriptionModel());
			view = mockedView.Object;

			var mockedCustomerRepository = new Mock<ICustomerRepository>();
			mockedCustomerRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(CustomerFactory.Get(customerId));

			_mockedSubscriptionRepository = new Mock<ISubscriptionRepository>();
			presenter = new CreateLensSubscriptionPresenter(view, mockedCustomerRepository.Object, _mockedSubscriptionRepository.Object) {HttpContext = mockedHttpContext.Object};

			//Act
			_saveEventArgs = new SaveSubscriptionEventArgs
			{
				AccountNumber = "123456789",
                ClearingNumber = "1234",
                MonthlyAmount = 699.25M
			};

			presenter.View_Submit(null, _saveEventArgs);
		}

		[Test]
		public void Presenter_gets_expected_customer()
		{
			_mockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(c => c.CreatedDate.IsSameDay(DateTime.Now))));
			_mockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(c => c.Customer.Id.Equals(5))));
			_mockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(c => c.PaymentInfo.AccountNumber.Equals(_saveEventArgs.AccountNumber))));
			_mockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(c => c.PaymentInfo.ClearingNumber.Equals(_saveEventArgs.ClearingNumber))));
			_mockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(c => c.PaymentInfo.MonthlyAmount.Equals(_saveEventArgs.MonthlyAmount))));
			_mockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(c => c.Status.Equals(SubscriptionStatus.Created))));
		}
	}
}