using Moq;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Models.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Test.TestHelpers;

namespace Spinit.Wpc.Synologen.Presentation.Site.Test.LensSubscriptionTests.TestHelpers
{
	public abstract class ListSubscriptionErrorsTestbase : PresenterTestbase<ListSubscriptionErrorsPresenter,IListSubscriptionErrorView,ListSubscriptionErrorModel>
	{
		protected Mock<ISubscriptionRepository> MockedSubscriptionRepository;
		protected Mock<ISubscriptionErrorRepository> MockedSubscriptionErrorRepository;

		protected ListSubscriptionErrorsTestbase()
		{
			SetUp = () =>
			{
				MockedSubscriptionRepository = new Mock<ISubscriptionRepository>();
				MockedSubscriptionErrorRepository = new Mock<ISubscriptionErrorRepository>();
			};

			GetPresenter = () => 
			{
				return new ListSubscriptionErrorsPresenter(
					MockedView.Object,
					MockedSubscriptionErrorRepository.Object,
					MockedSubscriptionRepository.Object);
			};
		}


	}
}