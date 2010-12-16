using Moq;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Models.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Test.TestHelpers;

namespace Spinit.Wpc.Synologen.Presentation.Site.Test.LensSubscriptionTests.TestHelpers
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
					MockedView.Object, 
					MockedTransactionRepository.Object,
					MockedSubscriptionRepository.Object, 
					MockedTransactionArticleRepository.Object);
			};
		}


	}
}