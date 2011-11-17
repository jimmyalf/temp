using Moq;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Intranet.Test.TestHelpers;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Test.LensSubscriptionTests.TestHelpers
{
	public abstract class CreateSubscriptionTestbase : PresenterTestbase<CreateLensSubscriptionPresenter, ICreateLensSubscriptionView, CreateLensSubscriptionModel>
	{
		protected Mock<ISubscriptionRepository> MockedSubscriptionRepository;
		protected Mock<ICustomerRepository> MockedCustomerRepository;
		protected Mock<ISynologenMemberService> MockedSynologenMemberService;

		protected CreateSubscriptionTestbase()
		{
			SetUp = () =>
			{
				MockedSubscriptionRepository = new Mock<ISubscriptionRepository>();
				MockedCustomerRepository = new Mock<ICustomerRepository>();
				MockedSynologenMemberService = new Mock<ISynologenMemberService>();
			};

			GetPresenter = () =>
			{
				return new CreateLensSubscriptionPresenter(
					MockedView.Object,
					MockedCustomerRepository.Object,
					MockedSubscriptionRepository.Object,
					MockedSynologenMemberService.Object);
			};
		}
	}
}