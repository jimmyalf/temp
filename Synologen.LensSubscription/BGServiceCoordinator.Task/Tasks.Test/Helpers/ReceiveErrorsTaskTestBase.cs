using FakeItEasy;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.Test.Helpers
{
	public abstract class ReceiveErrorsTaskTestBase : TaskTestBase
	{
		protected IBGReceivedErrorRepository BGReceivedErrorRepository;

		protected ReceiveErrorsTaskTestBase()
		{
			BGReceivedErrorRepository = A.Fake<IBGReceivedErrorRepository>();
		}

		protected override ITask GetTask()
		{
			return new ReceiveConsents.ReceiveErrors.Task(Log4NetLogger);
		}
	}
}