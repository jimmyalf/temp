using Moq;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Models.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Test.TestHelpers;

namespace Spinit.Wpc.Synologen.Presentation.Site.Test.LensSubscriptionTests.TestHelpers
{
	public abstract class CreateCustomerTestbase : PresenterTestbase<CreateCustomerPresenter,ICreateCustomerView,CreateCustomerModel>
	{
		protected Mock<ICustomerRepository> MockedCustomerRepository;
		protected Mock<ICountryRepository> MockedCountryRepository;
		protected Mock<ISynologenMemberService> MockedSynologenMemberService;
		protected Mock<IShopRepository> MockedShopRepository;

		protected CreateCustomerTestbase()
		{
			SetUp = () =>
			{
				MockedCustomerRepository = new Mock<ICustomerRepository>();
				MockedCountryRepository = new Mock<ICountryRepository>();
				MockedSynologenMemberService = new Mock<ISynologenMemberService>();
				MockedShopRepository = new Mock<IShopRepository>();
			};

			GetPresenter = () => 
			{
				return new CreateCustomerPresenter(
					MockedView.Object,
					MockedCustomerRepository.Object,
					MockedShopRepository.Object,
					MockedCountryRepository.Object,
					MockedSynologenMemberService.Object);
			};
		}

	}
}