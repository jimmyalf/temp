using FakeItEasy;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.Test.Helpers
{
    public abstract class ReceiveConsentsTaskTestBase : CommonTaskTestBase
    {
    	protected IBGReceivedConsentRepository BGReceivedConsentRepository;
    	protected IAutogiroFileReader<ConsentsFile, Consent> ConsentFileReader;

		protected override void SetUp()
		{
			base.SetUp();
			BGReceivedConsentRepository = A.Fake<IBGReceivedConsentRepository>();
			ConsentFileReader = A.Fake<IAutogiroFileReader<ConsentsFile, Consent>>();
			TaskRepositoryResolver.AddRepository(BGReceivedConsentRepository);
			//A.CallTo(() => TaskRepositoryResolver.GetRepository<IBGReceivedConsentRepository>()).Returns(BGReceivedConsentRepository);
		}

    	protected override ITask GetTask()
        {
            return new ReceiveConsents.Task(
                Log4NetLogger,
                //ReceivedFileRepository,
                //BGReceivedConsentRepository,
                ConsentFileReader,
				TaskRepositoryResolver);
        }
    }
}
