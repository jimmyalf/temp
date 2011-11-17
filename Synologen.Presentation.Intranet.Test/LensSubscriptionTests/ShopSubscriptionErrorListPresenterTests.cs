using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Shouldly;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Intranet.Test.LensSubscriptionTests.Factories;
using Spinit.Wpc.Synologen.Presentation.Intranet.Test.LensSubscriptionTests.TestHelpers;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Test.LensSubscriptionTests
{
	[TestFixture]
	[Category("ShopSubscriptionErrorListPresenterTester")]
	public class When_loading_shop_subscription_error_list_view : ShopSubscriptionErrorListTestbase
	{
		private readonly IList<SubscriptionError> _expectedErrors;
		private readonly string _expectedSubscriptionUrl;
		private readonly int _expectedSubscriptionPageId;
		private readonly int _expectedShopId;

		public When_loading_shop_subscription_error_list_view()
		{
			_expectedSubscriptionPageId = 8;
			_expectedShopId = 77;
			_expectedSubscriptionUrl = "/test/url";
			_expectedErrors = SubscriptionErrorFactory.GetList();

			Context = () =>
			{
				MockedView.SetupGet(x => x.SubscriptionPageId).Returns(_expectedSubscriptionPageId);
				MockedSynologenMemberService.Setup(x => x.GetPageUrl(It.IsAny<int>())).Returns(_expectedSubscriptionUrl);
				MockedSubscriptionErrorRepository.Setup(x => x.FindBy(It.IsAny<AllUnhandledSubscriptionErrorsForShopCriteria>())).Returns(_expectedErrors);
				MockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(_expectedShopId);
			};

			Because = presenter => { presenter.View_Load(null, new EventArgs()); };
		}

		[Test]
		public void Model_has_expected_number_of_errors()
		{
			AssertUsing( view => view.Model.UnhandledErrors.Count().ShouldBe(_expectedErrors.Count()));
		}

		[Test]
		public void Model_has_expected_errors()
		{
			AssertUsing( view => view.Model.UnhandledErrors.And(_expectedErrors).Do( (viewItem, domainItem) =>
			{
				viewItem.CreatedDate.ShouldBe(domainItem.CreatedDate.ToString("yyyy-MM-dd"));
				viewItem.CustomerName.ShouldBe(domainItem.Subscription.Customer.ParseName(x => x.FirstName, x => x.LastName));
				viewItem.Reason.ShouldBe(domainItem.Type.GetEnumDisplayName());
				viewItem.SubscriptionLink.ShouldBe(String.Format("{0}?subscription={1}", _expectedSubscriptionUrl, domainItem.Subscription.Id));
			}));
		}

		[Test]
		public void Presenter_fetches_expected_subscription_url_from_service()
		{
			MockedSynologenMemberService.Verify(x => x.GetPageUrl(
				It.Is<int>(pageId => pageId.Equals(_expectedSubscriptionPageId))
			));
		}

		[Test]
		public void Presenter_fetches_expected_errors_from_repository()
		{
			MockedSubscriptionErrorRepository.Verify(x => x.FindBy(It.Is<AllUnhandledSubscriptionErrorsForShopCriteria>( 
				criteria => criteria.ShopId.Equals(_expectedShopId)
			)));
		}
	}

	[TestFixture]
	[Category("ShopSubscriptionErrorListPresenterTester")]
	public class When_loading_shop_subscription_error_list_view_without_subscription_page_set : ShopSubscriptionErrorListTestbase
	{
		private readonly IList<SubscriptionError> _expectedErrors;

		public When_loading_shop_subscription_error_list_view_without_subscription_page_set()
		{
			_expectedErrors = SubscriptionErrorFactory.GetList();

			Context = () =>
			{
				MockedSubscriptionErrorRepository.Setup(x => x.FindBy(It.IsAny<AllUnhandledSubscriptionErrorsForShopCriteria>())).Returns(_expectedErrors);
			};

			Because = presenter => { presenter.View_Load(null, new EventArgs()); };
		}

		[Test]
		public void Model_has_default_subscription_url()
		{
			AssertUsing( view => view.Model.UnhandledErrors.And(_expectedErrors).Do((viewItem, domainItem) => viewItem.SubscriptionLink.ShouldBe(String.Format("{0}?subscription={1}", "#", domainItem.Subscription.Id))));
		}
	}
}