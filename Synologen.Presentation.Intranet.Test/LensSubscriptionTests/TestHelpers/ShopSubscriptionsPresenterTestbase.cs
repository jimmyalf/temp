using FakeItEasy;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Intranet.Test.TestHelpers;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Test.LensSubscriptionTests.TestHelpers
{
	public abstract class ShopSubscriptionsPresenterTestbase : PresenterTestbase<ShopSubscriptionsPresenter, IShopSubscriptionsView, ShopSubscriptionsModel>
	{
		protected ISubscriptionRepository SubscriptionRepository;
		protected ISynologenMemberService SynologenMemberService;
		protected IRoutingService RoutingService;
		protected ShopSubscriptionsPresenterTestbase()
		{
			SetUp = () =>
			{
				SubscriptionRepository = A.Fake<ISubscriptionRepository>();
				SynologenMemberService = A.Fake<ISynologenMemberService>();
				RoutingService = A.Fake<IRoutingService>();
			};

			GetPresenter = () => new ShopSubscriptionsPresenter(View, SubscriptionRepository, SynologenMemberService, RoutingService);
		}

	}
}