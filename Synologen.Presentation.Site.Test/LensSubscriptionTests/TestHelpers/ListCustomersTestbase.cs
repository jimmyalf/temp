using Moq;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Models.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Test.TestHelpers;

namespace Spinit.Wpc.Synologen.Presentation.Site.Test.LensSubscriptionTests.TestHelpers
{
	public abstract class ListCustomersTestbase : PresenterTestbase<ListCustomersPresenter, IListCustomersView, ListCustomersModel>
	{
		protected Mock<ICustomerRepository> MockedCustomerRepository;
		protected Mock<ISynologenMemberService> MockedSynologenMemberService;

		protected ListCustomersTestbase()
		{
			SetUp = () =>
			{
				MockedCustomerRepository = new Mock<ICustomerRepository>();
				MockedSynologenMemberService = new Mock<ISynologenMemberService>();
			};

			GetPresenter = () => 
			{
				return new ListCustomersPresenter(MockedView.Object, MockedCustomerRepository.Object, MockedSynologenMemberService.Object);
			};
		}
	}
}