using FakeItEasy;
using Spinit.Test;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.BGServer;
using Synologen.LensSubscription.BGServiceCoordinator.App.Services;

namespace Synologen.LensSubscription.BGService.Test.TestHelpers
{
	public class BGFtpPasswordServiceTestBase : BehaviorActionTestbase<BGFtpPasswordService>
	{
		protected IBGFtpPasswordRepository BGFtpPasswordRepository;

		protected override void SetUp()
		{
			base.SetUp();
			BGFtpPasswordRepository = A.Fake<IBGFtpPasswordRepository>();
		}
		protected override BGFtpPasswordService GetTestEntity() 
		{
			return new BGFtpPasswordService(BGFtpPasswordRepository);
		}
	}
}