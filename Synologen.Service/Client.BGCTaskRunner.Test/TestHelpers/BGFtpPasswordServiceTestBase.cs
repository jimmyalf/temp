using FakeItEasy;
using Spinit.Test;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.BGServer;
using Synologen.Service.Client.BGCTaskRunner.App.Services;

namespace Synologen.Service.Client.BGCTaskRunner.Test.TestHelpers
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