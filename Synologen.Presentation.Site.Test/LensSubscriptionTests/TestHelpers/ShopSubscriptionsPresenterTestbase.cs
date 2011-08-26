using FakeItEasy;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Models.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Test.TestHelpers;

namespace Spinit.Wpc.Synologen.Presentation.Site.Test.LensSubscriptionTests.TestHelpers
{
	public abstract class ShopSubscriptionsPresenterTestbase : PresenterTestbase<ShopSubscriptionsPresenter, IShopSubscriptionsView, ShopSubscriptionsModel>
	{
		protected ISubscriptionRepository SubscriptionRepository;
		protected ISynologenMemberService SynologenMemberService;
		protected ShopSubscriptionsPresenterTestbase()
		{
			SetUp = () =>
			{
				SubscriptionRepository = A.Fake<ISubscriptionRepository>();
				SynologenMemberService = A.Fake<ISynologenMemberService>();
			};

			GetPresenter = () => 
			{
				return new ShopSubscriptionsPresenter(MockedView.Object, SubscriptionRepository, SynologenMemberService);
			};
		}

	}
}