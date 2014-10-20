using Moq;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Intranet.Test.TestHelpers;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Test.LensSubscriptionTests.TestHelpers
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

			GetPresenter = () => new ListTransactionsPresenter(View, MockedTransactionRepository.Object);
		}
	}
}