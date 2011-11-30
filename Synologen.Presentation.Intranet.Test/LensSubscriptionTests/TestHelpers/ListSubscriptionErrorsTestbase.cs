using Moq;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Intranet.Test.TestHelpers;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Test.LensSubscriptionTests.TestHelpers
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

			GetPresenter = () => new ListSubscriptionErrorsPresenter(
			                     	View,
			                     	MockedSubscriptionErrorRepository.Object,
			                     	MockedSubscriptionRepository.Object);
		}


	}
}