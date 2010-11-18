using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Moq;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.EventArguments.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Test.LensSubscriptionTests.Factories;
using Spinit.Wpc.Synologen.Presentation.Site.Test.LensSubscriptionTests.TestHelpers;

namespace Spinit.Wpc.Synologen.Presentation.Site.Test.LensSubscriptionTests
{
	[TestFixture]
	[Category("SubscriptionErrorListPresenterTester")]
	public class When_loading_subscription_error_list_view : ListSubscriptionErrorsTestbase
	{
		private readonly IList<SubscriptionError> _errorList;

		public When_loading_subscription_error_list_view()
		{
			const int customerId = 5;
			const int shopId = 5;
			const int subscriptionId = 5;
			var subscription = SubscriptionFactory.GetWithErrors(CustomerFactory.Get(customerId, shopId));
			_errorList = subscription.Errors.ToList();

			Context = () =>
			{
				MockedSubscriptionRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(subscription);
				MockedHttpContext.SetupSingleQuery("subscription",subscriptionId.ToString());
			};

			Because = presenter => presenter.View_Load(null, new EventArgs());
		}

		[Test]
		public void Model_should_have_expected_values()
		{
			AssertUsing( view =>
			{
				view.Model.List.Count().ShouldBe(_errorList.Count());
				view.Model.HasErrors.ShouldBe(true);
				view.Model.List.For((index,errorItem) =>
				{
					errorItem.CreatedDate.ShouldBe(_errorList.ElementAt(index).CreatedDate.ToString("yyyy-MM-dd"));
					errorItem.TypeName.ShouldBe(_errorList.ElementAt(index).Type.GetEnumDisplayName());
					errorItem.HandledDate.ShouldBe(_errorList.ElementAt(index).HandledDate.HasValue ? _errorList.ElementAt(index).HandledDate.Value.ToString("yyyy-MM-dd") : String.Empty);
					errorItem.ErrorId.ShouldBe(_errorList.ElementAt(index).Id.ToString());
					errorItem.IsVisible.ShouldBe(!_errorList.ElementAt(index).IsHandled);
				});
			});
		}
	}

	[TestFixture]
	[Category("SubscriptionErrorListPresenterTester")]
	public class When_error_is_set_as_handled : ListSubscriptionErrorsTestbase
	{
		private const int _errorId = 4;

		public When_error_is_set_as_handled()
		{
			const int customerId = 5;
			const int shopId = 5;
			const int subscriptionId = 5;
			var customer = CustomerFactory.Get(customerId, shopId);
			var subscriptionWithErrors = SubscriptionFactory.GetWithErrors(customer);
			var subscriptionError =  SubscriptionErrorFactory.Get(subscriptionWithErrors, _errorId);

			Context = () =>
			{
				MockedHttpContext.SetupSingleQuery("subscription", subscriptionId.ToString());
				MockedSubscriptionErrorRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(subscriptionError);
				MockedSubscriptionRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(subscriptionWithErrors);
			};

			Because = presenter =>
			{
				presenter.View_Load(null, new EventArgs());
				presenter.View_SetHandled(null, new SetErrorHandledEventArgs { ErrorId = _errorId });
			};
		}

		[Test]
		public void Presenter_gets_expected_error_id()
		{
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(t => t.Id.Equals(_errorId))));
		}
	}


}
