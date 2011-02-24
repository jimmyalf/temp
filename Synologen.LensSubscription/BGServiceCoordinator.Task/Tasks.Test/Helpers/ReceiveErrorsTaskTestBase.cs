using FakeItEasy;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.Test.Helpers
{
	public abstract class ReceiveErrorsTaskTestBase : CommonTaskTestBase
	{
		protected IBGReceivedErrorRepository BGReceivedErrorRepository;
        protected IAutogiroFileReader<ErrorsFile, Error> ErrorFileReader;

		protected ReceiveErrorsTaskTestBase()
		{
			BGReceivedErrorRepository = A.Fake<IBGReceivedErrorRepository>();
		    ErrorFileReader = A.Fake<IAutogiroFileReader<ErrorsFile, Error>>();
		}

		protected override ITask GetTask()
		{
			return new ReceiveErrors.Task(
                                        Log4NetLogger,
                                        ReceivedFileRepository,
                                        BGReceivedErrorRepository,
                                        ErrorFileReader);
		}
	}
}