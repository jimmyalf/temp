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
	public abstract class ListCustomersTestbase : PresenterTestbase<ListCustomersPresenter, IListCustomersView, ListCustomersModel>
	{
		protected Mock<ICustomerRepository> MockedCustomerRepository;
		protected Mock<ISynologenMemberService> MockedSynologenMemberService;
		protected IRoutingService RoutingService;

		protected ListCustomersTestbase()
		{
			SetUp = () =>
			{
				MockedCustomerRepository = new Mock<ICustomerRepository>();
				MockedSynologenMemberService = new Mock<ISynologenMemberService>();
				RoutingService = A.Fake<IRoutingService>();
			};

			GetPresenter = () => new ListCustomersPresenter(View, MockedCustomerRepository.Object, MockedSynologenMemberService.Object, RoutingService);
		}
	}
}