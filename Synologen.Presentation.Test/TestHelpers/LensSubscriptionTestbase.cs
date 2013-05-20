using Moq;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Application.Services;
using Spinit.Wpc.Synologen.Presentation.Controllers;

namespace Spinit.Wpc.Synologen.Presentation.Test.TestHelpers
{
	public class LensSubscriptionTestbase<TViewModel> : ControllerTestbase<LensSubscriptionController,TViewModel> where TViewModel : class
	{
		protected Mock<ISubscriptionRepository> MockedSubscriptionRepository;
		protected Mock<IAdminSettingsService> MockedAdminSettingsService;
		protected Mock<ITransactionArticleRepository>  MockedTransactionArticleRepository;
		private ILensSubscriptionViewService ViewService;

		protected LensSubscriptionTestbase()
		{
			SetUp = () => 
			{
				MockedSubscriptionRepository = new Mock<ISubscriptionRepository>();
				MockedAdminSettingsService = new Mock<IAdminSettingsService>();
				MockedTransactionArticleRepository = new Mock<ITransactionArticleRepository>();
				ViewService = new LensSubscriptionViewService(MockedSubscriptionRepository.Object, MockedTransactionArticleRepository.Object);
			};
			GetController = () => new LensSubscriptionController(ViewService, MockedAdminSettingsService.Object, MockedSubscriptionRepository.Object);
		}
	}
}