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
		protected Mock<ITransactionRepository> MockedTransactionRepository;

		protected ListTransactionsTestbase()
		{
			SetUp = () =>
			{
				MockedTransactionRepository = new Mock<ITransactionRepository>();
			};

			GetPresenter = () => 
			{
				return new ListTransactionsPresenter(MockedView.Object, MockedTransactionRepository.Object);
			};
		}
	}
}