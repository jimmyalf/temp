using Moq;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Intranet.Test.TestHelpers;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Test.LensSubscriptionTests.TestHelpers
{
	public abstract class EditCustomerTestbase : PresenterTestbase<EditCustomerPresenter,IEditCustomerView,EditCustomerModel>
	{
		protected Mock<ICustomerRepository> MockedCustomerRepository;
		protected Mock<ICountryRepository> MockedCountryRepository;
		protected Mock<ISynologenMemberService> MockedSynologenMemberService;

		protected EditCustomerTestbase()
		{
			SetUp = () =>
			{
				MockedCustomerRepository = new Mock<ICustomerRepository>();
				MockedCountryRepository = new Mock<ICountryRepository>();
				MockedSynologenMemberService = new Mock<ISynologenMemberService>();
			};

			GetPresenter = () => 
			{
				return new EditCustomerPresenter(
					MockedView.Object,
					MockedCustomerRepository.Object,
					MockedCountryRepository.Object,
					MockedSynologenMemberService.Object);
			};

		}
	}
}