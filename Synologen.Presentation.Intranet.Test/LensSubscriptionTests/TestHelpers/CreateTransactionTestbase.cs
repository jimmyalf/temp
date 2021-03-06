using Moq;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Intranet.Test.TestHelpers;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Test.LensSubscriptionTests.TestHelpers
{
	public abstract class CreateTransactionTestbase : PresenterTestbase<CreateTransactionPresenter, ICreateTransactionView, CreateTransactionModel>
	{
		protected Mock<ITransactionRepository> MockedTransactionRepository;
		protected Mock<ISubscriptionRepository> MockedSubscriptionRepository;
		protected Mock<ITransactionArticleRepository> MockedTransactionArticleRepository;

		protected CreateTransactionTestbase()
		{
			SetUp = () =>
			{
				MockedTransactionRepository = new Mock<ITransactionRepository>();
				MockedSubscriptionRepository = new Mock<ISubscriptionRepository>();
				MockedTransactionArticleRepository = new Mock<ITransactionArticleRepository>();
			};

			GetPresenter = () =>
			{
				return new CreateTransactionPresenter(
					View, 
					MockedTransactionRepository.Object,
					MockedSubscriptionRepository.Object, 
					MockedTransactionArticleRepository.Object);
			};
		}


	}
}