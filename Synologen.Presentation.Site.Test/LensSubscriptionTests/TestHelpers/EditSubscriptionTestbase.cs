using Moq;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Models.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Test.TestHelpers;

namespace Spinit.Wpc.Synologen.Presentation.Site.Test.LensSubscriptionTests.TestHelpers
{
	public abstract class EditSubscriptionTestbase : PresenterTestbase<EditLensSubscriptionPresenter,IEditLensSubscriptionView,EditLensSubscriptionModel>
	{
		protected Mock<ISubscriptionRepository> MockedSubscriptionRepository;
		protected Mock<ISynologenMemberService> MockedSynologenMemberService;

		protected EditSubscriptionTestbase()
		{
			SetUp = () =>
			{
				MockedSubscriptionRepository = new Mock<ISubscriptionRepository>();
				MockedSynologenMemberService = new Mock<ISynologenMemberService>();
			};

			GetPresenter = () => 
			{
				return new EditLensSubscriptionPresenter(MockedView.Object,MockedSubscriptionRepository.Object,MockedSynologenMemberService.Object);
			};
		}
	}
}