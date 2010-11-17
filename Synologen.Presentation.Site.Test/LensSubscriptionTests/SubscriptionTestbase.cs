using System;
using Moq;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Models.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Test.MockHelpers;

namespace Spinit.Wpc.Synologen.Presentation.Site.Test.LensSubscriptionTests
{
	[TestFixture]
	public abstract class SubscriptionTestbase
	{
		protected SubscriptionTestbase()
		{
			Context = () => { };
			Because = presenter => { throw new AssertionException("An action for Because has not been set!"); };
		}

		[SetUp]
		protected void SetUpTest()
		{
			MockedView = MvpHelpers.GetMockedView<IEditLensSubscriptionView, EditLensSubscriptionModel>();
			MockedSubscriptionRepository = new Mock<ISubscriptionRepository>();
			MockedSynologenMemberService = new Mock<ISynologenMemberService>();
			MockedHttpContext = new HttpContextMock();
			Presenter = new EditLensSubscriptionPresenter(MockedView.Object, MockedSubscriptionRepository.Object, MockedSynologenMemberService.Object)
			{
				HttpContext = MockedHttpContext.Object
			};
			Context();
			Because(Presenter);
		}

		protected Action Context;
		protected Action<EditLensSubscriptionPresenter> Because;
		protected Mock<IEditLensSubscriptionView> MockedView;
		protected Mock<ISubscriptionRepository> MockedSubscriptionRepository;
		protected HttpContextMock MockedHttpContext;
		protected Mock<ISynologenMemberService> MockedSynologenMemberService;
		protected EditLensSubscriptionPresenter Presenter;

		protected void AssertUsing(Action<IEditLensSubscriptionView> action)
		{
			action(MockedView.Object);
		}

		protected TResult GetResult<TResult>(Func<IEditLensSubscriptionView, TResult> function)
		{
			return function(MockedView.Object);
		}
	}
}