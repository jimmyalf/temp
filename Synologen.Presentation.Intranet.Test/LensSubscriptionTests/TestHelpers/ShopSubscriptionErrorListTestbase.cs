using FakeItEasy;
using Moq;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Intranet.Test.TestHelpers;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Test.LensSubscriptionTests.TestHelpers
{
	public abstract class ShopSubscriptionErrorListTestbase : PresenterTestbase<ShopSubscriptionErrorListPresenter, IShopSubscriptionErrorListView, ShopSubscriptionErrorListModel>
	{
		protected Mock<ISynologenMemberService> MockedSynologenMemberService;
		protected Mock<ISubscriptionErrorRepository> MockedSubscriptionErrorRepository;
		protected IRoutingService RoutingService;

		protected ShopSubscriptionErrorListTestbase()
		{
			SetUp = () =>
			{
				MockedSynologenMemberService = new Mock<ISynologenMemberService>();
				MockedSubscriptionErrorRepository = new Mock<ISubscriptionErrorRepository>();
				RoutingService = A.Fake<IRoutingService>();
			};

			GetPresenter = () => new ShopSubscriptionErrorListPresenter(
			                     	View,
			                     	MockedSynologenMemberService.Object,
			                     	MockedSubscriptionErrorRepository.Object,
			                     	RoutingService
			                     	);
		}
	}
}