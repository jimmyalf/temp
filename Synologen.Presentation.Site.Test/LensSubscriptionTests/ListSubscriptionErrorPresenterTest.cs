using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using NUnit.Framework;
using Moq;
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
	[Category("SubscriptionErrorListPresenterTester")]
	public class When_loading_subscription_error_list_view
	{

		private readonly IListSubscriptionErrorView _view;
		private readonly SubscriptionError[] _errorList;

		public When_loading_subscription_error_list_view()
		{

			// Arrange
			const int customerId = 5;
			const int shopId = 5;
			const int subscriptionId = 5;
			var mockedHttpContext = new Mock<HttpContextBase>();
			mockedHttpContext.SetupGet(x => x.Request.Params).Returns(new NameValueCollection { { "subscription", subscriptionId.ToString() } });

			var subscription = SubscriptionFactory.GetWithErrors(CustomerFactory.Get(customerId, shopId));

			_errorList = subscription.Errors.ToArray();

			var view = new Mock<IListSubscriptionErrorView>();
			view.SetupGet(x => x.Model).Returns(new ListSubscriptionErrorModel());
			_view = view.Object;

			var subscriptionRepository = new Mock<ISubscriptionRepository>();
			subscriptionRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(subscription);

			var subscriptionErrorRepository = new Mock<ISubscriptionErrorRepository>();

			var presenter = new ListSubscriptionErrorsPresenter(view.Object, 
																subscriptionErrorRepository.Object,
																subscriptionRepository.Object) { HttpContext = mockedHttpContext.Object }; ;
				
			//Act
			presenter.View_Load(null, new EventArgs());

		}

		[Test]
		public void Model_should_have_expected_values()
		{
			Func<SubscriptionError, SubscriptionErrorListItemModel> errorConverter = error =>
				new SubscriptionErrorListItemModel
				{
					CreatedDate = error.CreatedDate.ToString("yyyy-MM-dd"),
					TypeName = error.Type.GetEnumDisplayName(),
					HandledDate = error.HandledDate.HasValue ? error.HandledDate.Value.ToString("yyyy-MM-dd") : string.Empty,
					ErrorId = error.Id.ToString(),
					IsVisible = error.IsHandled ? false : true
				};

			var modelListItems = _errorList.Select(errorConverter);

			_view.Model.List.Count().ShouldBe(6);
			_view.Model.HasErrors.ShouldBe(true);
			for (var i = 0; i < _errorList.Length; i++)
			{
				
				_view.Model.List.ToArray()[i].CreatedDate.ShouldBe(modelListItems.ToArray()[i].CreatedDate);
				_view.Model.List.ToArray()[i].TypeName.ShouldBe(modelListItems.ToArray()[i].TypeName);
				_view.Model.List.ToArray()[i].HandledDate.ShouldBe(modelListItems.ToArray()[i].HandledDate);
				_view.Model.List.ToArray()[i].ErrorId.ShouldBe(modelListItems.ToArray()[i].ErrorId);
				_view.Model.List.ToArray()[i].IsVisible.ShouldBe(modelListItems.ToArray()[i].IsVisible);
			}
		}
	}

	[TestFixture]
	[Category("SubscriptionErrorListPresenterTester")]
	public class When_error_is_set_as_handled
	{
		private readonly Mock<ISubscriptionErrorRepository> _subscriptionErrorRepository;
		private readonly SetErrorHandledEventArgs _updateArgs;

		private const int _errorId = 4;

		public When_error_is_set_as_handled()
		{
			// Arrange
			const int countryId = 1;
			const int customerId = 5;
			const int shopId = 5;
			const int subscriptionId = 5;
			var mockedHttpContext = new Mock<HttpContextBase>();
			mockedHttpContext.SetupGet(x => x.Request.Params).Returns(new NameValueCollection { { "subscription", subscriptionId.ToString() } });

			var subscription = SubscriptionFactory.GetWithErrors(CustomerFactory.Get(customerId, shopId));

			var view = new Mock<IListSubscriptionErrorView>();
			view.SetupGet(x => x.Model).Returns(new ListSubscriptionErrorModel());

			var subscriptionRepository = new Mock<ISubscriptionRepository>();
			subscriptionRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(subscription);

			_subscriptionErrorRepository = new Mock<ISubscriptionErrorRepository>();
			_subscriptionErrorRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(
									SubscriptionErrorFactory.Get(
												SubscriptionFactory.Get(subscriptionId,
																		CustomerFactory.Get(customerId, countryId, shopId)
																		), _errorId));

			subscriptionRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(subscription);

			var presenter = new ListSubscriptionErrorsPresenter(view.Object,
																_subscriptionErrorRepository.Object,
																subscriptionRepository.Object) { HttpContext = mockedHttpContext.Object }; ;

			_updateArgs = new SetErrorHandledEventArgs { ErrorId = _errorId };

			presenter.View_Load(null, new EventArgs());
			presenter.View_SetHandled(null, _updateArgs);
		}


		[Test]
		public void Presenter_gets_expected_error_id()
		{
			_subscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(t => t.Id.Equals(_updateArgs.ErrorId))));
		}
	}


}
