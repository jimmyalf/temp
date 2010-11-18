using Moq;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Models.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Test.TestHelpers;

namespace Spinit.Wpc.Synologen.Presentation.Site.Test.LensSubscriptionTests.TestHelpers
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