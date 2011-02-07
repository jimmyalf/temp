using System;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;
using Spinit.Wpc.Synologen.LensSubscriptionServiceCoordinator.Tasks;

namespace Synologen.ServiceCoordinator.Test.TestHelpers
{
	public abstract class SendPaymentsTaskTestBase : TaskTestBase
	{
		protected override ITask GetTask()
		{
			return new SendPaymentsTask(
				MockedWebServiceClient.Object,
				MockedSubscriptionRepository.Object,
				LoggingService);
		}
	}
}
