using Moq;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Models.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Test.TestHelpers;

namespace Spinit.Wpc.Synologen.Presentation.Site.Test.LensSubscriptionTests.TestHelpers
{
	public abstract class ShopSubscriptionErrorListTestbase : PresenterTestbase<ShopSubscriptionErrorListPresenter, IShopSubscriptionErrorListView, ShopSubscriptionErrorListModel>
	{
		protected Mock<ISynologenMemberService> MockedSynologenMemberService;
		protected Mock<ISubscriptionErrorRepository> MockedSubscriptionErrorRepository;

		protected ShopSubscriptionErrorListTestbase()
		{
			SetUp = () =>
			{
				MockedSynologenMemberService = new Mock<ISynologenMemberService>();
				MockedSubscriptionErrorRepository = new Mock<ISubscriptionErrorRepository>();
			};

			GetPresenter = () => 
			{
				return new ShopSubscriptionErrorListPresenter(
					MockedView.Object,
					MockedSynologenMemberService.Object,
					MockedSubscriptionErrorRepository.Object
				);
			};
		}
	}
}