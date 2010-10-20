using System;
using System.Collections.Specialized;
using System.Web;
using Moq;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Models.LensSubscription;

namespace Spinit.Wpc.Synologen.Presentation.Site.Test.LensSubscriptionTests
{
	[TestFixture]
	[Category("SubscriptionRepositoryTester")]
	public class When_loading_create_subscription_view 
	{
		protected CreateLensSubscriptionPresenter presenter;
		private readonly ICreateLensSubscriptionView view;


		public When_loading_create_subscription_view()
		{
			//Arrange
			var httpContext = new Mock<HttpContextBase>();
			httpContext.SetupGet(x => x.Request.Params).Returns(new NameValueCollection{{"customer","5"}});
			var mockedView = new Mock<ICreateLensSubscriptionView>();
			mockedView.SetupGet(x => x.Model).Returns(new CreateLensSubscriptionModel());
			view = mockedView.Object;
			presenter = new CreateLensSubscriptionPresenter(view) {HttpContext = httpContext.Object};

			//Act
			presenter.View_Load(null,new EventArgs());
		}

		[Test]
		public void Model_should_have_expected_values()
		{
			view.Model.CustomerId.ShouldBe(5);
		}
	}
}