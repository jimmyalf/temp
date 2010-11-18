using Moq;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Models.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Test.TestHelpers;

namespace Spinit.Wpc.Synologen.Presentation.Site.Test.LensSubscriptionTests.TestHelpers
{
	public abstract class ListTransactionsTestbase : PresenterTestbase<ListTransactionsPresenter, IListTransactionView, ListTransactionModel>
	{
		protected Mock<ISubscriptionRepository> MockedSubscriptionRepository;

		protected ListTransactionsTestbase()
		{
			SetUp = () =>
			{
				MockedSubscriptionRepository = new Mock<ISubscriptionRepository>();
			};

			GetPresenter = () => 
			{
				return new ListTransactionsPresenter(MockedView.Object, MockedSubscriptionRepository.Object);
			};
		}
	}
}