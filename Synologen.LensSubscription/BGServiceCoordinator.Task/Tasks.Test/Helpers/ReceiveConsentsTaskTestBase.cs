using FakeItEasy;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.Test.Helpers
{
    public abstract class ReceiveConsentsTaskTestBase : TaskTestBase
    {
    	protected IBGReceivedConsentRepository BGReceivedConsentRepository;
    	protected IAutogiroFileReader<ConsentsFile, Consent> ConsentFileReader;

    	protected ReceiveConsentsTaskTestBase()
    	{
    		BGReceivedConsentRepository = A.Fake<IBGReceivedConsentRepository>();
			ConsentFileReader = A.Fake<IAutogiroFileReader<ConsentsFile, Consent>>();
    	}

    	protected override ITask GetTask()
        {
            return new ReceiveConsents.Task(
                Log4NetLogger,
                ReceivedFileRepository,
                BGReceivedConsentRepository,
                ConsentFileReader);
        }
    }
}
